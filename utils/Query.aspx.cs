using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data.Common;
using PigeonCms;
using System.IO;

public partial class Query : Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void BtnRun_Click(object sender, EventArgs e)
    {
        LitResult.Text = "";

        DbProviderFactory myProv = Database.ProviderFactory;
        DbConnection myConn = myProv.CreateConnection();
        DbDataReader myRd = null;
        DbCommand myCmd = myConn.CreateCommand();
        string sqlQuery = "";    //full sql query

        try
        {
            myConn.ConnectionString = TxtConnString.Text; //Database.ConnString;
            myConn.Open();
            myCmd.Connection = myConn;

            sqlQuery = Database.ParseSql(TxtSql.Text, TxtTabPrefix.Text);
            LitResult.Text = Database.ExecuteQuery(myRd, myCmd, sqlQuery);
        }
        catch (Exception ex)
        {
            LitResult.Text = ex.ToString();
        }
        finally
        {
            myConn.Dispose();
        }
    }

    protected void BtnCancel_Click(object sender, EventArgs e)
    {
        TxtSql.Text = "";
        LitResult.Text = "";
    }
}
