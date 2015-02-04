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


namespace PigeonCms
{
    /// <summary>
    /// DAL for XmlType generic obj
    /// </summary>
    /// 
    public class XmlTypeManager<T,F> where T: XmlType, new()
                                     where F: XmlTypeFilter, new()
    {
        private ParseStepDelegate parseSteps;
        private FilterStepDelegate filterSteps;

        public delegate void ParseStepDelegate(T result, XmlDocument doc);
        public delegate void FilterStepDelegate(T item, F filter, ref bool result);

        public ParseStepDelegate ParseSteps
        {
            get { return this.parseSteps; }
            set { this.parseSteps = value; }
        }

        public FilterStepDelegate FilterSteps
        {
            get { return this.filterSteps; }
            set { this.filterSteps = value; }
        }

        public string Path { get; set; }

        public XmlTypeManager(string path): this(path, false)
        { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        /// <param name="parseOnlyTagInstallAttributes">true to avoid recursive params loading in</param>
        public XmlTypeManager(string path, bool parseOnlyTagInstallAttributes)
        {
            this.Path = path;

            this.ParseSteps += this.parseTagInstallAttributes;
            if (!parseOnlyTagInstallAttributes)
            {
                this.ParseSteps += this.parseTagsInstallUninstall;
                this.ParseSteps += this.parseTagParams;
            }
            this.FilterSteps += this.baseFilterStep;
        }

        /// <summary>
        /// determine if a module is installed
        /// </summary>
        /// <param name="fullName">module namespace and name</param>
        /// <returns></returns>
        public bool Exist(string fullName)
        {
            string filePath = HttpContext.Current.Request.MapPath(this.Path + fullName) + "\\install.xml";
            return System.IO.File.Exists(filePath);
        }

        public Dictionary<string, string> GetList()
        {
            Dictionary<string, string> res = new Dictionary<string, string>();
            try
            {
                foreach (string dir in Directory.GetDirectories(
                    HttpContext.Current.Request.MapPath(this.Path)))
                {
                    string dirName = System.IO.Path.GetFileName(dir);
                    if (dirName.ToLower() != ".svn")
                        res.Add(dirName, dirName);
                }
            }
            finally
            {
            }
            //causes overflow
            //F filter = new F();
            //List<T> list = GetByFilter(filter, "");
            //foreach (T item in list)
            //{
            //    res.Add(item.FullName, item.FullName);
            //}
            return res;
        }

        public List<T> GetByFilter(F filter, string sort)
        {
            bool bAdd;
            List<T> result= new List<T>();

            try
            {
                foreach (string dir in Directory.GetDirectories(HttpContext.Current.Request.MapPath(this.Path)))
                {
                    T item = GetByPath(dir, "install.xml");
                    bAdd = true; 
                    this.FilterSteps(item, filter, ref bAdd);

                    if (bAdd)
                    {
                        result.Add(item);
                    }
                }
            }
            finally
            {
            }
            return result;
        }

        public T GetByFullName(string fullName)
        {
            return GetByFullName(fullName, "install.xml");
        }

        public T GetByFullName(string fullName, string filename)
        {
            return GetByPath(
                HttpContext.Current.Request.MapPath(this.Path + fullName), filename);
        }

        public T GetByPath(string path, string filename)
        {
            T result = new T();
            try
            {
                result = loadTypeFromXml(path, filename);
            }
            finally { }
            return result;
        }

        public int DeleteById(int keyId)
        {
            throw new NotImplementedException();
        }

        #region private methods

        private T loadTypeFromXml(string path, string filename)
        {
            T result = new T();
            XmlDocument doc = new XmlDocument();
            XmlDocument docCommon = new XmlDocument();
            string filePath = path + "\\" + filename;
            string docCommonPath = HttpContext.Current.Request.MapPath(this.Path) + filename;
            try
            {
                if (System.IO.File.Exists(filePath))
                {
                    doc.Load(filePath);
                    this.ParseSteps(result, doc);
                    
                    if (System.IO.File.Exists(docCommonPath))
                    {
                        //file with common params for all modules or items or else
                        docCommon.Load(docCommonPath);
                        this.parseTagParams(result, docCommon);
                    }
                }
            }
            catch(Exception ex)
            {
                Tracer.Log("loadTypeFromXml error: " + ex, TracerItemType.Error);
                throw ex;
                //result.Params[0].Type = ModuleParamTypeEnum.Error;
            }
            finally
            { }
            return result;
        }

        /// <summary>
        /// parse params tag
        /// </summary>
        /// <param name="result"></param>
        /// <param name="doc"></param>
        private void parseTagParams(T result, XmlDocument doc)
        {
            try
            {
                XmlNodeList memberNodes = doc.SelectNodes("//install//params");
                foreach (XmlNode nodeParams in memberNodes)
                {
                    XmlNodeList paramsList = nodeParams.SelectNodes("param");
                    foreach (XmlNode nodeParam in paramsList)
                    {
                        if (nodeParam.Attributes["type"] != null)
                        {
                            FormField item = FormBuilder.GetFormFieldFromXmlNode(nodeParam, nodeParams);
                            result.Params.Add(item);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Parsing "+ result.FullName +" params", ex);
            }
        }

        private void parseTagInstallAttributes(T result, XmlDocument doc)
        {
            XmlNodeList memberNodes = doc.SelectNodes("//install"); //root
            XmlNode nodeInstall = memberNodes.Item(0);

            if (nodeInstall.Attributes["installerVersion"] != null)
            {
                result.InstallerFullVersion = nodeInstall.Attributes["installerVersion"].Value;
            }
            if (nodeInstall.Attributes["version"] != null)
            {
                result.FullVersion = nodeInstall.Attributes["version"].Value;
            }
            if (nodeInstall.Attributes["core"] != null)
            {
                bool isCore = false;
                bool.TryParse(nodeInstall.Attributes["core"].Value, out isCore);
                result.IsCore = isCore;
            }
            if (nodeInstall.Attributes["namespace"] != null)
            {
                result.ModuleNamespace = nodeInstall.Attributes["namespace"].Value;
            }
            if (nodeInstall.Attributes["name"] != null)
            {
                result.Name = nodeInstall.Attributes["name"].Value;
            }
            if (nodeInstall.Attributes["author"] != null)
            {
                result.Author = nodeInstall.Attributes["author"].Value;
            }
            if (nodeInstall.Attributes["creationDate"] != null)
            {
                result.CreationDate = DateTime.Parse(nodeInstall.Attributes["creationDate"].Value);
            }
            if (nodeInstall.Attributes["copyright"] != null)
            {
                result.Copyright = nodeInstall.Attributes["copyright"].Value;
            }
            if (nodeInstall.Attributes["license"] != null)
            {
                result.License = nodeInstall.Attributes["license"].Value;
            }
            if (nodeInstall.Attributes["authorEmail"] != null)
            {
                result.AuthorEmail = nodeInstall.Attributes["authorEmail"].Value;
            }
            if (nodeInstall.Attributes["authorUrl"] != null)
            {
                result.AuthorUrl = nodeInstall.Attributes["authorUrl"].Value;
            }
            if (nodeInstall.Attributes["description"] != null)
            {
                result.Description = nodeInstall.Attributes["description"].Value;
            }
            if (nodeInstall.Attributes["allowDirectEditMode"] != null)
            {
                bool allowDirectEditMode = false;
                bool.TryParse(nodeInstall.Attributes["allowDirectEditMode"].Value, out allowDirectEditMode);
                result.AllowDirectEditMode = allowDirectEditMode;
            }
            //if (nodeInstall.Attributes["templateBlockName"] != null)
            //{
            //    result.TemplateBlockName = nodeInstall.Attributes["templateBlockName"].Value;
            //}
        }

        private void parseTagsInstallUninstall(T result, XmlDocument doc)
        {
            foreach (XmlNode nodeFileName in doc.SelectNodes("//install//install//sql//filename"))
            {
                if (nodeFileName.Attributes["file"] != null)
                    result.InstallSqlFiles.Add(nodeFileName.Attributes["file"].Value);
            }
            foreach (XmlNode nodeFileName in doc.SelectNodes("//install//uninstall//sql//filename"))
            {
                if (nodeFileName.Attributes["file"] != null)
                    result.UninstallSqlFiles.Add(nodeFileName.Attributes["file"].Value);
            }
            foreach (XmlNode nodeFileName in doc.SelectNodes("//install//install//sql//query"))
            {
                if (!string.IsNullOrEmpty(nodeFileName.InnerText))
                    result.InstallQueries.Add(nodeFileName.InnerText);
            }
            foreach (XmlNode nodeFileName in doc.SelectNodes("//install//uninstall//sql//query"))
            {
                if (!string.IsNullOrEmpty(nodeFileName.InnerText))
                    result.UninstallQueries.Add(nodeFileName.InnerText);
            }
        }

        private void baseFilterStep(T item, F filter, ref bool res)
        {
            if (!res) return;

            if (string.IsNullOrEmpty(item.Name))
                res = false;

            if (!string.IsNullOrEmpty(filter.FullName))
            {
                if (filter.FullName.ToLower() != item.FullName.ToLower())
                {
                    res = false;
                }
            }
            if (!string.IsNullOrEmpty(filter.FullNamePart))
            {
                if (!item.FullName.ToLower().Contains(filter.FullNamePart.ToLower()))
                {
                    res = false;
                }
            }
            if (!string.IsNullOrEmpty(filter.FullVersion))
            {
                //if (filter.FullVersion.ToLower() != item.FullVersion.ToLower())
                //{
                //    res = false;
                //}
            }
            if (!string.IsNullOrEmpty(filter.ModuleNamespace))
            {
                if (filter.ModuleNamespace.ToLower() != item.ModuleNamespace.ToLower())
                {
                    res = false;
                }
            }
            if (!string.IsNullOrEmpty(filter.Name))
            {
                if (filter.Name.ToLower() != item.Name.ToLower())
                {
                    res = false;
                }
            }

            if (!string.IsNullOrEmpty(filter.Author))
            {
                if (filter.Author.ToLower() != item.Author.ToLower())
                {
                    res = false;
                }
            }
            if (filter.IsCore != Utility.TristateBool.NotSet)
            {
                if (bool.Parse(filter.IsCore.ToString()) != item.IsCore)
                {
                    res = false;
                }
            }
        }

        #endregion
    }
}