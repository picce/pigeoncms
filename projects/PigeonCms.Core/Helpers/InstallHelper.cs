using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Reflection;
using PigeonCms;
using System.IO;
using System.Data.Common;

namespace PigeonCms
{
    /// <summary>
    /// static functions to manage installation and updates
    /// </summary>
    public static class InstallHelper
    {
        /// <summary>
        /// install additional components (modules, themes, etc)
        /// </summary>
        /// <param name="sourceFolder">
        /// absolute path of source folder ex:c:\..\..\..\installation\tmp\modulename
        /// with unpacked installation files and folders
        /// </param>
        public static void Install(string sourceFolder)
        {
            DbProviderFactory myProv = Database.ProviderFactory;
            DbTransaction myTrans = null;
            DbConnection myConn = myProv.CreateConnection();
            DbDataReader myRd = null;
            DbCommand myCmd = myConn.CreateCommand();
            string sqlQuery = ""; ;

            ModuleType moduleType = new ModuleType();

            try
            {
                moduleType = new ModuleTypeManager().GetByPath(sourceFolder, "install.xml");

                //execute sql install
                if (moduleType.InstallQueries.Count > 0)
                {
                    try
                    {
                        myConn.ConnectionString = Database.ConnString;
                        myConn.Open();
                        myCmd.Connection = myConn;
                        myTrans = myConn.BeginTransaction();
                        myCmd.Transaction = myTrans;

                        foreach (string query in moduleType.InstallQueries)
                        {
                            sqlQuery += Database.ParseSql(query);
                        }
                        Database.ExecuteQuery(myRd, myCmd, sqlQuery);
                    }
                    catch (Exception ex)
                    {
                        myTrans.Rollback();
                        throw ex;
                    }
                    finally
                    {
                        myTrans.Dispose();
                        myConn.Dispose();
                    }
                }

                string currFolder;
                string destFolder;
                //code folder
                currFolder = Path.Combine(sourceFolder, "_install\\code");
                destFolder = HttpContext.Current.Request.MapPath("~/App_Code/modules/"+ moduleType.FullName);
                if (Directory.Exists(currFolder))
                    Utility.CopyFolder(currFolder, destFolder);
                //admin folder
                currFolder = Path.Combine(sourceFolder, "_install\\admin");
                destFolder = HttpContext.Current.Request.MapPath("~/Admin/" + moduleType.FullName);
                if (Directory.Exists(currFolder))
                    Utility.CopyFolder(currFolder, destFolder);
                //module folders
                currFolder = Path.Combine(sourceFolder, "");
                destFolder = HttpContext.Current.Request.MapPath("~/Modules/" + moduleType.FullName);
                if (Directory.Exists(currFolder))
                    Utility.CopyFolder(currFolder, destFolder);
            }
            catch (Exception e)
            {
                PigeonCms.Tracer.Log("Error installing " + sourceFolder + ": " + e.ToString(), TracerItemType.Error);
                throw e;
            }
            finally
            {

            }
        }
    }
}
