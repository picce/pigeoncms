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
    /// static class to manage LogItem
    /// </summary>
    public static class LogProvider
    {
        public static bool AppUseLog 
        { 
            get
            {
                bool res = false;
                bool.TryParse(AppSettingsManager.GetValue("UseLog"), out res);
                return res;
            } 
        }

        #region public methods

        /// <summary>
        /// write a LogItem in logItems table
        /// </summary>
        /// <param name="module">current module</param>
        /// <param name="description">the event log description</param>
        /// <param name="type">the event log type. default TracerItemType.Info</param>
        public static void Write(PigeonCms.Module module, string description, TracerItemType type)
        {
            if (module.UseLog == Utility.TristateBool.True ||
                (module.UseLog == Utility.TristateBool.NotSet && LogProvider.AppUseLog))
            {
                var item = new LogItem();
                var man = new LogItemsManager();

                item.ModuleId = module.Id;
                item.Type = type;
                try
                {
                    //sometimes throw NullReferenceException
                    item.UserHostAddress = HttpContext.Current.Request.UserHostAddress;
                }
                catch { }
                try
                {
                    //sometimes throw NullReferenceException
                    item.SessionId = HttpContext.Current.Session.SessionID;
                }
                catch { }
                //item.Url = HttpContext.Current.Request.RawUrl;    //parte finale
                item.Url = Utility.Html.GetTextPreview(HttpContext.Current.Request.Url.AbsoluteUri, 495, ""); //all url
                item.Description = Utility.Html.GetTextPreview(description, 495, "");

                man.Insert(item);
            }
        }

        /// <summary>
        /// write a LogItem in logItems table. default TracerItemType.Info
        /// </summary>
        public static void Write(PigeonCms.Module module, string description)
        {
            Write(module, description, TracerItemType.Info);
        }

        #endregion
    }
}