using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.ComponentModel;
using System.Collections.Generic;
using System.IO;
using System.Data.Common;
using PigeonCms;
using System.Diagnostics;
using System.Web.Configuration;

namespace PigeonCms
{
    public class WebConfigEntry : ITable
    {
        string key = "";
        string value = "";

        /// <summary>
        /// Automatic Id as PKey
        /// </summary>
        [DataObjectField(true)]
        public string Key
        {
            [DebuggerStepThrough()]
            get { return key; }
            [DebuggerStepThrough()]
            set { key = value; }
        }

        public string Value
        {
            [DebuggerStepThrough()]
            get { return this.value; }
            [DebuggerStepThrough()]
            set { this.value = value; }
        }

        public WebConfigEntry() { }
    }

    [Serializable]
    public class WebConfigEntryFilter
    {
        string key = "";

        [DataObjectField(true)]
        public string Key
        {
            [DebuggerStepThrough()]
            get { return key; }
            [DebuggerStepThrough()]
            set { key = value; }
        }
    }

    /// <summary>
    /// DAL for Web.config appSettings section
    /// see http://www.aspnetpro.com/newsletterarticle/2007/02/asp200702jk_l/asp200702jk_l.asp
    /// </summary>
    public class WebConfigManager
    {
        [DebuggerStepThrough()]
        public WebConfigManager()
        {
        }

        public Dictionary<string, string> GetList()
        {
            Dictionary<string, string> res = new Dictionary<string, string>();
            WebConfigEntryFilter filter = new WebConfigEntryFilter();
            List<WebConfigEntry> list = GetByFilter(filter);
            foreach (WebConfigEntry item in list)
            {
                res.Add(item.Key, item.Value);
            }
            return res;
        }

        public List<WebConfigEntry> GetByFilter(WebConfigEntryFilter filter)
        {
            List<WebConfigEntry> result = new List<WebConfigEntry>();
            Configuration configuration = WebConfigurationManager.OpenWebConfiguration(HttpContext.Current.Request.ApplicationPath);
            AppSettingsSection appSettingsSection = (AppSettingsSection)configuration.GetSection("appSettings");
            if (appSettingsSection != null)
            {
                if (!string.IsNullOrEmpty(filter.Key))
                {
                    result.Add(GetByKey(filter.Key));
                }
                else
                {
                    foreach (string key in appSettingsSection.Settings.AllKeys)
                    {
                        WebConfigEntry item = new WebConfigEntry();
                        item.Key = key;
                        item.Value = appSettingsSection.Settings[key].Value;
                        result.Add(item);
                    }
                }
            }
            return result;
        }

        public WebConfigEntry GetByKey(string key)
        {
            WebConfigEntry result = new WebConfigEntry();
            Configuration configuration = WebConfigurationManager.OpenWebConfiguration(HttpContext.Current.Request.ApplicationPath);
            AppSettingsSection appSettingsSection = (AppSettingsSection)configuration.GetSection("appSettings");
            if (appSettingsSection != null)
            {
                if (!string.IsNullOrEmpty(key))
                {
                    result.Key = key;
                    result.Value = appSettingsSection.Settings[key].Value;
                }
            }
            return result;
        }

        public int Update(WebConfigEntry theObj)
        {
            int result = 0;
            Configuration configuration = WebConfigurationManager.OpenWebConfiguration(HttpContext.Current.Request.ApplicationPath);
            AppSettingsSection appSettingsSection = (AppSettingsSection)configuration.GetSection("appSettings");
            if (appSettingsSection != null)
            {
                appSettingsSection.Settings[theObj.Key].Value = theObj.Value;
                configuration.Save();
                result = 1;
            }
            return result;
        }

        public void Insert(WebConfigEntry newObj)
        {
            Configuration configuration = WebConfigurationManager.OpenWebConfiguration(HttpContext.Current.Request.ApplicationPath);
            AppSettingsSection appSettingsSection = (AppSettingsSection)configuration.GetSection("appSettings");
            if (appSettingsSection != null)
            {
                appSettingsSection.Settings.Add(newObj.Key, newObj.Value);
                configuration.Save();
            }
        }

        public int Delete(string key)
        {
            int res = 0;
            Configuration configuration = WebConfigurationManager.OpenWebConfiguration(HttpContext.Current.Request.ApplicationPath);
            AppSettingsSection appSettingsSection = (AppSettingsSection)configuration.GetSection("appSettings");
            if (appSettingsSection != null)
            {
                appSettingsSection.Settings.Remove(key);
                configuration.Save();
                res = 1;
            } 
            return res;
        }
    }
}