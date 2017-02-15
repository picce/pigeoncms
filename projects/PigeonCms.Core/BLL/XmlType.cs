using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Diagnostics;
using System.ComponentModel;
using System.Collections.Generic;
using System.IO;
using PigeonCms;


namespace PigeonCms
{
    public class XmlType
    {
        private string installerFullVersion = "1.0.0";
        private string fullVersion = "1.0.0";
        private string moduleNamespace = "";
        private string name = "";
        private string author = "";
        private DateTime creationDate;
        private string copyright = "";
        private string license = "";
        private string authorEmail = "";
        private string authorUrl = "";
        private string description = "";
        private string iconClass = "";
        private string panelClass = "";
        //private List<FormField> commonParams = new List<FormField>();
        private List<FormField> _params = new List<FormField>();
        private bool isCore = false;
        private bool allowDirectEditMode = false;
        private List<string> installQueries = new List<string>();
        private List<string> installSqlFiles = new List<string>();
        private List<string> uninstallQueries = new List<string>();
        private List<string> uninstallSqlFiles = new List<string>();

        public XmlType() { }

        /// <summary>
        /// pkey identity 1,1
        /// </summary>
        [DataObjectField(true)]
        public string FullName
        {
            [DebuggerStepThrough()]
            get { return moduleNamespace + "." + name; }
        }

        public string InstallerFullVersion
        {
            [DebuggerStepThrough()]
            get { return installerFullVersion; }
            [DebuggerStepThrough()]
            set { installerFullVersion = value; }
        }

        public int InstallerVersion
        {
            [DebuggerStepThrough()]
            get
            {
                int res = 0;
                string[] arrVers = installerFullVersion.Split('.');
                int.TryParse(arrVers[0], out res);
                return res;
            }
        }

        public int InstallerRevision
        {
            [DebuggerStepThrough()]
            get
            {
                int res = 0;
                string[] arrVers = installerFullVersion.Split('.');
                int.TryParse(arrVers[1], out res);
                return res;
            }
        }

        public int InstallerRelease
        {
            [DebuggerStepThrough()]
            get
            {
                int res = 0;
                string[] arrVers = installerFullVersion.Split('.');
                int.TryParse(arrVers[2], out res);
                return res;
            }
        }

        public int NumericInstallerVersion
        {
            [DebuggerStepThrough()]
            get
            {
                int res = this.InstallerVersion * 100000 + this.InstallerRevision * 1000 + this.InstallerRelease * 10;
                return res;
            }
        }

        public string FullVersion
        {
            [DebuggerStepThrough()]
            get { return fullVersion; }
            [DebuggerStepThrough()]
            set { fullVersion = value; }
        }

        public int Version
        {
            [DebuggerStepThrough()]
            get 
            {
                int res = 0;
                string[] arrVers = fullVersion.Split('.');
                int.TryParse(arrVers[0], out res);
                return res; 
            }
        }

        public int Revision
        {
            [DebuggerStepThrough()]
            get
            {
                int res = 0;
                string[] arrVers = fullVersion.Split('.');
                int.TryParse(arrVers[1], out res);
                return res;
            }
        }

        public int Release
        {
            [DebuggerStepThrough()]
            get
            {
                int res = 0;
                string[] arrVers = fullVersion.Split('.');
                int.TryParse(arrVers[2], out res);
                return res;
            }
        }

        public int NumericVersion
        {
            [DebuggerStepThrough()]
            get
            {
                int res = this.Version * 100000 + this.Revision * 1000 + this.Release * 10;
                return res;
            }
        }

        public string ModuleNamespace
        {
            [DebuggerStepThrough()]
            get { return moduleNamespace; }
            [DebuggerStepThrough()]
            set { moduleNamespace = value; }
        }

        public string Name
        {
            [DebuggerStepThrough()]
            get { return name; }
            [DebuggerStepThrough()]
            set { name = value; }
        }

        public string Author
        {
            [DebuggerStepThrough()]
            get { return author; }
            [DebuggerStepThrough()]
            set { author = value; }
        }

        public DateTime CreationDate
        {
            [DebuggerStepThrough()]
            get { return creationDate; }
            [DebuggerStepThrough()]
            set { creationDate = value; }
        }

        public string Copyright
        {
            [DebuggerStepThrough()]
            get { return copyright; }
            [DebuggerStepThrough()]
            set { copyright = value; }
        }

        public string License
        {
            [DebuggerStepThrough()]
            get { return license; }
            [DebuggerStepThrough()]
            set { license = value; }
        }

