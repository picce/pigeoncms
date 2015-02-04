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


namespace PigeonCms
{
    public class ItemType: XmlType
    {
        private List<FormField> _fields = new List<FormField>();

        public ItemType() { }

        public List<FormField> Fields
        {
            [DebuggerStepThrough()]
            get { return _fields; }
            [DebuggerStepThrough()]
            set { _fields = value; }
        }
    }


    [Serializable]
    public class ItemTypeFilter: XmlTypeFilter { }
}