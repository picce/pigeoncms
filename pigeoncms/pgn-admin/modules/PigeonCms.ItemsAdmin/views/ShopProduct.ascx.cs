using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Linq;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Web.Caching;
using System.Collections.Generic;
using System.Globalization;
using PigeonCms;
using PigeonCms.Core.Helpers;
using PigeonCms.Shop;

public partial class Controls_ShopProduct : PigeonCms.ItemsAdminControl
{
    const int COL_ALIAS_INDEX = 2;
    const int COL_CATEGORY_INDEX = 4;
    const int COL_ACCESSTYPE_INDEX = 5;
    const int COL_ORDER_ARROWS_INDEX = 7;
    const int COL_UPLOADFILES_INDEX = 8;
    const int COL_UPLOADIMAGES_INDEX = 9;
    const int COL_DELETE_INDEX = 10;

    const int VIEW_GRID = 0;
    const int VIEW_INSERT = 1;

    const int PRODUCT_SECTION = 6;

    protected PigeonCms.Shop.Settings ShopSettings = new PigeonCms.Shop.Settings();

    ItemAttributesValuesManager man = new ItemAttributesValuesManager();
    AttributeValuesManager vman = new AttributeValuesManager();
    AttributesManager aman = new AttributesManager();
    AttributeSetsManager sman = new AttributeSetsManager();
    List<PigeonCms.Attribute> attributes = new List<PigeonCms.Attribute>();

    protected DateTime ItemDate
    {
        get
        {
            CultureInfo culture;
            DateTimeStyles styles;
            culture = CultureInfo.CreateSpecificCulture("it-IT");
            styles = DateTimeStyles.None;
            DateTime res;
            DateTime.TryParse(TxtItemDate.Text, culture, styles, out res);
            return res.Date;
            
        }
        set
        {
            TxtItemDate.Text = "";
            if (value != DateTime.MinValue)
                TxtItemDate.Text = value.ToShortDateString();
        }
    }

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
        ContentEditorProvider.InitEditor(this, Upd1, base.ContentEditorConfig);

        string titleId = "";
        foreach (KeyValuePair<string, string> item in Config.CultureList)
        {
            //title
            Panel pan1 = new Panel();
            pan1.CssClass = "form-group input-group";

            TextBox txt1 = new TextBox();
            txt1.ID = "TxtTitle" + item.Value;
            txt1.MaxLength = 200;
            txt1.CssClass = "form-control";
            txt1.ToolTip = item.Key;
            txt1.ClientIDMode = System.Web.UI.ClientIDMode.Static;
            LabelsProvider.SetLocalizedControlVisibility(this.ShowOnlyDefaultCulture, item.Key, txt1);
            pan1.Controls.Add(txt1);
            Literal lit1 = new Literal();
            if (!this.ShowOnlyDefaultCulture)
                lit1.Text = "<span class='input-group-addon'>" + item.Value + "</span>";
            pan1.Controls.Add(lit1);
            if (item.Key == Config.CultureDefault)
                titleId = txt1.ClientID;
            PanelTitle.Controls.Add(pan1);

            //description
            var txt2 = (Controls_ContentEditorControl)LoadControl("~/Controls/ContentEditorControl.ascx");
            txt2.ID = "TxtDescription" + item.Value;
            //txt2.CssClass = "adminMediumText";
            txt2.Configuration = base.ContentEditorConfig;
            LabelsProvider.SetLocalizedControlVisibility(this.ShowOnlyDefaultCulture, item.Key, txt2);
            PanelDescription.Controls.Add(txt2);

            Literal lit2 = new Literal();
            if (!this.ShowOnlyDefaultCulture)
                lit2.Text = "&nbsp;[<i>" + item.Value + "</i>]<br /><br />";
            PanelDescription.Controls.Add(lit2);
        }

        if (IsPostBack)
        {
            int setId = 0;
            int.TryParse(DropSets.SelectedValue, out setId);
            var afilter = new AttributeFilter();
            if (setId > 0)
            {
                var set = sman.GetByKey(setId);
                foreach (var attributeId in set.AttributesList)
                {
                    attributes.Add(aman.GetByKey(attributeId));
                }
            }
            else
            {
                attributes = aman.GetByFilter(afilter, "");
            }
            PanelAttributes.Controls.Add(generateAttributesDropDown());
            PanelAttributes.Controls.Add(generateAttributesTextBox());
            QuickAttributes.Controls.Add(generateAttributesDropDown(true));
            QuickAttributes.Controls.Add(generateAttributesTextBox(true));
        }

        if (this.BaseModule.DirectEditMode)
        {
        }

        TxtAlias.Attributes.Add("onfocus", "preloadAlias('" + titleId + "', this)");

        //restrictions
        Grid1.AllowSorting = false;
        Grid1.Columns[COL_ORDER_ARROWS_INDEX].Visible = this.AllowOrdering;
        Grid1.Columns[COL_UPLOADFILES_INDEX].Visible = this.TargetFilesUpload > 0;
        Grid1.Columns[COL_UPLOADIMAGES_INDEX].Visible = this.TargetImagesUpload > 0;
        Grid1.Columns[COL_DELETE_INDEX].Visible = base.AllowDelete;

        Grid1.Columns[COL_ALIAS_INDEX].Visible = this.ShowAlias;
        TxtAlias.Visible = this.ShowAlias;

        Grid1.Columns[COL_ACCESSTYPE_INDEX].Visible = this.ShowSecurity;
        PermissionsControl1.Visible = this.ShowSecurity;

        TxtItemDate.Visible = this.ShowDates;
        TxtValidFrom.Visible = this.ShowDates;
        TxtValidTo.Visible = this.ShowDates;

        DropEnabledFilter.Visible = this.ShowEnabledFilter;
        ItemFields1.Visible = this.ShowFieldsPanel;
        ItemParams1.Visible = this.ShowParamsPanel;

