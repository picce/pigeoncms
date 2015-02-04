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

public partial class Controls_Default : PigeonCms.BaseModuleControl
{

    protected string ModuleFullName
    {
        get { return base.GetStringParam("ModuleFullName", "", "ModuleFullName"); }
    }

    protected string ModuleFullNamePart
    {
        get { return base.GetStringParam("ModuleFullNamePart", ""); }
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
        LblOk.Text = RenderSuccess( "");
        LblErr.Text = RenderError( "");

        if (!Page.IsPostBack)
        {
            loadDropsModuleTypes();
            loadDropTextMode();
        }
        else
        {
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

            //LinkButton LnkSel = (LinkButton)e.Row.FindControl("LnkSel");
            //LnkSel.Text = "<i class='fa fa-pgn_edit fa-fw'></i>";
            //if (item.TextMode != this.LastTextMode) { }
            //LnkSel.OnClientClick = "editRow('edit__"+ item.ResourceId +"'); return;";
            //ScriptManager.GetCurrent(this.Page).RegisterPostBackControl(LnkSel);//cause full postback for init tinymce well

            Literal LitSel = (Literal)e.Row.FindControl("LitSel");
            LitSel.Text = "<a href='javascript:void(0)' onclick=\"editRow('edit__" + item.ResourceSet + "|" + item.ResourceId + "');\"><i class='fa fa-pgn_edit fa-fw'></i></a>";

            var LitTextMode = (Literal)e.Row.FindControl("LitTextMode");
            LitTextMode.Text = item.TextMode.ToString();

            Literal LitValue = (Literal)e.Row.FindControl("LitValue");
            string values = "";

            var labelList = new List<ResLabel>();
            var man = new LabelsManager();
            var lfilter = new LabelsFilter();
            lfilter.ResourceSet = item.ResourceSet;
            lfilter.ResourceId = item.ResourceId;
            labelList = man.GetByFilter(lfilter, "");
            foreach(var label in labelList)
            {
                values += label.Value + ", ";                
            }
            if (values.EndsWith(", ")) values = values.Substring(0, values.Length - 2);
            LitValue.Text += Utility.Html.GetTextPreview(values, 100, "");
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
        LblErr.Text = RenderError( "");
        if (DropModuleTypesFilter.SelectedValue != "xxx")
            editRow(DropModuleTypesFilter.SelectedValue, "");
        else
            LblErr.Text = RenderError(
                base.GetLabel("SelectResource", "Please select a resource set"));

    }

    protected void BtnCancel_Click(object sender, EventArgs e)
    {
        LblErr.Text = RenderError( "");
        LblOk.Text = RenderSuccess( "");
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

    
    private void clearForm()
    {
        LitResourceSet.Text = "";
        TxtResourceId.Text = "";

        var panelCommentUI = new LabelsProvider.UI(false, PanelComment);

        foreach (KeyValuePair<string, string> item in Config.CultureList)
        {
            setTransArea("TxtValue", PanelValue, null, item);
            panelCommentUI.SetTransText("TxtComment", null, item);
        }
        Utility.SetDropByValue(DropTextMode, "2");
    }

    private void form2obj(ResLabelTrans obj)
    {        
        obj.ResourceSet = LitResourceSet.Text;
        obj.ResourceId = TxtResourceId.Text;

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
        Utility.SetDropByValue(DropTextMode, ((int)obj.TextMode).ToString());


        filter.ResourceSet = obj.ResourceSet;
        filter.ResourceId = obj.ResourceId;

        if (!string.IsNullOrEmpty(obj.ResourceId))
        {
            TxtResourceId.Enabled = false;
            foreach (KeyValuePair<string, string> item in Config.CultureList)
            {
                label = new ResLabel();
                var TxtValue = new Controls_ContentEditorControl();
                var TxtComment = new TextBox();
                TxtValue = (Controls_ContentEditorControl)PanelValue.FindControl("TxtValue" + item.Value);
                TxtComment = (TextBox)PanelComment.FindControl("TxtComment" + item.Value);

                filter.CultureName = item.Key;
                labelsList = lman.GetByFilter(filter, "");
                if (labelsList.Count > 0)
                    label = labelsList[0];
                TxtValue.Text = label.Value;
                TxtComment.Text = label.Comment;
            }
        }
    }

    private void editRow(string resourceSet, string resourceId)
    {
        var obj = new PigeonCms.ResLabelTrans();
        LblOk.Text = RenderSuccess( "");
        LblErr.Text = RenderError( "");

        clearForm();
        CurrentKey = resourceSet + "|" + resourceId;
        if (string.IsNullOrEmpty(resourceId))
        {
            //initLangControls(obj.TextMode);
            obj.ResourceSet = resourceSet;
            obj.ResourceId = resourceId;
            obj2form(obj);
        }
        else
        {
            obj = new LabelsManager().GetLabelTransByKey(resourceSet, resourceId);
            //initLangControls(obj.TextMode);
            obj2form(obj);
        }

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
        LblErr.Text = RenderError( "");
        LblOk.Text = RenderSuccess( "");

        try
        {
            var o1 = new ResLabelTrans();
            o1 = man.GetLabelTransByKey(getResSet(CurrentKey), getResId(CurrentKey));
            form2obj(o1);            
            foreach (KeyValuePair<string, string> item in Config.CultureList)
            {
                var label = new ResLabel();
                var labelList = new List<ResLabel>();

                var TxtValue = new Controls_ContentEditorControl();
                TxtValue = (Controls_ContentEditorControl)PanelValue.FindControl("TxtValue" + item.Value);
                TextBox TxtComment = new TextBox();
                TxtComment = (TextBox)PanelComment.FindControl("TxtComment" + item.Value);

                //man.DeleteByResourceId(o1.ResourceSet, o1.ResourceId, item.Key);

                lFilter.ResourceSet = o1.ResourceSet;
                lFilter.ResourceId = o1.ResourceId;
                lFilter.CultureName = item.Key;
                labelList = man.GetByFilter(lFilter, "");
                if (labelList.Count > 0)
                {
                    label = labelList[0];
                    label.Value = TxtValue.Text;
                    label.Comment = TxtComment.Text;
                    label.TextMode = o1.TextMode;
                    man.Update(label);
                }
                else
                {
                    label.ResourceSet = o1.ResourceSet;
                    label.ResourceId = o1.ResourceId;
                    label.CultureName = item.Key;
                    label.Value = TxtValue.Text;
                    label.Comment = TxtComment.Text;
                    label.TextMode = o1.TextMode;
                    man.Insert(label);
                }
            }
            LabelsProvider.ClearCacheByResourceSet(o1.ResourceSet);
            Grid1.DataBind();
            LblOk.Text = RenderSuccess( Utility.GetLabel("RECORD_SAVED_MSG"));
            MultiView1.ActiveViewIndex = 0;
            res = true;
        }
        catch (Exception e1)
        {
            LblErr.Text = RenderError( Utility.GetLabel("RECORD_ERR_MSG") + "<br />" + e1.ToString());
        }
        finally
        { }
        return res;
    }

    private void deleteRow(string resourceId)
    {
        LblOk.Text = RenderSuccess( "");
        LblErr.Text = RenderError( "");

        try
        {
            new LabelsManager().DeleteByResourceId(DropModuleTypesFilter.SelectedValue, resourceId);
        }
        catch (Exception e)
        {
            LblErr.Text = RenderError( e.Message);
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
            LblErr.Text = RenderError( ex.ToString());
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
            LblErr.Text = RenderError( ex.ToString());
        }
    }

    private void getTransArea(string panelPrefix, Panel panel,
        Dictionary<string, string> translations,
        KeyValuePair<string, string> cultureItem)
    {
        var t1 = new Controls_ContentEditorControl();
        t1 = Utility.FindControlRecursive<Controls_ContentEditorControl>(this, panelPrefix + cultureItem.Value);
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

        Literal lit = new Literal();
        //if (!this.ShowOnlyDefaultCulture)
        lit.Text = "&nbsp;[<i>" + cultureItem.Value + "</i>]<br /><br />";
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
        PanelComment.Controls.Clear();

        var panelCommentUI = new LabelsProvider.UI(false, PanelComment);
        foreach (KeyValuePair<string, string> item in Config.CultureList)
        {
            addTransArea("TxtValue", PanelValue, editorConfig, item);
            panelCommentUI.AddTransText("TxtComment", item, 0, "form-control");
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
