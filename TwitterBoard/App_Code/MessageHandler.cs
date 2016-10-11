using System;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Web.Script.Serialization;
using System.Collections.Generic;
using System.Diagnostics;

namespace TwitterBoard {
   public class MessageHandler : IHttpHandler {
      // Connect information:
      private const string SQL_CONNECT_STRING = "{your connect string}";
      private const string SQL_TABLE          = "{your table}";

      // Here are the required columns in SQL_TABLE:
      protected struct MessageEntry {
         public Int64 ID;        // 'bigint',        non-null, primary key
         public string UserFrom; // 'nvarchar(MAX)', non-null
         public string Message;  // 'nvarchar(140)', non-null
      }

      public bool IsReusable { get { return false; }}
      public void ProcessRequest (HttpContext context) {
         // Log debug stuff.
         int threadID = System.Threading.Thread.CurrentThread.ManagedThreadId;
         Debug.WriteLine (threadID + ": New connection");

         // Get our HTTP connection.
         HttpResponse Response = context.Response;
         Response.ContentType = "text/event-stream";

         // Some preperation for our loop coming up.
         Int64 nextId = 0;
         string topString = "TOP(25) ";
         var js = new JavaScriptSerializer ();
         SqlConnection connection = null;

         // Send out data until it doesn't work anymore.
         int pollCount = 1;
         while (Response.IsClientConnected) {
            Debug.WriteLine (threadID + ": Polling, #" + pollCount++ + ".");
            LinkedList<MessageEntry> entries = new LinkedList<MessageEntry>();
            try {
               try {
                  // Make sure we have a connection to the SQL server.
                  SqlConnect (ref connection);

                  string query = "SELECT " + topString +
                     "ID,UserFrom,Message FROM " + SQL_TABLE + " WHERE ID>=" +
                     nextId + " ORDER BY ID DESC";
                  topString = "";
                  SqlCommand cmd = new SqlCommand (query, connection);
                  SqlDataReader reader = cmd.ExecuteReader ();

                  while (reader.Read ()) {
                     MessageEntry entry = new MessageEntry ();
                     entry.ID       = reader.GetInt64 (0);
                     entry.UserFrom = reader[1].ToString ();
                     entry.Message  = reader[2].ToString ();
                     entries.AddFirst (entry);
                  }
               }
               catch (SqlException e) {
                  MessageEntry entry = new MessageEntry ();
                  entry.ID = -1;
                  entry.UserFrom = "Error";
                  entry.Message = e.Message;
                  entries.AddFirst (entry);
                  connection = null;
               }

               // The connection wants to be closed in most circumstances, so close it manually.
               if (connection != null) {
                  connection.Close ();
                  connection = null;
               }

               if (entries.Count > 0) {
                  Debug.WriteLine (threadID + ": Read " + entries.Count + " rows.");
                  foreach (var entry in entries) {
                     Response.Write ("data: " + js.Serialize (entry) + "\n\n");
                     if (entry.ID > -1)
                     nextId = entry.ID + 1;
                  }

                  // There appears to be a bug where HttpReponse.Flush() refuses to send all data
                  // out.  This force the rest of the data through at the cost of some valuable
                  // bandwidth.
                  Response.Write ("data: {" + new String (' ', 256) + "}\n\n");
                  Response.Flush ();
               }

               // Wait at least one second before trying again.
               System.Threading.Thread.Sleep (1000);
            }
            // Sometimes our connection is closed from the other side. In that case, bail now.
            catch (HttpException e) {
               Debug.WriteLine (threadID + ": HttpException - connection closed. (" + e.Message + ")");
               Response.End ();
               return;
            }
         }

         // Looks like our client disappeared.
         Debug.WriteLine (threadID + ": Client left.");
      }

      static public string SqlConnect (ref SqlConnection connection) {
         // Create our connection if it doesn't exist yet.
         if (connection == null)
            connection = new SqlConnection (SQL_CONNECT_STRING);

         // Don't do anything unless we're closed.
         if (connection.State != ConnectionState.Closed)
            return null;

         // Attempt to open the connection.
         connection.Open ();

         // Looks like it worked!  Return 'null' to indicate success.
         return null;
      }
   }
}