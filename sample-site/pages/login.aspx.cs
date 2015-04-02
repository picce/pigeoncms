using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PigeonCms;
using System.Web.Security;


public partial class _login : BasePage
{
    Module fakeModule = new Module();

    protected void Page_Load(object sender, EventArgs e)
    {
        fakeModule.UseCache = Utility.TristateBool.True;
    }

    protected void CmdLogin_Click(object sender, EventArgs e)
    {
        LitRes.Text = "";
        const string LOG_STACK = "login.aspx > CmdLogin_Click(): ";

        try
        {
            if (Membership.ValidateUser(TxtUser.Text, TxtPassword.Text))
            {
                //PgnUser user = (PgnUser)Membership.GetUser(TxtUser.Text, true);
                PgnUser user = PgnUserCurrent.GetUser(TxtUser.Text);
                if (user.Enabled && user.IsApproved)
                {
                    FormsAuthentication.RedirectFromLoginPage(user.UserName, false);
                    LogProvider.Write(fakeModule, LOG_STACK + TxtUser.Text + " logged in");

                    string returnUrl = "";
                    if (Request["ReturnUrl"] != null)  //querystring param
                    {
                        returnUrl = Request["ReturnUrl"].ToString();
                    }
                    if (!string.IsNullOrEmpty(returnUrl))
                        Response.Redirect(returnUrl, false);
                    else
                        Response.Redirect("/private", false);
                    Context.ApplicationInstance.CompleteRequest();
                }
                else
                {
                    LogProvider.Write(fakeModule, LOG_STACK + TxtUser.Text + " is not enabled", TracerItemType.Warning);
                    LitRes.Text = "Invalid login";
                }
            }
            else
            {
                LitRes.Text = "Invalid login";
                LogProvider.Write(fakeModule, LOG_STACK + TxtUser.Text + " invalid login", TracerItemType.Warning);
            }
        }
        catch (Exception ex)
        {
            LitRes.Text = ex.ToString();
            LogProvider.Write(fakeModule, LOG_STACK +  TxtUser.Text + " login error: " + ex.ToString(), TracerItemType.Error);
        }
    }
}