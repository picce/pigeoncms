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

    protected void DropEnabledFilter_SelectedIndexChanged(object sender, EventArgs e)
    {
        Grid1.DataBind();
    }

    protected void ObjDs1_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
    {
        TemplateBlockFilter filter = new TemplateBlockFilter();
        switch (DropEnabledFilter.SelectedValue)
        {
            case "1":
                filter.Enabled = Utility.TristateBool.True;
                break;
            case "0":
                filter.Enabled = Utility.TristateBool.False;
                break;
            default:
                filter.Enabled = Utility.TristateBool.NotSet;
                break;
        }
        filter.Name = "";
        e.InputParameters["filter"] = filter;
    }

    protected void Grid1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Select")
        {
            edit(e.CommandArgument.ToString());
        }
        if (e.CommandName == "DeleteRow")
        {
            delete(e.CommandArgument.ToString());
        }
        if (e.CommandName == "MoveDown")
        {
            moveRecord(e.CommandArgument.ToString(), Database.MoveRecordDirection.Down);
        }
        if (e.CommandName == "MoveUp")
        {
            moveRecord(e.CommandArgument.ToString(), Database.MoveRecordDirection.Up);
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
            var item = new TemplateBlock();
            item = (TemplateBlock)e.Row.DataItem;

            LinkButton LnkName = (LinkButton)e.Row.FindControl("LnkName");
            LnkName.Text = "<i class='fa fa-pgn_edit fa-fw'></i>";
            LnkName.Text += item.Name;
        }
    }

    protected void BtnSave_Click(object sender, EventArgs e)
    {
        LblErr.Text = "";
        LblOk.Text = "";

        try
        {
            TemplateBlock p1 = new TemplateBlock();
            if (TxtId.Text == string.Empty)
            {
                form2obj(p1);
                p1 = new TemplateBlocksManager().Insert(p1);
            }
            else
            {
                p1 = new TemplateBlocksManager().GetByKey(TxtName.Text);//precarico i campi esistenti e nn gestiti dal form
                form2obj(p1);
                new TemplateBlocksManager().Update(p1);
            }
            Grid1.DataBind();
            LblOk.Text = Utility.GetLabel("RECORD_SAVED_MSG");
            MultiView1.ActiveViewIndex = 0;
        }
        catch (Exception e1)
        {
            LblErr.Text = Utility.GetLabel("RECORD_ERR_MSG") + "<br />" + e1.ToString();
        }
        finally
        {
        }
    }

    protected void BtnCancel_Click(object sender, EventArgs e)
    {
        MultiView1.ActiveViewIndex = 0;
    }

    protected void BtnNew_Click(object sender, EventArgs e)
    {
        edit("");
    }


    #region private methods
    private void form2obj(TemplateBlock obj1)
    {
        obj1.Name = TxtName.Text;
        obj1.Title = TxtTitle.Text;
        obj1.Enabled = ChkEnabled.Checked;
    }

    private void obj2form(TemplateBlock obj1)
    {
        TxtName.Text = obj1.Name;
        TxtTitle.Text = obj1.Title;
        ChkEnabled.Checked = obj1.Enabled;
    }

    private void edit(string name)
    {
        LblOk.Text = "";
        LblErr.Text = "";

        TxtName.Text = name;
        TxtName.Enabled = true;
        TxtTitle.Text = "";
        ChkEnabled.Checked = true;
        TxtId.Text = "";
        if (name != "")
        {
            TxtId.Text = "1";
            TxtName.Enabled = false;
            TemplateBlock currObj = new TemplateBlock();
            currObj = new TemplateBlocksManager().GetByKey(name);
            obj2form(currObj);
        }
        MultiView1.ActiveViewIndex = 1;
    }

    private void delete(string name)
    {
        LblOk.Text = "";
        LblErr.Text = "";

        try
        {
            new TemplateBlocksManager().DeleteById(name);
        }
        catch (Exception e)
        {
            LblErr.Text = e.Message;
        }
        Grid1.DataBind();
    }

    protected void moveRecord(string pageName, Database.MoveRecordDirection direction)
    { }
    #endregion
}
