using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PigeonCms;
using MyCompany;


public partial class pages_list : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        loadList();
    }

    protected void Rep1_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Header)
        {
            return;
        }

        var item = (Item)e.Item.DataItem;

        //*** Resources in "Public" folder should be likend through Virtual Folder in IIS enviroment
        var LitImg = (Literal)e.Item.FindControl("LitImg");
        if (item.Images.Count > 0)
            LitImg.Text = "<img src='" + item.DefaultImage.FileUrl + "' width='50' />";

        var LitPermissions = (Literal)e.Item.FindControl("LitPermissions");
        LitPermissions.Text = item.ReadAccessType.ToString();

    }

    /// <summary>
    /// load items in specified category example checking user context 
    /// </summary>
    private void loadList()
    {
        var man = new PigeonCms.ItemsManager<Item, ItemsFilter>(true, false);
        var filter = new PigeonCms.ItemsFilter();
        filter.CategoryId = MyCompany.Settings.SampleCatId;
        var list = man.GetByFilter(filter, "");

        Rep1.DataSource = list;
        Rep1.DataBind();
    }
}