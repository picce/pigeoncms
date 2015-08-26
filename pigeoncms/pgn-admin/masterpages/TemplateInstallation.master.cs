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
using PigeonCms;


public partial class Installation_TemplateInstallation : MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //Utility.Html.StripTagsRegex("");
        Utility.Script.RegisterClientScriptInclude(this, "jquery", ResolveUrl("~/Js/jquery.js"));
        Utility.Script.RegisterClientScriptInclude(this, "jquery-ui", ResolveUrl("~/Js/jquery-ui.js"));
        //Utility.Script.RegisterClientScriptInclude(this, "fsmenu", ResolveUrl("~/Js/fsmenu/fsmenu.js"));

    }
}
