using System;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using PigeonCms;
using System.Web;

public partial class Controls_CategoriesAdmin : PigeonCms.Modules.CategoriesAdminControl
{

	public string TitleItem = "";
    private CategoriesManager man = new CategoriesManager(true, true);

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
        foreach (KeyValuePair<string, string> item in Config.CultureList)
        {
            //title
            Panel pan1 = new Panel();
            pan1.CssClass = "form-group input-group";
            PanelTitle.Controls.Add(pan1);

            Literal lit1 = new Literal();
            lit1.Text = "<div class='input-group-addon'><span>" + item.Value.Substring(0, 3) + "</span></div>";
            pan1.Controls.Add(lit1);
            TextBox txt1 = new TextBox();
            txt1.ID = "TxtTitle" + item.Value;
            txt1.MaxLength = 200;
            txt1.CssClass = "form-control";
            txt1.ToolTip = item.Key;
            LabelsProvider.SetLocalizedControlVisibility(false, item.Key, txt1);
            pan1.Controls.Add(txt1);
            //if (item.Key == Config.CultureDefault)
            //    titleId = txt1.ClientID;

            //description
            Literal lit2 = new Literal();
            lit2.Text = "<span class='lang-description'>- <i>" + item.Value + "</i> -</span>";
            PanelDescription.Controls.Add(lit2);

            var txt2 = new TextBox();
            txt2.ID = "TxtDescription" + item.Value;
            txt2.Rows = 3;
            txt2.CssClass = "form-control";
            txt2.TextMode = TextBoxMode.MultiLine;
            LabelsProvider.SetLocalizedControlVisibility(false, item.Key, txt2);
            PanelDescription.Controls.Add(txt2);
        }

        //restrictions
        BtnNew.Visible = this.AllowNew;
        PermissionsControl1.Visible = this.ShowSecurity;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        setSuccess("");
        setError("");

        //Tree1.NodeClick += new NodeClickDelegate(Tree_NodeClick);
        //initTree();
        if (!Page.IsPostBack)
        {
            loadDropSectionsFilter(base.SectionId);
            loadList();
        }
        else
        {
            string eventArg = HttpContext.Current.Request["__EVENTARGUMENT"];
            if (eventArg == "items")
                loadList();
            else if (eventArg == "sortcomplete")
            {
                updateSortedTable();
                loadList();
            }
        }
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

