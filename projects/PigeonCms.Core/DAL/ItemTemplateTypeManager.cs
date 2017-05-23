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
using System.Xml;
using PigeonCms;


namespace PigeonCms.Core
{
    public class ItemTemplateTypeManager : XmlTypeManager<ItemTemplateType, ItemTemplateTypeFilter>
    {
        public ItemTemplateTypeManager() : base(Config.ItemsPath)
        {
            //specific xml parse steps
        }
    }
}