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
using PigeonCms;

public partial class Controls_LoginStatus : PigeonCms.BaseModuleControl
{
    public string LitContent = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        LitContent = "";
        if (!Page.IsPostBack)
        {
            if (PgnUserCurrent.IsAuthenticated)
            {
                LitContent = "<a href='" + VirtualPathUtility.ToAbsolute("~/") + "default.aspx?act=logout'>"
                + base.GetLabel("LblLogout", "logout")
                + "</a> "
                + PgnUserCurrent.UserName;
            }
        }
    }
}
