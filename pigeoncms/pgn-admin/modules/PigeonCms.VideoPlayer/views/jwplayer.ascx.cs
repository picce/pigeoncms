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

public partial class PigeonCms_VideoPlayer_jwplayer : PigeonCms.VideoPlayerControl
{
    protected string LblContent = "";


    protected void Page_Load(object sender, EventArgs e)
    {
        string viewFolder = Config.ModulesPath + this.BaseModule.ModuleFullName +
            "/views/" + this.BaseModule.CurrViewFolder;

        Utility.Script.RegisterClientScriptInclude(this, "swfobject", ResolveUrl(viewFolder + "/swfobject.js"));

        LblContent = "<script type='text/javascript'>\n";
        LblContent += "var so = new SWFObject('"+ ResolveUrl(viewFolder +"/player.swf") +"','mpl','"+ base.Width +"','"+ base.Height +"','9');\n";
        LblContent += "so.addParam('allowfullscreen','true');\n";
        LblContent += "so.addParam('allowscriptaccess','always');\n";
        LblContent += "so.addParam('wmode','opaque');\n";
        LblContent += "so.addVariable('file','"+ base.File +"');\n";
        LblContent += "//so.addVariable('image','/js/jw-player/preview.jpg');\n";
        LblContent += "so.addVariable('volume','50');\n";
        LblContent += "so.addVariable('autostart','true');\n";
        LblContent += "so.write('mediaspace');\n";
        LblContent += "</script>";
    }
}
