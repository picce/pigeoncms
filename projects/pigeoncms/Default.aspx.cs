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
using PigeonCms;

public partial class _Default : BasePage
{
	protected void Page_Init(object sender, EventArgs e)
    {
        PigeonCms.ModuleHelper.LoadModules(base.MenuEntry, this.Page);
    }
}