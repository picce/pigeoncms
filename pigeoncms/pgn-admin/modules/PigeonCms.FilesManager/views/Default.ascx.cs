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
using System.Globalization;
using System.IO;
using System.Diagnostics;


public partial class Controls_FilesManager : PigeonCms.FileUploadControl
{
    const int COL_PREVIEW = 0;
    const int COL_SIZE = 1;
    const int COL_META = 2;
    const int COL_DEL = 3;

    protected string ContentBeforePage
    {
        get { return base.GetStringParam("ContentBeforePage", ""); }
    }

    protected string ContentAfterPage
    {
        get { return base.GetStringParam("ContentAfterPage", ""); }
    }

    private Utility.TristateBool allowed = Utility.TristateBool.NotSet;
    protected Utility.TristateBool Allowed
    {
        get
        {
            if (allowed == Utility.TristateBool.NotSet)
            {
                string type = Utility._QueryString("type").ToLower();
                string id = Utility._QueryString("id").ToLower();

                if (!checkFolderNameGrants(type, id))
                    allowed = Utility.TristateBool.False;
                else
                    allowed = Utility.TristateBool.True;
            }
            return allowed;
        }
    }

    private bool checkFolderNameGrants(string type, string sId)
    {
        bool res = true;


        if (this.TypeParamRequired)
        {
            int id = 0;
            int.TryParse(sId, out id);

            if (string.IsNullOrEmpty(type))
                return false;

            if (id == 0 && type != "temp")
                return false;

            switch (type)
            {
                case "items":
                    {
                        var man = new ItemsManager<Item, ItemsFilter>(true, true);
                        var item = man.GetByKey(id);
                        if (item.Id == 0) res = false;
                    }
                    break;

                case "categories":
                    {
                        var man = new CategoriesManager(true, true);
                        var item = man.GetByKey(id);
                        if (item.Id == 0) res = false;
                    }
                    break;

                case "sections":
                    {
                        var man = new SectionsManager(true, true);
                        var item = man.GetByKey(id);
                        if (item.Id == 0) res = false;
                    }
                    break;

                case "temp":
                    res = this.AllowTemporaryFiles && sId == Utility._SessionID();
                    break;

                default:
                    res = false;
                    break;
            }
        }

        return res;
    }

    protected string FolderName
    {
        get
        {
            //TODO: add param DiskSpaceAvailable

            string res = "";
            if (ViewState["FolderName"] != null)
                res = (string)ViewState["FolderName"];
            else
            {
                if (this.TypeParamRequired)
                {
                    string type = Utility._QueryString("type").ToLower();
                    string id = Utility._QueryString("id").ToLower();

                    if (!string.IsNullOrEmpty(type) && !string.IsNullOrEmpty(id))
                        res = type + "/" + id;
                }

                //res = base.GetStringParam("Folder", res, "folder"); //doesnt accept params from querystring
            }

            
            if (!string.IsNullOrEmpty(res))
            {
                if (!res.EndsWith("/"))
                    res += "/";
            }

            return res;
        }
        set { ViewState["FolderName"] = value; }
    }

    bool allowFilesUpload = true;
    public bool AllowFilesUpload
    {
        get { return GetBoolParam("AllowFilesUpload", allowFilesUpload); }
        set { allowFilesUpload = value; }
    }

    bool allowFilesSelection = false;
    public bool AllowFilesSelection
    {
        get { return GetBoolParam("AllowFilesSelection", allowFilesSelection); }
        set { allowFilesSelection = value; }
    }

    bool allowFilesEdit = true;
    public bool AllowFilesEdit
    {
        get { return GetBoolParam("AllowFilesEdit", allowFilesEdit); }
        set { allowFilesEdit = value; }
    }

    bool allowFilesDel = true;
    public bool AllowFilesDel
    {
        get { return GetBoolParam("AllowFilesDel", allowFilesDel); }
        set { allowFilesDel = value; }
    }

    bool allowFoldersNavigation = false;
    public bool AllowFoldersNavigation
    {
        get { return GetBoolParam("AllowFoldersNavigation", allowFoldersNavigation); }
        set { allowFoldersNavigation = value; }
    }

    private bool allowNewFolder = false;
    public bool AllowNewFolder
    {
        get { return GetBoolParam("AllowNewFolder", allowNewFolder); }
        set { allowNewFolder = value; }
    }

