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
            loadDropMasterPages();
            loadDropThemes();
        }
    }

    protected void DropPublishedFilter_SelectedIndexChanged(object sender, EventArgs e)
    {
        Grid1.DataBind();
    }

    protected void ObjDs1_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
    {
        MvcRoutesFilter filter = new MvcRoutesFilter();

        filter.Published = Utility.TristateBool.NotSet;
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
            setFlag(Convert.ToInt32(e.CommandArgument), false, "published");
            Grid1.DataBind();
        }
        if (e.CommandName == "ImgEnabledKo")
        {
            setFlag(Convert.ToInt32(e.CommandArgument), true, "published");
            Grid1.DataBind();
        }
        //Use SSL
        if (e.CommandName == "ImgUseSslOk")
        {
            setFlag(Convert.ToInt32(e.CommandArgument), false, "usessl");
            Grid1.DataBind();
        }
        if (e.CommandName == "ImgUseSslKo")
        {
            setFlag(Convert.ToInt32(e.CommandArgument), true, "usessl");
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
            MvcRoute item = new MvcRoute();
            item = (MvcRoute)e.Row.DataItem;

            LinkButton LnkName = (LinkButton)e.Row.FindControl("LnkName");
            LnkName.Text = "<i class='fa fa-pgn_edit fa-fw'></i>";
            LnkName.Text += item.Name;
            if (string.IsNullOrEmpty(item.Name))
                LnkName.Text += Utility.GetLabel("NO_VALUE", "<no value>");

            //Published
            if (item.Published)
            {
                var img1 = e.Row.FindControl("ImgEnabledOk");
                img1.Visible = true;
            }
            else
            {
                var img1 = e.Row.FindControl("ImgEnabledKo");
                img1.Visible = true;
            }

            //Use ssl
            if (item.UseSsl)
            {
                var img1 = e.Row.FindControl("ImgUseSslOk");
                img1.Visible = true;
            }
            else
            {
                var img1 = e.Row.FindControl("ImgUseSslKo");
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

    protected void BtnApply_Click(object sender, EventArgs e)
    {
        LblErr.Text = "";
        LblOk.Text = "";

        try
        {
            new MvcRoutesManager().SetAppRoutes();
        }
        catch (Exception ex)
        {
            LblErr.Text = RenderError("Error updating routes table:" + ex.ToString());
            PigeonCms.Trace.Warn("Error", "Error updating routes table", ex);
        }
        finally
        {
            LblOk.Text = RenderSuccess("Routes list updated sucessfully");
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
            MvcRoute o1 = new MvcRoute();
            if (base.CurrentId == 0)
            {
                form2obj(o1);
                o1 = new MvcRoutesManager().Insert(o1);
            }
            else
            {
                o1 = new MvcRoutesManager().GetByKey(base.CurrentId);  //precarico i campi esistenti e nn gestiti dal form
                form2obj(o1);
                new MvcRoutesManager().Update(o1);
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
        LblErr.Text = "";
        LblOk.Text = "";
        MultiView1.ActiveViewIndex = 0;
    }

    #region private methods
    private void clearForm()
    {
        LitId.Text = "";
        TxtName.Text = "";
        TxtName.Enabled = true;
        TxtPattern.Text = "";
        ChkPublished.Checked = true;
        ChkUseSsl.Checked = false;
        ChkIsCore.Checked = false;
        Utility.SetDropByValue(DropMasterPage, "");
        Utility.SetDropByValue(DropTheme, "");
    }

    private void form2obj(MvcRoute obj)
    {
        obj.Id = base.CurrentId;
        obj.Published = ChkPublished.Checked;
        obj.UseSsl = ChkUseSsl.Checked;
        obj.Name = TxtName.Text;
        obj.Pattern = TxtPattern.Text;
        obj.IsCore = ChkIsCore.Checked;
        obj.CurrMasterPage = DropMasterPage.SelectedValue;
        obj.CurrTheme = DropTheme.SelectedValue;
    }

    private void obj2form(MvcRoute obj)
    {
        TxtName.Text = obj.Name;
        TxtPattern.Text = obj.Pattern;
        ChkPublished.Checked = obj.Published;
        ChkUseSsl.Checked = obj.UseSsl;
        LitId.Text = obj.Id.ToString();
        ChkIsCore.Checked = obj.IsCore;
        Utility.SetDropByValue(DropMasterPage, obj.CurrMasterPage);
        Utility.SetDropByValue(DropTheme, obj.CurrTheme);
        TxtName.Enabled = !obj.IsCore;
    }

    private void editRow(int recordId)
    {
        LblOk.Text = "";
        LblErr.Text = "";

        clearForm();
        base.CurrentId = recordId;
        if (base.CurrentId > 0)
        {
            MvcRoute obj = new MvcRoute();
            obj = new MvcRoutesManager().GetByKey(base.CurrentId);
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
            new MvcRoutesManager().DeleteById(recordId);
        }
        catch (Exception e)
        {
            LblErr.Text = RenderError(e.Message);
        }
        Grid1.DataBind();
    }

    private void setFlag(int recordId, bool value, string flagName)
    {
        try
        {
            PigeonCms.MvcRoute o1 = new PigeonCms.MvcRoute();
            o1 = new MvcRoutesManager().GetByKey(recordId);
            switch (flagName.ToLower())
            {
                case "published":
                    o1.Published = value;
                    break;
                case "usessl":
                    o1.UseSsl = value;
                    break;
                default:
                    break;
            }
            new MvcRoutesManager().Update(o1);
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

        try
        {
            new MvcRoutesManager().MoveRecord(recordId, direction);
            Grid1.DataBind();
            MultiView1.ActiveViewIndex = 0;
        }
        catch (Exception e1)
        {
            LblErr.Text = RenderError(Utility.GetLabel("RECORD_ERR_MSG") + "<br />" + e1.ToString());
        }
        finally { }
    }

    private void loadDropMasterPages()
    {
        DropMasterPage.Items.Clear();
        DropMasterPage.Items.Add(new ListItem(Utility.GetLabel("LblUseBlobal", "Use global"), ""));

        Dictionary<string, string> recordList = new MasterPagesObjManager().GetList();
        foreach (KeyValuePair<string, string> item in recordList)
        {
            DropMasterPage.Items.Add(new ListItem(item.Value, item.Key));
        }
    }

    private void loadDropThemes()
    {
        DropTheme.Items.Clear();
        DropTheme.Items.Add(new ListItem(Utility.GetLabel("LblUseBlobal", "Use global"), ""));

        Dictionary<string, string> recordList = new ThemesObjManager().GetList();
        foreach (KeyValuePair<string, string> item in recordList)
        {
            DropTheme.Items.Add(new ListItem(item.Value, item.Key));
        }
    }
    #endregion
}
