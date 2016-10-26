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
using System.Threading;


namespace PigeonCms
{
    /// <summary>
    /// -AllPages: module shown in every page
    /// -NoPages: module never shown
    /// -List: list of pages detailed
    /// -MenuContent: main content of a menu entry
    /// </summary>
    public enum ModulesMenuSelection
    {
        AllPages = 0,
        NoPages = 1,
        List = 2, 
        MenuContent = 3
    }

    [DebuggerDisplay("Id={id}, Name={moduleNamespace}{moduleName}")]
    public class Module: ITableWithPermissions, ITableWithOrdering
    {
        private int id = 0;
        private string content = "";
        private int ordering = 0;
        private string templateBlockName = "";
        private bool published = true;
        private string moduleName = "";
        private string moduleNamespace = "";
        private DateTime dateInserted;
        private string userInserted = "";
        private DateTime dateUpdated;
        private string userUpdated = "";
        private bool showTitle = false;
        private string moduleParams = "";
        private bool isCore = false;
        ModulesMenuSelection menuSelection = ModulesMenuSelection.AllPages;
        List<int> modulesMenu = new List<int>();
        List<string> modulesMenuTypes = new List<string>();
        private string currView = "";
        
        //read permissions
        MenuAccesstype readAccessType = MenuAccesstype.Public;
        private int readPermissionId = 0;
        List<string> readRolenames = new List<string>();
        string readAccessCode = "";
        int readAccessLevel = 0;

        //write permissions
        MenuAccesstype writeAccessType = MenuAccesstype.Public;
        private int writePermissionId = 0;
        List<string> writeRolenames = new List<string>();
        private string writeAccessCode = "";
        private int writeAccessLevel = 0;

        string cssFile = "";
        string cssClass = "";
        Utility.TristateBool useCache = Utility.TristateBool.NotSet;
        Utility.TristateBool useLog = Utility.TristateBool.NotSet;
        private bool directEditMode = false;
        string systemMessagesTo = "";

        public Module()
        {
        }

        /// <summary>
        /// pkey identity 1,1
        /// </summary>
        [DataObjectField(true)]
        public int Id
        {
            [DebuggerStepThrough()]
            get { return id; }
            [DebuggerStepThrough()]
            set { id = value; }
        }


        string name = "";
        /// <summary>
        /// module name
        /// </summary>
        public string Name
        {
            [DebuggerStepThrough()]
            get { return name; }
            [DebuggerStepThrough()]
            set { name = value; }
        }

        /// <summary>
        /// Module Title in current culture
        /// </summary>
        [DataObjectField(false)]
        [Obsolete("Use Module.Name instead")]
        public string Title
        {
            get
            {
                string res = "";
                titleTranslations.TryGetValue(Thread.CurrentThread.CurrentCulture.Name, out res);
                if (string.IsNullOrEmpty(res))
                    titleTranslations.TryGetValue(Config.CultureDefault, out res);
                if (string.IsNullOrEmpty(res))
                    res = this.Name;
                if (string.IsNullOrEmpty(res))
                    res = this.ModuleFullName;
                return res;
            }
        }

        private Dictionary<string, string> titleTranslations = new Dictionary<string, string>();
        /// <summary>
        /// Title in different culture
        /// </summary>
        [DataObjectField(false)]
        [Obsolete("Use Module.Name instead")]
        public Dictionary<string, string> TitleTranslations
        {
            [DebuggerStepThrough()]
            get { return titleTranslations; }
            [DebuggerStepThrough()]
            set { titleTranslations = value; }
        }


        /// <summary>
        /// content of the page
        /// </summary>
        public string Content
        {
            [DebuggerStepThrough()]
            get { return content; }
            [DebuggerStepThrough()]
            set { content = value; }
        }

        /// <summary>
        /// module order when in the same templateBlock
        /// </summary>
        public int Ordering
        {
            [DebuggerStepThrough()]
            get { return ordering; }
            [DebuggerStepThrough()]
            set { ordering = value; }
        }

        /// <summary>
        /// FKey to TemplateBlocks
        /// </summary>
        public string TemplateBlockName
        {
            [DebuggerStepThrough()]
            get { return templateBlockName; }
            [DebuggerStepThrough()]
            set { templateBlockName = value; }
        }

