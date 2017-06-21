using PigeonCms;
using System;
using System.Web.UI;
using Newtonsoft.Json.Linq;
using System.Web;
using System.IO;

namespace PigeonCms.Controls
{
    /// <summary>
    /// interface for pigeon\controls\PageComposer\PageComposer.ascx
    /// </summary>
    public class PageComposerHelper
	{
        public static void IncludeJsFileContent(Page page)
        {
            string url = "~/Controls/Pagecomposer/assets/js/app.js";
            string content = File.ReadAllText(HttpContext.Current.Server.MapPath(url));

            Utility.Script.RegisterStartupScript(page, $"pagecomposer-app", content);
        }

        public static string GetCssFilesContent()
        {
            string url = "~/Controls/Pagecomposer/assets/css/application.css";
            string content = File.ReadAllText(HttpContext.Current.Server.MapPath(url));

            string res = $@"
                <style>
                /*pagecomposer css include*/
                {content}
                </style>
                ";

            return res;
        }

        public static void RegisterScripts(Control control)
        {
            JObject pageComposerSettings = new JObject
            {
                { "sources", new JArray { "element:input[id=aq_pagecomposer_value]" } },
                { "targets", new JArray { "element:input[id=aq_pagecomposer_value]" } },
                { "endpoints", new JObject {
                        { "getPreview", "/Controls/ImageUpload/PageComposerUploadHandler.ashx?action=previewurl" },
                        { "upload", "/Controls/ImageUpload/PageComposerUploadHandler.ashx" },
                        { "delete", "/Controls/ImageUpload/PageComposerUploadHandler.ashx?action=delete" },
                    }
                }
            };

            PigeonCms.Utility.Script.RegisterStartupScript(control, "aq_pagecomposer_start", @"
                try 
                {       
                    console.log('aq_pagecomposer_start');
                    window.AQuest.PageComposer.init(" + pageComposerSettings.ToString() + @");
                }
                catch (exc)
                {
                    console.log('aq_pagecomposer_start > DOMContentLoaded');
                    document.addEventListener('DOMContentLoaded', function () {
                        window.AQuest.PageComposer.init(" + pageComposerSettings.ToString() + @");
                    });
                }
                ");
        }
    }
}
