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
using System.Collections.Generic;
using System.IO;
using PigeonCms;


namespace PigeonCms.Core
{
    /// <summary>
    /// used to parse item templates
    /// </summary>
    public class ItemTemplateType: XmlType
    {
        public ItemTemplateType() { }

    }


    [Serializable]
    public class ItemTemplateTypeFilter : XmlTypeFilter { }
}