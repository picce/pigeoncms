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
using System.IO;
using System.Data.Common;
using PigeonCms;
using System.Diagnostics;

namespace PigeonCms
{
    /// <summary>
    /// DAL for Culture obj (in table Cultures)
    /// </summary>
    public class CulturesManager: TableManagerWithOrdering<Culture, CulturesFilter, string>
    {
        [DebuggerStepThrough()]
        public CulturesManager()
        {
            this.TableName = "#__cultures";
            this.KeyFieldName = "CultureCode";
        }

        public override Dictionary<string, string> GetList()
        {
            Dictionary<string, string> res = new Dictionary<string, string>();
            CulturesFilter filter = new CulturesFilter();
            List<Culture> list = GetByFilter(filter, "");
            foreach (Culture item in list)
            {
                res.Add(item.CultureCode.ToString(), item.DisplayName);
            }
            return res;
        }

        public override List<Culture> GetByFilter(CulturesFilter filter, string sort)
        {
            DbProviderFactory myProv = Database.ProviderFactory;
            DbConnection myConn = myProv.CreateConnection();
            DbDataReader myRd = null;
            DbCommand myCmd = myConn.CreateCommand();
            string sSql;
            List<Culture> result = new List<Culture>();

            try
            {
                myConn.ConnectionString = Database.ConnString;
                myConn.Open();
                myCmd.Connection = myConn;

                sSql = "SELECT t.cultureCode "
                    + " FROM [#__cultures] t "
                    + " WHERE 1=1 ";
                if (!string.IsNullOrEmpty(filter.CultureCode))
                {
                    sSql += " AND t.CultureCode = @CultureCode ";
                    myCmd.Parameters.Add(Database.Parameter(myProv, "CultureCode", filter.CultureCode));
                }
                if (filter.Enabled != Utility.TristateBool.NotSet)
                {
                    sSql += " AND t.Enabled = @Enabled ";
                    myCmd.Parameters.Add(Database.Parameter(myProv, "Enabled", filter.Enabled));
                }

                if (!string.IsNullOrEmpty(sort))
                {
                    sSql += " ORDER BY " + sort;
                }
                else
                {
                    sSql += " ORDER BY t.Ordering ";
                }

                myCmd.CommandText = Database.ParseSql(sSql);
                myRd = myCmd.ExecuteReader();
                while (myRd.Read())
                {
                    Culture item = GetByKey(myRd["CultureCode"].ToString());
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

        public override Culture GetByKey(string cultureCode)
        {
            DbProviderFactory myProv = Database.ProviderFactory;
            DbConnection myConn = myProv.CreateConnection();
            DbDataReader myRd = null;
            DbCommand myCmd = myConn.CreateCommand();
            string sSql;
            Culture result = new Culture();

            try
            {
                myConn.ConnectionString = Database.ConnString;
                myConn.Open();
                myCmd.Connection = myConn;

                sSql = "SELECT CultureCode, DisplayName, Enabled, Ordering "
                + " FROM [#__cultures] "
                + " WHERE CultureCode = @CultureCode ";
                myCmd.CommandText = Database.ParseSql(sSql);
                myCmd.Parameters.Add(Database.Parameter(myProv, "CultureCode", cultureCode));
                myRd = myCmd.ExecuteReader();
                if (myRd.Read())
                {
                    if (!Convert.IsDBNull(myRd["CultureCode"]))
                        result.CultureCode = myRd["CultureCode"].ToString();
                    if (!Convert.IsDBNull(myRd["DisplayName"]))
                        result.DisplayName = myRd["DisplayName"].ToString();
                    if (!Convert.IsDBNull(myRd["Enabled"]))
                        result.Enabled = (bool)myRd["Enabled"];
                    if (!Convert.IsDBNull(myRd["Ordering"]))
                        result.Ordering = (int)myRd["Ordering"];
                }
                myRd.Close();
            }
            finally
            {
                myConn.Dispose();
            }
            return result;
        }

        public override int Update(Culture theObj)
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

                if (theObj.Ordering == 0)
                {
                    theObj.Ordering = this.GetNextOrdering();
                }

                sSql = "UPDATE [#__cultures] "
                + " SET DisplayName=@DisplayName, Enabled=@Enabled, Ordering=@Ordering "
                + " WHERE CultureCode = @CultureCode";
                myCmd.CommandText = Database.ParseSql(sSql);
                myCmd.Parameters.Add(Database.Parameter(myProv, "CultureCode", theObj.CultureCode));
                myCmd.Parameters.Add(Database.Parameter(myProv, "DisplayName", theObj.DisplayName));
                myCmd.Parameters.Add(Database.Parameter(myProv, "Enabled", theObj.Enabled));
                myCmd.Parameters.Add(Database.Parameter(myProv, "Ordering", theObj.Ordering));

                result = myCmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                myConn.Dispose();
            }
            return result;
        }

        public override Culture Insert(Culture newObj)
        {
            DbProviderFactory myProv = Database.ProviderFactory;
            DbConnection myConn = myProv.CreateConnection();
            DbCommand myCmd = myConn.CreateCommand();
            string sSql;
            Culture result = new Culture();

            try
            {
                myConn.ConnectionString = Database.ConnString;
                myConn.Open();
                myCmd.Connection = myConn;

                result = newObj;
                result.Ordering = base.GetNextOrdering();

                sSql = "INSERT INTO [#__cultures](CultureCode, DisplayName, Enabled, Ordering) "
                + " VALUES(@CultureCode, @DisplayName, @Enabled, @Ordering) ";
                myCmd.CommandText = Database.ParseSql(sSql);
                myCmd.Parameters.Add(Database.Parameter(myProv, "CultureCode", result.CultureCode));
                myCmd.Parameters.Add(Database.Parameter(myProv, "DisplayName", result.DisplayName));
                myCmd.Parameters.Add(Database.Parameter(myProv, "Enabled", result.Enabled));
                myCmd.Parameters.Add(Database.Parameter(myProv, "Ordering", result.Ordering));

                myCmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                myConn.Dispose();
            }
            return result;
        }

        public void RefreshCultureList()
        {
            try
            {
                Dictionary<String, String> cultureList = new Dictionary<string, string>();

                CulturesFilter filter = new CulturesFilter();
                filter.Enabled = Utility.TristateBool.True;
                var list = new List<PigeonCms.Culture>();
                try
                {
                    list = new CulturesManager().GetByFilter(filter, "");
                    foreach (PigeonCms.Culture item in list)
                    {
                        cultureList.Add(item.CultureCode, item.DisplayName);
                    }
                }
                catch
                {
                    //20141205 pice
                    //first pigeoncms run, no table present
                }

                if (list.Count == 0)
                {
                    //default value - only for first website run
                    cultureList.Add("en-US", "English");

                    //cultureList.Add("it-IT", "Italiano");
                    //cultureList.Add("de-DE", "Deutsch");
                    //cultureList.Add("es-ES", "Español");
                }

                HttpContext.Current.Application["CultureList"] = cultureList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}