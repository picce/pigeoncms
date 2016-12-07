using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using PigeonCms;
using System.Collections;
using System.Web;

public partial class Controls_ModulesAdmin : PigeonCms.BaseModuleControl
{
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
            txt1.MaxLength = 50;
            txt1.ToolTip = item.Key;
            txt1.CssClass = "form-control";
            pan1.Controls.Add(txt1);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        setSuccess("");
        setError("");

        if (!Page.IsPostBack)
        {
            loadDropTemplateBlocks();
            loadDropPublishedFilter();
            loadDropsModuleTypes();
            MenuHelper.LoadListMenu(ListMenu, 0);
            loadList();

            RadioMenuAll.Attributes.Add("onclick", "disableListMenu();");
            RadioMenuNone.Attributes.Add("onclick", "disableListMenu();");
            RadioMenuSelection.Attributes.Add("onclick", "enableListMenu();");
        }
        else
        {
            string eventArg = HttpContext.Current.Request["__EVENTARGUMENT"];
            if (eventArg == "items")
                loadList();

            //reload params on every postback, because cannot manage dinamically fields
            var currModule = new PigeonCms.Module();
            if (base.CurrentId > 0)
            {
                currModule = new ModulesManager().GetByKey(base.CurrentId);
                ModuleParams1.LoadParams(currModule);
            }
            else
            {
                //manually set ModType
                try
                {
                    currModule.ModuleNamespace = LitModuleType.Text.Split('.')[0];
                    currModule.ModuleName = LitModuleType.Text.Split('.')[1];
                    currModule.CurrView = DropViews.SelectedValue;
                    ModuleParams1.LoadParams(currModule);
                }
                catch { }
            }
        }
    }

    protected void Filter_Changed(object sender, EventArgs e)
    {
        loadList();
    }

    protected void DropNewModule_SelectedIndexChanged(object sender, EventArgs e)
    {
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

        var item = (PigeonCms.Module)e.Item.DataItem;

        var LitTitle = (Literal)e.Item.FindControl("LitTitle");
        LitTitle.Text = item.Name;
        if (string.IsNullOrEmpty(item.Name))
            LitTitle.Text = Utility.Html.GetTextPreview(item.Title, 30, "");


        var LitInfo = (Literal)e.Item.FindControl("LitInfo");
        if (item.IsCore)
            LitInfo.Text += "CORE";


        var LitBlock = (Literal)e.Item.FindControl("LitBlock");
        LitBlock.Text = item.TemplateBlockName;


        var LitMenuEntries = (Literal)e.Item.FindControl("LitMenuEntries");
        if (item.MenuSelection == ModulesMenuSelection.AllPages)
            LitMenuEntries.Text = GetLabel("PagesAll", "All pages");
        else if (item.MenuSelection == ModulesMenuSelection.NoPages)
            LitMenuEntries.Text = GetLabel("PagesNone", "No pages");
        else if (item.MenuSelection == ModulesMenuSelection.List)
        {
            string entries = "";
            foreach (string menuType in item.ModulesMenuTypes)
            {
                var menuType1 = new Menutype();
                menuType1 = new MenutypesManager().GetByMenuType(menuType);
                entries += "[" + menuType1.MenuType + "], ";  //record1.MenuType + "|" 
            }
            foreach (int menuId in item.ModulesMenu)
            {
                PigeonCms.Menu record1 = new PigeonCms.Menu();
                record1 = new MenuManager().GetByKey(menuId);
                entries += record1.Name + ", ";  //record1.MenuType + "|" 
            }
            if (entries.EndsWith(", ")) entries = entries.Substring(0, entries.Length - 2);
            LitMenuEntries.Text = Utility.Html.GetTextPreview(entries, 30, "");
        }


        var LitModuleNameDesc = (Literal)e.Item.FindControl("LitModuleNameDesc");
        LitModuleNameDesc.Text = item.ModuleFullName + "<br>"
            + "<i>" + item.CurrView + "</i>";

        //moduleContent
        var LnkModuleContent = (HyperLink)e.Item.FindControl("LnkModuleContent");
        if (!string.IsNullOrEmpty(item.EditContentUrl))
        {
            LnkModuleContent.Visible = true;
            LnkModuleContent.NavigateUrl = item.EditContentUrl;
        }

        //permissions
        var LitAccessTypeDesc = (Literal)e.Item.FindControl("LitAccessTypeDesc");
        LitAccessTypeDesc.Text = RenderAccessTypeSummary(item);


        //published
        var LitPublished = (Literal)e.Item.FindControl("LitPublished");
        string pubClass = "";
        if (item.Published)
            pubClass = "checked";
        LitPublished.Text = "<span class='table-modern--checkbox--square " + pubClass + "'></span>";

        var LnkPublished = (LinkButton)e.Item.FindControl("LnkPublished");
        LnkPublished.CssClass = "table-modern--checkbox " + pubClass;
        LnkPublished.CommandName = item.Published ? "Published0" : "Published1";
    }

    protected void Rep1_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "Select")
        {
            editRow(int.Parse(e.CommandArgument.ToString()));
        }
        if (e.CommandName == "CopyRow")
        {
            editRow(int.Parse(e.CommandArgument.ToString()), false, true);
        }
        if (e.CommandName == "DeleteRow")
        {
            deleteRow(int.Parse(e.CommandArgument.ToString()));
        }
        //Published
        if (e.CommandName == "Published0")
        {
            setFlag(Convert.ToInt32(e.CommandArgument), false, "published");
            loadList();
        }
        if (e.CommandName == "Published1")
        {
            setFlag(Convert.ToInt32(e.CommandArgument), true, "published");
            loadList();
        }
    }


    #region private methods

    private bool saveForm()
    {
        bool res = false;
        setError("");
        setSuccess("");

        try
        {
            var o1 = new Module();
            if (base.CurrentId == 0)
            {
                form2obj(o1);
                o1 = new ModulesManager().Insert(o1);
            }
            else
            {
                o1 = new ModulesManager().GetByKey(base.CurrentId);
                form2obj(o1);
                new ModulesManager().Update(o1);
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
        foreach (KeyValuePair<string, string> item in Config.CultureList)
        {
            TextBox t1 = new TextBox();
            t1 = (TextBox)PanelTitle.FindControl("TxtTitle" + item.Value);
            t1.Text = "";
        }

        LblId.Text = "";
        LitModuleType.Text = "";
        TxtName.Text = "";
        TxtContent.Text = "";
        ChkPublished.Checked = true;
        ChkShowTitle.Checked = true;
        LblCreated.Text = "";
        LblUpdated.Text = "";

        PermissionsControl1.ClearForm();

        foreach (ListItem item in ListMenu.Items)
        {
            if (item.Selected) item.Selected = false;
        }
    }

    private void form2obj(Module obj)
    {
        obj.Id = base.CurrentId;
        obj.Name = TxtName.Text;
        obj.Content = TxtContent.Text;
        obj.Published = ChkPublished.Checked;
        obj.ShowTitle = ChkShowTitle.Checked;

        obj.TemplateBlockName = DropTemplateBlockName.SelectedValue;
        obj.MenuSelection = ModulesMenuSelection.NoPages;    //default
        obj.ModulesMenu.Clear();
        obj.ModulesMenuTypes.Clear();
        if (base.CurrentId == 0)
        {
            obj.ModuleNamespace = LitModuleType.Text.Split('.')[0];
            obj.ModuleName = LitModuleType.Text.Split('.')[1];
        }

        if (RadioMenuAll.Checked)
            obj.MenuSelection = ModulesMenuSelection.AllPages;
        else if (RadioMenuNone.Checked)
            obj.MenuSelection = ModulesMenuSelection.NoPages;
        else
        {
            obj.MenuSelection = ModulesMenuSelection.List;
            foreach (ListItem item in ListMenu.Items)
            {
                if (item.Selected)
                {
                    int menuId = 0;
                    if (int.TryParse(item.Value, out menuId))
                        obj.ModulesMenu.Add(menuId);
                    else
                        obj.ModulesMenuTypes.Add(item.Value);
                }
            }
        }

        obj.TitleTranslations.Clear();
        foreach (KeyValuePair<string, string> item in Config.CultureList)
        {
            TextBox t1 = new TextBox();
            t1 = (TextBox)PanelTitle.FindControl("TxtTitle" + item.Value);
            obj.TitleTranslations.Add(item.Key, t1.Text);
        }

        int ordering = 0;
        int.TryParse(DropOrdering.SelectedValue, out ordering);
        obj.Ordering = ordering;

        obj.CurrView = DropViews.SelectedValue;
        obj.CssFile = ModuleParams1.CssFile;
        obj.CssClass = ModuleParams1.CssClass;
        obj.UseCache = ModuleParams1.UseCache;
        obj.UseLog = ModuleParams1.UseLog;
        obj.DirectEditMode = ModuleParams1.DirectEditMode;
        obj.SystemMessagesTo = ModuleParams1.SystemMessagesTo;

        obj.ModuleParams = FormBuilder.GetParamsString(obj.ModuleType.Params, ModuleParams1);
        if (!string.IsNullOrEmpty(obj.CurrView))
        {
            //loads current view specific params
            PigeonCms.ModuleType viewType = null;
            viewType = new ModuleTypeManager().GetByFullName(
                obj.ModuleFullName, obj.CurrView.Replace(".ascx", ".xml"));
            if (viewType != null)
                obj.ModuleParams += "|" + FormBuilder.GetParamsString(viewType.Params, ModuleParams1);
        }

        PermissionsControl1.Form2obj(obj);
    }

    private void obj2form(Module obj, bool changeView)
    {
        foreach (KeyValuePair<string, string> item in Config.CultureList)
        {
            string sTitleTranslation = "";
            TextBox t1 = new TextBox();
            t1 = (TextBox)PanelTitle.FindControl("TxtTitle" + item.Value);
            obj.TitleTranslations.TryGetValue(item.Key, out sTitleTranslation);
            t1.Text = sTitleTranslation;
        }

        LblId.Text = obj.Id.ToString();
        LitModuleType.Text = obj.ModuleFullName;
        if (!new ModuleTypeManager().Exist(obj.ModuleFullName))
        {
            setError(Utility.GetErrorLabel("NotInstalledModuleType", "Not installed module type"));
        }
        TxtName.Text = obj.Name;
        TxtContent.Text = obj.Content;
        ChkPublished.Checked = obj.Published;
        ChkShowTitle.Checked = obj.ShowTitle;
        if (!string.IsNullOrEmpty(obj.TemplateBlockName))
            Utility.SetDropByValue(DropTemplateBlockName, obj.TemplateBlockName);
        else
        {
            //set default templateBlockName for specific module (from module install.xml)
            ModuleType modType = obj.ModuleType;
            if (!string.IsNullOrEmpty(modType.TemplateBlockName))
                Utility.SetDropByValue(DropTemplateBlockName, modType.TemplateBlockName);
        }

        loadDropOrdering();
        Utility.SetDropByValue(DropOrdering, obj.Ordering.ToString());

        if (changeView)
            obj.CurrView = DropViews.SelectedValue;
        else
        {
            loadDropViews(obj);
            Utility.SetDropByValue(DropViews, obj.CurrView);
        }

        ModuleParams1.LoadParams(obj);
        PermissionsControl1.Obj2form(obj);

        LblCreated.Text = obj.DateInserted + " " + obj.UserInserted;
        LblUpdated.Text = obj.DateUpdated + " " + obj.UserUpdated;

        //choose selected menu entries
        RadioMenuAll.Checked = false;
        RadioMenuNone.Checked = false;
        RadioMenuSelection.Checked = false;
        if (obj.MenuSelection == ModulesMenuSelection.AllPages)
        {
            Utility.Script.RegisterStartupScript(this, ID, "disableListMenu();");
            //ScriptManager.RegisterStartupScript(this, GetType(), ID, "disableListMenu();", true);
            RadioMenuAll.Checked = true;
        }
        else if (obj.MenuSelection == ModulesMenuSelection.NoPages)
        {
            Utility.Script.RegisterStartupScript(this, ID, "disableListMenu();");
            //ScriptManager.RegisterStartupScript(this, GetType(), ID, "disableListMenu();", true);
            RadioMenuNone.Checked = true;
        }
        else
        {
            Utility.Script.RegisterStartupScript(this, ID, "enableListMenu();");
            //ScriptManager.RegisterStartupScript(this, GetType(), ID, "enableListMenu();", true);
            RadioMenuSelection.Checked = true;
            Utility.SetListBoxByValues(ListMenu, obj.ModulesMenu);
            Utility.SetListBoxByValues(ListMenu, obj.ModulesMenuTypes, false);
        }
    }

    private void editRow(int recordId)
    {
        editRow(recordId, false, false);
    }

    private void editRow(int recordId, bool changeView, bool copyRow)
    {
        var obj = new Module();
        setSuccess("");
        setError("");

        clearForm();
        base.CurrentId = recordId;

        if (base.CurrentId == 0)
        {
            obj.ModuleNamespace = DropNewModule.SelectedValue.Split('.')[0];
            obj.ModuleName = DropNewModule.SelectedValue.Split('.')[1];
            obj2form(obj, changeView);      //loads new module params fields
            LitModuleType.Text = DropNewModule.SelectedValue;
        }
        else
        {
            obj = new ModulesManager().GetByKey(base.CurrentId);
            obj2form(obj, changeView);
        }
        if (copyRow)
        {
            base.CurrentId = 0;
            LblId.Text = "0";
            TxtName.Text += "-copy";
        }

        showInsertPanel(true);
    }

    private void deleteRow(int recordId)
    {
        setSuccess("");
        setError("");

        try
        {
            var man = new ModulesManager();
            var item = man.GetByKey(recordId);

            if (item.IsCore)
                throw new InvalidOperationException("Cannot delete core modules");

            man.DeleteById(recordId);
        }
        catch (Exception e)
        {
            setError(e.Message);
        }
        loadList();
    }

    private void loadList()
    {
        var man = new ModulesManager(true, true);
        var filter = new ModulesFilter();

        filter.IsContent = Utility.TristateBool.False;  //show only not contents modules
        switch (DropPublishedFilter.SelectedValue)
        {
            case "1":
                filter.Published = Utility.TristateBool.True;
                break;
            case "0":
                filter.Published = Utility.TristateBool.False;
                break;
            default:
                filter.Published = Utility.TristateBool.NotSet;
                break;
        }
        if (DropTemplateBlockNameFilter.SelectedValue != "")
            filter.TemplateBlockName = DropTemplateBlockNameFilter.SelectedValue;

        if (DropModuleTypesFilter.SelectedValue != "")
        {
            filter.ModuleNamespace = DropModuleTypesFilter.SelectedValue.Split('.')[0];
            filter.ModuleName = DropModuleTypesFilter.SelectedValue.Split('.')[1];
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
            var pages = new ArrayList();
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

    private void loadDropPublishedFilter()
    {
        DropPublishedFilter.Items.Clear();
        DropPublishedFilter.Items.Add(new ListItem(Utility.GetLabel("LblSelectState", "Select state"), ""));
        DropPublishedFilter.Items.Add(new ListItem("On-line", "1"));
        DropPublishedFilter.Items.Add(new ListItem("Off-line", "0"));
    }

    private void loadDropTemplateBlocks()
    {
        DropTemplateBlockName.Items.Clear();
        DropTemplateBlockNameFilter.Items.Clear();
        DropTemplateBlockNameFilter.Items.Add(new ListItem(Utility.GetLabel("LblSelectPosition", "Select position"), ""));

        TemplateBlockFilter filter = new TemplateBlockFilter();
        filter.Enabled = Utility.TristateBool.NotSet;
        List<TemplateBlock> recordList = new TemplateBlocksManager().GetByFilter(filter, "");
        foreach (TemplateBlock record1 in recordList)
        {
            DropTemplateBlockName.Items.Add(
                new ListItem(record1.Name, record1.Name));
            DropTemplateBlockNameFilter.Items.Add(
                new ListItem(record1.Name, record1.Name));
        }
    }

    private void loadDropOrdering()
    {
        string templateBlockName = "";
        DropOrdering.Items.Clear();

        if (base.CurrentId > 0)
        {
            templateBlockName = new ModulesManager().GetByKey(base.CurrentId).TemplateBlockName;
        }

        ModulesFilter filter = new ModulesFilter();
        filter.TemplateBlockName = templateBlockName;
        List<Module> recordList = new ModulesManager().GetByFilter(filter, "Ordering");
        int ordering = 1;
        foreach (Module record1 in recordList)
        {
            DropOrdering.Items.Add(
                new ListItem(ordering.ToString() + ": " + record1.Title,
                    ordering.ToString()));
            ordering++;
        }
    }

    private void loadDropViews(Module currentModule)
    {
        DropViews.Items.Clear();
        foreach (string item in currentModule.ModuleType.Views)
        {
            ListItem listItem = new ListItem(item, item);
            DropViews.Items.Add(listItem);
        }
    }

    private void loadDropsModuleTypes()
    {
        try
        {
            DropNewModule.Items.Clear();
            DropNewModule.Items.Add(new ListItem(
                PigeonCms.Utility.GetLabel("LblCreateNew", "Create new"), ""));

            DropModuleTypesFilter.Items.Clear();
            DropModuleTypesFilter.Items.Add(new ListItem(Utility.GetLabel("LblSelectModule", "Select module"), ""));

            ModuleTypeFilter filter = new ModuleTypeFilter();
            List<ModuleType> recordList = new ModuleTypeManager(true).GetByFilter(filter, "FullName");
            foreach (ModuleType record1 in recordList)
            {
                DropNewModule.Items.Add(
                    new ListItem(record1.FullName, record1.FullName));

                DropModuleTypesFilter.Items.Add(
                    new ListItem(record1.FullName, record1.FullName));
            }
        }
        catch (Exception ex)
        {
            setError(ex.ToString());
        }
    }

    private void setFlag(int recordId, bool value, string flagName)
    {
        try
        {
            var o1 = new ModulesManager().GetByKey(recordId);
            switch (flagName.ToLower())
            {
                case "published":
                    o1.Published = value;
                    break;
                default:
                    break;
            }
            new ModulesManager().Update(o1);
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
        {
            PanelInsert.Visible = true;
            DropViews.Enabled = false;
        }
        else
        {
            PanelInsert.Visible = false;
            Utility.SetDropByValue(DropNewModule, "");  //select module
        }
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
