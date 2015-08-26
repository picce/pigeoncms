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

        //add js script
        Utility.Script.RegisterClientScriptInclude(this, "jquery.cycle",
            ResolveUrl(Config.ModulesPath + this.BaseModule.ModuleFullName + "/views/" + this.BaseModule.CurrViewFolder + "/jquery.cycle.js"));

        ImagesListString = "";
        int counter = 0;
        foreach (FileMetaInfo item in this.ImagesList)
        {
            string itemClass = "";
            //if (counter == 0) itemClass = "active";
            itemClass = "jqueryCycleSlide";
            ImagesListString += "<img src='" + item.FileUrl + "' class='"+ itemClass +"' "
                + "alt='" + item.Title + "' title='" + item.Title + "' />";
            counter++;
        }
    }
}
