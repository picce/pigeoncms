using System;
using System.Data;
using System.Linq;
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
using PigeonCms.Controls;


public partial class Controls_CategoriesAdmin : PigeonCms.Modules.CategoriesAdminControl
{

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
        BtnNew.Visible = this.AllowNew;
        PermissionsControl1.Visible = this.ShowSecurity;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        LblOk.Text = "";
        LblErr.Text = "";

        Tree1.NodeClick += new NodeClickDelegate(Tree_NodeClick);
        initTree();
        if (!Page.IsPostBack)
        {
            loadDropSectionsFilter(base.SectionId);
            Tree1.BindTree(this.CurrentSectionId);
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

        Tree1.SectionId = secID;
        Tree1.BindTree(secID);
        this.LastSelectedSectionId = secID;
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
            var o1 = new Category();
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
            Tree1.BindTree(this.CurrentSectionId);
            
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
        TxtExtId.Text = "";
        ChkEnabled.Checked = true;
        LitSection.Text = "";
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
        TxtExtId.Text = obj.ExtId;
        ChkEnabled.Checked = obj.Enabled;
        LitSection.Text = obj.Section.Title;
        loadListParentId();
        Utility.SetListBoxByValues(ListParentId, obj.ParentId);

        PermissionsControl1.Obj2form(obj);
    }

    private void editRow(int recordId)
    {
        LblOk.Text = "";
        LblErr.Text = "";

        clearForm();
        base.CurrentId = recordId;
        var obj = new Category();
        if (base.CurrentId > 0)
            obj = new CategoriesManager().GetByKey(base.CurrentId);

        obj2form(obj);
        MultiView1.ActiveViewIndex = 1;
    }

    private void deleteRow(int recordId)
    {
        if (!this.AllowDelete)
            return;

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
        Tree1.BindTree(this.CurrentSectionId);
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
            LblErr.Text = RenderError(Utility.GetLabel("RECORD_ERR_MSG") + "<br />" + e1.ToString());
        }
        finally { }
    }

    protected void moveRecord(int recordId, Database.MoveRecordDirection direction)
    {
        LblErr.Text = "";
        LblOk.Text = "";

        if (!this.AllowEdit)
            return;

        try
        {
            new CategoriesManager(true, true).MoveRecord(recordId, direction);
            Tree1.BindTree(this.CurrentSectionId);

            MultiView1.ActiveViewIndex = 0;
        }
        catch (Exception e1)
        {
            LblErr.Text = RenderError(Utility.GetLabel("RECORD_ERR_MSG") + "<br />" + e1.ToString());
        }
        finally { }
    }

    private void initTree()
    {
        Tree1.TargetImagesUpload = base.TargetImagesUpload;
        Tree1.TargetFilesUpload = base.TargetFilesUpload;
        Tree1.SectionId = base.SectionId;

        Tree1.ShowSecurity = base.ShowSecurity;
        Tree1.ShowOnlyDefaultCulture = base.ShowOnlyDefaultCulture;
        Tree1.ShowItemsCount = base.ShowItemsCount;
        Tree1.AllowOrdering = base.AllowOrdering;
        Tree1.AllowEdit = base.AllowEdit;
        Tree1.AllowDelete = base.AllowDelete;
        Tree1.AllowNew = base.AllowNew;
        Tree1.AllowSelection = base.AllowSelection;
    }

    #endregion

}
