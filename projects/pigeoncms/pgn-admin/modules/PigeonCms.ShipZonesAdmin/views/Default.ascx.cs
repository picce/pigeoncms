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
public partial class Controls_ShipZonesAdmin : PigeonCms.BaseModuleControl
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
            loadListContinents();
            loadListCountries();
            LoadListCities();
        }
    }

    protected void DropEnabledFilter_SelectedIndexChanged(object sender, EventArgs e)
    {
        Grid1.DataBind();
    }

    protected void ObjDs1_ObjectCreating(object sender, ObjectDataSourceEventArgs e)
    {
        var typename = new ShipZonesManager();
        e.ObjectInstance = typename;
    }

    protected void ObjDs1_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
    {
        var filter = new ShipZonesFilter();
        e.InputParameters["filter"] = filter;
    }

    protected void Grid1_RowCommand(object sender, GridViewCommandEventArgs e)
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

    protected void Grid1_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
            Utility.AddGlyph(Grid1, e.Row);
    }

    protected void Grid1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            var item = new ShipZones();
            item = (ShipZones)e.Row.DataItem;

            LinkButton LnkTitle = (LinkButton)e.Row.FindControl("LnkTitle");
            LnkTitle.Text = "<i class='fa fa-pgn_edit fa-fw'></i>";
            LnkTitle.Text += Utility.Html.GetTextPreview(item.Title, 50, "");
            if (string.IsNullOrEmpty(LnkTitle.Text))
                LnkTitle.Text += Utility.GetLabel("NO_VALUE", "<no value>");
            if (Roles.IsUserInRole("debug"))
                LnkTitle.Text += " [" + item.Code + "]";

        }
    }

    protected void BtnNew_Click(object sender, EventArgs e)
    {
        editRow("");
    }

    protected void BtnSave_Click(object sender, EventArgs e)
    {
        LblErr.Text = "";
        LblOk.Text = "";

        try
        {
            var o1 = new ShipZones();
            var man = new ShipZonesManager();
            if (string.IsNullOrEmpty(base.CurrentKey))
            {
                form2obj(o1);
                o1 = man.Insert(o1);
            }
            else
            {
                o1 = man.GetByKey(base.CurrentKey);  //precarico i campi esistenti e nn gestiti dal form
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
        TxtCode.Text = "";
        TxtTitle.Text = "";
    }

    private void form2obj(ShipZones obj)
    {
        obj.Code = TxtCode.Text;
        obj.Title = TxtTitle.Text;

        foreach (ListItem item in ListContinents.Items)
        {
            if (item.Selected)
            {
                var o = new ShipGeoZones();
                o.Continent = item.Value;
                o.ZoneCode = obj.Code;
                new ShipGeoZonesManager().Insert(o);
            }
        }

        foreach (ListItem item in ListCountries.Items)
        {
            if (item.Selected)
            {
                var c = new PigeonCms.Geo.CountriesManager().GetByKey(item.Value);
                var o = new ShipGeoZones();
                //o.Continent = c.Continent;
                o.CountryCode = c.Code;
                o.ZoneCode = obj.Code;
                new ShipGeoZonesManager().Insert(o);
            }
        }

        foreach (ListItem item in ListCities.Items)
        {
            if (item.Selected)
            {
                var z = new PigeonCms.Geo.ZonesManager().GetByKey(int.Parse(item.Value));
                var c = new PigeonCms.Geo.CountriesManager().GetByKey(z.CountryCode);
                var o = new ShipGeoZones();
                //o.Continent = c.Continent;
                //o.CountryCode = z.CountryCode;
                o.CityCode = z.Id.ToString();
                o.ZoneCode = obj.Code;
                new ShipGeoZonesManager().Insert(o);
            }
        }

    }

    private void obj2form(ShipZones obj)
    {
        TxtCode.Text = obj.Code;
        TxtTitle.Text = obj.Title;
        
        var f = new ShipGeoZonesFilter();
        f.ZoneCode = obj.Code;
        var geo = new ShipGeoZonesManager().GetByFilter(f, "");

        var cities = geo.Where(x => x.HasCity).Select(x => x.CityCode).ToList();
        var countries = geo.Where(x => x.HasCountry).Select(x => x.CountryCode).ToList();
        var continents = geo.Where(x => x.HasContinent).Select(x => x.Continent).ToList();

        Utility.SetListBoxByValues(ListCities, cities, true);
        Utility.SetListBoxByValues(ListCountries, countries, true);
        Utility.SetListBoxByValues(ListContinents, continents, true);
    }

    private void editRow(string recordId)
    {
        LblOk.Text = "";
        LblErr.Text = "";

        clearForm();
        base.CurrentKey = recordId;
        var man = new ShipZonesManager();
        if (!string.IsNullOrEmpty(base.CurrentKey))
        {
            var obj = new ShipZones();
            obj = man.GetByKey(base.CurrentKey);
            obj2form(obj);
        }
        MultiView1.ActiveViewIndex = 1;
    }

    private void deleteRow(string recordId)
    {
        LblOk.Text = "";
        LblErr.Text = "";
        var man = new ShipZonesManager();
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

    private void loadListContinents()
    {
        ListContinents.Items.Clear();

        var nodes = new PigeonCms.Geo.CountriesManager().GetByFilter(new PigeonCms.Geo.CountriesFilter(), "");

        var continents = nodes.GroupBy(x => x.Continent)
                     .Select(g => g.First().Continent)
                     .ToList();

        foreach (var item in continents)
        {
            var i = new ListItem();
            i.Value = item;
            i.Text = item;
            i.Enabled = true;
            ListContinents.Items.Add(i);
        }
    }

    private void loadListCountries()
    {
        ListCountries.Items.Clear();

        var nodes = new PigeonCms.Geo.CountriesManager().GetByFilter(new PigeonCms.Geo.CountriesFilter(), "");

        foreach (var item in nodes)
        {
            var i = new ListItem();
            i.Value = item.Code;
            i.Text = item.Name;
            i.Enabled = true;
            ListCountries.Items.Add(i);
        }
    }

    private void LoadListCities()
    {
        ListCities.Items.Clear();

        var nodes = new PigeonCms.Geo.ZonesManager().GetByFilter(new PigeonCms.Geo.ZonesFilter(), "");

        foreach (var item in nodes)
        {
            var i = new ListItem();
            i.Value = item.Id.ToString();
            i.Text = item.Name;
            i.Enabled = true;
            ListCities.Items.Add(i);
        }
    }


    #endregion
}
