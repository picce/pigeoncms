using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.IO;

/// <summary>
/// rewrite tha Search Engine (& human) Friendly Url in right url to process
/// </summary>

namespace PigeonCms
{
    public class PagesUrlRewrite : IHttpHandlerFactory
    {
        public IHttpHandler GetHandler(HttpContext context, string requestType, string url, string pathTranslated)
        {
            context.Items["fileName"] = Path.GetFileNameWithoutExtension(url);
            return PageParser.GetCompiledPageInstance(url, context.Server.MapPath("~/default.aspx"), context);
        }

        public void ReleaseHandler(IHttpHandler handler) { }
    }
}