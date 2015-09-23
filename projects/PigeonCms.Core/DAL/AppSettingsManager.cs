using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.ComponentModel;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Data.Common;


namespace PigeonCms
{
    /// <summary>
    /// Data Access Layer for AppSetting class
    /// </summary>
    [DataObject()]
    [Obsolete("don't use static class. Use new AppSettingsManager by resourceSet")]
    public static class AppSettingsManager
    {
        const string KEYSET = "PigeonCms.Core";


        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public static List<AppSetting> GetSettings()
        {
            DbProviderFactory myProv = Database.ProviderFactory;
            DbConnection myConn = myProv.CreateConnection();
            DbDataReader myRd = null;
            DbCommand myCmd = myConn.CreateCommand();
            string sSql;
            List<AppSetting> result = new List<AppSetting>();

            try
            {
                myConn.ConnectionString = Database.ConnString;
                myConn.Open();
                myCmd.Connection = myConn;

                sSql = "SELECT KeyName FROM #__appSettings ORDER BY keyName ";
                myCmd.CommandText = Database.ParseSql(sSql);
                myRd = myCmd.ExecuteReader();
                while (myRd.Read())
                {
                    AppSetting item = GetSettingByKey((string)myRd["KeyName"]);
                    result.Add(item);
                }
                myRd.Close();
            }
            finally
            {
                myConn.Dispose();
            }
            return result;
        }

        /// <summary>
        /// retrieve AppSetting Value
        /// </summary>
        /// <param name="keyName"></param>
        /// <returns></returns>
        public static string GetValue(string keyName, string defaultValue = "")
        {
            string result = "";
            Object obj = HttpContext.Current.Application[keyName];
            if (obj == null)
            {
                AppSetting appSetting = new AppSetting();
                appSetting = AppSettingsManager.GetSettingByKey(keyName);
                refreshApplicationVar(appSetting.KeyName, appSetting.KeyValue);
                result = appSetting.KeyValue;
                if (string.IsNullOrEmpty(result))
                {
                    PigeonCms.Tracer.Log("Missing AppSetting '" + keyName + "'", TracerItemType.Error);
                }
            }
            else
            {
                result = obj.ToString();
            }

            if (string.IsNullOrEmpty(result))
                result = defaultValue;

            return result;
        }

        /// <summary>
        /// retrieve AppSetting from database
        /// </summary>
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static AppSetting GetSettingByKey(string keyName)
        {
            DbProviderFactory myProv = Database.ProviderFactory;
            DbConnection myConn = myProv.CreateConnection();
            DbDataReader myRd = null;
            DbCommand myCmd = myConn.CreateCommand();
            string sSql;
            AppSetting result = new AppSetting();

            try
            {
                myConn.ConnectionString = Database.ConnString;
                myConn.Open();
                myCmd.Connection = myConn;

                sSql = "SELECT KeySet, KeyName, KeyTitle, KeyValue, KeyInfo "
                + " FROM #__appSettings m "
                + " WHERE KeySet = @KeySet AND KeyName = @KeyName ";
                myCmd.CommandText = Database.ParseSql(sSql);
                myCmd.Parameters.Add(Database.Parameter(myProv, "KeySet", KEYSET));
                myCmd.Parameters.Add(Database.Parameter(myProv, "KeyName", keyName));
                myRd = myCmd.ExecuteReader();
                if (myRd.Read())
                {
                    if (!Convert.IsDBNull(myRd["KeySet"]))
                        result.KeySet = (string)myRd["KeySet"];
                    if (!Convert.IsDBNull(myRd["KeyName"]))
                        result.KeyName = (string)myRd["KeyName"];
                    if (!Convert.IsDBNull(myRd["KeyTitle"]))
                        result.KeyTitle = (string)myRd["KeyTitle"];
                    if (!Convert.IsDBNull(myRd["KeyValue"]))
                        result.KeyValue = (string)myRd["KeyValue"];
                    if (!Convert.IsDBNull(myRd["KeyInfo"]))
                        result.KeyInfo = (string)myRd["KeyInfo"];
                }
                myRd.Close();
            }
            finally
            {
                myConn.Dispose();
            }
            return result;
        }

