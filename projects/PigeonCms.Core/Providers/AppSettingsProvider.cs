using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Diagnostics;
using System.Web.Configuration;
using System.Collections.Specialized;
using System.Data.Common;
using System.Text;
using System.Collections.Generic;
using System.Web.Caching;
using PigeonCms.Core.Helpers;

namespace PigeonCms
{
    /// <summary>
    /// manager AppSettings with PigeonCms.AppSettingsManager2 class
    /// </summary>
    public class AppSettingsProvider
    {
        const string APPCACHE_PREFIX = "Pgn.AppSettingsProvider__";

        private string keySet = "";
        public string KeySet
        {
            get { return this.keySet; }
        }

        private AppSettingsManager2 appSettingsMan;
        private ApplicationManager<string> appCache;
        private PigeonCms.Module fakeModule;

        public AppSettingsProvider(string keySet)
        {
            if (string.IsNullOrEmpty(keySet))
                throw new ArgumentException("Missing KeySet", "keySet");

            this.keySet = keySet;

            appSettingsMan = new AppSettingsManager2();

            appCache = new ApplicationManager<string>(APPCACHE_PREFIX + this.KeySet);

            fakeModule = new PigeonCms.Module();
            fakeModule.UseLog = Utility.TristateBool.True;
            fakeModule.ModuleNamespace = "PigeonCms.Core";
            fakeModule.ModuleName = "AppSettingsProvider";
        }

        /// <summary>
        /// retrieve AppSetting Value from cache.
        /// If not exists, retrieve from db.
        /// If not exists insert with default value
        /// </summary>
        /// <returns></returns>
        public string GetValue(string keyName, string defaultValue = "")
        {
            string result = "";
            if (appCache.IsEmpty(keyName))
            {
                var item = appSettingsMan.GetByKey(this.KeySet, keyName);
                if (string.IsNullOrEmpty(item.KeyName))
                {
                    //not inserted yet in db
                    item.KeySet = this.KeySet;
                    item.KeyName = keyName;
                    item.KeyTitle = keyName;//TODO from xml
                    item.KeyInfo = "SYSTEM";//TODO from xml
                    item.KeyValue = defaultValue;
                    
                    item = appSettingsMan.Insert(item);

                    LogProvider.Write(fakeModule, 
                        "Insert AppSetting(keySet="+ item.KeySet +"|keyName="+ item.KeyName +"|keyValue="+ item.KeyValue +")", 
                        TracerItemType.Debug);
                }
                appCache.Insert(item.KeyName, item.KeyValue);
                result = item.KeyValue;
            }
            else
            {
                result = appCache.GetValue(keyName);
            }

            if (string.IsNullOrEmpty(result))
                result = defaultValue;

            return result;
        }

        /// <summary>
        /// invalidate cache of current keyName
        /// </summary>
        public void Invalidate(string keyName)
        {
            appCache.Remove(keyName);
        }

        /// <summary>
        /// invalidate cache and reload values from db
        /// </summary>
        public void Refresh()
        {
            appCache.Clear();

            var list = appSettingsMan.GetByKeySet(this.KeySet);
            foreach (var item in list)
            {
                appCache.Insert(item.KeyName, item.KeyValue);
            }
        }

        /// <summary>
        /// invalidate all AppCache
        /// </summary>
        public static void InvalidateAll()
        {
            var appCache = new ApplicationManager<string>(APPCACHE_PREFIX);
            appCache.Clear();
        }

    }
}