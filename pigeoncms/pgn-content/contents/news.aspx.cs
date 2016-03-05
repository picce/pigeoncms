using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PigeonCms;
using Acme;

public partial class contents_news : Acme.BasePage
{
    protected List<Item> ListNews;

    protected void Page_Load(object sender, EventArgs e)
    {
        CurrentMasterPage.DataSection = "news";
		CurrentMasterPage.LinkFooter = "/contents/examples";
		CurrentMasterPage.TextLinkFooter = "examples";

        //get news list
        var newsMan = new ItemsManager<Item, ItemsFilter>(true, false);
        var filter = new ItemsFilter();
		filter.SectionId = Acme.Settings.NewsSectionId;
		filter.CategoryId = Acme.Settings.ArchiveCategoryId;
        filter.Enabled = PigeonCms.Utility.TristateBool.True;
        ListNews = newsMan.GetByFilter(filter, "");

    }
}