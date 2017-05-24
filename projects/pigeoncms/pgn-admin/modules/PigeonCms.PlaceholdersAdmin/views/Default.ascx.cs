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
using PigeonCms.Core.Helpers;

public partial class Controls_Default : PigeonCms.BaseModuleControl
{
    protected string Name
    {
        get { return base.GetStringParam("Name", "", "Name"); }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        setSuccess("");
        setError("");

        if (!Page.IsPostBack)
        {
            loadList();
        }

        if (this.BaseModule.DirectEditMode)
        {
            if (string.IsNullOrEmpty(this.Name))
                throw new ArgumentException();
            if (string.IsNullOrEmpty(
                new PlaceholdersManager().GetByName(this.Name).Name))
                throw new ArgumentException();

            BtnNew.Visible = false;
            BtnCancel.OnClientClick = "closePopup();";
            editRow(this.Name);
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

    protected void Rep1_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Header)
            return;

        var item = (PigeonCms.Placeholder)e.Item.DataItem;

        {
            var LitContent = (Literal)e.Item.FindControl("LitContent");
            LitContent.Text = Utility.Html.GetTextPreview(item.Content, 100, "");


            var LitEnabled = (Literal)e.Item.FindControl("LitEnabled");
            string enabledClass = "";
            if (item.Visible)
                enabledClass = "checked";
            LitEnabled.Text = "<span class='table-modern--checkbox--square " + enabledClass + "'></span>";
        }
    }


    protected void Rep1_ItemCommand(object source, RepeaterCommandEventArgs e)
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

    protected void BtnSave_Click(object sender, EventArgs e)
    {
        //rempoved 20130610 OnClientClick="MyObject.UpdateEditorFormValue();"
        setSuccess("");
        setError("");

        try
        {
            var o1 = new Placeholder();
            if (base.CurrentKey == "")
            {
                form2obj(o1);
                o1 = new PlaceholdersManager().Insert(o1);
            }
            else
            {
                o1 = new PlaceholdersManager().GetByName(base.CurrentKey);//precarico i campi esistenti e nn gestiti dal form
                form2obj(o1);
                new PlaceholdersManager().Update(o1);
            }
            new CacheManager<Placeholder>("PigeonCms.Placeholder").Remove(base.CurrentKey);

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
        editRow("");
    }


    #region private methods

    private void clearForm()
    {
        TxtName.Text = "";
        TxtContent.Text = "";
        ChkVisibile.Checked = true;
        TxtName.Enabled = true;
    }

    private void form2obj(Placeholder obj1)
    {
        obj1.Name = TxtName.Text;
        obj1.Visible = ChkVisibile.Checked;
        obj1.Content = TxtContent.Text;
    }

    private void obj2form(Placeholder obj1)
    {
        TxtName.Text = obj1.Name;
        ChkVisibile.Checked = obj1.Visible;
        TxtContent.Text = obj1.Content;
    }

    private void editRow(string recordId)
    {
        setSuccess("");
        setError("");

        clearForm();
        base.CurrentKey = recordId;

        if (base.CurrentKey != "")
        {
            TxtName.Enabled = false;
            var currObj = new Placeholder();
            currObj = new PlaceholdersManager().GetByName(base.CurrentKey);
            obj2form(currObj);
        }
        showInsertPanel(true);
    }

    private void deleteRow(string recordId)
    {
        setSuccess("");
        setError("");

        try
        {
            new PlaceholdersManager().DeleteById(recordId);
            new CacheManager<Placeholder>("PigeonCms.Placeholder").Remove(recordId);
        }
        catch (Exception e)
        {
            setError(e.Message);
        }
        loadList();
    }

    private void loadList()
    {
        var man = new PigeonCms.PlaceholdersManager();
        var filter = new PlaceholderFilter();

        if (!string.IsNullOrEmpty(this.Name))
            filter.Name = this.Name;

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
