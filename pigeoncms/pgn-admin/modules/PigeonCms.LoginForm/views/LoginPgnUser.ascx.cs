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

public partial class Controls_LoginPgnUser : PigeonCms.LoginFormControl
{
    public string LblErrore = "";
    

    protected void Page_Load(object sender, EventArgs e)
    {
    }

    protected void CmdLogin_Click(object sender, EventArgs e)
    {
        LblErrore = "";
        
        try
        {
            if (Membership.ValidateUser(TxtUser.Text, TxtPassword.Text))
            {
                //PgnUser user = (PgnUser)Membership.GetUser(TxtUser.Text, true);
                PgnUser user = PgnUserCurrent.GetUser(TxtUser.Text);
                if (user.Enabled && user.IsApproved)
                {
                    FormsAuthentication.RedirectFromLoginPage(user.UserName, false);
                    LogProvider.Write(this.BaseModule, TxtUser.Text + " logged in");

                    string returnUrl = "";
                    if (Request["ReturnUrl"] != null)  //querystring param
                    {
                        returnUrl = Request["ReturnUrl"].ToString();
                    }
                    if (!string.IsNullOrEmpty(returnUrl))
                        Response.Redirect(returnUrl, false);
                    else
                        Response.Redirect(RedirectUrl, false);
                    Context.ApplicationInstance.CompleteRequest();
                }
                else
                {
                    LogProvider.Write(this.BaseModule, TxtUser.Text + " is not enabled", TracerItemType.Warning);
                    LblErrore = Resources.PublicLabels.LblInvalidLogin;
                }
            }
            else
            {
                LblErrore = Resources.PublicLabels.LblInvalidLogin;
                LogProvider.Write(this.BaseModule, TxtUser.Text + " invalid login", TracerItemType.Warning);
            }
        }
        catch (Exception ex)
        {
            //LblErrore = ex.Message;
            LblErrore = ex.ToString();
            LogProvider.Write(this.BaseModule, TxtUser.Text + " login error: " + ex.ToString(), TracerItemType.Error);
        }
    }
}
