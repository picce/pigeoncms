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
using System.Collections.Generic;
using PigeonCms;

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
            editRow(e.CommandArgument.ToString());
        }
        if (e.CommandName == "DeleteRow")
        {
            deleteRow(e.CommandArgument.ToString());
        }
    }

    protected void Rep1_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Header)
            return;

        var currItem = new WebConfigEntry();
        currItem = (WebConfigEntry)e.Item.DataItem;

        {
            //var LitEnabled = (Literal)e.Item.FindControl("LitEnabled");
            //string chkClass = "";
            //if (currItem.Enabled)
            //    chkClass = "checked";
            //LitEnabled.Text = "<span class='table-modern--checkbox--square " + chkClass + "'></span>";
        }
    }

    protected void BtnNew_Click(object sender, EventArgs e)
    {
        editRow("");
    }

    protected void BtnSave_Click(object sender, EventArgs e)
    {
        setSuccess("");
        setError("");

        try
        {
            var o1 = new WebConfigEntry();
            if (base.CurrentKey == "")
            {
                form2obj(o1);
                new WebConfigManager().Insert(o1);
            }
            else
            {
                o1 = new WebConfigManager().GetByKey(base.CurrentKey);  //precarico i campi esistenti e nn gestiti dal form
                form2obj(o1);
                new WebConfigManager().Update(o1);
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

    #region private methods
    private void clearForm()
    {
        TxtKey.Enabled = true;
        TxtKey.Text = "";
        TxtValue.Text = "";
    }

    private void form2obj(WebConfigEntry obj)
    {
        obj.Key = TxtKey.Text;
        obj.Value = TxtValue.Text;
    }

    private void obj2form(WebConfigEntry obj)
    {
        TxtKey.Enabled = string.IsNullOrEmpty(obj.Key);

        TxtKey.Text = obj.Key;
        TxtValue.Text = obj.Value;
    }

    private void editRow(string recordId)
    {
        setSuccess("");
        setError("");

        clearForm();
        base.CurrentKey = recordId;
        if (!string.IsNullOrEmpty(base.CurrentKey))
        {
            var obj = new WebConfigEntry();
            obj = new WebConfigManager().GetByKey(base.CurrentKey);
            obj2form(obj);
        }
        showInsertPanel(true);
    }

    private void deleteRow(string recordId)
    {
        setSuccess("");
        setError("");

        try
        {
            new WebConfigManager().Delete(recordId);
        }
        catch (Exception e)
        {
            setError(e.Message);
        }
        loadList();
    }

    private void loadList()
    {
        var man = new WebConfigManager();
        var filter = new WebConfigEntryFilter();


        var list = man.GetByFilter(filter);
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
