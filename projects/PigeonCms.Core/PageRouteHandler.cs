using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Web.Routing;
using System.Collections.Generic;
using System.Web.Compilation;

namespace PigeonCms
{
    public class PageRouteHandler : IRouteHandler
    {
        //http://msdn.microsoft.com/it-it/library/cc668202.aspx

        public string VirtualPath { get; private set; }

        public PageRouteHandler()
        {
            this.VirtualPath = "~/default.aspx";
        }

        public PageRouteHandler(string virtualPath)
        {
            this.VirtualPath = virtualPath;
        }

        IHttpHandler IRouteHandler.GetHttpHandler(RequestContext requestContext)
        {
            System.Web.Routing.RouteBase routeBase = requestContext.RouteData.Route;
            string routeUrl = ((System.Web.Routing.Route)routeBase).Url;

            requestContext.HttpContext.Items.Add("routeUrl", routeUrl);
            foreach (KeyValuePair<string, object> token in requestContext.RouteData.Values)
            {
                requestContext.HttpContext.Items.Add(token.Key, token.Value);
            }
            IHttpHandler result = BuildManager.CreateInstanceFromVirtualPath(
                this.VirtualPath, typeof(Page)) as IHttpHandler;

            return result;
        }
    }
}