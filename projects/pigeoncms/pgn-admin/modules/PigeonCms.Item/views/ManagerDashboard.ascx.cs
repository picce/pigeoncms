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

public partial class Controls_PgnItem_ManagerDashboard : PigeonCms.ItemControl<Item, ItemsFilter>
{

    protected new void Page_Load(object sender, EventArgs e)
    {
        base.Page_Load(sender, e);
    }

}
