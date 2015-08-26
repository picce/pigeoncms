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

public partial class Controls_Default : PigeonCms.BaseModuleControl
{
    protected string Name
    {
        get { return base.GetStringParam("Name", "", "Name"); }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        LblOk.Text = "";
        LblErr.Text = "";

        if (this.BaseModule.DirectEditMode)
        {
            if (string.IsNullOrEmpty(this.Name))
                throw new ArgumentException();
            if (string.IsNullOrEmpty(
                new PlaceholdersManager().GetByName(this.Name).Name))
                throw new ArgumentException();

            BtnNew.Visible = false;
            //BtnSave.OnClientClick = "closePopup();";
            BtnCancel.OnClientClick = "closePopup();";
            edit(this.Name);
        }
    }

    protected void ObjDs1_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
    {
        PlaceholderFilter filter = new PlaceholderFilter();
        filter.Visible = Utility.TristateBool.NotSet;
        if (!string.IsNullOrEmpty(this.Name))
            filter.Name = this.Name;

        e.InputParameters["filter"] = filter;
    }

    protected void Grid1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            var item = new PigeonCms.Placeholder();
            item = (PigeonCms.Placeholder)e.Row.DataItem;

            LinkButton LnkName = (LinkButton)e.Row.FindControl("LnkName");
            LnkName.Text = "<i class='fa fa-pgn_edit fa-fw'></i>";
            LnkName.Text += Utility.Html.GetTextPreview(item.Name, 30, "");
            if (string.IsNullOrEmpty(item.Name))
                LnkName.Text += Utility.GetLabel("NO_VALUE", "<no value>");

        }
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
    }

    protected void Grid1_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
            Utility.AddGlyph(Grid1, e.Row);
    }

    protected void MultiView1_ActiveViewChanged(object sender, EventArgs e)
    {
        if (this.BaseModule.DirectEditMode)
        {
            //list view not allowed (in case of js hacking)
            if (MultiView1.ActiveViewIndex == 0)
                MultiView1.ActiveViewIndex = 1;

        }
    }

    protected void BtnSave_Click(object sender, EventArgs e)
    {
        //rempoved 20130610 OnClientClick="MyObject.UpdateEditorFormValue();"
        LblErr.Text = "";
        LblOk.Text = "";

        try
        {
            Placeholder p1 = new Placeholder();
            if (TxtId.Text == string.Empty)
            {
                form2obj(p1);
                p1 = new PlaceholdersManager().Insert(p1);
            }
            else
            {
                p1 = new PlaceholdersManager().GetByName(TxtName.Text);//precarico i campi esistenti e nn gestiti dal form
                form2obj(p1);
                new PlaceholdersManager().Update(p1);
            }
            new CacheManager<Placeholder>("PigeonCms.Placeholder").Remove(p1.Name);

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

    protected void BtnNew_Click(object sender, EventArgs e)
    {
        edit("");
    }


    #region private methods
    private void form2obj(Placeholder obj1)
    {
        obj1.Name = TxtName.Text;
        obj1.Visible = ChkVisibile.Checked;
        obj1.Content = TxtContent.Text;
    }

    private void obj2form(Placeholder obj1)
    {
        TxtName.Text = obj1.Name;
        ChkVisibile.Checked = obj1.Visible;
        TxtContent.Text = obj1.Content;
    }

    private void edit(string name)
    {
        LblOk.Text = "";
        LblErr.Text = "";

        TxtName.Text = name;
        TxtName.Enabled = true;
        ChkVisibile.Checked = true;
        TxtId.Text = "";
        TxtContent.Text = "";
        if (name != "")
        {
            TxtId.Text = "1";
            TxtName.Enabled = false;
            Placeholder currObj = new Placeholder();
            currObj = new PlaceholdersManager().GetByName(name);
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
            new PlaceholdersManager().Delete(name);
            new CacheManager<Placeholder>("PigeonCms.Placeholder").Remove(name);
        }
        catch (Exception e)
        {
            LblErr.Text = RenderError(e.Message);
        }
        Grid1.DataBind();
    }
    #endregion
}
