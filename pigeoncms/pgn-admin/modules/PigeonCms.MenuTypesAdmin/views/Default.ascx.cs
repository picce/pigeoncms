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
        if (!Page.IsPostBack)
        {
        }
    }

    protected void ObjDs1_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
    {
        MenutypeFilter filter = new MenutypeFilter();
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
            Menutype o1 = new PigeonCms.Menutype();
            if (base.CurrentId == 0)
            {
                form2obj(o1);
                o1 = new MenutypesManager().Insert(o1);
            }
            else
            {
                o1 = new MenutypesManager().GetByKey(base.CurrentId);
                form2obj(o1);
                new MenutypesManager().Update(o1);
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

    protected void MultiView1_ActiveViewChanged(object sender, EventArgs e)
    {
        if (MultiView1.ActiveViewIndex == 1)    //edit view
        {
        }
    }

    #region private methods

    private void clearForm()
    {
        TxtMenuType.Text = "";
        TxtTitle.Text = "";
        TxtDescription.Text = "";
    }

    private void form2obj(Menutype obj)
    {
        obj.Id = base.CurrentId;
        obj.MenuType = TxtMenuType.Text;
        obj.Title = TxtTitle.Text;
        obj.Description = TxtDescription.Text;
    }

    private void obj2form(Menutype obj)
    {
        TxtMenuType.Text = obj.MenuType;
        TxtTitle.Text = obj.Title;
        TxtDescription.Text = obj.Description;
    }

    private void editRow(int recordId)
    {
        LblOk.Text = "";
        LblErr.Text = "";

        clearForm();
        base.CurrentId = recordId;
        if (base.CurrentId > 0)
        {
            Menutype obj = new Menutype();
            obj = new MenutypesManager().GetByKey(base.CurrentId);
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
            new MenutypesManager().DeleteById(recordId);
        }
        catch (Exception e)
        {
            LblErr.Text = e.Message;
        }
        Grid1.DataBind();
    }

    #endregion
}
