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
            loadDropMasterPages();
            loadDropThemes();
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

    protected void DropPublishedFilter_SelectedIndexChanged(object sender, EventArgs e)
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


		var item = (MvcRoute)e.Item.DataItem;

		{
			var LitPublished = (Literal)e.Item.FindControl("LitPublished");
			string enabledClass = "";
			if (item.Published)
				enabledClass = "checked";
			LitPublished.Text = "<span class='table-modern--checkbox--square " + enabledClass + "'></span>";

			var LnkPublished = (LinkButton)e.Item.FindControl("LnkPublished");
			LnkPublished.CssClass = "table-modern--checkbox " + enabledClass;
			LnkPublished.CommandName = item.Published ? "Enabled0" : "Enabled1";
		}


		{
			var LitUseSsl = (Literal)e.Item.FindControl("LitUseSsl");
			string enabledClass = "";
			if (item.UseSsl)
				enabledClass = "checked";
			LitUseSsl.Text = "<span class='table-modern--checkbox--square " + enabledClass + "'></span>";

			var LnkUseSsl = (LinkButton)e.Item.FindControl("LnkUseSsl");
			LnkUseSsl.CssClass = "table-modern--checkbox " + enabledClass;
			LnkUseSsl.CommandName = item.Published ? "UseSsl0" : "UseSsl1";
		}

		{
			var LitCore = (Literal)e.Item.FindControl("LitCore");
			string enabledClass = "";
			if (item.IsCore)
				enabledClass = "checked";
			LitCore.Text = "<span class='table-modern--checkbox--square " + enabledClass + "'></span>";
		}
	}

	protected void Rep1_ItemCommand(object source, RepeaterCommandEventArgs e)
	{
		if (e.CommandName == "Select")
		{
			try
			{
				editRow(int.Parse(e.CommandArgument.ToString()));
			}
			catch (Exception e1)
			{
				setError(e1.Message);
			}
		}
		if (e.CommandName == "DeleteRow")
		{
			deleteRow(int.Parse(e.CommandArgument.ToString()));
		}
		//Enabled
		if (e.CommandName == "Enabled0")
		{
			setFlag(Convert.ToInt32(e.CommandArgument), false, "enabled");
			loadList();
		}
		if (e.CommandName == "Enabled1")
		{
			setFlag(Convert.ToInt32(e.CommandArgument), true, "enabled");
			loadList();
		}

		//Use SSL
		if (e.CommandName == "UseSsl0")
		{
			setFlag(Convert.ToInt32(e.CommandArgument), false, "usessl");
			loadList();
		}
		if (e.CommandName == "UseSsl1")
		{
			setFlag(Convert.ToInt32(e.CommandArgument), true, "usessl");
			loadList();
		}
	}


    protected void BtnApply_Click(object sender, EventArgs e)
    {
		setError("");
		setSuccess("");

        try
        {
            new MvcRoutesManager().SetAppRoutes();
        }
        catch (Exception ex)
        {
            setError("Error updating routes table:" + ex.ToString());
            PigeonCms.Trace.Warn("Error", "Error updating routes table", ex);
        }
        finally
        {
            setSuccess("Routes list updated sucessfully");
        }
    }

    protected void BtnNew_Click(object sender, EventArgs e)
    {
        editRow(0);
    }

    protected void BtnSave_Click(object sender, EventArgs e)
    {
		setError("");
		setSuccess("");

        try
        {
            var o1 = new MvcRoute();
            if (base.CurrentId == 0)
            {
                form2obj(o1);
                o1 = new MvcRoutesManager().Insert(o1);
            }
            else
            {
                o1 = new MvcRoutesManager().GetByKey(base.CurrentId);  //precarico i campi esistenti e nn gestiti dal form
                form2obj(o1);
                new MvcRoutesManager().Update(o1);
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
		setError("");
		setSuccess("");
		showInsertPanel(false);
    }

    #region private methods

    private void clearForm()
    {
        TxtName.Text = "";
        TxtName.Enabled = true;
        TxtPattern.Text = "";
        ChkPublished.Checked = true;
        ChkUseSsl.Checked = false;
        ChkIsCore.Checked = false;
        Utility.SetDropByValue(DropMasterPage, "");
        Utility.SetDropByValue(DropTheme, "");
    }

    private void form2obj(MvcRoute obj)
    {
        obj.Id = base.CurrentId;
        obj.Published = ChkPublished.Checked;
        obj.UseSsl = ChkUseSsl.Checked;
        obj.Name = TxtName.Text;
        obj.Pattern = TxtPattern.Text;
        obj.IsCore = ChkIsCore.Checked;
        obj.CurrMasterPage = DropMasterPage.SelectedValue;
        obj.CurrTheme = DropTheme.SelectedValue;
    }

    private void obj2form(MvcRoute obj)
    {
        TxtName.Text = obj.Name;
        TxtPattern.Text = obj.Pattern;
        ChkPublished.Checked = obj.Published;
        ChkUseSsl.Checked = obj.UseSsl;
        ChkIsCore.Checked = obj.IsCore;
        Utility.SetDropByValue(DropMasterPage, obj.CurrMasterPage);
        Utility.SetDropByValue(DropTheme, obj.CurrTheme);
        TxtName.Enabled = !obj.IsCore;
    }

    private void editRow(int recordId)
    {
		setError("");
		setSuccess("");

        clearForm();
        base.CurrentId = recordId;
        if (base.CurrentId > 0)
        {
            var obj = new MvcRoute();
            obj = new MvcRoutesManager().GetByKey(base.CurrentId);
            obj2form(obj);
        }
		showInsertPanel(true);
    }

    private void deleteRow(int recordId)
    {
		setError("");
		setSuccess("");

        try
        {
            new MvcRoutesManager().DeleteById(recordId);
        }
        catch (Exception e)
        {
            setError(e.Message);
        }
		loadList();
    }

    private void setFlag(int recordId, bool value, string flagName)
    {
        try
        {
            var o1 = new PigeonCms.MvcRoute();
            o1 = new MvcRoutesManager().GetByKey(recordId);
            switch (flagName.ToLower())
            {
				case "enabled":
                    o1.Published = value;
                    break;
                case "usessl":
                    o1.UseSsl = value;
                    break;
                default:
                    break;
            }
            new MvcRoutesManager().Update(o1);
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

		foreach (string sid in rowIds)
		{
			int id = 0;
			int.TryParse(sid, out id);
			sortRecord(id, ordering);
			ordering++;
		}
	}

	public void sortRecord(int id, int ordering)
	{
		var man = new MvcRoutesManager();
		var item = man.GetByKey(id);
		item.Ordering = ordering;
		man.Update(item);
	}

    private void loadDropMasterPages()
    {
        DropMasterPage.Items.Clear();
        DropMasterPage.Items.Add(new ListItem(Utility.GetLabel("LblUseBlobal", "Use global"), ""));

        Dictionary<string, string> recordList = new MasterPagesObjManager().GetList();
        foreach (KeyValuePair<string, string> item in recordList)
        {
            DropMasterPage.Items.Add(new ListItem(item.Value, item.Key));
        }
    }

    private void loadDropThemes()
    {
        DropTheme.Items.Clear();
        DropTheme.Items.Add(new ListItem(Utility.GetLabel("LblUseBlobal", "Use global"), ""));

        Dictionary<string, string> recordList = new ThemesObjManager().GetList();
        foreach (KeyValuePair<string, string> item in recordList)
        {
            DropTheme.Items.Add(new ListItem(item.Value, item.Key));
        }
    }

	private void loadList()
	{
		var man = new MvcRoutesManager();
		var filter = new MvcRoutesFilter();

		filter.Published = Utility.TristateBool.NotSet;
		switch (DropPublishedFilter.SelectedValue)
		{
			case "1":
				filter.Published = Utility.TristateBool.True;
				break;
			case "0":
				filter.Published = Utility.TristateBool.False;
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
