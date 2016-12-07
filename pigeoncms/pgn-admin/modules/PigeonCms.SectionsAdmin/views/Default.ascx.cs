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

public partial class Controls_SectionsAdmin : PigeonCms.BaseModuleControl
{
    public string TitleItem = "";

    int targetImagesUpload = 0;
    protected int TargetImagesUpload
    {
        get { return GetIntParam("TargetImagesUpload", targetImagesUpload); }
        set { targetImagesUpload = value; }
    }

    int targetFilesUpload = 0;
    protected int TargetFilesUpload
    {
        get { return GetIntParam("TargetFilesUpload", targetFilesUpload); }
        set { targetFilesUpload = value; }
    }

    string imagesUploadUrl = "";
    protected string ImagesUploadUrl
    {
        get
        {
            if (string.IsNullOrEmpty(imagesUploadUrl) && this.TargetImagesUpload > 0)
            {
                var menuMan = new MenuManager();
                var menuTarget = new PigeonCms.Menu();
                menuTarget = menuMan.GetByKey(this.TargetImagesUpload);
                imagesUploadUrl = Utility.GetRoutedUrl(menuTarget);
            }
            return imagesUploadUrl;
        }
    }

    string filesUploadUrl = "";
    protected string FilesUploadUrl
    {
        get
        {
            if (string.IsNullOrEmpty(filesUploadUrl) && this.TargetFilesUpload > 0)
            {
                var menuMan = new MenuManager();
                var menuTarget = new PigeonCms.Menu();
                menuTarget = menuMan.GetByKey(this.TargetFilesUpload);
                filesUploadUrl = Utility.GetRoutedUrl(menuTarget);
            }
            return filesUploadUrl;
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
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        setSuccess("");
        setError("");

        if (!Page.IsPostBack)
        {
            loadDropEnabledFilter();
            loadDropItemType();
            loadList();
        }
    }

    protected void DropEnabledFilter_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadList();
    }

    protected void BtnNew_Click(object sender, EventArgs e)
    {
        try
        {
            editRow(0);
        }
        catch (Exception e1)
        {
            setError(e1.Message);
        }
    }

    protected void BtnSave_Click(object sender, EventArgs e)
    {
        setError("");
        setSuccess("");

        try
        {
            var o1 = new Section();
            if (base.CurrentId == 0)
            {
                form2obj(o1);
                o1 = new SectionsManager().Insert(o1);
            }
            else
            {
                o1 = new SectionsManager().GetByKey(base.CurrentId);  //precarico i campi esistenti e nn gestiti dal form
                form2obj(o1);
                new SectionsManager().Update(o1);
            }

            loadList();
            setSuccess(Utility.GetLabel("RECORD_SAVED_MSG"));
            showInsertPanel(false);
        }
        catch (Exception e1)
        {
            setError(Utility.GetLabel("RECORD_ERR_MSG") + "<br />" + e1.ToString());
        }
    }

    protected void BtnCancel_Click(object sender, EventArgs e)
    {
        setError("");
        setSuccess("");
        showInsertPanel(false);
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

    protected void Rep1_ItemCommand(object sender, RepeaterCommandEventArgs e)
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
        if (e.CommandName == "Enabled0")
        {
            setFlag(Convert.ToInt32(e.CommandArgument), false, "enabled");
            loadList();
        }
        if (e.CommandName == "Enabled1")
        {
            setFlag(Convert.ToInt32(e.CommandArgument), true, "enabled");
            loadList();
        }
    }


