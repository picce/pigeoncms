using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class puppets : System.Web.UI.MasterPage
{
    protected BasePage CurrPage;

    protected void Page_Load(object sender, EventArgs e)
    {
        CurrPage = (BasePage)this.Page;
    }
}
