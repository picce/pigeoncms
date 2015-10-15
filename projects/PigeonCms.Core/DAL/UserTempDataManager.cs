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
    public class UserTempDataManager : TableManager<UserTempData, UserTempDataFilter, int>, ITableManager
    {
        public const int NO_OF_COLS = 20;
        private bool checkUserContext = false;

        public bool CheckUserContext
        {
            get { return checkUserContext; }
        }


        /// <summary>
        /// CheckUserContext=true
        /// </summary>
        [DebuggerStepThrough()]
        public UserTempDataManager(): this(true)
        { }

        public UserTempDataManager(bool checkUserContext)
        {
            this.TableName = "#__userTempData";
            this.KeyFieldName = "Id";
            this.checkUserContext = checkUserContext;
        }

        public override List<UserTempData> GetByFilter(UserTempDataFilter filter, string sort)
        {
            DbProviderFactory myProv = Database.ProviderFactory;
            DbConnection myConn = myProv.CreateConnection();
            DbDataReader myRd = null;
            DbCommand myCmd = myConn.CreateCommand();
            string sSql;
            var result = new List<UserTempData>();

            string topItems = "";

            try
            {
                myConn.ConnectionString = Database.ConnString;
                myConn.Open();
                myCmd.Connection = myConn;

                if (filter.NumOfRecords > 0)
                    topItems = "TOP " + filter.NumOfRecords.ToString();

                sSql = "SELECT " + topItems + " t.Id, t.Username, t.SessionId, "
                    + " t.DateInserted, t.DateExpiration, t.Enabled, "
                    + " t.col01, t.col02, t.col03, t.col04, t.col05, "
                    + " t.col06, t.col07, t.col08, t.col09, t.col10,  "
                    + " t.col11, t.col12, t.col13, t.col14, t.col15,  "
                    + " t.col16, t.col17, t.col18, t.col19, t.col20  "
                    + " FROM [" + this.TableName + "] t "
                    + " WHERE t.Id > 0 ";

                //user context
                if (this.CheckUserContext)
                {
                    if (!PgnUserCurrent.IsAuthenticated)
                        sSql += " AND 1=0";
                    else
                        filter.Username = PgnUserCurrent.UserName;
                }

                if (filter.Id > 0 || filter.Id == -1)
                {
                    sSql += " AND t.Id = @Id ";
                    myCmd.Parameters.Add(Database.Parameter(myProv, "Id", filter.Id));
                }
                if (!string.IsNullOrEmpty(filter.Username))
                {
                    sSql += " AND (t.Username = @Username) ";
                    myCmd.Parameters.Add(Database.Parameter(myProv, "Username", filter.Username));
                }
                if (!string.IsNullOrEmpty(filter.SessionId))
                {
                    sSql += " AND t.SessionId = @SessionId ";
                    myCmd.Parameters.Add(Database.Parameter(myProv, "SessionId", filter.SessionId));
                }
                if (filter.IsExpired != Utility.TristateBool.NotSet)
                {
                    if (filter.IsExpired == Utility.TristateBool.True)
                        sSql += " AND t.dateExpiration < @GetDate ";
                    else
                        sSql += " AND t.dateExpiration >= @GetDate ";
                    myCmd.Parameters.Add(Database.Parameter(myProv, "GetDate", DateTime.Now.Date));
                }
                if (filter.Enabled != Utility.TristateBool.NotSet)
                {
                    sSql += " AND t.Enabled = @Enabled ";
                    myCmd.Parameters.Add(Database.Parameter(myProv, "Enabled", filter.Enabled));
                }

                if (!string.IsNullOrEmpty(sort))
                    sSql += " ORDER BY " + sort;
                else
                    sSql += " ORDER BY t.Id ";

                myCmd.CommandText = Database.ParseSql(sSql);
                myRd = myCmd.ExecuteReader();
                while (myRd.Read())
                {
                    var item = new UserTempData();
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

        public override UserTempData GetByKey(int id)
        {
            var result = new UserTempData();
            var resultList = new List<UserTempData>();
            var filter = new UserTempDataFilter();
            filter.Id = id==0 ? -1 : id;
            resultList = GetByFilter(filter, "");
            if (resultList.Count > 0)
                result = resultList[0];
            return result;
        }

        public override int Update(UserTempData theObj)
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
                + " SET Enabled=@Enabled "
                + " WHERE Id=@Id";
                myCmd.CommandText = Database.ParseSql(sSql);
                myCmd.Parameters.Add(Database.Parameter(myProv, "Id", theObj.Id));
                myCmd.Parameters.Add(Database.Parameter(myProv, "Enabled", theObj.Enabled));

                result = myCmd.ExecuteNonQuery();
            }
            finally
            {
                myConn.Dispose();
            }
            return result;
        }

        public override UserTempData Insert(UserTempData newObj)
        {
            DbProviderFactory myProv = Database.ProviderFactory;
            DbConnection myConn = myProv.CreateConnection();
            DbCommand myCmd = myConn.CreateCommand();
            string sSql;
            var result = new UserTempData();

            try
            {
                myConn.ConnectionString = Database.ConnString;
                myConn.Open();
                myCmd.Connection = myConn;

                result = newObj;
                result.Id = base.GetNextId();
                result.DateInserted = DateTime.Now;
                if (string.IsNullOrEmpty(result.Username))
                    result.Username = PgnUserCurrent.UserName;


                sSql = "INSERT INTO [" + this.TableName + "](Id, Username, SessionId, "
                + " DateInserted, DateExpiration, Enabled, "
                + " Col01, Col02, Col03, Col04, Col05, "
                + " Col06, Col07, Col08, Col09, Col10, "
                + " Col11, Col12, Col13, Col14, Col15, "
                + " Col16, Col17, Col18, Col19, Col20) "
                + " VALUES(@Id, @Username, @SessionId, "
                + " @DateInserted, @DateExpiration, @Enabled, "
                + " @Col01, @Col02, @Col03, @Col04, @Col05, "
                + " @Col06, @Col07, @Col08, @Col09, @Col10, "
                + " @Col11, @Col12, @Col13, @Col14, @Col15, "
                + " @Col16, @Col17, @Col18, @Col19, @Col20) ";
                myCmd.CommandText = Database.ParseSql(sSql);

                myCmd.Parameters.Add(Database.Parameter(myProv, "Id", result.Id));
                myCmd.Parameters.Add(Database.Parameter(myProv, "Username", result.Username));
                myCmd.Parameters.Add(Database.Parameter(myProv, "SessionId", result.SessionId));
                myCmd.Parameters.Add(Database.Parameter(myProv, "DateInserted", result.DateInserted));
                myCmd.Parameters.Add(Database.Parameter(myProv, "DateExpiration", result.DateExpiration));
                myCmd.Parameters.Add(Database.Parameter(myProv, "Enabled", result.Enabled));
                for (int i = 0; i < NO_OF_COLS; i++)
                {
                    myCmd.Parameters.Add(Database.Parameter(myProv, "Col" + (i+1).ToString("00"), result.Columns[i]));
                }
                myCmd.ExecuteNonQuery();
            }
            finally
            {
                myConn.Dispose();
            }
            return result;
        }

        /// <summary>
        /// delete one by one using filter
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public int DeleteByFilter(UserTempDataFilter filter)
        {
            int res = 0;
            var list = this.GetByFilter(filter, "");
            foreach (var item in list)
            {
                base.DeleteById(item.Id);
                res++;
            }
            return res;
        }

        protected override void FillObject(UserTempData result, DbDataReader myRd)
        {
            if (!Convert.IsDBNull(myRd["Id"]))
                result.Id = (int)myRd["Id"];
            if (!Convert.IsDBNull(myRd["Username"]))
                result.Username = (string)myRd["Username"];
            if (!Convert.IsDBNull(myRd["SessionId"]))
                result.SessionId = (string)myRd["SessionId"];
            if (!Convert.IsDBNull(myRd["DateExpiration"]))
                result.DateExpiration = (DateTime)myRd["DateExpiration"];
            if (!Convert.IsDBNull(myRd["Enabled"]))
                result.Enabled = (bool)myRd["Enabled"];
            for (int i = 0; i < NO_OF_COLS; i++)
            {
                result.Columns[i] = (string)myRd["Col" + (i+1).ToString("00")];
            }
        }

    }
}