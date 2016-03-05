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
using System.Collections.Generic;
using PigeonCms;
using System.Text;
using System.IO;
using System.Data.SqlClient;
using System.Data.Common;
using System.Web.Configuration;

public partial class Installation_Default : Page
{
    static Random random = new Random();
    
    public static string ServerVariables = "";
    public static string FilesPermissionsList = "";
    public static string StepsList = "";

    const int ViewSystemIndex = 0;
    const int ViewDatabaseIndex = 1;
    const int ViewSiteIndex = 2;
    const int ViewSummaryIndex = 3;
    const int ViewFinishIndex = 4;


    protected void Page_Init(object sender, EventArgs e)
    {
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        //file to rename once installation completed
        string installFileName = Config.InstallationPath + "Install.txt";
        installFileName = HttpContext.Current.Request.MapPath(installFileName);
        if (!File.Exists(installFileName))
            Response.Redirect(Config.SessionTimeOutUrl, true);

        if (!Page.IsPostBack)
        {
            loadSystemInfo();
            loadStepsList();
        }
        else
        {
        }
    }

    protected void RadioSqlversion_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (RadioSqlversion.SelectedValue == "express")
            TxtHostName.Text = ".\\SQLEXPRESS";
        else
            TxtHostName.Text = "(local)";
    }

    protected void BtnNext_Click(object sender, EventArgs e)
    {
        LblErr.Text = "";
        LblOk.Text = "";
        
        try
        {
            if (processView())
            {
                if (MultiView1.ActiveViewIndex < MultiView1.Views.Count - 1)
                    MultiView1.ActiveViewIndex++;
            }
        }
        catch (Exception e1)
        {
            LblErr.Text = renderError("an error occoured<br />" + e1.ToString());
        }
        finally
        {
        }
    }

    protected void BtnPrevious_Click(object sender, EventArgs e)
    {
        LblErr.Text = "";
        LblOk.Text = "";

        if (MultiView1.ActiveViewIndex > 0)
            MultiView1.ActiveViewIndex--;
    }

    protected void MultiView1_ActiveViewChanged(object sender, EventArgs e)
    {
        loadStepsList();

        switch (MultiView1.ActiveViewIndex)
        {
            case ViewSystemIndex:
                BtnNext.Text = "Next";
                break;

            case ViewDatabaseIndex:
                BtnNext.Text = "Next";
                break;

            case ViewSiteIndex:
                BtnNext.Text = "Next";
                break;

            case ViewSummaryIndex:
                BtnNext.Text = "Finish";
                break;

            case ViewFinishIndex:
                BtnNext.Visible = false;
                BtnPrevious.Visible = false;
                break;

            default:
                break;
        }
            
    }

    #region private methods

    private string renderError(string content)
    {
        const string HTML = @"<div class='alert alert-danger alert-dismissable'>
        <button type='button' class='close' data-dismiss='alert' aria-hidden='true'>×</button>
        [[content]]
        </div>";

        string res = "";
        if (!string.IsNullOrEmpty(content))
        {
            res = HTML.Replace("[[content]]", content);
        }
        return res;
    }

    private string getConnString()
    {
        string res = "Data Source=" + TxtHostName.Text + ";" +
            "Initial Catalog=" + TxtDbName.Text + ";Persist Security Info=True;" +
            "User ID=" + TxtUserId.Text + ";Pwd=" + TxtPassword.Text;
        return res;
    }

    private bool loadSystemInfo()
    {
        bool res = true;

        ServerVariables = "";
        ServerVariables += ".NET version: "+ System.Environment.Version.ToString()  + "<br />";

        FilesPermissionsList = "<ul>";
        if (checkWebConfig())
            FilesPermissionsList += "<li>web.config permissions: OK</li>";
        else
        {
            //res = false; -->allow next anyway
            FilesPermissionsList += @"<li>web.config permissions: 
                <span class='error'>KO</span> set write permissions to web.config 
                or if you are in dev mode run visual studio as admin.
                However you can manually update web.config file once the installation is completed.</li>";
        }
        FilesPermissionsList += "</ul>";

        TxtEncryptKey.Text = generateEncryptKey();

        return res;
    }


    private bool checkWebConfig()
    {
        bool res = true;
        try
        {
            Configuration configuration =
            WebConfigurationManager.OpenWebConfiguration(HttpContext.Current.Request.ApplicationPath);
        }
        catch (Exception)
        {
            res = false;
        }
        return res;
    }

    private void loadStepsList()
    {
        StepsList = "";
        StepsList += renderStep("System check", 0);
        StepsList += renderStep("Database settings", 1);
        StepsList += renderStep("Site info", 2);
        StepsList += renderStep("Settings summary", 3);
        StepsList += renderStep("Finish", 4);
    }

    private string renderStep(string title, int index)
    {
        string cssClass = "";
        string iconClass = "fa-question";
        //string invertedClass = (index % 2 == 0 ? "" : "timeline-inverted");
		string invertedClass = ""; //NEW

        if (index == MultiView1.ActiveViewIndex)
            cssClass = "warning";
        else if (index < MultiView1.ActiveViewIndex)
        {
            cssClass = "success";
            iconClass = "fa-check";
        }

        if (ViewFinishIndex == MultiView1.ActiveViewIndex)
        {
            cssClass = "success";
            iconClass = "fa-check";
        }

        const string ROW = @"
        <li class='[[invertedClass]]'>
            <div class='timeline-badge [[cssClass]]'>
                <i class='fa [[iconClass]]'></i>
            </div>
            <div class='timeline-panel'>
                <div class='timeline-heading'>
                    <h4 class='timeline-title'>[[title]]</h4>
                </div>
            </div>
        </li>
        ";

        return ROW
            .Replace("[[title]]", title)
            .Replace("[[invertedClass]]", invertedClass)
            .Replace("[[iconClass]]", iconClass)
            .Replace("[[cssClass]]", cssClass);
    }

    private bool processView()
    {
        bool res = true;

        switch (MultiView1.ActiveViewIndex)
        {
            case ViewSystemIndex:
                res = loadSystemInfo();
                break;

            case ViewDatabaseIndex:
                break;

            case ViewSiteIndex:
                res = parseData();
                break;

            case ViewSummaryIndex:
                res = setData();
                break;

            case ViewFinishIndex:
                break;

            default:
                break;
        }

        return res;
    }

    private bool parseData()
    {
        DbProviderFactory myProv = Database.ProviderFactory;
        DbConnection myConn = myProv.CreateConnection();
        DbCommand myCmd = myConn.CreateCommand();

        bool res = true;

        LitConnString.Text = "";
        LitTablesPrefix.Text = TxtTablesPrefix.Text;
        LitBackupTables.Text = ChkBackupTables.Checked.ToString();
        LitInstallExampleData.Text = ChkExampleData.Checked.ToString();
        LitSiteTitle.Text = TxtSiteTitle.Text;
        LitEncryptKey.Text = TxtEncryptKey.Text;
        LitSiteUrl.Text = Utility.GetAbsoluteUrl();
        LitAdminPassword.Text = TxtAdminPassword.Text;
        LitEmail.Text = TxtEmail.Text;

        //check password
        if (TxtAdminPassword.Text.Length < 8)
        {
            res = false;
            LblErr.Text += "admin password must be at least 8 chars long<br />";
        }

        //check encrypt key
        if (TxtEncryptKey.Text.Length < 8)
        {
            res = false;
            LblErr.Text += "encrypt key must be at least 8 chars long<br />";
        }

        //check connection string
        try
        {
            myConn.ConnectionString = getConnString();
            myConn.Open();
            LitConnString.Text = getConnString();
        }
        catch (SqlException ex)
        {
            res = false;
            LblErr.Text += "invalid connection string<br />";
            LblErr.Text += ex.Message + "<br />";
        }
        finally
        {
            myConn.Dispose();
        }

        //check sql installation files
        string filePath = HttpContext.Current.Request.MapPath(Config.InstallationPath + "sql/");
        if (!File.Exists(filePath + "create.sql"))
        {
            res = false;
            LblErr.Text += "create.sql file not found<br />";
        }
        if (!File.Exists(filePath + "bulk.sql"))
        {
            res = false;
            LblErr.Text += "bulk.sql file not found<br />";
        }

        LblErr.Text = renderError(LblErr.Text);

        return res;
    }

    private bool setData()
    {
        DbProviderFactory myProv = Database.ProviderFactory;
        DbTransaction myTrans = null;
        DbConnection myConn = myProv.CreateConnection();
        DbCommand myCmd = myConn.CreateCommand();
        DbDataReader myRd = null;
        string sSql;
        string sResult;
        bool res = true;

        try
        {
            myConn.ConnectionString = getConnString();
            myConn.Open();
            myCmd.Connection = myConn;
            myTrans = myConn.BeginTransaction();
            myCmd.Transaction = myTrans;

            //tables structure creation
            TextReader tr = new StreamReader(
                HttpContext.Current.Request.MapPath(Config.InstallationPath + "sql/create.sql"));
            sSql = tr.ReadToEnd();
            tr.Close();
            sSql = Database.ParseSql(sSql, TxtTablesPrefix.Text);
            sResult = Database.ExecuteQuery(myRd, myCmd, sSql);

            //bulk data
            tr = new StreamReader(
                HttpContext.Current.Request.MapPath(Config.InstallationPath + "sql/bulk.sql"));
            sSql = tr.ReadToEnd();
            tr.Close();
            sSql = Database.ParseSql(sSql, TxtTablesPrefix.Text);
            sResult = Database.ExecuteQuery(myRd, myCmd, sSql);

            myTrans.Commit();
        }
        catch (SqlException ex)
        {
            res = false;
            myTrans.Rollback();
            LblErr.Text += "error in sql query<br />";
            LblErr.Text += ex.Message + "<br />";
        }
        finally
        {
            myTrans.Dispose();
            myConn.Dispose();
        }

        //set custom data with direct sql because web.config settings reload at next request
        if (res)
        {
            try
            {
                myConn.ConnectionString = getConnString();
                myConn.Open();
                myCmd.Connection = myConn;
                myTrans = myConn.BeginTransaction();
                myCmd.Transaction = myTrans;

                sSql = "UPDATE #__memberUsers SET password=@password " +
                " WHERE username = @username";
                myCmd.CommandText = Database.ParseSql(sSql, TxtTablesPrefix.Text);
                myCmd.Parameters.Add(Database.Parameter(myProv, "username", "admin"));
                myCmd.Parameters.Add(Database.Parameter(myProv, "password", TxtAdminPassword.Text));
                myCmd.ExecuteNonQuery();

                myCmd.Parameters.Clear();
                sSql = "UPDATE #__appSettings SET KeyValue=@KeyValue " +
                " WHERE  KeyName=@KeyName ";
                myCmd.CommandText = Database.ParseSql(sSql, TxtTablesPrefix.Text);
                myCmd.Parameters.Add(Database.Parameter(myProv, "KeyName", "MetaSiteTitle"));
                myCmd.Parameters.Add(Database.Parameter(myProv, "KeyValue", TxtSiteTitle.Text));
                myCmd.ExecuteNonQuery();

                myCmd.Parameters.Clear();
                sSql = "UPDATE #__appSettings SET KeyValue=@KeyValue " +
                " WHERE  KeyName=@KeyName ";
                myCmd.CommandText = Database.ParseSql(sSql, TxtTablesPrefix.Text);
                myCmd.Parameters.Add(Database.Parameter(myProv, "KeyName", "EmailSender"));
                myCmd.Parameters.Add(Database.Parameter(myProv, "KeyValue", TxtEmail.Text));
                myCmd.ExecuteNonQuery();

                myTrans.Commit();
            }
            catch (SqlException ex)
            {
                res = false;
                myTrans.Rollback();
                LblErr.Text += "error setting custom data<br />";
                LblErr.Text += ex.Message + "<br />";
            }
            finally
            {
                myTrans.Dispose();
                myConn.Dispose();
            }

            //apply routes
            try
            {
                new MvcRoutesManager().SetAppRoutes();
            }
            catch (Exception ex)
            {
                LblErr.Text += "error setting application default routes<br />";
                LblErr.Text += ex.Message + "<br />";
            }

            //update web.config
            if (res)
            {
                try
                {
                    Configuration configuration =
                        WebConfigurationManager.OpenWebConfiguration(HttpContext.Current.Request.ApplicationPath);
                    AppSettingsSection appSettingsSection = (AppSettingsSection)configuration.GetSection("appSettings");
                    if (appSettingsSection != null)
                    {
                        appSettingsSection.Settings["ConnectionStringName"].Value = TxtConnectionName.Text;
                        appSettingsSection.Settings["TabPrefix"].Value = TxtTablesPrefix.Text;
                        appSettingsSection.Settings["EncryptKey"].Value = TxtEncryptKey.Text;
                        //appSettingsSection.Settings["CultureDefault"].Value = TxtCultureDefault.Text;
                    }
                    ConnectionStringsSection connectionStringsSection = (ConnectionStringsSection)configuration.GetSection("connectionStrings");
                    if (connectionStringsSection != null)
                    {
                        ConnectionStringSettings setting =
                            new ConnectionStringSettings(TxtConnectionName.Text, getConnString(), "System.Data.SqlClient");
                        if (connectionStringsSection.ConnectionStrings[TxtConnectionName.Text] != null)
                            connectionStringsSection.ConnectionStrings[TxtConnectionName.Text].ConnectionString = getConnString();
                        else
                            connectionStringsSection.ConnectionStrings.Add(setting);
                    }

                    MembershipSection membershipSection = (MembershipSection)configuration.GetSectionGroup("system.web").Sections["membership"];
                    if (membershipSection != null)
                    {
                        membershipSection.Providers["PgnUserProvider"].Parameters["connectionStringName"] = TxtConnectionName.Text;
                    }

                    RoleManagerSection roleManagerSection = (RoleManagerSection)configuration.GetSectionGroup("system.web").Sections["roleManager"];
                    if (roleManagerSection != null)
                    {
                        roleManagerSection.Providers["PgnRoleProvider"].Parameters["connectionStringName"] = TxtConnectionName.Text;
                    }

                    configuration.Save();
                    ConfigurationManager.RefreshSection("appSettings");
                    ConfigurationManager.RefreshSection("connectionStrings");
                    ConfigurationManager.RefreshSection("membership");
                    ConfigurationManager.RefreshSection("roleManager");
                }
                catch (Exception ex)
                {
                    res = false;
                    LblErr.Text += "Error updating 'web.config' file. Please edit 'Conn1' connection string manually and upload this file again.<br />";
                    LblErr.Text += ex.Message + "<br />";
                }
            }

            //rename install.txt file
            try
            {
                string installFileName = Config.InstallationPath + "Install.txt";
                string renamedInstallFileName = Config.InstallationPath + "Install_completed.txt";
                installFileName = HttpContext.Current.Request.MapPath(installFileName);
                renamedInstallFileName = HttpContext.Current.Request.MapPath(renamedInstallFileName);
                if (File.Exists(installFileName))
                    File.Move(installFileName, renamedInstallFileName);
            }
            catch (Exception ex)
            {
                LblErr.Text += "Error renaming 'install.txt' file, please rename manually in '/pgn-admin/installation/Install_completed.txt'.<br />";
                LblErr.Text += ex.Message + "<br />";
            }
        }

        LblErr.Text = renderError(LblErr.Text);

        return res;
    }

    public string generateEncryptKey()
    {
        StringBuilder builder = new StringBuilder();
        builder.Append(randomNumber(random, 10, 99));
        builder.Append(randomString(random, 3));
        builder.Append(randomNumber(random, 10, 99));
        builder.Append(randomString(random, 3));

        return builder.ToString();
    }

    private int randomNumber(Random random, int min, int max)
    {
        return random.Next(min, max);
    }

    private string randomString(Random random, int size)
    {
        var builder = new StringBuilder();
        char ch;
        for (int i = 0; i < size; i++)
        {
            ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
            builder.Append(ch);
        }
        return builder.ToString().ToUpper();
    }

    #endregion
}