        /// <summary>
        /// publish or not menu the module
        /// </summary>
        public bool Published
        {
            [DebuggerStepThrough()]
            get { return published; }
            [DebuggerStepThrough()]
            set { published = value; }
        }

        /// <summary>
        /// name of the module
        /// </summary>
        public string ModuleName
        {
            [DebuggerStepThrough()]
            get { return moduleName; }
            [DebuggerStepThrough()]
            set { moduleName = value; }
        }

        /// <summary>
        /// namespace of the module ex: Pigeon.Data
        /// </summary>
        public string ModuleNamespace
        {
            [DebuggerStepThrough()]
            get { return moduleNamespace; }
            [DebuggerStepThrough()]
            set { moduleNamespace = value; }
        }

        /// <summary>
        /// module namespace and name
        /// </summary>
        public string ModuleFullName
        {
            [DebuggerStepThrough()]
            get 
            {
                string res = "";
                if (!string.IsNullOrEmpty(moduleNamespace))
                    res += moduleNamespace + ".";
                res += moduleName;
                return res;
            }
        }

        /// <summary>
        /// record inserted date
        /// </summary>
        public DateTime DateInserted
        {
            [DebuggerStepThrough()]
            get { return dateInserted; }
            [DebuggerStepThrough()]
            set { dateInserted = value; }
        }

        /// <summary>
        /// record inserted user
        /// </summary>
        public string UserInserted
        {
            [DebuggerStepThrough()]
            get { return userInserted; }
            [DebuggerStepThrough()]
            set { userInserted = value; }
        }

        /// <summary>
        /// record updated date
        /// </summary>
        public DateTime DateUpdated
        {
            [DebuggerStepThrough()]
            get { return dateUpdated; }
            [DebuggerStepThrough()]
            set { dateUpdated = value; }
        }

        /// <summary>
        /// record updated user
        /// </summary>
        public string UserUpdated
        {
            [DebuggerStepThrough()]
            get { return userUpdated; }
            [DebuggerStepThrough()]
            set { userUpdated = value; }
        }

        /// <summary>
        /// show or not the module title
        /// </summary>
        public bool ShowTitle
        {
            [DebuggerStepThrough()]
            get { return showTitle; }
            [DebuggerStepThrough()]
            set { showTitle = value; }
        }

