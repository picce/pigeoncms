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

public partial class Modules_Fsmenu : PigeonCms.TopMenuControl
{
    protected string LblContent = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        //base.Page_Load(sender, e);

        Utility.Script.RegisterClientScriptInclude(this, "fsmenu", 
            ResolveUrl(Config.ModulesPath + this.BaseModule.ModuleFullName + 
            "/views/" + this.BaseModule.CurrViewFolder + "/fsmenu.js"));

        LblContent = base.GetContent();
    }
}
