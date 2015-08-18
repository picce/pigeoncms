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

public partial class Controls_StaticPage: PigeonCms.BaseModuleControl
{
    private int cacheDuration = Config.DefaultCacheValue;
    public int CacheDuration
    {
        get { return cacheDuration; }
        set { cacheDuration = value; }
    }

    private string pageName = "";
    public string PageName
    {
        get { return GetStringParam("PageName", pageName); }
        set { pageName = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        var sp1 = new StaticPage();
        var cache = new CacheManager<StaticPage>("PigeonCms.StaticPage", this.CacheDuration);
        if (cache.IsEmpty(this.PageName))
        {
            if (!StaticPagesManager.ExistPage(this.PageName)) 
                this.PageName = StaticPagesManager.DEFAULT_PAGE_NAME;
            sp1 = new StaticPagesManager().GetStaticPageByName(this.PageName);
            cache.Insert(this.PageName, sp1);
        }
        else
        {
            sp1 = cache.GetValue(this.PageName);
        }

        if (sp1.ShowPageTitle) LitPageTitle.Text = sp1.PageTitle + "<br />";
        LitPageContent.Text = "";
        if (!sp1.IsPageContentTranslated)
        {
            //LitPageContent.Text += "<span class='textNote'>" + Resources.PublicLabels.LblTextNotTranslated + "</span><br />";
        }
        LitPageContent.Text += sp1.PageContentParsed;
    }
}