        /// <summary>
        /// inline serielized params for the module
        /// </summary>
        public string ModuleParams
        {
            [DebuggerStepThrough()]
            get { return moduleParams; }
            [DebuggerStepThrough()]
            set { moduleParams = value; }
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
        /// restrictions
        /// </summary>
        public ModulesMenuSelection MenuSelection
        {
            [DebuggerStepThrough()]
            get { return menuSelection; }
            [DebuggerStepThrough()]
            set { menuSelection = value; }
        }

        /// <summary>
        /// Menu entries where the module will be shown
        /// </summary>
        [DataObjectField(false)]
        public List<int> ModulesMenu
        {
            [DebuggerStepThrough()]
            get { return modulesMenu; }
            [DebuggerStepThrough()]
            set { modulesMenu = value; }
        }

        /// <summary>
        /// Menutypes where the module will be shown
        /// </summary>
        [DataObjectField(false)]
        public List<string> ModulesMenuTypes
        {
            [DebuggerStepThrough()]
            get { return modulesMenuTypes; }
            [DebuggerStepThrough()]
            set { modulesMenuTypes = value; }
        }

        ModuleType moduleType = null;
        public ModuleType ModuleType
        {
            //[DebuggerStepThrough()]
            get 
            {
                if (moduleType == null)
                {
                    moduleType = new ModuleTypeManager().GetByFullName(this.ModuleFullName);
                }
                return moduleType;
            }
        }

        string editContentUrl = null;
        /// <summary>
        /// url for direct content editing
        /// fill placeholders before use
        /// example: contentediturl.aspx?param1={param1}&param2={param2}
        /// </summary>
        public string EditContentUrl
        {
            get
            {
                if (editContentUrl == null)
                {
                    string res = "";
                    var menuMan = new MenuManager();
                    var menuTarget = new PigeonCms.Menu();
                    var filter = new PigeonCms.MenuFilter();
                    filter.Name = this.ModuleType.EditContentTag.MenuName;
                    filter.MenuType = this.ModuleType.EditContentTag.MenuType;
                    filter.ModuleFullName = this.ModuleType.EditContentTag.ModuleFullName;
                    if (!string.IsNullOrEmpty(filter.Name + filter.MenuType + filter.ModuleFullName))
                    {
                        var list = menuMan.GetByFilter(filter, "");
                        if (list.Count > 0)
                            res = Utility.GetRoutedUrl(list[0]);

                    }
                    if (!string.IsNullOrEmpty(res))
                    {
                        if (this.ModuleType.EditContentTag.EditParamsList.Count > 0)
                        {
                            var sepChar = "?";
                            var paramValue = "";
                            var paramsString = "";
                            foreach (var paramName in this.ModuleType.EditContentTag.EditParamsList)
                            {
                                if (!string.IsNullOrEmpty(paramName))
                                {
                                    this.Params.TryGetValue(paramName, out paramValue);

                                    if (string.IsNullOrEmpty(paramValue) || paramValue == "0")
                                    {
                                        if (HttpContext.Current.Items[paramName.ToLower()] != null)
                                        {
                                            paramValue = HttpContext.Current.Items[paramName.ToLower()].ToString().Replace(".aspx", "");
                                        }
                                        else if (HttpContext.Current.Request[paramName] != null)  //querystring param
                                        {
                                            paramValue = HttpContext.Current.Request[paramName].ToString();
                                        }
                                    }
                                    if (!string.IsNullOrEmpty(paramValue) && paramValue != "0")
                                    {
                                        paramsString += sepChar + paramName + "=" + paramValue;
                                        sepChar = "&";
                                    }
                                }
                            }
                            if (string.IsNullOrEmpty(paramsString))
                                res = "";
                            else
                                res += paramsString;
                        }
                    }
                    editContentUrl = res;
                }
                return editContentUrl;
            }
        }

        public Dictionary<string, string> Params
        {
            [DebuggerStepThrough()]
            get { return Utility.GetParamsDictFromString(this.ModuleParams); }
        }

        public string CurrView
        {
            [DebuggerStepThrough()]
            get 
            {
                if (string.IsNullOrEmpty(currView))
                    currView = "default.ascx";
                return currView; 
            }
            [DebuggerStepThrough()]
            set { currView = value; }
        }

        public string CurrViewFolder
        {
            [DebuggerStepThrough()]
            get
            {
                string res = this.CurrView;
                res = res.Replace(".ascx", "");
                return res;
            }
        }

        public MenuAccesstype ReadAccessType
        {
            [DebuggerStepThrough()]
            get { return readAccessType; }
            [DebuggerStepThrough()]
            set { readAccessType = value; }
        }

        public int ReadPermissionId
        {
            [DebuggerStepThrough()]
            get { return readPermissionId; }
            [DebuggerStepThrough()]
            set { readPermissionId = value; }
        }

        /// <summary>
        /// roles allowed depending on this.PermissionId
        /// </summary>
        [DataObjectField(false)]
        public List<string> ReadRolenames
        {
            [DebuggerStepThrough()]
            get { return readRolenames; }
            [DebuggerStepThrough()]
            set { readRolenames = value; }
        }

        public string ReadAccessCode
        {
            [DebuggerStepThrough()]
            get { return readAccessCode; }
            [DebuggerStepThrough()]
            set { readAccessCode = value; }
        }

        public int ReadAccessLevel
        {
            [DebuggerStepThrough()]
            get { return readAccessLevel; }
            [DebuggerStepThrough()]
            set { readAccessLevel = value; }
        }

        public MenuAccesstype WriteAccessType
        {
            [DebuggerStepThrough()]
            get { return writeAccessType; }
            [DebuggerStepThrough()]
            set { writeAccessType = value; }
        }

        public int WritePermissionId
        {
            [DebuggerStepThrough()]
            get { return writePermissionId; }
            [DebuggerStepThrough()]
            set { writePermissionId = value; }
        }

        [DataObjectField(false)]
        public List<string> WriteRolenames
        {
            [DebuggerStepThrough()]
            get { return writeRolenames; }
            [DebuggerStepThrough()]
            set { writeRolenames = value; }
        }

        public string WriteAccessCode
        {
            [DebuggerStepThrough()]
            get { return writeAccessCode; }
            [DebuggerStepThrough()]
            set { writeAccessCode = value; }
        }

        public int WriteAccessLevel
        {
            [DebuggerStepThrough()]
            get { return writeAccessLevel; }
            [DebuggerStepThrough()]
            set { writeAccessLevel = value; }
        }

        public string CssFile
        {
            [DebuggerStepThrough()]
            get { return cssFile; }
            [DebuggerStepThrough()]
            set { cssFile = value; }
        }

        public string CssClass
        {
            [DebuggerStepThrough()]
            get { return cssClass; }
            [DebuggerStepThrough()]
            set { cssClass = value; }
        }

        public Utility.TristateBool UseCache
        {
            [DebuggerStepThrough()]
            get { return useCache; }
            [DebuggerStepThrough()]
            set { useCache = value; }
        }

        public Utility.TristateBool UseLog
        {
            [DebuggerStepThrough()]
            get { return useLog; }
            [DebuggerStepThrough()]
            set { useLog = value; }
        }

        /// <summary>
        /// modules that allow direct item editing. ex:edit a static page from a public page
        /// </summary>
        public bool DirectEditMode
        {
            [DebuggerStepThrough()]
            get { return directEditMode; }
            [DebuggerStepThrough()]
            set { directEditMode = value; }
        }

        /// <summary>
        /// toList of members allowed to receive module messages (if any)
        /// example: admin; user1; user2
        /// leave blank for no messages
        /// </summary>
        public string SystemMessagesTo
        {
            [DebuggerStepThrough()]
            get { return systemMessagesTo; }
            [DebuggerStepThrough()]
            set { systemMessagesTo = value; }
        }

        public override string ToString()
        {
            return "Id=" + this.Id + ", Name=" + this.ModuleFullName + ", Title=" + this.Title;
        }
    }