        public string AuthorEmail
        {
            [DebuggerStepThrough()]
            get { return authorEmail; }
            [DebuggerStepThrough()]
            set { authorEmail = value; }
        }

        public string AuthorUrl
        {
            [DebuggerStepThrough()]
            get { return authorUrl; }
            [DebuggerStepThrough()]
            set { authorUrl = value; }
        }

        public string Description
        {
            [DebuggerStepThrough()]
            get { return description; }
            [DebuggerStepThrough()]
            set { description = value; }
        }

        public string IconClass
        {
            [DebuggerStepThrough()]
            get { return iconClass; }
            [DebuggerStepThrough()]
            set { iconClass = value; }
        }

        public string PanelClass
        {
            [DebuggerStepThrough()]
            get { return panelClass; }
            [DebuggerStepThrough()]
            set { panelClass = value; }
        }

        public List<string> InstallQueries
        {
            [DebuggerStepThrough()]
            get { return installQueries; }
            [DebuggerStepThrough()]
            set { installQueries = value; }
        }

        public List<string> InstallSqlFiles
        {
            [DebuggerStepThrough()]
            get { return installSqlFiles; }
            [DebuggerStepThrough()]
            set { installSqlFiles = value; }
        }

        public List<string> UninstallQueries
        {
            [DebuggerStepThrough()]
            get { return uninstallQueries; }
            [DebuggerStepThrough()]
            set { uninstallQueries = value; }
        }

        public List<string> UninstallSqlFiles
        {
            [DebuggerStepThrough()]
            get { return uninstallSqlFiles; }
            [DebuggerStepThrough()]
            set { uninstallSqlFiles = value; }
        }

        /*public List<FormField> CommonParams
        {
            [DebuggerStepThrough()]
            get { return commonParams; }
            [DebuggerStepThrough()]
            set { commonParams = value; }
        }*/

        public List<FormField> Params
        {
            [DebuggerStepThrough()]
            get { return _params; }
            [DebuggerStepThrough()]
            set { _params = value; }
        }

        /// <summary>
        /// tell if the module is a core module
        /// </summary>
        public bool IsCore
        {
            [DebuggerStepThrough()]
            get { return isCore; }
            [DebuggerStepThrough()]
            set { isCore = value; }
        }

        /// <summary>
        /// enable or not Module.DirectEditMode param
        /// </summary>
        public bool AllowDirectEditMode
        {
            [DebuggerStepThrough()]
            get { return allowDirectEditMode; }
            [DebuggerStepThrough()]
            set { allowDirectEditMode = value; }
        }

        /// <summary>
        /// specifices the assembly that contains the item class definition
        /// default value is PigeonCms.Core
        /// Used in ItemsProxy to apply IoC, ItemTypeName --> Item
        /// eg: Acme.MyPrj
        /// @20170214
        /// </summary>
        public string AssemblyString { get; set; } = "";
    }


    /// <summary>
    /// Filter used in search
    /// </summary>
    /// <remarks></remarks>
    [Serializable]
    public class XmlTypeFilter
    {
        private string fullName;
        private string fullNamePart = "";
        private string fullVersion = "1.0.0";
        private string moduleNamespace = "";
        private string name = "";
        private string author = "";
        private Utility.TristateBool isCore = Utility.TristateBool.NotSet;

        public string FullName
        {
            [DebuggerStepThrough()]
            get { return fullName; }
            [DebuggerStepThrough()]
            set { fullName = value; }
        }

        public string FullNamePart
        {
            [DebuggerStepThrough()]
            get { return fullNamePart; }
            [DebuggerStepThrough()]
            set { fullNamePart = value; }
        }

        public string FullVersion
        {
            [DebuggerStepThrough()]
            get { return fullVersion; }
            [DebuggerStepThrough()]
            set { fullVersion = value; }
        }

        public string ModuleNamespace
        {
            [DebuggerStepThrough()]
            get { return moduleNamespace; }
            [DebuggerStepThrough()]
            set { moduleNamespace = value; }
        }

        public string Name
        {
            [DebuggerStepThrough()]
            get { return name; }
            [DebuggerStepThrough()]
            set { name = value; }
        }

        public string Author
        {
            [DebuggerStepThrough()]
            get { return author; }
            [DebuggerStepThrough()]
            set { author = value; }
        }

        public Utility.TristateBool IsCore
        {
            [DebuggerStepThrough()]
            get { return isCore; }
            [DebuggerStepThrough()]
            set { isCore = value; }
        }
    }
}