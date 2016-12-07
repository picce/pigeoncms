using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using PigeonCms;
using System.Collections;

public partial class Controls_Default : PigeonCms.BaseModuleControl
{

    protected void Page_Load(object sender, EventArgs e)
    {
        setSuccess("");
        setError("");

        if (!Page.IsPostBack)
        {
            loadList();
        }
    }

    protected void RepPaging_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Header)
        {
            return;
        }

        int page = int.Parse(e.Item.DataItem.ToString());
        if (page - 1 == base.ListCurrentPage)
        {
            var BtnPage = (LinkButton)e.Item.FindControl("BtnPage");
            BtnPage.CssClass = "selected";
        }
    }

    protected void RepPaging_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "Page")
        {
            base.ListCurrentPage = int.Parse(e.CommandArgument.ToString()) - 1;
            loadList();
        }
    }

    protected void Rep1_ItemCommand(object sender, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "Select")
        {
            editRow(int.Parse(e.CommandArgument.ToString()));
        }
        if (e.CommandName == "DeleteRow")
        {
            deleteRow(int.Parse(e.CommandArgument.ToString()));
        }
    }

    protected void Rep1_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Header)
            return;

        var currItem = new Menutype();
        currItem = (Menutype)e.Item.DataItem;

    }

    protected void BtnSave_Click(object sender, EventArgs e)
    {
        setSuccess("");
        setError("");

        try
        {
            var o1 = new PigeonCms.Menutype();
            if (base.CurrentId == 0)
            {
                form2obj(o1);
                o1 = new MenutypesManager().Insert(o1);
            }
            else
            {
                o1 = new MenutypesManager().GetByKey(base.CurrentId);
                form2obj(o1);
                new MenutypesManager().Update(o1);
            }
            loadList();
            setSuccess(Utility.GetLabel("RECORD_SAVED_MSG"));
            showInsertPanel(false);
        }
        catch (Exception e1)
        {
            setError(Utility.GetLabel("RECORD_ERR_MSG") + "<br />" + e1.ToString());
        }
        finally
        {
        }
    }

    protected void BtnCancel_Click(object sender, EventArgs e)
    {
        showInsertPanel(false);
    }

    protected void BtnNew_Click(object sender, EventArgs e)
    {
        editRow(0);
    }


    #region private methods

    private void clearForm()
    {
        TxtMenuType.Text = "";
        TxtTitle.Text = "";
        TxtDescription.Text = "";
    }

    private void form2obj(Menutype obj)
    {
        obj.Id = base.CurrentId;
        obj.MenuType = TxtMenuType.Text;
        obj.Title = TxtTitle.Text;
        obj.Description = TxtDescription.Text;
    }

    private void obj2form(Menutype obj)
    {
        TxtMenuType.Text = obj.MenuType;
        TxtTitle.Text = obj.Title;
        TxtDescription.Text = obj.Description;
    }

    private void editRow(int recordId)
    {
        setSuccess("");
        setError("");

        clearForm();
        base.CurrentId = recordId;
        TxtMenuType.Enabled = true;

        if (base.CurrentId > 0)
        {
            TxtMenuType.Enabled = false;
            Menutype obj = new Menutype();
            obj = new MenutypesManager().GetByKey(base.CurrentId);
            obj2form(obj);
        }
        showInsertPanel(true);
    }

    private void deleteRow(int recordId)
    {
        setSuccess("");
        setError("");

        try
        {
            new MenutypesManager().DeleteById(recordId);
        }
        catch (Exception e)
        {
            setError(e.Message);
        }
        loadList();
    }

    private void loadList()
    {
        var man = new MenutypesManager();
        var filter = new MenutypeFilter();



        var list = man.GetByFilter(filter, "");
        var ds = new PagedDataSource();
        ds.DataSource = list;
        ds.AllowPaging = true;
        ds.PageSize = base.ListPageSize;
        ds.CurrentPageIndex = base.ListCurrentPage;

        RepPaging.Visible = false;
        if (ds.PageCount > 1)
        {
            RepPaging.Visible = true;
            ArrayList pages = new ArrayList();
            for (int i = 0; i <= ds.PageCount - 1; i++)
            {
                pages.Add((i + 1).ToString());
            }
            RepPaging.DataSource = pages;
            RepPaging.DataBind();
        }

        Rep1.DataSource = ds;
        Rep1.DataBind();
    }

    /// function for display insert panel
    /// <summary>
    /// </summary>
    private void showInsertPanel(bool toShow)
    {

        PigeonCms.Utility.Script.RegisterStartupScript(Upd1, "bodyBlocked", "bodyBlocked(" + toShow.ToString().ToLower() + ");");

        if (toShow)
            PanelInsert.Visible = true;
        else
            PanelInsert.Visible = false;
    }

    private void setError(string content)
    {
        LblErrInsert.Text = LblErrSee.Text = RenderError(content);
    }

    private void setSuccess(string content)
    {
        LblOkInsert.Text = LblOkSee.Text = RenderSuccess(content);
    }

    #endregion
}