    private bool typeParamRequired = true;
    public bool TypeParamRequired
    {
        get { return GetBoolParam("TypeParamRequired", typeParamRequired); }
        set { typeParamRequired = value; }
    }


    bool allowTemporaryFiles = false;
    public bool AllowTemporaryFiles
    {
        get { return GetBoolParam("AllowTemporaryFiles", allowTemporaryFiles); }
        set { allowTemporaryFiles = value; }
    }

    protected new void Page_Init(object sender, EventArgs e)
    {
        base.Page_Init(sender, e);

        Utility.Script.RegisterClientScriptInclude(
            this, "pigeoncms.filesmanager", base.CurrViewPath + "FilesManager.js");


        initFileUploadControl();

        foreach (KeyValuePair<string, string> item in Config.CultureList)
        {
            //title
            TextBox txt1 = new TextBox();
            txt1.ID = "TxtTitle" + item.Value;
            txt1.MaxLength = 50;
            txt1.CssClass = "adminMediumText";
            txt1.ToolTip = item.Key;
            PanelTitle.Controls.Add(txt1);
            Literal lit1 = new Literal();
            lit1.Text = "&nbsp;[<i>" + item.Value + "</i>]<br /><br />";
            PanelTitle.Controls.Add(lit1);

            //description
            TextBox txt2 = new TextBox();
            txt2.ID = "TxtDescription" + item.Value;
            txt2.TextMode = TextBoxMode.MultiLine;
            txt2.Rows = 3;
            txt2.CssClass = "adminMediumText";
            PanelDescription.Controls.Add(txt2);
            Literal lit2 = new Literal();
            lit2.Text = "&nbsp;[<i>" + item.Value + "</i>]<br /><br />";
            PanelDescription.Controls.Add(lit2);
        }
        BtnNewFolder.Visible = this.AllowNewFolder && this.Allowed==Utility.TristateBool.True;
        TxtNewFolder.Visible = this.AllowNewFolder && this.Allowed == Utility.TristateBool.True;
        BtnParentFolder.Visible = this.AllowFoldersNavigation && this.Allowed == Utility.TristateBool.True;
        Grid1.Columns[COL_META].Visible = this.AllowFilesEdit;

        Grid1.Columns[COL_PREVIEW].HeaderText = base.GetLabel("Preview", "");
        Grid1.Columns[COL_SIZE].HeaderText = base.GetLabel("Size", "Size");
        Grid1.Columns[COL_META].HeaderText = base.GetLabel("Meta", "Meta data");
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        LblOk.Text = "";
        LblErr.Text = "";

        if (!Page.IsPostBack)
        {
            LitContentBefore.Text = getPageContent(this.ContentBeforePage);
            LitContentAfter.Text = getPageContent(this.ContentAfterPage);

            loadGrid();
        }
    }

    protected void Grid1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        LblErr.Text = "";
        LblOk.Text = "";

