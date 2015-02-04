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
using System.Collections.Generic;
using System.Diagnostics;
using System.Web.Services;
using System.Web.Script.Services;
using System.Web.Script.Serialization;
using PigeonCms;
using PigeonCms.Core.Offline;

/// <summary>
/// BasePage for the common funtionality in all 
/// the web pages of the site.
/// </summary>

namespace PigeonCms
{
    public class BasePage : Page
    {
        private PigeonCms.Menu menuEntry = new PigeonCms.Menu();
        /// <summary>
        /// return a copy of current menuEntry
        /// </summary>
        public PigeonCms.Menu MenuEntry
        {
            [DebuggerStepThrough()]
            get
            {
                return menuEntry.Copy();
            }
        }

        public BasePage() { }

        /// <summary>
        /// Creates a generic ScriptMethod for use on ANY public static method, whether it is in a UserControl, MasterPage or some other random place.
        /// tnx to: http://www.chadscharf.com/index.php/2009/11/creating-a-page-method-scriptmethod-within-an-ascx-user-control-using-ajax-json-base-classes-and-reflection/
        /// </summary>
        /// <param name="typeName">Name of the type.</param>
        /// <param name="methodName">Name of the method.</param>
        /// <param name="args">The args.</param>
        /// <returns>The string/value type result OR a JSON serialized instance of the object returned by the target of the invocation.</returns>
        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = false)]
        public static string PageServiceRequest(string typeName, string methodName, object[] args)
        {
            Type ctl = Type.GetType(typeName);
            if (ctl != null)
            {
                object o = ctl.InvokeMember(
                    methodName,
                      System.Reflection.BindingFlags.Static
                    | System.Reflection.BindingFlags.InvokeMethod
                    | System.Reflection.BindingFlags.Public
                    | System.Reflection.BindingFlags.IgnoreCase,
                    null, null, args ?? new object[] { });
                if (o != null)
                {
                    if (o is string || o.GetType().IsValueType)
                        return o.ToString(); // If it is a string or value type, return a string

                    // If it is a complex object, return a serialized version of it.
                    JavaScriptSerializer serializer = new JavaScriptSerializer();
                    return serializer.Serialize(o); // allow anonymous types, etc
                }
            }
            return "{}"; // return an empty JSON object
        }

        protected override void OnPreInit(EventArgs e)
        {
            try
            {
                offlineRedirect();
            }
            catch (Exception ex1)
            {
                Tracer.Log("BasePage.OnPreInit>offlineRedirect(): " + ex1.ToString(), TracerItemType.Error);
            }

            base.OnPreInit(e);

            try
            {
                menuEntry = MenuHelper.GetCurrentMenu("");
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                if (Utility._QueryString("act").ToLower() == "timeout")
                {
                    //redirected from install page, wrong conn string in web.config
                }
                else
                {
                    //no connstring set, probably cms not configured. redirect to install page
                    Response.Redirect(Config.InstallationPath);
                }
            }

            if (menuEntry.CurrentUseSsl && !Request.IsSecureConnection)
            {
                //redirect to https
                Response.Redirect(Request.Url.ToString().Replace("http://", "https://"));
            }
            if (!menuEntry.CurrentUseSsl && Request.IsSecureConnection)
            {
                //redirect to http
                Response.Redirect(Request.Url.ToString().Replace("https://", "http://"));
            }


            if (menuEntry.Id == 0)
            {
                //see default 404 handler in web.config
                throw new HttpException(404, "Page not found");
            }

            setCurrentTheme();
            setCurrentMasterPage();
        }

        /// <summary>
        /// Raises the <see cref="E:System.Web.UI.Control.Init"/> event to initialize the page.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs"/> that contains the event data.</param>
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            registerPageServiceRequestProxy();
        }

        /// <summary>
        /// Confirms that an HtmlForm control is rendered for the specified ASP.NET server control at run time.
        /// Used for gridview export (xls, doc, pdf, etc..) functionality
        /// see http://www.aspsnippets.com/Articles/Export-GridView-To-WordExcelPDFCSV-in-ASP.Net.aspx
        /// see http://www.aspsnippets.com/Articles/Exporting-Multiple-GridViews-To-Excel-SpreadSheet-in-ASP.Net.aspx
        /// </summary>
        /// <param name="control"></param>
        public override void VerifyRenderingInServerForm(Control control)
        {
            //base.VerifyRenderingInServerForm(control);
        }

        /// <Summary>
        /// Overriding the InitializeCulture method to set the user selected
        /// option in the current thread. Note that this method is called much
        /// earlier in the Page lifecycle and we don't have access to any controls
        /// in this stage, so have to use Form collection.
        /// </Summary>
        protected override void InitializeCulture()
        {
            //try different qrystring param
            string lngParam = "";
            lngParam = Utility._QueryString("len");
            if (string.IsNullOrEmpty(lngParam))
                lngParam = Utility._QueryString("lng");
            if (string.IsNullOrEmpty(lngParam))
                lngParam = Utility._QueryString("lang");

            //check if user/linked requested lang is enabled
            if (!string.IsNullOrEmpty(lngParam))
            {
                string displayName = "";
                if (!Config.CultureList.ContainsKey(lngParam))
                    lngParam = Config.CultureDefault;
                Config.CultureList.TryGetValue(lngParam, out displayName);
                setCulture(lngParam, displayName);
            }

            //check if browser requested lang is enabled
            {
                string len = Thread.CurrentThread.CurrentCulture.Name;
                string displayName = "";
                if (!Config.CultureList.ContainsKey(len))
                {
                    //if not enabled then set default lang
                    len = Config.CultureDefault;
                    Config.CultureList.TryGetValue(len, out displayName);
                    setCulture(len, displayName);
                }
            }


            //Get the culture from the session if the control is tranferred to a new page in the same application.
            if (Session["MyUICulture"] != null && Session["MyCulture"] != null)
            {
                Thread.CurrentThread.CurrentUICulture = (CultureInfo)Session["MyUICulture"];
                Thread.CurrentThread.CurrentCulture = (CultureInfo)Session["MyCulture"];
            }
            base.InitializeCulture();
        }

        /// <Summary>
        /// Sets the current UICulture and CurrentCulture based on
        /// the arguments
        /// </Summary>
        protected void setCulture(string culture, string displayName)
        {
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(culture);
            Thread.CurrentThread.CurrentCulture = new CultureInfo(culture);
            ///<remarks>
            ///Saving the current thread's culture set by the User in the Session
            ///so that it can be used across the pages in the current application.
            ///</remarks>
            Session["MyUICulture"] = Thread.CurrentThread.CurrentUICulture;
            Session["MyCulture"] = Thread.CurrentThread.CurrentCulture;
            Session["MyCultureDisplayName"] = displayName;
        }

        /// <summary>
        /// set current theme
        /// </summary>
        private void setCurrentTheme()
        {
            this.Theme = menuEntry.CurrTheme;
        }

        /// <summary>
        /// set current masterpage
        /// </summary>
        private void setCurrentMasterPage()
        {
            this.MasterPageFile = menuEntry.CurrMasterPageFile; //specific for mobile, desktop, o default
        }

        /// <summary>
        /// check offline status and redirect if necessary
        /// move in global.asax Application_OnBeginRequest to check all requests (too heavy)
        /// </summary>
        private void offlineRedirect()
        {
            if (OfflineProvider.IsOffline)
            {
                bool redirect = true;

                if (User.IsInRole("admin"))
                {
                    redirect = false;
                }
                else
                {
                    string requestedUrl = System.IO.Path.GetFileName(Request.PhysicalPath);
                    string[] safePages = { "default.xml", "offlineadmin.aspx" };
                    foreach (var page in safePages)
                    {
                        if (string.Compare(page, requestedUrl, true) == 0)
                            redirect = false;
                    }
                }
                if (redirect)
                {
                    OfflineProvider.ResetOfflineStatus();
                    Response.Redirect(OfflineProvider.XmlFilePath);
                    //Server.Transfer(OfflineProvider.XmlFilePath);
                }
            }
        }

        /// <summary>
        /// Registers the page service request proxy. This is used by any user controls and/or master pages that also want to
        /// encapsulate web methods without having to put all of them in the page.
        /// tnx to: http://www.chadscharf.com/index.php/2009/11/creating-a-page-method-scriptmethod-within-an-ascx-user-control-using-ajax-json-base-classes-and-reflection/
        /// </summary>
        /// <remarks>AJAX Voodoo!</remarks>
        private void registerPageServiceRequestProxy()
        {
            ScriptManager.RegisterClientScriptBlock(
                this,
                GetType(),
                "PageServiceRequestProxy",
                "function InvokeServiceRequest(typeName,methodName,successCallback,failureCallback){if(PageMethods.PageServiceRequest){try{var parms=[];for(var i=4;i<arguments.length;i++){parms.push(arguments[i]);}PageMethods.PageServiceRequest(typeName,methodName,parms,successCallback,failureCallback);}catch(e){alert(e.toString());}}}",
                true);

            /* debug version
            function InvokeServiceRequest(typeName,methodName,successCallback,failureCallback){
                if(PageMethods.PageServiceRequest){
                    try{
                        var parms=[];
                        for(var i=4;i<arguments.length;i++){
                            parms.push(arguments[i]);
                        }
                        PageMethods.PageServiceRequest(typeName,methodName,parms,successCallback,failureCallback);
                    }
                    catch(e){
                        alert(e.toString());
                    }
                }
            }            
            */
        }
    }
}