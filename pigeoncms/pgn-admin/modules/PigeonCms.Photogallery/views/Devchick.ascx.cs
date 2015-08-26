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

    protected int PageNumber
    {
        get
        {
            int res = 0;
            if (ViewState["PageNumber"] != null)
                res = (int)ViewState["PageNumber"];
            return res;
        }
        set 
        {   
            int pageNumber = 0;
            if (value > 0)
                pageNumber = value;
            ViewState["PageNumber"] = pageNumber; 
        }
    }

    protected int PreviewPerPage
    { 
        get { return 18; }
    }

    int numberOfPages = -1;
    protected int NumberOfPages
    {
        get 
        {
            if (numberOfPages == -1)
            {
                int res = 1;
                if (this.ImagesList.Count > 0)
                {
                    res = this.ImagesList.Count / this.PreviewPerPage;
                    if (this.ImagesList.Count % this.PreviewPerPage > 0)
                        res++;
                }
                numberOfPages = res;
            }
            return numberOfPages;
        }
    }


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
            Utility.Script.RegisterClientScriptInclude(this, "jquery.galleria", ResolveUrl(Config.ModulesPath + this.BaseModule.ModuleFullName + "/views/" + this.BaseModule.CurrViewFolder + "/jquery.galleria.js"));

            if (!ScriptManager1.IsInAsyncPostBack)
            {
                buildImagesList();
                buildChildsList();
            }
        }
    }

    private void buildImagesList()
    {
        ImagesListString = "";
        int counter = 0;
        int index = 0;

        index = this.PageNumber * this.PreviewPerPage;
        while(index < this.ImagesList.Count && counter < this.PreviewPerPage)
        {
            var item = this.ImagesList[index];
            string itemClass = "";
            if (counter == 0) itemClass = "active";
            ImagesListString += "<li class='" + itemClass + "'>"
                + "<img src='" + item.FileUrl + "'"
                + Utility.AddTracking(item.FileName, this.StaticFilesTracking)
                + "alt='" + item.Title + "' title='" + item.Title + "' />"
                + "</li>";

            counter++;
            index++;
        }
    }

    private void buildChildsList()
    {
        //clear panel
        while (PanelChildsList.Controls.Count > 0)
        {
            PanelChildsList.Controls.RemoveAt(0);
        }
        bool showLink = false;
        if (this.NumberOfPages > 1)
        {
            //prev link
            showLink = false;
            if (this.PageNumber > 0)
                showLink = true;
            addLink(this.PageNumber - 1, "LinkChildPrev", "<<", showLink, "prev");

            for (int i = 0; i < this.NumberOfPages; i++)
            {
                showLink = true;
                addLink(i, "LinkChild" + i.ToString(), (i + 1).ToString(), showLink, "changeChild");
            }

            //next link
            showLink = false;
            if (this.PageNumber < this.NumberOfPages-1)
                showLink = true;
            addLink(this.PageNumber + 1, "LinkChildNext", ">>", showLink, "next");
            
        }
    }

    private void addLink(int argument, string linkId, string linkText, bool show, string commandName)
    {
        string cssClass = "";
        if (argument == this.PageNumber)
            cssClass = "selected";

        LinkButton linkChild = new LinkButton();
        linkChild.ID = linkId;
        linkChild.Text = linkText;
        linkChild.CommandName = commandName;
        linkChild.CommandArgument = argument.ToString();
        linkChild.Command += new CommandEventHandler(link_Command);
        linkChild.CssClass = cssClass;
        linkChild.Visible = show;
        PanelChildsList.Controls.Add(linkChild);

        Literal litSeparator = new Literal();
        litSeparator.Text = "";//not used, use css class instead
        PanelChildsList.Controls.Add(litSeparator);
    }

    void link_Command(object sender, CommandEventArgs e)
    {
        switch (e.CommandName)
        {
            case "changeChild":
                int arg = 0;
                int.TryParse((string)e.CommandArgument, out arg);
                this.PageNumber = arg;
                break;
            case "prev":
                this.PageNumber--;
                break;
            case "next":
                this.PageNumber++;
                break;
        }
        buildImagesList();
        buildChildsList();
    }
}
