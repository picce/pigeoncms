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
        if (!Page.IsPostBack)
        {
            loadDropTopRowsFilter();
            loadDropsModuleTypes();
            loadDropTracerItemTypeFilter();
            loadDropDatesRangeFilter();
			loadList();
        }
    }

    protected void Filter_Changed(object sender, EventArgs e)
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

	protected void Rep1_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
		if (e.CommandName == "Select")
		{
			editRow(int.Parse(e.CommandArgument.ToString()));
		}
    }

	protected void Rep1_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
		if (e.Item.ItemType == ListItemType.Header)
			return;

		var item = (PigeonCms.LogItem)e.Item.DataItem;


		var LitType = (Literal)e.Item.FindControl("LitType");
		switch (item.Type)
		{
			case TracerItemType.Debug:
				LitType.Text = "<i class='fa fa-pgn_debug fa-fw'></i>";
				break;
			case TracerItemType.Info:
				LitType.Text = "<i class='fa fa-pgn_info fa-fw'></i>";
				break;
			case TracerItemType.Warning:
				LitType.Text = "<i class='fa fa-pgn_warning fa-fw'></i>";
				break;
			case TracerItemType.Alert:
				LitType.Text = "<i class='fa fa-pgn_alert fa-fw'></i>";
				break;
			case TracerItemType.Error:
				LitType.Text = "<i class='fa fa-pgn_error fa-fw'></i>";
				break;
			default:
				LitType.Text = "<i class='fa fa-pgn_debug fa-fw'></i>";
				break;
		}


		var LitDateInserted = (Literal)e.Item.FindControl("LitDateInserted");
		LitDateInserted.Text = item.DateInserted.ToString();

		var LitItemInfo = (Literal)e.Item.FindControl("LitItemInfo");
		LitItemInfo.Text = item.UserHostAddress + "<br>"
			+ item.SessionId;

		var LitId = (Literal)e.Item.FindControl("LitId");
		LitId.Text = item.Id.ToString() + " - " + item.UserInserted;

        var LitModule = (Literal)e.Item.FindControl("LitModule");
        LitModule.Text = item.ModuleFullName;

        var LitUrl = (Literal)e.Item.FindControl("LitUrl");
		string path = Utility.Html.GetTextPreview(item.Url, 50, "");
        try
        {
            Uri url = new Uri(item.Url);
            path = Utility.Html.GetTextPreview(url.AbsolutePath, 50, "");
            if (item.Url.StartsWith("https://"))
                LitUrl.Text += "https://...";
        }
        catch { }
        LitUrl.Text += path;

        var LitDescription = (Literal)e.Item.FindControl("LitDescription");
        LitDescription.Text = Utility.Html.GetTextPreview(item.Description, 150, "");
    }

    protected void BtnCancel_Click(object sender, EventArgs e)
    {
		showInsertPanel(false);
    }


    private void clearForm()
    {
        LitId.Text = "";
        LitCreated.Text = "";
        LitType.Text = "";
        LitModuleType.Text = "";
        LitView.Text = "";
        LitUserHostAddress.Text = "";
        LitSessionId.Text = "";
        LitUrl.Text = "";
        LitDescription.Text = "";
    }

    private void form2obj(LogItem obj)
    {
        throw new NotImplementedException("form2obj");
    }

    private void obj2form(LogItem obj)
    {
        LitId.Text = obj.Id.ToString();
        LitCreated.Text = obj.DateInserted + " " + obj.UserInserted;
        LitType.Text = obj.Type.ToString();

        var module = new Module();
        module = new ModulesManager().GetByKey(obj.ModuleId);
        LitModuleType.Text = module.ModuleFullName;
        if (string.IsNullOrEmpty(module.ModuleFullName))
        {
            LitModuleType.Text += "&nbsp;" + Utility.GetErrorLabel("NotInstalledModuleType", "Not installed module type");
        }
        LitView.Text = module.CurrView;
        LitUserHostAddress.Text = obj.UserHostAddress;
        LitSessionId.Text = obj.SessionId;
        LitUserInserted.Text = obj.UserInserted;
        LitUrl.Text = obj.Url;
        LitDescription.Text = obj.Description;
    }

    private void editRow(int recordId)
    {
        var obj = new LogItem();

        clearForm();
        base.CurrentId = recordId;

        if (base.CurrentId > 0)
        {
            obj = new LogItemsManager().GetByKey(base.CurrentId);
            obj2form(obj);
        }
		showInsertPanel(true);
    }

	private void showInsertPanel(bool toShow)
	{
		PigeonCms.Utility.Script.RegisterStartupScript(Upd1, "bodyBlocked", "bodyBlocked(" + toShow.ToString().ToLower() + ");");

		if (toShow)
			PanelInsert.Visible = true;
		else
			PanelInsert.Visible = false;
	}

    private void deleteRow(int recordId)
    {
        try
        {
            new LogItemsManager().DeleteById(recordId);
        }
        catch (Exception e)
        {
            //LblErrInsert.Text = e.Message;
        }
		loadList();
    }

	private void loadList()
	{
		var man = new LogItemsManager();
		var filter = new LogItemsFilter();
		filter.TopRows = int.Parse(DropTopRowsFilter.SelectedValue);
		if (DropTracerItemTypeFilter.SelectedValue != "")
		{
			filter.FilterType = true;
			filter.Type = (TracerItemType)Enum.Parse(typeof(TracerItemType), DropTracerItemTypeFilter.SelectedValue);
		}

		if (DropModuleTypesFilter.SelectedValue != "")
			filter.ModuleFullName = DropModuleTypesFilter.SelectedValue;

		if (DropDatesRangeFilter.SelectedValue != "")
		{
			DatesRange.RangeType rangeType =
				(DatesRange.RangeType)Enum.Parse(typeof(DatesRange.RangeType), DropDatesRangeFilter.SelectedValue);
			DatesRange datesRange = new DatesRange(rangeType);
			filter.DateInsertedRange = datesRange;
		}

		if (!string.IsNullOrEmpty(TxtDescriptionFilter.Text))
			filter.DescriptionPart = TxtDescriptionFilter.Text;

		if (!string.IsNullOrEmpty(TxtIpFilter.Text))
			filter.UserHostAddressPart = TxtIpFilter.Text;

		if (!string.IsNullOrEmpty(TxtSessionIdFilter.Text))
			filter.SessionIdPart = TxtSessionIdFilter.Text;


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
			var pages = new ArrayList();
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

    private void loadDropTracerItemTypeFilter()
    {
        DropTracerItemTypeFilter.Items.Clear();
        DropTracerItemTypeFilter.Items.Add(
            new ListItem(Utility.GetLabel("LblSelectType", "Select type"), ""));
        foreach (string item in Enum.GetNames(typeof(TracerItemType)))
        {
            int value = (int)Enum.Parse(typeof(TracerItemType), item);
            ListItem listItem = new ListItem(item, value.ToString());
            DropTracerItemTypeFilter.Items.Add(listItem);
        }
    }

    private void loadDropsModuleTypes()
    {
        DropModuleTypesFilter.Items.Clear();
        DropModuleTypesFilter.Items.Add(new ListItem(Utility.GetLabel("LblSelectModule", "Select module"), ""));

        ModuleTypeFilter filter = new ModuleTypeFilter();
        List<ModuleType> recordList = new ModuleTypeManager(true).GetByFilter(filter, "FullName");
        foreach (ModuleType record1 in recordList)
        {
            DropModuleTypesFilter.Items.Add(
                new ListItem(record1.FullName, record1.FullName));
        }
    }

    private void loadDropDatesRangeFilter()
    {
		DropDatesRangeFilter.Items.Clear();
		DropDatesRangeFilter.Items.Add(new ListItem("Today", "3"));
		DropDatesRangeFilter.Items.Add(new ListItem("Always", "2"));
		DropDatesRangeFilter.Items.Add(new ListItem("Last week", "4"));
		DropDatesRangeFilter.Items.Add(new ListItem("Last month", "5"));
    }

    private void loadDropTopRowsFilter()
    {
        DropTopRowsFilter.Items.Clear();
        DropTopRowsFilter.Items.Add(new ListItem("Last 50 items", "50"));
        DropTopRowsFilter.Items.Add(new ListItem("Last 100 items", "100"));
        DropTopRowsFilter.Items.Add(new ListItem("Last 200 items", "200"));
        DropTopRowsFilter.Items.Add(new ListItem("Last 500 items", "500"));
        DropTopRowsFilter.Items.Add(new ListItem("All items", "0"));
    }

}
