using System;
using System.Data;
using System.Linq;
using System.Configuration;
using System.Collections;
using System.IO;
using System.Data.OleDb;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Web.Caching;
using System.Collections.Generic;
using PigeonCms;


public partial class Controls_Default : PigeonCms.BaseModuleControl
{
    const int COL_SELECT = 0;
    const int COL_RESSET = 1;
    const int COL_RESID = 2;
    const int COL_TEXTMODE = 3;
    const int COL_VALUES = 4;
    const int COL_DELETE = 5;
    const int COL_VALUE_1 = 6;
    const int COL_VALUE_2= 7;
    const int COL_VALUE_3 = 8;
    const int COL_VALUE_4 = 9;
    const int COL_VALUE_5 = 10;
    const string FAKE_RESOURCESET = "xxx";
    const int VIEW_GRID_IDX = 0;
    const int VIEW_EDIT_IDX = 1;
    const int VIEW_IMPORT_IDX = 2;

    public string BtnActionClass = "";

    private enum MissingFilterEnum
    {
        AllValues = 0,
        MissingValues = 1
    }

    private List<PigeonCms.Culture> culturesList = null;
    private List<PigeonCms.Culture> CultureList
    {
        get
        {
            if (culturesList == null)
            {
                culturesList = new List<PigeonCms.Culture>();
                var cultMan = new PigeonCms.CulturesManager();
                var cultFilter = new PigeonCms.CulturesFilter();
                cultFilter.Enabled = Utility.TristateBool.NotSet;
                if (this.ShowOnlyEnabledCultures)
                    cultFilter.Enabled = Utility.TristateBool.True;

                culturesList = cultMan.GetByFilter(cultFilter, "");
            }
            return culturesList;
        }
    }

    protected string ModuleFullName
    {
        get { return base.GetStringParam("ModuleFullName", "", "ModuleFullName"); }
    }

    protected string ModuleFullNamePart
    {
        get { return base.GetStringParam("ModuleFullNamePart", ""); }
    }

    /// <summary>
    /// Target for resources with Textmode=Image
    /// </summary>
    protected int TargetImagesUpload
    {
        get { return GetIntParam("TargetImagesUpload", 0); }
    }

    /// <summary>
    /// Default resource folder
    /// </summary>
    protected string DefaultResourceFolder
    {
        get { return base.GetStringParam("DefaultResourceFolder", "~/public/res"); }
    }

    /// <summary>
    /// Allow new resource. default false
    /// </summary>
    public bool AllowNew
    {
        get
        {

            bool res = GetBoolParam("AllowNew", false);
            if (this.AllowAdminMode && Roles.IsUserInRole("admin"))
            {
                res = true;
            }
            return res;
        }
    }

    /// <summary>
    /// Allow delete resource. default false
    /// </summary>
    public bool AllowDel
    {
        get
        {

            bool res = GetBoolParam("AllowDel", false);
            if (this.AllowAdminMode && Roles.IsUserInRole("admin"))
            {
                res = true;
            }
            return res;
        }
    }

    /// <summary>
    /// Allow text mode edit. default false
    /// </summary>
    public bool AllowTextModeEdit
    {
        get
        {

            bool res = GetBoolParam("AllowTextModeEdit", false);
            if (this.AllowAdminMode && Roles.IsUserInRole("admin"))
            {
                res = true;
            }
            return res;
        }
    }

    /// <summary>
    /// Allow resource params edit. default false
    /// </summary>
    public bool AllowParamsEdit
    {
        get
        {
            bool res = GetBoolParam("AllowParamsEdit", false);
            if (this.AllowAdminMode && Roles.IsUserInRole("admin"))
            {
                res = true;
            }
            return res;
        }
    }

    /// <summary>
    /// Allow import/export from Excel file
    /// </summary>
    public bool AllowImportExport
    {
        get
        {
            bool res = GetBoolParam("AllowImportExport", false);
            if (this.AllowAdminMode && Roles.IsUserInRole("admin"))
            {
                res = true;
            }
            return res;
        }
    }

    /// <summary>
    /// Users in admin role override other security settings. Default true
    /// </summary>
    public bool AllowAdminMode
    {
        get { return GetBoolParam("AllowAdminMode", true); }
    }

