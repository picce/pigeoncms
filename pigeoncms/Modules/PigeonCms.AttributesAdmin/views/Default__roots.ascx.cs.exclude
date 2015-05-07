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

public partial class Controls_AttributesAdmin : PigeonCms.BaseModuleControl
{
    const int COL_DELETE_IDX = 4;

    FormFieldTypeEnum CurrentFieldType
    {
        get
        {
            var res = FormFieldTypeEnum.Text;
            int fieldType = 0;
            if (int.TryParse(DropFieldType.SelectedValue, out fieldType))
                res = (FormFieldTypeEnum)fieldType;

            return res;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        LblOk.Text = "";
        LblErr.Text = "";

        if (!Page.IsPostBack)
        {
            loadDropFieldType();
        }
    }

    protected void Filter_TextChanged(object sender, EventArgs e)
    {
        try { Grid1.DataBind(); }
        catch (Exception ex)
        {
            LblErr.Text = ex.Message;
        }
    }

    protected void DropFieldType_SelectedIndexChanged(object sender, EventArgs e)
    {
        try 
        {
            switchControl(CurrentFieldType);
        }
        catch (Exception ex)
        {
            LblErr.Text = ex.Message;
        }
    }

    protected void ObjDs1_ObjectCreating(object sender, ObjectDataSourceEventArgs e)
    {
        //var typename = new CompaniesManager();
        //e.ObjectInstance = typename;
    }

    protected void ObjDs1_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
    {
        var filter = new FormFieldFilter();
        if (!string.IsNullOrEmpty(TxtNameFilter.Text))
            filter.NamePart = TxtNameFilter.Text;
        filter.Enabled = Utility.TristateBool.NotSet;
        e.InputParameters["filter"] = filter;
    }

    protected void ObjDsValues_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
    {
        var filter = new FormFieldOptionFilter();
        filter.FormFieldId = this.CurrentId>0 ? this.CurrentId : -1;
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

    protected void GridValues_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        //Ordering
        if (e.CommandName == "MoveDown")
        {
            moveRecord(int.Parse(e.CommandArgument.ToString()), Database.MoveRecordDirection.Down);
        }
        if (e.CommandName == "MoveUp")
        {
            moveRecord(int.Parse(e.CommandArgument.ToString()), Database.MoveRecordDirection.Up);
        }
        if (e.CommandName == "DeleteRow")
        {
            deleteValue(int.Parse(e.CommandArgument.ToString()));
        }
    }

    protected void Grid1_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
            Utility.AddGlyph(Grid1, e.Row);
    }

