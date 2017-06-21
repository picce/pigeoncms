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

public partial class Controls_Default: PigeonCms.ItemControl<Item, ItemsFilter>
{
    public StringBuilder LitDefaultImage = new StringBuilder();
    public StringBuilder LitImages = new StringBuilder();
    public StringBuilder LitFiles = new StringBuilder();
    public StringBuilder LitPages = new StringBuilder();
    public StringBuilder PathString = new StringBuilder();
    public string LitDescriptionPage = "";

    protected new void Page_Load(object sender, EventArgs e)
    {
        base.Page_Load(sender, e);
        Item currItem = this.CurrItem;

        if (currItem.CategoryId > 0)
        {
            PathString.Append(base.CurrItem.Category.Section.Title + " > " + base.CurrItem.Category.Title);
            PathString.Append(" > " + currItem.Title);
        }

        //description page
        LitDescriptionPage = currItem.DescriptionPages[this.CurrentPage - 1];

        //images
        if (!string.IsNullOrEmpty(currItem.DefaultImage.FileName))
        {
            LitDefaultImage.Append("<a class='" + BaseModule.CssClass + "' rel='item' "
                + "href='" + currItem.DefaultImage.FileUrl + "' "
                + Utility.AddTracking(currItem.DefaultImage.FileName , this.StaticFilesTracking) + " "
                + "title='" + currItem.DefaultImage.Title + "' rel='product'>"
            + "<img id='bigPicture' "
            + "src='" + PhotoManager.GetPreviewSrc(currItem.DefaultImage.FileUrl, base.PreviewSize, base.CustomWidth) + "' "
            + "alt='" + currItem.DefaultImage.Title + "' class='" + BaseModule.CssClass + "' /></a>");
        }
        int counter = 0;
        foreach (FileMetaInfo img in currItem.Images)//currItem.ImagesNotDefault
        {
            LitImages.Append("<li class='" + BaseModule.CssClass + "'>"
                + "<a class='" + BaseModule.CssClass + "' href='" + img.FileUrl + "' title='" + img.Title + "' "
                + Utility.AddTracking(img.FileName, this.StaticFilesTracking) + " rel='product'>");
            LitImages.Append("<img id='bigPicture" + counter.ToString() + "' "
                + "src='" + PhotoManager.GetPreviewSrc(img.FileUrl, base.PreviewSize, base.CustomWidth) + "' "
                + "alt='" + img.Title + "' class='" + BaseModule.CssClass + "' />");
            LitImages.Append("</a></li>");
            counter++;
        }

        //files
        foreach (FileMetaInfo file in currItem.Files)
        {
            LitFiles.Append("<li class='" + base.BaseModule.CssClass + "'>"
            + "<a class='" + base.BaseModule.CssClass + "' href='" + file.FileUrl + "' "
            + Utility.AddTracking(file.FileName , this.StaticFilesTracking) + " target='blank'>"
            + "<span>" + file.FileName + "</span>"
            + "</a>"
            + "</li>");
        }

        //pages bar
        if (currItem.DescriptionPages.Count > 1)
        {
            LitPages.Append("<li class='" + base.BaseModule.CssClass + " pageslabel'><span>");
            LitPages.Append(base.GetLabel("pages", "Pages"));
            LitPages.Append("</span></li>");

            if (this.CurrentPage > 1)
            {
                addPageLink(this.CurrentPage - 1, this.CurrentPage, base.GetLabel("previous", "<<"), LitPages, "pagePrevious");
            }
            for (int i = 0; i < currItem.DescriptionPages.Count; i++)
            {
                addPageLink(i + 1, this.CurrentPage, "", LitPages, "");
            }
            if (this.CurrentPage < currItem.DescriptionPages.Count)
            {
                addPageLink(this.CurrentPage + 1, this.CurrentPage, base.GetLabel("next", ">>"), LitPages, "pageNext");
            }
        }
    }

    private void addPageLink(int pageNumber, int pageCurrent, string text, StringBuilder lit, string cssClass)
    {
        if (string.IsNullOrEmpty(text))
            text = pageNumber.ToString();

        lit.Append("<li class='");
        lit.Append(base.BaseModule.CssClass);
        if (pageNumber == pageCurrent)
            lit.Append(" selected");
        if (!string.IsNullOrEmpty(cssClass))
            lit.Append(" " + cssClass);
        lit.Append("'>");
        if (pageNumber != this.CurrentPage)
        {
            //link
            lit.Append("<a class='");
            lit.Append(base.BaseModule.CssClass);
            if (pageNumber == this.CurrentPage)
                lit.Append(" selected");
            if (!string.IsNullOrEmpty(cssClass))
                lit.Append(" " + cssClass);
            lit.Append("' href='");
            lit.Append(HttpContext.Current.Request.Path + "?page=" + (pageNumber).ToString() + "'>");
        }
        lit.Append("<span>" + text + "</span>");
        if (pageNumber != pageCurrent)
        {
            lit.Append("</a>");
        }
        lit.Append("</li>");
    }
}
