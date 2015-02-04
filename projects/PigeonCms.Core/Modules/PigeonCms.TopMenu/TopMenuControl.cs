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
using System.Collections.Generic;
using System.Threading;
using System.Text;
using PigeonCms;
using System.Web.Routing;


namespace PigeonCms
{
    public class TopMenuControl: PigeonCms.BaseModuleControl
    {
        protected class MenuTemplate
        {
            public class MenuAttributes
            {
                public string MenuCssClass = "";
                public string MenuStyle = "";
                public string MenuId = "";
                public string ItemSelectedClass = "";
                public string ItemCssClass = "";
                public string ItemStyle = "";
            }

            public string Header = "<ul class='{{MenuCssClass}} [[MenuCssClass]]' style='{{MenuStyle}} [[MenuStyle]]' id='[[MenuId]]'>";
            public string ItemHeader = "<li class='[[ItemSelectedClass]] [[ItemCssClass]]'>"
                + "<a href='[[ItemHref]]' onclick='[[ItemOnClick]]' class='{{ItemCssClass}} [[ItemSelectedClass]] [[ItemCssClass]]' style='{{ItemStyle}} [[ItemStyle]]'>"
                + "[[ItemContent]]"
                + "</a>";
            public string ItemFooter = "</li>";
            public string Footer = "</ul>";

            public MenuAttributes Style;

            public MenuTemplate() : this(new MenuAttributes()) { }

            public MenuTemplate(MenuAttributes style)
            {
                this.Style = style;
                this.ReBuild(style);
            }

            public void ReBuild(MenuAttributes style)
            {
                this.Header = this.Header
                    .Replace("{{MenuCssClass}}", style.MenuCssClass)
                    .Replace("{{MenuStyle}}", style.MenuStyle);

                //override the one set in backend
                if (!string.IsNullOrEmpty(style.MenuId))
                    Header = Header.Replace("[[MenuId]]", style.MenuId);


                this.ItemHeader = this.ItemHeader
                    .Replace("{{ItemCssClass}}", style.ItemCssClass)
                    .Replace("{{ItemStyle}}", style.ItemStyle);
            }
        }

        protected MenuTemplate Template = new MenuTemplate();

        #region public fields

        private string menuId = "";
        public string MenuId
        {
            get 
            {
                string res = "";
                res = GetStringParam("MenuId", menuId);
                if (string.IsNullOrEmpty(res))
                    res = "listMenuRoot";
                return res;
            }
            set { menuId = value; }
        }

        /// <summary>
        /// wrapper for base.BaseModule.CssClass with default value
        /// </summary>
        public string CssClass
        {
            get 
            {
                string res = base.BaseModule.CssClass;
                if (string.IsNullOrEmpty(res))
                    res = "menulist";
                return res; 
            }
        }

        private string itemSelectedClass = "";  //default selected
        public string ItemSelectedClass
        {
            get 
            {
                string res = this.Template.Style.ItemSelectedClass;
                if (string.IsNullOrEmpty(res))
                    res = GetStringParam("ItemSelectedClass", itemSelectedClass);
                return res;
            }
            set { itemSelectedClass = value; }
        }

        private string itemLastClass = "";  //default last
        public string ItemLastClass
        {
            get { return GetStringParam("ItemLastClass", itemLastClass); }
            set { itemLastClass = value; }
        }

        private string menuType = "";
        public string MenuType
        {
            get { return GetStringParam("MenuType", menuType); }
            set { menuType = value; }
        }

        private int menuLevel = 0;      //menu entries level
        public int MenuLevel
        {
            get { return GetIntParam("MenuLevel", menuLevel); }
            set { menuLevel = value; }
        }

        private Utility.TristateBool showChild = Utility.TristateBool.NotSet; //show or not child entries
        public Utility.TristateBool ShowChild
        {
            get 
            {
                int res = (int)showChild;
                res = GetIntParam("ShowChild", res);
                return (PigeonCms.Utility.TristateBool)res;
            }
            set { showChild = value; }
        }

        public string PageName
        {
            get 
            {
                string pageName = Request.Url.Segments[Request.Url.Segments.Length - 1];
                pageName = pageName.Replace(".aspx", "");
                return pageName;
            }
        }

        List<PigeonCms.Menu> itemsList = new List<PigeonCms.Menu>();
        public List<PigeonCms.Menu> ItemsList
        {
            get
            {
                MenuFilter filter = new MenuFilter();
                filter.FilterContentType = false;
                filter.MenuType = this.MenuType;
                filter.Visible = Utility.TristateBool.True;
                itemsList = new MenuManager(true, false).GetByFilter(filter, "menuType, t.ParentId, t.Ordering");;
                return itemsList;
            }
        }
        #endregion


