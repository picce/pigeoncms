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

public partial class Controls_Default: PigeonCms.ItemsControl<NewsItem, NewsItemFilter>
{
    public StringBuilder ListString = new StringBuilder();
    public StringBuilder DetailsString = new StringBuilder();


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
            string itemDate = "";
            string cssClass = base.BaseModule.CssClass;

            if (counter == this.ItemsList.Count - 1)
            {
                cssClass += " last";
            }

            if (item.ItemDate > DateTime.MinValue)
                itemDate = Utility.GetMonthName(item.ItemDate.Month) + " " + item.ItemDate.Year.ToString();

            if (base.ShowImages)
            {
                if (!string.IsNullOrEmpty(item.DefaultImage.FileUrl))
                {
                    imgUrl = VirtualPathUtility.ToAbsolute("~/Handlers/ImageHandler.ashx") + "?imageUrl=" + item.DefaultImage.FileUrl;
                    imgUrl += "&size=m";
                }
                else
                {
                    imgUrl = Utility.GetThemedImageSrc("spacer.gif");
                }
            }

            link = base.GetLinkAddress(item);

            ListString.Append("<li class='" + cssClass + "'>");
            ListString.Append("<div class='itemDate'>" + itemDate + "</div>");
            ListString.Append("<div class='itemTitle'><a href='javascript:void(0)' "
                + " onclick=\"collapse('item-"+ item.Id.ToString() +"'); \">"
                + item.Title 
                + "</a></div>");
            ListString.Append("<div class='itemLongDesc' id='item-" + item.Id.ToString() + "' style='display:none'>" 
                + item.DescriptionParsed 
                + "</div>");

            ListString.Append("<div class='itemFiles'>" + getFilesList(item) + "</div>");
            ListString.Append("<a href='" + link + "' class='" + base.BaseModule.CssClass + "'>");
            if (this.ShowImages)
            {
                ListString.Append("<img class='" + base.BaseModule.CssClass + "' src='" + imgUrl + "' alt='" + item.DefaultImage.Title + "' /><br />");
            }
            ListString.Append("</a>");

            ListString.Append("</li>");

            counter++;
        }
    }

    private string getFilesList(Item item)
    {
        string res = "";
        if (base.ShowFiles)
        {
            res = "<ul class='" + base.BaseModule.CssClass + "'>";
            foreach (var file in item.Files)
            {
                //var cat = new Category();
                //var sect = new Section();
                //if (item.CategoryId > 0)
                //{
                //    cat = new CategoriesManager().GetByKey(item.CategoryId);
                //    sect = new SectionsManager().GetByKey(cat.SectionId);
                //}

                res += "<li class='" + base.BaseModule.CssClass + "'>"
                + "<a class='" + base.BaseModule.CssClass + "' href='" + file.FileUrl + "' "
                + Utility.AddTracking(file.FileName, this.StaticFilesTracking) + "target='blank'>"
                + "<span>" + file.FileName + "</span>"
                + "</a>"
                + "</li>";
            }
            res += "</ul>";
        }
        return res;
    }
}