        /// <summary>
        /// Update an existing AppSetting
        /// </summary>
        /// <param name="theObj"></param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Update, false)]
        public static int Update(AppSetting theObj)
        {
            DbProviderFactory myProv = Database.ProviderFactory;
            DbConnection myConn = myProv.CreateConnection();
            DbCommand myCmd = myConn.CreateCommand();
            string sSql;
            int result = 0;

            try
            {
                myConn.ConnectionString = Database.ConnString;
                myConn.Open();
                myCmd.Connection = myConn;

                sSql = "UPDATE #__appSettings SET KeyTitle=@KeyTitle, KeyValue=@KeyValue, KeyInfo=@KeyInfo "
                + " WHERE KeySet=@KeySet AND KeyName=@KeyName ";
                myCmd.CommandText = Database.ParseSql(sSql);
                myCmd.Parameters.Add(Database.Parameter(myProv, "KeySet", theObj.KeySet));
                myCmd.Parameters.Add(Database.Parameter(myProv, "KeyName", theObj.KeyName));
                myCmd.Parameters.Add(Database.Parameter(myProv, "KeyTitle", theObj.KeyTitle));
                myCmd.Parameters.Add(Database.Parameter(myProv, "KeyValue", theObj.KeyValue));
                myCmd.Parameters.Add(Database.Parameter(myProv, "KeyInfo", theObj.KeyInfo));
                result = myCmd.ExecuteNonQuery();

                refreshApplicationVar(theObj.KeyName, theObj.KeyValue);
            }
            finally
            {
                myConn.Dispose();
            }
            return result;
        }

        /// <summary>
        /// Insert a new AppSetting in AppSettings table
        /// </summary>
        /// <param name="newObj">The info about new page</param>
        /// <returns>The new AppSetting object</returns>
        [DataObjectMethod(DataObjectMethodType.Insert, false)]
        public static AppSetting Insert(AppSetting newObj)
        {
            DbProviderFactory myProv = Database.ProviderFactory;
            DbConnection myConn = myProv.CreateConnection();
            DbCommand myCmd = myConn.CreateCommand();
            string sSql;
            AppSetting result = new AppSetting();

            try
            {
                myConn.ConnectionString = Database.ConnString;
                myConn.Open();
                myCmd.Connection = myConn;

                result.KeyName = newObj.KeyName;
                result.KeyTitle = newObj.KeyTitle;
                result.KeyValue = newObj.KeyValue;
                result.KeyInfo = newObj.KeyInfo;

                sSql = "INSERT INTO #__AppSettings(KeySet, KeyName, KeyTitle, KeyValue, KeyInfo) "
                + "VALUES(@KeySet, @KeyName, @KeyTitle, @KeyValue, @KeyInfo) ";
                myCmd.CommandText = Database.ParseSql(sSql);
                myCmd.Parameters.Add(Database.Parameter(myProv, "KeySet", result.KeySet));
                myCmd.Parameters.Add(Database.Parameter(myProv, "KeyName", result.KeyName));
                myCmd.Parameters.Add(Database.Parameter(myProv, "KeyTitle", result.KeyTitle));
                myCmd.Parameters.Add(Database.Parameter(myProv, "KeyValue", result.KeyValue));
                myCmd.Parameters.Add(Database.Parameter(myProv, "KeyInfo", result.KeyInfo));
                myCmd.ExecuteNonQuery();

                refreshApplicationVar(result.KeyName, result.KeyValue);
            }
            finally
            {
                myConn.Dispose();
            }
            return result;
        }

        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public static int Delete(string keyName)
        {
            DbProviderFactory myProv = Database.ProviderFactory;
            DbConnection myConn = myProv.CreateConnection();
            DbCommand myCmd = myConn.CreateCommand();
            string sSql;
            int res = 0;

            try
            {
                myConn.ConnectionString = Database.ConnString;
                myConn.Open();
                myCmd.Connection = myConn;


                sSql = "DELETE FROM #__AppSettings WHERE @KeySet = KeySet AND KeyName = @KeyName ";
                myCmd.CommandText = Database.ParseSql(sSql);
                myCmd.Parameters.Add(Database.Parameter(myProv, "KeySet", KEYSET));
                myCmd.Parameters.Add(Database.Parameter(myProv, "KeyName", keyName));
                res = myCmd.ExecuteNonQuery();

                HttpContext.Current.Application.Remove(keyName);
            }
            finally
            {
                myConn.Dispose();
            }
            return res;
        }

        /// <summary>
        /// refresh al application vars
        /// </summary>
        public static void RefreshApplicationVars()
        {
            var settings = AppSettingsManager.GetSettings();
            foreach (AppSetting setting in settings)
            {
                refreshApplicationVar(setting.KeyName, setting.KeyValue);
            }
        }

        private static void refreshApplicationVar(string keyName, string keyValue)
        {
            HttpContext.Current.Application[keyName] = keyValue;
        }
    }
}