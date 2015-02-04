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
using PigeonCms;
using System.Collections.Generic;

public partial class Controls_Default : PigeonCms.CategoriesControl
{
    public string ListString = "";
    
    protected new void Page_Load(object sender, EventArgs e)
    {
        base.Page_Load(sender, e);

        ListString = "";
        foreach (Category item in this.CategoriesList)
        {
            string link = base.GetLinkAddress(item);
            
            string img = "";
            if (base.ShowImages)
            {
                string src = PhotoManager.GetPreviewSrc(item.DefaultImage.FileUrl, base.PreviewSize, base.CustomWidth);
                img += "<div class='categoryImage'>"
                + "<a href='" + link + "' class='" + base.BaseModule.CssClass + "'>"
                + "<img src='" + src + "' class='" + base.BaseModule.CssClass + "' /></a>"
                + "</div>";
            }

            ListString += "<li class='" + base.BaseModule.CssClass + "'>"
                + img
                + "<div class='categoryTitle'>"
                + "<a href='" + link + "' class='" + base.BaseModule.CssClass + "'>"
                + item.Title + "</a>"
                + "</div>";

            if (base.ShowDescription)
                ListString += "<div class='categoryDescription'>" + item.Description + "</div>";
                
            ListString += "</li>";
        }
    }
}
