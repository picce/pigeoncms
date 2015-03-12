using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PigeonCms;
using PigeonCms.Core.Helpers;


public partial class _cache : BasePage
{
    protected string LitRows = "";
    protected string CACHE_KEY = "";
    protected CacheManager<List<Item>> CachedList;


    protected void Page_Load(object sender, EventArgs e)
    {
        CACHE_KEY = "Rows_Items";
        CachedList = new CacheManager<List<Item>>("AQ.CacheSample");
        List<Item> list;

        if (!Page.IsPostBack)
        {
            
        }

        if (CachedList.IsEmpty(CACHE_KEY))
        {
            LitRes.Text = "data from db";
            list = getList();
            CachedList.Insert(CACHE_KEY, list);
        }
        else
        {
            LitRes.Text = "data from cache";
            list = CachedList.GetValue(CACHE_KEY);
        }
        LitRows = renderRows(list);
    }

    protected void BtnClearCache_Click(object sender, EventArgs e)
    {
        try
        {
            CachedList.Remove(CACHE_KEY);
            LitRes.Text = "Cache cleared";
        }
        catch (Exception e1) { LitRes.Text = e1.Message; }
    }

    private List<Item> getList()
    {
        var man = new PigeonCms.ItemsManager<Item, ItemsFilter>(false, false);
        var filter = new PigeonCms.ItemsFilter();
        filter.CategoryId = MyCompany.Settings.SampleCatId;
        var list = man.GetByFilter(filter, "");

        return list;
    }

    private string renderRows(List<Item> list)
    {
        string res = "";

        const string ROW = @"
        <tr>
            <td>[[Id]]</td>
            <td>[[Title]]</td>
        </tr>";

        foreach (var item in list)
        {
            res += ROW
                .Replace("[[Id]]", item.Id.ToString())
                .Replace("[[Title]]", item.Title);
        }

        return res;
    }
}