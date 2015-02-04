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
using System.IO;
using PigeonCms;
using System.Collections.Generic;
using System.Threading;


namespace PigeonCms
{
    /// <summary>
    /// type of access to menu entries (pgn_menu.accessType)
    /// </summary>
    public enum MenuAccesstype
    {
        Public = 0,
        Registered = 1
        //Special = 2
    }

    /// <summary>
    /// type of content of menu entries (pgn_menu.contentType)
    /// </summary>
    public enum MenuContentType
    {
        Module = 0,
        Url = 1,
        Javascript = 2, 
        Alias = 3
    }

    /// <summary>
    /// menu entries
    /// </summary>
    [DebuggerDisplay("Id={id}, Alias={alias}")]
    public class Menu: ITableWithPermissions, ITableWithOrdering
    {
        private int id = 0;
        private string menuType = "";
        private string name = "";
        private string alias = "";
        private string link = "";
        private MenuContentType contentType = MenuContentType.Module;
        private bool published = true;
        private int parentId = 0;
        private int moduleId = 0;
        private int ordering = 0;
        private bool overridePageTitle = true;
        private int referMenuId = 0;
        private string currMasterPageStored = "";
        private string routeMasterPage = ""; //masterpage stored in current route
        private string currThemeStored = "";
        private string routeTheme = "";     //theme stored in current route

        private string cssClass = "";
        private bool visible = false;
        private int routeId = 0;
        private string routeName = "";
        private string routePattern = "";

        //read permissions
        private MenuAccesstype readAccessType = MenuAccesstype.Public;
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

        private bool isCore = false;
        Utility.TristateBool useSsl = Utility.TristateBool.NotSet;
        private bool routeUseSsl = false;

        private Dictionary<string, string> titleTranslations = new Dictionary<string, string>();
        private Dictionary<string, string> titleWindowTranslations = new Dictionary<string, string>();

        public Menu()
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

        /// <summary>
        /// FKey to menuTypes.menuType
        /// </summary>
        public string MenuType
        {
            [DebuggerStepThrough()]
            get { return menuType; }
            [DebuggerStepThrough()]
            set { menuType = value; }
        }

        /// <summary>
        /// name of menu voice
        /// </summary>
        public string Name
        {
            [DebuggerStepThrough()]
            get { return name; }
            [DebuggerStepThrough()]
            set { name = value; }
        }

        /// <summary>
        /// menu voice alias
        /// </summary>
        public string Alias
        {
            [DebuggerStepThrough()]
            get { return alias; }
            [DebuggerStepThrough()]
            set { alias = value; }
        }

        /// <summary>
        /// link (for external url)
        /// </summary>
        public string Link
        {
            [DebuggerStepThrough()]
            get { return link; }
            [DebuggerStepThrough()]
            set { link = value; }
        }

        /// <summary>
        /// type of content (module|url|javascript|..)
        /// </summary>
        public MenuContentType ContentType
        {
            [DebuggerStepThrough()]
            get { return contentType; }
            [DebuggerStepThrough()]
            set { contentType = value; }
        }

        /// <summary>
        /// content reachable or not (also if menu voice is not visible)
        /// </summary>
        public bool Published
        {
            [DebuggerStepThrough()]
            get { return published; }
            [DebuggerStepThrough()]
            set { published = value; }
        }

        /// <summary>
        /// Id of parent menu voice
        /// </summary>
        public int ParentId
        {
            [DebuggerStepThrough()]
            get { return parentId; }
            [DebuggerStepThrough()]
            set { parentId = value; }
        }

        /// <summary>
        /// Id of module shown as content
        /// </summary>
        public int ModuleId
        {
            [DebuggerStepThrough()]
            get { return moduleId; }
            [DebuggerStepThrough()]
            set { moduleId = value; }
        }

        /// <summary>
        /// menu voice order
        /// </summary>
        public int Ordering
        {
            [DebuggerStepThrough()]
            get { return ordering; }
            [DebuggerStepThrough()]
            set { ordering = value; }
        }

        /// <summary>
        /// if true override the default page title
        /// </summary>
        public bool OverridePageTitle
        {
            [DebuggerStepThrough()]
            get { return overridePageTitle; }
            [DebuggerStepThrough()]
            set { overridePageTitle = value; }
        }

