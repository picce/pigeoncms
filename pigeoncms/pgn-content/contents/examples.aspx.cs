using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PigeonCms;
using Acme;

public partial class contents_examples : Acme.BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        CurrentMasterPage.DataSection = "examples";
        CurrentMasterPage.LinkFooter = "/contents/reserved-area";
        CurrentMasterPage.TextLinkFooter = "reserved area";
    }
}