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

public partial class Masterpages_PigeonModernBlank : BaseMasterPage
{
    public string THEME_FOLDER = VirtualPathUtility.ToAbsolute(Config.MasterPagesPath + "PigeonModern");

	protected string PigeonModernJsInclude
	{
		get
		{
			string res = "<script src='" + THEME_FOLDER + "/js/min/pigeon-modern.min.js'></script>";
			if (Roles.IsUserInRole("debug"))
				res = "<script src='" + THEME_FOLDER + "/js/pigeon-modern.js'></script>";

			return res;
			return "";
		}
	}

    protected void Page_Load(object sender, EventArgs e)
    {
    }


    protected new void Page_Init(object sender, EventArgs e)
    {
        base.Page_Init(sender, e);

        Utility.Script.RegisterClientScriptInclude(this, "jquery", THEME_FOLDER + "/js/jquery.js");
        Utility.Script.RegisterClientScriptInclude(this, "fancybox", ResolveUrl("~/Js/fancybox/jquery.fancybox.js"));

        var css1 = new Literal();
        css1.Text = @"
            <!-- Bootstrap Core CSS -->
            <link href='[[THEME_FOLDER]]/css/bootstrap.min.css' rel='stylesheet'>

            <!-- MetisMenu CSS -->
            <link href='[[THEME_FOLDER]]/css/plugins/metisMenu/metisMenu.min.css' rel='stylesheet'>

            <!-- DataTables CSS -->
            <link href='[[THEME_FOLDER]]/css/plugins/dataTables.bootstrap.css' rel='stylesheet'>

            <!-- Timeline CSS -->
            <link href='[[THEME_FOLDER]]/css/plugins/timeline.css' rel='stylesheet'>

            <!-- Custom CSS -->
            <link href='[[THEME_FOLDER]]/css/main.css' rel='stylesheet'>

            <!-- Custom Fonts -->
            <link href='[[THEME_FOLDER]]/font-awesome-4.1.0/css/font-awesome.css' rel='stylesheet' type='text/css'>

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

}
