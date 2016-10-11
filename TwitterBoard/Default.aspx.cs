using System;
using System.Web;
using System.Web.UI;
using System.Web.Services;
using System.Data;
using System.Data.SqlClient;

namespace TwitterBoard {
   public partial class _Default : Page {
      [WebMethod(EnableSession = true)]
      public static string PostTweet (string message) {
         // Error checking.
         if (message == null)
            return "Error: No message";
         else if (message.Length >= 140)
            return "Error: Message too long (" + message.Length + " / 140)";
         else if (HttpContext.Current == null)
            return "Error: No HttpContext.Current";
         else if (HttpContext.Current.User == null)
            return "Error: No HttpContext.Current.User";
         else if (HttpContext.Current.User.Identity == null)
            return "Error: No HttpContext.Current.Identity";
         else if (!HttpContext.Current.User.Identity.IsAuthenticated)
            return "Error: User is not authenticated";

         // Attempt to get our connection. Return an error if it didn't work.
         SqlConnection connection = null;
         string error;
         if ((error = MessageHandler.SqlConnect (ref connection)) != null)
            return "Error: Unable to connect:\n" + error;

         // We're connected! Build our query.
         int rows = 0;
         try {
            string query = "INSERT INTO dbo.Messages(Message,UserFrom) VALUES(@param1,@param2)";
            SqlCommand cmd = new SqlCommand (query, connection);
            cmd.Parameters.Add ("@param1", SqlDbType.NVarChar, 140).Value = message;
            cmd.Parameters.Add ("@param2", SqlDbType.Text).Value = HttpContext.Current.User.Identity.Name;
            rows = cmd.ExecuteNonQuery ();
         }
         catch (Exception e) {
            connection.Close ();
            return "Error: SQL Query Exception:\n" + e.Message;
         }

         // Looks like everything worked.
         return "Success: " + rows + " row(s) affected";
      }

      protected void Page_Load (object sender, EventArgs e) {
         Session["User"] = "Simon";
      }
   }
}