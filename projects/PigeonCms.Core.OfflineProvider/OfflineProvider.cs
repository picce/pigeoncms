using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Diagnostics;
using System.Collections.Specialized;
using System.Data.Common;
using System.Text;
using System.Collections.Generic;
using System.IO;


namespace PigeonCms.Core.Offline
{
    /// <summary>
    /// static class to switch website offline
    /// </summary>
    public static class OfflineProvider
    {
        public static string XmlFilePath
        {
            [DebuggerStepThrough()]
            get { return "~/Offline/default.xml"; }
        }

        public static string TemplatesFolder
        {
            [DebuggerStepThrough()]
            get { return "~/Offline/templates"; }
        }

        public static string OfflineFolder
        {
            [DebuggerStepThrough()]
            get { return "~/Offline"; }
        }

        public static string OfflineFilename
        {
            [DebuggerStepThrough()]
            get { return "offline.eve"; }
        }

        public static bool IsOffline
        {
            //[DebuggerStepThrough()]
            get
            {
                bool res = false;
                if (HttpContext.Current.Application["IsOffline"] != null)
                {
                    res = (bool)HttpContext.Current.Application["IsOffline"];
                }
                else
                {
                    var offline = new OfflineManager();
                    offline.GetData();
                    res = offline.Offline;
                    HttpContext.Current.Application["IsOffline"] = res;
                }
                return res;
            }
        }

        /// <summary>
        /// remove Application["IsOffline"] var. 
        /// </summary>
        public static void ResetOfflineStatus()
        {
            HttpContext.Current.Application.Remove("IsOffline");
        }

        public static List<string> GetTemplatesList()
        {
            List<string> result = new List<string>();
            string filespath = HttpContext.Current.Request.MapPath(OfflineProvider.TemplatesFolder);
            DirectoryInfo dir = new DirectoryInfo(filespath);

            if (dir.Exists)
            {
                FileInfo[] files = dir.GetFiles("*.xsl");
                foreach (FileInfo file in files)
                {
                    string item = file.Name.Replace(".xsl", "");
                    result.Add(item);
                }
            }
            return result;
        }
    }
}
