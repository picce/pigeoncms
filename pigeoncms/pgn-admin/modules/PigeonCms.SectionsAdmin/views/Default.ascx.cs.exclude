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

            TextBox txt1 = new TextBox();
            txt1.ID = "TxtTitle" + item.Value;
            txt1.MaxLength = 50;
            txt1.CssClass = "form-control";
            txt1.ToolTip = item.Key;
            LabelsProvider.SetLocalizedControlVisibility(false, item.Key, txt1);
            pan1.Controls.Add(txt1);
            Literal lit1 = new Literal();
            lit1.Text = "<span class='input-group-addon'>" + item.Value + "</span>";
            pan1.Controls.Add(lit1);

            //description
            Panel pan2 = new Panel();
            pan2.CssClass = "form-group input-group";
            PanelDescription.Controls.Add(pan2);

            TextBox txt2 = new TextBox();
            txt2.ID = "TxtDescription" + item.Value;
            txt2.Rows = 3;
            txt2.TextMode = TextBoxMode.MultiLine;
            txt2.CssClass = "form-control";
            txt2.ToolTip = item.Key;
            LabelsProvider.SetLocalizedControlVisibility(false, item.Key, txt2);
            pan2.Controls.Add(txt2);
            Literal lit2 = new Literal();
            lit2.Text = "<span class='input-group-addon'>" + item.Value + "</span>";
            pan2.Controls.Add(lit2);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        LblOk.Text = RenderSuccess("");
        LblErr.Text = RenderError("");

        if (!Page.IsPostBack)
        {
            loadDropEnabledFilter();
            loadDropItemType();
        }
    }

    protected void DropEnabledFilter_SelectedIndexChanged(object sender, EventArgs e)
    {
        Grid1.DataBind();
    }

    protected void ObjDs1_ObjectCreating(object sender, ObjectDataSourceEventArgs e)
    {
        var typename = new SectionsManager(true, true);
        e.ObjectInstance = typename;
    }

    protected void ObjDs1_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
    {
        SectionsFilter filter = new SectionsFilter();

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
            var item = new PigeonCms.Section();
            item = (PigeonCms.Section)e.Row.DataItem;

            LinkButton LnkTitle = (LinkButton)e.Row.FindControl("LnkTitle");
            LnkTitle.Text = "<i class='fa fa-pgn_edit fa-fw'></i>";
            LnkTitle.Text += Utility.Html.GetTextPreview(item.Title, 30, "");
            if (string.IsNullOrEmpty(LnkTitle.Text))
                LnkTitle.Text += Utility.GetLabel("NO_VALUE", "<no value>");

            var LitItemInfo = (Literal)e.Row.FindControl("LitItemInfo");
            if (!string.IsNullOrEmpty(item.ExtId))
                LitItemInfo.Text += base.GetLabel("ExtId", "ExtId") + ": " + item.ExtId + "<br>";
            LitItemInfo.Text += item.ItemType;

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
                LitAccessTypeDesc.Text += Utility.Html.GetTextPreview(roles, 80, "");
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
                LitAccessTypeDesc.Text += Utility.Html.GetTextPreview(roles, 80, "");
                LitAccessTypeDesc.Text += writeAccessLevel;
            }


            //files upload
            var LnkUploadFiles = (HyperLink)e.Row.FindControl("LnkUploadFiles");
            LnkUploadFiles.NavigateUrl = this.FilesUploadUrl
                + "?type=sections&id=" + item.Id.ToString();
            LnkUploadFiles.CssClass = "fancyRefresh";
            var LitFilesCount = (Literal)e.Row.FindControl("LitFilesCount");
            int filesCount = item.Files.Count;
            if (filesCount > 0)
            {
                LitFilesCount.Text = filesCount.ToString();
                LitFilesCount.Text += filesCount == 1 ? " file" : " files";
            }

            //images upload
            var LnkUploadImg = (HyperLink)e.Row.FindControl("LnkUploadImg");
            LnkUploadImg.NavigateUrl = this.ImagesUploadUrl
                + "?type=sections&id=" + item.Id.ToString();
            LnkUploadImg.CssClass = "fancyRefresh";
            var LitImgCount = (Literal)e.Row.FindControl("LitImgCount");
            int imgCount = item.Images.Count;
            if (imgCount > 0)
            {
                LitImgCount.Text = imgCount.ToString();
                LitImgCount.Text += imgCount == 1 ? " file" : " files";
            }

            //items inserted/allowed
            var LitItems = (Literal)e.Row.FindControl("LitItems");
            string itemsAllowed = "";
            string numOfItems = "0";

            if (item.MaxItems > 0) itemsAllowed = " / " + item.MaxItems.ToString();
            numOfItems = item.NumOfItems.ToString();
            LitItems.Text = numOfItems + itemsAllowed;
            if (LitItems.Text == "0") LitItems.Text = "";

            //space used/allowed
            var LitDiskSpace = (Literal)e.Row.FindControl("LitDiskSpace");
            LitDiskSpace.Text = Utility.GetFileHumanLength(item.SizeOfItems);
            if (item.MaxAttachSizeKB > 0)
                LitDiskSpace.Text += " / " + Utility.GetFileHumanLength(item.MaxAttachSizeKB*1024);

        }
    }

    protected void BtnNew_Click(object sender, EventArgs e)
    {
        LblErr.Text = RenderError("");
        LblOk.Text = RenderSuccess("");

        try
        {
            editRow(0);
        }
        catch (Exception e1)
        {
            LblErr.Text = RenderError(e1.Message);
        }
        finally
        {
        }
    }

    protected void BtnSave_Click(object sender, EventArgs e)
    {
        LblErr.Text = RenderError("");
        LblOk.Text = RenderSuccess("");

        try
        {
            Section o1 = new Section();
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
        LblOk.Text = RenderSuccess("");
        LblErr.Text = RenderError("");

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
        MultiView1.ActiveViewIndex = 1;
    }

    private void deleteRow(int recordId)
    {
        LblOk.Text = RenderSuccess("");
        LblErr.Text = RenderError("");

        try
        {
            new SectionsManager(true, true).DeleteById(recordId);
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
            LblErr.Text = RenderError(Utility.GetLabel("RECORD_ERR_MSG") + "<br />" + e1.ToString());
        }
        finally { }
    }

    #endregion
}
