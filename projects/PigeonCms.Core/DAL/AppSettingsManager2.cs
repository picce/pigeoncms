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
    public class AppSettingsManager2 : 
        TableManager<AppSetting, AppSettingsFilter, string>, ITableManager
    {

        [DebuggerStepThrough()]
        public AppSettingsManager2()
        {
            this.TableName = "#__appSettings";
            //this.KeyFieldName = "KeySet|KeyName";
        }

        public List<string> GetKetSetGroups()
        {
            DbProviderFactory myProv = Database.ProviderFactory;
            DbConnection myConn = myProv.CreateConnection();
            DbDataReader myRd = null;
            DbCommand myCmd = myConn.CreateCommand();
            var res = new List<string>();

            try
            {
                myConn.ConnectionString = Database.ConnString;
                myConn.Open();
                myCmd.Connection = myConn;

                string sSql = "SELECT KeySet FROM [" + this.TableName + "] "
                    + "  GROUP BY KeySet ORDER BY KeySet";

                myCmd.CommandText = Database.ParseSql(sSql);
                myRd = myCmd.ExecuteReader();
                while (myRd.Read())
                {
                    if (!Convert.IsDBNull(myRd["KeySet"]))
                        res.Add((string)myRd["KeySet"]);
                }
                myRd.Close();
            }
            finally
            {
                myConn.Dispose();
            }

            return res;
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<AppSetting> GetByFilter(AppSettingsFilter filter, string sort)
        {
            DbProviderFactory myProv = Database.ProviderFactory;
            DbConnection myConn = myProv.CreateConnection();
            DbDataReader myRd = null;
            DbCommand myCmd = myConn.CreateCommand();
            string sSql;
            var result = new List<AppSetting>();

            try
            {
                myConn.ConnectionString = Database.ConnString;
                myConn.Open();
                myCmd.Connection = myConn;

                sSql = "SELECT t.KeySet, t.KeyName, "
                + " t.KeyTitle, t.KeyValue, t.KeyInfo "
                + " FROM [" + this.TableName + "] t "
                + " WHERE 1 = 1 ";
                if (!string.IsNullOrEmpty(filter.KeySet))
                {
                    sSql += " AND t.KeySet = @KeySet ";
                    myCmd.Parameters.Add(Database.Parameter(myProv, "KeySet", filter.KeySet));
                }
                if (!string.IsNullOrEmpty(filter.KeyName))
                {
                    sSql += " AND t.KeyName = @KeyName ";
                    myCmd.Parameters.Add(Database.Parameter(myProv, "KeyName", filter.KeyName));
                }
                if (!string.IsNullOrEmpty(sort))
                {
                    sSql += " ORDER BY " + sort;
                }
                else
                {
                    sSql += " ORDER BY t.KeySet, t.KeyName";
                }
                
                myCmd.CommandText = Database.ParseSql(sSql);
                myRd = myCmd.ExecuteReader();
                while (myRd.Read())
                {
                    var item = new AppSetting();
                    FillObject(item, myRd);
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

        [Obsolete("Use AppSettingsManager2.GetByKey(string keySet, string keyName)", true)]
        public override AppSetting GetByKey(string id)
        {
            throw new NotSupportedException();
        }

        public AppSetting GetByKey(string keySet, string keyName)
        {
            var result = new AppSetting();
            var resultList = new List<AppSetting>();
            var filter = new AppSettingsFilter();

            if (string.IsNullOrEmpty(keySet) || string.IsNullOrEmpty(keyName))
                return result;

            filter.KeySet = keySet;
            filter.KeyName = keyName;
            resultList = GetByFilter(filter, "");
            if (resultList.Count > 0)
                result = resultList[0];

            return result;
        }

        public List<AppSetting> GetByKeySet(string keySet)
        {
            var result = new List<AppSetting>();
            var filter = new AppSettingsFilter();

            if (string.IsNullOrEmpty(keySet))
                return result;

            filter.KeySet = keySet;
            result = GetByFilter(filter, "");

            return result;
        }

        [DataObjectMethod(DataObjectMethodType.Update, false)]
        public override int Update(AppSetting theObj)
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

                sSql = "UPDATE [" + this.TableName + "] "
                + " SET KeyTitle=@KeyTitle, KeyValue=@KeyValue, KeyInfo=@KeyInfo "
                + " WHERE KeySet=@KeySet AND KeyName=@KeyName ";
                myCmd.CommandText = Database.ParseSql(sSql);
                myCmd.Parameters.Add(Database.Parameter(myProv, "KeySet", theObj.KeySet));
                myCmd.Parameters.Add(Database.Parameter(myProv, "KeyName", theObj.KeyName));
                myCmd.Parameters.Add(Database.Parameter(myProv, "KeyTitle", theObj.KeyTitle));
                myCmd.Parameters.Add(Database.Parameter(myProv, "KeyValue", theObj.KeyValue));
                myCmd.Parameters.Add(Database.Parameter(myProv, "KeyInfo", theObj.KeyInfo));
                result = myCmd.ExecuteNonQuery();
            }
            finally
            {
                myConn.Dispose();
            }
            return result;
        }

        [DataObjectMethod(DataObjectMethodType.Insert, false)]
        public override AppSetting Insert(AppSetting newObj)
        {
            DbProviderFactory myProv = Database.ProviderFactory;
            DbConnection myConn = myProv.CreateConnection();
            DbCommand myCmd = myConn.CreateCommand();
            string sSql;

            try
            {
                myConn.ConnectionString = Database.ConnString;
                myConn.Open();
                myCmd.Connection = myConn;

                sSql = "INSERT INTO #__AppSettings(KeySet, KeyName, KeyTitle, KeyValue, KeyInfo) "
                + "VALUES(@KeySet, @KeyName, @KeyTitle, @KeyValue, @KeyInfo) ";
                myCmd.CommandText = Database.ParseSql(sSql);
                myCmd.Parameters.Add(Database.Parameter(myProv, "KeySet", newObj.KeySet));
                myCmd.Parameters.Add(Database.Parameter(myProv, "KeyName", newObj.KeyName));
                myCmd.Parameters.Add(Database.Parameter(myProv, "KeyTitle", newObj.KeyTitle));
                myCmd.Parameters.Add(Database.Parameter(myProv, "KeyValue", newObj.KeyValue));
                myCmd.Parameters.Add(Database.Parameter(myProv, "KeyInfo", newObj.KeyInfo));
                myCmd.ExecuteNonQuery();
            }
            finally
            {
                myConn.Dispose();
            }
            return newObj;
        }


        [Obsolete("Use AppSettingsManager2.Delete(string keySet, string keyName)", true)]
        public override int DeleteById(string recordId)
        {
            throw new NotSupportedException();
        }

        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public int DeleteByKey(string keySet, string keyName)
        { 
            if (string.IsNullOrEmpty(keyName))
                return 0;

            return delete(keySet, keyName);
        }

        [DataObjectMethod(DataObjectMethodType.Delete, false)]
        public int DeleteByKeySet(string keySet)
        {
            return delete(keySet);
        }

        private int delete(string keySet, string keyName = "")
        {
            DbProviderFactory myProv = Database.ProviderFactory;
            DbConnection myConn = myProv.CreateConnection();
            DbCommand myCmd = myConn.CreateCommand();
            string sSql;
            int res = 0;

            if (string.IsNullOrEmpty(keySet))
                throw new ArgumentException("empty keyset", "keySet");

            try
            {
                myConn.ConnectionString = Database.ConnString;
                myConn.Open();
                myCmd.Connection = myConn;

                sSql = "DELETE FROM [" + this.TableName + "]  WHERE @KeySet = KeySet ";
                myCmd.Parameters.Add(Database.Parameter(myProv, "KeySet", keySet));
                if (!string.IsNullOrEmpty(keyName))
                {
                    sSql += " AND KeyName = @KeyName ";
                    myCmd.Parameters.Add(Database.Parameter(myProv, "KeyName", keyName));
                }
                myCmd.CommandText = Database.ParseSql(sSql);
                res = myCmd.ExecuteNonQuery();
            }
            finally
            {
                myConn.Dispose();
            }
            return res;
        }

        protected override void FillObject(AppSetting result, DbDataReader myRd)
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

    }
}