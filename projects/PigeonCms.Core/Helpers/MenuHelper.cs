using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using PigeonCms;
using System.Collections.Generic;

namespace PigeonCms
{
    /// <summary>
    /// Useful static functions to manage menus
    /// </summary>
    public static class MenuHelper
    {
        public static PigeonCms.Menu GetCurrentMenu(string  menuType)
        {
            var res = new PigeonCms.Menu();
            var menuMan = new MenuManager(true, false);
            var filter = new MenuFilter();
            filter.FilterContentType = false;
            filter.MenuType = menuType;
            filter.Alias = MenuHelper.GetCurrentAlias();
            filter.RoutePattern = MenuHelper.GetCurrentRoutePattern();
            filter.ParentId = -1;
            List<PigeonCms.Menu> list = menuMan.GetByFilter(filter, "");
            if (list.Count > 0)
            {
                res = list[0];
            }
            return res;
        }

        public static string GetCurrentAlias()
        {
            var context = HttpContext.Current;
            string pageName = "";
            if (context.Items["pagename"] != null)
            {
                pageName = context.Items["pagename"].ToString();
                pageName = pageName.Replace(".aspx", "");
            }
            if (string.IsNullOrEmpty(pageName))
            {
                pageName = context.Request.Url.Segments[
                    context.Request.Url.Segments.Length - 1];
                pageName = pageName.Replace(".aspx", "");
            }

            //used in basepage
            //if (string.IsNullOrEmpty(pageName))
            //    pageName = StaticPagesManager.DEFAULT_PAGE_NAME;

            return pageName;
        }

        public static string GetCurrentRoutePattern()
        {
            var context = HttpContext.Current;
            string res = "";
            if (context.Items["routeUrl"] != null)
            {
                res = context.Items["routeUrl"].ToString();
            }

            return res;
        }

        public static void LoadListMenu(ListBox listMenu, int currMenuId)
        {
            LoadListMenu(listMenu, "", currMenuId);
        }

        /// <summary>
        /// load listbox menu list
        /// </summary>
        /// <param name="listMenu">the listbox obj</param>
        /// <param name="menuType">menutype filter</param>
        /// <param name="currMenuId">current menu id (will be a disbled item in list)</param>
        public static void LoadListMenu(ListBox listMenu, string menuType, int currMenuId)
        {
            listMenu.Items.Clear();
            //ListItem listGroupItem = new ListItem("Menu", "");   //value.ToString()
            //ListMenu.Items.Add(listGroupItem);

            MenutypeFilter menutypeFilter = new MenutypeFilter();
            if (!string.IsNullOrEmpty(menuType))
                menutypeFilter.MenuType = menuType;
            List<Menutype> menuTypes = new MenutypesManager().GetByFilter(menutypeFilter, "");
            foreach (Menutype menuTypeItem in menuTypes)
            {
                //ListItem separatorListItem = new ListItem(" ", "");
                //listMenu.Items.Add(separatorListItem);

                ListItem menuTypeListItem = new ListItem();
                menuTypeListItem.Value = menuTypeItem.MenuType;
                menuTypeListItem.Text = menuTypeItem.MenuType;
                menuTypeListItem.Enabled = true;
                //menuTypeListItem.Attributes.Add("disabled", "disabled");
                listMenu.Items.Add(menuTypeListItem);

                loadMenuItems(listMenu, menuTypeItem.MenuType, 0, 0, currMenuId);
            }

        }

        private static void loadMenuItems(ListBox listMenu, string menuType, int parentId, int level, int currMenuId)
        {
            MenuFilter menuFilter = new MenuFilter();
            menuFilter.Published = Utility.TristateBool.NotSet;
            menuFilter.MenuType = menuType;
            menuFilter.ParentId = parentId;

            List<PigeonCms.Menu> recordList = 
                new MenuManager().GetTree(menuFilter, level);
            foreach (PigeonCms.Menu record1 in recordList)
            {
                ListItem menuItem = new ListItem();
                menuItem.Text = record1.Name;
                menuItem.Value = record1.Id.ToString();
                if (record1.Id == currMenuId)
                {
                    menuItem.Attributes.Add("disabled", "disabled");
                }
                listMenu.Items.Add(menuItem);
            }
        }
    }
}