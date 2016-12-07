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

public partial class RedirectItem: PigeonCms.ItemControl<Item, ItemsFilter>
{
    string sErr = "";
   

    protected new void Page_Load(object sender, EventArgs e)
    {
        base.Page_Load(sender, e);
        Item currItem = this.CurrItem;
        int redirectTo = 0;
        
        try
        {
            if (currItem.Params.ContainsKey("RedirectTo"))
                int.TryParse(currItem.Params["RedirectTo"], out redirectTo);
            if (redirectTo > 0)
            {
                string url = "";
                var refMnu = new PigeonCms.Menu();
                refMnu = new MenuManager(true, false).GetByKey(redirectTo);
                url = Utility.GetRoutedUrl(refMnu, "", Config.AddPageSuffix);
                HttpContext.Current.Response.Redirect(url, true);
            }
        }
        catch (Exception ex)
        {
            sErr = ex.ToString();
        }
    }
}
