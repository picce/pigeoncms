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
            AppSetting app1 = new AppSetting();
            app1 = (AppSetting)e.Row.DataItem;

            LinkButton LnkName = (LinkButton)e.Row.FindControl("LnkName");
            LnkName.Text = "<i class='fa fa-pgn_edit fa-fw'></i>";
            LnkName.Text += Utility.Html.GetTextPreview(app1.KeyName, 30, "");
            if (string.IsNullOrEmpty(app1.KeyName))
                LnkName.Text += Utility.GetLabel("NO_VALUE", "<no value>");

            Label LblKeyValue = (Label)e.Row.FindControl("LblKeyValue");
            LblKeyValue.Text = Utility.Html.GetTextPreview(app1.KeyValue, 40, "");
        }
    }

    protected void BtnSave_Click(object sender, EventArgs e)
    {
        LblErr.Text = "";
        LblOk.Text = "";

        try
        {
            AppSetting p1 = new AppSetting();
            form2obj(p1);
            if (HiddenNewRecord.Value == "true")
            {
                p1 = AppSettingsManager.Insert(p1);
            }
            else
            {
                AppSettingsManager.Update(p1);
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

    protected void BtnNew_Click(object sender, EventArgs e)
    {
        editRow("");
    }

    protected void BtnApply_Click(object sender, EventArgs e)
    {
        AppSettingsManager.RefreshApplicationVars();
    }


    #region private methods
    private void form2obj(AppSetting obj)
    {
        obj.KeyName = TxtKeyName.Text;
        obj.KeyTitle = TxtKeyTitle.Text;
        obj.KeyValue = TxtKeyValue.Text;
        obj.KeyInfo = TxtKeyInfo.Text;
    }

    private void obj2form(AppSetting obj)
    {
        TxtKeyName.Text = obj.KeyName;
        TxtKeyTitle.Text = obj.KeyTitle;
        TxtKeyValue.Text = obj.KeyValue;
        TxtKeyInfo.Text = obj.KeyInfo;
    }

    private void editRow(string keyName)
    {
        LblOk.Text = "";
        LblErr.Text = "";

        clearForm();
        if (keyName != string.Empty)
        {
            AppSetting obj = new AppSetting();
            obj = AppSettingsManager.GetSettingByKey(keyName);
            TxtKeyName.Enabled = false;
            obj2form(obj);
        }
        else
        {
            HiddenNewRecord.Value = "true";
            TxtKeyName.Enabled = true;
        }
        MultiView1.ActiveViewIndex = 1;
    }

    private void deleteRow(string keyName)
    {
        LblOk.Text = "";
        LblErr.Text = "";
        try
        {
            AppSettingsManager.Delete(keyName);
        }
        catch (Exception e)
        {
            LblErr.Text = RenderError(e.Message);
        }
        Grid1.DataBind();
    }

    private void clearForm()
    {
        HiddenNewRecord.Value = "";
        TxtKeyName.Text = "";
        TxtKeyTitle.Text = "";
        TxtKeyValue.Text = "";
        TxtKeyInfo.Text = "";
    }
    #endregion
}
