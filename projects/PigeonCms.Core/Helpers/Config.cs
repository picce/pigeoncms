using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;

namespace PigeonCms
{
    /// <summary>
    /// expose pigeoncms settings in web.config
    /// </summary>
    public class Config
    {

        /// <summary>
        /// add or not .aspx to each page in menus
        /// </summary>
        public static bool AddPageSuffix
        {
            get 
            {
                bool res = true;
                if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings["AddPageSuffix"]))
                {
                    bool.TryParse(
                        ConfigurationManager.AppSettings["AddPageSuffix"],
                        out res);
                }
                return res;
            }
        }

        /// <summary>
        /// db tables prefix (usually pgn_)
        /// </summary>
        public static string TabPrefix
        {
            get { return ConfigurationManager.AppSettings["TabPrefix"].ToString(); }
        }

        /// <summary>
        /// folder with installed modules
        /// </summary>
        public static string ModulesPath
        {
            get { return ConfigurationManager.AppSettings["ModulesPath"].ToString(); }
        }

        /// <summary>
        /// path for installation wizard
        /// </summary>
        public static string InstallationPath
        {
            get { return ConfigurationManager.AppSettings["InstallationPath"].ToString(); }
        }

        /// <summary>
        /// folder with installed items 
        /// </summary>
        public static string ItemsPath
        {
            get { return ConfigurationManager.AppSettings["ItemsPath"].ToString(); }
        }

        /// <summary>
        /// folder with installed Masterpages files (*.master)
        /// </summary>
        public static string MasterPagesPath
        {
            get { return ConfigurationManager.AppSettings["MasterPagesPath"].ToString(); }
        }

        public static string DocsPublicPath
        {
            get { return ConfigurationManager.AppSettings["DocsPublicPath"].ToString(); }
        }

        public static string SessionTimeOutUrl
        {
            get { return ConfigurationManager.AppSettings["SessionTimeOutUrl"].ToString(); }
        }

        public static int DefaultCacheValue
        {
            get { return int.Parse(ConfigurationManager.AppSettings["defaultCacheValue"]); }
        }

        public static string CultureDefault
        {
            get { return ConfigurationManager.AppSettings["CultureDefault"].ToString(); }
        }

        /// <summary>
        /// used by fckeditr js plugin
        /// </summary>
        public static string FCKUserFilesPath
        {
            get { return VirtualPathUtility.ToAbsolute(ConfigurationManager.AppSettings["FCKeditor:UserFilesPath"].ToString()); }
        }

        /// <summary>
        /// allowed site cultures
        /// </summary>
        public static Dictionary<string, string> CultureList
        {
            get
            {
                Dictionary<String, String> cultureList = new Dictionary<string, string>();

                if (HttpContext.Current.Application["CultureList"] != null)
                {
                    cultureList = (Dictionary<string, string>)Utility._Application("CultureList");
                }
                else
                {
                    new PigeonCms.CulturesManager().RefreshCultureList();
                    cultureList = (Dictionary<string, string>)Utility._Application("CultureList");
                }
                return cultureList;
            }
        }

        /// <summary>
        /// get application default masterpage
        /// </summary>
        public static string CurrentMasterPage
        {
            get
            {
                string res = "";
                res = AppSettingsManager.GetValue("CurrentMasterPage");
                if (string.IsNullOrEmpty(res))
                {
                    res = "default";
                }
                return res;
            }
        }

        /// <summary>
        /// get application default theme
        /// </summary>
        public static string CurrentTheme
        {
            get 
            { 
                string res = "";
                res = AppSettingsManager.GetValue("CurrentTheme");
                if (string.IsNullOrEmpty(res))
                {
                    res = "default";
                }
                return res;
            }
        }
    } 
}
