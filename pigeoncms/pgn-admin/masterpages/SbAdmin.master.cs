using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using PigeonCms;
using System.Collections.Generic;
using System.Threading;

public partial class Masterpages_SbAdmin : BaseMasterPage
{
    public string LblErrore = "";
    public string THEME_FOLDER = VirtualPathUtility.ToAbsolute(Config.MasterPagesPath + "SbAdmin");

    private bool? isHomePage = null;
    protected bool IsHomePage
    {
        get
        {
            if (isHomePage == null)
            {
                var page = (BasePage)this.Page;
                isHomePage = page.MenuEntry.CssClass.Contains("home");
            }
            return isHomePage == true;
        }
    }

    private bool? isLoginPage = null;
    protected bool IsLoginPage
    {
        get
        {
            if (isLoginPage == null)
            {
                var page = (BasePage)this.Page;
                isLoginPage = page.MenuEntry.Alias.Equals("my-catalogue", StringComparison.CurrentCultureIgnoreCase);
            }
            return isLoginPage == true;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        //if (PgnUserCurrent.IsAuthenticated && !this.IsLoginPage)
        //    CphLogin.Visible = false;
    }

    protected string SiteTitle
    {
        get
        {
            string res = AppSettingsManager.GetValue("MetaSiteTitle");
            return res;
        }
    }

    protected string SiteVersion
    {
        get
        {
            string res = AppSettingsManager.GetValue("PgnVersion");
            if (!string.IsNullOrEmpty(res))
            {
                res = "v" + res;
            }
            return res;
        }
    }

    protected string RenderMenuUser()
    {
        string res = "";
        
        //<li><a href='#'><i class='fa fa-user fa-fw'></i> User Profile</a></li>
        //<li><a href='#'><i class='fa fa-gear fa-fw'></i> Settings</a></li>
        //<li class='divider'></li>

        if (PgnUserCurrent.IsAuthenticated)
        {
            res = @"<li class='dropdown'>
                <a class='dropdown-toggle' data-toggle='dropdown' href='#'>
                    <i class='fa fa-user fa-fw'></i>  <i class='fa fa-caret-down'></i>
                </a>
                <ul class='dropdown-menu dropdown-user'>
                    <li>
                        <a href='/default.aspx?act=logout'><i class='fa fa-sign-out fa-fw'></i> Logout [[username]]</a>
                    </li>
                </ul>
            </li>"
                .Replace("[[username]]", PgnUserCurrent.UserName);
        }
        return res;
    }


    protected new void Page_Init(object sender, EventArgs e)
    {
        base.Page_Init(sender, e);
        if (IsHomePage)
            Body1.Attributes.Add("class", "home");

        Utility.Script.RegisterClientScriptInclude(this, "jquery", THEME_FOLDER + "/js/jquery.js");
        Utility.Script.RegisterClientScriptInclude(this, "fancybox", ResolveUrl("~/Js/fancybox/jquery.fancybox.js"));
        //Utility.Script.RegisterClientScriptInclude(this, "jquery", THEME_FOLDER + "/js/jquery-1.8.2.min.js");

        //20150505
        //Utility.Script.RegisterClientScriptInclude(this, "jquery-ui", ResolveUrl("~/Js/jquery-ui/jquery-ui.min.js"));


        var css1 = new Literal();
        css1.Text = @"
            <!-- Bootstrap Core CSS -->
            <link href='[[THEME_FOLDER]]/css/bootstrap.min.css' rel='stylesheet'>

            <!-- MetisMenu CSS -->
            <link href='[[THEME_FOLDER]]/css/plugins/metisMenu/metisMenu.min.css' rel='stylesheet'>

            <!-- DataTables CSS -->
            <link href='[[THEME_FOLDER]]/css/plugins/dataTables.bootstrap.css' rel='stylesheet'>

            <!-- Custom CSS -->
            <link href='[[THEME_FOLDER]]/css/sb-admin-2.css' rel='stylesheet'>

            <!-- Custom Fonts -->
            <link href='[[THEME_FOLDER]]/font-awesome-4.1.0/css/font-awesome.css' rel='stylesheet' type='text/css'>

            <!-- HTML5 Shim and Respond.js IE8 support of HTML5 elements and media queries -->
            <!-- WARNING: Respond.js doesn't work if you view the page via file:// -->
            <!--[if lt IE 9]>
                <script src='https://oss.maxcdn.com/libs/html5shiv/3.7.0/html5shiv.js'></script>
                <script src='https://oss.maxcdn.com/libs/respond.js/1.4.2/respond.min.js'></script>
            <![endif]-->

            <!-- fancy -->
            <link href='/Js/fancybox/fancy.css' rel='stylesheet' type='text/css'>


        ";
		//<link href='/Js/jquery-ui/jquery-ui.css' rel='stylesheet' type='text/css' media='screen' />
        css1.Text = css1.Text.Replace("[[THEME_FOLDER]]", THEME_FOLDER);
        Page.Header.Controls.Add(css1);

    }

    public override string RenderSuccess(string content)
    {
        const string HTML = @"<div class='alert alert-success alert-dismissable'>
        <button type='button' class='close' data-dismiss='alert' aria-hidden='true'>×</button>
        [[content]]
        </div>";

        string res = "";
        if (!string.IsNullOrEmpty(content))
        {
            res = HTML.Replace("[[content]]", content);
        }
        return res;
    }

    public override string RenderError(string content)
    {
        const string HTML = @"<div class='alert alert-danger alert-dismissable'>
        <button type='button' class='close' data-dismiss='alert' aria-hidden='true'>×</button>
        [[content]]
        </div>";

        string res = "";
        if (!string.IsNullOrEmpty(content))
        {
            res = HTML.Replace("[[content]]", content);
        }
        return res;
    }

    protected string RenderMenu()
    {
        string res = "";
        var page = (BasePage)this.Page;
        string currAlias = page.MenuEntry.Alias.ToLower();

        res += renderRow("hospitality", "Hospitality", currAlias);
        res += renderRow("wines", "Grapes & Wines", currAlias);

        return res;
    }

    private string renderRow(string alias, string title, string currAlias)
    {
        string ROW = @"
        <li>
            <a class='[[cssClass]]' href='/pages/[[alias]].aspx'>[[title]]</a>
        </li>        
        ";
        string cssClass = "";
        if (currAlias == alias.ToLower())
            cssClass = "active";

        string res = ROW
            .Replace("[[cssClass]]", cssClass)
            .Replace("[[alias]]", alias)
            .Replace("[[title]]", title);

        return res;
    }
}
