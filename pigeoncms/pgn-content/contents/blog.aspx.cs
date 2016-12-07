using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PigeonCms;
using Acme;

public partial class pgn_content_contents_blog : Acme.BasePage
{
    protected List<Item> ListBlog;

    protected void Page_Load(object sender, EventArgs e)
    {
        CurrentMasterPage.DataSection = "blog";
        CurrentMasterPage.LinkFooter = "/contents/examples";
        CurrentMasterPage.TextLinkFooter = "examples";

        //get blog list
        var man = new ItemsManager<Item, ItemsFilter>(true, false);
        var filter = new ItemsFilter();
        filter.SectionId = SiteSettings.ContentsSectionId;
        filter.CategoryId = SiteSettings.BlogCatId;
        filter.Enabled = PigeonCms.Utility.TristateBool.True;
        ListBlog = man.GetByFilter(filter, "");
    }
}