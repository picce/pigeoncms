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
using PigeonCms.Core.Helpers;

public partial class Controls_Placeholder : PigeonCms.BaseModuleControl
{

    private string name = "";
    public string Name
    {
        get { return GetStringParam("Name", name); }
        set { name = value; }
    }

    public string PageContent
    {
        get { return LitContent.Text; }
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        var obj1 = new PigeonCms.Placeholder();
        var cache = new CacheManager<PigeonCms.Placeholder>("PigeonCms.Placeholder");
        if (cache.IsEmpty(this.Name))
        {
            obj1 = new PlaceholdersManager().GetByName(this.Name);
            cache.Insert(this.Name, obj1);
        }
        else
        {
            obj1 = cache.GetValue(this.Name);
        }

        LitContent.Text = "";
        if (obj1.Visible)
        {
            LitContent.Text = obj1.Content;
        }
    }
}
