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
    const int COL_ORDERING_INDEX = 3;
    const int COL_ORDER_ARROWS_INDEX = 4;
    const int COL_ACCESS_INDEX = 6;
    const int COL_FILES_INDEX = 7;
    const int COL_IMAGES_INDEX = 8;
    const int COL_ID_INDEX = 10;

    protected new void Page_Init(object sender, EventArgs e)
    {
        base.Page_Init(sender, e);

        Grid1.Columns[3].AccessibleHeaderText = base.GetLabel("LblCustomVlaue", "Valore Custom", null, true);
        Grid1.Columns[4].AccessibleHeaderText = base.GetLabel("LblMeasureUnit", "Unità di misura", null, true);

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

        }

    }

    protected void Page_Load(object sender, EventArgs e)
    {
        LblOk.Text = "";
        LblErr.Text = "";

    }

    protected void DropEnabledFilter_SelectedIndexChanged(object sender, EventArgs e)
    {
        Grid1.DataBind();
    }

    protected void ObjDs1_ObjectCreating(object sender, ObjectDataSourceEventArgs e)
    {
        var typename = new PigeonCms.AttributesManager();
        e.ObjectInstance = typename;
    }

    protected void ObjDs1_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
    {
        PigeonCms.AttributeFilter filter = new PigeonCms.AttributeFilter();

        //filter.Enabled = Utility.TristateBool.NotSet;
        //switch (DropEnabledFilter.SelectedValue)
        //{
        //    case "1":
        //        filter.Enabled = Utility.TristateBool.True;
        //        break;
        //    case "0":
        //        filter.Enabled = Utility.TristateBool.False;
        //        break;
        //    default:
        //        filter.Enabled = Utility.TristateBool.NotSet;
        //        break;
        //}
        //if (this.SectionId > 0)
        //    filter.SectionId = this.SectionId;
        //else
        //{
        //    int secId = -1;
        //    int.TryParse(DropSectionsFilter.SelectedValue, out secId);
        //    filter.SectionId = secId;
        //}

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
        if (e.CommandName == "EditValues")
        {
            editValues(int.Parse(e.CommandArgument.ToString()));
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
            PigeonCms.Attribute item = new PigeonCms.Attribute();
            item = (PigeonCms.Attribute)e.Row.DataItem;

            LinkButton LnkTitle = (LinkButton)e.Row.FindControl("LnkTitle");
            LnkTitle.Text = "<i class='fa fa-pgn_edit fa-fw'></i>";
            LnkTitle.Text += Utility.Html.GetTextPreview(item.Name, 50, "");
            if (string.IsNullOrEmpty(LnkTitle.Text))
                LnkTitle.Text += Utility.GetLabel("NO_VALUE", "<no value>");

            Literal LnkItemType = (Literal)e.Row.FindControl("LnkItemType");
            LnkItemType.Text += Utility.Html.GetTextPreview(item.ItemType, 50, "");
            if (string.IsNullOrEmpty(LnkItemType.Text))
                LnkItemType.Text += Utility.GetLabel("NO_VALUE", "<no value>");

            Literal LnkCustomValue = (Literal)e.Row.FindControl("LnkCustomValue");
            LnkCustomValue.Text += item.AllowCustomValue.ToString();
            if (string.IsNullOrEmpty(LnkCustomValue.Text))
                LnkCustomValue.Text += Utility.GetLabel("NO_VALUE", "<no value>");

            Literal LnkMeasureUnit = (Literal)e.Row.FindControl("LnkMeasureUnit");
            LnkMeasureUnit.Text += item.MeasureUnit;
            if (string.IsNullOrEmpty(LnkMeasureUnit.Text))
                LnkMeasureUnit.Text += Utility.GetLabel("NO_VALUE", "<no value>");

        }
    }

    protected void BtnNew_Click(object sender, EventArgs e)
    {
        editRow(0);
    }

    protected void BtnSave_Click(object sender, EventArgs e)
    {
        LblErr.Text = "";
        LblOk.Text = "";

        try
        {
            PigeonCms.Attribute o1 = new PigeonCms.Attribute();
            if (base.CurrentId == 0)
            {
                form2obj(o1);
                o1 = new AttributesManager().Insert(o1);
            }
            else
            {
                o1 = new AttributesManager().GetByKey(base.CurrentId);  //precarico i campi esistenti e nn gestiti dal form
                form2obj(o1);
                new AttributesManager().Update(o1);
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

    protected void BtnSaveValues_Click(object sender, EventArgs e)
    {
        LblErr.Text = "";
        LblOk.Text = "";

        try
        {
            PigeonCms.AttributeValue o1 = new PigeonCms.AttributeValue();
            if (base.CurrentId == 0)
            {
                //values2obj(o1);
                o1 = new AttributeValuesManager().Insert(o1);
            }
            else
            {
                //o1 = new AttributeValuesManager().GetByKey(base.CurrentId);  //precarico i campi esistenti e nn gestiti dal form
                //form2obj(o1);
                //new AttributesManager().Update(o1);
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
        TxtName.Text = "";
        TxtMeasureUnit.Text = "";
        DropItemType.SelectedValue = null;
        ChkCustomValue.Checked = false;
    }

    private void form2obj(PigeonCms.Attribute obj)
    {
        obj.Id = base.CurrentId;
        obj.AllowCustomValue = ChkCustomValue.Checked;
        obj.Name = TxtName.Text;
        obj.AttributeType = 0;
        obj.ItemType = DropItemType.SelectedValue;
        obj.MeasureUnit = TxtMeasureUnit.Text;
    }

    private void obj2form(PigeonCms.Attribute obj)
    {
        TxtName.Text = obj.Name;
        ChkCustomValue.Checked = obj.AllowCustomValue;
        DropItemType.SelectedValue = obj.ItemType;
        TxtMeasureUnit.Text = obj.MeasureUnit;
    }

    private void values2obj(PigeonCms.Attribute obj)
    {
        TxtName.Text = obj.Name;
        ChkCustomValue.Checked = obj.AllowCustomValue;
        DropItemType.SelectedValue = obj.ItemType;
        TxtMeasureUnit.Text = obj.MeasureUnit;
    }
    private void values2form(PigeonCms.Attribute obj)
    {
        List<PigeonCms.AttributeValue> valueList = new List<PigeonCms.AttributeValue>();
        AttributeValueFilter filter = new AttributeValueFilter();
        filter.AttributeId = base.CurrentId;
        valueList = new PigeonCms.AttributeValuesManager().GetByFilter(filter, "");

        if (valueList.Count > 0 && valueList != null)
        {
            ValuesOk.Visible = true;
            foreach (PigeonCms.AttributeValue value in valueList)
            {
                ListItem item = new ListItem(value.Value, value.Id.ToString());
                ValueList.Items.Add(item);
            }
        }
        else
        {
            EmptyList.Visible = true;
            lblEmptyList.Text = "no records";
        }
        

    }

    private void editRow(int recordId)
    {
        LblOk.Text = "";
        LblErr.Text = "";

        clearForm();
        base.CurrentId = recordId;
        if (base.CurrentId > 0)
        {
            PigeonCms.Attribute obj = new PigeonCms.Attribute();
            obj = new PigeonCms.AttributesManager().GetByKey(base.CurrentId);
            obj2form(obj);
            loadDropsItemTypes();
        }
        MultiView1.ActiveViewIndex = 1;
    }

    private void editValues(int recordId)
    {
        LblOk.Text = "";
        LblErr.Text = "";

        clearForm();
        base.CurrentId = recordId;
        if (base.CurrentId > 0)
        {
            PigeonCms.Attribute obj = new PigeonCms.Attribute();
            obj = new PigeonCms.AttributesManager().GetByKey(base.CurrentId);
            editValueName.Text = "Edit: " +  obj.Name;
            values2form(obj);
            //loadDropsItemTypes();
        }
        MultiView1.ActiveViewIndex = 2;
    }

    private void deleteRow(int recordId)
    {
        LblOk.Text = "";
        LblErr.Text = "";

        try
        {
            new PigeonCms.AttributesManager().DeleteById(recordId);
        }
        catch (Exception e)
        {
            LblErr.Text = RenderError(e.Message);
        }
        Grid1.DataBind();
    }

    private void loadDropsItemTypes()
    {

        //DropItemType.Items.Clear();
        DropItemType.Items.Add(new ListItem(Utility.GetLabel("LblSelectItem", "Select item"), ""));

        var filter = new ItemTypeFilter();
        List<ItemType> recordList = new ItemTypeManager().GetByFilter(filter, "FullName");
        foreach (ItemType record1 in recordList)
        {
            DropItemType.Items.Add(
                new ListItem(record1.FullName, record1.FullName));
        }

    }

    #endregion
}
