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
using System.Linq;
using PigeonCms.Core.Helpers;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

//TOCHECK-LOLLO
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

        foreach (KeyValuePair<string, string> item in Config.CultureList)
        {
            //title
            Panel pan1 = new Panel();
            pan1.ID = "PnlTitle" + item.Value;
            pan1.CssClass = "form-group input-group";
            PanelTitle.Controls.Add(pan1);

            TextBox txt1 = new TextBox();
            txt1.ID = "TxtTitle" + item.Value;
            txt1.MaxLength = 50;
            txt1.CssClass = "form-control";
            txt1.ToolTip = item.Key;
            if (!ChkInLang.Checked)
                LabelsProvider.SetLocalizedControlVisibility(true, item.Key, pan1);              
            pan1.Controls.Add(txt1);
            Literal lit1 = new Literal();
            lit1.Text = "<span class='input-group-addon'>" + item.Value + "</span>";
            pan1.Controls.Add(lit1);

        }

    }

    protected void ChkInLang_CheckedChanged(object sender, EventArgs e)
    {
        foreach (KeyValuePair<string, string> item in Config.CultureList)
        {
            Panel pan1 = (Panel)PanelTitle.FindControl("PnlTitle" + item.Value);

            if (!ChkInLang.Checked)
                LabelsProvider.SetLocalizedControlVisibility(true, item.Key, pan1);
            else
            {
                pan1.Visible = true;
            }
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
            // the 0 will leave blank the value field
            editValues(int.Parse(e.CommandArgument.ToString()), 0);
        }
        //Custom
        if (e.CommandName == "CustomEnabled")
        {
            setFlag(Convert.ToInt32(e.CommandArgument), false, "enabled");
            Grid1.DataBind();
        }
        if (e.CommandName == "CustomDisabled")
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
            PigeonCms.Attribute item = new PigeonCms.Attribute();
            item = (PigeonCms.Attribute)e.Row.DataItem;

            LinkButton LnkTitle = (LinkButton)e.Row.FindControl("LnkTitle");
            LnkTitle.Text = "<i class='fa fa-pgn_edit fa-fw'></i>";
            LnkTitle.Text += Utility.Html.GetTextPreview(item.Name, 50, "");
            if (string.IsNullOrEmpty(LnkTitle.Text))
                LnkTitle.Text += Utility.GetLabel("NO_VALUE", "<no value>");

            //if(item.AllowCustomValue == false) {

            //    var filter = new AttributeValueFilter();
            //    filter.AttributeId = item.Id;
            //    filter.NumOfRecords = 3;
            //    var values = new AttributeValuesManager().GetByFilter(filter, "");

            //    LinkButton LnkEditValues = (LinkButton)e.Row.FindControl("LnkEditValues");
            //    LnkEditValues.Text = "<i class='fa fa-pgn_edit fa-fw'></i> Edit Records <br>";
            //    Literal ValuesPreview = (Literal)e.Row.FindControl("ValuesPreview");
            //    string records = "";
            //    foreach (var value in values)
            //    {
            //        records +=  " - " + Utility.Html.GetTextPreview(value.Value, 50, "");
            //    }

            //    if(records.Length > 2)
            //        ValuesPreview.Text = records.Substring(2);

            //    if (string.IsNullOrEmpty(LnkTitle.Text))
            //        LnkEditValues.Text += Utility.GetLabel("NO_VALUE", "<no value>");

            //}
            //else
            //{
            //    LinkButton LnkEditValues = (LinkButton)e.Row.FindControl("LnkEditValues");
            //    LnkEditValues.Text = "";
            //    LnkEditValues.Enabled = false;
            //}

            if (item.AllowCustomValue)
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

    protected void ObjValueSource_ObjectCreating(object sender, ObjectDataSourceEventArgs e)
    {
        var typename = new PigeonCms.AttributeValuesManager();
        e.ObjectInstance = typename;
    }

    protected void ObjValueSource_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
    {
        PigeonCms.AttributeValueFilter filter = new PigeonCms.AttributeValueFilter();
        filter.AttributeId = base.CurrentId;
        e.InputParameters["filter"] = filter;
    }

    protected void GridValues_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Select")
        {
            // edit values now have Id, we can edit it !
            editValues(base.CurrentId, int.Parse(e.CommandArgument.ToString()));
        }
        if (e.CommandName == "DeleteRow")
        {
            deleteValue(int.Parse(e.CommandArgument.ToString()));
        }

    }

    protected void GridValues_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
            Utility.AddGlyph(GridValues, e.Row);
    }

    protected void GridValues_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            PigeonCms.AttributeValue item = new PigeonCms.AttributeValue();
            item = (PigeonCms.AttributeValue)e.Row.DataItem;

            LinkButton LnkTitle = (LinkButton)e.Row.FindControl("LnkTitle");
            LnkTitle.Text = "<i class='fa fa-pgn_edit fa-fw'></i>";
            LnkTitle.Text += Utility.Html.GetTextPreview(item.Value, 50, "");
            if (string.IsNullOrEmpty(LnkTitle.Text))
                LnkTitle.Text += Utility.GetLabel("NO_VALUE", "<no value>");

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

        // conrollo che non tutti i valori siano vuoti
        bool allEmpty = true;

        foreach (KeyValuePair<string, string> item in Config.CultureList)
        {
            TextBox t1 = new TextBox();
            t1 = (TextBox)PanelTitle.FindControl("TxtTitle" + item.Value);
            if (!string.IsNullOrEmpty(t1.Text))
            {
                allEmpty = false;
                break;
            }
        }

        if (allEmpty)
        {
            return;
        }

        try
        {
            PigeonCms.AttributeValue o1 = new PigeonCms.AttributeValue();
            if (Convert.ToInt32(base.CurrentKey) == 0)
            {
                values2obj(o1);
                o1 = new AttributeValuesManager().Insert(o1);
            }
            else
            {
                o1 = new AttributeValuesManager().GetByKey(Convert.ToInt32(base.CurrentKey));  //precarico i campi esistenti e nn gestiti dal form
                o1.ValueTranslations.Clear();
                values2obj(o1);
                new AttributeValuesManager().Update(o1);
            }
            base.CurrentKey = "0";
            clearForm();
            Grid1.DataBind();
            GridValues.DataBind();
            LblOk.Text = RenderSuccess(Utility.GetLabel("RECORD_SAVED_MSG"));

        }
        catch (Exception e1)
        {
            LblErr.Text = RenderError(Utility.GetLabel("RECORD_ERR_MSG") + "<br />" + e1.ToString());
        }
        finally
        {
        }
    }

    protected void BtnNewValue_Click(object sender, EventArgs e)
    {
        base.CurrentKey = "0";
        clearForm();
        Grid1.DataBind();
        GridValues.DataBind();
    }

    protected void BtnCancel_Click(object sender, EventArgs e)
    {
        MultiView1.ActiveViewIndex = 0;
    }

    #region private methods

    private void clearForm()
    {
        TxtName.Text = "";
        ChkCustomValue.Checked = false;
        ChkInLang.Checked = true;
        foreach (KeyValuePair<string, string> item in Config.CultureList)
        {
            TextBox t1 = new TextBox();
            t1 = (TextBox)PanelTitle.FindControl("TxtTitle" + item.Value);
            t1.Text = "";
        }
    }

    private void form2obj(PigeonCms.Attribute obj)
    {
        obj.Id = base.CurrentId;
        obj.AllowCustomValue = ChkCustomValue.Checked;
        obj.Name = TxtName.Text;
    }

    private void obj2form(PigeonCms.Attribute obj)
    {
        TxtName.Text = obj.Name;
        ChkCustomValue.Checked = obj.AllowCustomValue;
    }

    private void values2obj(PigeonCms.AttributeValue obj)
    {

        foreach (KeyValuePair<string, string> item in Config.CultureList)
        {
            TextBox t1 = new TextBox();
            t1 = (TextBox)PanelTitle.FindControl("TxtTitle" + item.Value);
            obj.ValueTranslations.Add(item.Key, t1.Text);
        }

        obj.AttributeId = base.CurrentId;
    }

    private void values2form(PigeonCms.AttributeValue obj)
    {

        foreach (KeyValuePair<string, string> item in Config.CultureList)
        {
            string sTitleTranslation = "";
            TextBox t1 = new TextBox();
            t1 = (TextBox)PanelTitle.FindControl("TxtTitle" + item.Value);
            obj.ValueTranslations.TryGetValue(item.Key, out sTitleTranslation);
            if (string.IsNullOrEmpty(sTitleTranslation))
                obj.ValueTranslations.TryGetValue(Config.CultureDefault, out sTitleTranslation);
            t1.Text = sTitleTranslation;
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
        }
        MultiView1.ActiveViewIndex = 1;
    }

    private void editValues(int recordId, int attrValId)
    {
        LblOk.Text = "";
        LblErr.Text = "";

        clearForm();

        base.CurrentKey = attrValId.ToString();
        base.CurrentId = recordId;

        if (recordId > 0)
        {
            PigeonCms.Attribute obj = new PigeonCms.Attribute();
            obj = new PigeonCms.AttributesManager().GetByKey(base.CurrentId);
            editValueName.Text = "Edit: " +  obj.Name;
        }
        if (attrValId > 0)
        {
            PigeonCms.AttributeValue obj = new PigeonCms.AttributeValue();
            obj = new PigeonCms.AttributeValuesManager().GetByKey(attrValId);
            values2form(obj);
        }
        GridValues.DataBind();
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

    private void deleteValue(int recordId)
    {
        LblOk.Text = "";
        LblErr.Text = "";

        try
        {
            // have to check if is used by some products 
            var iavfilter = new ItemAttributeValueFilter();
            iavfilter.AttributeValueId = recordId;
            iavfilter.OnlyPopulatedFields = true;
            bool isUsed = (new ItemAttributesValuesManager().GetByFilter(iavfilter, "").Count > 0);
            if (!isUsed)
            {
                new PigeonCms.AttributeValuesManager().DeleteById(recordId);
            }
            else
            {
                LblErr.Text = RenderError("value assigned to a product. delete the product before the attribute.");
            }
            
        }
        catch (Exception e)
        {
            LblErr.Text = RenderError(e.Message);
        }
        GridValues.DataBind();
    }

    private void setFlag(int recordId, bool value, string flagName)
    {
        try
        {
            if (!PgnUserCurrent.IsAuthenticated)
                throw new Exception("user not authenticated");

            var o1 = new PigeonCms.AttributesManager().GetByKey(recordId);
            switch (flagName.ToLower())
            {
                case "enabled":
                    o1.AllowCustomValue = value;
                    break;
                default:
                    break;
            }
            new PigeonCms.AttributesManager().Update(o1);
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
        LblOk.Text = RenderSuccess("");

        try
        {
            if (!PgnUserCurrent.IsAuthenticated)
                throw new Exception("user not authenticated");

            //new PigeonCms.AttributesManager().MoveRecord(recordId, direction);
            Grid1.DataBind();
            MultiView1.ActiveViewIndex = 1;
        }
        catch (Exception e1)
        {
            LblErr.Text = RenderError(Utility.GetLabel("RECORD_ERR_MSG") + "<br />" + e1.ToString());
        }
        finally { }
    }

    #endregion

}
