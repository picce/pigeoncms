using System;
using System.Data;
using System.Linq;
using System.Configuration;
using System.Collections;
using System.IO;
using System.Data.OleDb;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Web.Caching;
using System.Collections.Generic;
using PigeonCms;
using System.Data.Common;
using System.Data.SqlClient;


public partial class PigeonCms_ExportData_Default : PigeonCms.BaseModuleControl
{
    private List<PigeonCms.Culture> culturesList = null;
    private List<PigeonCms.Culture> CultureList
    {
        get
        {
            if (culturesList == null)
            {
                culturesList = new List<PigeonCms.Culture>();
                var cultMan = new PigeonCms.CulturesManager();
                var cultFilter = new PigeonCms.CulturesFilter();
                cultFilter.Enabled = Utility.TristateBool.NotSet;
                //if (this.ShowOnlyEnabledCultures)
                cultFilter.Enabled = Utility.TristateBool.True;

                culturesList = cultMan.GetByFilter(cultFilter, "");
            }
            return culturesList;
        }
    }

    private AppSettingsProvider exportProvider = new PigeonCms.AppSettingsProvider("PigeonCms.Export");

    private string Resources
    {
        get 
        {
            return exportProvider.GetValue("Resources");
        }
    }

    private enum MissingFilterEnum
    {
        AllValues = 0,
        MissingValues = 1
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        LblOk.Text = RenderSuccess("");
        LblErr.Text = RenderError("");

        if (!Page.IsPostBack)
        {
            loadDropMissingFilter();
            loadDropResourceFilter();
            loadGrid();
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
            //var item = (UserTempData)e.Row.DataItem;
            //var LitCol01 = (Literal)e.Row.FindControl("LitCol01");
            //LitCol01.Text = HttpUtility.HtmlEncode(Utility.Html.GetTextPreview(item.Columns[0], TXT_PREVIEW_LEN, "", false));
        }
    }

    protected void Grid1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        Grid1.PageIndex = e.NewPageIndex;
        loadGrid();
    }

    protected void BtnExport_Click(object sender, EventArgs e)
    {
        LblOk.Text = "";
        LblErr.Text = "";

        try
        {
            var dt = getDataTable(DropResourceFilter.SelectedValue);
            var columns = new List<string>();
            foreach (DataColumn col in dt.DefaultView.Table.Columns)
            {
                columns.Add(col.ColumnName);
            }

            var exportHelper = new PigeonCms.ExportHelper();
            string filename = DropResourceFilter.SelectedValue + "_" + DateTime.Now.ToString("yyyyMMdd-HHmmss");
            string file = exportHelper.GridToExcel(Grid1, filename, true, columns);

            if (!string.IsNullOrEmpty(file))
                LblOk.Text = RenderSuccess("File " + file + " exported");
            else
                LblErr.Text = RenderError("No rows to export");
        }
        catch (Exception ex)
        {
            LblErr.Text = RenderError("Error during export procedure. Check log errors");

            LogProvider.Write(this.BaseModule,
                "BtnExport_Click>Error during export procedure. Ex:" + ex.ToString(),
                TracerItemType.Error);
        }

    }

    protected void Filter_Changed(object sender, EventArgs e)
    {
        loadGrid();
    }

    private void loadDropResourceFilter()
    {
        var drop = DropResourceFilter;
        var list = Utility.String2List(this.Resources, "|");
        foreach (var item in list)
        {
            string value = Utility.String2List(item, ",")[0];
            string text = Utility.String2List(item, ",")[1];
            
            drop.Items.Add(new ListItem(text.Trim(), value.Trim()));
        }
    }

    private void loadDropMissingFilter()
    {
        var drop = DropMissingFilter;
        drop.Items.Clear();

        drop.Items.Add(
            new ListItem(
                base.GetLabel("FilterAll", "All values"),
                ((int)MissingFilterEnum.AllValues).ToString()
            )
        );

        drop.Items.Add(
            new ListItem(
                base.GetLabel("FilterMissingValues", "Only with missing values"),
                ((int)MissingFilterEnum.MissingValues).ToString()
            )
        );
    }

    private void loadGrid()
    {
        try
        {
            var dt = getDataTable(DropResourceFilter.SelectedValue);
            Grid1.DataSource = dt.DefaultView;
            Grid1.DataBind();
        }
        catch (Exception ex)
        {
            LblErr.Text = RenderError(ex.Message);            
        }
    }

    private DataTable getDataTable(string resource)
    {
        DataTable dt = new DataTable();
        DbProviderFactory myProv = Database.ProviderFactory;
        DbConnection myConn = myProv.CreateConnection();
        DbCommand myCmd = myConn.CreateCommand();
        string sSql;
        DbDataAdapter ad = myProv.CreateDataAdapter();
        const string FILTERS_OPERATOR = "AND";

        try
        {
            myConn.ConnectionString = Database.ConnString;
            myConn.Open();
            myCmd.Connection = myConn;

            sSql = resource;

            myCmd.CommandText = Database.ParseSql(sSql);
            ad.SelectCommand = myCmd;
            ad.SelectCommand.CommandType = CommandType.StoredProcedure;
            ad.Fill(dt);

            //dt = dt.AsEnumerable()
            //    .Where(r => r.Field<string>("it-IT").Contains("DE"))
            //    .CopyToDataTable();

            string sqlFilter = "";

            var missingValuesFilter = (MissingFilterEnum)int.Parse(DropMissingFilter.SelectedValue);
            if (missingValuesFilter == MissingFilterEnum.MissingValues)
            {
                //add filter to check for missing values in all columns
                sqlFilter += "(";
                foreach (DataColumn col in dt.DefaultView.Table.Columns)
                {
                    if (col.DataType.Name == "String")
                        sqlFilter += "[" + col.ColumnName + "] = '' OR ";
                }
                sqlFilter += " 1=0)";
            }

            if (TxtValuesStartsWithFilter.Text.Length > 0)
            {
                //add filter to check values starting with in all columns
                if (!string.IsNullOrEmpty(sqlFilter))
                    sqlFilter += " " + FILTERS_OPERATOR + " ";

                sqlFilter += "(";
                foreach (DataColumn col in dt.DefaultView.Table.Columns)
                {
                    if (col.DataType.Name == "String")
                        sqlFilter += "[" + col.ColumnName + "] like '" + TxtValuesStartsWithFilter.Text + "%' OR ";
                }
                sqlFilter += " 1=0)";
            }

            if (TxtValuesContainsFilter.Text.Length > 0)
            {
                //add filter to check values starting with in all columns
                if (!string.IsNullOrEmpty(sqlFilter))
                    sqlFilter += " " + FILTERS_OPERATOR + " ";

                sqlFilter += "(";
                foreach (DataColumn col in dt.DefaultView.Table.Columns)
                {
                    if (col.DataType.Name == "String")
                        sqlFilter += "[" + col.ColumnName + "] like '%" + TxtValuesContainsFilter.Text + "%' OR ";
                }
                sqlFilter += " 1=0)";
            }

            //http://stackoverflow.com/questions/7815916/how-to-handle-copytodatatable-when-no-rows
            var rows = dt.Select(sqlFilter);
            dt = rows.Any() ? rows.CopyToDataTable() : dt.Clone();

        }
        finally
        {
            myConn.Dispose();
        }
        return dt;
    }
}
