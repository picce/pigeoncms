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
    protected void Page_Load(object sender, EventArgs e)
    {
        LblOk.Text = "";
        LblErr.Text = "";
    }

    protected void ObjDs1_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
    {
        WebConfigEntryFilter filter = new WebConfigEntryFilter();
        e.InputParameters["filter"] = filter;
    }

    protected void Grid1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Select")
        {
            editRow(e.CommandArgument.ToString());
        }
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
            PigeonCms.WebConfigEntry item = new PigeonCms.WebConfigEntry();
            item = (PigeonCms.WebConfigEntry)e.Row.DataItem;

            LinkButton LnkSel = (LinkButton)e.Row.FindControl("LnkSel");
            LnkSel.Text = "<i class='fa fa-pgn_edit fa-fw'></i>";
            LnkSel.Text += item.Key;
        }
    }

    protected void BtnNew_Click(object sender, EventArgs e)
    {
        editRow("");
    }

    protected void BtnSave_Click(object sender, EventArgs e)
    {
        LblErr.Text = "";
        LblOk.Text = "";

        try
        {
            WebConfigEntry o1 = new WebConfigEntry();
            if (base.CurrentKey == "")
            {
                form2obj(o1);
                new WebConfigManager().Insert(o1);
            }
            else
            {
                o1 = new WebConfigManager().GetByKey(base.CurrentKey);  //precarico i campi esistenti e nn gestiti dal form
                form2obj(o1);
                new WebConfigManager().Update(o1);
            }
            Grid1.DataBind();
            LblOk.Text = RenderSuccess( Utility.GetLabel("RECORD_SAVED_MSG"));
            MultiView1.ActiveViewIndex = 0;
        }
        catch (Exception e1)
        {
            LblErr.Text = RenderError( Utility.GetLabel("RECORD_ERR_MSG") + "<br />" + e1.ToString());
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
        TxtKey.Enabled = true;
        TxtKey.Text = "";
        TxtValue.Text = "";
    }

    private void form2obj(WebConfigEntry obj)
    {
        obj.Key = TxtKey.Text;
        obj.Value = TxtValue.Text;
    }

    private void obj2form(WebConfigEntry obj)
    {
        TxtKey.Enabled = string.IsNullOrEmpty(obj.Key);

        TxtKey.Text = obj.Key;
        TxtValue.Text = obj.Value;
    }

    private void editRow(string recordId)
    {
        LblOk.Text = "";
        LblErr.Text = "";

        clearForm();
        base.CurrentKey = recordId;
        if (!string.IsNullOrEmpty(base.CurrentKey))
        {
            WebConfigEntry obj = new WebConfigEntry();
            obj = new WebConfigManager().GetByKey(base.CurrentKey);
            obj2form(obj);
        }
        MultiView1.ActiveViewIndex = 1;
    }

    private void deleteRow(string recordId)
    {
        LblOk.Text = "";
        LblErr.Text = "";

        try
        {
            new WebConfigManager().Delete(recordId);
        }
        catch (Exception e)
        {
            LblErr.Text = RenderError( e.Message);
        }
        Grid1.DataBind();
    }
    #endregion
}
