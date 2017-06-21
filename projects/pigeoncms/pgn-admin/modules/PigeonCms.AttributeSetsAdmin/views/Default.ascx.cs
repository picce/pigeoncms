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
        var typename = new AttributeSetsManager();
        e.ObjectInstance = typename;
    }

    protected void ObjDs1_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
    {
        var filter = new AttributeSetFilter();
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

            var item = new AttributeSet();
            item = (AttributeSet)e.Row.DataItem;

            LinkButton LnkTitle = (LinkButton)e.Row.FindControl("LnkTitle");
            LnkTitle.Text = "<i class='fa fa-pgn_edit fa-fw'></i>";
            LnkTitle.Text += Utility.Html.GetTextPreview(item.Name, 50, "");
            if (string.IsNullOrEmpty(LnkTitle.Text))
                LnkTitle.Text += Utility.GetLabel("NO_VALUE", "<no value>");
            if (Roles.IsUserInRole("debug"))
                LnkTitle.Text += " [" + item.Id.ToString() + "]";


            // Get all selected attributes
            string selectedAttributes = "";
            var aman = new AttributesManager();
            foreach (var attributeId in item.AttributesList)
            {
                selectedAttributes += " - <i>" + aman.GetByKey(attributeId).Name  + "<i>";
            }

            selectedAttributes = selectedAttributes.Substring(2);

            Literal LitAttributesSelected = (Literal)e.Row.FindControl("LitAttributesSelected");
            LitAttributesSelected.Text = selectedAttributes;

        }
    }

    protected void Grid2_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
            Utility.AddGlyph(Grid2, e.Row);
    }
    protected void Grid2_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            var item = new PigeonCms.Attribute();
            item = (PigeonCms.Attribute)e.Row.DataItem;
        }
    }

    protected void ObjDs2_ObjectCreating(object sender, ObjectDataSourceEventArgs e)
    {
        var typename = new PigeonCms.AttributesManager();
        e.ObjectInstance = typename;
    }
    protected void ObjDs2_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
    {
        var filter = new PigeonCms.AttributeFilter();
        e.InputParameters["filter"] = filter;
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
            var o1 = new AttributeSet();
            var man = new AttributeSetsManager();
            if (base.CurrentId == 0)
            {
                form2obj(o1);
                o1 = man.Insert(o1);
            }
            else
            {
                o1 = man.GetByKey(base.CurrentId);  //precarico i campi esistenti e nn gestiti dal form
                form2obj(o1);
                man.Update(o1);
            }
            Grid1.DataBind();
            Grid2.DataBind();
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
        TxtSetName.Text = "";

        foreach (GridViewRow r in Grid2.Rows)
        {
            CheckBox cb = (CheckBox)r.FindControl("chkRow");
            cb.Checked = false;
        }
    }

    private void form2obj(AttributeSet obj)
    {
        obj.Name = TxtSetName.Text;
        
        var ids = new List<int>();
        foreach (GridViewRow r in Grid2.Rows)
        {
            CheckBox cb = (CheckBox)r.FindControl("chkRow");
            string IdString = r.Cells[3].Text;
            int id = 0;
            int.TryParse(IdString, out id);

            if (cb.Checked)
            {
                ids.Add(id);
            }
        }

        obj.AttributesList = ids;
    }

    private void obj2form(AttributeSet obj)
    {
        TxtSetName.Text = obj.Name;

        foreach (GridViewRow r in Grid2.Rows)
        {
            CheckBox cb = (CheckBox)r.FindControl("chkRow");
            string IdString = r.Cells[3].Text;
            int id = 0;
            int.TryParse(IdString, out id);

            if (obj.AttributesList.Contains(id))
            {
                cb.Checked = true;
            }
            else
            {
                cb.Checked = false;
            }
        }

    }

    private void editRow(int recordId)
    {
        LblOk.Text = "";
        LblErr.Text = "";

        clearForm();
        base.CurrentId = recordId;
        var man = new AttributeSetsManager();
        if (base.CurrentId > 0)
        {
            var obj = new AttributeSet();
            obj = man.GetByKey(base.CurrentId);
            obj2form(obj);
        }
        MultiView1.ActiveViewIndex = 1;
    }

    private void deleteRow(int recordId)
    {
        LblOk.Text = "";
        LblErr.Text = "";
        var man = new AttributeSetsManager();
        try
        {
            man.DeleteById(recordId);
        }
        catch (Exception e)
        {
            LblErr.Text = RenderError(e.Message);
        }
        Grid1.DataBind();
    }

    #endregion
}
