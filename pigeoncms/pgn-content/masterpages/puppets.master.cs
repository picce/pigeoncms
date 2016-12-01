using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PigeonCms;
using System.Web.Security;

public partial class puppets : System.Web.UI.MasterPage, Acme.IMaster
{
    protected PigeonCms.Engine.BasePage CurrPage;

    private string dataSection = "";
    public string DataSection
    {
        get { return dataSection; }
        set { this.dataSection = value; }
    }
    private string linkFooter = "";
    public string LinkFooter
    {
        get { return linkFooter; }
        set { this.linkFooter = value; }
    }

    private string textLinkFooter = "";
    public string TextLinkFooter
    {
        get { return textLinkFooter; }
        set { this.textLinkFooter = value; }
    }

    private bool isHomepage = false;
    public bool IsHomepage 
    {
        get { return isHomepage; }
        set { this.isHomepage = value; }
    }

    protected const string ClassHidden = "hidden";
    protected int Count = 1;
    protected List<PigeonCms.Menu> MenuList;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            //get menu list
            var menuMan = new MenuManager(true, false);
            var menuFilter = new MenuFilter();
            menuFilter.MenuType = "mainmenu";
            menuFilter.Published = Utility.TristateBool.True;
            menuFilter.Visible = Utility.TristateBool.True;
            MenuList = menuMan.GetByFilter(menuFilter, "");
        }
        catch (System.Data.SqlClient.SqlException ex)
        {
            //no connstring set, probably cms not configured. redirect to install page
            Response.Redirect(Config.InstallationPath);
        }
    }
}
