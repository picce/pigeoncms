using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Web.Caching;
using System.Collections.Generic;
using PigeonCms;
using PigeonCms.Shop;
using PigeonCms.Core.Helpers;
using System.Globalization;

public partial class Controls_CouponAdmin : PigeonCms.BaseModuleControl
{
    const int COL_ORDERING_INDEX = 3;
    const int COL_ORDER_ARROWS_INDEX = 4;
    const int COL_ACCESS_INDEX = 6;
    const int COL_FILES_INDEX = 7;
    const int COL_IMAGES_INDEX = 8;
    const int COL_ID_INDEX = 10;
    private PigeonCms.Shop.Settings shopSettings = new PigeonCms.Shop.Settings();

    protected DateTime ValidFrom
    {
        get
        {
            CultureInfo culture;
            DateTimeStyles styles;
            culture = CultureInfo.CreateSpecificCulture("it-IT");
            styles = DateTimeStyles.None;
            DateTime res;
            DateTime.TryParse(TxtValidFrom.Text, culture, styles, out res);
            return res.Date;
        }
        set
        {
            TxtValidFrom.Text = "";
            if (value != DateTime.MinValue)
                TxtValidFrom.Text = value.ToShortDateString();
        }
    }

    protected DateTime ValidTo
    {
        get
        {
            CultureInfo culture;
            DateTimeStyles styles;
            culture = CultureInfo.CreateSpecificCulture("it-IT");
            styles = DateTimeStyles.None;
            DateTime res;
            DateTime.TryParse(TxtValidTo.Text, culture, styles, out res);
            return res.Date;
        }
        set
        {
            TxtValidTo.Text = "";
            if (value != DateTime.MinValue)
                TxtValidTo.Text = value.ToShortDateString();
        }
    }

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
            loadListCategories();
            loadListItems();
        }
    }


    protected void ObjDs1_ObjectCreating(object sender, ObjectDataSourceEventArgs e)
    {
        var typename = new CouponsManager();
        e.ObjectInstance = typename;
    }

    protected void ObjDs1_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
    {
        CouponsFilter filter = new CouponsFilter();

        filter.Enabled = Utility.TristateBool.NotSet;

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
        //Enabled
        if (e.CommandName == "ImgEnabledOk")
        {
            setFlag(Convert.ToInt32(e.CommandArgument), false, "enabled");
            Grid1.DataBind();
        }
        if (e.CommandName == "ImgEnabledKo")
        {
            setFlag(Convert.ToInt32(e.CommandArgument), true, "enabled");
            Grid1.DataBind();
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
            var item = new Coupon();
            item = (Coupon)e.Row.DataItem;

            var LnkTitle = (LinkButton)e.Row.FindControl("LnkTitle");
            LnkTitle.Text = "<i class='fa fa-pgn_edit fa-fw'></i>";
            LnkTitle.Text += Utility.Html.GetTextPreview(item.Code, 50, "");
            if (string.IsNullOrEmpty(LnkTitle.Text))
                LnkTitle.Text += Utility.GetLabel("NO_VALUE", "<no value>");
            if (Roles.IsUserInRole("debug"))
                LnkTitle.Text += " [" + item.Id.ToString() + "]";


            var LitCouponInfo = e.Row.FindControl("LitCouponInfo") as Literal;
            LitCouponInfo.Text += "max uses: " 
                + (item.MaxUses > 0 ? item.MaxUses.ToString() : "unlimited") 
                + " times <br>";
            LitCouponInfo.Text += "used: " + item.UsesCounter + " times <br>";
            if (item.CategoriesIdList.Count > 0)
                LitCouponInfo.Text += item.CategoriesIdList.Count + " filters on categories<br>";
            if (item.ItemsIdList.Count > 0)
                LitCouponInfo.Text += item.ItemsIdList.Count + " filters on items<br>";
            
            //if percentage show percent, also the default 
            Literal LitValueAmount = e.Row.FindControl("LitValueAmount") as Literal;
            if (item.IsPercentage)
            {
                LitValueAmount.Text = (item.Amount * 100).ToString() + "%";
            }
            else
            {
                LitValueAmount.Text = shopSettings.CurrencyDefault.Symbol + " " + item.Amount.ToString("0.00");
            }

            //Published
            if (item.Enabled)
            {
                var img1 = e.Row.FindControl("ImgEnabledOk");
                img1.Visible = true;
            }
            else
            {
                var img1 = e.Row.FindControl("ImgEnabledKo");
                img1.Visible = true;
            }

            var LitValidForm = e.Row.FindControl("LitValidFrom") as Literal;
            var LitValidTo = e.Row.FindControl("LitValidTo") as Literal;

            if (item.ValidFrom != DateTime.MinValue)
                LitValidForm.Text = item.ValidFrom.ToShortDateString();
            if (item.ValidTo != DateTime.MinValue)
                LitValidTo.Text = item.ValidTo.ToShortDateString();

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
            Coupon o1 = new Coupon();
            if (base.CurrentId == 0)
            {
                form2obj(o1);
                o1 = new CouponsManager().Insert(o1);
            }
            else
            {
                o1 = new CouponsManager().GetByKey(base.CurrentId);  //precarico i campi esistenti e nn gestiti dal form
                form2obj(o1);
                new CouponsManager().Update(o1);
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
        TxtAmount.Text = "";
        TxtMaxUses.Text = "";
        TxtMinAmount.Text = "";
        ChkEnabled.Checked = true;
        ChkPercentage.Checked = false;
        this.ValidFrom = DateTime.MinValue;
        this.ValidTo = DateTime.MinValue;
        Utility.SetListBoxByValues(ListCategories, 0);
        Utility.SetListBoxByValues(ListItems, 0);
    }

    private void form2obj(Coupon obj)
    {
        obj.Id = base.CurrentId;
        obj.Enabled = ChkEnabled.Checked;
        obj.Code = TxtCode.Text;
        decimal amount = 0m;
        if (!string.IsNullOrEmpty(TxtAmount.Text))
        {
            decimal.TryParse(TxtAmount.Text, out amount);
        }
        if (amount > 0)
        {
            obj.Amount = amount;
        }
        decimal minAmount = 0m;
        if (!string.IsNullOrEmpty(TxtMinAmount.Text))
        {
            decimal.TryParse(TxtMinAmount.Text, out minAmount);
        }
        if (minAmount > 0)
        {
            obj.MinOrderAmount = minAmount;
        }
        obj.IsPercentage = ChkPercentage.Checked;
        obj.ValidFrom = this.ValidFrom;
        obj.ValidTo = this.ValidTo;
        int maxUses = 0;
        int.TryParse(TxtMaxUses.Text, out maxUses);
        obj.MaxUses = maxUses;
        
        obj.CategoriesIdListString = String.Join(",", 
            ListCategories.Items.Cast<ListItem>()
                .Where(i => i.Selected && i.Value != "0")
                .Select(i => i.Value)
                .ToArray());

        obj.ItemsIdListString = String.Join(",",
            ListItems.Items.Cast<ListItem>()
                .Where(i => i.Selected && i.Value != "0")
                .Select(i => i.Value)
                .ToArray());

    }

    private void obj2form(Coupon obj)
    {
        ChkEnabled.Checked = obj.Enabled;
        TxtCode.Text = obj.Code;
        TxtAmount.Text = obj.Amount.ToString();
        TxtMinAmount.Text = obj.MinOrderAmount.ToString();
        ChkPercentage.Checked = obj.IsPercentage;
        TxtMaxUses.Text = obj.MaxUses.ToString();
        this.ValidFrom = obj.ValidFrom;
        this.ValidTo = obj.ValidTo;

        Utility.SetListBoxByValues(ListCategories, obj.CategoriesIdList);
        Utility.SetListBoxByValues(ListItems, obj.ItemsIdList);
    }

    private void editRow(int recordId)
    {
        LblOk.Text = "";
        LblErr.Text = "";

        clearForm();
        base.CurrentId = recordId;
        if (base.CurrentId > 0)
        {
            Coupon obj = new Coupon();
            obj = new CouponsManager().GetByKey(base.CurrentId);
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
            new CouponsManager().DeleteById(recordId);
        }
        catch (Exception e)
        {
            LblErr.Text = RenderError(e.Message);
        }
        Grid1.DataBind();
    }


    private void setFlag(int recordId, bool value, string flagName)
    {
        try
        {
            var o1 = new PigeonCms.Shop.Coupon();
            o1 = new CouponsManager().GetByKey(recordId);
            switch (flagName.ToLower())
            {
                case "enabled":
                    o1.Enabled = value;
                    break;
                default:
                    break;
            }
            new CouponsManager().Update(o1);
        }
        catch (Exception e1)
        {
            LblErr.Text = RenderError(Utility.GetLabel("RECORD_ERR_MSG") + "<br />" + e1.ToString());
        }
        finally { }
    }

    private void loadListCategories()
    {
        var man = new CategoriesManager();
        var filter = new CategoriesFilter();
        filter.SectionId = shopSettings.SectionId;
        var list = man.GetByFilter(filter, "");

        ListCategories.Items.Clear();
        ListCategories.Items.Add(new ListItem("", "0"));
        foreach (var item in list)
        {
            var listItem = new ListItem();
            listItem.Value = item.Id.ToString();
            listItem.Text = item.Title;
            listItem.Enabled = true;

            ListCategories.Items.Add(listItem);
        }
    }

    private void loadListItems()
    {
        var man = new ItemsManager<Item, ItemsFilter>();
        var filter = new ItemsFilter();
        filter.SectionId = shopSettings.SectionId;
        var list = man.GetByFilter(filter, "");

        ListItems.Items.Clear();
        ListItems.Items.Add(new ListItem("", "0"));
        foreach (var item in list)
        {
            var listItem = new ListItem();
            listItem.Value = item.Id.ToString();
            listItem.Text = item.Category.Title + " > " + item.Title;
            listItem.Enabled = true;

            ListItems.Items.Add(listItem);
        }
    }

    #endregion
}
