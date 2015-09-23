using System;
using System.Data;
using System.Linq;
using System.Configuration;
using System.Collections;
using System.IO;
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
            if (!ScriptManager.GetCurrent(Page).IsInAsyncPostBack)
            {
                string eventArg = HttpContext.Current.Request["__EVENTARGUMENT"];
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
                    Grid1.DataBind();
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


        if (!Page.IsPostBack)
        {
            loadDropsModuleTypes();
            loadDropTextMode();

            BtnNew.Visible = this.AllowNew;
            DropTextMode.Enabled = this.AllowTextModeEdit;
            TxtComment.Enabled = this.AllowParamsEdit;
            TxtResourceParams.Enabled = this.AllowParamsEdit;
            Grid1.Columns[COL_DELETE].Visible = this.AllowDel;
        }
        else
        {
            //TOCHECK - remove?
            if (!ScriptManager.GetCurrent(Page).IsInAsyncPostBack)
            {
                string eventArg = HttpContext.Current.Request["__EVENTARGUMENT"];
                if (eventArg.Contains("edit__"))
                {
                    string args = eventArg.Replace("edit__", "");
                    editRow(getResSet(args), getResId(args));
                }
            }
        }
    }

    protected void DropModuleTypesFilter_SelectedIndexChanged(object sender, EventArgs e)
    {
        Grid1.DataBind();
    }

    protected void ObjDs1_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
    {
        var filter = new LabelTransFilter();

        if (!string.IsNullOrEmpty(this.ModuleFullName))
            filter.ResourceSet = this.ModuleFullName;
        else
            filter.ResourceSet = DropModuleTypesFilter.SelectedValue;

        e.InputParameters["filter"] = filter;
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

            Literal LitSel = (Literal)e.Row.FindControl("LitSel");
            LitSel.Text = "<a href='javascript:void(0)' onclick=\"editRow('edit__" + item.ResourceSet + "|" + item.ResourceId + "');\"><i class='fa fa-pgn_edit fa-fw'></i></a>";

            var LitTextMode = (Literal)e.Row.FindControl("LitTextMode");
            LitTextMode.Text = item.TextMode.ToString();

            Literal LitValue = (Literal)e.Row.FindControl("LitValue");
            string values = "";
            string defaultValue = "";

            var labelList = new List<ResLabel>();
            var man = new LabelsManager();
            var lfilter = new LabelsFilter();
            lfilter.ResourceSet = item.ResourceSet;
            lfilter.ResourceId = item.ResourceId;
            labelList = man.GetByFilter(lfilter, "");
            
            const string ROW = "<span class='[[rowClass]]'><i>[[rowCulture]]</i>: [[rowLabel]]</span><br>";
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
                    label.Value = "<i>NO VALUE</i>";
                    if (culture.Enabled)
                        rowClass = "text-danger";//missing
                    else
                        rowClass = "text-warning";//missing but cutlure not enabled
                }

                values += ROW
                    .Replace("[[rowClass]]", rowClass)
                    .Replace("[[rowCulture]]", culture.CultureCode)
                    .Replace("[[rowLabel]]", Utility.Html.GetTextPreview(label.Value, 50, ""));
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

    protected void BtnSave_Click(object sender, EventArgs e)
    {
        if (checkForm())
        {
            if (saveForm())
                MultiView1.ActiveViewIndex = 0;
        }
        Grid1.DataBind();

    }

    protected void BtnNew_Click(object sender, EventArgs e)
    {
        LblErr.Text = RenderError("");
        if (DropModuleTypesFilter.SelectedValue != "xxx")
            editRow(DropModuleTypesFilter.SelectedValue, "");
        else
            LblErr.Text = RenderError(
                base.GetLabel("SelectResource", "Please select a resource set"));

    }

    protected void BtnCancel_Click(object sender, EventArgs e)
    {
        LblErr.Text = RenderError("");
        LblOk.Text = RenderSuccess("");
        MultiView1.ActiveViewIndex = 0;

        Grid1.DataBind();
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
        if (MultiView1.ActiveViewIndex == 0)    //list view
        {
        }

        if (MultiView1.ActiveViewIndex == 1)    //edit view
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

        MultiView1.ActiveViewIndex = 1;
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
            Grid1.DataBind();
            LblOk.Text = RenderSuccess(Utility.GetLabel("RECORD_SAVED_MSG"));
            MultiView1.ActiveViewIndex = 0;
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
        Grid1.DataBind();
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
                    new ListItem("-- " + base.GetLabel("SelectReourceSet", "Select resource") + " --", "xxx")
                );
                foreach (string item in list)
                {
                    DropModuleTypesFilter.Items.Add(new ListItem(item, item));
                }
            }
            else
            {
                //installed modules
                DropModuleTypesFilter.Items.Add(
                    new ListItem("-- " + Utility.GetLabel("LblSelectModule", "Select module") + " --", "xxx"));
                Dictionary<string, string> recordList = new ModuleTypeManager(true).GetList();
                foreach (var record1 in recordList)
                {
                    DropModuleTypesFilter.Items.Add(
                        new ListItem(record1.Key, record1.Value));
                }

                //installed controls
                recordList.Clear();
                DropModuleTypesFilter.Items.Add(
                    new ListItem("-- " + Utility.GetLabel("LblSelectControl", "Select control") + " --", "xxx"));
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

}