    protected void Rep1_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Header)
        {
            return;
        }

        var item = (PigeonCms.Category)e.Item.DataItem;


        var LitEnabled = (Literal)e.Item.FindControl("LitEnabled");
        string enabledClass = "";
        if (item.Enabled)
            enabledClass = "checked";
        LitEnabled.Text = "<span class='table-modern--checkbox--square " + enabledClass + "'></span>";


        var LnkEnabled = (LinkButton)e.Item.FindControl("LnkEnabled");
        LnkEnabled.CssClass = "table-modern--checkbox " + enabledClass;
        LnkEnabled.CommandName = item.Enabled ? "Enabled0" : "Enabled1";


        var LitTitle = (Literal)e.Item.FindControl("LitTitle");
        if (!item.IsThreadRoot)
            LitTitle.Text += "--";
        LitTitle.Text += Utility.Html.GetTextPreview(item.Title, 50, "");
        if (string.IsNullOrEmpty(item.Title))
            LitTitle.Text += Utility.GetLabel("NO_VALUE", "<no value>");
        if (base.ShowSecurity && Roles.IsUserInRole("debug"))
            LitTitle.Text += " [" + item.Id.ToString() + "]";


        var LitItemDate = (Literal)e.Item.FindControl("LitItemDate");
        LitItemDate.Text = item.DateUpdated.ToString();

        var LitItemInfo = (Literal)e.Item.FindControl("LitItemInfo");
        if (!string.IsNullOrEmpty(item.ExtId))
            LitItemInfo.Text += "extId: <strong>" + item.ExtId + "</strong><br>";
        if (this.ShowType)
            LitItemInfo.Text += item.ItemTypeName + "<br>";
        if (!string.IsNullOrEmpty(item.CssClass))
            LitItemInfo.Text += "class: " + item.CssClass;


        if (item.CategoryId > 0)
        {
            var mgr = new CategoriesManager();
            var cat = mgr.GetByKey(item.CategoryId);
            var LitCategoryTitle = (Literal)e.Item.FindControl("LitCategoryTitle");
            LitCategoryTitle.Text = cat.Title;
        }

        //permissions
        var LitAccessTypeDesc = (Literal)e.Item.FindControl("LitAccessTypeDesc");
        LitAccessTypeDesc.Text = RenderAccessTypeSummary(item);

        //files upload
        var LnkUploadFiles = (HyperLink)e.Item.FindControl("LnkUploadFiles");
        LnkUploadFiles.NavigateUrl = this.FilesUploadUrl
            + "?type=items&id=" + item.Id.ToString();
        LnkUploadFiles.Visible = this.TargetFilesUpload > 0;


        var LitFilesCount = (Literal)e.Item.FindControl("LitFilesCount");
        int filesCount = item.Files.Count;
        LitFilesCount.Text = "&nbsp;";
        if (filesCount > 0)
        {
            LitFilesCount.Text = filesCount.ToString();
            LitFilesCount.Text += filesCount == 1 ? " file" : " files";
            LitFilesCount.Text += " / " + Utility.GetFileHumanLength(item.FilesSize);
        }

        //images upload
        var LnkUploadImg = (HyperLink)e.Item.FindControl("LnkUploadImg");
        LnkUploadImg.NavigateUrl = this.ImagesUploadUrl
            + "?type=items&id=" + item.Id.ToString();
        LnkUploadImg.Visible = this.TargetImagesUpload > 0;


        var LitImgCount = (Literal)e.Item.FindControl("LitImgCount");
        int imgCount = item.Images.Count;
        LitImgCount.Text = "&nbsp;";
        if (imgCount > 0)
        {
            LitImgCount.Text = imgCount.ToString();
            LitImgCount.Text += imgCount == 1 ? " file" : " files";
            LitImgCount.Text += " / " + Utility.GetFileHumanLength(item.ImagesSize);
        }

    }

    protected void Tree_NodeClick(object sender, NodeClickEventArgs e)
    {
        LblOk.Text = RenderSuccess(e.Command.ToString() + " " + e.CategoryId.ToString());

        switch (e.Command)
        {
            case NodeClickCommandEnum.Select:
            case NodeClickCommandEnum.Edit:
                editRow(e.CategoryId);
                break;

            case NodeClickCommandEnum.Enabled:
                bool enabledValue = bool.Parse(e.CustomCommand);
                setFlag(e.CategoryId, enabledValue, "enabled");
                Tree1.BindTree(this.CurrentSectionId);
                break;

            case NodeClickCommandEnum.MoveUp:
                moveRecord(e.CategoryId, Database.MoveRecordDirection.Up);
                break;
            
            case NodeClickCommandEnum.MoveDown:
                moveRecord(e.CategoryId, Database.MoveRecordDirection.Down);
                break;
            
            case NodeClickCommandEnum.Delete:
                deleteRow(e.CategoryId);
                break;

            case NodeClickCommandEnum.Custom:
                break;

            default:
                break;
        }

    }

    protected void DropSectionsFilter_SelectedIndexChanged(object sender, EventArgs e)
    {
        int secID = 0;
        int.TryParse(DropSectionsFilter.SelectedValue, out secID);

        loadList();

        this.LastSelectedSectionId = secID;
    }

    protected void BtnNew_Click(object sender, EventArgs e)
    {
        if (DropSectionsFilter.SelectedValue == "0" || DropSectionsFilter.SelectedValue == "")
        {
            setError(base.GetLabel("ChooseSectionBefore", "Choose a section before"));
            return;
        }
        editRow(0);
    }

    protected void BtnSave_Click(object sender, EventArgs e)
    {
        if (saveForm())
            showInsertPanel(false);
    }

    protected void BtnCancel_Click(object sender, EventArgs e)
    {
        setError("");
        setSuccess("");
        showInsertPanel(false);
    }

    #region private methods

    private bool saveForm()
    {
        bool res = false;
        setError("");
        setSuccess("");

        try
        {
            var o1 = new Category();

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

            loadList();
            setSuccess(Utility.GetLabel("RECORD_SAVED_MSG"));
            res = true;
        }
        catch (Exception e1)
        {
            setError(Utility.GetLabel("RECORD_ERR_MSG") + "<br />" + e1.ToString());
        }
        finally
        {
        }
        return res;
    }

    private void clearForm()
    {
        TitleItem = "";
        LblId.Text = "";
        LblOrderId.Text = "";
        TxtCssClass.Text = "";
        TxtExtId.Text = "";
        ChkEnabled.Checked = true;
        LitSection.Text = "";

        foreach (KeyValuePair<string, string> item in Config.CultureList)
        {
            TextBox t1 = new TextBox();
            t1 = (TextBox)PanelTitle.FindControl("TxtTitle" + item.Value);
            t1.Text = "";

            TextBox t2 = new TextBox();
            t2 = (TextBox)PanelDescription.FindControl("TxtDescription" + item.Value);
            t2.Text = "";
        }
        PermissionsControl1.ClearForm();
    }

    private void form2obj(Category obj)
    {
        obj.Id = base.CurrentId;
        obj.Enabled = ChkEnabled.Checked;
        obj.TitleTranslations.Clear();
        obj.DescriptionTranslations.Clear();
        obj.SectionId = int.Parse(DropSectionsFilter.SelectedValue);
        obj.CssClass = TxtCssClass.Text;
        obj.ExtId = TxtExtId.Text;

        int parentId = 0;
        foreach (ListItem item in ListParentId.Items)
        {
            if (item.Selected)
            {
                if (int.TryParse(item.Value, out parentId))
                    obj.ParentId = parentId;
            }
        }
        obj.ParentId = parentId;

        foreach (KeyValuePair<string, string> item in Config.CultureList)
        {
            TextBox t1 = new TextBox();
            t1 = (TextBox)PanelTitle.FindControl("TxtTitle" + item.Value);
            obj.TitleTranslations.Add(item.Key, t1.Text);

            TextBox t2 = new TextBox();
            t2 = (TextBox)PanelDescription.FindControl("TxtDescription" + item.Value);
            obj.DescriptionTranslations.Add(item.Key, t2.Text);
        }

        PermissionsControl1.Form2obj(obj);
    }

    private void obj2form(Category obj)
    {

        LblId.Text = obj.Id.ToString();
        LblOrderId.Text = obj.Ordering.ToString();
        ChkEnabled.Checked = obj.Enabled;
        TxtCssClass.Text = obj.CssClass;
        TxtExtId.Text = obj.ExtId;

        foreach (KeyValuePair<string, string> item in Config.CultureList)
        {
            string sTitleTranslation = "";
            TextBox t1 = new TextBox();
            t1 = (TextBox)PanelTitle.FindControl("TxtTitle" + item.Value);
            obj.TitleTranslations.TryGetValue(item.Key, out sTitleTranslation);
            t1.Text = sTitleTranslation;

            string sDescriptionTraslation = "";
            TextBox t2 = new TextBox();
            t2 = (TextBox)PanelDescription.FindControl("TxtDescription" + item.Value);
            obj.DescriptionTranslations.TryGetValue(item.Key, out sDescriptionTraslation);
            t2.Text = sDescriptionTraslation;
        }
        LitSection.Text = obj.Section.Title;
        loadListParentId();
        Utility.SetListBoxByValues(ListParentId, obj.ParentId);

        PermissionsControl1.Obj2form(obj);
    }

    private void editRow(int recordId)
    {
        var obj = new PigeonCms.Category();
        setSuccess("");
        setError("");

        clearForm();
        base.CurrentId = recordId;

        if (base.CurrentId > 0)
            obj = man.GetByKey(base.CurrentId);

        obj2form(obj);
        showInsertPanel(true);
    }

    private void deleteRow(int recordId)
    {
        if (!this.AllowDelete)
            return;

        setSuccess("");
        setError("");

        try
        {
            if (!PgnUserCurrent.IsAuthenticated)
                throw new Exception("user not authenticated");

            man.DeleteById(recordId);
        }
        catch (Exception e)
        {
            setError(e.Message);
        }
        loadList();
    }


    /// <summary>
    /// loads sections filter and section drops
    /// </summary>
    private void loadDropSectionsFilter(int sectionId)
    {
        DropSectionsFilter.Items.Clear();

        var filter = new SectionsFilter();
        filter.Id = sectionId;
        List<Section> recordList = new SectionsManager(true, true).GetByFilter(filter, "");
        foreach (Section record1 in recordList)
        {
            DropSectionsFilter.Items.Add(new ListItem(record1.Title, record1.Id.ToString()));
        }
        if (this.LastSelectedSectionId > 0)
            Utility.SetDropByValue(DropSectionsFilter, this.LastSelectedSectionId.ToString());
    }

    private void loadListParentId()
    {
        ListParentId.Items.Clear();

        //root virtual item
        var listItem = new ListItem();
        listItem.Value = "0";
        listItem.Text = "";
        listItem.Enabled = true;
        ListParentId.Items.Add(listItem);

        var man = new CategoriesManager(true, true);
        var filter = new CategoriesFilter();
        filter.Enabled = Utility.TristateBool.NotSet;
        if (this.SectionId > 0)
            filter.SectionId = this.SectionId;
        else
        {
            int secId = -1;
            int.TryParse(DropSectionsFilter.SelectedValue, out secId);
            filter.SectionId = secId;
        }
        var list = man.GetByFilter(filter, "");
        loadListParentId(list, 0, 0, this.CurrentId);
    }

    private void loadListParentId(List<Category> list, int parentId, int level, int currentId)
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
            listItem.Enabled = true;
            if (item.Id == currentId)
            {
               listItem.Attributes.Add("disabled", "disabled");
            }
            ListParentId.Items.Add(listItem);

            loadListParentId(list, item.Id, level + 1, currentId);
        }
    }

    private void setFlag(int recordId, bool value, string flagName)
    {
        if (!this.AllowEdit)
            return;

        try
        {
            var man = new CategoriesManager(true, true);
            var o1 = man.GetByKey(recordId);
            if (o1.Id > 0)
            {
                switch (flagName.ToLower())
                {
                    case "enabled":
                        o1.Enabled = value;
                        break;
                    default:
                        break;
                }
                man.Update(o1);
            }
        }
        catch (Exception e1)
        {
            setError(Utility.GetLabel("RECORD_ERR_MSG") + "<br />" + e1.ToString());
        }
        finally { }
    }

    private void updateSortedTable()
    {
        //<input type="hidden" name="RowId" value='<%# Eval("Id") %>' />
        string[] rowIds = Request.Form.GetValues("RowId");
        int ordering = 1;

        foreach (string sid in rowIds)
        {
            int id = 0;
            int.TryParse(sid, out id);
            sortRecord(id, ordering);
            ordering++;
        }
    }

    public void sortRecord(int id, int ordering)
    {
        var man = new CategoriesManager(true, false);
        var item = man.GetByKey(id);
        item.Ordering = ordering;
        man.Update(item);
    }

    /// function for display insert panel
    /// <summary>
    /// </summary>
    private void showInsertPanel(bool toShow)
    {

        PigeonCms.Utility.Script.RegisterStartupScript(Upd1, "bodyBlocked", "bodyBlocked(" + toShow.ToString().ToLower() + ");");

        if (toShow)
            PanelInsert.Visible = true;
        else
            PanelInsert.Visible = false;
    }

    private void setError(string content)
    {
        LblErrInsert.Text = LblErrSee.Text = RenderError(content);
    }

    private void setSuccess(string content)
    {
        LblOkInsert.Text = LblOkSee.Text = RenderSuccess(content);
    }

    //private void initTree()
    //{
    //    Tree1.TargetImagesUpload = base.TargetImagesUpload;
    //    Tree1.TargetFilesUpload = base.TargetFilesUpload;
    //    Tree1.SectionId = base.SectionId;

    //    Tree1.ShowSecurity = base.ShowSecurity;
    //    Tree1.ShowOnlyDefaultCulture = base.ShowOnlyDefaultCulture;
    //    Tree1.ShowItemsCount = base.ShowItemsCount;
    //    Tree1.AllowOrdering = base.AllowOrdering;
    //    Tree1.AllowEdit = base.AllowEdit;
    //    Tree1.AllowDelete = base.AllowDelete;
    //    Tree1.AllowNew = base.AllowNew;
    //    Tree1.AllowSelection = base.AllowSelection;
    //}

    #endregion

}
