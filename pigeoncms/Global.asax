<%@ Application Language="C#" %>
<%@ Import Namespace="System.Web.Routing" %>
<%@ Import Namespace="System.IO" %>
<%@ Import Namespace="PigeonCms" %>
<%@ Import Namespace="PigeonCms.Core.Offline" %>
<%@ Import Namespace="System.Reflection" %>

<script runat="server">
    
    void Application_Start(object sender, EventArgs e) 
    {
        try
        {
            PigeonCms.AppSettingsManager.RefreshApplicationVars();
            new PigeonCms.MvcRoutesManager().SetAppRoutes();
            OfflineProvider.ResetOfflineStatus();
            fixAppDomainRestartWhenTouchingFiles();
        }
        catch { }
        finally { }
    }
    
    void Application_End(object sender, EventArgs e) 
    {}

    void Application_OnBeginRequest(object sender, EventArgs e)
    {}   
        
    void Application_Error(object sender, EventArgs e) 
    { 
        //untrapped errors handler
        Exception objErr = Server.GetLastError();
        
        string url = "";
        HttpContext c = HttpContext.Current;
        if (c != null)
            url = c.Request.Url.ToString();

        string message = "";
        string stack = "";
        if (objErr != null)
        {
            message = objErr.Message.ToString();
            stack = Server.GetLastError().ToString();
        }
        
        string err = Environment.NewLine
            + "untrapped error occured " + DateTime.Now.ToString() + Environment.NewLine
            + "Error in: " + url + Environment.NewLine
            + "Error message: " + message + Environment.NewLine
            + "Stack trace: " + stack + Environment.NewLine;
        //Server.ClearError();

        string logFileName = "~/Logs/errors.txt";
        logFileName = HttpContext.Current.Request.MapPath(logFileName);

        try
        {
            //append log only if file exist; 
            //manually delete file to disable log
            //manually create file to enable log            
            if (File.Exists(logFileName))
            {
                TextWriter tw = new StreamWriter(logFileName, true);
                tw.WriteLine(err);
                tw.Close();
            }
        }
        catch (DirectoryNotFoundException)
        {
            Directory.CreateDirectory(
                HttpContext.Current.Request.MapPath("~/Logs"));
        }
        catch (Exception) { }
        //System.Diagnostics.EventLog.WriteEntry("PigeonCms.Application_Error", err, System.Diagnostics.EventLogEntryType.Error);
    }

    void Session_Start(object sender, EventArgs e) 
    {}

    void Session_End(object sender, EventArgs e) 
    {}

    private void fixAppDomainRestartWhenTouchingFiles()
    {
        
        if (true /*CurrentTrustLevel == AspNetHostingPermissionLevel.Unrestricted*/)
        {
            // From: http://forums.asp.net/p/1310976/2581558.aspx
            // FIX disable AppDomain restart when deleting subdirectory
            // This code will turn off monitoring from the root website directory.
            // Monitoring of Bin, App_Themes and other folders will still be operational, so updated DLLs will still auto deploy.
            PropertyInfo p = typeof(HttpRuntime).GetProperty("FileChangesMonitor", BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static);
            object o = p.GetValue(null, null);
            FieldInfo f = o.GetType().GetField("_dirMonSubdirs", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.IgnoreCase);
            object monitor = f.GetValue(o);
            MethodInfo m = monitor.GetType().GetMethod("StopMonitoring", BindingFlags.Instance | BindingFlags.NonPublic);
            m.Invoke(monitor, new object[] { });
        }
    }
</script>