    /// <summary>
    /// true: show only enabled cultures
    /// false: show all inserted cultures
    /// </summary>
    public bool ShowOnlyEnabledCultures
    {
        get { return GetBoolParam("ShowOnlyEnabledCultures", false); }
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

    /// <summary>
    /// number of columns added in grid for export purpose only
    /// </summary>
    private int CultureColumnsCount
    {
        get
        {
            int res = 5;
            if (this.CultureList.Count < res)
                res = this.CultureList.Count;
            return res;
        }
    }

    public ContentEditorProvider.Configuration.EditorTypeEnum LastTextMode
    {
        get
        {
            var res = ContentEditorProvider.Configuration.EditorTypeEnum.Text;
            if (ViewState["LastTextMode"] != null)
                res = (ContentEditorProvider.Configuration.EditorTypeEnum)ViewState["LastTextMode"];
            return res;
        }
        set
        {
            ViewState["LastTextMode"] = value;
        }
    }

    protected new void Page_Init(object sender, EventArgs e)
    {
        base.Page_Init(sender, e);

        if (Page.IsPostBack)
        {
            string eventArg = HttpContext.Current.Request["__EVENTARGUMENT"];
            if (!ScriptManager.GetCurrent(Page).IsInAsyncPostBack)
            {
                if (eventArg.Contains("edit__"))
                {
                    string args = eventArg.Replace("edit__", "");

                    //check resource textmode
                    var obj = new LabelsManager().GetLabelTransByKey(getResSet(args), getResId(args));
                    initLangControls(obj.TextMode);
                    editRow(getResSet(args), getResId(args));
                }
                else if (eventArg == "grid")
                {
                    loadGrid(Grid1);
                }
                else
                {
                    int textMode = 0;
                    int.TryParse((string)Request.Form[DropTextMode.UniqueID], out textMode);
                    initLangControls((ContentEditorProvider.Configuration.EditorTypeEnum)textMode);

                }
            }
            else
            {


                int textMode = 0;
                int.TryParse((string)Request.Form[DropTextMode.UniqueID], out textMode);
                initLangControls((ContentEditorProvider.Configuration.EditorTypeEnum)textMode);
            }
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        LblOk.Text = RenderSuccess("");
        LblErr.Text = RenderError("");

        BtnActionClass = "";
        if (!this.AllowImportExport)
            BtnActionClass = "display-none";

        if (!Page.IsPostBack)
        {
            loadDropsModuleTypes();
            loadDropMissingFilter();
            loadDropTextMode();
            loadGrid(Grid1);

            BtnNew.Visible = this.AllowNew;
            DropTextMode.Enabled = this.AllowTextModeEdit;
            TxtComment.Enabled = this.AllowParamsEdit;
            TxtResourceParams.Enabled = this.AllowParamsEdit;
            Grid1.Columns[COL_DELETE].Visible = this.AllowDel;
            BtnImport.Visible = this.AllowImportExport;
            BtnExport.Visible = this.AllowImportExport;

            //prepare hidden cols for export
            for (int i = 0; i < this.CultureColumnsCount; i++)
            {
                var culture = this.CultureList[i];
                Grid1.Columns[COL_DELETE + i+1].HeaderText = "Value " + culture.CultureCode;
            }
        }
        else
        {
            //TOCHECK - remove?
            string eventArg = HttpContext.Current.Request["__EVENTARGUMENT"];
            if (!ScriptManager.GetCurrent(Page).IsInAsyncPostBack)
            {
                if (eventArg.Contains("edit__"))
                {
                    string args = eventArg.Replace("edit__", "");
                    editRow(getResSet(args), getResId(args));
                }
            }

            if (eventArg == "gridPreview")
            {
                loadGridImportPreview();
            }
        }

        //fileupload
        Page.Form.Attributes.Add("enctype", "multipart/form-data");
    }

    protected void Grid1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        //done client side
        //if (e.CommandName == "Select")
        //{
        //    editRow(e.CommandArgument.ToString());
        //}
        if (e.CommandName == "DeleteRow")
        {
            deleteRow(e.CommandArgument.ToString());
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
            var item = new ResLabelTrans();
            item = (ResLabelTrans)e.Row.DataItem;

            var LitSel = (Literal)e.Row.FindControl("LitSel");
            LitSel.Text = "<a href='javascript:void(0)' onclick=\"editRow('edit__" + item.ResourceSet + "|" + item.ResourceId + "');\"><i class='fa fa-pgn_edit fa-fw'></i></a>";

            var LitResourceSet = (Literal)e.Row.FindControl("LitResourceSet");
            LitResourceSet.Text = item.ResourceSet;

            var LitResourceId = (Literal)e.Row.FindControl("LitResourceId");
            LitResourceId.Text = item.ResourceId;

            var LitTextMode = (Literal)e.Row.FindControl("LitTextMode");
            LitTextMode.Text = item.TextMode.ToString();

            var LitValue = (Literal)e.Row.FindControl("LitValue");
            string values = "";
            string defaultValue = "";

            List<Literal> LitValues = new List<Literal>();
            LitValues.Add((Literal)e.Row.FindControl("LitValue1"));
            LitValues.Add((Literal)e.Row.FindControl("LitValue2"));
            LitValues.Add((Literal)e.Row.FindControl("LitValue3"));
            LitValues.Add((Literal)e.Row.FindControl("LitValue4"));
            LitValues.Add((Literal)e.Row.FindControl("LitValue5"));


            var labelList = new List<ResLabel>();
            var man = new LabelsManager();
            var lfilter = new LabelsFilter();
            lfilter.ResourceSet = item.ResourceSet;
            lfilter.ResourceId = item.ResourceId;
            labelList = man.GetByFilter(lfilter, "");
            
            const string ROW = "<span class='[[rowClass]]'><i>[[rowCulture]]</i>: [[rowLabel]]</span><br>";
            int cultureIdx = 0;
            foreach (var culture in this.CultureList)
            {
                ResLabel label = labelList.FirstOrDefault(
                    s => s.CultureName == culture.CultureCode);

                if (label == null)
                    label = new ResLabel();

                if (string.IsNullOrEmpty(defaultValue))
                    defaultValue = label.Value;

                string rowClass = "text-success";
                if (string.IsNullOrEmpty(label.Value))
                {
                    //label.Value = "<i>NO VALUE</i>";
                    if (culture.Enabled)
                        rowClass = "text-danger";//missing
                    else
                        rowClass = "text-warning";//missing but cutlure not enabled
                }

                values += ROW
                    .Replace("[[rowClass]]", rowClass)
                    .Replace("[[rowCulture]]", culture.CultureCode)
                    .Replace("[[rowLabel]]", Utility.Html.GetTextPreview(label.Value, 50, ""));

                if (cultureIdx < this.CultureColumnsCount)
                    LitValues[cultureIdx].Text = label.Value;

                cultureIdx++;
            }
            LitValue.Text += values;

            //image preview on Image TextMode
            Image ImgPreview = (Image)e.Row.FindControl("ImgPreview");
            ImgPreview.Visible = false;
            if (item.TextMode == ContentEditorProvider.Configuration.EditorTypeEnum.Image)
            {
                ImgPreview.Visible = true;
                var file = new FileMetaInfo(defaultValue);
                ImgPreview.ImageUrl = PhotoManager.GetFileIconSrc(file, true);
            }

        }
    }

    protected void Grid1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        Grid1.PageIndex = e.NewPageIndex;
        loadGrid(Grid1);
    }

    protected void GridImportPreview_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        LblErr.Text = "";
        LblOk.Text = "";

