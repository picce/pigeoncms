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


public partial class Controls_LoginPigeonModernAdmin : PigeonCms.LoginFormControl
{
    public string LblErrore = "";


    protected string SiteTitle
    {
        get
        {
            string res = AppSettingsManager.GetValue("MetaSiteTitle");
            return res;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            TxtUser.Attributes.Add("placeholder", base.GetLabel("Username", "Username").ToUpper());
            TxtPassword.Attributes.Add("placeholder", base.GetLabel("Password", "Password").ToUpper());
            LitRememberMe.Text = base.GetLabel("RememberMe", "Remember me");
        }
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
                    FormsAuthentication.RedirectFromLoginPage(user.UserName, ChkRememberMe.Checked);
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
                    LblErrore = RenderError(Resources.PublicLabels.LblInvalidLogin);
                }
            }
            else
            {
                LblErrore = RenderError(base.GetLabel("LblInvalidLogin", "Invalid username or password."));
                LogProvider.Write(this.BaseModule, TxtUser.Text + "-"+ TxtPassword.Text + " invalid login", TracerItemType.Warning);
            }
        }
        catch (Exception ex)
        {
            LblErrore = ex.ToString();
        }
    }
}
