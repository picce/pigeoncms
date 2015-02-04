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

public partial class Controls_ItemsSearch : PigeonCms.BaseModuleControl
{
    private int minSearchChars = 0;
    public int MinSearchChars
    {
        get { return GetIntParam("MinSearchChars", minSearchChars); }
        set { minSearchChars = value; }
    }

    private int itemsTarget = 0;
    public int ItemsTarget
    {
        get { return GetIntParam("ItemsTarget", itemsTarget); }
        set { itemsTarget = value; }
    }

    private string headerText = "";
    public string HeaderText
    {
        get { return GetStringParam("HeaderText", headerText); }
        set { headerText = value; }
    }

    private string footerText = "";
    public string FooterText
    {
        get { return GetStringParam("FooterText", footerText); }
        set { footerText = value; }
    }

    private string search = "";
    public string Search
    {
        get { return GetStringParam("Search", search, "search"); }
        set { search = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        //WaterSearch.WatermarkText = base.GetLabel("LblSearchLink", "search");
        if (!Page.IsPostBack)
        {
            TxtSearch.Text = this.Search;
        }
    }

    protected void TxtSearch_TextChanged(object sender, EventArgs e)
    {
        doSearch();
    }


    protected void BtnSearch_Click(object sender, EventArgs e)
    {
        doSearch();
    }

    private void doSearch()
    {
        string url = "";
        bool allow = true;
        if (TxtSearch.Text.Length < this.MinSearchChars)
            allow = false;

        if (allow)
        {
            PigeonCms.Menu menuTarget = null;
            if (this.ItemsTarget > 0)
            {
                if (menuTarget == null)
                {
                    menuTarget = new MenuManager().GetByKey(this.ItemsTarget);
                }

                try
                {
                    url = Utility.GetRoutedUrl(menuTarget, "search=" + TxtSearch.Text, Config.AddPageSuffix);
                    //if (menuTarget.RoutePattern.Contains("{itemname}"))
                    //    res = Utility.GetRoutedUrl(
                    //    menuTarget, new RouteValueDictionary { { "itemname", item.Title } }, "", true);
                    Response.Redirect(url);
                }
                catch (Exception ex)
                {
                    Tracer.Log("GetLinkAddress(): " + ex.ToString(), TracerItemType.Error);
                }
            }
        }
    }


}
