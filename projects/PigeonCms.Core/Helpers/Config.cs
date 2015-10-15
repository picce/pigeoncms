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
        /// from 2015 jul 29
        /// true: pigeon is bundle inside its own folder; you can create your website using pigeon framework
        /// false: old pigeon structure. The website is built only with pigeon
        /// </summary>
        public static bool IsPigeonBundle
        {
            get 
            {
                bool res = false;
                string value = getConfigValue("IsPigeonBundle");
                if (value.ToLower() == "true")
                    res = true;
                return res;
            }
        }

        /// <summary>
        /// pigeon backend path 
        /// </summary>
        public static string PigeonAdminPath
        {
            get { return "~/pgn-admin/"; }
        }

        /// <summary>
        /// pigeon frontend content path 
        /// </summary>
        public static string PigeonContentPath
        {
            get { return "~/pgn-content/"; }
        }

        /// <summary>
        /// folder with installed modules
        /// </summary>
        public static string ModulesPath
        {
            get { return (IsPigeonBundle ? PigeonAdminPath + "modules/" : getConfigValue("ModulesPath")); }
        }

        /// <summary>
        /// folder with settings meta info
        /// </summary>
        public static string SettingsPath
        {
            get { return (IsPigeonBundle ? PigeonAdminPath + "settings/" : "~/settings/"); }
        }

        /// <summary>
        /// path for installation wizard
        /// </summary>
        public static string InstallationPath
        {
            get { return (IsPigeonBundle ? PigeonAdminPath + "installation/" : getConfigValue("InstallationPath")); }
        }

        /// <summary>
        /// folder with installed items 
        /// </summary>
        public static string ItemsPath
        {
            get { return (IsPigeonBundle ? PigeonAdminPath + "items/" : getConfigValue("ItemsPath")); }
        }

        /// <summary>
        /// folder with installed Masterpages files (*.master)
        /// </summary>
        public static string MasterPagesPath
        {
            get { return (IsPigeonBundle ? PigeonAdminPath + "masterpages/" : getConfigValue("MasterPagesPath")); }
        }

        /// <summary>
        /// folder log files
        /// </summary>
        public static string LogsPath
        {
            get { return (IsPigeonBundle ? PigeonAdminPath + "logs/" : getConfigValue("LogsPath", "~/Logs/")); }
        }

        /// <summary>
        /// folder for vendor plugins
        /// </summary>
        public static string VendorPath
        {
            get { return (IsPigeonBundle ? PigeonAdminPath + "vendor/" : getConfigValue("VendorPath", "~/Plugins/")); }
        }

        /// <summary>
        /// folder for temporary files
        /// ~/public/temp/
        /// </summary>
        public static string TempGenericPath
        {
            get { return "~/public/temp/"; }
        }

        /// <summary>
        /// folder for user session temporary files
        /// ~/public/temp/<sessionID>/
        /// </summary>
        public static string TempUserSessionPath
        {
            get { return TempGenericPath + Utility._SessionID() + "/"; }
        }


        public static string DocsPublicPath
        {
            get { return getConfigValue("DocsPublicPath"); }
        }

        public static string SessionTimeOutUrl
        {
            get { return getConfigValue("SessionTimeOutUrl"); }
        }

        public static int DefaultCacheValue
        {
            get { return int.Parse(getConfigValue("defaultCacheValue")); }
        }

        /// <summary>
        /// culture code in web.config 
        /// used as default culture in tranlations
        /// </summary>
        public static string CultureDefault
        {
            get { return getConfigValue("CultureDefault"); }
        }

        /// <summary>
        /// development culture in web.config
        /// used as default culture for automatic resources insert (example: labels)
        /// If not present, the default value is Config.CultureDefault
        /// </summary>
        public static string CultureDev
        {
            get { return getConfigValue("CultureDev", CultureDefault); }
        }

        /// <summary>
        /// used by fckeditr js plugin
        /// </summary>
        [Obsolete("", true)]
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
            get { return AppSettingsManager.GetValue("CurrentMasterPage", "default"); }
        }

        /// <summary>
        /// get application default theme
        /// </summary>
        public static string CurrentTheme
        {
            get { return AppSettingsManager.GetValue("CurrentTheme", "default"); }
        }

        public static string GetConfigValue(string key, string defaultValue = "")
        {
            return getConfigValue(key, defaultValue);
        }

        private static string getConfigValue(string key, string defaultValue = "")
        {
            string res = "";

            if (ConfigurationManager.AppSettings[key] != null)
                res = ConfigurationManager.AppSettings[key].ToString();

            if (string.IsNullOrEmpty(res))
                res = defaultValue;

            return res;
        }
    } 
}