    /// <summary>
    /// Filter used in search
    /// </summary>
    /// <remarks></remarks>
    [Serializable]
    public class ModulesFilter
    {
        #region fields definition
        private int id = 0;
        private string templateBlockName = "";
        private Utility.TristateBool published = Utility.TristateBool.NotSet;
        private string moduleName = "";
        private string moduleNamespace = "";
        private int menuId = 0;
        private string menuType = "";
        private Utility.TristateBool isContent = Utility.TristateBool.NotSet;
        private Utility.TristateBool isCore = Utility.TristateBool.NotSet;


        public int Id
        {
            [DebuggerStepThrough()]
            get { return id; }
            [DebuggerStepThrough()]
            set { id = value; }
        }

        public string TemplateBlockName
        {
            [DebuggerStepThrough()]
            get { return templateBlockName; }
            [DebuggerStepThrough()]
            set { templateBlockName = value; }
        }

        public Utility.TristateBool Published
        {
            [DebuggerStepThrough()]
            get { return published; }
            [DebuggerStepThrough()]
            set { published = value; }
        }

        public string ModuleName
        {
            [DebuggerStepThrough()]
            get { return moduleName; }
            [DebuggerStepThrough()]
            set { moduleName = value; }
        }

        public string ModuleNamespace
        {
            [DebuggerStepThrough()]
            get { return moduleNamespace; }
            [DebuggerStepThrough()]
            set { moduleNamespace = value; }
        }

        public int MenuId
        {
            [DebuggerStepThrough()]
            get { return menuId; }
            [DebuggerStepThrough()]
            set { menuId = value; }
        }

        public string MenuType
        {
            [DebuggerStepThrough()]
            get { return menuType; }
            [DebuggerStepThrough()]
            set { menuType = value; }
        }

        public Utility.TristateBool IsContent
        {
            [DebuggerStepThrough()]
            get { return isContent; }
            [DebuggerStepThrough()]
            set { isContent = value; }
        }

        public Utility.TristateBool IsCore
        {
            [DebuggerStepThrough()]
            get { return isCore; }
            [DebuggerStepThrough()]
            set { isCore = value; }
        }

        #endregion

    }
}