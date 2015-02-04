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
    [Obsolete("Use BaseMasterPage")]
    public class BaseMasterPageAdmin : BaseMasterPage
    {

        public BaseMasterPageAdmin()
        {
        }

        protected new void Page_Init(object sender, EventArgs e)
        {
            base.Page_Init(sender, e);

            //jquery & fancybox
            //added on 20130805
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

            //## commented 2012-05-11
            //if (isSessionExpired())
            //{
            //    HttpContext.Current.Response.Redirect(Config.SessionTimeOutUrl, true);
            //}
        }

        private bool isSessionExpired()
        {
            bool res = false;
            if (Session.IsNewSession)
            {
                string szCookieHeader = System.Web.HttpContext.Current.Request.Headers["Cookie"];
                if ((szCookieHeader != null) && (szCookieHeader.IndexOf("ASP.NET_SessionId") > -1))
                {
                    res = true;
                }
            }
            return res;
        }
    }
    
}