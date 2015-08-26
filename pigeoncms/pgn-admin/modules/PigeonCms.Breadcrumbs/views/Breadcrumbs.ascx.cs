using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Web.Caching;
using System.Collections.Generic;
using PigeonCms;

public partial class Controls_Breadcrumbs : PigeonCms.BaseModuleControl
{
    private string menuType = "";
    public string MenuType
    {
        get { return GetStringParam("MenuType", menuType); }
        set { menuType = value; }
    }

    protected string LblContent = "";
    
    protected void Page_Load(object sender, EventArgs e)
    {
        var menuMan = new MenuManager(true, false);
        var currentMenu = ((PigeonCms.BasePage)this.Page).MenuEntry;
        var menuList = new List<PigeonCms.Menu>();

        while (currentMenu.Id > 0)
        {
            menuList.Add(currentMenu);

            if (currentMenu.ParentId > 0)
                currentMenu = menuMan.GetByKey(currentMenu.ParentId);
            else
                currentMenu = new PigeonCms.Menu();
        }

        for (int i = 0; i < menuList.Count; i++)
        {
            string link = "<a href='" + menuList[i].Url + "'>" + menuList[i].Title + "</a>";
            string liClass = "";
            if (i == 0)
            {
                liClass = "last";
                link = menuList[i].Title;
            }
            else if (i == menuList.Count - 1)
                liClass = "first";

            LblContent = "<li class='"+ liClass +"'>" + link +"</li>" + LblContent;
        }
        LblContent = "<ul class='breadcrumbs " + this.BaseModule.CssClass + "'>" + LblContent + "</ul>";
    }
}
