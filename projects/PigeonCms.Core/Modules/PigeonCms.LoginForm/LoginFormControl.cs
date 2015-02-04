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
using System.Web.Routing;


namespace PigeonCms
{
    public class LoginFormControl: PigeonCms.BaseModuleControl
    {
        private string redirectUrl = "";

        public string RedirectUrl
        {
            get { return GetStringParam("RedirectUrl", redirectUrl); }
            set { redirectUrl = value; }
        }
    }
}