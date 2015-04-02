using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PigeonCms;
using System.Web.Security;

public partial class puppets : System.Web.UI.MasterPage
{
    protected BasePage CurrPage;
    protected string MenuLogout = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        CurrPage = (BasePage)this.Page;


        if (Request.QueryString["act"] == "logout")
            logout();
        if (Request.QueryString["act"] == "timeout")
            logout();

        if (PgnUserCurrent.IsAuthenticated)
        {
            MenuLogout = "<a href='/default.aspx?act=logout'>Logout " + PgnUserCurrent.UserName + "</a>";
        }
    }

    private void logout()
    {
        if (PgnUserCurrent.IsAuthenticated)
        {
            FormsAuthentication.SignOut();
            Response.Redirect(Request.Url.ToString());  //cookie removed on second request
        }
    }
}
