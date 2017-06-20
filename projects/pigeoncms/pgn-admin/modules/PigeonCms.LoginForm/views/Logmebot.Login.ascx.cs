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
using System.Text;
using System.Collections.Generic;
using Logmebot.Net;

public partial class Controls_Logmebot_Login : PigeonCms.LoginFormControl
{
    protected string LblErr = "";
    private LogmebotClient logmebotClient;
    private static Random random = new Random();
    private const string oauthMetaKey = "oauth_logmebot";
    private const string oauthMetaValueTemplate = "{UserId}";


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


    private bool checkRolesValidity()
    {
        bool res = true;
        if (!string.IsNullOrEmpty(this.DefaultRoles))
        {
            string[] rolesToAdd = this.DefaultRoles.Split(',');
            foreach(string role in rolesToAdd)
            {
                res = res && Roles.RoleExists(role);
            }
        }

        return res;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        bool showButton = false;

        try
        {

            logmebotClient = new LogmebotClient(
                this.AppClientId,
                this.AppClientSecret,
                this.AppCallbackUri);

            //extra security check
            if (this.DefaultRoles.ToLower().Contains("admin"))
                throw new ArgumentException("Invalid default roles");

            if (!checkRolesValidity())
                throw new ArgumentException("Invalid roles");

            showButton = true;

            if (Request.Params["code"] != null)
            {
                if (string.IsNullOrEmpty(logmebotClient.AccessToken))
                {
                    string token = logmebotClient.GetAccessToken(
                        Request.Params["code"], Request.Params["state"]);
                }
            }


            if (!string.IsNullOrEmpty(logmebotClient.AccessToken))
            {
                var me = logmebotClient.GetMe(logmebotClient.AccessToken);
                LogProvider.Write(this.BaseModule, "LogMeBot provider Authorized. UserId:{UserId}; Nickname:{Nickname}; Email:{Email}"
                    .Replace("{UserId}", me.UserId)
                    .Replace("{Nickname}", me.Nickname)
                    .Replace("{Email}", me.Email));

                var user = new PgnUser();
                var userMetaMan = new PgnUserMetaManager();

                //look for meta value matching
                var userMeta = userMetaMan.GetFirstByMetaKeyValue(
                    oauthMetaKey, 
                    oauthMetaValueTemplate.Replace("{UserId}", me.UserId));

                if (userMeta.Id > 0)
                {
                    //found matching user
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
                        LblErr = RenderError(Resources.PublicLabels.LblInvalidLogin);
                    }
                }
                else
                {
                    //register new user
                    if (this.EnableUserRegistration)
                    {
                        //create user
                        string username = "logmebot-" + me.UserId;  //unique username 
                        string password = generatePassword();
                        var prov = new PgnUserProvider();
                        user = (PgnUser)Membership.CreateUser(username, password, me.Email);

                        //enable user and update nickname
                        user.Enabled = true;
                        user.IsApproved = true;
                        user.NickName = me.Nickname;
                        Membership.UpdateUser(user);

                        //add default roles
                        if (!string.IsNullOrEmpty(this.DefaultRoles))
                        {
                            string[] rolesToAdd = this.DefaultRoles.Split(',');
                            string[] users = { user.UserName };
                            Roles.AddUsersToRoles(users, rolesToAdd);
                        }

                        //add meta value
                        userMeta = new PngUserMeta();
                        userMeta.Username = username;
                        userMeta.MetaKey = oauthMetaKey;
                        userMeta.MetaValue = oauthMetaValueTemplate.Replace("{UserId}", me.UserId);
                        userMetaMan.Insert(userMeta);

                        //retrieve just created full user
                        user = (PgnUser)Membership.GetUser(userMeta.Username);
                        if (user.Enabled && user.IsApproved)
                        {
                            FormsAuthentication.RedirectFromLoginPage(user.UserName, ChkRememberMe.Checked);
                            LogProvider.Write(this.BaseModule, user.UserName + " logged in");
                            redirAfterLogin();
                        }
                    }
                    else
                    {
                        LogProvider.Write(this.BaseModule, "Your Logmebot user is actually not allowed on this site", TracerItemType.Warning);
                        LblErr = RenderError(base.GetLabel("Oauth_Logmebot_NotAllowed", "Your Logmebot user is actually not allowed on this site"));
                    }
                }

            }//access token

        }
        catch (Exception ex)
        {
            LblErr = RenderError(base.GetLabel("Oauth_Logmebot_Exception", "LogMeBot provider error."));
            LogProvider.Write(this.BaseModule, "LogMeBot provider error. " + ex.ToString(), TracerItemType.Error);
        }

        if (!Page.IsPostBack)
        {
            CmdOauthLogmebot.Visible = showButton;
            LitRememberMe.Text = base.GetLabel("RememberMe", "Remember me");
        }
    }

    protected void CmdOauthLogmebot_Click(object sender, EventArgs e)
    {
        //server side redir
        logmebotClient.LogOn();
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

    public string generatePassword()
    {
        StringBuilder builder = new StringBuilder();
        builder.Append(randomString(random, 6));
        builder.Append(randomNumber(random, 100, 999));

        return builder.ToString();
    }

    private int randomNumber(Random random, int min, int max)
    {
        return random.Next(min, max);
    }

    private string randomString(Random random, int size)
    {
        var builder = new StringBuilder();
        char ch;
        for (int i = 0; i < size; i++)
        {
            ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
            builder.Append(ch);
        }
        return builder.ToString().ToUpper();
    }
}
