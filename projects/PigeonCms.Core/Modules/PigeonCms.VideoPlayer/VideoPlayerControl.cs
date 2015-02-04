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
    public class VideoPlayerControl: PigeonCms.BaseModuleControl
    {
        #region private fields
        private string file = "";
        private string width = "320";
        private string height = "240";
        #endregion


        #region public fields

        public string File
        {
            get { return GetStringParam("File", file); }
            set { file = value; }
        }

        public string Width
        {
            get { return GetStringParam("Width", width); }
            set { width = value; }
        }

        public string Height
        {
            get { return GetStringParam("Height", height); }
            set { height = value; }
        }

        #endregion
    }
}