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
using PigeonCms.Core.Helpers;

public partial class Controls_CategoriesAdmin : PigeonCms.BaseModuleControl
{
    const int COL_ORDERING_INDEX = 3;
    const int COL_ORDER_ARROWS_INDEX = 4;
    const int COL_ACCESS_INDEX = 6;
    const int COL_FILES_INDEX = 7;
    const int COL_IMAGES_INDEX = 8;
    const int COL_ID_INDEX = 10;

    private int sectionId = 0;
    public int SectionId
    {
        get { return GetIntParam("SectionId", sectionId, "sectionid"); }
        set { sectionId = value; }
    }

    private bool mandatorySectionFilter = false;
    public bool MandatorySectionFilter
    {
        get { return GetBoolParam("MandatorySectionFilter", mandatorySectionFilter); }
    }

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

    //restrictions params
    #region params restrictions

    private bool showSecurity = true;
    public bool ShowSecurity
    {
        get { return GetBoolParam("ShowSecurity", showSecurity); }
    }

    private bool showOnlyDefaultCulture = false;
    public bool ShowOnlyDefaultCulture
    {
        get { return GetBoolParam("ShowOnlyDefaultCulture", showOnlyDefaultCulture); }
    }

    private bool showEnabledFilter = true;
    public bool ShowEnabledFilter
    {
        get { return GetBoolParam("ShowEnabledFilter", showEnabledFilter); }
    }

    private bool allowOrdering = true;
    public bool AllowOrdering
    {
        get { return GetBoolParam("AllowOrdering", allowOrdering); }
    }

    #endregion

