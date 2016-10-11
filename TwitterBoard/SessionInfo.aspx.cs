using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TwitterBoard {
   public partial class SessionInfo : System.Web.UI.Page {
      protected string sessionHtml () {
         // Start with a header.
         string html = "<h3>Session Info:</h3>\n";

         // If we don't have any session variables, say so and do nothing further.
         if (Session.Count == 0) {
            html += "<p>No session variables.</p>\n";
            return html;
         }

         // Build a list of our session variables.
         html += "<ul>";
         foreach (string key in Session)
            html += "<li><b>" + key + "</b>: " + Session[key] + "</li>\n";
         html += "</ul>\n";

         // Return our list of session variables.
         return html;
      }

      protected void Page_Load (object sender, EventArgs e) {
         sessionDiv.InnerHtml = sessionHtml ();
      }
   }
}