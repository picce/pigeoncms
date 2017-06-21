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
		else
		{
			string eventArg = HttpContext.Current.Request["__EVENTARGUMENT"];
			if (eventArg == "sortcomplete")
			{
				updateSortedTable();
				loadList();
			}
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

	protected void Rep1_ItemDataBound(object sender, RepeaterItemEventArgs e)
	{
		if (e.Item.ItemType == ListItemType.Header)
			return;

		var item = (PigeonCms.Culture)e.Item.DataItem;

		{
			var LitEnabled = (Literal)e.Item.FindControl("LitEnabled");
			string enabledClass = "";
			if (item.Enabled)
				enabledClass = "checked";
			LitEnabled.Text = "<span class='table-modern--checkbox--square " + enabledClass + "'></span>";

			var LnkEnabled = (LinkButton)e.Item.FindControl("LnkPublished");
			LnkEnabled.CssClass = "table-modern--checkbox " + enabledClass;
			LnkEnabled.CommandName = item.Enabled ? "Enabled0" : "Enabled1";
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
		//Enabled
		if (e.CommandName == "Enabled0")
		{
			setFlag(e.CommandArgument.ToString(), false, "enabled");
			loadList();
		}
		if (e.CommandName == "Enabled1")
		{
			setFlag(e.CommandArgument.ToString(), true, "enabled");
			loadList();
		}
    }

    protected void BtnApply_Click(object sender, EventArgs e)
    {
		setSuccess("");
		setError("");

        try
        {
            new CulturesManager().RefreshCultureList();
        }
        catch (Exception ex)
        {
            setError("Error updating culture list:" + ex.ToString());
            PigeonCms.Trace.Warn("Error", "Error updating culture list", ex);
        }
        finally
        {
            setSuccess("Culture list updated sucefully");
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
            Culture o1 = new Culture();
            if (base.CurrentKey == "")
            {
                form2obj(o1);
                o1 = new CulturesManager().Insert(o1);
            }
            else
            {
                o1 = new CulturesManager().GetByKey(base.CurrentKey);  //precarico i campi esistenti e nn gestiti dal form
                form2obj(o1);
                new CulturesManager().Update(o1);
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
        TxtCultureCode.Enabled = true;
        TxtCultureCode.Text = "";
		TxtShortCode.Text = "";
        TxtDisplayName.Text = "";
        ChkEnabled.Checked = true;
    }

    private void form2obj(Culture obj)
    {
        obj.CultureCode = TxtCultureCode.Text;
        obj.DisplayName = TxtDisplayName.Text;
		obj.ShortCode = TxtShortCode.Text;
        obj.Enabled = ChkEnabled.Checked;
    }

    private void obj2form(Culture obj)
    {
        TxtCultureCode.Enabled = string.IsNullOrEmpty(obj.CultureCode);

        TxtCultureCode.Text = obj.CultureCode;
        TxtDisplayName.Text = obj.DisplayName;
		TxtShortCode.Text = obj.ShortCode;
        ChkEnabled.Checked = obj.Enabled;
    }

    private void editRow(string recordId)
    {
		setSuccess("");
		setError("");

        clearForm();
        base.CurrentKey = recordId;
        if (base.CurrentKey != "")
        {
            Culture obj = new Culture();
            obj = new CulturesManager().GetByKey(base.CurrentKey);
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
            new CulturesManager().DeleteById(recordId);
        }
        catch (Exception e)
        {
            setError(e.Message);
        }
		loadList();
    }

    private void setFlag(string recordId, bool value, string flagName)
    {
        try
        {
            var o1 = new CulturesManager().GetByKey(recordId);
            switch (flagName.ToLower())
            {
                case "enabled":
                    o1.Enabled = value;
                    break;
                default:
                    break;
            }
            new CulturesManager().Update(o1);
        }
        catch (Exception e1)
        {
            setError(Utility.GetLabel("RECORD_ERR_MSG") + "<br />" + e1.ToString());
        }
        finally { }
    }

	private void updateSortedTable()
	{
		//<input type="hidden" name="RowId" value='<%# Eval("Id") %>' />
		string[] rowIds = Request.Form.GetValues("RowId");
		int ordering = 1;

		foreach (string cultureCode in rowIds)
		{
			sortRecord(cultureCode, ordering);
			ordering++;
		}
	}

	public void sortRecord(string cultureCode, int ordering)
	{
		var man = new CulturesManager();
		var item = man.GetByKey(cultureCode);
		item.Ordering = ordering;
		man.Update(item);
	}

	private void loadList()
	{
		var man = new CulturesManager();
		var filter = new CulturesFilter();

		filter.Enabled = Utility.TristateBool.NotSet;
		switch (DropEnabledFilter.SelectedValue)
		{
			case "1":
				filter.Enabled = Utility.TristateBool.True;
				break;
			case "0":
				filter.Enabled = Utility.TristateBool.False;
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
