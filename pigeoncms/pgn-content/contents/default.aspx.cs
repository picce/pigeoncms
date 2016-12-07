using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PigeonCms;

public partial class _default : Acme.BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        CurrentMasterPage.DataSection = "homepage";
        CurrentMasterPage.IsHomepage = true;
    }
}