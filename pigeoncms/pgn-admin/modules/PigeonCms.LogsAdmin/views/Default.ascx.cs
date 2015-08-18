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
        }
    }

    protected void Filter_Changed(object sender, EventArgs e)
    {
        try { Grid1.DataBind(); }
        catch (Exception ex)
        {
            LblErr.Text = ex.Message;
        }
    }

    protected void ObjDs1_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
    {
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

        e.InputParameters["filter"] = filter;
    }

    protected void Grid1_DataBinding(object sender, EventArgs e)
    {

    }

    protected void Grid1_RowCommand(object sender, GridViewCommandEventArgs e)
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

    protected void Grid1_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
            Utility.AddGlyph(Grid1, e.Row);
    }

    protected void Grid1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            var item = new PigeonCms.LogItem();
            item = (PigeonCms.LogItem)e.Row.DataItem;

            Literal LitModule = (Literal)e.Row.FindControl("LitModule");
            LitModule.Text = item.ModuleFullName;

            Literal LitIp = (Literal)e.Row.FindControl("LitIp");
            LitIp.Text = item.UserHostAddress;

            Literal LitSessionId = (Literal)e.Row.FindControl("LitSessionId");
            LitSessionId.Text = item.SessionId;

            Literal LitUrl = (Literal)e.Row.FindControl("LitUrl");
            string path = Utility.Html.GetTextPreview(item.Url, 40, "");
            try
            {
                Uri url = new Uri(item.Url);
                path = Utility.Html.GetTextPreview(url.AbsolutePath, 40, "");
                if (item.Url.StartsWith("https://"))
                    LitUrl.Text += "https://...";
            }
            catch { }

            LitUrl.Text += path;

            Literal LitDescription = (Literal)e.Row.FindControl("LitDescription");
            LitDescription.Text = Utility.Html.GetTextPreview(item.Description, 120, "");

            var LnkSel = (LinkButton)e.Row.FindControl("LnkSel");
            switch (item.Type)
            {
                case TracerItemType.Debug:
                    LnkSel.Text = "<i class='fa fa-pgn_debug fa-fw'></i>";
                    break;
                case TracerItemType.Info:
                    LnkSel.Text = "<i class='fa fa-pgn_info fa-fw'></i>";
                    break;
                case TracerItemType.Warning:
                    LnkSel.Text = "<i class='fa fa-pgn_warning fa-fw'></i>";
                    break;
                case TracerItemType.Alert:
                    LnkSel.Text = "<i class='fa fa-pgn_alert fa-fw'></i>";
                    break;
                case TracerItemType.Error:
                    LnkSel.Text = "<i class='fa fa-pgn_error fa-fw'></i>";
                    break;
                default:
                    LnkSel.Text = "<i class='fa fa-pgn_debug fa-fw'></i>";
                    break;
            }
        }
    }

    protected void BtnCancel_Click(object sender, EventArgs e)
    {
        LblErr.Text = "";
        LblOk.Text = "";
        MultiView1.ActiveViewIndex = 0;
    }

    protected void MultiView1_ActiveViewChanged(object sender, EventArgs e)
    {
        if (MultiView1.ActiveViewIndex == 0)    //list view
        {
        }
    }

    #region private methods

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
        LblOk.Text = "";
        LblErr.Text = "";

        clearForm();
        base.CurrentId = recordId;

        if (base.CurrentId == 0)
        { }
        else
        {
            obj = new LogItemsManager().GetByKey(base.CurrentId);
            obj2form(obj);
        }
        MultiView1.ActiveViewIndex = 1;
    }

    private void deleteRow(int recordId)
    {
        LblOk.Text = "";
        LblErr.Text = "";

        try
        {
            new LogItemsManager().DeleteById(recordId);
        }
        catch (Exception e)
        {
            LblErr.Text = e.Message;
        }
        Grid1.DataBind();
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
        try
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
        catch (Exception ex)
        {
            LblErr.Text = ex.ToString();
        }
    }

    private void loadDropDatesRangeFilter()
    {
        try
        {
            DropDatesRangeFilter.Items.Clear();
            DropDatesRangeFilter.Items.Add(new ListItem("Today", "3"));
            DropDatesRangeFilter.Items.Add(new ListItem("Always", "2"));
            DropDatesRangeFilter.Items.Add(new ListItem("Last week", "4"));
            DropDatesRangeFilter.Items.Add(new ListItem("Last month", "5"));
        }
        catch (Exception ex)
        {
            LblErr.Text = ex.ToString();
        }
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

    #endregion
}
