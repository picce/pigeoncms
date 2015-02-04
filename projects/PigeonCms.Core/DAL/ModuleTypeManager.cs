using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.ComponentModel;
using System.Collections.Generic;
using System.IO;
using System.Data.Common;
using System.Xml;
using PigeonCms;
//using System.Reflection;


namespace PigeonCms
{

    /// <summary>
    /// DAL for ModuleType obj (from Modules folder)
    /// </summary>
    public class ModuleTypeManager: XmlTypeManager<ModuleType,ModuleTypeFilter>
    {
        public ModuleTypeManager():this(false)
        {}

        public ModuleTypeManager(bool parseOnlyTagInstallAttributes)
            : base(Config.ModulesPath, parseOnlyTagInstallAttributes)
        {
            base.ParseSteps += this.parseTagInstallAttributes;
            if (!parseOnlyTagInstallAttributes)
            {
                //specific xml parse steps
                base.ParseSteps += this.loadViewsList;
                base.ParseSteps += this.parseTagEditContent;
            }
            //specific getByFilter steps
            base.FilterSteps += this.baseFilterStep;
        }

        private void parseTagInstallAttributes(ModuleType result, XmlDocument doc)
        {
            XmlNodeList memberNodes = doc.SelectNodes("//install"); //root
            XmlNode nodeInstall = memberNodes.Item(0);

            if (nodeInstall.Attributes["templateBlockName"] != null)
            {
                result.TemplateBlockName = nodeInstall.Attributes["templateBlockName"].Value;
            }
        }

        private void parseTagEditContent(ModuleType result, XmlDocument doc)
        {
            XmlNodeList memberNodes = doc.SelectNodes("//install/editContent");
            if (memberNodes.Count > 0)
            {
                XmlNode node = memberNodes.Item(0);

                if (node.Attributes["menuType"] != null)
                    result.EditContentTag.MenuType = node.Attributes["menuType"].Value;
                if (node.Attributes["menuName"] != null)
                    result.EditContentTag.MenuName = node.Attributes["menuName"].Value;
                if (node.Attributes["moduleFullName"] != null)
                    result.EditContentTag.ModuleFullName = node.Attributes["moduleFullName"].Value;
                if (node.Attributes["editParams"] != null)
                {
                    string editParam = node.Attributes["editParams"].Value;
                    result.EditContentTag.EditParamsList = Utility.String2List(editParam);
                }
            }
        }

        private void baseFilterStep(ModuleType item, ModuleTypeFilter filter, ref bool res)
        {
            if (!res) return;

            if (!string.IsNullOrEmpty(filter.TemplateBlockName))
            {
                if (filter.TemplateBlockName.ToLower() != item.TemplateBlockName.ToLower())
                {
                    res = false;
                }
            }
        }

        /// <summary>
        /// list of ascx files in modulefolder/views/
        /// </summary>
        /// <param name="result"></param>
        private void loadViewsList(ModuleType result, XmlDocument doc)
        {
            string filespath = this.Path + result.FullName + "/views/";
            filespath = HttpContext.Current.Request.MapPath(filespath);
            DirectoryInfo dir = new DirectoryInfo(filespath);
            if (dir.Exists)
            {
                FileInfo[] files = dir.GetFiles("*.ascx");
                foreach (FileInfo file in files)
                {
                    result.Views.Add(file.Name);
                }
            }
        }
    }
}