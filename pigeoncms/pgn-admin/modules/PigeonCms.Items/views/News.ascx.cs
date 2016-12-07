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
                    imgUrl = PhotoManager.GetPreviewSrc(item.DefaultImage.FileUrl, base.PreviewSize, base.CustomWidth);
                else
                    imgUrl = Utility.GetThemedImageSrc("spacer.gif");
            }

            link = base.GetLinkAddress(item);

            ListString.Append("<li class='" + cssClass + "'>");
            ListString.Append("<div class='itemDate'>" + itemDate + "</div>");
            ListString.Append("<div class='itemTitle'>" + item.Title + "</div>");
            ListString.Append("<div class='itemShortDesc'>" + base.GetShortDescription(item) + "</div>");
            if (base.ShortDescLen != 0 && item.Description.Length > base.ShortDescLen)
            {
                ListString.Append("<div class='itemDivMore'>"
                    + "<a class='itemMore' href='#item-" + item.Id.ToString() + "'><span>" + base.GetLabel("LblMoreInfo", "more info") + "</span></a>"
                    + "</div>");
            }
            else
            {
                ListString.Append("<div class='itemFiles'>" + getFilesList(item) + "</div>");
            }


            ListString.Append("<div class='itemImage'><a href='" + link + "' class='" + base.BaseModule.CssClass + "'>");
            if (this.ShowImages)
            {
                ListString.Append("<img class='" + base.BaseModule.CssClass + "' src='" + imgUrl + "' alt='" + item.DefaultImage.Title + "' /><br />");
            }
            ListString.Append("</a></div>");

            ListString.Append("</li>");

            counter++;
        }

        DetailsString.Append("<div style='display:none'>");
        foreach (var item in this.ItemsList)
        {
            if (base.ShortDescLen != 0 && item.Description.Length > base.ShortDescLen)
            {
                //hide details only if linked
                string itemDate = "";
                if (item.ItemDate > DateTime.MinValue)
                    itemDate = Utility.GetMonthName(item.ItemDate.Month) + " " + item.ItemDate.Year.ToString();

                DetailsString.Append("<div id='item-" + item.Id.ToString() + "'>");
                DetailsString.Append("<div class='itemPrint'>"
                    + "<a href='javascript:void(0);' onclick=\"printIt($('#item-" + item.Id.ToString() + "')[0].innerHTML);\">"
                    + base.GetLabel("LblPrint", "Print")
                    + "</a></div>");
                DetailsString.Append("<div class='itemDate'>" + itemDate + "</div>");
                DetailsString.Append("<div class='itemTitle'>" + item.Title + "</div>");
                DetailsString.Append("<div class='itemLongDesc'>" + item.DescriptionParsed + "</div>");
                DetailsString.Append("<div class='itemFiles'>" + getFilesList(item) + "</div>");
                DetailsString.Append("</div>");
            }
        }
        DetailsString.Append("</div>");
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
