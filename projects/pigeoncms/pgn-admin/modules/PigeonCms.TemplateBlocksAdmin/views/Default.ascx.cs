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
			loadDropEnabledFilter();
			loadList();
		}
    }

    protected void DropEnabledFilter_SelectedIndexChanged(object sender, EventArgs e)
    {
		loadList();
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
            edit(e.CommandArgument.ToString());
        }
        if (e.CommandName == "DeleteRow")
        {
            delete(e.CommandArgument.ToString());
        }
    }

	protected void Rep1_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
		if (e.Item.ItemType == ListItemType.Header)
			return;

		var currItem = new TemplateBlock();
		currItem = (TemplateBlock)e.Item.DataItem;

		{
			var LitEnabled = (Literal)e.Item.FindControl("LitEnabled");
			string chkClass = "";
			if (currItem.Enabled)
				chkClass = "checked";
			LitEnabled.Text = "<span class='table-modern--checkbox--square " + chkClass + "'></span>";
		}
    }

    protected void BtnSave_Click(object sender, EventArgs e)
    {
		setSuccess("");
		setError("");

        try
        {
            var p1 = new TemplateBlock();
            if (this.CurrentKey == string.Empty)
            {
                form2obj(p1);
                p1 = new TemplateBlocksManager().Insert(p1);
            }
            else
            {
                p1 = new TemplateBlocksManager().GetByKey(TxtName.Text);//precarico i campi esistenti e nn gestiti dal form
                form2obj(p1);
                new TemplateBlocksManager().Update(p1);
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
        edit("");
    }


    #region private methods

    private void form2obj(TemplateBlock obj1)
    {
        obj1.Name = TxtName.Text;
        obj1.Title = TxtTitle.Text;
        obj1.Enabled = ChkEnabled.Checked;
    }

    private void obj2form(TemplateBlock obj1)
    {
        TxtName.Text = obj1.Name;
        TxtTitle.Text = obj1.Title;
        ChkEnabled.Checked = obj1.Enabled;
    }

    private void edit(string recordId)
    {
		setSuccess("");
		setError("");

		base.CurrentKey = recordId;

		TxtName.Text = recordId;
        TxtName.Enabled = true;
        TxtTitle.Text = "";
        ChkEnabled.Checked = true;
        
		if (recordId != "")
        {
            TxtName.Enabled = false;
            var currObj = new TemplateBlock();
			currObj = new TemplateBlocksManager().GetByKey(recordId);
            obj2form(currObj);
        }
		showInsertPanel(true);
    }

    private void delete(string name)
    {
		setSuccess("");
		setError("");

        try
        {
            new TemplateBlocksManager().DeleteById(name);
        }
        catch (Exception e)
        {
            setError(e.Message);
        }
		loadList();
    }

	private void loadList()
	{
		var man = new TemplateBlocksManager();
		var filter = new TemplateBlockFilter();

		filter.Enabled = Utility.TristateBool.NotSet;
		switch (DropEnabledFilter.SelectedValue)
		{
			case "1":
				filter.Enabled = Utility.TristateBool.True;
				break;
			case "0":
				filter.Enabled = Utility.TristateBool.False;
				break;
			default:
				filter.Enabled = Utility.TristateBool.NotSet;
				break;
		}

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

	private void loadDropEnabledFilter()
	{
		DropEnabledFilter.Items.Clear();
		DropEnabledFilter.Items.Add(new ListItem(Utility.GetLabel("LblSelectState", "Select state"), ""));
		DropEnabledFilter.Items.Add(new ListItem("On-line", "1"));
		DropEnabledFilter.Items.Add(new ListItem("Off-line", "0"));
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
