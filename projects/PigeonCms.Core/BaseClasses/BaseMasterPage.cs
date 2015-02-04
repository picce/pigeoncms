using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Threading;
using System.Globalization;
using PigeonCms;

namespace PigeonCms
{

    public class BaseMasterPage : MasterPage
    {
        public string PageTitle
        {
            get { return this.Page.Header.Title; }
            set { this.Page.Header.Title = value; }
        }

        private string errorString = "";
        public string ErrorString
        {
            get { return errorString; }
            set { errorString = value; }
        }

        public BaseMasterPage()
        {
        }

        public virtual string RenderSuccess(string content)
        {
            const string HTML = @"<span class='success'>[[content]]</span>";

            string res = "";
            if (!string.IsNullOrEmpty(content))
            {
                res = HTML.Replace("[[content]]", content);
            }
            return res;
        }

        public virtual string RenderError(string content)
        {
            const string HTML = @"<span class='error'>[[content]]</div>";

            string res = "";
            if (!string.IsNullOrEmpty(content))
            {
                res = HTML.Replace("[[content]]", content);
            }
            return res;
        }

        private CultureInfo myUICulture = null;
        private CultureInfo myCulture = null;
        private string displayName = null;

        private void saveCultureSection()
        {
            if (Session["MyUICulture"] != null)
                myUICulture = (CultureInfo)Session["MyUICulture"];
            if (Session["MyCulture"] != null)
                myCulture = (CultureInfo)Session["MyCulture"];
            if (Session["MyCultureDisplayName"] != null)
                displayName = (string)Session["MyCultureDisplayName"];
            
        }

        private void restoreCultureSection()
        {
            if (myUICulture != null)
                Session["MyUICulture"] = myUICulture;
            if (myCulture != null)
                Session["MyCulture"] = myCulture;
            if (displayName != null)
                Session["MyCultureDisplayName"] = displayName;
        }

        private void logout()
        {
            if (PgnUserCurrent.IsAuthenticated)
            {
                //Session.Abandon();
                saveCultureSection();
                Session.Clear();
                restoreCultureSection();
                FormsAuthentication.SignOut();
                Response.Redirect(Request.Url.ToString());  //cookie removed on second request
            }
        }

        protected virtual void AddMetaTags()
        {
            PigeonCms.PageHelper.AddDefaultMetaTags(Page.Header);
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {

                try
                {
                    AddMetaTags();
                }
                catch (Exception)
                {
                    PigeonCms.Tracer.Log("Errore adding Metatag", TracerItemType.Error);
                }

                if (Request.QueryString["act"] == "logout")
                    logout();
                if (Request.QueryString["act"] == "timeout")
                    logout();

                //jquery & fancybox
                //added on 20101015
                //removed on 20130805 for customization free
                /*
                Utility.Script.RegisterClientScriptInclude(this, "jquery", ResolveUrl("~/Js/jquery.js"));
                Utility.Script.RegisterClientScriptInclude(this, "fancybox", ResolveUrl("~/Js/fancybox/jquery.fancybox.js"));
                Utility.Script.RegisterStartupScript(this, ID,
                    @"
                                    $(document).ready(function () {
                                        $(""a.fancy"").fancybox({
                                            'width': '80%',
                                            'height': '80%',
                                            'type': 'iframe',
                                            'hideOnContentClick': false,
                                            onClosed: function () { }
                                        });
                                    });
                                    "
                    );
                */

                //20111018
                //hack to avoid Sys.ScriptLoadFailedException ScriptManager error
                //see http://blog.lavablast.com/?tag=/webkit
                Utility.Script.RegisterStartupScript(this, "WebKit",
                @"
                if (typeof(Sys) != 'undefined'){
                    Sys.Browser.WebKit = {};
                    if (navigator.userAgent.indexOf('WebKit/') > -1) {
                        Sys.Browser.agent = Sys.Browser.WebKit;
                        Sys.Browser.version = parseFloat(navigator.userAgent.match(/WebKit\/(\d+(\.\d+)?)/)[1]);
                        Sys.Browser.name = 'WebKit';
                    }
                }");

                //20110421
                //hack to avoid entire page_load process due to httphandler redirect
                Utility.Script.RegisterStartupScript(this, "PageMethods",
                    "if(typeof(PageMethods)!='undefined'){PageMethods.set_path('" + ResolveUrl("~/default.aspx") + "');}");

                var css1 = new Literal();
                css1.Text = "<link href='" + ResolveUrl("~") + "Js/fancybox/fancy.css' rel='stylesheet' type='text/css' media='screen' />"
                    + "<link href='" + ResolveUrl("~") + "Css/common.css' rel='stylesheet' type='text/css' media='screen' />";
                Page.Header.Controls.Add(css1);
            }
        }
    }

}