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

public partial class Controls_LoginPgnLogged : PigeonCms.LoginFormControl
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
                PgnUser user = (PgnUser)Membership.GetUser(TxtUser.Text, true);
                if (user.Enabled && user.IsApproved)
                {
                    FormsAuthentication.RedirectFromLoginPage(user.UserName, false);
                    LogProvider.Write(this.BaseModule, TxtUser.Text + " logged in");

                    string redirUrl = "";
                    if (Request["ReturnUrl"] != null)  //querystring param
                        redirUrl = Request["ReturnUrl"].ToString();
                    else if (Request["aspxerrorpath"] != null)
                        redirUrl = Request["aspxerrorpath"].ToString();
                    else
                        redirUrl = base.RedirectUrl;

                    if (!string.IsNullOrEmpty(redirUrl))
                    {
                        Response.Redirect(redirUrl, false);
                        Context.ApplicationInstance.CompleteRequest();
                    }
                }
                else
                {
                    LogProvider.Write(this.BaseModule, TxtUser.Text + " is not enabled", TracerItemType.Warning);
                    LblErrore = Resources.PublicLabels.LblInvalidLogin;
                }
            }
            else
            {
                LblErrore = base.GetLabel("LblInvalidLogin", "Invalid username or password.");
                LogProvider.Write(this.BaseModule, TxtUser.Text + "-"+ TxtPassword.Text + " invalid login", TracerItemType.Warning);
            }
        }
        catch (Exception ex)
        {
            LblErrore = ex.ToString();
        }
    }
}