        try
        {
            if (e.CommandName == "ImgEnabledOk")
            {
                setFlagUserTempData(Convert.ToInt32(e.CommandArgument), false, "enabled");
                loadGridImportPreview();
            }
            if (e.CommandName == "ImgEnabledKo")
            {
                setFlagUserTempData(Convert.ToInt32(e.CommandArgument), true, "enabled");
                loadGridImportPreview();
            }
        }
        catch (Exception e1)
        {
            LblErr.Text = RenderError(e1.Message);
        }
        finally
        {
        }
    }

    protected void GridImportPreview_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
            Utility.AddGlyph(GridImportPreview, e.Row);
    }

    protected void GridImportPreview_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        const int TXT_PREVIEW_LEN = 80;
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            var item = (UserTempData)e.Row.DataItem;
            var LitCol01 = (Literal)e.Row.FindControl("LitCol01");
            var LitCol02 = (Literal)e.Row.FindControl("LitCol02");
            var LitCol03 = (Literal)e.Row.FindControl("LitCol03");
            var LitCol04 = (Literal)e.Row.FindControl("LitCol04");
            var LitCol05 = (Literal)e.Row.FindControl("LitCol05");
            var LitCol06 = (Literal)e.Row.FindControl("LitCol06");

            LitCol01.Text = HttpUtility.HtmlEncode(Utility.Html.GetTextPreview(item.Columns[0], TXT_PREVIEW_LEN, "", false));
            LitCol02.Text = HttpUtility.HtmlEncode(Utility.Html.GetTextPreview(item.Columns[1], TXT_PREVIEW_LEN, "", false));
            LitCol03.Text = HttpUtility.HtmlEncode(Utility.Html.GetTextPreview(item.Columns[2], TXT_PREVIEW_LEN, "", false));
            LitCol04.Text = HttpUtility.HtmlEncode(Utility.Html.GetTextPreview(item.Columns[3], TXT_PREVIEW_LEN, "", false));
            LitCol05.Text = HttpUtility.HtmlEncode(Utility.Html.GetTextPreview(item.Columns[4], TXT_PREVIEW_LEN, "", false));
            LitCol06.Text = HttpUtility.HtmlEncode(Utility.Html.GetTextPreview(item.Columns[5], TXT_PREVIEW_LEN, "", false));

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
        }
    }

    protected void GridImportPreview_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridImportPreview.PageIndex = e.NewPageIndex;
        loadGridImportPreview();
    }

    protected void BtnSave_Click(object sender, EventArgs e)
    {
        if (checkForm())
        {
            if (saveForm())
                MultiView1.ActiveViewIndex = VIEW_GRID_IDX;
        }
    }

    protected void BtnNew_Click(object sender, EventArgs e)
    {
        LblErr.Text = RenderError("");
        if (DropModuleTypesFilter.SelectedValue != FAKE_RESOURCESET)
            editRow(DropModuleTypesFilter.SelectedValue, "");
        else
            LblErr.Text = RenderError(
                base.GetLabel("SelectResource", "Please select a resource set"));

    }

    protected void BtnCancel_Click(object sender, EventArgs e)
    {
        LblErr.Text = RenderError("");
        LblOk.Text = RenderSuccess("");
        MultiView1.ActiveViewIndex = VIEW_GRID_IDX;

        loadGrid(Grid1);
    }

    protected void BtnApplyImport_Click(object sender, EventArgs e)
    {
        //LblErr.Text = "";
        //LblOk.Text = "";

        //try
        //{
        //    int records = importData();
        //    updateCatalogueDate();
        //    MultiView1.ActiveViewIndex = VIEW_FINISH;

        //    string fin = base.GetLabel("FinishMessage", "You have sucessfully imported %1 items in your catalogue");
        //    fin = fin.Replace("%1", records.ToString());
        //    LitFinish.Text = fin;
        //    LogProvider.Write(this.BaseModule, records.ToString() + " items imported", TracerItemType.Info);
        //}
        //catch (Exception e1)
        //{
        //    LblErr.Text = Utility.GetLabel("RECORD_ERR_MSG") + "<br />" + e1.ToString();
        //}
        //finally
        //{
        //}
    }

    protected void BtnImport_Click(object sender, EventArgs e)
    {
        MultiView1.ActiveViewIndex = VIEW_IMPORT_IDX;
        BtnApplyImport.Enabled = false;
        deleteUserTempData();
        loadDropsImport();
        loadGridImportPreview();
    }

    protected void BtnExport_Click(object sender, EventArgs e)
    {
        LblOk.Text = "";
        LblErr.Text = "";

        try
        {
            int rows = exportToExcel(prepareGridForExport(Grid1), "labels_" + DropModuleTypesFilter.SelectedValue);

            if (rows > 0)
                LblOk.Text = RenderSuccess(rows.ToString() + " exported");
            else
                LblErr.Text = RenderError("No rows to export");
        }
        catch (Exception ex)
        {
            LblErr.Text = RenderError("Error during export procedure. Check log errors");

            LogProvider.Write(this.BaseModule, 
                "BtnExport_Click>Error during export procedure. Ex:" + ex.ToString(), 
                TracerItemType.Error);
        }

    }

    protected void BtnImportSelectAll_Click(object sender, EventArgs e)
    {
        selectAll(true);
    }

    protected void BtnImportDeselectAll_Click(object sender, EventArgs e)
    {
        selectAll(false);
    }

    protected void Filter_Changed(object sender, EventArgs e)
    {
        loadGrid(Grid1);
    }

    protected void DropTextMode_TextChanged(object sender, EventArgs e)
    {
        int textMode = 0;
        int.TryParse(DropTextMode.SelectedValue, out textMode);
        var editorType = (ContentEditorProvider.Configuration.EditorTypeEnum)textMode;

        var obj = new LabelsManager().GetLabelTransByKey(getResSet(this.CurrentKey), getResId(this.CurrentKey));

        if (string.IsNullOrEmpty(obj.ResourceSet))
            obj.ResourceSet = DropModuleTypesFilter.SelectedValue;
        if (string.IsNullOrEmpty(obj.ResourceId))
            obj.ResourceId = TxtResourceId.Text;

        obj.TextMode = editorType;
        obj2form(obj);

    }

    protected void MultiView1_ActiveViewChanged(object sender, EventArgs e)
    {
        if (MultiView1.ActiveViewIndex == VIEW_GRID_IDX)    //list view
        {
        }

        if (MultiView1.ActiveViewIndex == VIEW_EDIT_IDX)    //edit view
        {
        }
    }

    protected void File1_UploadedComplete(object sender, AjaxControlToolkit.AsyncFileUploadEventArgs e)
    {
        var uploadField = (AjaxControlToolkit.AsyncFileUpload)sender;

        if (uploadField.HasFile)
        {
            string path = FilesHelper.MapPathWhenVirtual(this.DefaultResourceFolder);

            //filename = ResourceSet_ResourceKey.extension
            string filename = this.CurrentKey.Replace("|", "_") + "_" + uploadField.FileName;

            string filePath = Path.Combine(path, filename);
            string fileUrl = VirtualPathUtility.Combine(this.DefaultResourceFolder, filename);

            PigeonCms.LogProvider.Write(
                this.BaseModule,
                "file upload completed --path:" + path + " -- filename:" + filename + " -- filepath:" + filePath + " -- fileurl: " + fileUrl,
                PigeonCms.TracerItemType.Debug
            );


            DirectoryInfo dir = new DirectoryInfo(path);
            if (!dir.Exists)
                dir.Create();

            uploadField.SaveAs(filePath);
        }
    }

    protected void UploadPreview_OnUploadedComplete(object sender, AjaxControlToolkit.AsyncFileUploadEventArgs e)
    {
        System.Threading.Thread.Sleep(500);

        string destFilename = e.filename;
        if (!(sender is AjaxControlToolkit.AsyncFileUpload))
            return;


        var fu = (AjaxControlToolkit.AsyncFileUpload)sender;
        if (!fu.HasFile)
            return;

        string fileExtension = PigeonCms.Utility.GetFileExt(fu.FileName.ToLower());
        if (fileExtension != "xls" && fileExtension != "xlsx")
        {
            LblErr.Text = RenderError("File type not allowed");
            return;
        }

        try
        {
            string destPath = FilesHelper.MapPathWhenVirtual(Config.TempUserSessionPath);
            DirectoryInfo dir = new DirectoryInfo(destPath);
            if (!dir.Exists)
                dir.Create();

            string filePath = Path.Combine(destPath, destFilename);
            fu.SaveAs(filePath);

            LogProvider.Write(this.BaseModule, "UploadPreview_OnUploadedComplete:" + filePath);

            importPreviewFromExcel(filePath, true);
        }
        catch (Exception ex)
        {
            LblErr.Text = RenderError(ex.Message);
            LogProvider.Write(this.BaseModule, "UploadPreview_OnUploadedComplete:" + ex.ToString(), TracerItemType.Error);

        }
    }

    protected void UploadPreview_UploadedFileError(object sender, AjaxControlToolkit.AsyncFileUploadEventArgs e)
    {
        LogProvider.Write(this.BaseModule,
            "UploadPreview_UploadedFileError: state=" + e.state + "; message=" + e.statusMessage,
            TracerItemType.Error);
    }

   
    private void clearForm()
    {
        LitResourceSet.Text = "";
        TxtResourceId.Text = "";
        TxtComment.Text = "";
        TxtResourceParams.Text = "";

        foreach (var item in this.CultureList)
        {
            setTransArea("TxtValue", PanelValue, null, item.ToKeyValuePay());
        }
        Utility.SetDropByValue(DropTextMode, "2");
    }

    private void form2obj(ResLabelTrans obj)
    {
        obj.ResourceSet = LitResourceSet.Text;
        obj.ResourceId = TxtResourceId.Text;
        obj.ResourceParams = TxtResourceParams.Text;
        obj.Comment = TxtComment.Text;

        int textMode = 0;
        int.TryParse(DropTextMode.SelectedValue, out textMode);
        obj.TextMode = (ContentEditorProvider.Configuration.EditorTypeEnum)textMode;
    }

    private void obj2form(ResLabelTrans obj)
    {
        var label = new ResLabel();
        var labelsList = new List<ResLabel>();
        var filter = new LabelsFilter();
        var lman = new LabelsManager();

        LitResourceSet.Text = obj.ResourceSet;
        TxtResourceId.Text = obj.ResourceId;
        TxtResourceId.Enabled = true;
        TxtResourceParams.Text = obj.ResourceParams;
        TxtComment.Text = obj.Comment;
        TxtResourceParams.Enabled = obj.TextMode == ContentEditorProvider.Configuration.EditorTypeEnum.Image;
        Utility.SetDropByValue(DropTextMode, ((int)obj.TextMode).ToString());

        filter.ResourceSet = obj.ResourceSet;
        filter.ResourceId = obj.ResourceId;

        if (!string.IsNullOrEmpty(obj.ResourceId))
        {
            TxtResourceId.Enabled = false;
            foreach (var item in this.CultureList)
            {
                var culture = item.ToKeyValuePay();
                label = new ResLabel();
                var TxtValue = new Controls_ContentEditorControl();
                TxtValue = (Controls_ContentEditorControl)PanelValue.FindControl("TxtValue" + culture.Value);

                filter.CultureName = culture.Key;
                labelsList = lman.GetByFilter(filter, "");
                if (labelsList.Count > 0)
                    label = labelsList[0];
                TxtValue.Text = label.Value;
            }
        }
    }

    private void editRow(string resourceSet, string resourceId)
    {
        var obj = new PigeonCms.ResLabelTrans();
        LblOk.Text = RenderSuccess("");
        LblErr.Text = RenderError("");

        clearForm();
        CurrentKey = resourceSet + "|" + resourceId;
        if (string.IsNullOrEmpty(resourceId))
        {
            obj.ResourceSet = resourceSet;
            obj.ResourceId = resourceId;
            obj2form(obj);
        }
        else
        {
            obj = new LabelsManager().GetLabelTransByKey(resourceSet, resourceId);
            obj2form(obj);
        }

        string filename = this.CurrentKey.Replace("|", "_") + "_";
        string fileUrl = this.DefaultResourceFolder + "/" + filename;
        TxtCurrentPath.Value = VirtualPathUtility.ToAbsolute(fileUrl);

        MultiView1.ActiveViewIndex = VIEW_EDIT_IDX;
    }

    private bool checkForm()
    {
        LblErr.Text = "";
        string err = "";
        LblOk.Text = RenderSuccess("");
        bool res = true;

        if (string.IsNullOrEmpty(LitResourceSet.Text))
        {
            err += "Invalid resource set<br>";
            res = false;
        }

        if (string.IsNullOrEmpty(TxtResourceId.Text))
        {
            err += "Invalid resource Id<br>";
            res = false;
        }
        if (!res)
            LblErr.Text = RenderError(err);

        return res;
    }

    private bool saveForm()
    {
        bool res = false;
        var man = new LabelsManager();
        var lFilter = new LabelsFilter();
        LblErr.Text = RenderError("");
        LblOk.Text = RenderSuccess("");

        try
        {
            var o1 = new ResLabelTrans();
            o1 = man.GetLabelTransByKey(getResSet(CurrentKey), getResId(CurrentKey));
            form2obj(o1);
            foreach (var item in this.CultureList)
            {
                var label = new ResLabel();
                var labelList = new List<ResLabel>();
                var TxtValue = new Controls_ContentEditorControl();
                
                TxtValue = (Controls_ContentEditorControl)PanelValue.FindControl("TxtValue" + item.ToKeyValuePay().Value);

                lFilter.ResourceSet = o1.ResourceSet;
                lFilter.ResourceId = o1.ResourceId;
                lFilter.CultureName = item.CultureCode;
                labelList = man.GetByFilter(lFilter, "");
                if (labelList.Count > 0)
                {
                    label = labelList[0];
                    label.Value = TxtValue.Text;
                    label.Comment = TxtComment.Text;
                    label.TextMode = o1.TextMode;
                    label.ResourceParams = TxtResourceParams.Text;
                    man.Update(label);
                }
                else
                {
                    label.ResourceSet = o1.ResourceSet;
                    label.ResourceId = o1.ResourceId;
                    label.CultureName = item.CultureCode;
                    label.Value = TxtValue.Text;
                    label.Comment = TxtComment.Text;
                    label.TextMode = o1.TextMode;
                    label.ResourceParams = TxtResourceParams.Text;
                    man.Insert(label);
                }
            }
            LabelsProvider.ClearCacheByResourceSet(o1.ResourceSet);
            loadGrid(Grid1);
            LblOk.Text = RenderSuccess(Utility.GetLabel("RECORD_SAVED_MSG"));
            MultiView1.ActiveViewIndex = VIEW_GRID_IDX;
            res = true;
        }
        catch (Exception e1)
        {
            LblErr.Text = RenderError(Utility.GetLabel("RECORD_ERR_MSG") + "<br />" + e1.ToString());
        }
        finally
        { }
        return res;
    }

    private void deleteRow(string resourceId)
    {
        LblOk.Text = RenderSuccess("");
        LblErr.Text = RenderError("");

        try
        {
            LabelsProvider.ClearCacheByResourceSet(DropModuleTypesFilter.SelectedValue);
            new LabelsManager().DeleteByResourceId(DropModuleTypesFilter.SelectedValue, resourceId);
        }
        catch (Exception e)
        {
            LblErr.Text = RenderError(e.Message);
        }
        loadGrid(Grid1);
    }

    private void loadDropTextMode()
    {
        try
        {
            //add all content type except .Module
            foreach (string item in Enum.GetNames(
                typeof(ContentEditorProvider.Configuration.EditorTypeEnum)))
            {
                int value = (int)Enum.Parse(
                    typeof(ContentEditorProvider.Configuration.EditorTypeEnum), item);
                DropTextMode.Items.Add(new ListItem(item, value.ToString()));
            }
        }
        catch (Exception ex)
        {
            LblErr.Text = RenderError(ex.ToString());
        }
    }

    private void loadDropMissingFilter()
    {
        var drop = DropMissingFilter;
        drop.Items.Clear();
        
        drop.Items.Add(
            new ListItem(
                base.GetLabel("FilterAllLabels", "All labels"),
                ((int)MissingFilterEnum.AllValues).ToString()
            )
        );

        drop.Items.Add(
            new ListItem(
                base.GetLabel("FilterMissingValues", "Only with missing values"),
                ((int)MissingFilterEnum.MissingValues).ToString()
            )
        );
    }

    private void loadDropsModuleTypes()
    {
        try
        {
            DropModuleTypesFilter.Items.Clear();
            if (!string.IsNullOrEmpty(this.ModuleFullName))
            {
                DropModuleTypesFilter.Items.Add(
                    new ListItem(this.ModuleFullName, this.ModuleFullName));
            }
            else if (!string.IsNullOrEmpty(this.ModuleFullNamePart))
            {
                //load resourceset from db
                var man = new LabelsManager();
                var list = man.GetResourceSetList(this.ModuleFullNamePart);

                DropModuleTypesFilter.Items.Add(
                    new ListItem("-- " + base.GetLabel("SelectReourceSet", "Select resource") + " --", FAKE_RESOURCESET)
                );

                if (this.AllowAdminMode)
                {
                    DropModuleTypesFilter.Items.Add(
                        new ListItem(base.GetLabel("AllForExport", "All for export"), "")
                    );
                }


                foreach (string item in list)
                {
                    DropModuleTypesFilter.Items.Add(new ListItem(item, item));
                }
            }
            else
            {
                //installed modules
                DropModuleTypesFilter.Items.Add(
                    new ListItem("-- " + Utility.GetLabel("LblSelectModule", "Select module") + " --", FAKE_RESOURCESET));

                if (this.AllowAdminMode)
                {
                    DropModuleTypesFilter.Items.Add(
                        new ListItem(base.GetLabel("AllForExport", "All for export"), "")
                    );
                }


                Dictionary<string, string> recordList = new ModuleTypeManager(true).GetList();
                foreach (var record1 in recordList)
                {
                    DropModuleTypesFilter.Items.Add(
                        new ListItem(record1.Key, record1.Value));
                }

                //installed controls
                recordList.Clear();
                DropModuleTypesFilter.Items.Add(
                    new ListItem("-- " + Utility.GetLabel("LblSelectControl", "Select control") + " --", FAKE_RESOURCESET));
                recordList = new ControlTypeManager().GetList();
                foreach (var record1 in recordList)
                {
                    DropModuleTypesFilter.Items.Add(
                        new ListItem(record1.Key, record1.Value));
                }
            }
        }
        catch (Exception ex)
        {
            LblErr.Text = RenderError(ex.ToString());
        }
    }

    /// <summary>
    /// load grid with data to import
    /// </summary>
    private void loadGridImportPreview()
    {
        var man = new UserTempDataManager(true);
        var filter = new UserTempDataFilter();
        filter.Username = PgnUserCurrent.UserName;
        filter.SessionId = Utility._SessionID();
        filter.IsExpired = Utility.TristateBool.False;
        var list = man.GetByFilter(filter, "");

        //LitNumOfRecords.Text = base.GetLabel("RecordsToImport", "Records to import") + ": ";
        int count = 0;
        foreach (var item in list)
        {
            if (item.Enabled)
                count++;
        }
        //LitNumOfRecords.Text += count.ToString();
        GridImportPreview.PageSize = 100;
        GridImportPreview.DataSource = list;
        GridImportPreview.DataBind();
        
        BtnApplyImport.Enabled = count > 0;
    }

    private void loadGrid(GridView grid)
    {
        var man = new PigeonCms.LabelsManager();
        var filter = new LabelTransFilter();

        if (!string.IsNullOrEmpty(this.ModuleFullName))
            filter.ResourceSet = this.ModuleFullName;
        else
            filter.ResourceSet = DropModuleTypesFilter.SelectedValue;

        var res = new List<ResLabelTrans>();
        var list = man.GetLabelTransByFilter(filter, "");


        var missingValuesFilter = (MissingFilterEnum)int.Parse(DropMissingFilter.SelectedValue);
        if (missingValuesFilter == MissingFilterEnum.MissingValues
            || TxtValuesStartsWithFilter.Text.Length > 0
            || TxtValuesContainsFilter.Text.Length > 0)
        {
            //filter list with missing values
            foreach (var item in list)
            {
                var labelList = new List<ResLabel>();
                var lblman = new LabelsManager();
                var lfilter = new LabelsFilter();
                lfilter.ResourceSet = item.ResourceSet;
                lfilter.ResourceId = item.ResourceId;
                labelList = lblman.GetByFilter(lfilter, "");

                //match vars init
                bool hasMissingValuesMatch = false;
                bool startsWithValueMatch = false;
                bool containsValueMatch = false;

                if (missingValuesFilter != MissingFilterEnum.MissingValues)
                    hasMissingValuesMatch = true;

                if (TxtValuesStartsWithFilter.Text.Length == 0)
                    startsWithValueMatch = true;

                if (TxtValuesContainsFilter.Text.Length == 0)
                    containsValueMatch = true;

                foreach (var culture in this.CultureList)
                {
                    ResLabel label = labelList.FirstOrDefault(
                        s => s.CultureName == culture.CultureCode);

                    if (label == null)
                        label = new ResLabel();

                    //missing values filter
                    if (!hasMissingValuesMatch && missingValuesFilter == MissingFilterEnum.MissingValues)
                    {
                        if (string.IsNullOrEmpty(label.Value))
                            hasMissingValuesMatch = true;
                    }

                    //startsWith value filter
                    if (!startsWithValueMatch && TxtValuesStartsWithFilter.Text.Length > 0)
                    {
                        if (label.Value.StartsWith(TxtValuesStartsWithFilter.Text, StringComparison.InvariantCultureIgnoreCase))
                            startsWithValueMatch = true;
                    }

                    //contains value filter
                    if (!containsValueMatch && TxtValuesContainsFilter.Text.Length > 0)
                    {
                        if (label.Value.IndexOf(TxtValuesContainsFilter.Text, 0, StringComparison.InvariantCultureIgnoreCase) != -1)
                            containsValueMatch = true;
                    }
                }

                //add to results
                if (hasMissingValuesMatch && startsWithValueMatch && containsValueMatch)
                    res.Add(item);
            }
        }
        else
        {
            //no additional filters
            res = list;
        }

        grid.DataSource = res;
        grid.DataBind();
    }

    private void getTransArea(string panelPrefix, Panel panel,
        Dictionary<string, string> translations,
        KeyValuePair<string, string> cultureItem)
    {
        var t1 = new Controls_ContentEditorControl();
        t1 = PigeonCms.Utility.FindControlRecursive<Controls_ContentEditorControl>(this, panelPrefix + cultureItem.Value);
        translations.Add(cultureItem.Key, t1.Text);
    }


    private string setTransArea(string panelPrefix, Panel panel,
        Dictionary<string, string> translations,
        KeyValuePair<string, string> cultureItem)
    {
        string res = "";
        Controls_ContentEditorControl t1 = new Controls_ContentEditorControl();
        t1 = Utility.FindControlRecursive<Controls_ContentEditorControl>(this, panelPrefix + cultureItem.Value);
        if (translations != null)
            translations.TryGetValue(cultureItem.Key, out res);
        if (t1 != null)
            t1.Text = res;

        return res;
    }

    private void addTransArea(string panelPrefix, Panel panel,
        ContentEditorProvider.Configuration editorConfig,
        KeyValuePair<string, string> cultureItem)
    {
        var txt = (Controls_ContentEditorControl)LoadControl("~/Controls/ContentEditorControl.ascx");
        txt.ID = panelPrefix + cultureItem.Value;
        txt.Configuration = editorConfig;

        LabelsProvider.SetLocalizedControlVisibility(false/*this.ShowOnlyDefaultCulture*/, cultureItem.Key, txt);
        panel.Controls.Add(txt);

        if (editorConfig.EditorType == ContentEditorProvider.Configuration.EditorTypeEnum.Image)
        {
            plhOnlyInImg.Visible = true;
            //add fileupload fake button
            var litUpload = new Literal();
            litUpload.Text = @"<input type='button'  value='Select file' "
            + " id='BtnUpload_" + panelPrefix + cultureItem.Value + "' class='btn btn-default btn-xs action-uploadalias'"
            + " onclick='document.getElementById(\"" + File1.ClientID + "_ctl02\").click(); var id = $(this).prevAll().eq(1).get(0).id; var box = \"#langBox\"; $(box).val(id); console.log(id)'>";
            panel.Controls.Add(litUpload);
        }
        else
        {
            plhOnlyInImg.Visible = false;
        }

        Literal lit = new Literal();
        lit.Text = "&nbsp;[<i>" + cultureItem.Value + "</i>]";
        lit.Text += "<br /><br />";
        panel.Controls.Add(lit);

    }

    private void initLangControls(ContentEditorProvider.Configuration.EditorTypeEnum editorType)
    {
        this.LastTextMode = editorType;

        var editorConfig = new ContentEditorProvider.Configuration();
        editorConfig.EditorType = editorType;
        editorConfig.FileButton = false;
        editorConfig.FilesUploadUrl = "";
        editorConfig.PageBreakButton = false;
        editorConfig.ReadMoreButton = false;
        ContentEditorProvider.InitEditor(this, Upd1, editorConfig);

        PanelValue.Controls.Clear();

        foreach (var item in this.CultureList)
        {
            addTransArea("TxtValue", PanelValue, editorConfig, item.ToKeyValuePay());
        }
    }

    private void setFlagUserTempData(int recordId, bool value, string flagName)
    {
        try
        {
            if (!PgnUserCurrent.IsAuthenticated)
                throw new Exception("user not authenticated");

            var o1 = new UserTempDataManager(true).GetByKey(recordId);
            switch (flagName.ToLower())
            {
                case "enabled":
                    o1.Enabled = value;
                    break;
                default:
                    break;
            }
            new UserTempDataManager(true).Update(o1);
        }
        catch (Exception e1)
        {
            LblErr.Text = Utility.GetLabel("RECORD_ERR_MSG") + "<br />" + e1.ToString();
        }
        finally { }
    }

    private string getResSet(string key)
    {
        string[] args = key.Split('|');
        string resSet = "";
        if (args != null && args.Length > 1)
        {
            resSet = args[0];
        }
        if (string.IsNullOrEmpty(resSet))
            resSet = "-1";
        return resSet;
    }

    private string getResId(string key)
    {
        string[] args = key.Split('|');
        string resId = "";
        if (args != null && args.Length > 1)
        {
            resId = args[1];
        }
        if (string.IsNullOrEmpty(resId))
            resId = "-1";
        return resId;
    }

    //tnx to http://www.siddharthrout.com/2014/07/20/creating-a-new-excel-file-and-adding-data-using-ace/
    private int exportToExcel(GridView ctrl, string filename)
    {
        if (ctrl.Rows.Count == 0)
            return 0;

        //create file
        var olecon = new OleDbConnection();
        var olecmd = new OleDbCommand();
        string filePath = new FilesGallery().TempPhisicalPath;
        filename += ".xlsx";
        string connstring = "Provider=Microsoft.ACE.OLEDB.12.0;" +
                            "Data Source=" + Path.Combine(filePath, filename) + ";" +
                            "Extended Properties=\"Excel 12.0 Xml;HDR=YES;\"";

        if (!Directory.Exists(filePath))
            Directory.CreateDirectory(filePath);

        if (File.Exists(Path.Combine(filePath, filename)))
            File.Delete(Path.Combine(filePath, filename));

        olecon.ConnectionString = connstring;
        olecon.Open();
        olecmd.Connection = olecon;

        //create table
        olecmd.CommandText = getCreateTableSql(ctrl);
        olecmd.ExecuteNonQuery();

        //insert rows
        for (int i = 0; i < ctrl.Rows.Count; i++)
        {
            GridViewRow row = ctrl.Rows[i];
            olecmd.CommandText = getInsertSqlFromRow(row, ctrl);
            olecmd.ExecuteNonQuery();
        }
        olecon.Close();


        //download file
        FileInfo file = new FileInfo(Path.Combine(filePath, filename));
        if (file.Exists)
        {
            Response.Clear();
            Response.ClearHeaders();
            Response.ClearContent();
            Response.AddHeader("content-disposition", "attachment; filename=" + filename);
            Response.AddHeader("Content-Type", "application/Excel");
            Response.ContentType = "application/vnd.xls";
            Response.AddHeader("Content-Length", file.Length.ToString());
            Response.WriteFile(file.FullName);
            Response.End();
        }

        return ctrl.Rows.Count;
    }

    private void deleteUserTempData()
    {
        var man = new UserTempDataManager(true);
        var filter = new UserTempDataFilter();
        filter.Username = PgnUserCurrent.UserName;
        filter.SessionId = Utility._SessionID();
        filter.IsExpired = Utility.TristateBool.NotSet;

        //remove user data in current section
        man.DeleteByFilter(filter);
    }

    private bool importPreviewFromExcel(string fileName, bool hasHeaders)
    {
        var res = false;
        string hdr = hasHeaders ? "Yes" : "No";
        string strConn;
        if (fileName.Substring(fileName.LastIndexOf('.')).ToLower() == ".xlsx")
            strConn = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + fileName + ";Extended Properties=\"Excel 12.0;HDR=" + hdr + ";IMEX=0\"";
        else
        {
            strConn = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + fileName + ";Extended Properties=\"Excel 8.0;HDR=" + hdr + ";IMEX=0\"";
        }

        DataSet output = new DataSet();
        using (OleDbConnection conn = new OleDbConnection(strConn))
        {
            conn.Open();
            DataTable schemaTable = conn.GetOleDbSchemaTable(
                OleDbSchemaGuid.Tables, new object[] { null, null, null, "TABLE" });

            //sheets
            int sheetIdx = 0;
            foreach (DataRow schemaRow in schemaTable.Rows)
            {
                string sheet = schemaRow["TABLE_NAME"].ToString();
                if (!sheet.EndsWith("_"))
                {
                    try
                    {
                        if (sheetIdx > 0) continue; //import only first shhet

                        var cmd = new OleDbCommand("SELECT * FROM [" + sheet + "]", conn);
                        cmd.CommandType = CommandType.Text;

                        DataTable outputTable = new DataTable(sheet);
                        output.Tables.Add(outputTable);
                        new OleDbDataAdapter(cmd).Fill(outputTable);



                        //insert imported data
                        deleteUserTempData();
                        var man = new UserTempDataManager(true);
                        foreach (DataRow row in outputTable.Rows)
                        {
                            var data = new UserTempData();
                            data.Enabled = false;
                            data.Username = PgnUserCurrent.UserName;
                            data.SessionId = Utility._SessionID();
                            data.DateExpiration = DateTime.Now.Date.Add(new TimeSpan(1/*day*/, 0, 0, 0));
                            for (int col = 0; col < row.ItemArray.Length; col++)
                            {
                                data.Columns[col] = row.ItemArray.GetValue(col).ToString();
                            }
                            man.Insert(data);
                        }
                        res = true;
                    }

                    catch (Exception ex)
                    {
                        throw new Exception(ex.Message + string.Format("Sheet:{0}.File:F{1}", sheet, fileName), ex);
                    }
                    sheetIdx++;
                }
            }
        }
        return res;
    }

    private void loadDropsImport()
    {
        loadDropColumns(DropColResourceSet, "0");
        loadDropColumns(DropColResourceId, "1");

        PanelPreviewValue0.Visible = false;
        PanelPreviewValue1.Visible = false;
        PanelPreviewValue2.Visible = false;
        PanelPreviewValue3.Visible = false;
        PanelPreviewValue4.Visible = false;

        for (int i = 0; i < this.CultureColumnsCount; i++)
        {
            var culture = this.CultureList[i];
            
            var panel = Utility.FindControlRecursive<HtmlControl>(ViewImport, "PanelPreviewValue" + i.ToString());
            var lit = Utility.FindControlRecursive<Literal>(ViewImport, "LitColValue" + i.ToString());
            var drop = Utility.FindControlRecursive<DropDownList>(ViewImport, "DropColValue" + i.ToString());

            panel.Visible = true;
            lit.Text = "Value " + culture.CultureCode + " column";
            loadDropColumns(drop, (i + 2).ToString());
        }

    }

    private void loadDropColumns(DropDownList drop, string selectedValue)
    {
        try
        {
            drop.Items.Clear();
            drop.Items.Add(new ListItem("--COL--", ""));

            drop.Items.Add(new ListItem("A", "0"));
            drop.Items.Add(new ListItem("B", "1"));
            drop.Items.Add(new ListItem("C", "2"));
            drop.Items.Add(new ListItem("D", "3"));
            drop.Items.Add(new ListItem("E", "4"));
            drop.Items.Add(new ListItem("F", "5"));
            drop.Items.Add(new ListItem("G", "6"));
            drop.Items.Add(new ListItem("H", "7"));
            drop.Items.Add(new ListItem("I", "8"));
            drop.Items.Add(new ListItem("J", "9"));
            drop.Items.Add(new ListItem("K", "10"));

            Utility.SetDropByValue(drop, selectedValue);
        }
        catch (Exception ex)
        {
            LblErr.Text = ex.ToString();
        }
    }

    private GridView prepareGridForExport(GridView ctrl)
    {
        ctrl.AllowPaging = false;
        ctrl.AllowSorting = false;
        loadGrid(ctrl);

        ctrl.Columns[COL_RESSET].HeaderText = "ResourceSet";
        ctrl.Columns[COL_RESID].HeaderText = "ResourceId";


        ctrl.Columns[COL_SELECT].Visible = false;
        ctrl.Columns[COL_RESSET].Visible = true;
        ctrl.Columns[COL_RESID].Visible = true;
        ctrl.Columns[COL_TEXTMODE].Visible = false;
        ctrl.Columns[COL_VALUES].Visible = false;
        ctrl.Columns[COL_DELETE].Visible = false;
        //values splitted per lang
        ctrl.Columns[COL_VALUE_1].Visible = true;
        ctrl.Columns[COL_VALUE_2].Visible = true;
        ctrl.Columns[COL_VALUE_3].Visible = true;
        ctrl.Columns[COL_VALUE_4].Visible = true;
        ctrl.Columns[COL_VALUE_5].Visible = true;

        return ctrl;
    }

    private string getCreateTableSql(GridView ctrl)
    {
        string res = "";
        string cols = "";
        foreach (DataControlField col in ctrl.Columns)
        {
            if (string.IsNullOrEmpty(col.HeaderText))
                col.Visible = false;

            if (!col.Visible)
                continue;

            cols += "[" + col.HeaderText + "] memo,";
        }
        if (cols.EndsWith(","))
            cols = cols.Substring(0, cols.Length - 1);

        res = "CREATE TABLE Sheet1(" + cols + ")";
        return res;
    }

    private string getInsertSqlFromRow(GridViewRow row, GridView ctrl)
    {
        string res = "";
        string cols = "";
        string values = "";
        int colIdx = 0;
        foreach (DataControlField col in ctrl.Columns)
        {
            if (!col.Visible)
            {
                colIdx++;
                continue;
            }

            cols += "[" + col.HeaderText + "],";

            var LitValue = FindFirstControlRecursive<Literal>(row.Cells[colIdx]);
            string value = row.Cells[colIdx].Text;
            if (LitValue != null)
                value = LitValue.Text;

            //issue on newline that becomes _x000d_ in excel
            if (value.Contains("\r\n"))
                value = value.Replace("\r\n", "");

            values += "'" + value.Replace("'", "''") + "',";

            colIdx++;
        }
        if (cols.EndsWith(","))
            cols = cols.Substring(0, cols.Length - 1);
        if (values.EndsWith(","))
            values = values.Substring(0, values.Length - 1);
        
        res = "INSERT INTO Sheet1("+ cols +") VALUES("+ values +")";
        return res;
    }

    private T FindFirstControlRecursive<T>(Control parentControl) where T : Control
    {
        T ctrl = default(T);

        if ((parentControl is T) /*&& (parentControl.ID == id)*/)
            return (T)parentControl;

        foreach (Control c in parentControl.Controls)
        {
            ctrl = FindFirstControlRecursive<T>(c);
            if (ctrl != null) break;
        }
        return ctrl;
    }

    private void selectAll(bool enabled)
    {
        LblErr.Text = "";
        LblOk.Text = "";

        try
        {
            var man = new UserTempDataManager(true);
            var filter = new UserTempDataFilter();
            filter.Username = PgnUserCurrent.UserName;
            filter.SessionId = Utility._SessionID();
            filter.IsExpired = Utility.TristateBool.False;
            var list = man.GetByFilter(filter, "");
            foreach (var item in list)
            {
                item.Enabled = enabled;
                man.Update(item);
            }
            loadGridImportPreview();
        }
        catch (Exception e1)
        {
            LblErr.Text = RenderError(e1.Message);
        }
        finally
        {
        }
    }

    private int importData()
    {
        int res = 0;
     
        int colResourceSet = 0;
        int colResourceId = 0;
        int colValue0 = 0;
        int colValue1 = 0;
        int colValue2 = 0;
        int colValue3 = 0;
        int colValue4 = 0;
        var category = new Category();

        int.TryParse(DropColResourceSet.SelectedValue, out colResourceSet);
        int.TryParse(DropColResourceId.SelectedValue, out colResourceId);
        int.TryParse(DropColValue0.SelectedValue, out colValue0);
        int.TryParse(DropColValue1.SelectedValue, out colValue1);
        int.TryParse(DropColValue2.SelectedValue, out colValue2);
        int.TryParse(DropColValue3.SelectedValue, out colValue3);
        int.TryParse(DropColValue4.SelectedValue, out colValue4);


        var man = new UserTempDataManager(true);
        var filter = new UserTempDataFilter();
        filter.Username = PgnUserCurrent.UserName;
        filter.SessionId = Utility._SessionID();
        filter.IsExpired = Utility.TristateBool.False;
        filter.Enabled = Utility.TristateBool.True;
        var list = man.GetByFilter(filter, "");
        foreach (var item in list)
        {
            //TODO insert labels

            //var o1 = new DroidCatalogue.DroidItem();
            //decimal price = 0m;
            //Decimal.TryParse(item.Columns[colItemPrice], out price);

            //o1.Enabled = true;
            //o1.TitleTranslations.Clear();
            //o1.DescriptionTranslations.Clear();
            //o1.CategoryId = category.Id;
            //o1.TitleTranslations.Add(Config.CultureDefault, item.Columns[colItemTitle]);
            //o1.DescriptionTranslations.Add(Config.CultureDefault, item.Columns[colItemDescription]);
            //o1.Sku = item.Columns[colItemSku];
            //o1.Price = price;

            //o1 = new DroidItemsManager(true, false).Insert(o1, false);
            res++;
        }
        
        return res;
    }
}
