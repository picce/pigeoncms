using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PigeonCms;
using Acme;

public partial class contents_elements : Acme.BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        CurrentMasterPage.DataSection = "elements";
        CurrentMasterPage.LinkFooter = "/contents/news";
        CurrentMasterPage.TextLinkFooter = "news";
    }
}