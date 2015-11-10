using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Web.Caching;
using System.Collections.Generic;
using System.Globalization;
using PigeonCms;
using PigeonCms.Shop;
using PigeonCms.Core.Helpers;


public partial class Controls_PigeonCms_Shop_OrdersAdmin :
    PigeonCms.Shop.OrdersAdminControl<
    PigeonCms.Shop.OrdersManager<Order, OrdersFilter, OrderRow, OrderRowsFilter>,
            Order, OrdersFilter, OrderRow, OrderRowsFilter>
{
    const int COL_ALIAS_INDEX = 2;
    const int COL_SECTION_INDEX = 3;
    const int COL_CATEGORY_INDEX = 4;
    const int COL_TYPE_INDEX = 5;
    const int COL_ORDER_ARROWS_INDEX = 6;
    const int COL_ORDERING_INDEX = 7;
    const int COL_ACCESSTYPE_INDEX = 9;
    const int COL_ACCESSLEVEL_INDEX = 10;
    const int COL_ID_INDEX = 14;

    const int VIEW_GRID = 0;
    const int VIEW_INSERT = 1;
    const int VIEW_ROWS = 2;



    protected new void Page_Init(object sender, EventArgs e)
    {
        base.Page_Init(sender, e);

        if (this.BaseModule.DirectEditMode)
        {
        }

        //restrictions
        //Grid1.Columns[COL_SECTION_INDEX].Visible = this.ShowSectionColumn;
        //TxtItemDate.Visible = this.ShowDates;
        //DropEnabledFilter.Visible = this.ShowEnabledFilter;
    }

    protected new void Page_Load(object sender, EventArgs e)
    {
        base.Page_Load(sender, e);

        LblOk.Text = RenderSuccess("");
        LblErr.Text = RenderError("");

        if (this.BaseModule.DirectEditMode)
        {
        }


        if (!Page.IsPostBack)
        {
            if (!string.IsNullOrEmpty(this.OwnerUser))
            {
                TxtOwnerUserFilter.Enabled = false;
                TxtOwnerUserFilter.Text = this.OwnerUser;
            }
            loadDropOrderDatesRangeFilter();
            loadDropPaymentFilter(this.PaymentFilter);
            loadDropConfirmedFilter(this.OrderConfirmedFilter);
            loadListOrders();
            loadDropPayments();
            loadDropShipments();
        }
    }

    protected void Filter_Changed(object sender, EventArgs e)
    {
        loadListOrders();
    }

    protected void BtnNew_Click(object sender, EventArgs e)
    {
        try
        {
            if (checkAddNewFilters())
            {
                editOrder(0);
            }
        }
        catch (Exception e1) { LblErr.Text = RenderError(e1.Message); }
    }

    protected void BtnRow_Click(object sender, EventArgs e)
    {
        try
        {
            editRow(0);
        }
        catch (Exception e1) { LblErr.Text = RenderError(e1.Message); }
    }


    protected void Grid1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Select")
        {
            try
            {
                editOrder(int.Parse(e.CommandArgument.ToString()));
            }
            catch (Exception e1) { LblErr.Text = RenderError(e1.Message); }
        }
        if (e.CommandName == "DeleteRow")
        {
            deleteOrder(int.Parse(e.CommandArgument.ToString()));
            loadListOrders();
        }
    }

    protected void GridProducts_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        //Selected
        if (e.CommandName == "ImgEnabledOk")
        {
            setSelected(e.CommandArgument.ToString(), false);
            //loadListProducts();
        }
        if (e.CommandName == "ImgEnabledKo")
        {
            setSelected(e.CommandArgument.ToString(), true);
            //loadListProducts();
        }
        if (e.CommandName == "ShowThreads")
        {
            this.CurrentKey = e.CommandArgument.ToString();
            loadListProducts(Convert.ToInt32(this.CurrentKey));
            //Grid1.DataBind();
        }
        if (e.CommandName == "HideThreads")
        {
            this.CurrentKey = "0";
            loadListProducts();
            //Grid1.DataBind();
        }
    }

    protected void Grid1_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
            Utility.AddGlyph(Grid1, e.Row);
    }

    protected void GridProducts_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
            Utility.AddGlyph(Grid1, e.Row);
    }

    protected void GridProducts_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            var item = (ProductItem)e.Row.DataItem;

            var LitProduct = (Literal)e.Row.FindControl("LitProduct");
            LitProduct.Text += Utility.Html.GetTextPreview(item.Title, 30, "");
            LitProduct.Text += "<br> [" + item.SKU + "] <br>";
            LitProduct.Text += "[ ";
            foreach (var i in item.AttributeValues)
            {
                LitProduct.Text += i.Value + " ";
            }
            LitProduct.Text += "]";
            var LitQty = (Literal)e.Row.FindControl("LitQty");
            LitQty.Text = item.Availability.ToString();

            var LitPrice = (Literal)e.Row.FindControl("LitPrice");
            LitPrice.Text = ((item.SalePrice > 0 && item.SalePrice < item.RegularPrice) ? item.SalePrice : item.RegularPrice).ToString("0.##") + " " + new PigeonCms.Shop.Settings().CurrencyDefault.Symbol;

            int threadId = 0;
            int.TryParse(base.CurrentKey, out threadId);

            LinkButton LnkShowVariants = (LinkButton)e.Row.FindControl("LnkShowVariants");
            if (!item.IsDraft && item.ProductType == ProductItem.ProductTypeEnum.Configurable && item.IsThreadRoot && (bool)item.HasThreads)
            {
                if (threadId > 0)
                {
                    LnkShowVariants.Text = "<i class='fa fa-minus fa-fw'></i>";
                    LnkShowVariants.CommandName = "HideThreads";
                }
                else
                {
                    LnkShowVariants.Text = "<i class='fa fa-plus fa-fw'></i>";
                    LnkShowVariants.CommandName = "ShowThreads";
                }
            }
            else
            {
                LnkShowVariants.Visible = false;
            }
            
            if (this.CurrentRowId > 0)
            {
                var order = OrdMan.GetByKey(this.CurrentId);
                var row = order.Rows.Find(x => x.Id == this.CurrentRowId);

                if (item.SKU == row.ProductCode)
                {
                    var img1 = e.Row.FindControl("ImgEnabledOk");
                    img1.Visible = true;
                }
                else
                {
                    var img1 = e.Row.FindControl("ImgEnabledKo");
                    img1.Visible = true;
                }
            }
            else
            {
                var img1 = e.Row.FindControl("ImgEnabledKo");
                img1.Visible = true;
            }

        }
    }

    protected void Grid1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            var item = (PigeonCms.Shop.Order)e.Row.DataItem;

            var LnkOrderRef = (LinkButton)e.Row.FindControl("LnkOrderRef");
            LnkOrderRef.Text = "<i class='fa fa-pgn_edit fa-fw'></i>";
            LnkOrderRef.Text += Utility.Html.GetTextPreview(item.OrderRef, 30, "");
            if (Roles.IsUserInRole("admin"))
                LnkOrderRef.Text += " ["+ item.Id +"]";

            if (!string.IsNullOrEmpty(item.OwnerUser))
            {
                var LitOwnerUser = (Literal)e.Row.FindControl("LitOwnerUser");
                LitOwnerUser.Text = "<i class='fa fa-fw fa-user'></i>" + item.OwnerUser;
            }

            var LitOrderDate = (Literal)e.Row.FindControl("LitOrderDate");
            LitOrderDate.Text = item.OrderDate.ToShortDateString();

            var LitCustomerName = (Literal)e.Row.FindControl("LitCustomerName");
            LitCustomerName.Text = item.OrdName;

            var LitCustomerAddress = (Literal)e.Row.FindControl("LitCustomerAddress");
            LitCustomerAddress.Text = Utility.Html.GetTextPreview(item.OrdAddress, 30, "") + "<br>"
                + item.OrdZipCode + " " + item.OrdCity + " " + item.OrdState + "<br>"
                + item.OrdNation;

            var LitConfirmed = (Literal)e.Row.FindControl("LitConfirmed");
            LitConfirmed.Text = "<i class='glyphicon glyphicon-ok-circle " + getCssClass(item.Confirmed) + "'></i>";

            var LitPaid = (Literal)e.Row.FindControl("LitPaid");
            LitPaid.Text = "<i class='fa fa-money " + getCssClass(item.Paid) + "'></i>";

            var LitProcessed = (Literal)e.Row.FindControl("LitProcessed");
            LitProcessed.Text = "<i class='fa fa-database " + getCssClass(item.Processed) + "'></i>";

            var LitInvoiced = (Literal)e.Row.FindControl("LitInvoiced");
            LitInvoiced.Text = "<i class='fa fa-send " + getCssClass(item.Invoiced) + "'></i>";


            var LitSummary = (Literal)e.Row.FindControl("LitSummary");
            LitSummary.Text +=
                "Total amount: <strong>" + item.Currency + " " + item.TotalAmount.ToString("0.00") + "</strong><br>" +
                "Total paid: <strong>" + item.Currency + " " + item.TotalPaid.ToString("0.00") + "</strong><br>";

        }
    }

    protected void Grid1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        Grid1.PageIndex = e.NewPageIndex;
        loadListOrders();
    }



    protected void GridRows_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Select")
        {
            try
            {
                editRow(int.Parse(e.CommandArgument.ToString()));
            }
            catch (Exception e1) { LblErr.Text = RenderError(e1.Message); }
        }
        if (e.CommandName == "DeleteRow")
        {
            deleteRow(int.Parse(e.CommandArgument.ToString()));
        }
    }

    protected void GridRows_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
            Utility.AddGlyph(GridRows, e.Row);
    }

    
    protected void GridRows_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            var item = (PigeonCms.Shop.OrderRow)e.Row.DataItem;

            var ImgPreview = (Image)e.Row.FindControl("ImgPreview");
            ImgPreview.Visible = false;
            var product = new PigeonCms.Shop.ProductItemsManager().GetBySku(item.ProductCode);
            if (product.Id > 0)
            {
                var file = product.DefaultImage;
                ImgPreview.ImageUrl = PhotoManager.GetFileIconSrc(file, true);
                ImgPreview.Visible = true;
            }

            var LnkProduct = (LinkButton)e.Row.FindControl("LnkProduct");
            LnkProduct.Text = "<i class='fa fa-pgn_edit fa-fw'></i>";
            LnkProduct.Text += Utility.Html.GetTextPreview(product.Title, 30, "");

            var LitProductDetail = (Literal)e.Row.FindControl("LitProductDetail");
            LitProductDetail.Text = item.ProductCode;

            var LitQty = (Literal)e.Row.FindControl("LitQty");
            LitQty.Text = item.Qty.ToString();

            var LitPriceNet = (Literal)e.Row.FindControl("LitPriceNet");
            LitPriceNet.Text = item.PriceNet.ToString("0.00");

            var LitAmountNet = (Literal)e.Row.FindControl("LitAmountNet");
            LitAmountNet.Text = item.AmountNet.ToString("0.00");

            var LitNotes = (Literal)e.Row.FindControl("LitNotes");
            LitNotes.Text = Utility.Html.GetTextPreview(item.RowNotes, 50, "");
        }
    }


    protected void BtnSave_Click(object sender, EventArgs e)
    {
        if (checkForm())
        {
            if (saveForm())
                MultiView1.ActiveViewIndex = VIEW_GRID;
        }
    }

    protected void BtnCancel_Click(object sender, EventArgs e)
    {
        LblErr.Text = RenderError("");
        LblOk.Text = RenderSuccess("");
        MultiView1.ActiveViewIndex = VIEW_GRID;
    }

    protected void BtnSaveRow_Click(object sender, EventArgs e)
    {
        if (checkFormRow())
        {
            if (saveFormRow())
            {
                MultiView1.ActiveViewIndex = VIEW_INSERT;
                Utility.Script.RegisterStartupScript(Upd1, "changeTab", @"changeTab('tab-rows');");
            }
        }
    }
    protected void BtnCancelRow_Click(object sender, EventArgs e)
    {
        if (checkUndo())
        {
            LblErr.Text = RenderError("");
            LblOk.Text = RenderSuccess("");
            MultiView1.ActiveViewIndex = VIEW_INSERT;
            Utility.Script.RegisterStartupScript(Upd1, "changeTab", @"changeTab('tab-rows');");
        }
    }

    protected void MultiView1_ActiveViewChanged(object sender, EventArgs e)
    {
        if (MultiView1.ActiveViewIndex == VIEW_GRID)    //list view
        {
        }
    }

    #region private methods

    private bool checkUndo()
    {
        LblErr.Text = RenderError("");
        LblOk.Text = RenderSuccess("");
        bool res = true;
        string err = "";
        try
        {
            if (this.CurrentRowId > 0)
            {
                OrdMan.Rows_DeleteById(this.CurrentRowId);
            }
        }
        catch (Exception ex)
        {
            res = false;
            err = ex.ToString();
        }
        return res;
    }

    private bool checkForm()
    {
        LblErr.Text = RenderError("");
        LblOk.Text = RenderSuccess("");
        bool res = true;
        string err = "";
        
        if (string.IsNullOrEmpty(TxtOrderRef.Text))
        {
            res = false;
            err += "Missing order ref<br>";
        }

        if (!res)
            LblErr.Text = RenderError(err);

        return res;
    }

    private bool checkFormRow()
    {
        LblErr.Text = RenderError("");
        LblOk.Text = RenderSuccess("");
        bool res = true;
        string err = "";

        int qty = -1;
        int.TryParse(TxtProdQty.Text, out qty);
        if (qty <= 0)
        {
            res = false;
            err += "Quantity value not valid<br>";
        }

        if (!res)
            LblErr.Text = RenderError(err);

        return res;
    }

    private bool saveForm()
    {
        bool res = false;
        LblErr.Text = RenderError("");
        LblOk.Text = RenderSuccess("");

        try
        {
            var o1 = new PigeonCms.Shop.Order();

            if (CurrentId == 0)
            {
                form2obj(o1);
                o1 = OrdMan.Insert(o1);
            }
            else
            {
                o1 = OrdMan.GetByKey(CurrentId);
                form2obj(o1);
                OrdMan.Update(o1);
            }

            loadListOrders();
            LblOk.Text = RenderSuccess(Utility.GetLabel("RECORD_SAVED_MSG"));
            res = true;
        }
        catch (CustomException e1)
        {
            LblErr.Text = RenderError(e1.CustomMessage);
        }
        catch (Exception e1)
        {
            LblErr.Text = RenderError(Utility.GetLabel("RECORD_ERR_MSG") + "<br />");
            LogProvider.Write(this.BaseModule, "saveForm() err: " + e1.ToString(), TracerItemType.Error);
        }
        finally
        {
        }
        return res;
    }

    private bool saveFormRow()
    {
        bool res = false;
        LblErr.Text = RenderError("");
        LblOk.Text = RenderSuccess("");

        try
        {
            var o1 = new PigeonCms.Shop.OrderRow();

            if (CurrentRowId == 0)
            {
                form2objRow(o1);
                o1 = OrdMan.Rows_Insert(o1);
            }
            else
            {
                o1 = OrdMan.Rows_GetByKey(CurrentRowId);
                form2objRow(o1);
                OrdMan.Rows_Update(o1);
            }

            loadRows(o1.OrderId);
            LblOk.Text = RenderSuccess(Utility.GetLabel("RECORD_SAVED_MSG"));
            //Utility.Script.RegisterStartupScript(Upd1, "changeTab", @"changeTab('tab-rows');");
            res = true;
        }
        catch (CustomException e1)
        {
            LblErr.Text = RenderError(e1.CustomMessage);
        }
        catch (Exception e1)
        {
            LblErr.Text = RenderError(Utility.GetLabel("RECORD_ERR_MSG") + "<br />");
            LogProvider.Write(this.BaseModule, "saveFormRow() err: " + e1.ToString(), TracerItemType.Error);
        }
        finally
        {
        }
        return res;
    }

    private void clearForm()
    {
        LitCurrentOrder.Text = "";
        TxtOrderRef.Text = "";
        TxtOrderDate.Text = "";
        TxtOrderDateRequested.Text = "";
        TxtOrderDateShipped.Text = "";
        Utility.SetDropByValue(DropPaymentCode, "");
        ChkPaid.Checked = false;
        Utility.SetDropByValue(DropShipCode, "");

        TxtOrdName.Text = "";
        TxtOrdAddress.Text = "";
        TxtOrdZipCode.Text = "";
        TxtOrdCity.Text = "";
        TxtOrdState.Text = "";
        TxtOrdNation.Text = "";
        TxtOrdPhone.Text = "";
        TxtOrdEmail.Text = "";
        TxtShipName.Text = "";
        TxtShipAddress.Text = "";
        TxtShipZipCode.Text = "";
        TxtShipCity.Text = "";
        TxtShipState.Text = "";
        TxtShipNation.Text = "";
        TxtNotes.Text = "";
    }

    private void clearFormRow()
    {
        //LitCurrentOrder.Text = "";
        //TxtOrderRef.Text = "";
    }

    private void form2obj(PigeonCms.Shop.Order obj)
    {
        obj.Id = CurrentId;
        //obj.ItemParams = FormBuilder.GetParamsString(obj.ItemType.Params, ItemParams1);

        obj.OrderRef = TxtOrderRef.Text;
        obj.OrderDate = GetDate(TxtOrderDate);
        obj.OrderDateRequested = GetDate(TxtOrderDateRequested);
        obj.OrderDateShipped = GetDate(TxtOrderDateShipped);
        obj.PaymentCode = DropPaymentCode.SelectedValue;
        obj.Paid = ChkPaid.Checked;
        // ChkOrderShipped.Checked = false;

         obj.OrdName = TxtOrdName.Text;
         obj.OrdAddress = TxtOrdAddress.Text;
         obj.OrdZipCode = TxtOrdZipCode.Text;
         obj.OrdCity = TxtOrdCity.Text;
         obj.OrdState = TxtOrdState.Text;
         obj.OrdNation = TxtOrdNation.Text;
         obj.OrdPhone = TxtOrdPhone.Text;
         obj.OrdEmail = TxtOrdEmail.Text;
         obj.Notes = TxtNotes.Text;

         obj.ShipName = TxtShipName.Text;
         obj.ShipAddress = TxtShipAddress.Text;
         obj.ShipZipCode = TxtShipZipCode.Text;
         obj.ShipCity = TxtShipCity.Text;
         obj.ShipState = TxtShipState.Text;
         obj.ShipNation = TxtShipNation.Text;

    }

    private void form2objRow(PigeonCms.Shop.OrderRow obj)
    {
        obj.Id = CurrentRowId;
        obj.OrderId = CurrentId;
        decimal qty = 0;
        decimal.TryParse(TxtProdQty.Text, out qty);
        //obj.ProductCode = TxtProductCode.Text;
        var item = new ProductItemsManager().GetBySku(obj.ProductCode);
        if (item.Availability < qty)
        {
            throw new Exception("not in stock");
        }
        obj.Qty = qty;
        obj.PriceNet = (item.SalePrice > 0 && item.SalePrice < item.RegularPrice) ? item.SalePrice : item.RegularPrice;
        
    }


    private void obj2form(PigeonCms.Shop.Order obj)
    {
        if (obj.Id == 0)
            LitCurrentOrder.Text = base.GetLabel("NewOrder", "New order");
        else
        {
            LitCurrentOrder.Text = base.GetLabel("EditOrder", "Edit order")
                + " " + obj.OrderRef + " - " + obj.OrderDate.ToShortDateString()
                + " - " + obj.OrdName;
        }

        TxtOrderRef.Text = obj.OrderRef;
        SetDate(TxtOrderDate, obj.OrderDate);
        SetDate(TxtOrderDateRequested, obj.OrderDateRequested);
        SetDate(TxtOrderDateShipped, obj.OrderDateShipped);
        Utility.SetDropByValue(DropPaymentCode, obj.PaymentCode);
        if (string.IsNullOrEmpty(obj.PaymentCode))
        {
            DropPaymentCode.Enabled = true;
        }
        else
        {
            DropPaymentCode.Enabled = false;
        }
        ChkPaid.Checked = obj.Paid;
        Utility.SetDropByValue(DropShipCode, obj.ShipCode);
        if (string.IsNullOrEmpty(obj.ShipCode))
        {
            DropShipCode.Enabled = true;
        }
        else
        {
            DropShipCode.Enabled = false;
        }
        TxtOrdName.Text = obj.OrdName;
        TxtOrdAddress.Text = obj.OrdAddress;
        TxtOrdZipCode.Text = obj.OrdZipCode;
        TxtOrdCity.Text = obj.OrdCity;
        TxtOrdState.Text = obj.OrdState;
        TxtOrdNation.Text = obj.OrdNation;
        TxtOrdPhone.Text = obj.OrdPhone;
        TxtOrdEmail.Text = obj.OrdEmail;
        TxtNotes.Text = obj.Notes;

        TxtShipName.Text = obj.ShipName;
        TxtShipAddress.Text = obj.ShipAddress;
        TxtShipZipCode.Text = obj.ShipZipCode;
        TxtShipCity.Text = obj.ShipCity;
        TxtShipState.Text = obj.ShipState;
        TxtShipNation.Text = obj.ShipNation;

        loadRows(this.CurrentId);
    }

    private void obj2formRow(PigeonCms.Shop.OrderRow obj)
    {
        //ProductItem item = new ProductItemsManager().GetBySku(obj.ProductCode);
        //if (item.Id == 0)
        //    TxtProductCode.Enabled = true;
        //TxtProductCode.Text = item.SKU;
        TxtProdQty.Text = obj.Qty.ToString();
    }

    private void editOrder(int recordId)
    {
        var obj = new PigeonCms.Shop.Order();
        LblOk.Text = RenderSuccess("");
        LblErr.Text = RenderError("");

        if (!PgnUserCurrent.IsAuthenticated)
            throw new Exception("user not authenticated");

        clearForm();
        this.CurrentId = recordId;
        if (CurrentId == 0)
        {
            obj2form(obj);
        }
        else
        {
            obj = OrdMan.GetByKey(CurrentId);
            obj2form(obj);
        }
        MultiView1.ActiveViewIndex = VIEW_INSERT;
    }

    private void editRow(int recordId)
    {
        var obj = new PigeonCms.Shop.OrderRow();
        LblOk.Text = RenderSuccess("");
        LblErr.Text = RenderError("");


        clearFormRow();
        this.CurrentRowId = recordId;
        loadListProducts();
        if (CurrentRowId == 0)
        {
            obj2formRow(obj);
        }
        else
        {
            obj = OrdMan.Rows_GetByKey(CurrentRowId);
            obj2formRow(obj);
        }
        MultiView1.ActiveViewIndex = VIEW_ROWS;
    }

    private void deleteOrder(int recordId)
    {
        LblOk.Text = RenderSuccess("");
        LblErr.Text = RenderError("");

        try
        {
            if (!PgnUserCurrent.IsAuthenticated)
                throw new Exception("user not authenticated");

            OrdMan.DeleteById(recordId);
        }
        catch (Exception e)
        {
            LblErr.Text = RenderError(e.Message);
        }
    }


    private void deleteRow(int recordId)
    {
        LblOk.Text = RenderSuccess("");
        LblErr.Text = RenderError("");

        try
        {
            OrdMan.Rows_DeleteById(recordId);
        }
        catch (Exception e)
        {
            LblErr.Text = RenderError(e.Message);
        }
        loadRows(this.CurrentId);
        Utility.Script.RegisterStartupScript(Upd1, "changeTab", @"changeTab('tab-rows');");
    }

    private void loadDropOrderDatesRangeFilter()
    {
        DropOrderDatesRangeFilter.Items.Clear();
        DropOrderDatesRangeFilter.Items.Add(new ListItem(base.GetLabel("DateToday", "Today"), "3"));
        DropOrderDatesRangeFilter.Items.Add(new ListItem(base.GetLabel("DateAlways", "Always"), "2"));
        DropOrderDatesRangeFilter.Items.Add(new ListItem(base.GetLabel("DateLastWeek", "Last week"), "4"));
        DropOrderDatesRangeFilter.Items.Add(new ListItem(base.GetLabel("DateLastMonth", "Last month"), "5"));

        Utility.SetDropByValue(DropOrderDatesRangeFilter, ((int)DatesRange.RangeType.LastMonth).ToString());

    }

    private void loadDropPaymentFilter(Utility.TristateBool forcedValue)
    {
        DropPaymentFilter.Items.Clear();
        DropPaymentFilter.Items.Add(new ListItem(base.GetLabel("NotSet", "Not set"), "2"));
        DropPaymentFilter.Items.Add(new ListItem(base.GetLabel("Done", "Done"), "1"));
        DropPaymentFilter.Items.Add(new ListItem(base.GetLabel("Pending", "Pending"), "0"));

        if (forcedValue != Utility.TristateBool.NotSet)
        {
            DropPaymentFilter.Enabled = false;
            Utility.SetDropByValue(DropPaymentFilter, ((int)forcedValue).ToString());
        }
    }

    private void loadDropConfirmedFilter(Utility.TristateBool forcedValue)
    {
        DropConfirmedFilter.Items.Clear();
        DropConfirmedFilter.Items.Add(new ListItem(base.GetLabel("NotSet", "Not set"), "2"));
        DropConfirmedFilter.Items.Add(new ListItem(base.GetLabel("Yes", "Yes"), "1"));
        DropConfirmedFilter.Items.Add(new ListItem(base.GetLabel("No", "No"), "0"));

        if (forcedValue != Utility.TristateBool.NotSet)
        {
            DropConfirmedFilter.Enabled = false;
            Utility.SetDropByValue(DropConfirmedFilter, ((int)forcedValue).ToString());
        }
    }

    private bool checkAddNewFilters()
    {
        bool res = true;
        return res;
    }

    private string getCssClass(bool value)
    {
        string res = "text-muted opacity-2";
        if (value)
            res = "text-primary";

        return res;
    }

    private void loadRows(int orderId)
    {
        RowsFilter.Reset();
        RowsFilter.OrderId = orderId;


        var list = OrdMan.Rows_GetByFilter(RowsFilter, "");
        GridRows.DataSource = list;
        GridRows.DataBind();
    }

    private void loadListOrders()
    {
        OrdFilter.Reset();

        //*** security check to avoid html hack ***

        if (DropOrderDatesRangeFilter.SelectedValue != "")
        {
            var rangeType = (DatesRange.RangeType)int.Parse(DropOrderDatesRangeFilter.SelectedValue);
            DatesRange datesRange = new DatesRange(rangeType);
            OrdFilter.OrderDatesRange = datesRange;
        }

        if (this.OrderConfirmedFilter != Utility.TristateBool.NotSet)
            OrdFilter.Confirmed = this.OrderConfirmedFilter;
        else
            OrdFilter.Confirmed = (Utility.TristateBool)int.Parse(DropConfirmedFilter.SelectedValue);

        if (this.PaymentFilter != Utility.TristateBool.NotSet)
            OrdFilter.Paid = this.PaymentFilter;
        else
            OrdFilter.Paid = (Utility.TristateBool)int.Parse(DropPaymentFilter.SelectedValue);

        if (!string.IsNullOrEmpty(this.OwnerUser))
            OrdFilter.OwnerUser = this.OwnerUser;
        else
            OrdFilter.OwnerUser = TxtOwnerUserFilter.Text;


        var list = OrdMan.GetByFilter(OrdFilter, "");
        Grid1.DataSource = list;
        Grid1.DataBind();

        int ordersCount = 0;
        int ordersToShip = 0;
        decimal ordersTotalAmount = 0;
        decimal ordersTotalPaid = 0;
        foreach (var item in list)
        {
            ordersCount++;
            ordersTotalAmount += item.TotalAmount;
            ordersTotalPaid += item.TotalPaid;
            if (item.OrderDateShipped != DateTime.MinValue)
                ordersToShip++;
        }
        LitBoardOrdersCount.Text = ordersCount.ToString();
        LitBoardOrdersTotalAmount.Text = ordersTotalAmount.ToString("0.00");
        LitBoardOrdersTotalPaid.Text = ordersTotalPaid.ToString("0.00");
        LitBoardOrdersToShip.Text = ordersToShip.ToString();
    }

    private void loadListProducts(int thread = 0)
    {

        var rows = OrdMan.GetByKey(this.CurrentId).Rows;
        var codes = new List<string>();
        foreach (var row in rows)
        {
            if(this.CurrentRowId != row.Id)
                codes.Add(row.ProductCode);
        }
        var f = new ProductItemFilter();
        var items = new List<ProductItem>();
        if (thread == 0)
        {
            items = new ProductItemsManager().GetByFilter(f, "");
        }
        else
        {
            f.ShowOnlyRootItems = false;
            f.ThreadId = thread;
            items = new ProductItemsManager().GetByFilter(f, "");
        }
        var itemsFiltered = items.Where(i => !codes.Any(c => c == i.SKU)).ToList();

        GridProducts.DataSource = itemsFiltered;
        GridProducts.DataBind();

    }

    private void setSelected(string productCode, bool enable)
    {
        var order = OrdMan.GetByKey(this.CurrentId);
        var row = order.Rows.Find(x => x.Id == this.CurrentRowId);
        if (row == null)
        {
            row = new OrderRow();
            row.OrderId = order.Id;
            row.ProductCode = productCode;
            this.CurrentRowId = OrdMan.Rows_Insert(row).Id;
        } 
        else 
        {
            row.ProductCode = productCode;
            OrdMan.Rows_Update(row);
        }
        if (this.CurrentKey == "")
            this.CurrentKey = "0";
        loadListProducts(Convert.ToInt32(this.CurrentKey));
    }

    private void loadDropPayments() 
    {
        DropPaymentCode.Items.Clear();
        DropPaymentCode.Items.Add(new ListItem(base.GetLabel("SelectPaymentMode", "Select Payment Mode"), "0"));
        var payments = new PigeonCms.Shop.PaymentsManager().GetByFilter(new PigeonCms.Shop.PaymentsFilter(), "");
        foreach (var p in payments)
        {
            DropPaymentCode.Items.Add(new ListItem(p.Name, p.PayCode));
        }
    }

    private void loadDropShipments()
    {
        DropShipCode.Items.Clear();
        DropShipCode.Items.Add(new ListItem(base.GetLabel("SelectPaymentMode", "Select Payment Mode"), "0"));
        var shipments = new PigeonCms.Shop.ShipmentsManager().GetByFilter(new PigeonCms.Shop.ShipmentFilter(), "");
        foreach (var s in shipments)
        {
            DropShipCode.Items.Add(new ListItem(s.Name, s.ShipCode));
        }
    }


    #endregion

}