        /// <summary>
        /// in case of contentType=alias, redirect to referMenuId menu entry
        /// </summary>
        public int ReferMenuId
        {
            [DebuggerStepThrough()]
            get { return referMenuId; }
            [DebuggerStepThrough()]
            set { referMenuId = value; }
        }

        /// <summary>
        /// Theme (name) for current menu entry (default theme if not set or not exists)
        /// Theme hierarchy: menu>route>config
        /// </summary>
        public string CurrTheme
        {
            get
            {
                //menu theme
                string res = currThemeStored;
                //route
                if (string.IsNullOrEmpty(res) ||
                    !System.IO.Directory.Exists(
                    HttpContext.Current.Server.MapPath("~/App_Themes/" + res)))
                {
                    res = this.routeTheme;
                }
                //config
                if (string.IsNullOrEmpty(res) ||
                    !System.IO.Directory.Exists(
                    HttpContext.Current.Server.MapPath("~/App_Themes/" + res)))
                {
                    res = Config.CurrentTheme; //"default";
                }
                return res;
            }
        }

        /// <summary>
        /// Stored Theme for current menu entry (empty if not set)
        /// </summary>
        public string CurrThemeStored
        {
            [DebuggerStepThrough()]
            get { return currThemeStored; }
            [DebuggerStepThrough()]
            set { currThemeStored = value; }
        }

        /// <summary>
        /// Theme stored in current menu route
        /// </summary>
        public string RouteTheme
        {
            [DebuggerStepThrough()]
            get { return routeTheme; }
            [DebuggerStepThrough()]
            set { routeTheme = value; }
        }

        /// <summary>
        /// masterpage (name) for current menu entry (default masterpage if not set)
        /// masterpage hierarchy: menu>route>config
        /// </summary>
        public string CurrMasterPage
        {
            [DebuggerStepThrough()]
            get
            {
                //menu masterpage
                string res = currMasterPageStored;
                //route
                if (string.IsNullOrEmpty(res))
                    res = this.routeMasterPage;
                //config
                if (string.IsNullOrEmpty(res))
                    res = Config.CurrentMasterPage; //"default";
                return res;
            }
        }

        /// <summary>
        /// Stored Masterpage for current menu entry (empty if not set)
        /// </summary>
        public string CurrMasterPageStored
        {
            [DebuggerStepThrough()]
            get { return currMasterPageStored; }
            [DebuggerStepThrough()]
            set { currMasterPageStored = value; }
        }

        /// <summary>
        /// Masterpagte stored in current menu route
        /// </summary>
        public string RouteMasterPage
        {
            [DebuggerStepThrough()]
            get { return routeMasterPage; }
            [DebuggerStepThrough()]
            set { routeMasterPage = value; }
        }

        /// <summary>
        /// masterpage path
        /// </summary>
        public string CurrMasterPageFile
        {
            get
            {
                var ctx = HttpContext.Current;
                string path = ctx.Request.MapPath(Config.MasterPagesPath);
                bool isMobile = Utility.Mobile.IsMobileDevice(ctx);
                FileInfo file;

                if (isMobile)
                {
                    file = new FileInfo(path + this.CurrMasterPage + "-mobile.master");
                    if (file.Exists)
                        return Config.MasterPagesPath + this.CurrMasterPage + "-mobile.master";
                }

                file = new FileInfo(path + this.CurrMasterPage + ".master");
                if (file.Exists)
                    return Config.MasterPagesPath + this.CurrMasterPage + ".master";


                if (isMobile)
                {
                    file = new FileInfo(path + Config.CurrentMasterPage + "-mobile.master");
                    if (file.Exists)
                        return Config.MasterPagesPath + Config.CurrentMasterPage + "-mobile.master";
                }
                return Config.MasterPagesPath + Config.CurrentMasterPage + ".master";
            }
        }

        /// <summary>
        /// Css class for current menu entry
        /// </summary>
        public string CssClass
        {
            [DebuggerStepThrough()]
            get { return cssClass; }
            [DebuggerStepThrough()]
            set { cssClass= value; }
        }

