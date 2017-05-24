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
using System.Text;

public partial class Controls_ItemsList : PigeonCms.ItemsControl<Item, ItemsFilter>
{
    public StringBuilder ListString = new StringBuilder();


    protected void Page_Load(object sender, EventArgs e)
    {
        loadList();
    }

    private void loadList()
    {
        int counter = 0;

        foreach (var item in this.ItemsList)
        {
            string imgUrl = "";
            string link = "";
            string sFiles = "";
            string cssClass = base.BaseModule.CssClass;

            if (counter == this.ItemsList.Count-1)
                cssClass += " last";
            if (!string.IsNullOrEmpty(item.CssClass))
                cssClass += " " + item.CssClass;

            if (base.ShowImages)
            {
                if (!string.IsNullOrEmpty(item.DefaultImage.FileUrl))
                    imgUrl = PhotoManager.GetPreviewSrc(item.DefaultImage.FileUrl, base.PreviewSize, base.CustomWidth);
                else
                    imgUrl = Utility.GetThemedImageSrc("spacer.gif");
            }

            if (item.Files.Count > 0)
            {
                sFiles = "<ul class='" + base.BaseModule.CssClass + "'>";
                foreach (var file in item.Files)
                {
                    sFiles += "<li class='" + base.BaseModule.CssClass + "'>"
                    + "<a class='" + base.BaseModule.CssClass + "' href='" + file.FileUrl + "' "
                    + Utility.AddTracking(file.FileUrl, this.StaticFilesTracking) + " target='blank'>"
                    + "<span>" + file.FileName + "</span>"
                    + "</a>"
                    + "</li>";
                }
                sFiles += "</ul>";
            }

            link = base.GetLinkAddress(item);

            ListString.Append("<li class='" + cssClass + "'>");
            ListString.Append("<div class='itemImage'>");            
            if (this.ShowImages)
            {
                if (this.ItemTarget > 0)
                    ListString.Append("<a href='" + link + "' class='" + base.BaseModule.CssClass + "'>");

                ListString.Append("<img class='" + base.BaseModule.CssClass + "' src='" + imgUrl + "' alt='" + item.DefaultImage.Title + "' /><br />");

                if (this.ItemTarget > 0)
                    ListString.Append("</a>");
            }
            ListString.Append("</div>");

            ListString.Append("<div class='itemTitle'>");
            if (this.ItemTarget > 0)
            {
                ListString.Append("<a href='" + link + "' class='" + base.BaseModule.CssClass + "'>" + item.Title + "</a>");
            }
            else
            {
                ListString.Append(item.Title);
            }
            ListString.Append("</div>");
            ListString.Append("<div class='itemShortDesc'>" + base.GetShortDescription(item) + "</div>");
            ListString.Append("<div class='itemFiles'>" + sFiles + "</div>");
            ListString.Append("</li>");

            counter++;
        }
    }
}
