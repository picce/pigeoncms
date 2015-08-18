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
    public string FirstImageSrc = "";
    public string FirstImageTitle = "";

    protected new void Page_Init(object sender, EventArgs e)
    {
        base.Page_Init(sender, e);
        buildChildsList();
    }

    protected new void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            base.Page_Load(sender, e);
            
            //add js script
            Utility.Script.RegisterClientScriptInclude(this, "jquery.interface",
                ResolveUrl(Config.ModulesPath + this.BaseModule.ModuleFullName + "/views/" + this.BaseModule.CurrViewFolder + "/interface.js"));

            if (!ScriptManager1.IsInAsyncPostBack)
            {
                buildImagesList();
                //buildChildsList();
            }
        }
    }

    private void buildImagesList()
    {
        ImagesListString = "";
        int counter = 0;
        foreach (FileMetaInfo item in this.ImagesList)
        {
            string itemClass = "";
            if (counter == 0)
            {
                itemClass = "active";
                FirstImageSrc = item.FileUrl;
                FirstImageTitle = item.Title;
            }
            ImagesListString += "<a class='dock-item " + itemClass + "' "
            + "href='" + item.FileUrl + "'" 
            + Utility.AddTracking(item.FileName , this.StaticFilesTracking) + ">"
            + "<span>" + item.Title + "</span>"
            + "<img src='" + item.FileUrl + "' alt='" + item.Title + "' />"
            + "</a>";
            counter++;
        }
        if (counter < 2)
            ImagesListString = "";
    }

    private void buildChildsList()
    {
        //clear panel
        while (PanelChildsList.Controls.Count > 0)
        {
            PanelChildsList.Controls.RemoveAt(0);
        }

        foreach (PhotogalleryControl.LinksList item in base.ChildList)
        {   
            LinkButton linkChild = new LinkButton();
            linkChild.ID = "LinkChild" + item.Id.ToString();
            linkChild.Text = item.Title;
            linkChild.CommandName = "changeChild";
            linkChild.CommandArgument = item.Id.ToString();
            linkChild.Command += new CommandEventHandler(link_Command);
            PanelChildsList.Controls.Add(linkChild);

            Literal litSeparator = new Literal();
            litSeparator.Text = "&nbsp;&nbsp;&nbsp;";
            PanelChildsList.Controls.Add(litSeparator);
        }
    }

    void link_Command(object sender, CommandEventArgs e)
    {
        if (e.CommandName == "changeChild")
        {
            int arg = 0;
            int.TryParse((string)e.CommandArgument, out arg);
            base.CategoryId = arg;
            buildImagesList();
        }
    }
}