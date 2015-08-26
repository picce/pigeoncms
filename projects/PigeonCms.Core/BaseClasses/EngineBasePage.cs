using PigeonCms;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;


/// <summary>
/// ##20141205 pice
/// to use PigeonCms.Core in not PigeonCms website
/// </summary>
namespace PigeonCms.Engine
{
    public class BasePage : Page
    {
        //20150324 labelsList as Dictionary to manage multiple resourceSet
        private Dictionary<string, List<ResLabel>> labelsList = 
            new Dictionary<string, List<ResLabel>>();

        /// <summary>
        /// on first invoke load in page cache all labels (labelsList)
        /// </summary>
        /// <param name="resourceSet">eg: MYSITE_PAGENAME</param>
        /// <param name="resourceId">eg: PageTitle</param>
        /// <param name="defaultValue">eg: MY web site --> on first call insert label in db with defaultValue</param>
        /// <returns>label value</returns>
        public string GetLabel(
            string resourceSet, 
            string resourceId,
            string defaultValue, 
            ContentEditorProvider.Configuration.EditorTypeEnum textMode = ContentEditorProvider.Configuration.EditorTypeEnum.Text, 
            string forcedCultureCode = "")
        {
            if (string.IsNullOrEmpty(resourceSet))
                throw new ArgumentException("empty resourceSet");

            if (string.IsNullOrEmpty(resourceId))
                throw new ArgumentException("empty resourceId");

            string res = "";

            try
            {
                if (!labelsList.ContainsKey(resourceSet))
                {
                    //preload all labels of current moduletype
                    var labels = LabelsProvider.GetLabelsByResourceSet(resourceSet);
                    labelsList.Add(resourceSet, labels);
                }
                res = LabelsProvider.GetLocalizedLabelFromList(
                    resourceSet,
                    labelsList[resourceSet],
                    resourceId,
                    defaultValue,
                    textMode,
                    forcedCultureCode);
                if (string.IsNullOrEmpty(res))
                {
                    res = defaultValue;
                }
                if (HttpContext.Current.Request.QueryString["tp"] == "1")
                {
                    res = "[" + resourceId + "]" + res;
                }
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                Response.Redirect(Config.InstallationPath);
            }

            return res;
        }

        /// <Summary>
        /// Overriding the InitializeCulture method to set the user selected
        /// option in the current thread. Note that this method is called much
        /// earlier in the Page lifecycle and we don't have access to any controls
        /// in this stage, so have to use Form collection.
        /// </Summary>
        protected override void InitializeCulture()
        {
            bool setCultureDone = false;
            var clist = new Dictionary<string, string>(Config.CultureList, StringComparer.InvariantCultureIgnoreCase);

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
                if (!clist.ContainsKey(lngParam))
                    lngParam = Config.CultureDefault;
                clist.TryGetValue(lngParam, out displayName);
                setCulture(lngParam, displayName);
                setCultureDone = true;
            }

            //check if browser requested lang is enabled
            if (!setCultureDone)
            {
                //string len = Thread.CurrentThread.CurrentCulture.Name;
                string len = "";
                if (Request.UserLanguages != null && Request.UserLanguages.Length > 0)
                    len = Request.UserLanguages[0];
                string displayName = "";

                if (!clist.ContainsKey(len))
                {
                    //if not enabled then set default lang
                    len = Config.CultureDefault;
                    clist.TryGetValue(len, out displayName);
                    setCulture(len, displayName);
                    setCultureDone = true;
                }
            }


            ////Get the culture from the session if the control is tranferred to a new page in the same application.
            //if (Session["MyUICulture"] != null && Session["MyCulture"] != null)
            //{
            //    Thread.CurrentThread.CurrentUICulture = (CultureInfo)Session["MyUICulture"];
            //    Thread.CurrentThread.CurrentCulture = (CultureInfo)Session["MyCulture"];
            //}
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
            //Session["MyUICulture"] = Thread.CurrentThread.CurrentUICulture;
            //Session["MyCulture"] = Thread.CurrentThread.CurrentCulture;
            //Session["MyCultureDisplayName"] = displayName;
        }
    }
}