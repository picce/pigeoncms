using System;
using System.Data;
using System.Linq;
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
using System.Globalization;
using PigeonCms;
using PigeonCms.Core.Helpers;
using PigeonCms.Controls;


public partial class Controls_ItemsAdmin : PigeonCms.ItemsAdminControl
{
    const int COL_ALIAS_INDEX = 1;
    const int COL_CATEGORY_INDEX = 3;
    const int COL_ACCESSTYPE_INDEX = 4;
    const int COL_ORDER_ARROWS_INDEX = 6;
    const int COL_UPLOADFILES_INDEX = 7;
    const int COL_UPLOADIMAGES_INDEX = 8;
    const int COL_DELETE_INDEX = 9;

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

    protected int CurrentSectionId
    {
        get
        {
            int res = 0;
            if (this.SectionId > 0)
                res = this.SectionId;
            else
                int.TryParse(DropSectionsFilter.SelectedValue, out res);
            return res;
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
            if (new ItemsManager<Item, ItemsFilter>(true, true).GetByKey(base.CurrItem.Id).Id == 0)
                throw new ArgumentException();
        }

        //Tree1.NodeClick += new PigeonCms.Modules.CategoriesAdminControl.NodeClickDelegate(Tree_NodeClick);
        //initTree();

        if (!Page.IsPostBack)
        {
            loadDropEnabledFilter();
            loadDropSectionsFilter(base.SectionId);
            {
                int secId = -1;
                int.TryParse(DropSectionsFilter.SelectedValue, out secId);
                loadDropCategoriesFilter(secId);
            }
            //Tree1.BindTree(this.CurrentSectionId);
            loadDropsItemTypes();
        }
        else
        {
            string eventArg = HttpContext.Current.Request["__EVENTARGUMENT"];
            if (eventArg == "items")
                Grid1.DataBind();

            //reload params on every postback, because cannot manage dinamically fields
            var currentItem = new PigeonCms.Item();
            if (CurrentId > 0)
            {
                currentItem = new ItemsManager<Item, ItemsFilter>(true, true).GetByKey(CurrentId);
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

    //protected void Tree_NodeClick(object sender, PigeonCms.Modules.CategoriesAdminControl.NodeClickEventArgs e)
    //{
    //    LblOk.Text = RenderSuccess(e.Command.ToString() + " " + e.CategoryId.ToString());

    //    switch (e.Command)
    //    {
    //        case PigeonCms.Modules.CategoriesAdminControl.NodeClickCommandEnum.Select:
    //            LblOk.Text = RenderSuccess(e.Command.ToString() + " " + e.CategoryId.ToString());
    //            break;
    //    }

    //}

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
        //Tree1.BindTree(secID);
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

    protected void DropNew_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (!checkAddNewFilters())
            Utility.SetDropByValue(DropNew, "");
        else
        {
            try { editRow(0); }
            catch (Exception e1) { LblErr.Text = RenderError(e1.Message); }
        }
    }

    protected void BtnNew_Click(object sender, EventArgs e)
    {
        try
        {
            if (checkAddNewFilters())
            {
                Utility.SetDropByValue(DropNew, this.ItemType);
                editRow(0);
            }
        }
        catch (Exception e1) { LblErr.Text = RenderError(e1.Message); }
    }

    protected void ObjDs1_ObjectCreating(object sender, ObjectDataSourceEventArgs e)
    {
        var typename = new ItemsManager<Item, ItemsFilter>(true, true);
        e.ObjectInstance = typename;
    }

    protected void ObjDs1_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
    {
        //see http://msdn.microsoft.com/en-us/library/w3f99sx1.aspx
        //for use generics with ObjDs TypeName
        var filter = new ItemsFilter();

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

        if (DropItemTypesFilter.SelectedValue != "")
            filter.ItemType = DropItemTypesFilter.SelectedValue;
        if (this.ItemId > 0)
            filter.Id = this.ItemId;

        int secId = -1;
        int.TryParse(DropSectionsFilter.SelectedValue, out secId);

        int catId = -1;
        int.TryParse(DropCategoriesFilter.SelectedValue, out catId);


        if (base.SectionId > 0)
            filter.SectionId = base.SectionId;
        else
            filter.SectionId = secId;
        filter.CategoryId = catId;

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
            var item = (PigeonCms.Item)e.Row.DataItem;

            if (item.IsThreadRoot)
                lastRowDataboundRoot = e.Row;
            else
            {
                if (lastRowDataboundRoot != null)
                    e.Row.RowState = lastRowDataboundRoot.RowState; //keeps same style of thread root
            }

            var LnkTitle = (LinkButton)e.Row.FindControl("LnkTitle");
            LnkTitle.Text = "<i class='fa fa-pgn_edit fa-fw'></i>";
            if (!item.IsThreadRoot)
                LnkTitle.Text += "--";
            LnkTitle.Text += Utility.Html.GetTextPreview(item.Title, 50, "");
            if (string.IsNullOrEmpty(item.Title))
                LnkTitle.Text += Utility.GetLabel("NO_VALUE", "<no value>");

            if (base.ShowSecurity && Roles.IsUserInRole("debug"))
                LnkTitle.Text += " ["+ item.Id.ToString() +"]";

            var LitItemInfo = (Literal)e.Row.FindControl("LitItemInfo");
            if (!string.IsNullOrEmpty(item.ExtId))
                LitItemInfo.Text += "extId: " + item.ExtId + "<br>";
            if (this.ShowType)
                LitItemInfo.Text += item.ItemTypeName + "<br>";
            if (!string.IsNullOrEmpty(item.CssClass))
                LitItemInfo.Text += "class: " + item.CssClass;


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

        int catId = 0;
        int.TryParse(DropCategories.SelectedValue, out catId);
        if (catId <= 0)
        {
            res = false;
            err += base.GetLabel("ChooseCategory", "alias in use") + "<br>";
        }

        if (!string.IsNullOrEmpty(TxtAlias.Text))
        {
            var filter = new ItemsFilter();
            var list = new List<PigeonCms.Item>();

            filter.Alias = TxtAlias.Text;
            list = new ItemsManager<Item,ItemsFilter>().GetByFilter(filter, "");
            if (list.Count > 0)
            {
                if (this.CurrentId == 0)
                {
                    res = false;
                    err += "alias in use<br />";
                }
                else
                {
                    if (list[0].Id != this.CurrentId)
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
            var o1 = new Item();

            if (CurrentId == 0)
            {
                form2obj(o1);
                o1 = new ItemsManager<Item, ItemsFilter>().Insert(o1);
            }
            else
            {
                o1 = new ItemsManager<Item, ItemsFilter>().GetByKey(CurrentId);  //precarico i campi esistenti e nn gestiti dal form
                form2obj(o1);
                new ItemsManager<Item, ItemsFilter>().Update(o1);
            }
            removeFromCache();

            Grid1.DataBind();
            LblOk.Text = RenderSuccess(Utility.GetLabel("RECORD_SAVED_MSG"));
            res = true;
        }
        catch (CustomException e1)
        {
            if (e1.CustomMessage == ItemsManager<Item, ItemsFilter>.MaxItemsException)
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
        LitSection.Text = "";
        LitItemType.Text = "";
        LblCreated.Text = "";
        LblUpdated.Text = "";
        ChkEnabled.Checked = true;
        TxtAlias.Text = "";
        TxtCssClass.Text = "";
        TxtExtId.Text = "";
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

    private void form2obj(Item obj)
    {
        obj.Id = CurrentId;
        obj.Enabled = ChkEnabled.Checked;
        obj.TitleTranslations.Clear();
        obj.DescriptionTranslations.Clear();
        obj.CategoryId = int.Parse(DropCategories.SelectedValue);
        obj.Alias = TxtAlias.Text;
        obj.CssClass = TxtCssClass.Text;
        obj.ExtId = TxtExtId.Text;
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
        }
        obj.ItemParams = FormBuilder.GetParamsString(obj.ItemType.Params, ItemParams1);
        string fieldsString = FormBuilder.GetParamsString(obj.ItemType.Fields, ItemFields1);
        obj.LoadCustomFieldsFromString(fieldsString);
        PermissionsControl1.Form2obj(obj);
    }

    private void obj2form(Item obj)
    {
        LblId.Text = obj.Id.ToString();
        LblOrderId.Text = obj.Ordering.ToString();
        LblUpdated.Text = obj.DateUpdated.ToString() + " by " + obj.UserUpdated;
        LblCreated.Text = obj.DateInserted.ToString() + " by " + obj.UserInserted;
        ChkEnabled.Checked = obj.Enabled;
        TxtAlias.Text = obj.Alias;
        TxtCssClass.Text = obj.CssClass;
        TxtExtId.Text = obj.ExtId;
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
        LitSection.Text = obj.Category.Section.Title;
        LitItemType.Text = obj.ItemTypeName;
        
        this.ItemDate = obj.ItemDate;
        this.ValidFrom = obj.ValidFrom;
        this.ValidTo = obj.ValidTo;
    }

    private void editRow(int recordId)
    {
        var obj = new PigeonCms.Item();
        LblOk.Text = RenderSuccess("");
        LblErr.Text = RenderError("");

        if (!PgnUserCurrent.IsAuthenticated)
            throw new Exception("user not authenticated");

        clearForm();
        CurrentId = recordId;
        if (CurrentId == 0)
        {
            loadDropCategories(int.Parse(DropSectionsFilter.SelectedValue));
            obj.ItemTypeName = DropNew.SelectedValue;
            obj.ItemDate = DateTime.Now;
            obj.ValidFrom = DateTime.Now;
            obj.ValidTo = DateTime.MinValue;
            int defaultCategoryId = 0;
            int.TryParse(DropCategoriesFilter.SelectedValue, out defaultCategoryId);
            obj.CategoryId = defaultCategoryId;
            obj2form(obj);
            LitItemType.Text = DropNew.SelectedValue;
        }
        else
        {
            obj = new ItemsManager<Item, ItemsFilter>(true, true).GetByKey(CurrentId);
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

            new ItemsManager<Item, ItemsFilter>(true, true).DeleteById(recordId);
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

        var secFilter = new SectionsFilter();
        secFilter.Id = sectionId;
        var secList = new SectionsManager(true, true).GetByFilter(secFilter, "");

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
        catFilter.SectionId = sectionId;
        if (catFilter.SectionId == 0) 
            catFilter.Id = -1;
        
        var list = new CategoriesManager(true, true).GetByFilter(catFilter, "");
        loadListCategories(DropCategoriesFilter, list, 0, 0, base.ShowItemsCount);

        if (base.LastSelectedCategoryId > 0)
            Utility.SetDropByValue(DropCategoriesFilter, base.LastSelectedCategoryId.ToString());
    }

    private void loadDropCategories(int sectionId)
    {
        DropCategories.Items.Clear();

        var catFilter = new CategoriesFilter();
        catFilter.SectionId = sectionId;
        if (catFilter.SectionId == 0) 
            catFilter.Id = -1;
        var list = new CategoriesManager().GetByFilter(catFilter, "");
        loadListCategories(DropCategories, list, 0, 0);
    }

    private void loadListCategories(DropDownList drop, 
        List<Category> list, int parentId, int level, bool showItemsCount = false)
    {
        var nodes = list.Where(x => x.ParentId == parentId);
        foreach (var item in nodes)
        {
            string levelString = "";
            for (int i = 0; i < level; i++)
            {
                levelString += ". . ";
            }

            var listItem = new ListItem();
            listItem.Value = item.Id.ToString();
            listItem.Text = levelString + item.Title;
            if (showItemsCount)
            {
                var iman = new ItemsManager<Item, ItemsFilter>();
                var ifilter = new ItemsFilter();
                ifilter.CategoryId = item.Id;
                int count = iman.GetByFilter(ifilter, "").Count;
                if (count > 0)
                    listItem.Text += " ("+ count.ToString() +")";
            }
            drop.Items.Add(listItem);

            loadListCategories(drop, list, item.Id, level + 1, showItemsCount);
        }
    }

    private void loadDropsItemTypes()
    {

        DropNew.Items.Clear();
        DropNew.Items.Add(new ListItem(Utility.GetLabel("LblCreateNew", "Create new"), ""));

        DropItemTypesFilter.Items.Clear();
        DropItemTypesFilter.Items.Add(new ListItem(Utility.GetLabel("LblSelectItem", "Select item"), ""));

        var filter = new ItemTypeFilter();
        if (!string.IsNullOrEmpty(this.ItemType))
            filter.FullName = this.ItemType;
        List<ItemType> recordList = new ItemTypeManager().GetByFilter(filter, "FullName");
        foreach (ItemType record1 in recordList)
        {
            DropNew.Items.Add(
                new ListItem(record1.FullName, record1.FullName));

            DropItemTypesFilter.Items.Add(
                new ListItem(record1.FullName, record1.FullName));
        }

        if (!string.IsNullOrEmpty(this.ItemType))
        {
            BtnNew.Visible = true;
            DropNew.Visible = false;
            DropItemTypesFilter.Visible = false;
        }
        else
        {
            BtnNew.Visible = false;
            DropNew.Visible = true;
            DropItemTypesFilter.Visible = true;
        }
    }

    private void setFlag(int recordId, bool value, string flagName)
    {
        try
        {
            if (!PgnUserCurrent.IsAuthenticated)
                throw new Exception("user not authenticated");

            var o1 = new ItemsManager<Item, ItemsFilter>().GetByKey(recordId);
            switch (flagName.ToLower())
            {
                case "enabled":
                    o1.Enabled = value;
                    break;
                default:
                    break;
            }
            new ItemsManager<Item, ItemsFilter>().Update(o1);
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

            new ItemsManager<Item, ItemsFilter>(true, true).MoveRecord(recordId, direction);
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

    //private void initTree()
    //{
    //    Tree1.TargetImagesUpload = 0;
    //    Tree1.TargetFilesUpload = 0;
    //    Tree1.SectionId = base.SectionId;

    //    Tree1.ShowSecurity = base.ShowSecurity;
    //    Tree1.ShowOnlyDefaultCulture = base.ShowOnlyDefaultCulture;
    //    Tree1.ShowItemsCount = true;
    //    Tree1.AllowOrdering = false;
    //    Tree1.AllowEdit = false;
    //    Tree1.AllowDelete = false;
    //    Tree1.AllowNew = false;
    //    Tree1.AllowSelection = true;
    //}

    private bool checkAddNewFilters()
    {
        bool res = true;
        if (DropSectionsFilter.SelectedValue == "0" || DropSectionsFilter.SelectedValue == "")
        {
            LblErr.Text = RenderError(base.GetLabel("ChooseSection", "Choose a section before"));
            res = false;
        }

        return res;
    }

    /// <summary>
    /// remove all items from cache
    /// </summary>
    protected static void removeFromCache()
    {
        new CacheManager<PigeonCms.Item>("PigeonCms.Item").Clear();
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
