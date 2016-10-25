using System;
using System.Web;
using System.Web.UI;
using System.Web.Routing;
using System.Collections.Generic;
using System.Web.Compilation;
using System.Configuration;
using System.IO;
using System.Reflection;


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
            RouteBase routeBase = requestContext.RouteData.Route;
            Route route = (Route)routeBase;

            string assemblyPath = (route.DataTokens != null && route.DataTokens.ContainsKey("PigeonCMS_AssemblyPath")) ? Convert.ToString(route.DataTokens["PigeonCMS_AssemblyPath"]) : string.Empty;
            string handlerName = (route.DataTokens != null && route.DataTokens.ContainsKey("PigeonCMS_HandlerName")) ? Convert.ToString(route.DataTokens["PigeonCMS_HandlerName"]) : string.Empty;

            requestContext.HttpContext.Items.Add("routeUrl", route.Url);
            foreach (KeyValuePair<string, object> token in requestContext.RouteData.Values)
            {
                requestContext.HttpContext.Items.Add(token.Key, token.Value);
            }

            bool isPigeonRule = string.IsNullOrEmpty(assemblyPath) || string.IsNullOrEmpty(handlerName);

            IHttpHandler result = null;

            if (isPigeonRule)
            {
                //pigeon default handler
                result = BuildManager.CreateInstanceFromVirtualPath(this.VirtualPath, typeof(Page)) as IHttpHandler;
            }
            else
            {
                result = LoadMVCHandler(requestContext, assemblyPath, handlerName);
            }

            return result;
        }

        /// <summary>
        /// 20161025 - r.sartori
        /// custom mvc handler loaded through reflection
        /// </summary>
        /// <param name="requestContext">current context</param>
        /// <param name="assemblyPath">
        /// dll path in mvc project. 
        /// example: ~/bin/mynamespace.mymvchandler.dll 
        /// example: c:\inetpub\myhandlers\customhandler.dll</param>
        /// <param name="handlerName">
        /// class full name that handle mvc controller
        /// example: mynamespace.MyMvchHndler</param>
        /// <returns></returns>
        IHttpHandler LoadMVCHandler(RequestContext requestContext, string assemblyPath, string handlerName)
        {
            string resolvedAssemblyPath = assemblyPath.StartsWith("~") ? requestContext.HttpContext.Server.MapPath(assemblyPath) : assemblyPath;

            if (!File.Exists(resolvedAssemblyPath))
                throw new FileNotFoundException("Assembly " + resolvedAssemblyPath + " not found", assemblyPath);

            Assembly assembly = Assembly.LoadFrom(resolvedAssemblyPath);
            if (assembly == null)
                throw new Exception("Error loading assembly " + resolvedAssemblyPath);

            Type type = assembly.GetType(handlerName);

            object oCustomMVCHandler = Activator.CreateInstance(type, new object[] { requestContext });

            IHttpHandler customMVCHandler = oCustomMVCHandler as IHttpHandler;

            if (customMVCHandler == null)
                throw new Exception("Cannot cast " + handlerName + " to IController");

            return customMVCHandler;
        }
    }
}