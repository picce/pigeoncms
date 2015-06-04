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
        Grid1.Columns[COL_ACCESSTYPE_INDEX].Visible = this.ShowSecurity;
        Grid1.Columns[COL_ACCESSLEVEL_INDEX].Visible = this.ShowSecurity;
        Grid1.Columns[COL_ID_INDEX].Visible = this.ShowSecurity;
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
            this.BaseModule.CurrViewFolder + "/app.js"));

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

    //protected void DropNew_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    if (!checkAddNewFilters())
    //        Utility.SetDropByValue(DropNew, "");
    //    else
    //    {
    //        try { editRow(0); }
    //        catch (Exception e1) { LblErr.Text = RenderError(e1.Message); }
    //    }
    //}

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

        //if (DropItemTypesFilter.SelectedValue != "")
        //    filter.ItemType = DropItemTypesFilter.SelectedValue;
        //if (this.ItemId > 0)
        //    filter.Id = this.ItemId;

        //int secId = -1;
        //int.TryParse(DropSectionsFilter.SelectedValue, out secId);

        //int catId = -1;
        //int.TryParse(DropCategoriesFilter.SelectedValue, out catId);


        //if (base.SectionId > 0)
        //    filter.SectionId = base.SectionId;
        //else
        //    filter.SectionId = secId;
        //filter.CategoryId = catId;

        //filter.ShowOnlyRootItems = false;

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

            //permissions
            //read
            string readAccessLevel = item.ReadAccessCode;
            if (item.ReadAccessLevel > 0)
                readAccessLevel += " " + item.ReadAccessLevel.ToString();
            if (!string.IsNullOrEmpty(readAccessLevel))
                readAccessLevel = " - " + readAccessLevel;

            //write
            string writeAccessLevel = item.WriteAccessCode;
            if (item.WriteAccessLevel > 0)
                writeAccessLevel += " " + item.WriteAccessLevel.ToString();
            if (!string.IsNullOrEmpty(writeAccessLevel))
                writeAccessLevel = " - " + writeAccessLevel;

            Literal LitAccessTypeDesc = (Literal)e.Row.FindControl("LitAccessTypeDesc");
            //read
            LitAccessTypeDesc.Text = item.ReadAccessType.ToString();
            if (item.ReadAccessType != MenuAccesstype.Public)
            {
                string roles = "";
                foreach (string role in item.ReadRolenames)
                {
                    roles += role + ", ";
                }
                if (roles.EndsWith(", ")) roles = roles.Substring(0, roles.Length - 2);
                if (roles.Length > 0)
                    roles = " (" + roles + ")";
                LitAccessTypeDesc.Text += Utility.Html.GetTextPreview(roles, 60, "");
                LitAccessTypeDesc.Text += readAccessLevel;
            }
            if (LitAccessTypeDesc.Text != "") LitAccessTypeDesc.Text += "<br />";
            //write
            LitAccessTypeDesc.Text += item.WriteAccessType.ToString();
            if (item.WriteAccessType != MenuAccesstype.Public)
            {
                string roles = "";
                foreach (string role in item.WriteRolenames)
                {
                    roles += role + ", ";
                }
                if (roles.EndsWith(", ")) roles = roles.Substring(0, roles.Length - 2);
                if (roles.Length > 0)
                    roles = " (" + roles + ")";
                LitAccessTypeDesc.Text += Utility.Html.GetTextPreview(roles, 60, "");
                LitAccessTypeDesc.Text += writeAccessLevel;
            }


            //files upload
            var LnkUploadFiles = (HyperLink)e.Row.FindControl("LnkUploadFiles");
            LnkUploadFiles.NavigateUrl = this.FilesUploadUrl
                + "?type=items&id=" + item.Id.ToString();
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
            var o1 = new ProductItem();

            if (CurrentId == 0)
            {
                form2obj(o1);
                o1 = new ProductItemsManager().Insert(o1);
            }
            else
            {
                o1 = new ProductItemsManager().GetByKey(CurrentId);  //precarico i campi esistenti e nn gestiti dal form
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
        //TxtAvailability.Text = "";
        //TxtProductCode.Text = "";
        //TxtOfferPrice.Text = "";
        //TxtPrice.Text = "";
        //TxtWeight.Text = "";
        //TxtDimL.Text = "";
        //TxtDimW.Text = "";
        //TxtDimH.Text = "";
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
        //obj.ProductCode = TxtProductCode.Text;
        //obj.RegularPrice = decimal.Parse(TxtPrice.Text);
        //obj.SalePrice = decimal.Parse(TxtOfferPrice.Text);
        //obj.Availability = int.Parse(TxtAvailability.Text);
        //obj.Weight = decimal.Parse(TxtWeight.Text);

        string diml, dimw, dimh;

        //diml = (string.IsNullOrEmpty(TxtDimL.Text)) ? "0" : TxtDimL.Text;
        //dimw = (string.IsNullOrEmpty(TxtDimW.Text)) ? "0" : TxtDimW.Text;
        //dimh = (string.IsNullOrEmpty(TxtDimH.Text)) ? "0" : TxtDimH.Text;

        //obj.Dimensions = diml + "," + dimw + "," + dimh;

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
            notSavedNav.Visible = false;
            plhTabContent.Visible = false;
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
        }
        else
        {
            obj = new ProductItemsManager().GetByKey(CurrentId);
            //if (!obj.IsThreadRoot)
            //{
            //    //onlyIfRoot.Visible = false;
            //    DropCategories.Enabled = false;
            //    TxtAlias.Enabled = false;
            //}
            //else
            //{
            //    //onlyIfRoot.Visible = true;
            //    DropCategories.Enabled = true;
            //    TxtAlias.Enabled = true;
            //}
            notSavedNav.Visible = true;
            plhTabContent.Visible = true;
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
        //DropCategories.Items.Add(new ListItem("", "0"));  //mandatory category

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

        //DropNew.Items.Clear();
        //DropNew.Items.Add(new ListItem(Utility.GetLabel("LblCreateNew", "Create new"), ""));

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
    /// </summary>
    /// <param name="itemId"></param>
    /// <returns></returns>
    [PigeonCms.UserControlScriptMethod]
    public static List<PigeonCms.Attribute> GetAttributes(int itemId)
    {
        // get all attributes referred to itemId
        var items = new ItemAttributesValuesManager().GetByReferredId(itemId);

         var attributesId = new List<int>();

        if (items != null && items.Count > 0)
        {
            // extract only the attributeId from the ItemAttributesValue List
            attributesId = items.GroupBy(x => x.AttributeId).Select(y => new PigeonCms.Attribute() { Id = y.Key }).ToList().Select(x => x.Id).ToList();
        }


        // get all attributes
        var allAttributes = new List<PigeonCms.Attribute>();
        // filter the attribute for item type !!
        //var filter = new AttributeFilter();
        //filter.AttributeType = "";
        allAttributes = new PigeonCms.AttributesManager().GetByFilter(new AttributeFilter(), "");
        
        //remove from list all attribute that have the same Id as the list above
        var res = (List<PigeonCms.Attribute>)allAttributes.Where(x => !attributesId.Any(x2 => x2 == x.Id)).ToList();
        return res;
    }

    /// <summary>
    /// get attributeValue from given AttributeId
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [PigeonCms.UserControlScriptMethod]
    public static List<PigeonCms.AttributeValue> GetAttributeValues(int id)
    {
        // get values
        var myValues = new List<PigeonCms.AttributeValue>();
        PigeonCms.AttributeValueFilter filter = new PigeonCms.AttributeValueFilter();
        filter.AttributeId = id;
        myValues = new PigeonCms.AttributeValuesManager().GetByFilter(filter, "");

        return myValues;
    }

    /// <summary>
    /// Save checkbox forms of attributes tab.
    /// It takes a JSON with checked checkboxes, excluding the itemAttributesValues saved 
    /// it generate 2 list, one to insert, one to exclude.
    /// You can't remove a variant with assigned itemId, only with referred and itemId = 0.
    /// </summary>
    /// <param name="jsonArr"></param>
    /// <param name="itemId"></param>
    /// <returns></returns>
    [PigeonCms.UserControlScriptMethod]
    public static string SaveAttributeValues(string jsonArr, int itemId)
    {
        // serialize JSON in ItemAttributeValue List
        var serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        var values = serializer.Deserialize<List<PigeonCms.ItemAttributeValue>>(jsonArr);

        // declare product objects
        var man = new PigeonCms.Shop.ProductItemsManager();
        var prodfilter = new ProductItemFilter();

        // declare itemAttributeValue objects
        var itemAttr = new PigeonCms.ItemAttributeValue();
        var filter = new PigeonCms.ItemAttributeValueFilter();

       // get all products (also child of itemId)
        prodfilter.ThreadId = itemId;
        prodfilter.ShowOnlyRootItems = false;
        var products = man.GetByFilter(prodfilter, "");

        // get all actual attributes
        var actualAttributes = new ItemAttributesValuesManager().GetByReferredId(itemId);

        var toDelete = new List<ItemAttributeValue>();

        if (actualAttributes != null && actualAttributes.Count > 0)
        {
            // exclude to actualAttributes the new Values, if values are less than actualAttributes, the exclusion will be deleted
            toDelete = actualAttributes.Except(values).ToList();
        }

        var toInsert  = values;

        if (actualAttributes != null && actualAttributes.Count > 0)
        {
            // exclude to new Values the nactualAttributes, if actualAttributes are less than values, the exclusion will be inserted
            toInsert = values.Except(actualAttributes).ToList();
        }


        //insert
        foreach (ItemAttributeValue insert in toInsert)
        {
            new PigeonCms.ItemAttributesValuesManager().Insert(insert);
        }

        //delete
        foreach (ItemAttributeValue delete in toDelete)
        {
            // TODO can't delete if assigned
            if (delete.ItemId > 0)
            {
                // return success message
                var error = new
                {
                    success = false,
                    message = "You have assigned variants with this attribute, can't delete it."
                };

                return toJson(error);

            }
            else
            {
                new PigeonCms.ItemAttributesValuesManager().Delete(delete.ItemId, delete.AttributeId, delete.AttributeValueId, delete.Referred);
            }
            
        }

        // return success message
        var success = new
        {
            success = true,
            message = "All savings done well."
        };

        // return in JSON format
        return toJson(success);

    }

    [PigeonCms.UserControlScriptMethod]
    public static string GetAttributeValuesForVariants(int itemId)
    {
        // filter for ItemAttributeValue
        var filter = new ItemAttributeValueFilter();
        // only referred to itemId
        filter.Referred = itemId;
        // exclude related variants
        //filter.ItemId = 0;



        // RUN
        var referredItemAttrVals = new ItemAttributesValuesManager().GetByFilter(filter, "");

        // get a list with attributes (to separe select object)
        var attributes = referredItemAttrVals.GroupBy(x => x.AttributeId).Select(y => new PigeonCms.Attribute() { Id = y.Key }).ToList();

        //prepare result
        var result = new List<object>();

        foreach (var attribute in attributes)
        {
            // get all itemId referred itemAttributeValue of attribute 
            var filterValue = new PigeonCms.AttributeValueFilter();
            filterValue.AttributeId = attribute.Id;
            var attributeValues = new PigeonCms.AttributeValuesManager().GetByFilter(filterValue, "");

            // keep only value who has same id as the list with only selected user values
            attributeValues = (List<PigeonCms.AttributeValue>)attributeValues.Where(x => referredItemAttrVals.Any(x2 => x2.AttributeValueId == x.Id)).ToList();

            var attributeObject = new List<object>();

            foreach (var attributeValue in attributeValues)
            {
                var filterItemAttVal = new ItemAttributeValueFilter();
                filterItemAttVal.ItemId = itemId;
                filterItemAttVal.AttributeId = attributeValue.AttributeId;
                filterItemAttVal.AttributeValueId = attributeValue.Id;
                var itemAttributeValue = new ItemAttributesValuesManager().GetByFilter(filterItemAttVal, "");
                string isSelected = "";
                if(itemAttributeValue != null && itemAttributeValue.Count > 0) {
                    var item = itemAttributeValue.First();
                    isSelected = (item.AttributeValueId == attributeValue.Id) ? "selected" : "";
                }
                
                //element base
                var infoValues = new
                {
                    //attrId = referreditemVal.AttributeId,
                    attrValId = attributeValue.Id,
                    //attribute = attribute.Name,
                    attributeValue = attributeValue.Value,
                    selected = isSelected
                };

                attributeObject.Add(infoValues);

            }

            var infoAttribute = new
            {
                attrId = attribute.Id,
                attribute = new PigeonCms.AttributesManager().GetByKey(attribute.Id).Name,
            };

            attributeObject.Add(infoAttribute);

            //add element to result
            result.Add(attributeObject);
            //result.Add(infoAttribute);
        }

        //convert in json and return
        return toJson(result);
    }
    
    /// <summary>
    /// Compile the select box used to select default values
    /// </summary>
    /// <param name="itemId"></param>
    /// <returns></returns>
    [PigeonCms.UserControlScriptMethod]
    public static string CompileAttributes(int itemId)
    {
        // get all referred attributes
        var items = new ItemAttributesValuesManager().GetByReferredId(itemId);

        var attributesId = new List<int>();

        // if not nulll retrieve only AttributesId form list 
        if (items != null && items.Count > 0)
        {
            attributesId = items.GroupBy(x => x.AttributeId).Select(y => new PigeonCms.Attribute() { Id = y.Key } ).ToList().Select(x => x.Id).ToList();
        }

        var attributesValues = new List<PigeonCms.AttributeValue>();

        var success = new List<object>();

        // iterate attributesId
        foreach (int attributeId in attributesId)
        {
            // get the record of AttributeValue having attributeId
            var filter = new AttributeValueFilter();
            filter.AttributeId = attributeId;
            attributesValues = new PigeonCms.AttributeValuesManager().GetByFilter(filter, "");

            // add the name of attribute on a list
            var singleAtt = new List<object>();
            singleAtt.Add( new AttributesManager().GetByKey(attributeId).Name );

            // iterate all values of attributeId
            foreach (PigeonCms.AttributeValue attrVal in attributesValues)
            {
                // get the element and put value checked if it is (if in initial list is present one of these values)
                int index = items.Select(x => x.AttributeValueId).ToList().IndexOf(attrVal.Id);
                string valueIn = (index > -1) ? "checked='true'" : "";

                // create object to add in return list
                var obj = new
                {
                    Id = attrVal.Id,
                    Value = attrVal.Value,
                    AttributeId = attrVal.AttributeId,
                    Checked = valueIn
                };
                singleAtt.Add(obj);
            }
            success.Add(singleAtt);
        }

        return toJson(success);
    }

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
    /// Save the variant creating a new item with threadId referred to parent element.
    /// </summary>
    /// <param name="itemId"></param>
    /// <param name="attributeId"></param>
    /// <param name="attributeValueId"></param>
    /// <returns></returns>
    [PigeonCms.UserControlScriptMethod]
    public static int SaveVariant(int itemId, string attributesValuesId, string defaults, string formFields, int variantId)
    {

        // serialize JSON in fake product
        var serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        var values = serializer.Deserialize<List<Dictionary<string, string>>>(formFields)[0];

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
            decimal.TryParse(values.ElementAt(2).Value.Replace(".", ","), out price);
            decimal offerPrice = 0m;
            decimal.TryParse(values.ElementAt(3).Value.Replace(".", ","), out offerPrice);
            decimal weight = 0m;
            decimal.TryParse(values.ElementAt(4).Value.Replace(".", ","), out weight);
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
                        //itemAttrVal.ItemId = theId;
                        new ItemAttributesValuesManager().UpdateItemId(itemAttrVal, theId);
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
            decimal.TryParse(values.ElementAt(2).Value.Replace(".", ","), out price);
            decimal offerPrice = 0m;
            decimal.TryParse(values.ElementAt(3).Value.Replace(".", ","), out offerPrice);
            decimal weight = 0m;
            decimal.TryParse(values.ElementAt(4).Value.Replace(".", ","), out weight);
            var dimensions = values.ElementAt(5).Value;

            product.ProductCode = productCode;
            product.Availability = Convert.ToInt32(availabilty);
            product.RegularPrice = price;
            product.SalePrice = offerPrice;
            product.Weight = weight;
            product.Dimensions = dimensions;

            new ProductItemsManager().Update(product);

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
                // get all values of variants and compile in object
                var product = new
                {
                    Id = productitem.Id,
                    ProductCode = productitem.ProductCode,
                    Availability = productitem.Availability,
                    RegularPrice = productitem.RegularPrice,
                    SalePrice = productitem.SalePrice,
                    Weight = productitem.Weight,
                    Dimensions = productitem.Dimensions
                };

                var ids = new List<string>();
                var values = new List<string>();

                // add ids and values to know the information of each box
                foreach (var variant in variants)
                {
                    ids.Add(variant.AttributeValueId.ToString());
                    var value = new PigeonCms.AttributeValuesManager().GetByKey(variant.AttributeValueId);
                    values.Add(value.Value);
                }

                var info = new
                {
                    ListIds = ids,
                    ListValues = values
                };

                var singleVariant = new
                {
                    Product = product,
                    Info = info
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
                    new ItemAttributesValuesManager().UpdateItemId(update, 0);
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
