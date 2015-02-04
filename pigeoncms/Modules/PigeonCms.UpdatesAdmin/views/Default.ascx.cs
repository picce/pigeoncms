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
using Ionic.Zip;
using System.IO;
using System.Data.Common;

public partial class Controls_UpdatesAdmin : PigeonCms.BaseModuleControl
{
    private enum ViewIndex
    {
        Install = 0, 
        SeeModules, 
        SeeTemplates, 
        EditModule, 
        Sql
    }

    int targetLabelsPopup = 0;
    protected int TargetLabelsPopup
    {
        get { return GetIntParam("TargetLabelsPopup", targetLabelsPopup); }
        set { targetLabelsPopup = value; }
    }

    string labelsPopupUrl = "";
    protected string LabelsPopupUrl
    {
        get
        {
            if (string.IsNullOrEmpty(labelsPopupUrl) && this.TargetLabelsPopup > 0)
            {
                var menuMan = new MenuManager();
                var menuTarget = new PigeonCms.Menu();
                menuTarget = menuMan.GetByKey(this.TargetLabelsPopup);
                labelsPopupUrl = Utility.GetRoutedUrl(menuTarget);
            }
            return labelsPopupUrl;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Roles.IsUserInRole("admin"))
            throw new HttpException(404, "Page not found");

        if (!Page.IsPostBack)
        {
            loadDropCoreFilter();
        }
        else
        {
            ////reload params on every postback, because cannot manage dinamically fields
            //PigeonCms.Menu currMenu = new PigeonCms.Menu();
            //PigeonCms.Module currModule = new PigeonCms.Module();
            //if (base.CurrentId > 0)
            //{
            //}
            //else
            //{
            //}
            //MenuHelper.LoadListMenu(ListParentId, base.CurrentId);
        }
    }

    protected void DropCoreFilter_SelectedIndexChanged(object sender, EventArgs e)
    {
        try { GridModules.DataBind(); }
        catch (Exception ex)
        {
            LblErr.Text = ex.Message;
        }
    }

    protected void TxtNameFilter_TextChanged(object sender, EventArgs e)
    {
        try { GridModules.DataBind(); }
        catch (Exception ex)
        {
            LblErr.Text = ex.Message;
        }
    }

    protected void ObjDs1_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
    {
        var filter = new ModuleTypeFilter();

        switch (DropCoreFilter.SelectedValue)
        {
            case "1":
                filter.IsCore = Utility.TristateBool.True;
                break;
            case "0":
                filter.IsCore = Utility.TristateBool.False;
                break;
            default:
                filter.IsCore = Utility.TristateBool.NotSet;
                break;
        }

        if (!string.IsNullOrEmpty(TxtNameFilter.Text))
        {
            filter.FullNamePart = TxtNameFilter.Text;
        }

        e.InputParameters["filter"] = filter;
    }

    protected void ObjDsTemplates_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
    {
        ThemeObjFilter filter = new ThemeObjFilter();
        e.InputParameters["filter"] = filter;
    }

