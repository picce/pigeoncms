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

        if (!Page.IsPostBack)
        {
        }
    }

    protected void DropEnabledFilter_SelectedIndexChanged(object sender, EventArgs e)
    {
        Grid1.DataBind();
    }

    protected void ObjDs1_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
    {
        CulturesFilter filter = new CulturesFilter();

        filter.Enabled = Utility.TristateBool.NotSet;
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
        //Enabled
        if (e.CommandName == "ImgEnabledOk")
        {
            setFlag(e.CommandArgument.ToString(), false, "enabled");
            Grid1.DataBind();
        }
        if (e.CommandName == "ImgEnabledKo")
        {
            setFlag(e.CommandArgument.ToString(), true, "enabled");
            Grid1.DataBind();
        }
        //Ordering
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
            PigeonCms.Culture item = new PigeonCms.Culture();
            item = (PigeonCms.Culture)e.Row.DataItem;

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

    protected void BtnApply_Click(object sender, EventArgs e)
    {
        LblErr.Text = "";
        LblOk.Text = "";

        try
        {
            new CulturesManager().RefreshCultureList();
        }
        catch (Exception ex)
        {
            LblErr.Text = "Error updating culture list:" + ex.ToString();
            PigeonCms.Trace.Warn("Error", "Error updating culture list", ex);
        }
        finally
        {
            LblOk.Text = "Culture list updated sucefully";
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
            Culture o1 = new Culture();
            if (base.CurrentKey == "")
            {
                form2obj(o1);
                o1 = new CulturesManager().Insert(o1);
            }
            else
            {
                o1 = new CulturesManager().GetByKey(base.CurrentKey);  //precarico i campi esistenti e nn gestiti dal form
                form2obj(o1);
                new CulturesManager().Update(o1);
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

    #region private methods
    private void clearForm()
    {
        TxtCultureCode.Text = "";
        TxtDisplayName.Text = "";
        ChkEnabled.Checked = true;
    }

    private void form2obj(Culture obj)
    {
        obj.CultureCode = TxtCultureCode.Text;
        obj.DisplayName = TxtDisplayName.Text;
        obj.Enabled = ChkEnabled.Checked;
    }

    private void obj2form(Culture obj)
    {
        TxtCultureCode.Text = obj.CultureCode;
        TxtDisplayName.Text = obj.DisplayName;
        ChkEnabled.Checked = obj.Enabled;
    }

    private void editRow(string recordId)
    {
        LblOk.Text = "";
        LblErr.Text = "";

        clearForm();
        base.CurrentKey = recordId;
        if (base.CurrentKey != "")
        {
            Culture obj = new Culture();
            obj = new CulturesManager().GetByKey(base.CurrentKey);
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
            new CulturesManager().DeleteById(recordId);
        }
        catch (Exception e)
        {
            LblErr.Text = e.Message;
        }
        Grid1.DataBind();
    }

    private void setFlag(string recordId, bool value, string flagName)
    {
        try
        {
            PigeonCms.Culture o1 = new PigeonCms.Culture();
            o1 = new CulturesManager().GetByKey(recordId);
            switch (flagName.ToLower())
            {
                case "enabled":
                    o1.Enabled = value;
                    break;
                default:
                    break;
            }
            new CulturesManager().Update(o1);
        }
        catch (Exception e1)
        {
            LblErr.Text = Utility.GetLabel("RECORD_ERR_MSG") + "<br />" + e1.ToString();
        }
        finally { }
    }

    protected void moveRecord(string recordId, Database.MoveRecordDirection direction)
    {
        LblErr.Text = "";
        LblOk.Text = "";

        try
        {
            new CulturesManager().MoveRecord(recordId, direction);
            Grid1.DataBind();
            MultiView1.ActiveViewIndex = 0;
        }
        catch (Exception e1)
        {
            LblErr.Text = Utility.GetLabel("RECORD_ERR_MSG") + "<br />" + e1.ToString();
        }
        finally { }
    }
    #endregion
}
