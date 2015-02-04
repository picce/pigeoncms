using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Caching;
using System.Diagnostics;
using System.Collections;
using System.Collections.Specialized;
using System.Web.UI;

namespace PigeonCms.Core.Helpers
{
    /// <summary>
    /// redirection helper methods
    /// </summary>
    public static class RedirHelper
    {
        /// <summary>
        /// POST data and Redirect to the specified url using the specified page.
        /// see http://www.codeproject.com/Articles/37539/Redirect-and-POST-in-ASP-NET
        /// </summary>
        /// <param name="page">The page which will be the referrer page.</param>
        /// <param name="destinationUrl">The destination Url to which
        /// the post and redirection is occuring.</param>
        /// <param name="data">The data should be posted.</param>
        public static void RedirectAndPOST(Page page, string destinationUrl, NameValueCollection data, string formId)
        {
            string strForm = preparePOSTForm(destinationUrl, data, formId);
            page.Controls.Add(new LiteralControl(strForm));
        }

        public static void RedirectAndPOST(Page page, string destinationUrl, NameValueCollection data)
        {
            RedirectAndPOST(page, destinationUrl, data, "");
        }

        /// <summary>
        /// This method prepares an Html form which holds all data
        /// in hidden field in the addetion to form submitting script.
        /// </summary>
        /// <param name="url">The destination Url to which the post and redirection
        /// will occur, the Url can be in the same App or ouside the App.</param>
        /// <param name="data">A collection of data that
        /// will be posted to the destination Url.</param>
        /// <returns>Returns a string representation of the Posting form.</returns>
        private static String preparePOSTForm(string url, NameValueCollection data, string formId)
        {
            if (string.IsNullOrEmpty(formId))
                formId = "PostForm";
            var strForm = new StringBuilder();
            strForm.Append("<form id=\"" + formId + "\" name=\"" + formId + "\" action=\"" + url + "\" method=\"POST\">");
            foreach (string key in data)
            {
                strForm.Append("<input type=\"hidden\" name=\"" + key + "\" value=\"" + data[key] + "\">");
            }
            strForm.Append("</form>");

            //Build the JavaScript which will do the Posting operation.
            var strScript = new StringBuilder();
            strScript.Append("<script language='javascript'>");
            strScript.Append("var v" + formId + " = document." + formId + ";");
            strScript.Append("v" + formId + ".submit();");
            strScript.Append("</script>");

            return strForm.ToString() + strScript.ToString();
        }
    }
}