	protected void Rep1_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Header)
            return;


        var item = (PigeonCms.Section)e.Item.DataItem;


        var LitEnabled = (Literal)e.Item.FindControl("LitEnabled");
        string enabledClass = "";
        if (item.Enabled)
            enabledClass = "checked";
        LitEnabled.Text = "<span class='table-modern--checkbox--square " + enabledClass + "'></span>";


        var LnkEnabled = (LinkButton)e.Item.FindControl("LnkEnabled");
        LnkEnabled.CssClass = "table-modern--checkbox " + enabledClass;
        LnkEnabled.CommandName = item.Enabled ? "Enabled0" : "Enabled1";


        var LitTitle = (Literal)e.Item.FindControl("LitTitle");
        LitTitle.Text += Utility.Html.GetTextPreview(item.Title, 50, "");
        if (string.IsNullOrEmpty(item.Title))
            LitTitle.Text += Utility.GetLabel("NO_VALUE", "<no value>");


        var LitItemInfo = (Literal)e.Item.FindControl("LitItemInfo");
        LitItemInfo.Text += "ID: " + item.Id.ToString() + "<br>";
        if (!string.IsNullOrEmpty(item.ExtId))
            LitItemInfo.Text += "extId: <strong>" + item.ExtId + "</strong><br>";
        if (!string.IsNullOrEmpty(item.CssClass))
            LitItemInfo.Text += "class: " + item.CssClass + "<br>";
        if (!string.IsNullOrEmpty(item.ItemType))
            LitItemInfo.Text += "items type: " + item.ItemType + "<br>";


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
        }



        //items inserted/allowed
        var LitItems = (Literal)e.Item.FindControl("LitItems");
        string itemsAllowed = "";
        string numOfItems = "0";
        string maxItems = "unlimited";
        if (item.MaxItems > 0)
            maxItems = item.MaxItems.ToString();
        itemsAllowed = " / " + maxItems;

        numOfItems = item.NumOfItems.ToString();
        LitItems.Text = "items: " + numOfItems + itemsAllowed;

        //space used/allowed
        var LitDiskSpace = (Literal)e.Item.FindControl("LitDiskSpace");
        LitDiskSpace.Text ="disk: " + Utility.GetFileHumanLength(item.SizeOfItems);
        string maxDiskSpace = "unlimited";
        if (item.MaxAttachSizeKB > 0)
            maxDiskSpace = Utility.GetFileHumanLength(item.MaxAttachSizeKB * 1024);
        LitDiskSpace.Text += " / " + maxDiskSpace;

    }


    #region private methods

    private void clearForm()
    {
        TitleItem = "";
        foreach (KeyValuePair<string, string> item in Config.CultureList)
        {
            TextBox t1 = new TextBox();
            t1 = (TextBox)PanelTitle.FindControl("TxtTitle" + item.Value);
            t1.Text = "";

            TextBox t2 = new TextBox();
            t2 = (TextBox)PanelDescription.FindControl("TxtDescription" + item.Value);
            t2.Text = "";
        }
        TxtExtId.Text = "";
        DropItemType.SelectedValue = "";
        TxtCssClass.Text = "";
        ChkEnabled.Checked = true;
        TxtMaxItems.Text = "";
        TxtMaxAttachSizeKB.Text = "";
        PermissionsControl1.ClearForm();
    }

    private void form2obj(Section obj)
    {
        obj.Id = base.CurrentId;
        obj.Enabled = ChkEnabled.Checked;
        obj.TitleTranslations.Clear();
        obj.DescriptionTranslations.Clear();
        foreach (KeyValuePair<string, string> item in Config.CultureList)
        {
            TextBox t1 = new TextBox();
            t1 = (TextBox)PanelTitle.FindControl("TxtTitle" + item.Value);
            obj.TitleTranslations.Add(item.Key, t1.Text);

            TextBox t2 = new TextBox();
            t2 = (TextBox)PanelDescription.FindControl("TxtDescription" + item.Value);
            obj.DescriptionTranslations.Add(item.Key, t2.Text);
        }
        int maxItems = 0;
        int.TryParse(TxtMaxItems.Text, out maxItems);
        obj.MaxItems = maxItems;

        int maxAttachSizeKB = 0;
        int.TryParse(TxtMaxAttachSizeKB.Text, out maxAttachSizeKB);
        obj.MaxAttachSizeKB  = maxAttachSizeKB;

        obj.ItemType = DropItemType.SelectedValue;
        obj.CssClass = TxtCssClass.Text;
        obj.ExtId = TxtExtId.Text;

        PermissionsControl1.Form2obj(obj);
    }

    private void obj2form(Section obj)
    {
        foreach (KeyValuePair<string, string> item in Config.CultureList)
        {
            string sTitleTranslation = "";
            TextBox t1 = new TextBox();
            t1 = (TextBox)PanelTitle.FindControl("TxtTitle" + item.Value);
            obj.TitleTranslations.TryGetValue(item.Key, out sTitleTranslation);
            t1.Text = sTitleTranslation;

            //Set title edit item
            if (string.IsNullOrEmpty(TitleItem))
                TitleItem = sTitleTranslation;


            string sDescriptionTraslation = "";
            TextBox t2 = new TextBox();
            t2 = (TextBox)PanelDescription.FindControl("TxtDescription" + item.Value);
            obj.DescriptionTranslations.TryGetValue(item.Key, out sDescriptionTraslation);
            t2.Text = sDescriptionTraslation;
        }

        TxtMaxItems.Text = obj.MaxItems.ToString();
        TxtMaxAttachSizeKB.Text = obj.MaxAttachSizeKB.ToString();
        TxtCssClass.Text = obj.CssClass;
        TxtExtId.Text = obj.ExtId;
        Utility.SetDropByValue(DropItemType, obj.ItemType);

        ChkEnabled.Checked = obj.Enabled;
        PermissionsControl1.Obj2form(obj);

        DropItemType.Enabled = (this.CurrentId == 0);
    }

    private void editRow(int recordId)
    {
        setSuccess("");
        setError("");

        if (!PgnUserCurrent.IsAuthenticated)
            throw new Exception("user not authenticated");

        clearForm();
        base.CurrentId = recordId;
        if (base.CurrentId > 0)
        {
            Section obj = new Section();
            obj = new SectionsManager(true, true).GetByKey(base.CurrentId);
            obj2form(obj);
        }
        else
        {
            //set default permissions for new section
            //if a role like username exist then add
            var obj = new Section();
            obj.WriteAccessType = MenuAccesstype.Registered;
            if (Roles.IsUserInRole(PgnUserCurrent.UserName))
                obj.WriteRolenames.Add(PgnUserCurrent.UserName);
            PermissionsControl1.Obj2form(obj);
        }
        showInsertPanel(true);
    }

    private void deleteRow(int recordId)
    {
        setSuccess("");
        setError("");

        try
        {
            new SectionsManager(true, true).DeleteById(recordId);
        }
        catch (Exception e)
        {
            setError(e.Message);
        }
        loadList();
    }

    private void loadList()
    {
        var man = new SectionsManager(true, true);
        var filter = new SectionsFilter();

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

        var list = man.GetByFilter(filter, "");
        var ds = new PagedDataSource();
        ds.DataSource = list;
        ds.AllowPaging = true;
        ds.PageSize = base.ListPageSize;
        ds.CurrentPageIndex = base.ListCurrentPage;

        RepPaging.Visible = false;
        if (ds.PageCount > 1)
        {
            RepPaging.Visible = true;
            ArrayList pages = new ArrayList();
            for (int i = 0; i <= ds.PageCount - 1; i++)
            {
                pages.Add((i + 1).ToString());
            }
            RepPaging.DataSource = pages;
            RepPaging.DataBind();
        }

        Rep1.DataSource = ds;
        Rep1.DataBind();
    }

    private void loadDropEnabledFilter()
    {
        DropEnabledFilter.Items.Clear();
        DropEnabledFilter.Items.Add(new ListItem(Utility.GetLabel("LblSelectState", "Select state"), ""));
        DropEnabledFilter.Items.Add(new ListItem("On-line", "1"));
        DropEnabledFilter.Items.Add(new ListItem("Off-line", "0"));
    }

    private void loadDropItemType()
    {
        DropItemType.Items.Clear();
        DropItemType.Items.Add(new ListItem("", ""));

        var filter = new ItemTypeFilter();
        var list = new ItemTypeManager().GetByFilter(filter, "FullName");
        foreach (ItemType type in list)
        {
            DropItemType.Items.Add(
                new ListItem(type.FullName, type.FullName));
        }
    }

    private void setFlag(int recordId, bool value, string flagName)
    {
        try
        {
            PigeonCms.Section o1 = new PigeonCms.Section();
            o1 = new SectionsManager().GetByKey(recordId);
            switch (flagName.ToLower())
            {
                case "enabled":
                    o1.Enabled = value;
                    break;
                default:
                    break;
            }
            new SectionsManager(true, true).Update(o1);
        }
        catch (Exception e1)
        {
            setError(Utility.GetLabel("RECORD_ERR_MSG") + "<br />" + e1.ToString());
        }
        finally { }
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

    #endregion
}
