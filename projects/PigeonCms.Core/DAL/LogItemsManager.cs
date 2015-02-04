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
    /// DAL for LogItem obj (in table #__logItems)
    /// </summary>
    public class LogItemsManager : TableManager<LogItem, LogItemsFilter, int>, ITableManager
    {
        [DebuggerStepThrough()]
        public LogItemsManager()
        {
            this.TableName = "#__logItems";
            this.KeyFieldName = "Id";
        }

        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public override List<LogItem> GetByFilter(LogItemsFilter filter, string sort)
        {
            DbProviderFactory myProv = Database.ProviderFactory;
            DbConnection myConn = myProv.CreateConnection();
            DbDataReader myRd = null;
            DbCommand myCmd = myConn.CreateCommand();
            string sSql;
            string sTopRows = "";
            var result = new List<LogItem>();

            try
            {
                myConn.ConnectionString = Database.ConnString;
                myConn.Open();
                myCmd.Connection = myConn;

                if (filter.TopRows > 0)
                    sTopRows = "TOP " + filter.TopRows;

                sSql = "SELECT "+ sTopRows +" t.Id, t.DateInserted, t.UserInserted, t.ModuleId, t.Type, "
                    + " t.UserHostAddress, t.Url, t.Description, "
                    + " m.ModuleName, m.ModuleNamespace, (m.ModuleNamespace + '.' + m.ModuleName)ModuleFullName, "
                    + " t.SessionId "
                    + " FROM ["+ this.TableName +"] t LEFT JOIN #__modules m ON t.moduleId = m.id "
                    + " WHERE t.Id > 0 ";
                if (filter.Id > 0 || filter.Id == -1)
                {
                    sSql += " AND t.Id = @Id ";
                    myCmd.Parameters.Add(Database.Parameter(myProv, "Id", filter.Id));
                }
                if (!string.IsNullOrEmpty(filter.UserInserted))
                {
                    sSql += " AND t.UserInserted = @UserInserted ";
                    myCmd.Parameters.Add(Database.Parameter(myProv, "UserInserted", filter.UserInserted));
                }
                if (filter.ModuleId > 0)
                {
                    sSql += " AND t.ModuleId = @ModuleId ";
                    myCmd.Parameters.Add(Database.Parameter(myProv, "ModuleId", filter.ModuleId));
                }
                if (filter.FilterType)
                {
                    sSql += " AND t.Type >= @Type ";
                    myCmd.Parameters.Add(Database.Parameter(myProv, "Type", (int)filter.Type));
                }

                //datesrange filter
                sSql += " AND ("
                     + Database.AddDatesRangeParameters(myCmd.Parameters, myProv, "t.DateInserted", filter.DateInsertedRange)
                     + ")";

                if (!string.IsNullOrEmpty(filter.UrlPart))
                {
                    sSql += " AND t.Url like @UrlPart ";
                    myCmd.Parameters.Add(Database.Parameter(myProv, "UrlPart", "%" + filter.UrlPart + "%"));
                }
                if (!string.IsNullOrEmpty(filter.DescriptionPart))
                {
                    sSql += " AND t.Description like @DescriptionPart ";
                    myCmd.Parameters.Add(Database.Parameter(myProv, "DescriptionPart", "%" + filter.DescriptionPart + "%"));
                }
                if (!string.IsNullOrEmpty(filter.ModuleFullName))
                {
                    sSql += " AND (m.ModuleNamespace + '.' + m.ModuleName) = @ModuleFullName ";
                    myCmd.Parameters.Add(Database.Parameter(myProv, "ModuleFullName", filter.ModuleFullName));
                }
                if (!string.IsNullOrEmpty(filter.ModuleNamespace))
                {
                    sSql += " AND m.ModuleNamespace = @ModuleNamespace ";
                    myCmd.Parameters.Add(Database.Parameter(myProv, "ModuleNamespace", filter.ModuleNamespace));
                }
                if (!string.IsNullOrEmpty(filter.UserHostAddressPart))
                {
                    sSql += " AND t.UserHostAddress like @UserHostAddressPart ";
                    myCmd.Parameters.Add(Database.Parameter(myProv, "UserHostAddressPart", "%" + filter.UserHostAddressPart + "%"));
                }
                if (!string.IsNullOrEmpty(filter.SessionIdPart))
                {
                    sSql += " AND t.SessionId like @SessionIdPart ";
                    myCmd.Parameters.Add(Database.Parameter(myProv, "SessionIdPart", "%" + filter.SessionIdPart + "%"));
                }
                if (!string.IsNullOrEmpty(sort))
                {
                    sSql += " ORDER BY " + sort;
                }
                else
                {
                    sSql += " ORDER BY t.Id DESC ";
                }

                myCmd.CommandText = Database.ParseSql(sSql);
                myRd = myCmd.ExecuteReader();
                while (myRd.Read())
                {
                    var item = new LogItem();
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

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public override LogItem GetByKey(int id)
        {
            LogItem result = new LogItem();
            var list = new List<LogItem>();
            LogItemsFilter filter = new LogItemsFilter();
            filter.Id = id;
            filter.TopRows = 0;
            filter.DateInsertedRange = new DatesRange();
            list = GetByFilter(filter, "");
            if (list.Count > 0)
                result = list[0];
            return result;
        }

        [DataObjectMethod(DataObjectMethodType.Insert, false)]
        public override LogItem Insert(LogItem newObj)
        {
            DbProviderFactory myProv = Database.ProviderFactory;
            DbConnection myConn = myProv.CreateConnection();
            DbCommand myCmd = myConn.CreateCommand();
            string sSql;
            var result = new PigeonCms.LogItem();

            try
            {
                myConn.ConnectionString = Database.ConnString;
                myConn.Open();
                myCmd.Connection = myConn;

                result = newObj;
                result.DateInserted = DateTime.Now;
                result.UserInserted = PgnUserCurrent.UserName;
                //result.Id = base.GetNextId();<-- identity in db

                sSql = "INSERT INTO [" + this.TableName + "]"
                    + "(DateInserted, UserInserted, ModuleId, Type, UserHostAddress, Url, Description, SessionId) "
                    + " VALUES(@DateInserted, @UserInserted, @ModuleId, @Type, @UserHostAddress, @Url, @Description, @SessionId) ";
                myCmd.CommandText = Database.ParseSql(sSql);

                myCmd.Parameters.Add(Database.Parameter(myProv, "DateInserted", result.DateInserted));
                myCmd.Parameters.Add(Database.Parameter(myProv, "UserInserted", result.UserInserted));
                myCmd.Parameters.Add(Database.Parameter(myProv, "ModuleId", result.ModuleId));
                myCmd.Parameters.Add(Database.Parameter(myProv, "Type", (int)result.Type));
                myCmd.Parameters.Add(Database.Parameter(myProv, "UserHostAddress", result.UserHostAddress));
                myCmd.Parameters.Add(Database.Parameter(myProv, "Url", result.Url));
                myCmd.Parameters.Add(Database.Parameter(myProv, "Description", Utility.Html.GetTextPreview(result.Description,490,".." )));
                myCmd.Parameters.Add(Database.Parameter(myProv, "SessionId", result.SessionId));

                myCmd.ExecuteNonQuery();
            }
            finally
            {
                myConn.Dispose();
            }
            return result;
        }

        protected override void FillObject(LogItem result, DbDataReader myRd)
        {
            if (!Convert.IsDBNull(myRd["Id"]))
                result.Id = (int)myRd["Id"];
            if (!Convert.IsDBNull(myRd["DateInserted"]))
                result.DateInserted = (DateTime)myRd["DateInserted"];
            if (!Convert.IsDBNull(myRd["UserInserted"]))
                result.UserInserted = (string)myRd["UserInserted"];
            if (!Convert.IsDBNull(myRd["ModuleId"]))
                result.ModuleId = (int)myRd["ModuleId"];
            if (!Convert.IsDBNull(myRd["Type"]))
                result.Type = (TracerItemType)int.Parse(myRd["Type"].ToString());
            if (!Convert.IsDBNull(myRd["UserHostAddress"]))
                result.UserHostAddress = (string)myRd["UserHostAddress"];
            if (!Convert.IsDBNull(myRd["Url"]))
                result.Url = (string)myRd["Url"];
            if (!Convert.IsDBNull(myRd["Description"]))
                result.Description = (string)myRd["Description"];
            if (!Convert.IsDBNull(myRd["ModuleFullName"]))
                result.ModuleFullName = (string)myRd["ModuleFullName"];
            if (!Convert.IsDBNull(myRd["SessionId"]))
                result.SessionId = (string)myRd["SessionId"];
        }
    }
}