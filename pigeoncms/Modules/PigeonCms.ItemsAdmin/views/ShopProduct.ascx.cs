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
//using FredCK.FCKeditorV2;

public partial class Controls_ShopProduct : PigeonCms.ItemsAdminControl
{
    //const int CAT_START = 1000;
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

    protected int ItemId
    {
        get
        {
            return base.CurrentId;   
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
            PanelTitle.Controls.Add(pan1);

            TextBox txt1 = new TextBox();
            txt1.ID = "TxtTitle" + item.Value;
            txt1.MaxLength = 200;
            txt1.CssClass = "form-control";
            txt1.ToolTip = item.Key;
            LabelsProvider.SetLocalizedControlVisibility(this.ShowOnlyDefaultCulture, item.Key, txt1);
            pan1.Controls.Add(txt1);
            Literal lit1 = new Literal();
            if (!this.ShowOnlyDefaultCulture)
                lit1.Text = "<span class='input-group-addon'>" + item.Value + "</span>";
            pan1.Controls.Add(lit1);
            if (item.Key == Config.CultureDefault)
                titleId = txt1.ClientID;

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

        if (this.BaseModule.DirectEditMode)
        {
        }

        TxtAlias.Attributes.Add("onfocus", "preloadAlias('" + titleId + "', this)");

        //restrictions
        Grid1.AllowSorting = this.AllowOrdering;
        Grid1.Columns[COL_ORDER_ARROWS_INDEX].Visible = this.AllowOrdering;
        Grid1.Columns[COL_ORDERING_INDEX].Visible = this.AllowOrdering;

        Grid1.Columns[COL_ALIAS_INDEX].Visible = this.ShowAlias;
        TxtAlias.Visible = this.ShowAlias;

        Grid1.Columns[COL_TYPE_INDEX].Visible = this.ShowType;
        LitItemType.Visible = this.ShowType;

        Grid1.Columns[COL_SECTION_INDEX].Visible = this.ShowSectionColumn;
        //Grid1.Columns[COL_ACCESSTYPE_INDEX].Visible = this.ShowSecurity;
        //Grid1.Columns[COL_ACCESSLEVEL_INDEX].Visible = this.ShowSecurity;
        //Grid1.Columns[COL_ID_INDEX].Visible = this.ShowSecurity;
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

        Utility.Script.RegisterClientScriptInclude(this, "appjs",
            ResolveUrl(Config.ModulesPath + this.BaseModule.ModuleFullName + "/views/" +
            this.BaseModule.CurrViewFolder + "/assets/js/app.js"));

        if (this.BaseModule.DirectEditMode)
        {
            if (base.CurrItem.Id == 0)
                throw new ArgumentException();
            if (new PigeonCms.Shop.ProductItemsManager().GetByKey(base.CurrItem.Id).Id == 0)
                throw new ArgumentException();
        }

        if (!Page.IsPostBack)
        {
            loadDropEnabledFilter();
            loadDropSectionsFilter(base.SectionId);
            {
                int secId = -1;
                int.TryParse(DropSectionsFilter.SelectedValue, out secId);
                loadDropCategoriesFilter(secId);
            }
            loadDropsItemTypes();
            //loadAttributesRepeater();
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
                currentItem = new ProductItemsManager().GetByKey(CurrentId);
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
            //DropNew.Visible = false;
            BtnNew.Visible = false;
            BtnCancel.OnClientClick = "closePopup();";

            editRow(base.CurrItem.Id);
        }
    }

    protected void DropEnabledFilter_SelectedIndexChanged(object sender, EventArgs e)
    {
        Grid1.DataBind();
    }

    protected void DropSectionsFilter_SelectedIndexChanged(object sender, EventArgs e)
    {
        int secID = 0;
        int.TryParse(DropSectionsFilter.SelectedValue, out secID);

        loadDropCategoriesFilter(secID);
        loadDropCategories(secID);
        Grid1.DataBind();
        base.LastSelectedSectionId = secID;
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

    protected void BtnNew_Click(object sender, EventArgs e)
    {
        try
        {
            //if (checkAddNewFilters())
            //{
                //Utility.SetDropByValue(DropNew, this.ItemType);
                editRow(0);
            //}
        }
        catch (Exception e1) { LblErr.Text = RenderError(e1.Message); }
    }

    protected void ObjDs1_ObjectCreating(object sender, ObjectDataSourceEventArgs e)
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

            LinkButton LnkTitle = (LinkButton)e.Row.FindControl("LnkTitle");
            LnkTitle.Text = "<i class='fa fa-pgn_edit fa-fw'></i>";
            if (!item.IsThreadRoot)
                LnkTitle.Text += "--";
            LnkTitle.Text += Utility.Html.GetTextPreview(item.Title, 30, "");
            if (string.IsNullOrEmpty(item.Title))
                LnkTitle.Text += Utility.GetLabel("NO_VALUE", "<no value>");

            if (item.CategoryId > 0)
            {
                CategoriesManager mgr = new CategoriesManager();
                Category cat = mgr.GetByKey(item.CategoryId);
                Literal LitCategoryTitle = (Literal)e.Row.FindControl("LitCategoryTitle");
                LitCategoryTitle.Text = cat.Title;
                if (cat.SectionId > 0)
                {
                    Literal LitSectionTitle = (Literal)e.Row.FindControl("LitSectionTitle");
                    LitSectionTitle.Text = new SectionsManager().GetByKey(cat.SectionId).Title;
                }
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

            // now have to compose the list with the couple of ids compiled, so take all variants productitem
            var productfilter = new ProductItemFilter();
            productfilter.ThreadId = item.Id;
            productfilter.ShowOnlyRootItems = false;
            var productitems = new ProductItemsManager().GetByFilter(productfilter, "");

            // iterate them
            foreach (var productitem in productitems)
            {
                // check if are compiled
                var variants = new ItemAttributesValuesManager().GetByItemId(productitem.Id);
                if (variants != null && variants.Count > 0)
                {
                    variants = variants.Where(x => x.AttributeValueId > 0).OrderBy(x => x.AttributeId).ToList();

                    Literal variantsCompiled = e.Row.FindControl("variantsCompiled") as Literal;

                    // add ids and values to know the information of each box
                    foreach (var variant in variants)
                    {
                        var value = new PigeonCms.AttributeValuesManager().GetByKey(variant.AttributeValueId).Value;
                        variantsCompiled.Text += value + " - " ;
                    }

                    variantsCompiled.Text = variantsCompiled.Text.Substring(0, variantsCompiled.Text.Length - 3) + "<br>";
                    
                }

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

    protected void BtnCancel_Click(object sender, EventArgs e)
    {
        if(new ProductItemsManager().GetByKey(this.CurrentId).IsDraft) {
            new ProductItemsManager().DeleteById(this.CurrentId);
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

        //if (MultiView1.ActiveViewIndex == VIEW_GRID)    //list view
        //{
        //    Utility.SetDropByValue(DropNew, "");
        //}
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

    private bool saveForm()
    {
        bool res = false;
        LblErr.Text = RenderError("");
        LblOk.Text = RenderSuccess("");

        try
        {
            var o1 = new ProductItemsManager().GetByKey(CurrentId);  //precarico i campi esistenti e nn gestiti dal form
            if (o1.IsDraft)
            {
                o1.IsDraft = false;
                form2obj(o1);
                new ProductItemsManager().Update(o1);
            }
            else
            {

                form2obj(o1);
                new ProductItemsManager().Update(o1);
            }
            removeFromCache();

            Grid1.DataBind();
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
            //FCKeditor t2 = new FCKeditor();
            //t2 = (FCKeditor)PanelDescription.FindControl("TxtDescription" + item.Value);
            //t2.Value = "";
        }
        PermissionsControl1.ClearForm();
    }

    private void form2obj(ProductItem obj)
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

            //FCKeditor t2 = new FCKeditor();
            //t2 = (FCKeditor)PanelDescription.FindControl("TxtDescription" + item.Value);
            //obj.DescriptionTranslations.Add(item.Key, t2.Value);
        }

        obj.ItemParams = FormBuilder.GetParamsString(obj.ItemType.Params, ItemParams1);
        string fieldsString = FormBuilder.GetParamsString(obj.ItemType.Fields, ItemFields1);
        obj.LoadCustomFieldsFromString(fieldsString);
        PermissionsControl1.Form2obj(obj);
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
            //string sDescriptionTraslation = "";
            //FCKeditor t2 = new FCKeditor();
            //t2 = (FCKeditor)PanelDescription.FindControl("TxtDescription" + item.Value);
            //obj.DescriptionTranslations.TryGetValue(item.Key, out sDescriptionTraslation);
            //t2.Value = sDescriptionTraslation;
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
    }



    private void editRow(int recordId)
    {
        var obj = new PigeonCms.Shop.ProductItem();
        LblOk.Text = RenderSuccess("");
        LblErr.Text = RenderError("");

        if (!PgnUserCurrent.IsAuthenticated)
            throw new Exception("user not authenticated");

        clearForm();
        CurrentId = recordId;
        if (CurrentId == 0)
        {
            loadDropCategories(int.Parse(DropSectionsFilter.SelectedValue));
            obj.ItemTypeName = "Shop.ProductItem"; //DropNew.SelectedValue;
            obj.ItemDate = DateTime.Now;
            obj.ValidFrom = DateTime.Now;
            obj.ValidTo = DateTime.MinValue;
            int defaultCategoryId = 0;
            int.TryParse(DropCategoriesFilter.SelectedValue, out defaultCategoryId);
            obj.CategoryId = defaultCategoryId;
            obj2form(obj);
            LitItemType.Text = "Shop.ProductItem"; //DropNew.SelectedValue;
            obj.IsDraft = true;
            var currentProd = new ProductItemsManager().Insert(obj);
            this.CurrentId = currentProd.Id;
        }
        else
        {
            obj = new ProductItemsManager().GetByKey(CurrentId);
            loadDropCategories(obj.SectionId);
            obj2form(obj);
        }
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

    private void loadDropSectionsFilter(int sectionId)
    {
        DropSectionsFilter.Items.Clear();
        if (!this.MandatorySectionFilter)
            DropSectionsFilter.Items.Add(new ListItem(Utility.GetLabel("LblSelectSection", "Select section"), "0"));

        var secFilter = new SectionsFilter();
        secFilter.Id = sectionId;
        var secList = new SectionsManager(true, false).GetByFilter(secFilter, "");

        foreach (var sec in secList)
        {
            DropSectionsFilter.Items.Add(new ListItem(sec.Title, sec.Id.ToString()));
        }
        if (base.LastSelectedSectionId > 0)
            Utility.SetDropByValue(DropSectionsFilter, base.LastSelectedSectionId.ToString());
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

    private void loadDropsItemTypes()
    {

        DropItemTypesFilter.Items.Clear();
        DropItemTypesFilter.Items.Add(new ListItem(Utility.GetLabel("LblSelectItem", "Select item"), ""));

        var filter = new ItemTypeFilter();
        if (!string.IsNullOrEmpty(this.ItemType))
            filter.FullName = this.ItemType;
        List<ItemType> recordList = new ItemTypeManager().GetByFilter(filter, "FullName");
        foreach (ItemType record1 in recordList)
        {
            //DropNew.Items.Add(
            //    new ListItem(record1.FullName, record1.FullName));

            DropItemTypesFilter.Items.Add(
                new ListItem(record1.FullName, record1.FullName));
        }

        BtnNew.Visible = true;

        //if (!string.IsNullOrEmpty(this.ItemType))
        //{
        //    BtnNew.Visible = true;
        //    //DropNew.Visible = false;
        //    DropItemTypesFilter.Visible = false;
        //}
        //else
        //{
        //    BtnNew.Visible = false;
        //    //DropNew.Visible = true;
        //    DropItemTypesFilter.Visible = true;
        //}
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

            new ProductItemsManager().MoveRecord(recordId, direction);
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

    Boolean checkAddNewFilters()
    {
        Boolean res = true;
        if (DropSectionsFilter.SelectedValue == "0" || DropSectionsFilter.SelectedValue == "")
        {
            LblErr.Text = RenderError(base.GetLabel("ChooseSection", "Choose a section before"));
            res = false;
        }

        if (DropCategoriesFilter.SelectedValue == "0" || DropCategoriesFilter.SelectedValue == "")
        {
            LblErr.Text = RenderError(base.GetLabel("ChooseCategory", "Choose a category before"));
            res = false;
        }
        return res;
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

    /// <summary>
    /// return all attributes that are not assigned to an element with specific itemId
    /// ritorna tutti gli attributi che non sono assegnati ad un elemento con uno specifico id.
    /// OK
    /// </summary>
    /// <param name="itemId"></param>
    /// <returns></returns>
    [PigeonCms.UserControlScriptMethod]
    public static object GetAttributes(int itemId)
    {
        // set the managers
        // dichiaro i managers
        var iavMng = new ItemAttributesValuesManager();
        var attrMng = new AttributesManager();

        // set filter
        // dichiaro il filter
        var filter = new AttributeFilter();

        // get all attributes referred to itemId
        // prendi gli attributi riferiti all'itemId richiesto
        var items = iavMng.GetByReferredId(itemId);

        // object to return
        // oggetto da ritornare 
        var result = new object();

        // groupedIds
        var groupByAttributesId = new List<int>();

        if (items != null && items.Count > 0)
        {
            // groupBy AttributeId the list , just to iterate on them.
            // raggruppo gli attributi , in modo da poter scorrerli
            //attributesId = items.GroupBy(x => x.AttributeId).Select(y => new PigeonCms.Attribute() { Id = y.Key }).ToList().Select(x => x.Id).ToList();
            groupByAttributesId = items.GroupBy(elems => elems.AttributeId)
                                    .Select(groups => groups.ToList())
                                    .Select(grouped => grouped.First().AttributeId)
                                    .ToList();
        }

        // get all attributes
        // prendo tutti gli attributi
        var allAttributes = attrMng.GetByFilter(filter, "");

        // separe the list, one with values, and one with custom values
        // separo la lista e prendo gli attributi con i valori compilati e quelli con i valori custom
        var withValues = allAttributes.Where(x => x.AllowCustomValue == false).ToList();
        var customValues = allAttributes.Where(x => x.AllowCustomValue == true).ToList();

        // remove from list all attribute that have the same Id as the list above
        // rimuovo dalla lista totale gli attributi che non hanno l'Id presente tra quelli riferiti all'Item padre.
        result = new
        {
            withValues = withValues.Where( wv => !groupByAttributesId.Any( g => g == wv.Id)).ToList(),
            withoutValues = customValues.Where(wv => !groupByAttributesId.Any(g => g == wv.Id)).ToList()
        };

        return result;
    }

    /// <summary>
    /// get attributeValue from given AttributeId
    /// OK
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [PigeonCms.UserControlScriptMethod]
    public static List<AttributeValue> GetAttributeValues(int id)
    {
        // declarations
        var filter = new AttributeValueFilter();
        var mng = new AttributeValuesManager();
        // apply filter
        // applico il filtro per un dato attributeId
        filter.AttributeId = id;
        // RUN
        var myValues = mng.GetByFilter(filter, "");
        //return
        return myValues;
    }

    /// <summary>
    /// Save checkbox forms of attributes tab.
    /// It takes a JSON with checked checkboxes, excluding the itemAttributesValues saved 
    /// it generate 2 list, one to insert, one to exclude.
    /// You can't remove a variant with assigned itemId, only with referred and itemId = 0.
    /// MIGLIORARE SI PUO'
    /// </summary>
    /// <param name="jsonArr"></param>
    /// <param name="itemId"></param>
    /// <returns></returns>
    [PigeonCms.UserControlScriptMethod]
    public static string SaveAttributeValues(string jsonArr, int itemId)
    {
        // serialize JSON in ItemAttributeValue List
        // serializzo la lista degli attributi selezionati in una lista di itemAttributeValues
        var serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        var values = serializer.Deserialize<List<ItemAttributeValue>>(jsonArr);

        // declare product objects
        // dichiarazione oggetto prodotti , filtro , manager
        var man = new ProductItemsManager();
        var filterProd = new ProductItemFilter();

        // declare itemAttributeValue objects
        // dichiaro gli oggetti, manager, filtri per gli itemAttributeValues
        var itemAttr = new ItemAttributeValue();
        var itemAttrMan = new ItemAttributesValuesManager();
        var filter = new ItemAttributeValueFilter();

        // get all products (also child of itemId)
        // prendo tutti i prodotti (anche i figli dell'itemId) v 
        filterProd.ThreadId = itemId;
        filterProd.ShowOnlyRootItems = false;
        var products = man.GetByFilter(filterProd, "");

        // get all actual attributes
        // prendo gli attributi attualmente salvati nel database
        var actualAttributes = itemAttrMan.GetByReferredId(itemId);

        // declare list to insert and to delete
        // dichiaro le liste che saranno popolate con i valori da inserire e da rimuovere
        var toInsert = new List<ItemAttributeValue>();
        var toDelete = new List<ItemAttributeValue>();

        if (actualAttributes != null && actualAttributes.Count > 0)
        {
            // we need to check if we added a new attribute
            // if yes, we will save the new attributes with ID for each record previously inserted
            // to not lose records 
            // controllo se nuovi attributi sono stati selezionati
            // se ci sono, salveremo con un id per ogni record precedentemente inserito
            // questo è per non perdere i valori già compilati ma al massimo editarli
            //var oldAttributes = actualAttributes.GroupBy(x => x.AttributeId).Select(y => new PigeonCms.Attribute() { Id = y.Key }).ToList();
            //var takenAttributes = values.GroupBy(x => x.AttributeId).Select(y => new PigeonCms.Attribute() { Id = y.Key }).ToList();
            var oldAttributes = actualAttributes.GroupBy(elems => elems.AttributeId)
                        .Select(groups => groups.ToList())
                        .Select(grouped => grouped.First().AttributeId)
                        .ToList();
            var takenAttributes = values.GroupBy(elems => elems.AttributeId)
                       .Select(groups => groups.ToList())
                       .Select(grouped => grouped.First().AttributeId)
                       .ToList();

            // newattributes is takenattributes without oldattributes
            // i nuovi attributi sono gli attributi appena presi dal form senza i vecchi salvati in db
            var newAttributes = takenAttributes.Except(oldAttributes).ToList();

            // TODO devo ricordarmi il perchè di questa cosa così mistica
            var valuesWithNewAttributes = values.Where(x => newAttributes.Any(x2 => x2 == x.AttributeId)).OrderBy(x => x.AttributeId).ToList();

            // the attributes actually saved with itemId more than zero, means that are assigned
            // negli attributi attualmente salvati, quelli con itemId maggiore di zero sono sicuramente popolati
            var actualAttributesAssigned = actualAttributes.Select(x => x.ItemId).Where(x => x > 0).ToList();

            // group to not have duplicates, if they assigned to more item with the same referredId we will have duplicates
            // raggruppo gli attributi attualmente assegnati, se sono assegnati a più di un elemento con stesso referredId avremo dei duplicati altrimenti
            // TODO che bella scritta così
            var groupedActualAssigned = actualAttributesAssigned.GroupBy(i => i, (e, g) => new { Value = e, Count = g.Count() });

            // iterate on list previously created
            // iteriamo nella lista appena generata
            foreach (var assigned in groupedActualAssigned)
            {
                // check mistyc things
                if (valuesWithNewAttributes != null && valuesWithNewAttributes.Count > 0)
                {
                    //iterate each Value to be inserted of the new attribute and assign the ItemId to keep Value
                    var toAdd = valuesWithNewAttributes.First();
                    // remove from list to insert
                    values.Remove(toAdd);
                    // insert with the id of variants populated
                    toAdd.ItemId = assigned.Value;
                    itemAttrMan.Insert(toAdd);
                }
            }

            //DELETE LIST
            // exclude to actualAttributes the new Values, if values are less than actualAttributes, the exclusion will be deleted
            // escludo dalla lista degli attributi salvati in database, quelli presi dal form, creando il fieldset da inserire
            toDelete = actualAttributes.Except(values).ToList();

            // INSERT LIST
            // exclude to new Values the actualAttributes, if actualAttributes are less than values, the exclusion will be inserted
            // escludo dalla lista degli attributi presi dal form quelli presenti in database, creando il fieldset da inserire
            toInsert = values.Except(actualAttributes).ToList();

        }
        else
        {
            toInsert = values;
        }

        // create object to return
        // creo gli oggetti da ritornare
        // TODO labels
        var error = new
        {
            success = false,
            message = "You have assigned variants with this attribute, can't delete it."
        };

        var success = new
        {
            success = true,
            message = "All savings done well."
        };

        // insert each value
        // inserisci ogni valore
        foreach (ItemAttributeValue insert in toInsert)
        {
            itemAttrMan.Insert(insert);
        }

        // delete each value
        // elimina ogni valore
        foreach (ItemAttributeValue delete in toDelete)
        {
            // if itemAttributeValue has itemId higher than zero, means that is assigned and you have to delete variant first.
            // se l'itemAttributeValue ha itemId maggiore di zero, significa che è assegnato e quindi dovrai prima eliminare le varianti a cui è stato assegnato.
            if (delete.ItemId > 0)
            {
                // return error message
                return toJson(error);
            }
            else
            {
                // delete the record
                itemAttrMan.Delete(delete.ItemId, delete.AttributeId, delete.AttributeValueId, delete.Referred);
            }
            
        }

        // return success JSON format
        return toJson(success);

    }

    /// <summary>
    /// Method used to return info to generate the box on variants.
    /// Metodo usato per recuperate le info e generare i box varianti
    /// MIGLIORARE SI PUO'
    /// </summary>
    /// <param name="jsonArr"></param>
    /// <param name="itemId"></param>
    /// <returns></returns>
    [PigeonCms.UserControlScriptMethod]
    public static string GetAttributeValuesForVariants(int itemId)
    {
        // object declaration
        // dichiarazioni
        var filter = new ItemAttributeValueFilter();
        var man = new ItemAttributesValuesManager();


        // only referred to itemId
        // prendo solamente gli elementi riferiti all'itemId
        filter.Referred = itemId;
        var referredItemAttrVals = man.GetByFilter(filter, "");

        // get a list with attributes (to separe select object)
        // prendo una lista con gli attributi (per separare gli oggetti)
        var valuesGroup = referredItemAttrVals.GroupBy(items => items.AttributeId)
                                .Select(groups => groups.ToList())
                                .ToList();

        // object declaration
        // dichiarazioni
        var attributes = new List<PigeonCms.Attribute>();
        var attMan = new AttributesManager();

        // iterate on created groups of attributes
        // scorro i gruppi appena creati
        foreach (var group in valuesGroup)
        {
            var attribute = attMan.GetByKey(group.First().AttributeId);
            // add only if values are not custom
            // aggiungi solo se i valori sono preimpostati e non custom
            if (!attribute.AllowCustomValue)
            {
                attributes.Add(attribute);
            }
        }

        // prepare result
        // preparo il risultato
        var result = new List<object>();

        // object declaration
        // dichiarazioni
        var valuesFilter = new AttributeValueFilter();
        var valuesMan = new AttributeValuesManager();

        foreach (var attribute in attributes)
        {
            // get all itemId referred itemAttributeValue of attribute 
            // prendo tutti gli itemId riferiti agli itemAttributeValue di ciascun attributo
            valuesFilter.AttributeId = attribute.Id;
            var attributeValues = valuesMan.GetByFilter(valuesFilter, "");

            // keep only value who has same id as the list with only selected user values
            // mantengo solamente i valori che corrispondono alla lista dei valori selezionati dall'utente
            attributeValues = attributeValues.Where(x => referredItemAttrVals.Any(x2 => x2.AttributeValueId == x.Id)).ToList();

            // delcarations
            // dichiarazioni
            var attributeObject = new List<object>();

            // iterate on this values
            // scorriamo su questi valori
            foreach (var attributeValue in attributeValues)
            {
                // filter = new ItemAttributeValueFilter();
                filter.ItemId = itemId;
                filter.AttributeId = attributeValue.AttributeId;
                filter.AttributeValueId = attributeValue.Id;
                var itemAttributeValue = man.GetByFilter(filter, "");

                // seleziono un probabile valore salvato
                string isSelected = "";
                if(itemAttributeValue != null && itemAttributeValue.Count > 0) {
                    var item = itemAttributeValue.First();
                    isSelected = (item.AttributeValueId == attributeValue.Id) ? "selected" : "";
                }
                
                // element base
                var infoValues = new
                {
                    attrValId = attributeValue.Id,
                    attributeValue = attributeValue.Value,
                    selected = isSelected
                };

                // add to list
                attributeObject.Add(infoValues);

            }

            // save here info about attribute (name and id)
            // salvo qui le info sull'attributo (nome e id)
            var infoAttribute = new
            {
                attrId = attribute.Id,
                attribute = attMan.GetByKey(attribute.Id).Name,
            };

            // add to list
            attributeObject.Add(infoAttribute);

            //add element to result
            result.Add(attributeObject);
        }

        //convert in json and return
        return toJson(result);
    }
    
    /// <summary>
    /// Compile the select box used to select default values
    /// Compila la select usata per indicare l'attributo di default.
    /// MIGLIORARE SI PUO'
    /// </summary>
    /// <param name="itemId"></param>
    /// <returns></returns>
    [PigeonCms.UserControlScriptMethod]
    public static string CompileAttributes(int itemId)
    {
        // declarations
        // dichiarazioni
        var attrValFilter = new ItemAttributeValueFilter();
        var attrValMan = new ItemAttributesValuesManager();

        // get all referred attributes
        // with no custom
        attrValFilter.Referred = itemId;
        attrValFilter.OnlyWithValues = true;
        var items = attrValMan.GetByFilter(attrValFilter, "");

        // declarations
        // dichiarazioni
        var attributesId = new List<int>();

        // if not nulll retrieve only AttributesId form list 
        // se ci sono valori, raggruppo per AttributeId
        if (items != null && items.Count > 0)
        {
            attributesId = items.GroupBy(elems => elems.AttributeId)
                                .Select(groups => groups.ToList())
                                .Select(grouped => grouped.First().AttributeId)
                                .ToList();
        }

        // declarations
        // dichiarazioni
        var attributesValues = new List<AttributeValue>();
        var boxes = new List<object>();

        // declarations
        // dichiarazioni
        var attFilter = new AttributeFilter();
        var attMan = new AttributesManager();

        // declarations
        // dichiarazioni
        var filter = new AttributeValueFilter();
        var man = new AttributeValuesManager();

        // iterate attributesId
        // scorro la lista di attributeId ottenuta prima
        foreach (int attributeId in attributesId)
        {
            // get the record of AttributeValue having attributeId
            // prendo i valori degli attributi
            filter.AttributeId = attributeId;
            attributesValues = man.GetByFilter(filter, "");

            // add the name of attribute on a list
            // aggiungo il nome dell'attributo in una lista
            var singleAtt = new List<object>();
            singleAtt.Add(attMan.GetByKey(attributeId).Name);

            // iterate all values of attributeId
            // scorro tutti i valori ottenuti
            foreach (var attrVal in attributesValues)
            {
                // get the element and put value checked if it is (if in initial list is present one of these values)
                // prendo l'elemento e lo rendo checked se nella lista iniziale è presente
                int index = items.Select(x => x.AttributeValueId).ToList().IndexOf(attrVal.Id);
                string valueIn = (index > -1) ? "checked='true'" : "";

                // create object to add in return list
                // creo l'oggetto da aggiungere nella lista da tornare
                var obj = new
                {
                    Id = attrVal.Id,
                    Value = attrVal.Value,
                    AttributeId = attrVal.AttributeId,
                    Checked = valueIn
                };
                singleAtt.Add(obj);
            }

            // aggiungi al box
            boxes.Add(singleAtt);
        }

        // get all referred attributes with custom values
        // prendo tutti gli elementi con gli attributi a valori custom
        attrValFilter = new ItemAttributeValueFilter();
        attrValFilter.Referred = itemId;
        attrValFilter.OnlyCustomFields = true;
        var itemsCustom = attrValMan.GetByFilter(attrValFilter, "");

        attributesId = new List<int>();

        // if not null retrieve only AttributesId form list 
        // se non nulla prendo solamente la lista degli attributeId
        if (itemsCustom != null && itemsCustom.Count > 0)
        {
            attributesId = items.GroupBy(elems => elems.AttributeId)
                                .Select(groups => groups.ToList())
                                .Select(grouped => grouped.First().AttributeId)
                                .ToList();
        }

        // declarations
        // dichiarazioni
        attributesValues = new List<AttributeValue>();
        var spans = new List<object>();

        // iterate attributesId
        // scorro gli attributeId
        foreach (int attributeId in attributesId)
        {
            // add the name of attribute on a list
            // aggiungo il nome in lista
            var singleAtt = new List<object>();

            // add name and id of attribute
            // aggiungo il name e l'id dell'attribute 
            singleAtt.Add(attMan.GetByKey(attributeId).Name);
            singleAtt.Add(attributeId);

            spans.Add(singleAtt);
        }

        // return complete list
        var success = new
        {
            boxes = boxes,
            spans = spans
        };

        return toJson(success);
    }

    // TODO Refactor
    /// <summary>
    /// Return an object that allows to compile the template with input forms for variants.
    /// Iterate on attributes and for each attributeValue add the single Id and Value in a list.
    /// </summary>
    /// <param name="itemId"></param>
    /// <param name="attributeId"></param>
    /// <param name="attributeValueId"></param>
    /// <returns></returns>
    [PigeonCms.UserControlScriptMethod]
    public static string GetLinkVariants(int itemId, int attributeId, int attributeValueId)
    {
        var filter = new ItemAttributeValueFilter();

        //can be present for select box
        if(attributeId > 0) 
            filter.AttributeId = attributeId;

        //can be present for select box
        if(attributeValueId > 0)
            filter.AttributeValueId = attributeValueId;

        //get only attributeValues referred to itemId
        filter.Referred = itemId;
        filter.OnlyWithValues = true;

        // now have to compose the list with the couple of ids compiled, so take all variants productitem
        var productfilter = new ProductItemFilter();
        productfilter.ThreadId = itemId;
        productfilter.ShowOnlyRootItems = false;
        var productitems = new ProductItemsManager().GetByFilter(productfilter, "");

        List<string> compiled = new List<string>();
        List<string> compiledValues = new List<string>();

        // iterate on each variant productitem
        foreach (var productitem in productitems)
        {
            // get the values related to item.
            var variants = new ItemAttributesValuesManager().GetByItemId(productitem.Id);

            // if are present
            if (variants != null && variants.Count > 0)
            {
                var ids = "";
                var vals = "";

                variants = variants.OrderBy(x => x.AttributeId).Where(x => x.AttributeValueId > 0).ToList();

                // iterate each value
                foreach (var variant in variants)
                {
                    // add with comma in list
                    ids += variant.AttributeValueId + ",";
                    vals += new AttributeValuesManager().GetByKey(variant.AttributeValueId).Value + ",";
                }
                compiledValues.Add(vals.Substring(0, vals.Length - 1));
                compiled.Add(ids.Substring(0, ids.Length - 1));
            }
            
        }

        // RUN
        var items = new ItemAttributesValuesManager().GetByFilter(filter, "");

        // group attributes
        var attributes = items.GroupBy(x => x.AttributeId).Select(y => new PigeonCms.Attribute() { Id = y.Key }).OrderBy(x => x.Id).ToList();

        // string list splitted by comma
        var firstListIds = new List<string>();
        var firstListValues = new List<string>();

        foreach (var attribute in attributes)
        {
            // get all values from attribute
            var filterValue = new PigeonCms.AttributeValueFilter();
            filterValue.AttributeId = attribute.Id;
            var attributeValues = new PigeonCms.AttributeValuesManager().GetByFilter(filterValue, "");

            // keep only selected attributes value
            attributeValues = (List<PigeonCms.AttributeValue>)attributeValues.Where(x => items.Any(x2 => x2.AttributeValueId == x.Id)).ToList();

            // where store ids and values to be added
            var secondListIds = new List<string>();
            var secondListValues = new List<string>();

            // prepare new ids and values
            foreach (var attributeValue in attributeValues)
            {
                secondListIds.Add(attributeValue.Id.ToString());
                secondListValues.Add(attributeValue.Value.ToString());
            }

            // if in firstList are values, i get one by one adding for each row
            // my new ids and values from secondList
            // then i replace first with second updating datas
            if (firstListIds.Count > 0)
            {
                var tempListIds = new List<string>();
                foreach (string secondIds in secondListIds)
                {
                    foreach (string firstIds in firstListIds)
                    {
                        tempListIds.Add(firstIds + "," + secondIds);
                    }
                }
                firstListIds = tempListIds;
            }
            else
            {
                firstListIds = secondListIds;
            }

            if (firstListValues.Count > 0)
            {
                var tempListValue = new List<string>();
                foreach (string secondValues in secondListValues)
                {
                    foreach (string firstValues in firstListValues)
                    {
                        tempListValue.Add(firstValues + "," + secondValues);
                    }
                }
                firstListValues = tempListValue;
            } 
            else 
            {
                firstListValues = secondListValues;
            }

        }

        // transform in list of list string
        // to be returned in object and not in one value with comma
        var listids = new List<List<string>>();
        var listvalues = new List<List<string>>();

        // exclude list previously compiled
        firstListIds = firstListIds.Except(compiled).ToList();
        firstListValues = firstListValues.Except(compiledValues).ToList();

        // transform now to int list
        foreach (string rowIds in firstListIds)
        {
            var row = rowIds.Split(',').ToList();
            listids.Add(row);
        }
        // transform now to string list
        foreach (string rowValues in firstListValues)
        {
            var row = rowValues.Split(',').ToList();
            listvalues.Add(row);
        }

        // prepare result
        var result = new
        {
            ListIds = listids,
            ListValues = listvalues
        };

        // return in json format
        return toJson(result);

    }

    /// <summary>
    /// Get only custom value
    /// Prendo solo i campi custom
    /// OK
    /// </summary>
    /// <param name="itemId"></param>
    /// <returns></returns>
    [PigeonCms.UserControlScriptMethod]
    public static List<object> GetCustomValues(int itemId)
    {
        // declarations 
        // dichiarazione
        var filter = new ItemAttributeValueFilter();
        var man = new ItemAttributesValuesManager();

        // get only custom fields of referredID
        // prendo solo i campi custom di un determinato referredId
        filter.OnlyCustomFields = true;
        filter.Referred = itemId;
        var items = man.GetByFilter(filter, "");

        // take only unassigned items
        // prendo solo gli elementi non assegnati
        items = items.Where(x => x.ItemId == 0).ToList();

        var iavs = new List<object>();

        foreach (var item in items)
        {
            var iav = new {
                CustomValue = item.CustomValueString,
                Name = new PigeonCms.AttributesManager().GetByKey(item.AttributeId).Name,
                Id = item.AttributeId
            };

            iavs.Add(iav);
        }

        return iavs;

    }


    /// <summary>
    /// Save the variant creating a new item with threadId referred to parent element.
    /// </summary>
    /// <param name="itemId"></param>
    /// <param name="attributeId"></param>
    /// <param name="attributeValueId"></param>
    /// <returns></returns>
    [PigeonCms.UserControlScriptMethod]
    public static int SaveVariant(int itemId, string attributesValuesId, string defaults, string formFields, string customFields, int variantId)
    {

        // serialize JSON in fake product
        var serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        var values = serializer.Deserialize<List<Dictionary<string, string>>>(formFields)[0];
        var customs = serializer.Deserialize <List<Dictionary<string, string>>>(customFields);

        var filter = new ItemAttributeValueFilter();

        var listDefault = defaults.Split(',').ToList();
        var toStore = attributesValuesId.Split(',').ToList();

        bool isDefault = defaults == attributesValuesId;

        // check if the default value was previously selected

        ProductItem childProduct = null;
        // get the parent, to update o to duplicate as child
        ProductItem product = new ProductItemsManager().GetByKey(itemId);
        int theId = 0;

        // we never inserted this variant before
        if (variantId == 0)
        {
            // if is default, father product id 
            if (isDefault)
            {
                theId = itemId;
            }
            // create a child if not !
            else
            {
                product.Id = 0;
                product.ThreadId = itemId;

                childProduct = new ProductItemsManager().Insert(product);
                theId = childProduct.Id;
            }

            // compile product with form data
            product = new ProductItemsManager().GetByKey(theId);

            var productCode = values.ElementAt(0).Value;
            var availabilty = values.ElementAt(1).Value;
            decimal price = 0m;
            if (!string.IsNullOrEmpty(values.ElementAt(2).Value))
            {
                decimal.TryParse(values.ElementAt(2).Value.Replace(".", ","), out price);
            }

            decimal offerPrice = 0m;
            if (!string.IsNullOrEmpty(values.ElementAt(3).Value))
            {
                decimal.TryParse(values.ElementAt(3).Value.Replace(".", ","), out offerPrice);
            }

            decimal weight = 0m;
            if (!string.IsNullOrEmpty(values.ElementAt(4).Value))
            {
                decimal.TryParse(values.ElementAt(4).Value.Replace(".", ","), out weight);
            }

            var dimensions = values.ElementAt(5).Value;


            product.ProductCode = productCode;
            product.Availability = Convert.ToInt32(availabilty);
            product.RegularPrice = price;
            product.SalePrice = offerPrice;
            product.Weight = weight;
            product.Dimensions = dimensions;

            // update
            new ProductItemsManager().Update(product);

            // store the variants ! assign the id of product to itemId if is 0, else create new record
            foreach (var store in toStore)
            {
                int attributeValueId = Convert.ToInt32(store);
                var attributeValue = new AttributeValuesManager().GetByKey(attributeValueId);
                filter.Referred = itemId;
                filter.AttributeId = attributeValue.AttributeId;
                filter.AttributeValueId = attributeValue.Id;
                var itemAttrVal = new ItemAttributesValuesManager().GetByFilter(filter, "").First();
                if (itemAttrVal != null)
                {

                    if (itemAttrVal.ItemId == 0)
                    {
                        //update
                        itemAttrVal.ItemId = theId;
                        new ItemAttributesValuesManager().Update(itemAttrVal);
                    }
                    else if (itemAttrVal.ItemId > 0)
                    {
                        //duplica
                        itemAttrVal.ItemId = theId;
                        new ItemAttributesValuesManager().Insert(itemAttrVal);
                    }

                }

            }

        }
        else
        {
            // we previously inserted this variant so simply update product
            product = new ProductItemsManager().GetByKey(variantId);

            theId = product.Id;

            var productCode = values.ElementAt(0).Value;
            var availabilty = values.ElementAt(1).Value;
            decimal price = 0m;
            if (!decimal.TryParse(values.ElementAt(2).Value.Replace(".", ","), out price))
            {
                price = 0;
            }
            
            decimal offerPrice = 0m;
            if(!decimal.TryParse(values.ElementAt(3).Value.Replace(".", ","), out offerPrice))
            {
                offerPrice = 0;
            }

            decimal weight = 0m;
            if (!decimal.TryParse(values.ElementAt(4).Value.Replace(".", ","), out weight))
            {
                weight = 0;
            }

            var dimensions = values.ElementAt(5).Value;

            product.ProductCode = productCode;
            product.Availability = Convert.ToInt32(availabilty);
            product.RegularPrice = price;
            product.SalePrice = offerPrice;
            product.Weight = weight;
            product.Dimensions = dimensions;

            new ProductItemsManager().Update(product);

        }

        foreach (var custom in customs)
        {
            filter = new ItemAttributeValueFilter();
            filter.OnlyCustomFields = true;
            filter.ItemId = theId;
            filter.AttributeId = Convert.ToInt32(custom["Id"]);
            var customField = new ItemAttributesValuesManager().GetByFilter(filter, "");
            if (customField == null || customField.Count == 0)
            {
                var insert = new ItemAttributeValue();
                insert.Referred = theId;
                insert.ItemId = theId;
                insert.AttributeId = Convert.ToInt32(custom["Id"]);
                insert.CustomValueString = custom["Value"];
                insert.AttributeValueId = 0;
                new ItemAttributesValuesManager().Insert(insert);
            }
            else
            {
               
                var update = customField.First();
                update.CustomValueString = custom["Value"];
                new ItemAttributesValuesManager().Update(update);
            }
        }

        // TODO return message
        return theId;

    }


    /// <summary>
    /// Show at tab variant the compiled variants
    /// </summary>
    /// <param name="itemId"></param>
    /// <returns></returns>
    [PigeonCms.UserControlScriptMethod]
    public static List<object> ShowVariants(int itemId)
    {
        // now have to compose the list with the couple of ids compiled, so take all variants productitem
        var productfilter = new ProductItemFilter();
        productfilter.ThreadId = itemId;
        productfilter.ShowOnlyRootItems = false;
        var productitems = new ProductItemsManager().GetByFilter(productfilter, "");

        var variantsList = new List<object>();

        // iterate them
        foreach (var productitem in productitems)
        {
            // check if are compiled
            var variants = new ItemAttributesValuesManager().GetByItemId(productitem.Id);
            if (variants != null && variants.Count > 0)
            {

                var valuesDims = productitem.Dimensions.Split(',');

                var vals = new
                {
                    DimL = valuesDims[0],
                    DimW = valuesDims[1],
                    DimH = valuesDims[2]
                };

                // get all values of variants and compile in object

                var product = new
                {
                    Id = productitem.Id,
                    ProductCode = productitem.ProductCode,
                    Availability = productitem.Availability,
                    RegularPrice = productitem.RegularPrice,
                    SalePrice = productitem.SalePrice,
                    Weight = productitem.Weight,
                    Dimensions = vals,
                    Photos = productitem.Images
                };

                var ids = new List<string>();
                var values = new List<string>();

                variants = variants.OrderBy(x => x.AttributeId).ToList();

                // add ids and values to know the information of each box
                foreach (var variant in variants)
                {
                    ids.Add(variant.AttributeValueId.ToString());
                    var value = new PigeonCms.AttributeValuesManager().GetByKey(variant.AttributeValueId);
                    values.Add(value.Value);
                }

                var filter = new ItemAttributeValueFilter();
                filter.Referred = itemId;
                filter.OnlyCustomFields = true;
                filter.ItemId = productitem.Id;
                var customFields = new ItemAttributesValuesManager().GetByFilter(filter, "");

                var custAttr = new List<object>();

                foreach (var customField in customFields)
                {
                    var attribute = new
                    {
                        Name = new PigeonCms.AttributesManager().GetByKey(customField.AttributeId).Name,
                        CustomValue = customField.CustomValueString,
                        Id = customField.AttributeId
                    };
                    custAttr.Add(attribute);
                }

                var info = new
                {
                    ListIds = ids,
                    ListValues = values
                };

                var singleVariant = new
                {
                    Product = product,
                    Info = info,
                    CustomFields = custAttr
                };

                // create the object with variantInfo and Datainfo
                variantsList.Add(singleVariant);
            }

        }

        return variantsList;
    }

    /// <summary>
    /// Delete variant previously inserted.
    /// </summary>
    /// <param name="itemId"></param>
    /// <param name="attributesValuesId"></param>
    /// <param name="variantId"></param>
    /// <returns></returns>
    [PigeonCms.UserControlScriptMethod]
    public static string DeleteVariant(int itemId, string attributesValuesId, int variantId)
    {
        if (variantId == 0)
        {
            return "false";
        }

        // make a list with AttributeValue ids to delete
        var toDelete = attributesValuesId.Split(',').ToList();

        // iterate on them
        foreach (var delete in toDelete)
        {
            // check if we have the value with 0 at id and itemId at referred, if we don't make this control
            // the application will delete the association with attribute.
            // If we have, simply delete the record.
            var filter = new ItemAttributeValueFilter();
            filter.ItemId = 0;
            filter.AttributeValueId = Convert.ToInt32(delete);
            filter.Referred = itemId;
            var items = new ItemAttributesValuesManager().GetByFilter(filter, "");
            ItemAttributeValue present = null;
            // check if list is not null, and take the record we would to check
            if (items != null && items.Count > 0)
            {
                present = items.First();
            }
            // not present, simply put 0 at itemId to not lost the association with item and attribute
            if (present == null)
            {
                filter.ItemId = variantId;
                var temp = new ItemAttributesValuesManager().GetByFilter(filter, "");
                if (temp != null && temp.Count > 0)
                {
                    var update = temp.First();
                    update.ItemId = 0;
                    new ItemAttributesValuesManager().Update(update);
                }
            }
            else
            {
                // brutally delete ! :D
                new ItemAttributesValuesManager().Delete(variantId, 0, Convert.ToInt32(delete), itemId);
            }
           

        }

        //don't delete if is the parentItem cause it' will delete the product ! :o
        if (itemId != variantId)
        {
            new ProductItemsManager().DeleteById(variantId);
        }

        // TODO return message
        return "true";
    }

    /// <summary>
    /// Get all items and filter with linq with typed characters
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    [PigeonCms.UserControlScriptMethod]
    public static List<object> GetRelatedProductSearch(string type, int itemId)
    {
        var items = new ProductItemsManager().GetByFilter(new ProductItemFilter(), "");
        items = items.Where(x => x.Id != itemId).ToList();
        var presents = new ProductItemsManager().getRelatedByKey(itemId);
        var except = items.Where(x => !presents.Any(x2 => x2.Id == x.Id)).ToList();
        var prods = except.Select(x => x).Where(x => x.Title.ToLower().Contains(type.ToLower())).ToList();
        var result = new List<object>();
        foreach (var prod in prods)
        {
            var product = new
            {
                label = prod.Title,
                value = prod.Id
            };

            result.Add(product);
        }
        return result;
    }
    /// <summary>
    /// Get related compiled previously
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    [PigeonCms.UserControlScriptMethod]
    public static List<object> GetRelatedById(int itemId)
    {
        var result = new List<object>();
        var items = new ProductItemsManager().getRelatedByKey(itemId);
        foreach(var item in items) {
            var prod = new {
                id = item.Id,
                name = item.Title
            };

            result.Add(prod);
        }
        return result;
    }



    /// <summary>
    /// Refresh gallery on images upload (popup close)
    /// </summary>
    /// <param name="itemId"></param>
    /// <returns></returns>
    [PigeonCms.UserControlScriptMethod]
    public static List<FileMetaInfo> RefreshGalleryById(int itemId)
    {
        var item = new ProductItemsManager().GetByKey(itemId);
        return item.Images;
    }

    /// <summary>
    /// Get a list of related products
    /// </summary>
    /// <param name="itemId"></param>
    /// <returns></returns>
    [PigeonCms.UserControlScriptMethod]
    public List<ProductItem> GetRelatedList(int itemId)
    {
        var item = new ProductItemsManager().getRelatedByKey(itemId);
        return item;
    }

    /// <summary>
    /// Delete related on close icon
    /// </summary>
    /// <param name="itemId"></param>
    /// <param name="relatedId"></param>
    [PigeonCms.UserControlScriptMethod]
    public static void DeleteRelated(int itemId, int relatedId)
    {
        new ProductItemsManager().deleteRelated(itemId, relatedId);
    }

    /// <summary>
    /// Delete related on close icon
    /// </summary>
    /// <param name="itemId"></param>
    /// <param name="relatedId"></param>    
    [PigeonCms.UserControlScriptMethod]
    public static object RemoveCustom(int attributeId, int itemId)
    {
        var filter = new ItemAttributeValueFilter();
        filter.OnlyCustomFields = true;
        filter.Referred = itemId;
        filter.AttributeId = attributeId;
        var customField = new ItemAttributesValuesManager().GetByFilter(filter, "");
        customField = customField.Where(x => x.ItemId > 0).ToList();
        if (customField == null || customField.Count == 0) {
            new ItemAttributesValuesManager().Delete(0, attributeId, 0, itemId);

            var success = new
            {
                success = true,
                message = "Successfully removed."
            };

            return success;

        }
        else
        {
            var error = new
            {
                success = false,
                message = "You have assigned variants with this attribute, can't delete it."
            };

            return error;
        }
    }
    
    /// <summary>
    /// set related on autocompile form
    /// </summary>
    /// <param name="itemId"></param>
    /// <param name="relatedId"></param>
    [PigeonCms.UserControlScriptMethod]
    public static void SetRelatedProducts(int itemId, int relatedId)
    {
        new ProductItemsManager().setRelated(itemId, relatedId);
    }

    /// <summary>
    /// Convert a json string into Dictionary<string, string>
    /// </summary>
    /// <param name="json"></param>
    /// <returns></returns>
    protected static Dictionary<int, string> toDictionary(string json)
    {
        var serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        return serializer.Deserialize<Dictionary<int, string>>(json);
    }

    /// <summary>
    /// Convert a Dictionary<string,string> into Json string
    /// </summary>
    /// <param name="dictionary"></param>
    /// <returns></returns>
    protected static string toJson(Dictionary<int, string> dictionary)
    {
        var serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        return serializer.Serialize(dictionary);
    }

    protected static string toJson(Object result)
    {
        var serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        return serializer.Serialize(result);
    }

    #endregion

}
