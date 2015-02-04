using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Reflection;
using PigeonCms;

namespace PigeonCms
{
    /// <summary>
    /// Useful static functions to manage pages
    /// </summary>
    public static class PageHelper
    {
        public static void SetHtmlMeta(HtmlMeta meta, string content)
        {
            meta.Content = content;
        }

        public static void SetHeaderTitle(HtmlHead header, string title)
        {
            if (string.IsNullOrEmpty(title))
                title = AppSettingsManager.GetValue("MetaSiteTitle");

            header.Title = title;
        }

        [Obsolete("Add and use meta placeholder in masterpage")]
        public static void AddDefaultMetaTags(HtmlHead header)
        {
            SetHeaderTitle(header, ""); 

            HtmlMeta meta = new HtmlMeta();
            meta.Name = "title";
            meta.Content = AppSettingsManager.GetValue("MetaSiteTitle");
            header.Controls.Add(meta);

            meta = new HtmlMeta();
            meta.Name = "description";
            meta.Content = AppSettingsManager.GetValue("MetaDescription");
            header.Controls.Add(meta);

            meta = new HtmlMeta();
            meta.Name = "keywords";
            meta.Content = AppSettingsManager.GetValue("MetaKeywords");
            header.Controls.Add(meta);

        }
    }
}