    /// <summary>
    /// the same as ItemsAdminControl. keep synched selected section id
    /// </summary>
    protected int LastSelectedSectionId
    {
        get
        {
            var res = 0;
            var session = new SessionManager<int>("PigeonCms.ItemsAdminControl");
            if (!session.IsEmpty("LastSelectedSectionId"))
                res = session.GetValue("LastSelectedSectionId");
            return res;
        }
        set
        {
            var session = new SessionManager<int>("PigeonCms.ItemsAdminControl");
            session.Insert("LastSelectedSectionId", value);
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

        //restrictions
        Grid1.AllowSorting = false;  //this.AllowOrdering;
        Grid1.Columns[COL_ORDER_ARROWS_INDEX].Visible = this.AllowOrdering;
        Grid1.Columns[COL_ORDERING_INDEX].Visible = this.AllowOrdering;
        Grid1.Columns[COL_ACCESS_INDEX].Visible = this.ShowSecurity;
        Grid1.Columns[COL_ID_INDEX].Visible = this.ShowSecurity;
        PermissionsControl1.Visible = this.ShowSecurity;
        DropEnabledFilter.Visible = this.ShowEnabledFilter;

        if (this.TargetFilesUpload == 0) Grid1.Columns[COL_FILES_INDEX].Visible = false;
        if (this.TargetImagesUpload == 0) Grid1.Columns[COL_IMAGES_INDEX].Visible = false;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        LblOk.Text = "";
        LblErr.Text = "";

        if (!Page.IsPostBack)
        {
            loadDropEnabledFilter();
            loadDropSectionsFilter(this.SectionId);
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

        Grid1.DataBind();
        this.LastSelectedSectionId = secID;
    }

    protected void ObjDs1_ObjectCreating(object sender, ObjectDataSourceEventArgs e)
    {
        var typename = new CategoriesManager(true, true);
        e.ObjectInstance = typename;
    }

    protected void ObjDs1_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
    {
        CategoriesFilter filter = new CategoriesFilter();

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
        if (this.SectionId > 0)
            filter.SectionId = this.SectionId;
        else
        {
            int secId = -1;
            int.TryParse(DropSectionsFilter.SelectedValue, out secId);
            filter.SectionId = secId;
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

    protected void Grid1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            PigeonCms.Category item = new PigeonCms.Category();
            item = (PigeonCms.Category)e.Row.DataItem;

            if (item.SectionId > 0)
            {
                Literal LitSectionTitle = (Literal)e.Row.FindControl("LitSectionTitle");
                LitSectionTitle.Text = new SectionsManager().GetByKey(item.SectionId).Title;
            }

            LinkButton LnkTitle = (LinkButton)e.Row.FindControl("LnkTitle");
            LnkTitle.Text = "<i class='fa fa-pgn_edit fa-fw'></i>";
            LnkTitle.Text += Utility.Html.GetTextPreview(item.Title, 50, "");
            if (string.IsNullOrEmpty(LnkTitle.Text))
                LnkTitle.Text += Utility.GetLabel("NO_VALUE", "<no value>");


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
                + "?type=categories&id=" + item.Id.ToString();
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
                + "?type=categories&id=" + item.Id.ToString();
            LnkUploadImg.CssClass = "fancyRefresh";
            var LitImgCount = (Literal)e.Row.FindControl("LitImgCount");
            int imgCount = item.Images.Count;
            if (imgCount > 0)
            {
                LitImgCount.Text = imgCount.ToString();
                LitImgCount.Text += imgCount == 1 ? " file" : " files";
            }
        }
    }

    protected void BtnNew_Click(object sender, EventArgs e)
    {
        if (DropSectionsFilter.SelectedValue == "0" || DropSectionsFilter.SelectedValue == "")
        {
            LblErr.Text = RenderError(base.GetLabel("ChooseSectionBefore", "Choose a section before"));
            return;
        }
        editRow(0);
    }

    protected void BtnSave_Click(object sender, EventArgs e)
    {
        LblErr.Text = "";
        LblOk.Text = "";

        try
        {
            Category o1 = new Category();
            if (base.CurrentId == 0)
            {
                form2obj(o1);
                o1 = new CategoriesManager().Insert(o1);
            }
            else
            {
                o1 = new CategoriesManager().GetByKey(base.CurrentId);  //precarico i campi esistenti e nn gestiti dal form
                form2obj(o1);
                new CategoriesManager().Update(o1);
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
        TxtCssClass.Text = "";
        ChkEnabled.Checked = true;
        Utility.SetDropByValue(DropSections, DropSectionsFilter.SelectedValue);
        PermissionsControl1.ClearForm();
    }

    private void form2obj(Category obj)
    {
        obj.Id = base.CurrentId;
        obj.Enabled = ChkEnabled.Checked;
        obj.TitleTranslations.Clear();
        obj.DescriptionTranslations.Clear();
        obj.SectionId = int.Parse(DropSections.SelectedValue);
        obj.CssClass = TxtCssClass.Text;
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
        TxtCssClass.Text = obj.CssClass;
        ChkEnabled.Checked = obj.Enabled;
        Utility.SetDropByValue(DropSections, obj.SectionId.ToString());
        PermissionsControl1.Obj2form(obj);
    }

    private void editRow(int recordId)
    {
        LblOk.Text = "";
        LblErr.Text = "";

        clearForm();
        base.CurrentId = recordId;
        if (base.CurrentId > 0)
        {
            Category obj = new Category();
            obj = new CategoriesManager().GetByKey(base.CurrentId);
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
            new CategoriesManager().DeleteById(recordId);
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

    /// <summary>
    /// loads sections filter and section drops
    /// </summary>
    private void loadDropSectionsFilter(int sectionId)
    {
        DropSectionsFilter.Items.Clear();
        DropSections.Items.Clear();

        if (this.SectionId == 0)
        {
            if (!this.MandatorySectionFilter)
            {
                DropSectionsFilter.Items.Add(new ListItem(Utility.GetLabel("LblSelectSection", "Select section"), "0"));
                //DropSections.Items.Add(new ListItem("", "0"));  //mandatory
            }
        }

        SectionsFilter filter = new SectionsFilter();
        filter.Id = sectionId;
        List<Section> recordList = new SectionsManager(true, true).GetByFilter(filter, "");
        foreach (Section record1 in recordList)
        {
            DropSectionsFilter.Items.Add(new ListItem(record1.Title, record1.Id.ToString()));
            DropSections.Items.Add(new ListItem(record1.Title, record1.Id.ToString()));
        }
        if (this.LastSelectedSectionId > 0)
            Utility.SetDropByValue(DropSectionsFilter, this.LastSelectedSectionId.ToString());
    }

    private void setFlag(int recordId, bool value, string flagName)
    {
        try
        {
            PigeonCms.Category o1 = new PigeonCms.Category();
            o1 = new CategoriesManager().GetByKey(recordId);
            switch (flagName.ToLower())
            {
                case "enabled":
                    o1.Enabled = value;
                    break;
                default:
                    break;
            }
            new CategoriesManager().Update(o1);
        }
        catch (Exception e1)
        {
            LblErr.Text = RenderError(Utility.GetLabel("RECORD_ERR_MSG") + "<br />" + e1.ToString());
        }
        finally { }
    }

    protected void moveRecord(int recordId, Database.MoveRecordDirection direction)
    {
        LblErr.Text = "";
        LblOk.Text = "";

        try
        {
            new CategoriesManager().MoveRecord(recordId, direction);
            Grid1.DataBind();
            MultiView1.ActiveViewIndex = 0;
        }
        catch (Exception e1)
        {
            LblErr.Text = RenderError(Utility.GetLabel("RECORD_ERR_MSG") + "<br />" + e1.ToString());
        }
        finally { }
    }
    #endregion
}