    protected void GridValues_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
            Utility.AddGlyph(Grid1, e.Row);
    }

    protected void Grid1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            var item = new FormField();
            item = (FormField)e.Row.DataItem;

            var LnkName = (LinkButton)e.Row.FindControl("LnkName");
            LnkName.Text = Utility.Html.GetTextPreview(item.Name, 50, "");
            if (string.IsNullOrEmpty(LnkName.Text))
                LnkName.Text = Utility.GetLabel("NO_VALUE", "<no value>");

            var LitFieldType = (Literal)e.Row.FindControl("LitFieldType");
            LitFieldType.Text = item.Type.ToString();

            //Published
            if (item.Enabled)
            {
                Image img1 = (Image)e.Row.FindControl("ImgEnabledOk");
                img1.Visible = true;
            }
            else
            {
                Image img1 = (Image)e.Row.FindControl("ImgEnabledKo");
                img1.Visible = true;
            }
        }
    }

    protected void GridValues_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        { }
    }

    protected void BtnNew_Click(object sender, EventArgs e)
    {
        editRow(0);
    }

    protected void BtnSave_Click(object sender, EventArgs e)
    {
        if (checkForm())
        {
            saveForm();
            MultiView1.ActiveViewIndex = 0;
        }
    }

    protected void BtnCancel_Click(object sender, EventArgs e)
    {
        MultiView1.ActiveViewIndex = 0;
    }

    protected void BtnAddValue_Click(object sender, EventArgs e)
    {
        bool res = true;

        if (string.IsNullOrEmpty(TxtValue.Text))
            res = false;

        if (res)
        {
            try
            {
                if (this.CurrentId == 0)
                {
                    if (checkForm())
                        base.CurrentId = saveForm();
                }

                var o1 = new FormFieldOption();
                var man = new FormFieldOptionsManager();
                if (base.CurrentId > 0)
                {
                    o1.FormFieldId = base.CurrentId;
                    o1.Label = TxtValue.Text;
                    o1.Value = TxtValue.Text;
                    o1 = man.Insert(o1);
                    TxtValue.Text = "";
                }
                GridValues.DataBind();
            }
            catch (Exception e1)
            {
                LblErr.Text = Utility.GetLabel("RECORD_ERR_MSG") + "<br />" + e1.ToString();
            }
            finally
            {
            }
        }
    }

    #region private methods

    private int saveForm()
    {
        int res = 0;
        LblErr.Text = "";
        LblOk.Text = "";

        try
        {
            var o1 = new FormField();
            var man = new FormFieldsManager();
            if (base.CurrentId == 0)
            {
                form2obj(o1);
                o1 = man.Insert(o1);
            }
            else
            {
                o1 = man.GetByKey(base.CurrentId);
                form2obj(o1);
                man.Update(o1);
            }
            Grid1.DataBind();
            LblOk.Text = Utility.GetLabel("RECORD_SAVED_MSG");
            res = o1.Id;
        }
        catch (Exception e1)
        {
            LblErr.Text = Utility.GetLabel("RECORD_ERR_MSG") + "<br />" + e1.ToString();
        }
        finally
        {
        }
        return res;
    }

    private void clearForm()
    {
        LblId.Text = "";
        LblCreated.Text = "";
        LblUpdated.Text = "";
        TxtName.Text = "";
        TxtMinValue.Text = "";
        TxtMaxValue.Text = "";
        ChkEnabled.Checked = true;

        TxtValue.Text = "";
    }

    private bool checkForm()
    {
        LblErr.Text = "";
        LblOk.Text = "";
        bool res = true;

        if (string.IsNullOrEmpty(TxtName.Text))
        {
            res = false;
            //LblErr.Text += "inserire la ragione sociale<br />";
        }

        if (res && base.CurrentId == 0)
        {
            var man = new FormFieldsManager();
            var filter = new FormFieldFilter();
            filter.Name = TxtName.Text;
            var list = man.GetByFilter(filter, "");
            if (list.Count > 0)
            {
                res = false;
            }
        }
        return res;
    }

    private void form2obj(FormField obj)
    {
        obj.Id = base.CurrentId;
        obj.Enabled = ChkEnabled.Checked;
        obj.Name = TxtName.Text;
        {
            int val = 0;
            int.TryParse(TxtMinValue.Text, out val);
            obj.MinValue = val;
        }
        {
            int val = 0;
            int.TryParse(TxtMaxValue.Text, out val);
            obj.MaxValue = val;
        }
        obj.Type = CurrentFieldType;
    }

    private void obj2form(FormField obj)
    {
        LblId.Text = obj.Id.ToString();

        TxtName.Text = obj.Name;
        ChkEnabled.Checked = obj.Enabled;
        if (obj.MinValue != 0)
            TxtMinValue.Text = obj.MinValue.ToString();
        if (obj.MaxValue != 0)
            TxtMaxValue.Text = obj.MaxValue.ToString();
        Utility.SetDropByValue(DropFieldType, ((int)obj.Type).ToString());

        switchControl(obj.Type);
        GridValues.DataBind();
    }

    private void editRow(int recordId)
    {
        LblOk.Text = "";
        LblErr.Text = "";

        clearForm();
        base.CurrentId = recordId;
        if (base.CurrentId > 0)
        {
            var obj = new FormField();
            obj = new FormFieldsManager().GetByKey(base.CurrentId);
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
            new PigeonCms.FormFieldsManager().DeleteById(recordId);
        }
        catch (Exception e)
        {
            LblErr.Text = e.Message;
        }
        Grid1.DataBind();
    }

    private void deleteValue(int recordId)
    {
        LblOk.Text = "";
        LblErr.Text = "";

        try
        {
            new FormFieldOptionsManager().DeleteById(recordId);
        }
        catch (Exception e)
        {
            LblErr.Text = e.Message;
        }
        GridValues.DataBind();
    }

    private void setFlag(int recordId, bool value, string flagName)
    {
        try
        {
            var man = new FormFieldsManager();
            var o1 = new FormField();
            o1 = man.GetByKey(recordId);
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
        catch (Exception e1)
        {
            LblErr.Text = Utility.GetLabel("RECORD_ERR_MSG") + "<br />" + e1.ToString();
        }
        finally { }
    }

    protected void moveRecord(int recordId, Database.MoveRecordDirection direction)
    {
        LblErr.Text = "";
        LblOk.Text = "";

        try
        {
            new FormFieldOptionsManager().MoveRecord(recordId, direction);
            GridValues.DataBind();
        }
        catch (Exception e1)
        {
            LblErr.Text = Utility.GetLabel("RECORD_ERR_MSG") + "<br />" + e1.ToString();
        }
        finally { }
    }

    private void lockControl(WebControl control, bool enabled)
    {
        control.Enabled = enabled;
        if (!enabled)
        {
            if (control.CssClass.LastIndexOf(" locked") == -1)
                control.CssClass += " locked";
        }
        else
        {
            if (control.CssClass.LastIndexOf(" locked") > -1)
                control.CssClass = control.CssClass.Replace(" locked", "");
        }
    }

    private void switchControl(FormFieldTypeEnum fieldType)
    {
        switch (fieldType)
        {
            case FormFieldTypeEnum.Text:
            case FormFieldTypeEnum.Html:
            case FormFieldTypeEnum.Numeric:
                lockControl(TxtMinValue, true);
                lockControl(TxtMaxValue, true);
                lockControl(TxtValue, false);
                lockControl(GridValues, false);
                lockControl(BtnAddValue, false);
                break;

            case FormFieldTypeEnum.List:
            case FormFieldTypeEnum.Combo:
            case FormFieldTypeEnum.Radio:
            case FormFieldTypeEnum.Check:
                lockControl(TxtMinValue, false);
                lockControl(TxtMaxValue, false);
                lockControl(TxtValue, true);
                lockControl(GridValues, true);
                lockControl(BtnAddValue, true);
                break;

            case FormFieldTypeEnum.Calendar:
            case FormFieldTypeEnum.Custom:
            case FormFieldTypeEnum.Spacer:
            case FormFieldTypeEnum.Hidden:
            case FormFieldTypeEnum.Error:
            default:
                break;
        }
    }

    private void loadDropFieldType()
    {
        try
        {
            DropFieldType.Items.Clear();
            //DropFieldType.Items.Add(new ListItem(base.GetLabel("SelectFieldType", "Field type"), ""));

            foreach (string item in Enum.GetNames(
                typeof(PigeonCms.FormFieldTypeEnum)))
            {
                if (item == "Error" || item == "Hidden" 
                    || item == "Hidden" || item == "Spacer"
                    || item == "Custom")
                    continue;

                int value = (int)Enum.Parse(typeof(PigeonCms.FormFieldTypeEnum), item);
                var listItem = new ListItem(item, value.ToString());
                DropFieldType.Items.Add(listItem);
            }
        }
        catch (Exception ex)
        {
            LblErr.Text = ex.ToString();
        }
    }

    #endregion
}
