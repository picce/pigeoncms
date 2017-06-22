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

public partial class Masterpages_PigeonModern : BaseMasterPage
{
	

    public string LblErrore = "";
    public string THEME_FOLDER = VirtualPathUtility.ToAbsolute(Config.MasterPagesPath + "PigeonModern");

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

	protected string PigeonModernJsInclude
	{
		get
		{
			string res = "<script src='"+ THEME_FOLDER +"/js/min/pigeon-modern.min.js'></script>";

            if (Roles.IsUserInRole("debug"))
				res = "<script src='" + THEME_FOLDER + "/js/pigeon-modern.js'></script>";

            res += "<script src='" + THEME_FOLDER + "/js/modernizr.js'></script>";

            return res;
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
            var settings = new PigeonCms.AppSettingsProvider();
            string res = settings.GetValue("MetaSiteTitle");
            return res;
        }
    }

    protected string SiteVersion
    {
        get
        {
            var settings = new PigeonCms.AppSettingsProvider();
            string res = settings.GetValue("PgnVersion");
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
            //get menu list
            var menuMan = new MenuManager(true, false);
            var menuFilter = new MenuFilter();
            menuFilter.MenuType = "usermenu";
            menuFilter.Published = Utility.TristateBool.True;
            menuFilter.Visible = Utility.TristateBool.True;

            var menuList = new List<PigeonCms.Menu>();
            menuList = menuMan.GetByFilter(menuFilter, "");

            var menuVoice = string.Empty;

            foreach (var menu in menuList)
            {
                menuVoice += "<li><a href='" + menu.Link + "'><i class='" + menu.CssClass + "'></i> " + menu.Title + "</a></li>";
            }

            res = @"<li class='search-modern'></li>
                    <li class='dropdown dropdown--modern'>
                        <a class='dropdown-toggle clearfix' data-toggle='dropdown' href='#'>
                            <span class='user-ico-modern' style='background-image: url([[userphoto]]); background-color: #463d63;'></span>
                            <span class='user-name-modern'>[[username]]</span>
                        </a>
                        <ul class='dropdown-menu dropdown-user'>
                            [[menuvoices]]
                        </ul>
                    </li>"
            .Replace("[[menuvoices]]", menuVoice)
            .Replace("[[userphoto]]", "/pgn-admin/masterpages/PigeonModern/img/user_default.jpg") //default image user icon
            .Replace("[[username]]", PgnUserCurrent.Current.NickName);
        }
        return res;
    }


    protected new void Page_Init(object sender, EventArgs e)
    {
        base.Page_Init(sender, e);
        if (IsHomePage)
            Body1.Attributes.Add("class", "home");

		Utility.Script.RegisterClientScriptInclude(this, "jquery", THEME_FOLDER + "/js/jquery.js");
		Utility.Script.RegisterClientScriptInclude(this, "jquery-ui", THEME_FOLDER + "/js/plugins/jquery-ui/jquery-ui.js");

        var css1 = new Literal();
        css1.Text = @"
            <!-- Bootstrap Core CSS -->
            <link href='[[THEME_FOLDER]]/css/bootstrap.min.css' rel='stylesheet'>

            <!-- MetisMenu CSS -->
            <link href='[[THEME_FOLDER]]/css/plugins/metisMenu/metisMenu.min.css' rel='stylesheet'>

            <!-- DataTables CSS -->
            <link href='[[THEME_FOLDER]]/css/plugins/dataTables.bootstrap.css' rel='stylesheet'>

            <!-- Custom CSS -->
            <link href='[[THEME_FOLDER]]/css/main.css' rel='stylesheet'>

            <!-- Custom Fonts -->
            <link href='[[THEME_FOLDER]]/font-awesome-4.1.0/css/font-awesome.css' rel='stylesheet' type='text/css'>

			<!-- fancybox -->
			<link href='[[THEME_FOLDER]]/js/plugins/fancybox/fancy.css' rel='stylesheet' type='text/css'>

			<!-- jquery-ui -->
			<link href='[[THEME_FOLDER]]/js/plugins/jquery-ui/jquery-ui.css' rel='stylesheet' type='text/css'>

            <!-- HTML5 Shim and Respond.js IE8 support of HTML5 elements and media queries -->
            <!-- WARNING: Respond.js doesn't work if you view the page via file:// -->
            <!--[if lt IE 9]>
                <script src='https://oss.maxcdn.com/libs/html5shiv/3.7.0/html5shiv.js'></script>
                <script src='https://oss.maxcdn.com/libs/respond.js/1.4.2/respond.min.js'></script>
            <![endif]-->

        ";
		
        css1.Text = css1.Text.Replace("[[THEME_FOLDER]]", THEME_FOLDER);
        Page.Header.Controls.Add(css1);
    }

    protected void TxtContentFilter_Changed(object sender, EventArgs e)
    {
        //loadList();
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