        try
        {
            if (e.CommandName == "Select")
            {
                if (this.AllowFilesEdit)
                    editRow(e.CommandArgument.ToString());
            }
            if (e.CommandName == "DeleteRow")
            {
                if (this.AllowFilesDel)
                {
                    string[] commandArgs = e.CommandArgument.ToString().Split(new char[] { ',' });
                    deleteRow(commandArgs[0], bool.Parse(commandArgs[1]));
                }
            }
            if (e.CommandName == "NavigateFolder")
            {
                if (this.AllowFoldersNavigation)
                    navigateFolder(e.CommandArgument.ToString());
            }
        }
        catch (Exception e1)
        {
            LblErr.Text = e1.ToString();
        }
        finally
        {
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
            var file = (FileMetaInfo)e.Row.DataItem;
            Image ImgPreview = (Image)e.Row.FindControl("ImgPreview");
            if (file.IsFolder)
            {
                if (!this.AllowFoldersNavigation)
                    e.Row.Visible = false;
                ImgPreview.ImageUrl = Utility.GetThemedImageSrc("explorer/folder.gif");
            }
            else
            {
                ImgPreview.ImageUrl = PhotoManager.GetFileIconSrc(file);
                if (this.AllowFilesSelection)
                {
                    //file selection
                    ImgPreview.ToolTip = this.GetLabel("Select", "Select");
                    ImgPreview.Attributes.Add("onclick",
                        "ImageManager.populateFields('" + file.FileUrl + "','" + file.Title + "'); " +
                        "ImageManager.onok(); " +
                        "parent.$.fancybox.close();");
                    ImgPreview.Style.Add("cursor", "pointer");
                }
            }

            var btnNavigate = (LinkButton)e.Row.FindControl("BtnNavigate");
            var lnkFileName = (HyperLink)e.Row.FindControl("LnkFileName");
            if (file.IsFolder)
            {
                btnNavigate.Text = file.FileName;
            }
            else
            {
                lnkFileName.ToolTip = this.GetLabel("Preview", "Preview") + " - " + file.FileName;
                lnkFileName.Text = file.FileName;
                lnkFileName.NavigateUrl = file.FileUrl;
                lnkFileName.Target = "_blank";
            }

            //meta tag edit
            if (!this.AllowFilesEdit)
                ((ImageButton)e.Row.FindControl("LnkSel")).Visible = false;
            if (!this.AllowFilesDel)
                ((ImageButton)e.Row.FindControl("LnkDel")).Visible = false;
        }
    }

    protected void FileUpload1_AfterUpload(object sender, FileUploadControl.FileUploadEventArgs e)
    {
        loadGrid();
    }

    protected void BtnSave_Click(object sender, EventArgs e)
    {
        LblErr.Text = "";
        LblOk.Text = "";

        try
        {
            var o1 = new FileMetaInfo(base.CurrentKey);
            var originalFile = new FileMetaInfo(base.CurrentKey);

            form2obj(o1);
            o1.SaveData();

            if (originalFile.FileName != TxtFileName.Text)
            {
                new FilesGallery(FileUpload1.FilePath, "").RenameFile(originalFile.FileName, TxtFileName.Text);
            }

            loadGrid();
            LblOk.Text = Utility.GetLabel("RECORD_SAVED_MSG");
            MultiView1.ActiveViewIndex = 0;
        }
        catch (Exception e1)
        {
            LblErr.Text = Utility.GetLabel("RECORD_ERR_MSG") + "<br />" + e1.ToString();
        }
        finally
        {
        }
    }

    protected void BtnCancel_Click(object sender, EventArgs e)
    {
        MultiView1.ActiveViewIndex = 0;
    }

    protected void BtnNewFolder_Click(object sender, EventArgs e)
    {
        LblOk.Text = "";
        LblErr.Text = "";

        try
        {
            if (!string.IsNullOrEmpty(TxtNewFolder.Text))
            {
                new FilesGallery(base.FilePath+this.FolderName, "").CreateFolder(TxtNewFolder.Text);
                loadGrid();
            }
        }
        catch (Exception ex)
        {
            LblErr.Text = ex.Message;
        }
    }

    protected void BtnParentFolder_Click(object sender, EventArgs e)
    {
        navigateFolder("..");
    }

    #region private methods

    private void loadGrid()
    {
        if (this.Allowed == Utility.TristateBool.True)
        {
            var files = new List<FileMetaInfo>();
            string searchPattern = "";
            if (this.FileNameType == FileNameTypeEnum.ForceFileName && !string.IsNullOrEmpty(this.ForcedFilename))
                searchPattern = this.ForcedFilename + ".*";

            files = new FilesGallery(FileUpload1.FilePath, "", searchPattern).GetAll();
            Grid1.DataSource = files;
            Grid1.DataBind();
            int count = files.Count;

            if (this.NumOfFilesAllowed > 0 && count >= this.NumOfFilesAllowed)
                FileUpload1.Visible = false;
            else
                FileUpload1.Visible = true;
        }
    }

    private void clearForm()
    {
        TxtFileName.Text = "";
        foreach (KeyValuePair<string, string> item in Config.CultureList)
        {
            TextBox t1 = new TextBox();
            t1 = (TextBox)PanelTitle.FindControl("TxtTitle" + item.Value);
            t1.Text = "";

            TextBox t2 = new TextBox();
            t2 = (TextBox)PanelDescription.FindControl("TxtDescription" + item.Value);
            t2.Text = "";
        }
    }

    private void form2obj(FileMetaInfo obj)
    {
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
    }

    private void obj2form(FileMetaInfo obj)
    {
        TxtFileName.Text = obj.FileName;
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
    }

    private void editRow(string fileUrl)
    {
        LblOk.Text = "";
        LblErr.Text = "";

        clearForm();
        base.CurrentKey = fileUrl;
        if (!string.IsNullOrEmpty(fileUrl))
        {
            var obj = new FileMetaInfo(base.CurrentKey);
            obj2form(obj);
        }
        MultiView1.ActiveViewIndex = 1;
    }

    private void deleteRow(string fileName, bool isFolder)
    {
        LblOk.Text = "";
        LblErr.Text = "";

        try
        {
            new FilesGallery(FileUpload1.FilePath, "").DeleteByFileName(fileName, isFolder);
            loadGrid();
            LblOk.Text = Utility.GetLabel("RECORD_DELETE_MSG");
        }
        catch (Exception e)
        {
            LblErr.Text = e.Message;
        }
    }

    private void navigateFolder(string folder)
    {
        LblOk.Text = "";
        LblErr.Text = "";

        try
        {
            if (folder == "..")
            {
                string currentFolder = this.FolderName;
                string parentFolder = "";
                if (!string.IsNullOrEmpty(currentFolder))
                    parentFolder = currentFolder.Substring(0, currentFolder.LastIndexOf(new DirectoryInfo(currentFolder).Name + "/"));
                this.FolderName = parentFolder;
            }
            else
            {
                this.FolderName += folder;
            }
            initFileUploadControl();
            loadGrid();
        }
        catch (Exception e)
        {
            LblErr.Text = e.Message;
        }
    }

    private void initFileUploadControl()
    {
        //FileUpload1.All
        FileUpload1.FileExtensions = base.FileExtensions;
        FileUpload1.FileSize = getMaxFileSize();
        FileUpload1.FileNameType = base.FileNameType;
        FileUpload1.FilePrefix = base.FilePrefix;
        FileUpload1.ForcedFilename = base.ForcedFilename;
        FileUpload1.UploadFields = base.UploadFields;
        FileUpload1.NumOfFilesAllowed = base.NumOfFilesAllowed;
        FileUpload1.ShowWorkingPath = base.ShowWorkingPath;
        FileUpload1.CustomWidth = base.CustomWidth;
        FileUpload1.CustomHeight = base.CustomHeight;
        FileUpload1.HeaderText = base.HeaderText;
        FileUpload1.FooterText = base.FooterText;
        FileUpload1.SuccessText = base.SuccessText;
        FileUpload1.ErrorText = base.ErrorText;

        FileUpload1.FilePath = base.FilePath;
        FileUpload1.FilePath += this.FolderName;

        if (!this.AllowFilesUpload || this.Allowed == Utility.TristateBool.False)
        {
            FileUpload1.UploadFields = 0;
            FileUpload1.Visible = false;
        }

        //int currentId = 0;
        //int.TryParse(Request.QueryString["id"], out currentId);
        //if (currentId > 0)
        //{
        //    FileUpload1.FilePath += currentId.ToString();
        //}
    }

    private int getMaxFileSize()
    {
        int res = base.FileSize;
        if (this.TypeParamRequired)
        {
            string type = Utility._QueryString("type").ToLower();
            if (type == "items")
            {
                int id = 0;
                int.TryParse(Utility._QueryString("id"), out id);
                if (id > 0)
                {
                    var item = new Item();
                    int remainSize = 0;
                    int maxAttachSizeKB = 0;
                    int sizeOfItemsKB = 0;

                    item = new ItemsManager<Item, ItemsFilter>(true, true).GetByKey(id);
                    maxAttachSizeKB = item.Category.Section.MaxAttachSizeKB;
                    if (maxAttachSizeKB > 0)
                    {
                        try { sizeOfItemsKB = (int)(item.Category.Section.SizeOfItems / 1024); }
                        catch { }
                        remainSize = maxAttachSizeKB - sizeOfItemsKB;
                        if (remainSize < res)
                            res = remainSize;
                    }
                }
            }

        }
        return res;
    }

    private string getPageContent(string pageName)
    {
        string res = "";
        if (!string.IsNullOrEmpty(pageName))
        {
            var pagesMan = new PigeonCms.StaticPagesManager();
            var page = pagesMan.GetStaticPageByName(pageName);
            res = page.PageContent;
        }
        return res;
    }

    #endregion
}
