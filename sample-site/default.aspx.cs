using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PigeonCms;

public partial class _default : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        LitSample5.Text = GetLabel("AQ_default", "Sample5", "Label value calling method in code behind");

    }

    private string getStoreData(int storeId)
    {
        string res = "";
        var man = new PigeonCms.ItemsManager<PigeonCms.Item, PigeonCms.ItemsFilter>();
        var item = man.GetByKey(storeId);

        res = item.Title + "<br>"
            + item.Description + "<br>"
            + "Brand: " + item.Params["Brand"] + "<br>"
            + "Store type: " + item.Params["StoreType"] + "<br>"
            + "Lat: " + item.Params["Latitude"] + "<br>"
            + "Lng: " + item.Params["Longitude"] + "<br>";

        return res;
    }
}