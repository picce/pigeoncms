using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Web.Caching;
using System.Collections.Generic;
using PigeonCms;

public partial class PigeonCms_VideoPlayer_wmvplayer : PigeonCms.VideoPlayerControl
{
    protected string LblContent = "";


    protected void Page_Load(object sender, EventArgs e)
    {
        string viewFolder = Config.ModulesPath + this.BaseModule.ModuleFullName +
            "/views/" + this.BaseModule.CurrViewFolder;

        Utility.Script.RegisterClientScriptInclude(this, "silverlight", ResolveUrl(viewFolder + "/silverlight.js"));
        Utility.Script.RegisterClientScriptInclude(this, "wmvplayer", ResolveUrl(viewFolder + "/wmvplayer.js"));

        LblContent = "<script type='text/javascript'>\n";
	    LblContent += "var cnt = document.getElementById('mediaspace');\n";
	    LblContent += "var src = '"+ viewFolder +"/wmvplayer.xaml';\n";
        LblContent += "var cfg = {\n"
        + "file:'"+ base.File +"',\n"
        + "height:'"+ base.Width +"',\n"
        + "width:'"+ base.Height +"'\n"
        + "};\n";
        LblContent += "var ply = new jeroenwijering.Player(cnt,src,cfg);\n";
        LblContent += "</script>";
    }
}
