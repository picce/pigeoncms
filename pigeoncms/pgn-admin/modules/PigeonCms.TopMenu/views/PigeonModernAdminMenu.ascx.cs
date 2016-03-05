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

public partial class Modules_PigeonModernAdminMenu : PigeonCms.TopMenuControl
{
    protected string LblContent = "";


    protected void Page_Load(object sender, EventArgs e)
    {
        var style = new MenuTemplate.MenuAttributes();
        style.MenuCssClass = "nav";
        style.MenuId = "side-menu";
        style.ItemSelectedClass = "active";

        Template = new MenuTemplate(style);
        //Template.ItemHeader = "<li class='[[ItemCssClass]]'>"
        //    + "<a href='[[ItemHref]]' onclick='[[ItemOnClick]]' class='{{ItemCssClass}} [[ItemCssClass]]' "
        //    + "style='{{ItemStyle}} [[ItemStyle]]'>"
        //    + "<i class='[[ItemCssClass]]'></i>"
        //    + "[[ItemContent]]"
        //    + "</a>";
        Template.ItemHeader = "<li class='[[ItemSelectedClass]]'>"
            + "<a href='[[ItemHref]]' class='[[ItemSelectedClass]]' onclick='[[ItemOnClick]]' "
            + "style='{{ItemStyle}} [[ItemStyle]]'>"
            + "<i class='[[ItemCssClass]]'></i>"
            + " [[ItemContent]]"
            + "</a>";
        Template.ReBuild(style);

        //base.Page_Load(sender, e);
        LblContent = base.GetContent();
    }
}
