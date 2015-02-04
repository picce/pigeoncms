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

            TextBox txt1 = new TextBox();
            txt1.ID = "TxtTitle" + item.Value;
            txt1.MaxLength = 50;
            txt1.ToolTip = item.Key;
            txt1.CssClass = "form-control";
            pan1.Controls.Add(txt1);
            Literal lit1 = new Literal();
            lit1.Text = "<span class='input-group-addon'>" + item.Value + "</span>";
            pan1.Controls.Add(lit1);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            loadDropTemplateBlocks();
            loadDropPublishedFilter();
            loadDropsModuleTypes();
            MenuHelper.LoadListMenu(ListMenu, 0);

            RadioMenuAll.Attributes.Add("onclick", "disableListMenu();");
            RadioMenuNone.Attributes.Add("onclick", "disableListMenu();");
            RadioMenuSelection.Attributes.Add("onclick", "enableListMenu();");
        }
        else
        {
            //reload params on every postback, because cannot manage dinamically fields
            PigeonCms.Module currentModule = new PigeonCms.Module();
            if (base.CurrentId > 0)
            {
                currentModule = new ModulesManager().GetByKey(base.CurrentId);
                ModuleParams1.LoadParams(currentModule);
            }
            else
            {
                //manually set ModType
                try
                {
                    currentModule.ModuleNamespace = LitModuleType.Text.Split('.')[0];
                    currentModule.ModuleName = LitModuleType.Text.Split('.')[1];
                    currentModule.CurrView = DropViews.SelectedValue;
                    ModuleParams1.LoadParams(currentModule);
                }
                catch { }
            }
        }
    }

    protected void DropViews_SelectedIndexChanged(object sender, EventArgs e)
    {
        editRow(this.CurrentId, true, false);
    }

    protected void DropPublishedFilter_SelectedIndexChanged(object sender, EventArgs e)
    {
        Grid1.DataBind();
    }

    protected void DropTemplateBlockNameFilter_SelectedIndexChanged(object sender, EventArgs e)
    {
        Grid1.DataBind();
    }

    protected void DropModuleTypesFilter_SelectedIndexChanged(object sender, EventArgs e)
    {
        Grid1.DataBind();
    }

    protected void DropNewModule_SelectedIndexChanged(object sender, EventArgs e)
    {
        editRow(0);
    }

    protected void ObjDs1_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
    {
        ModulesFilter filter = new ModulesFilter();

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

        e.InputParameters["filter"] = filter;
    }

    protected void Grid1_RowCommand(object sender, GridViewCommandEventArgs e)
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
        if (e.CommandName == "ImgPublishedOk")
        {
            setFlag(Convert.ToInt32(e.CommandArgument), false, "published");
            Grid1.DataBind();
        }
        if (e.CommandName == "ImgPublishedKo")
        {
            setFlag(Convert.ToInt32(e.CommandArgument), true, "published");
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
            var item = new PigeonCms.Module();
            item = (Module)e.Row.DataItem;

            LinkButton LnkTitle = (LinkButton)e.Row.FindControl("LnkTitle");
            LnkTitle.Text = "<i class='fa fa-pgn_edit fa-fw'></i>" + Utility.Html.GetTextPreview(item.Title, 20, "");

            Literal LitModuleNameDesc = (Literal)e.Row.FindControl("LitModuleNameDesc");
            LitModuleNameDesc.Text = item.ModuleFullName;

            Literal LitMenuEntries = (Literal)e.Row.FindControl("LitMenuEntries");
            if (item.MenuSelection == ModulesMenuSelection.AllPages)
                LitMenuEntries.Text = Utility.GetLabel("LblAll", "All");
            else if (item.MenuSelection == ModulesMenuSelection.NoPages)
                LitMenuEntries.Text = Utility.GetLabel("LblNone", "None");
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

            Literal LitAccessTypeDesc = (Literal)e.Row.FindControl("LitAccessTypeDesc");
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
                LitAccessTypeDesc.Text += Utility.Html.GetTextPreview(roles, 30, "");

                //render access level
                if (!string.IsNullOrEmpty(item.ReadAccessCode))
                    LitAccessTypeDesc.Text += item.ReadAccessCode;
                if (item.ReadAccessLevel > 0)
                    LitAccessTypeDesc.Text += " (" + item.ReadAccessLevel.ToString() + ")";
            }

            //moduleContent
            var LnkModuleContent = (HyperLink)e.Row.FindControl("LnkModuleContent");
            if (!string.IsNullOrEmpty(item.EditContentUrl))
            {
                LnkModuleContent.Visible = true;
                LnkModuleContent.NavigateUrl = item.EditContentUrl;
            }

            //Published
            if (item.Published)
            {
                var img1 = e.Row.FindControl("ImgPublishedOk");
                img1.Visible = true;
            }
            else
            {
                var img1 = e.Row.FindControl("ImgPublishedKo");
                img1.Visible = true;
            }

            //Delete            
            if (item.IsCore)
            {
                var img1 = e.Row.FindControl("LnkDel");
                img1.Visible = false;
            }
            else
            {
                var img1 = e.Row.FindControl("LnkDel");
                img1.Visible = true;
            }
        }
    }

    protected void BtnSave_Click(object sender, EventArgs e)
    {
        LblErr.Text = RenderError("");
        LblOk.Text = RenderSuccess("");

        try
        {
            Module o1 = new Module();
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
            Grid1.DataBind();
            LblOk.Text = RenderSuccess( Utility.GetLabel("RECORD_SAVED_MSG"));
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
        LblErr.Text = RenderError("");
        LblOk.Text = RenderSuccess( "");
        MultiView1.ActiveViewIndex = 0;
    }

    protected void MultiView1_ActiveViewChanged(object sender, EventArgs e)
    {
        if (MultiView1.ActiveViewIndex == 0)    //list view
        {
            Utility.SetDropByValue(DropNewModule, "");  //select module
        }

        if (MultiView1.ActiveViewIndex == 1)    //edit view
        {
            DropViews.Enabled = false;
        }
    }

    #region private methods

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
        TxtContent.Text = "";
        ChkPublished.Checked = true;
        ChkShowTitle.Checked = true;
        LblCreated.Text = "";
        LblUpdated.Text = "";
        ChkIsCore.Checked = false;

        PermissionsControl1.ClearForm();

        foreach (ListItem item in ListMenu.Items)
        {
            if (item.Selected) item.Selected = false;
        }
    }

    private void form2obj(Module obj)
    {
        obj.Id = base.CurrentId;
        obj.Content = TxtContent.Text;
        obj.Published = ChkPublished.Checked;
        obj.ShowTitle = ChkShowTitle.Checked;
        obj.IsCore = ChkIsCore.Checked;

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
            LitModuleType.Text += "&nbsp;" + Utility.GetErrorLabel("NotInstalledModuleType", "Not installed module type");
        }
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
        ChkIsCore.Checked = obj.IsCore;

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
        LblOk.Text = RenderSuccess( "");
        LblErr.Text = RenderError("");

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
            ChkIsCore.Checked = false;
            //TxtName.Text += "-copy";
        }

        MultiView1.ActiveViewIndex = 1;
    }

    private void deleteRow(int recordId)
    {
        LblOk.Text = RenderSuccess( "");
        LblErr.Text = RenderError("");

        try
        {
            new ModulesManager().DeleteById(recordId);
        }
        catch (Exception e)
        {
            LblErr.Text = RenderError(e.Message);
        }
        Grid1.DataBind();
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
            LblErr.Text = RenderError(ex.ToString());
        }
    }

    private void setFlag(int recordId, bool value, string flagName)
    {
        try
        {
            Module o1 = new Module();
            o1 = new ModulesManager().GetByKey(recordId);
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
            LblErr.Text = RenderError(Utility.GetLabel("RECORD_ERR_MSG") + "<br />" + e1.ToString());
        }
        finally { }
    }

    protected void moveRecord(int recordId, Database.MoveRecordDirection direction)
    {
        LblErr.Text = RenderError("");
        LblOk.Text = RenderSuccess( "");

        try
        {
            new ModulesManager().MoveRecord(recordId, direction);
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
