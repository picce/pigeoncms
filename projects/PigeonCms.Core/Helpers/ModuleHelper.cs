using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Reflection;
using PigeonCms;
using System.IO;


namespace PigeonCms
{
    /// <summary>
    /// Useful static functions to manage modules
    /// </summary>
    public static class ModuleHelper
    {
        public const string ContentTemplateBlock = "content";  //name of content template block (mainbody of the page)
        /// <summary>
        /// load the modules for the selected menu entry
        /// </summary>
        /// <param name="menuAlias">pgn_menu.alias</param>
        public static void LoadModules(PigeonCms.Menu menuEntry, Page currPage)
        {
            Tracer.Log("LoadModules(menu:" + menuEntry.ToString() + ", currPage:" + currPage + ")", TracerItemType.Debug);

            if (menuEntry.Published && !string.IsNullOrEmpty(menuEntry.Name))
            {
                if (menuEntry.OverridePageTitle)
                {
                    var myMaster = (BaseMasterPage)currPage.Master;
                    myMaster.PageTitle = menuEntry.TitleWindow;
                }
                //loop through templateBlocks in current masterpage
                foreach (Control cpl in currPage.Master.FindControl("Form1").Controls)
                {
                    if (cpl is ContentPlaceHolder)
                    {
                        string cplName = cpl.ID.Substring(3).ToLower(); //CphPlaceholdername --> Placeholdername
                        if (cplName != ContentTemplateBlock)
                        {
                            //check and load which modules fill in current templateBlock
                            foreach (var currModule in menuEntry.ModulesList)
                            {
                                if (currModule.TemplateBlockName.ToLower() == cplName)
                                    renderModule(currPage, cpl, currModule, menuEntry);
                            }
                        }
                        else
                        {
                            //load page content module
                            renderModule(currPage, cpl, menuEntry.ContentModule, menuEntry);
                        }
                    }
                }
            }
        }

        #region private methods

        /// <summary>
        /// render currModule ascx control in currPage>cpl
        /// </summary>
        /// <param name="currPage">current page</param>
        /// <param name="cpl">current placeholder</param>
        /// <param name="currModule">module (ascx control) to load dynamically</param>
        private static void renderModule(Page currPage, Control cpl, 
            PigeonCms.Module currModule, PigeonCms.Menu currMenu)
        {
            string modulePath;
            Tracer.Log("renderModule(module:" + currModule.ToString() + ")", TracerItemType.Debug);

            try
            {
                modulePath = Config.ModulesPath + currModule.ModuleFullName + "/views/" + currModule.CurrView;
                if (!new FileInfo(HttpContext.Current.Request.MapPath(modulePath)).Exists)
                {
                    modulePath = Config.ModulesPath + currModule.ModuleFullName + "/views/" + currModule.ModuleName + ".ascx";
                }
                if (!new FileInfo(HttpContext.Current.Request.MapPath(modulePath)).Exists)
                {
                    throw new CustomException("Missing module rendering file: " + modulePath,
                        CustomExceptionSeverity.Warning, CustomExceptionLogLevel.Debug);
                }

                HtmlGenericControl moduleTag = new HtmlGenericControl("div");   //module container
                HtmlGenericControl moduleTitleTag = new HtmlGenericControl();         //module title container
                HtmlGenericControl moduleContentTag = new HtmlGenericControl("div");   //module content container
                Control ctrl1 = currPage.LoadControl(modulePath);
                Type typeUserControl = ctrl1.GetType();

                //set Param BaseModule
                PropertyInfo propBaseModule = null;
                propBaseModule = typeUserControl.GetProperty("BaseModule");
                propBaseModule.SetValue(ctrl1, currModule, null);

                //set param CurrMenu
                PropertyInfo propCurrMenu = null;
                propCurrMenu = typeUserControl.GetProperty("CurrMenu");
                propCurrMenu.SetValue(ctrl1, currMenu, null);

                moduleTitleTag.TagName = "div";
                moduleTitleTag.Attributes["class"] = "title";
                if (currModule.ShowTitle)
                {
                    moduleTitleTag.InnerHtml = currModule.Title;
                }

                //direct content edit TO COMPLETE
                if (PgnUserCurrent.IsAuthenticated
                    && Roles.IsUserInRole("admin")
                    && !string.IsNullOrEmpty(currModule.EditContentUrl)/*WARN:cause GetList() */)
                {
                    string titleString = "edit " + currModule.Title + " [" + currModule.ModuleFullName + "]";
                    string src = Utility.GetThemedImageSrc("EditFile.gif", "adminDefault");
                    moduleTitleTag.InnerHtml += "<a class='fancy editContent' href='" + currModule.EditContentUrl + "'>"
                    + "<img src='" + src + "' title='" + titleString + "' />"
                    + "</a>";
                }

                moduleTag.Attributes["class"] = "module";
                moduleContentTag.Attributes["class"] = "moduleContent";
                if (!string.IsNullOrEmpty(currModule.CssClass))
                    moduleTag.Attributes["class"] += " " + currModule.CssClass;
                moduleTag.Controls.Add(moduleTitleTag);
                moduleTag.Controls.Add(moduleContentTag);
                moduleContentTag.Controls.Add(ctrl1);

                if (HttpContext.Current.Request.QueryString["tp"] == "1"
                    && PgnUserCurrent.IsAuthenticated
                    && Roles.IsUserInRole("debug"))
                {
                    //modules structure only for users in debug role
                    HtmlGenericControl divPreview = new HtmlGenericControl("div");
                    divPreview.Attributes["class"] = "mod-preview";
                    HtmlGenericControl divPreviewInfo = new HtmlGenericControl("div");
                    divPreviewInfo.Attributes["class"] = "mod-preview-info";
                    divPreviewInfo.InnerHtml = currModule.TemplateBlockName + " - " + currModule.Title + "<br />[" + currModule.ModuleFullName + "]";
                    HtmlGenericControl divPreviewWrapper = new HtmlGenericControl("div");
                    divPreviewWrapper.Attributes["class"] = "mod-preview-wrapper";

                    divPreviewWrapper.Controls.Add(moduleTag);
                    divPreview.Controls.Add(divPreviewInfo);
                    divPreview.Controls.Add(divPreviewWrapper);

                    ContentPlaceHolder ct = (ContentPlaceHolder)cpl;
                    ct.Controls.Add(divPreview);
                }
                else
                {
                    ContentPlaceHolder ct = (ContentPlaceHolder)cpl;
                    ct.Controls.Add(moduleTag);
                }
            }
            catch (Exception e)
            {
                PigeonCms.Tracer.Log("Error loading control " + currModule.ModuleFullName + ": " + e.ToString(), TracerItemType.Error);
            }
        } 
        #endregion
    }
}