        /// <summary>
        /// menu voice visible or not (item must be published too)
        /// </summary>
        public bool Visible
        {
            [DebuggerStepThrough()]
            get { return visible; }
            [DebuggerStepThrough()]
            set { visible = value; }
        }

        /// <summary>
        /// link to MvcRoute.Id
        /// </summary>
        public int RouteId
        {
            [DebuggerStepThrough()]
            get { return routeId; }
            [DebuggerStepThrough()]
            set { routeId = value; }
        }

        /// <summary>
        /// link to MvcRoute.Name (readonly)
        /// </summary>
        public string RouteName
        {
            [DebuggerStepThrough()]
            get { return routeName; }
            [DebuggerStepThrough()]
            set { routeName = value; }
        }

        /// <summary>
        /// link to MvcRoute.Pattern (readonly)
        /// </summary>
        public string RoutePattern
        {
            [DebuggerStepThrough()]
            get { return routePattern; }
            [DebuggerStepThrough()]
            set { routePattern = value; }
        }

        /// <summary>
        /// restrictions to menu voice and content
        /// </summary>
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

        public bool IsCore
        {
            [DebuggerStepThrough()]
            get { return isCore; }
            [DebuggerStepThrough()]
            set { isCore = value; }
        }

        public Utility.TristateBool UseSsl
        {
            [DebuggerStepThrough()]
            get { return useSsl; }
            [DebuggerStepThrough()]
            set { useSsl = value; }
        }

        public bool RouteUseSsl
        {
            [DebuggerStepThrough()]
            get { return routeUseSsl; }
            [DebuggerStepThrough()]
            set { routeUseSsl = value; }
        }

        /// <summary>
        /// use ssl in current page
        /// </summary>
        public bool CurrentUseSsl
        {
            get 
            {
                bool res = false;
                switch (this.UseSsl)
                {
                    case Utility.TristateBool.False:
                        res = false;
                        break;
                    case Utility.TristateBool.True:
                        res = true;
                        break;
                    case Utility.TristateBool.NotSet:
                        res = this.RouteUseSsl;
                        break;
                }
                return res;
            }
        }


        /// <summary>
        /// javascript onclick action
        /// </summary>
        [DataObjectField(false)]
        public string Onclick
        {
            get
            {
                string res = "";
                if (this.ContentType == MenuContentType.Javascript)
                {
                    res = this.Link;
                }
                return res;
            }
        }

        /// <summary>
        /// menu url
        /// </summary>
        [DataObjectField(false)]
        public string Url
        {
            get
            {
                string res = "";
                if (this.ContentType == MenuContentType.Url)
                {
                    res = this.Link;
                    if (res.StartsWith("~"))
                    {
                        res = Utility.GetAbsoluteUrl(res.Substring(1));
                    }
                }
                else if (this.ContentType == MenuContentType.Javascript)
                {
                    res = "javascript:void(0);";
                }
                else
                {
                    try
                    {
                        res = Utility.GetRoutedUrl(this, "", Config.AddPageSuffix);
                    }
                    catch
                    {
                        res = VirtualPathUtility.ToAbsolute("~/") + "pages/" + this.Alias.ToLower() + ".aspx";
                    }
                }
                //if (this.CurrentUseSsl)
                //{
                //    res = Utility.GetAbsoluteUrl(res, true);
                //}
                //else if (HttpContext.Current.Request.IsSecureConnection && !this.CurrentUseSsl)
                //{
                //    res = Utility.GetAbsoluteUrl(res, false);
                //}
                return res;
            }
        }

