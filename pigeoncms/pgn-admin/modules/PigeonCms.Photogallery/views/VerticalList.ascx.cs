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

public partial class Controls_Default: PigeonCms.PhotogalleryControl
{
    public string ImagesListString = "";


    protected new void Page_Load(object sender, EventArgs e)
    {
        base.Page_Load(sender, e);

        ImagesListString = "";
        int counter = 0;
        foreach (FileMetaInfo item in this.ImagesList)
        {
            string previewUrl = VirtualPathUtility.ToAbsolute("~/Handlers/ImageHandler.ashx");
            previewUrl += "?size=m&imageUrl=" + item.FileUrl;

            ImagesListString += "<tr>"
                + "<td class='verticalListGalleryImgCol'>"
                + "<a rel='gallery' class='verticalListGallery' title='" + item.Title + "' href='" + item.FileUrl + "'"
                + Utility.AddTracking(item.FileName , this.StaticFilesTracking) + ">"
                + "<img class='verticalListGallery' src='" + previewUrl + "' alt='" + item.Title + "' title='" + item.Title + "' />"
                + "</a>"
                + "</td>"
                + "<td class='verticalListGalleryTitleCol'>"
                + "<span class='verticalListGalleryTitle'>" + item.Title + "</span><br />"
                + "<span class='verticalListGalleryDescription'>" + item.Description + "</span>"
                + "</td>"
                + "</tr>";
            counter++;
        }
    }
}