        /// <summary>
        /// load recursively the hierarchical menu
        /// </summary>
        /// <param name="result">the result string (UL list)</param>
        /// <param name="menuFilter"></param>
        /// <param name="currLevel">the current level renderer</param>
        /// <param name="selectedBranch">true if we are in the branch of selected entry</param>
        private void loadTree(ref string result, MenuFilter menuFilter, int currLevel, List<int> selectedIdList, bool selectedBranch)
        {
            const int MaxLevel = 10;
            int recordCount = 0;
            string itemCssClass = "";
            string itemSelectedClass = "";
            bool hideEntry = false;

            currLevel++;
            if (currLevel < MaxLevel)
            {
                List<PigeonCms.Menu> recordList =
                    new MenuManager(true, false).GetByFilter(menuFilter, "MenuType, t.ParentId, t.Ordering");

                ParseMenuList(recordList);

                string ul = "";
                if (recordList.Count > 0)
                {
                    ul = Template.Header; 
                    
                    string menuId = "";
                    string menuStyle = "";
                    if (currLevel == 1)
                        menuId = this.MenuId; //ul += " id='" + this.MenuId + "' ";

                    if (currLevel > 1)
                    {
                        if (this.ShowChild == Utility.TristateBool.False)
                            hideEntry = true;
                        
                        if (this.ShowChild == Utility.TristateBool.NotSet) 
                        {
                            if (!selectedIdList.Exists(
                                    delegate(int id) {
                                        foreach (var record in recordList)
                                        { if (record.Id == id) return true; }
                                        return false;
                                    }
                                )
                            )
                            {
                                hideEntry = true;
                            }

                            if (selectedBranch)
                                hideEntry = false;
                        }
                    }
                    if (hideEntry)
                        menuStyle = " display:none;";

                    ul = ul
                        .Replace("[[MenuCssClass]]", this.CssClass)
                        .Replace("[[MenuId]]", menuId)
                        .Replace("[[MenuStyle]]", menuStyle);
                }
                
                result += ul;

                foreach (PigeonCms.Menu record1 in recordList)
                {
                    recordCount++;

                    itemCssClass = "";
                    itemSelectedClass = "";
                    if (selectedIdList.Contains(record1.Id))
                    {
                        itemSelectedClass = this.ItemSelectedClass;
                        selectedBranch = true;
                    }
                    else
                        selectedBranch = false;
                    if (!string.IsNullOrEmpty(record1.CssClass))
                    {
                        itemCssClass += " " + record1.CssClass;
                    }
                    if (/*currLevel == 0 &&*/ recordCount == recordList.Count)
                    {
                        itemCssClass += " " + this.ItemLastClass;
                    }
                    result += Template.ItemHeader
                        .Replace("[[ItemSelectedClass]]", itemSelectedClass)
                        .Replace("[[ItemCssClass]]", itemCssClass)
                        .Replace("[[ItemHref]]", record1.Url)
                        .Replace("[[ItemOnClick]]", record1.Onclick)
                        .Replace("[[ItemStyle]]", "")
                        .Replace("[[ItemContent]]", record1.Title);

                    menuFilter.ParentId = record1.Id;
                    loadTree(ref result, menuFilter, currLevel, selectedIdList, selectedBranch);

                    result += Template.ItemFooter;
                }
                if (recordList.Count > 0)
                {
                    result += Template.Footer;
                }
            }
        }

        /// <summary>
        /// apply other filters to menu list if needed by child controls
        /// </summary>
        /// <param name="list"></param>
        protected virtual void ParseMenuList(List<Menu>list){}

        protected virtual string GetContent()
        {
            string result = "";
            int startLevel = 0;
            var filter = new MenuFilter();
            var menuMan = new MenuManager(true, false);
            //var currentMenu = MenuHelper.GetCurrentMenu(this.MenuType);  //hide 20110325
            var currentMenu = ((PigeonCms.BasePage)this.Page).MenuEntry;
            var selectedIdList = new List<int>();     //list of parent id for current menu
                
            filter.FilterContentType = false;
            filter.MenuType = this.MenuType;
            filter.Visible = Utility.TristateBool.True;
            filter.ParentId = 0;

            selectedIdList = menuMan.GetParentIdList(currentMenu.Id);

            if (this.MenuLevel > 0)
            {
                //set current page as start level (used to build context menu)
                if (currentMenu.Id > 0)
                {
                    int currentMenuLevel = menuMan.GetMenuLevel(currentMenu.Id);
                    if (currentMenuLevel < this.MenuLevel)
                    {
                        filter.ParentId = currentMenu.Id;
                    }
                    else if (currentMenuLevel == this.MenuLevel)
                    {
                        filter.ParentId = currentMenu.ParentId;
                    }
                    else if (currentMenuLevel > this.MenuLevel)
                    {
                        //TO COMPLETE
                        filter.ParentId = menuMan.GetByKey(currentMenu.ParentId).ParentId;
                    }
                }
                else
                {
                    //if not found current page in menu does not load menu
                    filter.Id = -1;
                }
            }

            loadTree(ref result, filter, startLevel, selectedIdList, false);
            return result;
        }
    }
}