        /// <summary>
        /// Menu Title in current culture
        /// </summary>
        [DataObjectField(false)]
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
                return res;
            }
        }

        /// <summary>
        /// Title in different culture
        /// </summary>
        [DataObjectField(false)]
        public Dictionary<string, string> TitleTranslations
        {
            [DebuggerStepThrough()]
            get { return titleTranslations; }
            [DebuggerStepThrough()]
            set { titleTranslations = value; }
        }

        ///hide on 20100305: not used
        //public bool IsTitleTranslated
        //{
        //    get
        //    {
        //        bool res = true;
        //        string val = "";
        //        titleTranslations.TryGetValue(Thread.CurrentThread.CurrentCulture.Name, out val);
        //        if (string.IsNullOrEmpty(val))
        //            res = false;
        //        return res;
        //    }
        //}

        /// <summary>
        /// Window or browser Title in current culture
        /// </summary>
        [DataObjectField(false)]
        public string TitleWindow
        {
            get
            {
                string res = "";
                titleWindowTranslations.TryGetValue(Thread.CurrentThread.CurrentCulture.Name, out res);
                if (string.IsNullOrEmpty(res))
                    titleWindowTranslations.TryGetValue(Config.CultureDefault, out res);
                if (string.IsNullOrEmpty(res))
                    res = this.Title;
                return res;
            }
        }

        /// <summary>
        /// Window or browser Title in different culture
        /// </summary>
        [DataObjectField(false)]
        public Dictionary<string, string> TitleWindowTranslations
        {
            [DebuggerStepThrough()]
            get { return titleWindowTranslations; }
            [DebuggerStepThrough()]
            set { titleWindowTranslations = value; }
        }

        PigeonCms.Menu refMenu = null;
        public PigeonCms.Menu RefMenu
        {
            get
            {
                if (refMenu == null)
                {
                    if (this.ContentType == MenuContentType.Alias)
                        refMenu = new MenuManager(true, false).GetByKey(this.ReferMenuId);
                    else
                        refMenu = new PigeonCms.Menu();
                }
                return refMenu;
            }
        }

        Module contentModule = null;
        /// <summary>
        /// content module instance of current menu
        /// </summary>
        public Module ContentModule
        {
            get
            {
                if (contentModule == null)
                {
                    int contentModuleId = this.ModuleId;
                    //check if alias
                    if (this.ContentType == MenuContentType.Alias)
                    {
                        //load referring menu entry modules
                        contentModuleId = this.RefMenu.ModuleId;
                    }
                    if (contentModuleId > 0)
                        contentModule = new ModulesManager().GetByKey(contentModuleId);
                    else
                        contentModule = new Module();
                }
                return contentModule;
            }
        }

        List<Module> modulesList = null;
        /// <summary>
        /// list of modules for current menu
        /// </summary>
        public List<Module> ModulesList
        {
            get
            {
                if (modulesList == null)
                {
                    var filter = new ModulesFilter();
                    filter.Published = Utility.TristateBool.True;
                    if (this.ContentType == MenuContentType.Alias)
                    {
                        filter.MenuId = this.RefMenu.Id;
                        filter.MenuType = this.RefMenu.MenuType;
                    }
                    else
                    {
                        filter.MenuId = this.Id;
                        filter.MenuType = this.MenuType;
                    }
                    modulesList = new ModulesManager(true, false).GetByFilter(filter, "");

                    if (modulesList.Count == 0)
                        modulesList = new List<Module>();
                }
                return modulesList;
            }
        }

        public PigeonCms.Menu Copy()
        {
            return (PigeonCms.Menu)this.MemberwiseClone();
        }

        public override string ToString()
        {
            return "Id=" + this.Id + ", ModuleId=" + this.ModuleId + ", Alias=" + this.Alias;

        }
    }


    /// <summary>
    /// menu component
    /// </summary>
    public class Menutype : ITable
    {
        private int id = 0;
        private string menuType = "";
        private string title = "";
        private string description = "";

        public Menutype()
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

        /// <summary>
        /// Unique Index
        /// </summary>
        public string MenuType
        {
            [DebuggerStepThrough()]
            get { return menuType; }
            [DebuggerStepThrough()]
            set { menuType = value; }
        }

        /// <summary>
        /// title of menu
        /// </summary>
        public string Title
        {
            [DebuggerStepThrough()]
            get { return title; }
            [DebuggerStepThrough()]
            set { title = value; }
        }

        /// <summary>
        /// short description of menu
        /// </summary>
        public string Description
        {
            [DebuggerStepThrough()]
            get { return description; }
            [DebuggerStepThrough()]
            set { description = value; }
        }
    }

    /// <summary>
    /// Filter used in search
    /// </summary>
    /// <remarks></remarks>
    [Serializable]
    public class MenuFilter
    {
        #region fields definition
        private int id = 0;
        private string menuType = "";
        private string name = "";
        private string alias = "";
        private bool filterContentType = false;
        private MenuContentType contentType = MenuContentType.Module;
        private Utility.TristateBool published = Utility.TristateBool.NotSet;
        private int parentId = -1;
        private int moduleId = 0;
        private string moduleFullName = "";     //referred to moduleId (moduleNamespace.moduleName)
        private int referMenuId = 0;
        private string currMasterPage = "";
        private string currTheme = "";
        private Utility.TristateBool visible = Utility.TristateBool.NotSet;
        private int routeId = 0;
        private string routePattern = "";
        private Utility.TristateBool isCore = Utility.TristateBool.NotSet;


        public int Id
        {
            [DebuggerStepThrough()]
            get { return id; }
            [DebuggerStepThrough()]
            set { id = value; }
        }

        public string MenuType
        {
            [DebuggerStepThrough()]
            get { return menuType; }
            [DebuggerStepThrough()]
            set { menuType = value; }
        }

        public string Name
        {
            [DebuggerStepThrough()]
            get { return name; }
            [DebuggerStepThrough()]
            set { name = value; }
        }

        public string Alias
        {
            [DebuggerStepThrough()]
            get { return alias; }
            [DebuggerStepThrough()]
            set { alias = value; }
        }

        /// <summary>
        /// apply or not filter on ContentType
        /// </summary>
        public bool FilterContentType
        {
            [DebuggerStepThrough()]
            get { return filterContentType; }
            [DebuggerStepThrough()]
            set { filterContentType = value; }
        }

        public MenuContentType ContentType
        {
            [DebuggerStepThrough()]
            get { return contentType; }
            [DebuggerStepThrough()]
            set { contentType = value; }
        }

        public Utility.TristateBool Published
        {
            [DebuggerStepThrough()]
            get { return published; }
            [DebuggerStepThrough()]
            set { published = value; }
        }

        public int ParentId
        {
            [DebuggerStepThrough()]
            get { return parentId; }
            [DebuggerStepThrough()]
            set { parentId = value; }
        }

        public int ModuleId
        {
            [DebuggerStepThrough()]
            get { return moduleId; }
            [DebuggerStepThrough()]
            set { moduleId = value; }
        }

        public string ModuleFullName
        {
            [DebuggerStepThrough()]
            get { return moduleFullName; }
            [DebuggerStepThrough()]
            set { moduleFullName = value; }
        }

        public int ReferMenuId
        {
            [DebuggerStepThrough()]
            get { return referMenuId; }
            [DebuggerStepThrough()]
            set { referMenuId = value; }
        }

        public string CurrMasterPage
        {
            [DebuggerStepThrough()]
            get { return currMasterPage; }
            [DebuggerStepThrough()]
            set { currMasterPage = value; }
        }

        public string CurrTheme
        {
            [DebuggerStepThrough()]
            get { return currTheme; }
            [DebuggerStepThrough()]
            set { currTheme = value; }
        }

        public Utility.TristateBool Visible
        {
            [DebuggerStepThrough()]
            get { return visible; }
            [DebuggerStepThrough()]
            set { visible = value; }
        }

        public int RouteId
        {
            [DebuggerStepThrough()]
            get { return routeId; }
            [DebuggerStepThrough()]
            set { routeId = value; }
        }

        public string RoutePattern
        {
            [DebuggerStepThrough()]
            get { return routePattern; }
            [DebuggerStepThrough()]
            set { routePattern = value; }
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

    /// <summary>
    /// Filter used in search
    /// </summary>
    /// <remarks></remarks>
    public class MenutypeFilter
    {
        private int id = 0;
        private string menuType = "";

        public int Id
        {
            [DebuggerStepThrough()]
            get { return id; }
            [DebuggerStepThrough()]
            set { id = value; }
        }

        public string MenuType
        {
            [DebuggerStepThrough()]
            get { return menuType; }
            [DebuggerStepThrough()]
            set { menuType = value; }
        }
    }
}