        ItemFields1.Title = base.GetLabel("LblFields", "Fields", null, true);
        ItemParams1.Title = base.GetLabel("LblParameters", "Parameters", null, true);

    }

    protected void Page_Load(object sender, EventArgs e)
    {
        LblOk.Text = RenderSuccess("");
        LblErr.Text = RenderError("");

        if (this.BaseModule.DirectEditMode)
        {
            if (base.CurrItem.Id == 0)
                throw new ArgumentException();
            if (new ProductItemsManager(true, true).GetByKey(base.CurrItem.Id).Id == 0)
                throw new ArgumentException();
        }

        QuickTxtWeight.Attributes.Add("onclick", "autosuggest(" + TxtWeight.ClientID + ", this )");
        QuickTxtRegularPrice.Attributes.Add("onclick", "autosuggest(" + TxtRegularPrice.ClientID + ", this )");
        QuickTxtSalePrice.Attributes.Add("onclick", "autosuggest(" + TxtSalePrice.ClientID + ", this )");
        QuickTxtQty.Attributes.Add("onclick", "autosuggest(" + TxtQty.ClientID + ", this )");

        if (!Page.IsPostBack)
        {
            loadDropEnabledFilter();
            loadDropCategoriesFilter(PRODUCT_SECTION); // TOTO with settings
            loadDropsItemTypes();
            loadDropSets();
            loadDropProductType();
        }
        else
        {
            string eventArg = HttpContext.Current.Request["__EVENTARGUMENT"];
            if (eventArg == "items")
                Grid1.DataBind();

            //reload params on every postback, because cannot manage dinamically fields
            var currentItem = new ProductItem();
            if (CurrentId > 0)
            {
                currentItem = new ProductItemsManager(true, true).GetByKey(CurrentId);
                ItemParams1.LoadParams(currentItem);
                ItemFields1.LoadFields(currentItem);
            }
            else
            {
                //manually set ItemType
                try
                {
                    currentItem.ItemTypeName = LitItemType.Text;
                    ItemParams1.LoadParams(currentItem);
                    ItemFields1.LoadFields(currentItem);
                }
                catch { }
            }
        }
        if (this.BaseModule.DirectEditMode)
        {
            DropNew.Visible = false;
            BtnNew.Visible = false;
            BtnCancel.OnClientClick = "closePopup();";

            editRow(base.CurrItem.Id);
        }
    }

    protected void DropEnabledFilter_SelectedIndexChanged(object sender, EventArgs e)
    {
        Grid1.DataBind();
    }

    protected void DropCategoriesFilter_SelectedIndexChanged(object sender, EventArgs e)
    {
        int catID = 0;
        int.TryParse(DropCategoriesFilter.SelectedValue, out catID);

        Grid1.DataBind();
        base.LastSelectedCategoryId = catID;
    }

    protected void DropItemTypesFilter_SelectedIndexChanged(object sender, EventArgs e)
    {
        Grid1.DataBind();
    }

    protected void DropNew_SelectedIndexChanged(object sender, EventArgs e)
    {
        try { editRow(0); }
        catch (Exception e1) { LblErr.Text = RenderError(e1.Message); }
    }

    protected void DropSets_SelectedIndexChanged(object sender, EventArgs e)
    {
        string strIdSet = DropSets.SelectedValue;
        int id = 0;
        int.TryParse(strIdSet, out id);

        hideAttributes(id);
        hideAttributes(id, true);
        GridViewSimple.DataBind();
    }


    protected void DropProductTypeFilter_SelectedIndexChanged(object sender, EventArgs e)
    {
        Grid1.DataBind();
    }

    protected void BtnNew_Click(object sender, EventArgs e)
    {
        try
        {
            Utility.SetDropByValue(DropNew, this.CurrItem.CustomInt2.ToString());
            editRow(0);
        }
        catch (Exception e1) { LblErr.Text = RenderError(e1.Message); }
    }

    protected void ObjDs1_ObjectCreating(object sender, ObjectDataSourceEventArgs e)
    {
        var typename = new ProductItemsManager(true, true);
        e.ObjectInstance = typename;
    }

    protected void ObjDs2_ObjectCreating(object sender, ObjectDataSourceEventArgs e)
    {
        var typename = new ProductItemsManager(true, true);
        e.ObjectInstance = typename;
    }

    protected void ObjDs3_ObjectCreating(object sender, ObjectDataSourceEventArgs e)
    {
        var typename = new ProductItemsManager();
        e.ObjectInstance = typename;
    }

    protected void ObjDs1_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
    {
        //see http://msdn.microsoft.com/en-us/library/w3f99sx1.aspx
        //for use generics with ObjDs TypeName
        var filter = new ProductItemFilter();

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

        int threadId = 0;
        int.TryParse(base.CurrentKey, out threadId);

        if (threadId > 0)
        {
            filter.ShowOnlyRootItems = false;
            filter.ThreadId = threadId;
        }

        int catId = -1;
        int.TryParse(DropCategoriesFilter.SelectedValue, out catId);

        int prodType = -1;
        int.TryParse(DropProductTypeFilter.SelectedValue, out prodType);

        if (base.SectionId > 0)
            filter.SectionId = base.SectionId;
        else
            filter.SectionId = PRODUCT_SECTION; //TODO with settings;
        filter.CategoryId = catId;

        if (prodType > 0)
            filter.ProductType = prodType;

        e.InputParameters["filter"] = filter;
        e.InputParameters["sort"] = "";
    }

    protected void ObjDs2_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
    {
        //see http://msdn.microsoft.com/en-us/library/w3f99sx1.aspx
        //for use generics with ObjDs TypeName
        var filter = new ProductItemFilter();

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

        filter.ShowOnlyRootItems = false;

        var main = new ProductItemsManager(true, true).GetByKey(base.CurrentId);
        int setDrop = 0;
        int.TryParse(DropSets.SelectedValue, out setDrop);
        int setId = (main.AttributeSet > 0) ? main.AttributeSet : setDrop;

        filter.ProductType = (int)ProductItem.ProductTypeEnum.Simple;
        filter.AttributeSet = setId;

        e.InputParameters["filter"] = filter;
        e.InputParameters["sort"] = "";
    }

    protected void ObjDs3_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
    {
        //see http://msdn.microsoft.com/en-us/library/w3f99sx1.aspx
        //for use generics with ObjDs TypeName
        var filter = new ProductItemFilter();

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

        filter.ShowOnlyRootItems = true;

        e.InputParameters["filter"] = filter;
        e.InputParameters["sort"] = "";
    }

    protected void Grid1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Select")
        {
            try
            {
                editRow(int.Parse(e.CommandArgument.ToString()));
            }
            catch (Exception e1) { LblErr.Text = RenderError(e1.Message); }
        }
        if (e.CommandName == "ShowThreads")
        {
            showThreads(e.CommandArgument.ToString(), true);
            Grid1.DataBind();
        }
        if (e.CommandName == "HideThreads")
        {
            showThreads(e.CommandArgument.ToString(), false);
            Grid1.DataBind();
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
        //Ordering
        if (e.CommandName == "MoveDown")
        {
            moveRecord(int.Parse(e.CommandArgument.ToString()), Database.MoveRecordDirection.Down);
        }
        if (e.CommandName == "MoveUp")
        {
            moveRecord(int.Parse(e.CommandArgument.ToString()), Database.MoveRecordDirection.Up);
        }
    }

    protected void Grid1_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
            Utility.AddGlyph(Grid1, e.Row);
    }

    GridViewRow lastRowDataboundRoot;

    protected void Grid1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            PigeonCms.Shop.ProductItem item = new PigeonCms.Shop.ProductItem();
            item = (PigeonCms.Shop.ProductItem)e.Row.DataItem;

            //item.IsThreadRoot = true;

            if (item.IsThreadRoot)
                lastRowDataboundRoot = e.Row;
            else
            {
                if (lastRowDataboundRoot != null)
                    e.Row.RowState = lastRowDataboundRoot.RowState; //keeps same style of thread root
            }

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

            var LnkTitle = (LinkButton)e.Row.FindControl("LnkTitle");
            LnkTitle.Text = "<i class='fa fa-pgn_edit fa-fw'></i>";
            if (!item.IsThreadRoot)
                LnkTitle.Text += "--";
            LnkTitle.Text += Utility.Html.GetTextPreview(item.Title, 50, "");
            if (string.IsNullOrEmpty(item.Title))
                LnkTitle.Text += Utility.GetLabel("NO_VALUE", "<no value>");

            if (base.ShowSecurity && Roles.IsUserInRole("debug"))
                LnkTitle.Text += " [" + item.Id.ToString() + "]";

            var LitItemInfo = (Literal)e.Row.FindControl("LitItemInfo");
            if (this.ShowType)
                LitItemInfo.Text += item.ProductType.ToString() +"<br>";
            if (!string.IsNullOrEmpty(item.CssClass))
                LitItemInfo.Text += "class: " + item.CssClass +"<br>";
            if (item.ThreadItems.Count > 0)
                LitItemInfo.Text += "variants compiled: " + item.ThreadItems.Count;


            if (item.CategoryId > 0)
            {
                var mgr = new CategoriesManager();
                var cat = mgr.GetByKey(item.CategoryId);
                var LitCategoryTitle = (Literal)e.Row.FindControl("LitCategoryTitle");
                LitCategoryTitle.Text = cat.Title;
            }

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

            //permissions
            var LitAccessTypeDesc = (Literal)e.Row.FindControl("LitAccessTypeDesc");
            LitAccessTypeDesc.Text = RenderAccessTypeSummary(item);

            //files upload
            var LnkUploadFiles = (HyperLink)e.Row.FindControl("LnkUploadFiles");
            LnkUploadFiles.NavigateUrl = this.FilesUploadUrl
                + "?type=items&id=" + item.Id.ToString();
            LnkUploadFiles.Visible = this.TargetFilesUpload > 0;
            if (this.IsMobileDevice == false)
                LnkUploadFiles.CssClass = "fancyRefresh";
            var LitFilesCount = (Literal)e.Row.FindControl("LitFilesCount");
            int filesCount = item.Files.Count;
            if (filesCount > 0)
            {
                LitFilesCount.Text = filesCount.ToString();
                LitFilesCount.Text += filesCount == 1 ? " file" : " files";
                LitFilesCount.Text += "<br />(" + Utility.GetFileHumanLength(item.FilesSize) + ")";
            }

            //images upload
            var LnkUploadImg = (HyperLink)e.Row.FindControl("LnkUploadImg");
            LnkUploadImg.NavigateUrl = this.ImagesUploadUrl
                + "?type=items&id=" + item.Id.ToString();
            LnkUploadImg.Visible = this.TargetImagesUpload > 0;
            if (this.IsMobileDevice == false)
                LnkUploadImg.CssClass = "fancyRefresh";
            var LitImgCount = (Literal)e.Row.FindControl("LitImgCount");
            int imgCount = item.Images.Count;
            if (imgCount > 0)
            {
                LitImgCount.Text = imgCount.ToString();
                LitImgCount.Text += imgCount == 1 ? " file" : " files";
                LitImgCount.Text += "<br />(" + Utility.GetFileHumanLength(item.ImagesSize) + ")";
            }

        }

    }

    protected void GridViewSimple_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            var item = new ProductItem();
            item = (ProductItem)e.Row.DataItem;

            if (item.IsThreadRoot)
                lastRowDataboundRoot = e.Row;
            else
            {
                if (lastRowDataboundRoot != null)
                    e.Row.RowState = lastRowDataboundRoot.RowState; //keeps same style of thread root
            }

            CheckBox chkRow = (CheckBox)e.Row.FindControl("chkRow");
            if (item.ThreadId == this.CurrentId)
            {
                chkRow.Checked = true;
            }
        }

    }

    protected void GridRelated_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            var item = new ProductItem();
            item = (ProductItem)e.Row.DataItem;

            if (item.IsThreadRoot)
                lastRowDataboundRoot = e.Row;
            else
            {
                if (lastRowDataboundRoot != null)
                    e.Row.RowState = lastRowDataboundRoot.RowState; //keeps same style of thread root
            }

            if (item.Id == base.CurrentId)
            {
                e.Row.Visible = false;
            }

            var pman = new ProductItemsManager();
            var relatedIds = pman.GetRelatedItems(CurrentId).Select(x => x.Id);

            CheckBox chkRow = (CheckBox)e.Row.FindControl("chkRow");
            if (relatedIds.Contains(item.Id))
            {
                chkRow.Checked = true;
            }

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

    protected void BtnQuickAdd_Click(object sender, EventArgs e)
    {
        if (checkQuickAdd())
        {
            if (saveQuickAdd())
            {
                MultiView1.ActiveViewIndex = VIEW_INSERT;
                Utility.Script.RegisterStartupScript(Upd1, "changeTab", @"changeTab('tab-associated');");
            }
        }
    }

    protected void BtnCancel_Click(object sender, EventArgs e)
    {
        if (new ProductItemsManager(true, true).GetByKey(this.CurrentId).IsDraft)
        {
            new ProductItemsManager(true, true).DeleteById(this.CurrentId);
        }
        LblErr.Text = RenderError("");
        LblOk.Text = RenderSuccess("");
        MultiView1.ActiveViewIndex = VIEW_GRID;
    }

    protected void MultiView1_ActiveViewChanged(object sender, EventArgs e)
    {
        if (this.BaseModule.DirectEditMode)
        {
            //list view not allowed (in case of js hacking)
            if (MultiView1.ActiveViewIndex == VIEW_GRID)
                MultiView1.ActiveViewIndex = VIEW_INSERT;
        }

        if (MultiView1.ActiveViewIndex == VIEW_GRID)    //list view
        {
            Utility.SetDropByValue(DropNew, "");
        }
    }

    #region private methods

    private bool checkForm()
    {
        LblErr.Text = RenderError("");
        LblOk.Text = RenderSuccess("");
        bool res = true;
        string err = "";

        if (!string.IsNullOrEmpty(TxtAlias.Text))
        {
            var filter = new ProductItemFilter();
            var list = new List<PigeonCms.Shop.ProductItem>();

            filter.Alias = TxtAlias.Text;
            list = new ProductItemsManager().GetByFilter(filter, "");
            if (list.Count > 0)
            {
                if (this.CurrentId == 0)
                {
                    res = false;
                    err += "alias in use<br />";
                }
                else
                {
                    if (list[0].Id != this.CurrentId && this.CurrentId == list[0].Id)
                    {
                        res = false;
                        err += "alias in use<br />";
                    }
                }
            }
        }
        LblErr.Text = RenderError(err);
        return res;
    }

    private bool checkQuickAdd()
    {
        LblErr.Text = RenderError("");
        LblOk.Text = RenderSuccess("");
        bool res = true;
        string err = "";
        var pfilter = new ProductItemFilter();
        var filter = new ItemAttributeValueFilter();
        pfilter.ThreadId = CurrentId;
        pfilter.ShowOnlyRootItems = false;
        var pChilds = new ProductItemsManager(true, true).GetByFilter(pfilter, "");
        foreach (var pChild in pChilds)
        {
            int pId = pChild.Id;
            filter.ItemId = pId;
            var values = man.GetByFilter(filter, "");
            int foundIn = 0,
                presentAttributes = 0;

            // QUI salvo gli attributi quick edit

            foreach (var attribute in attributes)
            {
                if (!attribute.AllowCustomValue)
                {
                    DropDownList d1 = new DropDownList();
                    d1 = (DropDownList)PanelAttributes.FindControl("DropAttributeValuesQuick" + attribute.Name);
                    int attributeValueId = 0;
                    int.TryParse(d1.SelectedValue, out attributeValueId);
                    if (attributeValueId > 0)
                    {
                        presentAttributes++;
                        var exist = values.Exists(x => x.AttributeId == attribute.Id && x.AttributeValueId == attributeValueId);
                        if (exist)
                        {
                            foundIn++;
                        }
                    }
                }
            }
            if (foundIn == presentAttributes)
            {
                res = false;
                err += "Existing variant<br />";
            }
        }
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
            var o1 = new ProductItemsManager().GetByKey(CurrentId);  //precarico i campi esistenti e nn gestiti dal form
            List<ItemAttributeValue> a1 = null;

            a1 = form2obj(o1);
            new ProductItemsManager().Update(o1);

            man.DeleteByItemId(CurrentId);

            foreach (var a in a1)
            {
                man.Insert(a);
            }

            removeFromCache();

            Grid1.DataBind();
            GridViewSimple.DataBind();
            GridRelated.DataBind();
            LblOk.Text = RenderSuccess(Utility.GetLabel("RECORD_SAVED_MSG"));
            res = true;
        }
        catch (CustomException e1)
        {
            if (e1.CustomMessage == ProductItemsManager.MaxItemsException)
                LblErr.Text = RenderError(base.GetLabel("LblMaxItemsReached", "you have reached the maximum number of elements"));
            else
                LblErr.Text = RenderError(e1.CustomMessage);
        }
        catch (Exception e1)
        {
            LblErr.Text = RenderError(Utility.GetLabel("RECORD_ERR_MSG") + "<br />" + e1.ToString());
        }
        finally
        {
        }
        return res;
    }

    private bool saveQuickAdd()
    {
        bool res = false;
        LblErr.Text = RenderError("");
        LblOk.Text = RenderSuccess("");
        try
        {
            var o1 = new ProductItem();  //precarico i campi esistenti e nn gestiti dal form
            var a1 = quickform2obj(o1);

            var prod = new ProductItemsManager().Insert(o1);

            // cancello tutti i record con questo itemID
            man.DeleteByItemId(prod.Id);

            foreach (var a in a1)
            {
                a.ItemId = prod.Id;
                man.Insert(a);
            }
            removeFromCache();

            Grid1.DataBind();
            GridViewSimple.DataBind();
            clearQuickForm();
            LblOk.Text = RenderSuccess(Utility.GetLabel("RECORD_SAVED_MSG"));
            res = true;
        }
        catch (CustomException e1)
        {
            if (e1.CustomMessage == ProductItemsManager.MaxItemsException)
                LblErr.Text = RenderError(base.GetLabel("LblMaxItemsReached", "you have reached the maximum number of elements"));
            else
                LblErr.Text = RenderError(e1.CustomMessage);
        }
        catch (Exception e1)
        {
            LblErr.Text = RenderError(Utility.GetLabel("RECORD_ERR_MSG") + "<br />" + e1.ToString());
        }
        finally
        {
        }
        return res;
    }

    private void clearForm()
    {
        LblId.Text = "";
        LblOrderId.Text = "";
        LitItemType.Text = "";
        LblCreated.Text = "";
        LblUpdated.Text = "";
        ChkEnabled.Checked = true;
        TxtAlias.Text = "";
        TxtCssClass.Text = "";
        TxtRegularPrice.Text = "";
        TxtSalePrice.Text = "";
        TxtSKU.Text = "";
        TxtRegularPrice.Text = "";
        TxtSalePrice.Text = "";
        TxtWeight.Text = "";
        TxtQty.Text = "";
        //DropSets.SelectedValue = "-1";

        this.ItemDate = DateTime.MinValue;
        this.ValidFrom = DateTime.MinValue;
        this.ValidTo = DateTime.MinValue;
        foreach (KeyValuePair<string, string> item in Config.CultureList)
        {
            TextBox t1 = new TextBox();
            t1 = (TextBox)PanelTitle.FindControl("TxtTitle" + item.Value);
            t1.Text = "";

            var txt2 = new Controls_ContentEditorControl();
            txt2 = Utility.FindControlRecursive<Controls_ContentEditorControl>(this, "TxtDescription" + item.Value);
            txt2.Text = "";
        }

        foreach (var attribute in attributes)
        {
            if (!attribute.AllowCustomValue)
            {
                DropDownList d1 = new DropDownList();
                d1 = (DropDownList)PanelAttributes.FindControl("DropAttributeValues" + attribute.Name);
                d1.SelectedValue = "";
            }
            else
            {
                TextBox t1 = new TextBox();
                t1 = (TextBox)PanelAttributes.FindControl("TxtCustomField" + attribute.Name);
                t1.Text = "";
            }
        }

        PermissionsControl1.ClearForm();
    }

    private void clearQuickForm()
    {
        DropEnabled.SelectedValue = "0";
        QuickTxtRegularPrice.Text = "";
        QuickTxtSalePrice.Text = "";
        QuickTxtSKU.Text = "";
        QuickTxtRegularPrice.Text = "";
        QuickTxtSalePrice.Text = "";
        QuickTxtWeight.Text = "";
        QuickTxtQty.Text = "";
        QuickDropStock.SelectedValue = "1";
           
        foreach (var attribute in attributes)
        {
            if (!attribute.AllowCustomValue)
            {
                DropDownList d1 = new DropDownList();
                d1 = (DropDownList)QuickAttributes.FindControl("DropAttributeValuesQuick" + attribute.Name);
                d1.SelectedValue = "";
            }
            else
            {
                TextBox t1 = new TextBox();
                t1 = (TextBox)PanelAttributes.FindControl("TxtCustomFieldQuick" + attribute.Name);
                t1.Text = "";
            }

        }

    }

    private List<ItemAttributeValue> form2obj(ProductItem obj)
    {
        obj.Id = CurrentId;
        obj.Enabled = ChkEnabled.Checked;
        obj.TitleTranslations.Clear();
        obj.DescriptionTranslations.Clear();
        obj.CategoryId = int.Parse(DropCategories.SelectedValue);
        obj.Alias = TxtAlias.Text;
        obj.CssClass = TxtCssClass.Text;
        obj.ItemDate = this.ItemDate;
        obj.ValidFrom = this.ValidFrom;
        obj.ValidTo = this.ValidTo;

        // product fields

        // Product Type
        int type = 0;
        int.TryParse(DropNew.SelectedValue, out type);
        if (type > 0)
        {
            obj.ProductType = (ProductItem.ProductTypeEnum)type;
        } 
        // Attribute Set
        int attributeSetId = 0;
        int.TryParse(DropSets.SelectedValue, out attributeSetId);

        obj.AttributeSet = attributeSetId;

        // Draft
        obj.IsDraft = false;
        // SKU
        obj.SKU = TxtSKU.Text;
        // Regular Price
        decimal regPrice = 0m;
        decimal.TryParse(TxtRegularPrice.Text, out regPrice);
        if (regPrice > 0)
            obj.RegularPrice = regPrice;
        // Sale Price
        decimal salePrice = 0m;
        decimal.TryParse(TxtSalePrice.Text, out salePrice);
        if(salePrice > 0)
            obj.SalePrice = salePrice;
        // Weight
        decimal weight = 0m;
        decimal.TryParse(TxtWeight.Text, out weight);
        if(weight > 0)
            obj.Weight = weight;
        // Qty
        int qty = 0;
        int.TryParse(TxtQty.Text, out qty);
        if (weight > 0)
            obj.Availability = qty;
        // Stock
        int inStock = 0;
        int.TryParse(DropStock.SelectedValue, out inStock);
        if (inStock > 0)
            obj.InStock = (inStock == 1) ? true : false;

        if (CurrentId == 0)
            obj.ItemTypeName = LitItemType.Text;

        foreach (KeyValuePair<string, string> item in Config.CultureList)
        {
            TextBox t1 = new TextBox();
            t1 = (TextBox)PanelTitle.FindControl("TxtTitle" + item.Value);
            obj.TitleTranslations.Add(item.Key, t1.Text);

            var txt2 = new Controls_ContentEditorControl();
            txt2 = Utility.FindControlRecursive<Controls_ContentEditorControl>(this, "TxtDescription" + item.Value);
            obj.DescriptionTranslations.Add(item.Key, txt2.Text);
        }

        foreach(GridViewRow r in GridViewSimple.Rows)
        {
            CheckBox cb = (CheckBox)r.FindControl("chkRow");
            string IdString = r.Cells[5].Text;

            int Id = 0;
            int.TryParse(IdString, out Id);
            var p = new ProductItemsManager().GetByKey(Id);

            if (cb.Checked)
            {
                p.ThreadId = CurrentId;
            }
            else
            {
                if (p.ThreadId == CurrentId )
                    p.ThreadId = p.Id;
            }

            try
            {
                new ProductItemsManager().Update(p);
            }
            catch (Exception e1)
            {
                LblErr.Text = RenderError(Utility.GetLabel("RECORD_ERR_MSG") + "<br />" + e1.ToString());
            }

        }

        foreach (GridViewRow r in GridRelated.Rows)
        {
            CheckBox cb = (CheckBox)r.FindControl("chkRow");
            string IdString = r.Cells[5].Text;
            int Id = 0;
            int.TryParse(IdString, out Id);
            var p = new ProductItemsManager().GetByKey(Id);

            if (cb.Checked)
            {
                try
                {
                    new ProductItemsManager().SetRelated(CurrentId, p.Id);
                }
                catch (Exception e1)
                {
                    LblErr.Text = RenderError(Utility.GetLabel("RECORD_ERR_MSG") + "<br />" + e1.ToString());
                }
            }
            else
            {
                try
                {
                    new ProductItemsManager().DeleteRelated(CurrentId, p.Id);
                }
                catch (Exception e1)
                {
                    LblErr.Text = RenderError(Utility.GetLabel("RECORD_ERR_MSG") + "<br />" + e1.ToString());
                }
            }

        }

        var atts = new List<ItemAttributeValue>();

        // Store ItemAttributeValues

        var set = sman.GetByKey(attributeSetId);

        attributes.Clear();

        foreach (var attributeId in set.AttributesList)
        {
            var attribute = aman.GetByKey(attributeId);
            attributes.Add(attribute);
        }

        foreach (var attribute in attributes)
        {
            if (!attribute.AllowCustomValue)
            {
                DropDownList d1 = new DropDownList();
                d1 = (DropDownList)PanelAttributes.FindControl("DropAttributeValues" + attribute.Name);
                int attributeValueId = 0;
                int.TryParse(d1.SelectedValue, out attributeValueId);
                var record = new ItemAttributeValue();
                record.AttributeId = attribute.Id;
                record.AttributeValueId = attributeValueId;
                record.ItemId = CurrentId;
                atts.Add(record);
            }
            else
            {
                TextBox t1 = new TextBox();
                t1 = (TextBox)QuickAttributes.FindControl("TxtCustomField" + attribute.Name);

                var record = new ItemAttributeValue();
                record.AttributeId = attribute.Id;
                record.AttributeValueId = 0;
                record.CustomValueString = t1.Text;
                record.ItemId = CurrentId;
                atts.Add(record);

            }
        }

        obj.ItemParams = FormBuilder.GetParamsString(obj.ItemType.Params, ItemParams1);
        string fieldsString = FormBuilder.GetParamsString(obj.ItemType.Fields, ItemFields1);
        obj.LoadCustomFieldsFromString(fieldsString);
        PermissionsControl1.Form2obj(obj);

        return atts;

    }

    private List<ItemAttributeValue> quickform2obj(ProductItem obj)
    {
        int enabled = 0;
        int.TryParse(DropEnabled.SelectedValue, out enabled);
        obj.Enabled = (enabled == 1) ? true : false;
        obj.TitleTranslations.Clear();
        obj.DescriptionTranslations.Clear();
        obj.CategoryId = int.Parse(DropCategories.SelectedValue);
        obj.ThreadId = CurrentId;
        obj.ItemDate = this.ItemDate;
        obj.ValidFrom = this.ValidFrom;
        obj.ValidTo = this.ValidTo;

        // product fields
        obj.ProductType = ProductItem.ProductTypeEnum.Simple;
        int set = 0;
        int.TryParse(DropSets.SelectedValue, out set);
        obj.AttributeSet = set;
        obj.IsDraft = false;
        obj.SKU = QuickTxtSKU.Text;
        decimal regPrice = 0m;
        decimal.TryParse(QuickTxtRegularPrice.Text, out regPrice);
        obj.RegularPrice = regPrice;
        decimal salePrice = 0m;
        decimal.TryParse(QuickTxtSalePrice.Text, out salePrice);
        obj.SalePrice = salePrice;
        decimal weight = 0m;
        decimal.TryParse(QuickTxtWeight.Text, out weight);
        obj.Weight = weight;
        int qty = 0;
        int.TryParse(QuickTxtQty.Text, out qty);
        obj.Availability = qty;
        int inStock = 0;
        int.TryParse(QuickDropStock.SelectedValue, out inStock);
        obj.InStock = (inStock == 1) ? true : false;

        var atts = new List<ItemAttributeValue>();

        // QUI raccolo i quick attributevalue da salvare

        string endofname = "";
        foreach (var attribute in attributes)
        {
            if (!attribute.AllowCustomValue)
            {
                DropDownList d1 = new DropDownList();
                d1 = (DropDownList)QuickAttributes.FindControl("DropAttributeValuesQuick" + attribute.Name);
                int attributeValueId = 0;
                int.TryParse(d1.SelectedValue, out attributeValueId);
                if (attributeValueId > 0)
                {
                    endofname += "-" + d1.SelectedItem.Text;
                    var record = new ItemAttributeValue();
                    record.AttributeId = attribute.Id;
                    record.AttributeValueId = attributeValueId;
                    atts.Add(record);
                }
            }
            else
            {
                TextBox t1 = new TextBox();
                t1 = (TextBox)QuickAttributes.FindControl("TxtCustomFieldQuick" + attribute.Name);
                var record = new ItemAttributeValue();
                record.AttributeId = attribute.Id;
                record.AttributeValueId = 0;
                record.CustomValueString = t1.Text;
                atts.Add(record);
            }
        }

        foreach (KeyValuePair<string, string> item in Config.CultureList)
        {
            TextBox t1 = new TextBox();
            t1 = (TextBox)PanelTitle.FindControl("TxtTitle" + item.Value);
            obj.TitleTranslations.Add(item.Key, t1.Text + endofname);
        }
        obj.Alias = TxtAlias.Text + endofname.ToLower();

        obj.ItemParams = FormBuilder.GetParamsString(obj.ItemType.Params, ItemParams1);
        string fieldsString = FormBuilder.GetParamsString(obj.ItemType.Fields, ItemFields1);
        obj.LoadCustomFieldsFromString(fieldsString);
        PermissionsControl1.Form2obj(obj);

        return atts;
    }

    private void obj2form(ProductItem obj)
    {
        LblId.Text = obj.Id.ToString();
        LblOrderId.Text = obj.Ordering.ToString();
        LblUpdated.Text = obj.DateUpdated.ToString() + " by " + obj.UserUpdated;
        LblCreated.Text = obj.DateInserted.ToString() + " by " + obj.UserInserted;
        ChkEnabled.Checked = obj.Enabled;
        TxtAlias.Text = obj.Alias;
        TxtCssClass.Text = obj.CssClass;
        Utility.SetDropByValue(DropCategories, obj.CategoryId.ToString());

        foreach (KeyValuePair<string, string> item in Config.CultureList)
        {
            string sTitleTranslation = "";
            TextBox t1 = new TextBox();
            t1 = (TextBox)PanelTitle.FindControl("TxtTitle" + item.Value);
            obj.TitleTranslations.TryGetValue(item.Key, out sTitleTranslation);
            t1.Text = sTitleTranslation;

            string sDescriptionTraslation = "";
            var txt2 = new Controls_ContentEditorControl();
            txt2 = Utility.FindControlRecursive<Controls_ContentEditorControl>(this, "TxtDescription" + item.Value);
            obj.DescriptionTranslations.TryGetValue(item.Key, out sDescriptionTraslation);
            txt2.Text = sDescriptionTraslation;
        }

        // QUI popolo le varianti se son già salvate

        attributes.Clear();

        for (int i = 0; i < obj.Attributes.Count; i++)
        {
            DropDownList d1 = new DropDownList();
            d1 = (DropDownList)PanelAttributes.FindControl("DropAttributeValues" + obj.Attributes[i].Name);
            Utility.SetDropByValue(d1, obj.AttributeValues[i].Id.ToString());
        }

        for (int i = 0; i < obj.CustomAttributes.Count; i++)
        {
            TextBox t1 = new TextBox();
            t1 = (TextBox)PanelAttributes.FindControl("TxtCustomField" + obj.CustomAttributes[i].Name);
            t1.Text = obj.CustomAttributeValues[i];
        }

        //product fields
        TxtSKU.Text = obj.SKU;
        TxtRegularPrice.Text = (obj.RegularPrice > 0) ? obj.RegularPrice.ToString() : "" ;
        TxtSalePrice.Text = (obj.SalePrice > 0) ? obj.SalePrice.ToString() : "";
        TxtWeight.Text = (obj.Weight > 0) ? obj.Weight.ToString() : "";
        TxtQty.Text = (obj.Availability > 0) ? obj.Availability.ToString() : "";
        DropStock.SelectedValue = (obj.InStock) ? "1" : "0";
        if (obj.AttributeSet > 0)
        {
            DropSets.SelectedValue = (obj.AttributeSet.ToString());
            DropSets.Enabled = false;
        }
        else
        {
            DropSets.Enabled = true;
        }

        ItemParams1.ClearParams();
        ItemFields1.ClearParams();

        ItemParams1.LoadParams(obj);
        ItemFields1.LoadFields(obj);
        PermissionsControl1.Obj2form(obj);
        LitItemType.Text = obj.ItemTypeName;
        
        this.ItemDate = obj.ItemDate;
        this.ValidFrom = obj.ValidFrom;
        this.ValidTo = obj.ValidTo;

        int itemType = 0;
        int.TryParse(DropNew.SelectedValue, out itemType);
        if ((ProductItem.ProductTypeEnum)itemType == ProductItem.ProductTypeEnum.Configurable || obj.ProductType == ProductItem.ProductTypeEnum.Configurable)
        {
            PlhConfigurableProductPane.Visible = true;
            plhConfigurableProductTab.Visible = true;
        }
        else
        {
            PlhConfigurableProductPane.Visible = false;
            plhConfigurableProductTab.Visible = false;
        }

    }



    private void editRow(int recordId)
    {
        var obj = new PigeonCms.Shop.ProductItem();
        LblOk.Text = RenderSuccess("");
        LblErr.Text = RenderError("");

        if (!PgnUserCurrent.IsAuthenticated)
            throw new Exception("user not authenticated");

        clearForm();
        clearQuickForm();
        CurrentId = recordId;
        if (CurrentId == 0)
        {
            loadDropCategories(PRODUCT_SECTION); //TODO with settings.
            obj.ItemDate = DateTime.Now;
            obj.ValidFrom = DateTime.Now;
            obj.ValidTo = DateTime.MinValue;
            int defaultCategoryId = 0;
            int.TryParse(DropCategoriesFilter.SelectedValue, out defaultCategoryId);
            obj.CategoryId = defaultCategoryId;
            obj.AttributeSet = 0;
            obj2form(obj);
            LitItemType.Text = obj.ItemTypeName;
            obj.IsDraft = true;
            var currentProd = new ProductItemsManager().Insert(obj);
            this.CurrentId = currentProd.Id;
        }
        else
        {
            obj = new ProductItemsManager(true, true).GetByKey(CurrentId);
            loadDropCategories(obj.SectionId);

            // QUI nascondere le dropdown in più
            hideAttributes(obj.AttributeSet);
            hideAttributes(obj.AttributeSet, true);

            obj2form(obj);
        }
        GridRelated.DataBind();
        GridViewSimple.DataBind();
        MultiView1.ActiveViewIndex = VIEW_INSERT;
    }

    private void deleteRow(int recordId)
    {
        LblOk.Text = RenderSuccess("");
        LblErr.Text = RenderError("");

        try
        {
            if (!PgnUserCurrent.IsAuthenticated)
                throw new Exception("user not authenticated");

            new ProductItemsManager().DeleteById(recordId);
            removeFromCache();
        }
        catch (Exception e)
        {
            LblErr.Text = RenderError(e.Message);
        }
        Grid1.DataBind();
    }

    private void loadDropEnabledFilter()
    {
        DropEnabledFilter.Items.Clear();
        DropEnabledFilter.Items.Add(new ListItem(Utility.GetLabel("LblSelectState", "Select state"), ""));
        DropEnabledFilter.Items.Add(new ListItem("On-line", "1"));
        DropEnabledFilter.Items.Add(new ListItem("Off-line", "0"));
    }

    private void loadDropCategoriesFilter(int sectionId)
    {
        DropCategoriesFilter.Items.Clear();
        DropCategoriesFilter.Items.Add(new ListItem(Utility.GetLabel("LblSelectCategory", "Select category"), "0"));

        var catFilter = new CategoriesFilter();
        var catList = new List<Category>();

        catFilter = new CategoriesFilter();
        catFilter.SectionId = sectionId;
        if (catFilter.SectionId == 0) catFilter.Id = -1;
        catList = new CategoriesManager(true, false).GetByFilter(catFilter, "");
        foreach (var cat in catList)
        {
            DropCategoriesFilter.Items.Add(new ListItem(cat.Title, cat.Id.ToString()));
        }
        if (base.LastSelectedCategoryId > 0)
            Utility.SetDropByValue(DropCategoriesFilter, base.LastSelectedCategoryId.ToString());
    }

    private void loadDropCategories(int sectionId)
    {
        DropCategories.Items.Clear();

        var catFilter = new CategoriesFilter();
        var catList = new List<Category>();

        catFilter = new CategoriesFilter();
        catFilter.SectionId = sectionId;
        if (catFilter.SectionId == 0) catFilter.Id = -1;
        catList = new CategoriesManager().GetByFilter(catFilter, "");
        foreach (var cat in catList)
        {
            DropCategories.Items.Add(new ListItem(cat.Section.Title + " > " + cat.Title, cat.Id.ToString()));
        }
    }

    private void loadDropProductType() {
        DropProductTypeFilter.Items.Clear();
        DropProductTypeFilter.Items.Add(new ListItem(GetLabel("LblProductType", "Product type"), "0"));
        DropProductTypeFilter.Items.Add(new ListItem(GetLabel("LblSimpleProduct", "Simple Product"), ((int)ProductItem.ProductTypeEnum.Simple).ToString()));
        DropProductTypeFilter.Items.Add(new ListItem(GetLabel("LblConfigurableProduct", "Configurable Product"), ((int)ProductItem.ProductTypeEnum.Configurable).ToString()));
    }

    private void loadDropsItemTypes()
    {

        DropNew.Items.Clear();
        DropNew.Items.Add(new ListItem(GetLabel("LblNewProduct", "New Product"), "0"));
        DropNew.Items.Add(new ListItem(GetLabel("LblSimpleProduct", "Simple Product"), ((int)ProductItem.ProductTypeEnum.Simple).ToString()));
        DropNew.Items.Add(new ListItem(GetLabel("LblConfigurableProduct", "Configurable Product"), ((int)ProductItem.ProductTypeEnum.Configurable).ToString()));

        BtnNew.Visible = false;
        DropNew.Visible = true;
    }

    private void loadDropSets()
    {
        DropSets.Items.Clear();
        DropSets.Items.Add(new ListItem(GetLabel("LblSelectSets", "Select Attribute Set"), "-1"));
        var sfilter = new AttributeSetFilter();
        var sets = sman.GetByFilter(sfilter, "");
        foreach (var set in sets)
        {
            DropSets.Items.Add(new ListItem(set.Name, set.Id.ToString()));
        }
    }

    private Panel generateAttributesDropDown(bool isQuick = false)
    {        
        Panel pan1 = new Panel();
        pan1.CssClass = "form-group";

        foreach (var attribute in attributes)
        {

            if (!attribute.AllowCustomValue)
            {
                var valueFilter = new AttributeValueFilter();
                valueFilter.AttributeId = attribute.Id;
                var values = vman.GetByFilter(valueFilter, "");
                string quick = (isQuick) ? "Quick" : "";
                string validationGroup = (isQuick) ? "QuickProduct" : "SaveProduct";

                DropDownList drop = new DropDownList();
                drop.ID = "DropAttributeValues" + quick + attribute.Name;
                drop.CssClass = "form-control form-group";
                drop.Items.Add(new ListItem("-- " + attribute.Name + " --", ""));

                RequiredFieldValidator req = new RequiredFieldValidator();
                req.ControlToValidate = drop.ClientID;
                req.ValidationGroup = validationGroup;
                req.EnableClientScript = true;
                req.ErrorMessage = "";
                req.Text = "*";
                req.ForeColor = System.Drawing.Color.Red;
                req.Display = ValidatorDisplay.Dynamic;;

                foreach (var value in values)
                {
                    drop.Items.Add(new ListItem(value.Value, value.Id.ToString()));
                }
                Literal lit = new Literal();
                lit.Text = "<span><i>" + attribute.Name + "</i></span>";
                lit.ID = "LitAttributeName" + quick + attribute.Name;
                pan1.Controls.Add(lit);
                pan1.Controls.Add(req);
                pan1.Controls.Add(drop);
                
            }
        }

        return pan1;
    }

    private Panel generateAttributesTextBox(bool isQuick = false)
    {
        Panel pan1 = new Panel();
        pan1.CssClass = "form-group";
        foreach (var attribute in attributes)
        {

            if (attribute.AllowCustomValue)
            {
                string quick = (isQuick) ? "Quick" : "";

                TextBox txt = new TextBox();
                txt.ID = "TxtCustomField" + quick + attribute.Name;
                txt.CssClass = "form-control form-group";
                Literal lit = new Literal();
                lit.Text = "<span><i> Custom - " + attribute.Name + "</i></span>";
                lit.ID = "LitAttributeName" + quick + attribute.Name;
                pan1.Controls.Add(lit);
                pan1.Controls.Add(txt);
            }
        }

        return pan1;
    }

    private void hideAttributes(int setId, bool isQuick = false)
    {
        if(setId > 0) {
            // QUI nascondere le dropdown in più
            var set = sman.GetByKey(setId);

            attributes.Clear();
            foreach (var attributeId in set.AttributesList)
            {
                var afilter = new AttributeFilter();
                attributes.Add(aman.GetByKey(attributeId));
            }
        }

        string quick = (isQuick) ? "Quick" : "";
        DropDownList d1 = new DropDownList();
        TextBox t1 = new TextBox();
        Literal l1 = new Literal();

        var allAttributes = aman.GetByFilter(new AttributeFilter(), "");
        var present = attributes;
        var exclude = attributes = allAttributes.Except(attributes).ToList();

        foreach (var attribute in exclude)
        {
            if (!attribute.AllowCustomValue)
            {
                if (!isQuick)
                {
                    d1 = (DropDownList)PanelAttributes.FindControl("DropAttributeValues" + attribute.Name);
                    l1 = (Literal)PanelAttributes.FindControl("LitAttributeName" + attribute.Name);
                }
                else
                {
                    d1 = (DropDownList)QuickAttributes.FindControl("DropAttributeValues" + quick + attribute.Name);
                    l1 = (Literal)PanelAttributes.FindControl("LitAttributeName" + quick + attribute.Name);
                }
                d1.Visible = false;
                l1.Visible = false;
            }
            else
            {
                if (!isQuick)
                {
                    t1 = (TextBox)PanelAttributes.FindControl("TxtCustomField" + attribute.Name);
                    l1 = (Literal)PanelAttributes.FindControl("LitAttributeName" + attribute.Name);
                }
                else
                {
                    t1 = (TextBox)QuickAttributes.FindControl("TxtCustomField" + quick + attribute.Name);
                    l1 = (Literal)PanelAttributes.FindControl("LitAttributeName" + quick + attribute.Name);
                }
                t1.Visible = false;
                l1.Visible = false;
            }
        }
        foreach (var attribute in present)
        {
            if (!attribute.AllowCustomValue)
            {
                if (!isQuick)
                {
                    d1 = (DropDownList)PanelAttributes.FindControl("DropAttributeValues" + attribute.Name);
                    l1 = (Literal)PanelAttributes.FindControl("LitAttributeName" + attribute.Name);
                }
                else
                {
                    d1 = (DropDownList)QuickAttributes.FindControl("DropAttributeValues" + quick + attribute.Name);
                    l1 = (Literal)PanelAttributes.FindControl("LitAttributeName" + quick + attribute.Name);
                }
                d1.Visible = true;
                l1.Visible = true;
            }
            else
            {
                if (!isQuick)
                {
                    t1 = (TextBox)PanelAttributes.FindControl("TxtCustomField" + attribute.Name);
                    l1 = (Literal)PanelAttributes.FindControl("LitAttributeName" + attribute.Name);
                }
                else
                {
                    t1 = (TextBox)QuickAttributes.FindControl("TxtCustomField" + quick + attribute.Name);
                    l1 = (Literal)PanelAttributes.FindControl("LitAttributeName" + quick + attribute.Name);
                }
                t1.Visible = true;
                l1.Visible = true;
            }
        }
    }

    private void setFlag(int recordId, bool value, string flagName)
    {
        try
        {
            if (!PgnUserCurrent.IsAuthenticated)
                throw new Exception("user not authenticated");

            var o1 = new ProductItemsManager().GetByKey(recordId);
            switch (flagName.ToLower())
            {
                case "enabled":
                    o1.Enabled = value;
                    break;
                default:
                    break;
            }
            new ProductItemsManager().Update(o1);
            removeFromCache();
        }
        catch (Exception e1)
        {
            LblErr.Text = RenderError(Utility.GetLabel("RECORD_ERR_MSG") + "<br />" + e1.ToString());
        }
        finally { }
    }

    protected void moveRecord(int recordId, Database.MoveRecordDirection direction)
    {
        LblErr.Text = RenderError("");
        LblOk.Text = RenderSuccess("");

        try
        {
            if (!PgnUserCurrent.IsAuthenticated)
                throw new Exception("user not authenticated");

            new ProductItemsManager(true, true).MoveRecord(recordId, direction);
            removeFromCache();
            Grid1.DataBind();
            MultiView1.ActiveViewIndex = VIEW_GRID;
        }
        catch (Exception e1)
        {
            LblErr.Text = RenderError(Utility.GetLabel("RECORD_ERR_MSG") + "<br />" + e1.ToString());
        }
        finally { }
    }

    protected void showThreads(string recordId, bool show)
    {
        if (show)
            base.CurrentKey = recordId;
        else
            base.CurrentKey = "";
    }

    /// <summary>
    /// remove all items from cache
    /// </summary>
    protected static void removeFromCache()
    {
        new CacheManager<PigeonCms.Shop.ProductItem>("PigeonCms.ProductItem").Clear();
    }

    [PigeonCms.UserControlScriptMethod]
    public static void RemoveFromCache()
    {
        removeFromCache();
    }

    [PigeonCms.UserControlScriptMethod]
    public static void Refresh()
    {
        //return ClientScript.GetPostBackEventReference(this, null);
        //__doPostBack('__Page', 'MyCustomArgument')
    }

    #endregion

}
