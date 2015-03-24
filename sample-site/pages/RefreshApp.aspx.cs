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

public partial class RefreshApp : Page
{
    protected string LitErr = "";
    protected string LitSuccess = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            string action = "";
            if (Request.QueryString["action"] != null)
            {
                action = Request.QueryString["action"].ToString().ToLower();
            }

            switch (action)
            {
                case "vars":
                    PigeonCms.AppSettingsManager.RefreshApplicationVars();

                    //new PigeonCms.Core.Helpers.CacheManager<object>("Labels").Clear();
                    LabelsProvider.ClearCacheByResourceSet("Labels");

                    new PigeonCms.MvcRoutesManager().SetAppRoutes();
                    LitSuccess = "ApplicationVars refreshed;<br />"
                               + "AppRoutes refreshed";
                    break;

                case "resetofflinestatus":
                    PigeonCms.Core.Offline.OfflineProvider.ResetOfflineStatus();
                    LitSuccess = "Offline status var refreshed;";
                    break;

                default:
                    LitErr = "Choose an action";
                    break;
            }
        }
        catch(Exception ex)
        {
            LitErr = ex.ToString();
        }
        finally { }
    }
}
