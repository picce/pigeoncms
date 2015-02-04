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
using System.Collections.Generic;
using System.Threading;
using System.Text;
using PigeonCms;


namespace PigeonCms
{
    public class LabelsAdminControl: PigeonCms.BaseModuleControl
    {
        private string moduleFullName = "";

        public string ModuleFullName
        {
            get { return GetStringParam("ModuleFullName", moduleFullName); }
            set { moduleFullName = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        { }
    }
}