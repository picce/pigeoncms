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
using PigeonCms;


namespace PigeonCms
{
    public class WrapperControl: PigeonCms.BaseModuleControl
    {
        #region private fields
        private string url = "";
        private int width = 0;
        private int height = 0;
        private string scrolling = "";
        private int frameborder = 0;
        private string cssStyle = "";
        private string headerText = "";
        private string footerText = "";
        #endregion


        #region public fields

        public string Url
        {
            get { return GetStringParam("Url", url); }
            set { url = value; }
        }

        public int Width
        {
            get { return GetIntParam("Width", width); }
            set { width = value; }
        }

        public int Height
        {
            get { return GetIntParam("Height", height); }
            set { height = value; }
        }

        public string Scrolling
        {
            get { return GetStringParam("Scrolling", scrolling); }
            set { scrolling = value; }
        }

        public int Frameborder
        {
            get { return GetIntParam("Frameborder", frameborder); }
            set { frameborder = value; }
        }

        public string CssStyle
        {
            get { return GetStringParam("CssStyle", cssStyle); }
            set { cssStyle = value; }
        }

        public string HeaderText
        {
            get { return GetStringParam("HeaderText", headerText); }
            set { headerText = value; }
        }

        public string FooterText
        {
            get { return GetStringParam("FooterText", footerText); }
            set { footerText = value; }
        }
        
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
        }
    }
}