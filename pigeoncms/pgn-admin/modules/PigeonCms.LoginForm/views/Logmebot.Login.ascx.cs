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


public partial class Controls_Logmebot_Login : PigeonCms.LoginFormControl
{
    public string LblErrore = "";
    private LogMeBot.LogMeBotClient logMeBotClient;


    private string appClientId = "";
    public string AppClientId
    {
        get { return GetStringParam("AppClientId", appClientId); }
        set { appClientId = value; }
    }

    private string appClientSecret = "";
    public string AppClientSecret
    {
        get { return GetStringParam("AppClientSecret", appClientSecret); }
        set { appClientSecret = value; }
    }

    private string appCallbackUri = "";
    public string AppCallbackUri
    {
        get { return GetStringParam("AppCallbackUri", appCallbackUri); }
        set { appCallbackUri = value; }
    }

    private bool enableUserRegistration = false;
    public bool EnableUserRegistration
    {
        get { return GetBoolParam("EnableUserRegistration", enableUserRegistration); }
        set { enableUserRegistration = value; }
    }

    private string defaultRoles = "";
    public string DefaultRoles
    {
        get { return GetStringParam("DefaultRoles", defaultRoles); }
        set { defaultRoles = value; }
    }

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
        if (string.IsNullOrEmpty(this.AppClientId)
            || string.IsNullOrEmpty(this.AppClientSecret)
            || string.IsNullOrEmpty(this.AppCallbackUri))
        {
            LblErrore = RenderError("Invalid Logmebot app settings");
            return;
        }

        logMeBotClient = new LogMeBot.LogMeBotClient(
            this.AppClientId,
            this.AppClientSecret,
            this.AppCallbackUri);

        try
        {
            if (Request.Params["code"] != null)
            {
                if (string.IsNullOrEmpty(logMeBotClient.AccessToken))
                {
                    string token = logMeBotClient.GetAccessToken(
                        Request.Params["code"], Request.Params["state"]);
                }
            }

            if (!string.IsNullOrEmpty(logMeBotClient.AccessToken))
            {
                var me = logMeBotClient.GetMe(logMeBotClient.AccessToken);
                LogProvider.Write(this.BaseModule, "LogMeBot provider Authorized. Username:{username};Email:{email}"
                    .Replace("{username}", me.Username)
                    .Replace("{email}", me.Email));

                var user = new PgnUser();
                var userMetaMan = new PgnUserMetaManager();

                var userMeta = userMetaMan.GetByMetaKeyValue("oauth_logmebot", 
                    "{username}|{email}"
                    .Replace("{username}", me.Username)
                    .Replace("{email}", me.Email));

                if (userMeta.Id > 0)
                {
                    user = (PgnUser)Membership.GetUser(userMeta.Username);
                    if (user.Enabled && user.IsApproved)
                    {
                        FormsAuthentication.RedirectFromLoginPage(user.UserName, ChkRememberMe.Checked);
                        LogProvider.Write(this.BaseModule, user.UserName + " logged in");
                        redirAfterLogin();
                    }
                    else
                    {
                        LogProvider.Write(this.BaseModule, user.UserName + " is not enabled", TracerItemType.Warning);
                        LblErrore = RenderError(Resources.PublicLabels.LblInvalidLogin);
                    }
                }
                else
                {
                    //register user
                    if (this.EnableUserRegistration)
                    {
                        string username = "";
                        string password = "";
                        var prov = new PgnUserProvider();
                        user = (PgnUser)Membership.CreateUser(username, password, me.Email);

                        user.Enabled = true;
                        user.NickName = me.Username;
                        Membership.UpdateUser(user);

                        if (!string.IsNullOrEmpty(this.DefaultRoles))
                        {
                            string[] rolesToAdd = this.DefaultRoles.Split(',');
                            string[] users = { user.UserName };
                            Roles.AddUsersToRoles(users, rolesToAdd);
                        }
                    }
                    else
                    {
                        LogProvider.Write(this.BaseModule, "Your Logmebot user is actually not allowed on this site", TracerItemType.Warning);
                        LblErrore = RenderError(base.GetLabel("Oauth_Logmebot_NotAllowed", "Your Logmebot user is actually not allowed on this site"));
                    }
                }

            }
            else
            {
                LblErrore = RenderError(base.GetLabel("Oauth_Logmebot_NotAuthorized", "Not authorized."));
                LogProvider.Write(this.BaseModule, "LogMeBot provider Not Authorized. ", TracerItemType.Warning);
            }
        }
        catch (Exception ex)
        {
            LblErrore = RenderError(base.GetLabel("Oauth_Logmebot_Exception", "LogMeBot provider error. " + ex.Message));
            LogProvider.Write(this.BaseModule, "LogMeBot provider error. " + ex.Message, TracerItemType.Warning);
        }

        if (!Page.IsPostBack)
        {
            TxtUser.Attributes.Add("placeholder", base.GetLabel("Username", "Username").ToUpper());
            TxtPassword.Attributes.Add("placeholder", base.GetLabel("Password", "Password").ToUpper());
            LitRememberMe.Text = base.GetLabel("RememberMe", "Remember me");
        }
    }

    protected void CmdOauthLogmebot_Click(object sender, EventArgs e)
    {
        //server side redir
        logMeBotClient.LogOn();
    }

    private void redirAfterLogin()
    {
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
}