    protected void GridModules_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Select")
        {
            editModule(e.CommandArgument.ToString());
        }
        if (e.CommandName == "DeleteRow")
        {
            uninstallModule(e.CommandArgument.ToString());
        }
    }

    protected void GridModules_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
            Utility.AddGlyph(GridModules, e.Row);
    }

    protected void GridModules_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            PigeonCms.ModuleType item = new PigeonCms.ModuleType();
            item = (PigeonCms.ModuleType)e.Row.DataItem;


            //Literal LitName = (Literal)e.Row.FindControl("LitName");
            //if (item.ContentType == MenuContentType.Url && !string.IsNullOrEmpty(item.Link))
            //    LitName.Text = "<a href='" + item.Link + "' target='_blank' title='" + item.Link + "'>" +
            //        Utility.GetTextPreview(item.Name, 20, "") + "</a>";
            //else
            //    LitName.Text = Utility.GetTextPreview(item.Name, 20, "");

            var LnkModuleLabels = (HyperLink)e.Row.FindControl("LnkModuleLabels");
            LnkModuleLabels.NavigateUrl = this.LabelsPopupUrl
                + "?ModuleFullName=" + item.FullName;


            //Uninstall button
            ImageButton img1 = (ImageButton)e.Row.FindControl("LnkDel");
            if (item.IsCore)
                img1.Visible = false;
            else
                img1.Visible = true;
        }
    }

    protected void GridTemplates_RowCommand(object sender, GridViewCommandEventArgs e)
    {
    }

    protected void GridTemplates_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
            Utility.AddGlyph(GridModules, e.Row);
    }

    protected void GridTemplates_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

        }
    }

    protected void FileUpload1_AfterUpload(object sender, FileUploadControl.FileUploadEventArgs e)
    {
        string destFolder = "";
        if (FileUpload1.UploadedFiles.Count > 0)
        {
            using (ZipFile zip =
                ZipFile.Read(
                HttpContext.Current.Request.MapPath(
                FileUpload1.FilePath + FileUpload1.UploadedFiles[0])))
            {
                destFolder = HttpContext.Current.Request.MapPath("~/installation/tmp/" +
                    FileUpload1.UploadedFiles[0].Substring(0, FileUpload1.UploadedFiles[0].Length - 4));

                if (Directory.Exists(destFolder)) Directory.Delete(destFolder, true);

                zip.ExtractExistingFile = ExtractExistingFileAction.OverwriteSilently;
                zip.ExtractAll(
                    HttpContext.Current.Request.MapPath("~/installation/tmp/"));

            }
            InstallHelper.Install(destFolder);
        }
    }

    protected void BtnSaveModule_Click(object sender, EventArgs e)
    {
        LblErr.Text = "";
        LblOk.Text = "";

        try
        {
            //PigeonCms.Menu o1 = new PigeonCms.Menu();
            //Module currModule = new Module();
            //if (base.CurrentId == 0)
            //{

            //}
            //else
            //{

            //}
            //GridModules.DataBind();
            //LblOk.Text = Utility.GetLabel("RECORD_SAVED_MSG");
            //MultiView1.ActiveViewIndex = 0;
        }
        catch (Exception e1)
        {
            LblErr.Text = Utility.GetLabel("RECORD_ERR_MSG") + "<br />" + e1.ToString();
        }
        finally
        {
        }
    }

    protected void BtnCancelModule_Click(object sender, EventArgs e)
    {
        LblErr.Text = "";
        LblOk.Text = "";
        MultiView1.ActiveViewIndex = (int)ViewIndex.SeeModules;
    }

    protected void BtnSqlRun_Click(object sender, EventArgs e)
    {
        DbProviderFactory myProv = Database.ProviderFactory;
        DbConnection myConn = myProv.CreateConnection();
        DbDataReader myRd = null;
        DbCommand myCmd = myConn.CreateCommand();

        LitSqlResult.Text = "";
        string sqlQuery = "";    //full sql query

        try
        {
            myConn.ConnectionString = Database.ConnString;
            myConn.Open();
            myCmd.Connection = myConn;

            sqlQuery = Database.ParseSql(TxtSql.Text);
            LitSqlResult.Text = Database.ExecuteQuery(myRd, myCmd, sqlQuery);
        }
        catch (Exception ex)
        {
            LitSqlResult.Text = ex.ToString();
        }
        finally
        {
            myConn.Dispose();
        }
    }

    protected void BtnSqlCancel_Click(object sender, EventArgs e)
    {
        TxtSql.Text = "";
        LitSqlResult.Text = "";
    }

    protected void MultiView1_ActiveViewChanged(object sender, EventArgs e)
    {
        if (MultiView1.ActiveViewIndex == (int)ViewIndex.EditModule)
        { }
        else if (MultiView1.ActiveViewIndex == (int)ViewIndex.Install)
        { }
        else if (MultiView1.ActiveViewIndex == (int)ViewIndex.SeeModules)
        { }
        else if (MultiView1.ActiveViewIndex == (int)ViewIndex.SeeTemplates)
        { }
    }

    protected void LnkInstall_Click(object sender, EventArgs e)
    {
        LblErr.Text = "";
        LblOk.Text = "";
        MultiView1.ActiveViewIndex = (int)ViewIndex.Install;
    }

    protected void LnkModules_Click(object sender, EventArgs e)
    {
        LblErr.Text = "";
        LblOk.Text = "";
        MultiView1.ActiveViewIndex = (int)ViewIndex.SeeModules;
    }

    protected void LnkTemplates_Click(object sender, EventArgs e)
    {
        LblErr.Text = "";
        LblOk.Text = "";
        MultiView1.ActiveViewIndex = (int)ViewIndex.SeeTemplates;
    }

    protected void LnkSql_Click(object sender, EventArgs e)
    {
        LblErr.Text = "";
        LblOk.Text = "";
        MultiView1.ActiveViewIndex = (int)ViewIndex.Sql;
    }

    #region private methods

    private void clearForm()
    {
        LblId.Text = "";
        TxtName.Text = "";
    }

    private void form2obj(PigeonCms.Menu obj)
    {
        obj.Id = base.CurrentId;
        //obj.MenuType = DropMenuTypes.SelectedValue;
    }

    private void obj2form(PigeonCms.ModuleType obj)
    {
        LblModuleId.Text = obj.FullName;
    }

    private void editModule(string recordId)
    {
        var obj = new PigeonCms.ModuleType();
        LblOk.Text = "";
        LblErr.Text = "";

        clearForm();
        base.CurrentKey = recordId;

        obj = new ModuleTypeManager().GetByFullName(base.CurrentKey);
        obj2form(obj);

        MultiView1.ActiveViewIndex = (int)ViewIndex.EditModule;
    }

    private void uninstallModule(string recordId)
    {
        LblOk.Text = "";
        LblErr.Text = "";

        //try
        //{
        //    new MenuManager().DeleteById(recordId);
        //}
        //catch (Exception e)
        //{
        //    LblErr.Text = e.Message;
        //}
        GridModules.DataBind();
    }

    private void loadDropCoreFilter()
    {
        DropCoreFilter.Items.Clear();
        DropCoreFilter.Items.Add(new ListItem(Utility.GetLabel("LblSelectCore", "Select core state"), ""));
        DropCoreFilter.Items.Add(new ListItem("Core item", "1"));
        DropCoreFilter.Items.Add(new ListItem("Optional item", "0"));
    }

    #endregion
}
