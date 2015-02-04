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
using System.ComponentModel;
using System.IO;
using PigeonCms;
using System.Collections.Generic;
using System.Threading;
using System.Web.Compilation;
using System.Reflection;



namespace PigeonCms
{
    /// <summary>
    /// map inerithed class field to base class customizable field
    /// </summary>
    public class RssAttribute : Attribute
    {
        public enum RssTags
        {
            Title,
            Link,
            Guid,
            Description,
            PubDate
        }

        public RssTags TagName { get; set; }

        public RssAttribute(RssTags tagName)
        {
            this.TagName = tagName;
        }
    }

    public class RssItem
    {
        
     

    }

}