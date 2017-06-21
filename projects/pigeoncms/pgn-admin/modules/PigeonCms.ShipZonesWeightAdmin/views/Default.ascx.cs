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
using System.Linq;
using PigeonCms.Core.Helpers;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using PigeonCms.Shop;

//TOCHECK-LOLLO
public partial class Controls_ShipZonesWeightAdmin : PigeonCms.BaseModuleControl
{
    const int COL_ORDERING_INDEX = 3;
    const int COL_ORDER_ARROWS_INDEX = 4;
    const int COL_ACCESS_INDEX = 6;
    const int COL_FILES_INDEX = 7;
    const int COL_IMAGES_INDEX = 8;
    const int COL_ID_INDEX = 10;

    protected new void Page_Init(object sender, EventArgs e)
    {
        base.Page_Init(sender, e);
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        LblOk.Text = "";
        LblErr.Text = "";
        if (!Page.IsPostBack)
        {
            loadDropZones();
            loadDropZoneFilter();
        }
    }

    protected void DropZoneFilter_SelectedIndexChanged(object sender, EventArgs e)
    {
        Grid1.DataBind();
    }

    protected void ObjDs1_ObjectCreating(object sender, ObjectDataSourceEventArgs e)
    {
        var typename = new ShipZonesWeightManager();
        e.ObjectInstance = typename;
    }

    protected void ObjDs1_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
    {
        var filter = new ShipZonesWeightFilter();

        if (DropZoneFilter.SelectedValue != "")
        {
            filter.ZoneCode = DropZoneFilter.SelectedValue;
        }

        e.InputParameters["filter"] = filter;

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

            var item = new ShipZonesWeight();
            item = (ShipZonesWeight)e.Row.DataItem;

            var zone = new ShipZonesManager().GetByKey(item.ZoneCode);

            LinkButton LnkTitle = (LinkButton)e.Row.FindControl("LnkTitle");
            LnkTitle.Text = "<i class='fa fa-pgn_edit fa-fw'></i>";
            LnkTitle.Text += Utility.Html.GetTextPreview(zone.Title, 50, "");
            if (string.IsNullOrEmpty(LnkTitle.Text))
                LnkTitle.Text += Utility.GetLabel("NO_VALUE", "<no value>");
            if (Roles.IsUserInRole("debug"))
                LnkTitle.Text += " [" + item.Id + "]";

        }
    }

    protected void BtnNew_Click(object sender, EventArgs e)
    {
        editRow(0);
    }

    protected void BtnSave_Click(object sender, EventArgs e)
    {
        LblErr.Text = "";
        LblOk.Text = "";

        try
        {
            var o1 = new ShipZonesWeight();
            var man = new ShipZonesWeightManager();
            if (base.CurrentId == 0)
            {
                form2obj(o1);
                o1 = man.Insert(o1);
            }
            else
            {
                o1 = man.GetByKey(base.CurrentId);  //precarico i campi esistenti e nn gestiti dal form
                form2obj(o1);
                man.Update(o1);
            }
            Grid1.DataBind();
            LblOk.Text = RenderSuccess(Utility.GetLabel("RECORD_SAVED_MSG"));
            MultiView1.ActiveViewIndex = 0;
        }
        catch (Exception e1)
        {
            LblErr.Text = RenderError(Utility.GetLabel("RECORD_ERR_MSG") + "<br />" + e1.ToString());
        }
        finally
        {
        }
    }

    protected void BtnCancel_Click(object sender, EventArgs e)
    {
        MultiView1.ActiveViewIndex = 0;
    }

    #region private methods

    private void clearForm()
    {
        TxtWeightFrom.Text = "";
        TxtWeightTo.Text = "";
        TxtShippingPrice.Text = "";
    }

    private void form2obj(ShipZonesWeight obj)
    {
        obj.ZoneCode = DDLZones.SelectedValue;
        decimal weightFrom = 0m;
        decimal weightTo = 0m;
        decimal shippingPrice = 0m;
        decimal.TryParse(TxtWeightFrom.Text, out weightFrom);
        decimal.TryParse(TxtWeightTo.Text, out weightTo);
        decimal.TryParse(TxtShippingPrice.Text, out shippingPrice);
        obj.WeightFrom = weightFrom;
        obj.WeightTo = weightTo;
        obj.ShippingPrice = shippingPrice;
    }

    private void obj2form(ShipZonesWeight obj)
    {
        loadDropZones();
        Utility.SetDropByValue(DDLZones, obj.ZoneCode);
        TxtWeightFrom.Text = obj.WeightFrom.ToString();
        TxtWeightTo.Text = obj.WeightTo.ToString();
        TxtShippingPrice.Text = obj.ShippingPrice.ToString();
    }

    private void editRow(int recordId)
    {
        LblOk.Text = "";
        LblErr.Text = "";

        clearForm();
        base.CurrentId = recordId;
        var man = new ShipZonesWeightManager();
        if (base.CurrentId > 0)
        {
            var obj = new ShipZonesWeight();
            obj = man.GetByKey(base.CurrentId);
            obj2form(obj);
        }
        else
        {
            loadDropZones();
        }
        MultiView1.ActiveViewIndex = 1;
    }

    private void deleteRow(int recordId)
    {
        LblOk.Text = "";
        LblErr.Text = "";
        var man = new ShipZonesWeightManager();
        try
        {
            man.DeleteById(recordId);
        }
        catch (Exception e)
        {
            LblErr.Text = RenderError(e.Message);
        }
        Grid1.DataBind();
    }

    private void loadDropZones()
    {

        DDLZones.Items.Clear();
        DDLZones.Items.Add(new ListItem(Utility.GetLabel("LblSelectZone", "Seleziona Zona"), ""));

        var zones = new ShipZonesManager().GetByFilter(new ShipZonesFilter(), "");
        foreach (var z in zones)
        {
            DDLZones.Items.Add(
                    new ListItem(z.Title, z.Code));
        }
    }

    private void loadDropZoneFilter()
    {
        DropZoneFilter.Items.Clear();
        DropZoneFilter.Items.Add(new ListItem(Utility.GetLabel("LblFilterZone", "Filtra per Zona"), ""));

        var zones = new ShipZonesManager().GetByFilter(new ShipZonesFilter(), "");
        foreach (var z in zones)
        {
            DropZoneFilter.Items.Add(
                    new ListItem(z.Title, z.Code));
        }
    }

    #endregion
}
