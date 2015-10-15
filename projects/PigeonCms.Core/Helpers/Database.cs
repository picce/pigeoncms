using System;
using System.Configuration;
using System.Data;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Common;
using System.Reflection;
using System.Diagnostics;
using System.IO;

namespace PigeonCms
{
    /// <summary>
    /// Database useful functions
    /// </summary>
    public static class Database
    {
        /// <summary>
        /// retrieve the connectionstring using ConnectionStringName setting in AppSettings section
        /// </summary>
        public static string ConnString
        {
            get 
            {
                try
                {
                    string res = "";
                    string connectionStringName = ConfigurationManager.AppSettings["ConnectionStringName"].ToString();
                    ConnectionStringsSection section = (ConnectionStringsSection)ConfigurationManager.GetSection("connectionStrings");
                    res = section.ConnectionStrings[connectionStringName].ToString();
                    return res;
                }
                catch (Exception ex1)
                {
                    throw new Exception("Invalid connectionStringName", ex1);
                }
            }
        }

        public static string ProviderName
        {
            get { return ConfigurationManager.AppSettings["ProviderName"].ToString(); }
        }

        public static DbProviderFactory ProviderFactory 
        {
            get
            {
                return DbProviderFactories.GetFactory(Database.ProviderName);
            }
        }

        public enum MoveRecordDirection
        {
            Up = 1,
            Down
        }

        /// <summary>
        /// parse sql string before the command execution
        /// </summary>
        /// <param name="sqlQuery"></param>
        /// <param name="tabPrefix">current installation tab prefix (usually 'pgn_')</param>
        /// <returns></returns>
        public static string ParseSql(string sqlQuery, string tabPrefix)
        {
            const string PlaceHolder = "#__";
            if (!string.IsNullOrEmpty(tabPrefix))
                return sqlQuery.Replace(PlaceHolder, tabPrefix);
            else
                return sqlQuery.Replace(PlaceHolder, Config.TabPrefix);
        }

        /// <summary>
        /// parse sql string before the command execution
        /// </summary>
        /// <param name="sqlQuery"></param>
        /// <returns></returns>
        public static string ParseSql(string sqlQuery)
        {
            return ParseSql(sqlQuery, "");
        }

        public static string GetStringNotNull(string theField)
        {
            string res = "";
            if (theField != null)
                res = theField;
            return res;
        }

        public static int GetIntNotNull(int theField)
        {
            int res = 0;
            if (theField != null)
                res = theField;
            return res;
        }

        public static string AddDatesRangeParameters(DbParameterCollection parameters, 
            DbProviderFactory myProv, string sqlFieldName, DatesRange rangeFilter)
        {
            string res = "";

            if (rangeFilter.DateRangeType == DatesRange.RangeType.Always)
                res = "1=1";
            else if (rangeFilter.DateRangeType == DatesRange.RangeType.None)
                res = "1=0";
            else
            {
                res = "1=1";
                if (rangeFilter.InitDate.Date != DateTime.MinValue.Date)
                {
                    res += " AND " + sqlFieldName + " >= @InitDate";
                    parameters.Add(Database.Parameter(myProv, "InitDate", rangeFilter.InitDate));
                }
                if (rangeFilter.EndDate.Date != DateTime.MaxValue.Date)
                {
                    res += " AND " + sqlFieldName + " < @EndDate";
                    parameters.Add(Database.Parameter(myProv, "EndDate", rangeFilter.EndDate.AddDays(1)));
                }
            }
            return res;
        }

        /// <summary>
        /// execute 1 sql statement
        /// </summary>
        /// <param name="myRd"></param>
        /// <param name="myCmd"></param>
        /// <param name="sqlCommand">sql string (already parsed) with Database.ParseSql method</param>
        /// <returns>formatted results</returns>
        public static string ExecuteCommand(DbDataReader myRd, DbCommand myCmd, string sqlCommand)
        {
            string res = "";

            myCmd.CommandText = sqlCommand;
            myRd = myCmd.ExecuteReader();
            if (myRd.HasRows)
            {
                int row = 0;
                res += "<table>";
                while (myRd.Read())
                {
                    //col headers
                    if (row == 0)
                    {
                        res += "<tr>";
                        for (int i = 0; i < myRd.FieldCount; i++)
                        {
                            res += "<th>" + myRd.GetName(i) + "</th>";
                        }
                        res += "</tr>";
                    }

                    res += "<tr>";
                    for (int i = 0; i < myRd.FieldCount; i++)
                    {
                        res += "<td>" +
                            HttpContext.Current.Server.HtmlEncode(myRd[i].ToString()) + 
                            "</td>";
                    }
                    res += "</tr>";

                    row++;
                }
                res += "</table>";
            }
            myRd.Close();
            return res;
        }

        /// <summary>
        /// execute multiple sql statement
        /// </summary>
        /// <param name="myRd"></param>
        /// <param name="myCmd"></param>
        /// <param name="sqlQuery">sql string (already parsed) with Database.ParseSql method</param>
        /// <returns></returns>
        public static string ExecuteQuery(DbDataReader myRd, DbCommand myCmd, string sqlQuery)
        {
            string line = "";        //current sqlQuery line
            string sqlCommand = "";  //single command statement
            int lineCounter = 0;
            string res = "";

            try
            {
                StringReader reader = new StringReader(sqlQuery);
                while ((line = reader.ReadLine()) != null)
                {
                    lineCounter++;
                    line = line.Trim();
                    if (line.ToUpper() == "GO")
                    {
                        res += Database.ExecuteCommand(myRd, myCmd, sqlCommand);
                        sqlCommand = "";
                    }
                    else
                    {
                        sqlCommand += line + "\n";
                    }
                }
                if (!string.IsNullOrEmpty(sqlCommand))
                    res += Database.ExecuteCommand(myRd, myCmd, sqlCommand);
            }
            catch (DbException ex)
            {
                throw new Exception(@"Sql query line " + lineCounter, ex);
            }
            return res;
        }

        public static DbParameter Parameter(DbProviderFactory provider, string paramName, object paramValue)
        {
            return Parameter(provider, paramName, paramValue, DbType.Object);
        }

        public static DbParameter Parameter(DbProviderFactory provider, string paramName, object paramValue, DbType paramType)
        {
            DbParameter p1 = provider.CreateParameter();
            p1.ParameterName = paramName;
            p1.Value = paramValue;
            if (paramType != DbType.Object)
            {
                p1.DbType = paramType;    
            }
            //p1.Direction = ParameterDirection.Input
            return p1;
        }
    }


    public class TableManagerWithOrdering<T,F,Kkey>: 
        PigeonCms.TableManager<T,F,Kkey> where T: PigeonCms.ITableWithOrdering
    {
        /// <summary>
        /// change record order in list
        /// </summary>
        /// <param name="recordId">current record id</param>
        /// <param name="direction">direction up, down</param>
        [DataObjectMethod(DataObjectMethodType.Update, false)]
        public virtual void MoveRecord(Kkey recordId, Database.MoveRecordDirection direction)
        {
            T o1;// = new T();
            T o2;// = new T();
            int tmpOrdering = 0;

            try
            {
                o1 = GetByKey(recordId);
                tmpOrdering = o1.Ordering;
                if (direction == Database.MoveRecordDirection.Up)
                {
                    o2 = GetByKey(GetPreviousRecordInOrder(tmpOrdering, recordId));
                }
                else
                {
                    o2 = GetByKey(GetNextRecordInOrder(tmpOrdering, recordId));
                    if (o1.Ordering == o2.Ordering) o2.Ordering++;
                }
                if (o2.Ordering != tmpOrdering)
                {
                    o1.Ordering = o2.Ordering;
                    o2.Ordering = tmpOrdering;
                    Update(o1);
                    Update(o2);
                }
            }
            finally
            {
            }
        }

        /// <summary>
        /// return previous record id in order, if first return currentRecordId
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="currentRecordId"></param>
        /// <returns>a record id</returns>
        protected virtual Kkey GetPreviousRecordInOrder(int ordering, Kkey currentRecordId)
        {
            DbProviderFactory myProv = Database.ProviderFactory;
            DbConnection myConn = myProv.CreateConnection();
            DbDataReader myRd = null;
            DbCommand myCmd = myConn.CreateCommand();
            string sSql;
            Kkey result = currentRecordId;

            try
            {
                myConn.ConnectionString = Database.ConnString;
                myConn.Open();
                myCmd.Connection = myConn;

                sSql = "SELECT TOP 1 [" + this.KeyFieldName + "] FROM [" + this.TableName + "] WHERE ordering < @ordering ORDER BY ordering DESC ";
                myCmd.CommandText = Database.ParseSql(sSql);
                myCmd.Parameters.Add(Database.Parameter(myProv, "ordering", ordering));
                myRd = myCmd.ExecuteReader();
                if (myRd.Read())
                {
                    if (myRd[0] != DBNull.Value)
                    {
                        result = (Kkey)myRd[0];
                    }
                }
                myRd.Close();
                //se nn trovo un record prendo quello precedente per chiave (per init tabella)
                if (result.ToString() == "0")
                {
                    sSql = "SELECT TOP 1 [" + KeyFieldName + "] FROM " + TableName
                        + " WHERE [" + KeyFieldName + "] < @currentRecordId ORDER BY ordering ASC ";
                    myCmd.CommandText = Database.ParseSql(sSql);
                    myCmd.Parameters.Add(Database.Parameter(myProv, "currentRecordId", currentRecordId));
                    myRd = myCmd.ExecuteReader();
                    if (myRd.Read())
                    {
                        if (myRd[0] != DBNull.Value)
                        {
                            result = (Kkey)myRd[0];
                        }
                    }
                    myRd.Close();
                }
            }
            finally
            {
                myConn.Dispose();
            }
            return result;
        }

        /// <summary>
        /// return next record in order, if last return currentRecordId
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="currentRecordId"></param>
        /// <returns>a recordId</returns>
        protected virtual Kkey GetNextRecordInOrder(int ordering, Kkey currentRecordId)
        {
            DbProviderFactory myProv = Database.ProviderFactory;
            DbConnection myConn = myProv.CreateConnection();
            DbDataReader myRd = null;
            DbCommand myCmd = myConn.CreateCommand();
            string sSql;
            Kkey result = currentRecordId;

            try
            {
                myConn.ConnectionString = Database.ConnString;
                myConn.Open();
                myCmd.Connection = myConn;

                sSql = "SELECT TOP 1 [" + this.KeyFieldName + "] FROM [" + this.TableName + "] WHERE ordering > @ordering ORDER BY ordering ASC ";
                myCmd.CommandText = Database.ParseSql(sSql);
                myCmd.Parameters.Add(Database.Parameter(myProv, "ordering", ordering));
                myRd = myCmd.ExecuteReader();
                if (myRd.Read())
                {
                    if (myRd[0] != DBNull.Value)
                    {
                        result = (Kkey)myRd[0];
                    }
                }
                myRd.Close();
                //se nn trovo un record prendo quello successivo per chiave (per init tabella)
                if (result.ToString() == currentRecordId.ToString())
                {
                    sSql = "SELECT TOP 1 [" + KeyFieldName + "] FROM " + TableName
                        + " WHERE [" + KeyFieldName + "] > @currentRecordId ORDER BY ordering, [" + KeyFieldName + "] ";
                    myCmd.CommandText = Database.ParseSql(sSql);
                    myCmd.Parameters.Add(Database.Parameter(myProv, "currentRecordId", currentRecordId));
                    myRd = myCmd.ExecuteReader();
                    if (myRd.Read())
                    {
                        if (myRd[0] != DBNull.Value)
                        {
                            result = (Kkey)myRd[0];
                        }
                    }
                    myRd.Close();
                }
            }
            finally
            {
                myConn.Dispose();
            }
            return result;
        }
    }


    /// <summary>
    /// common methods in DAL class
    /// </summary>
    /// <typeparam name="T">BLL class</typeparam>
    /// <typeparam name="F">BLL filter class</typeparam>
    /// <typeparam name="K">type of key class</typeparam>
    [DataObject()]
    public class TableManager<T,F,Kkey> where T: PigeonCms.ITable
    {
        #region fields
        private string tableName = "";
        private string keyFieldName = "";

        /// <summary>
        /// name of the key field in the table
        /// </summary>
        public string KeyFieldName
        {
            [DebuggerStepThrough()]
            get { return this.keyFieldName; }
            [DebuggerStepThrough()]
            set { this.keyFieldName = value; } 
        }

        /// <summary>
        /// name of the table managed by the class
        /// </summary>
        public string TableName
        {
            [DebuggerStepThrough()]
            get { return this.tableName; }
            [DebuggerStepThrough()]
            set { this.tableName = value; }
        }
        #endregion
     
        [DebuggerStepThrough()]
        public TableManager(){}

        [DebuggerStepThrough()]
        public TableManager(string mainTableName, string keyFieldName)
        {
            TableName = mainTableName;
            KeyFieldName = keyFieldName;
        }

        /// <summary>
        /// dictionary list to use in module admin area (combo)
        /// </summary>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public virtual Dictionary<string, string> GetList()
        {
            int counter = 0;
            Dictionary<string, string> res = new Dictionary<string, string>();
            F filter = default(F); //= new F();
            List<T> list = GetByFilter(filter, "");
            foreach (T item in list)
            {
                res.Add(counter.ToString(), item.ToString());
            }
            return res;
        }

        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public virtual List<T> GetByFilter(F filter, string sort)
        {
            throw new NotImplementedException();    //to complete

            //Type t = typeof(F);
            //PropertyInfo[] props = t.GetProperties();

            //DbProviderFactory myProv = Database.ProviderFactory;
            //DbConnection myConn = myProv.CreateConnection();
            //DbDataReader myRd = null;
            //DbCommand myCmd = myConn.CreateCommand();
            //string sSql;
            //List<T> result = new List<T>();

            //try
            //{
            //    myConn.ConnectionString = Database.ConnString;
            //    myConn.Open();
            //    myCmd.Connection = myConn;

            //    sSql = "SELECT [" + this.KeyFieldName + "] FROM [" + this.TableName + "] t "
            //    + " WHERE [" + this.KeyFieldName + "] > 0 ";

            //    foreach (PropertyInfo prop in props)
            //    {
            //        DataObjectFieldAttribute[] attrs = (DataObjectFieldAttribute[])prop.GetCustomAttributes(typeof(DataObjectFieldAttribute), true);
            //        if (attrs.Length > 0)
            //        {
            //            //switch (attrs[0].GetType().)
            //            //{
            //            //    case Type.
            //            //    default:
            //            //}
            //            if (attrs[0].PrimaryKey)
            //            {
            //                if ((int)prop.GetValue(filter, null) > 0 || (int)prop.GetValue(filter, null) == -1)
            //                {
            //                    sSql += " AND ["+ prop.Name +"] = @RecordId ";
            //                    myCmd.Parameters.Add(Database.Parameter(myProv, "RecordId", (int)prop.GetValue(filter, null)));
            //                }
            //            }
            //        }

            //        //if (!string.IsNullOrEmpty(filter.Nome))
            //        //{
            //        //    sSql += " AND t.Nome = @Nome ";
            //        //    myCmd.Parameters.Add(Database.Parameter(myProv, "Nome", filter.Nome));
            //        //}
            //        //if (filter.Visible != Utility.TristateBool.NotSet)
            //        //{
            //        //    sSql += " AND t.Visible = @Visible ";
            //        //    myCmd.Parameters.Add(Database.Parameter(myProv, "Visible", filter.Visible));
            //        //} 
            //    }

            //    if (!string.IsNullOrEmpty(sort))
            //    {
            //        sSql += " ORDER BY " + sort;
            //    }
            //    else
            //    {
            //        sSql += " ORDER BY OrderId ";
            //    }

            //    myCmd.CommandText = Database.ParseSql(sSql);
            //    myRd = myCmd.ExecuteReader();
            //    while (myRd.Read())
            //    {
            //        T item = GetById((Kkey)myRd[this.KeyFieldName]);
            //        result.Add(item);
            //    }
            //    myRd.Close();
            //}
            //finally
            //{
            //    myConn.Dispose();
            //}
            //return result;
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public virtual T GetByKey(Kkey id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// update the existing record
        /// </summary>
        /// <param name="theObj">The obj to update</param>
        /// <returns>number of records affected</returns>
        [DataObjectMethod(DataObjectMethodType.Update, false)]
        public virtual int Update(T theObj)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Insert a new record; newObj.recordId=last+1, newObj.OrderId=last+1
        /// </summary>
        /// <param name="newObj">Data about the new object</param>
        /// <returns>The new obj</returns>
        [DataObjectMethod(DataObjectMethodType.Insert, false)]
        public virtual T Insert(T newObj)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Delete a record from its own table
        /// </summary>
        /// <param name="recordId">P Key of the record to delete</param>
        /// <returns>Num of records deleted</returns>
        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public virtual int DeleteById(Kkey recordId)
        {
            if (string.IsNullOrEmpty(TableName) || string.IsNullOrEmpty(KeyFieldName))
                throw new NotImplementedException("Not implemented or TableName/KeyFieldName not filled");

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

                sSql = "DELETE FROM [" + this.TableName + "] WHERE [" + this.KeyFieldName + "] = @recordId ";
                myCmd.CommandText = Database.ParseSql(sSql);
                myCmd.Parameters.Add(Database.Parameter(myProv, "recordId", recordId));
                res = myCmd.ExecuteNonQuery();
            }
            finally
            {
                myConn.Dispose();
            }
            return res;
        }

        /// <summary>
        /// fill the obj2Fill with data in myRd
        /// </summary>
        /// <param name="obj2Fill"></param>
        /// <param name="myRd"></param>
        protected virtual void FillObject(T obj2Fill, DbDataReader myRd)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Used during Insert method. Put the new record in the last order position
        /// </summary>
        /// <returns>The order position</returns>
        protected virtual int GetNextOrderId()
        {
            return GetNextProgressive(this.TableName, "orderId");
        }

        /// <summary>
        /// Used during Insert method. Put the new record in the last order position
        /// </summary>
        /// <returns>The order position</returns>
        protected virtual int GetNextOrdering()
        {
            return GetNextProgressive(this.TableName, "ordering");
        }

        /// <summary>
        /// Give the new record id for current class,key T,F
        /// </summary>
        /// <returns>The new record Id</returns>
        protected virtual int GetNextId()
        {
            return GetNextProgressive(this.TableName, this.KeyFieldName);
        }

        /// <summary>
        /// Give the new record id for current class,key T,F
        /// </summary>
        /// <returns>The next progressive value</returns>
        protected int GetNextProgressive(string tableName, string fieldName)
        {
            DbProviderFactory myProv = Database.ProviderFactory;
            DbConnection myConn = myProv.CreateConnection();
            DbDataReader myRd = null;
            DbCommand myCmd = myConn.CreateCommand();
            string sSql;
            int result = 0;

            try
            {
                myConn.ConnectionString = Database.ConnString;
                myConn.Open();
                myCmd.Connection = myConn;

                sSql = "SELECT max([" + fieldName + "]) FROM [" + tableName + "]";
                myCmd.CommandText = Database.ParseSql(sSql);
                myRd = myCmd.ExecuteReader();
                if (myRd.Read())
                {
                    if (myRd[0] != DBNull.Value)
                    {
                        result = (int)myRd[0];
                    }
                }
                myRd.Close();
                result++;
            }
            finally
            {
                myConn.Dispose();
            }
            return result;
        }

    }


    /// <summary>
    /// Generic Table used to implement a BLL object
    /// </summary>
    public interface ITable
    {
    }

    public interface ITableFilter
    {
    }

    public interface ITableWithOrdering: ITable
    {
        int Ordering { get; set; }
    }

    public interface ITableWithComments : ITable
    {
        int CommentsGroupId { get; set; }
    }

    public interface ITableExternalId
    {
        string ExtId { get; set; }
    }

    public interface ITableWithPermissions : ITable
    {
        //read
        MenuAccesstype ReadAccessType { get; set; }
        int ReadPermissionId { get; set; }
        List<string> ReadRolenames { get; set; }
        string ReadAccessCode { get; set; }
        int ReadAccessLevel { get; set; }
        //write
        MenuAccesstype WriteAccessType { get; set; }
        int WritePermissionId { get; set; }
        List<string> WriteRolenames { get; set; }
        string WriteAccessCode { get; set; }
        int WriteAccessLevel { get; set; }
    }

    public interface ITableManager
    {
        //*** not implemented

        //int DeleteById(int recordId);
        //int Update(ITableObject theObj);
        //ITableObject Insert(ITableObject newObj);
        //List<ITable> GetByFilter(F filter, string sort);
        //ITableObject GetById();
    }

    public interface ITableManagerWithPermission: ITable
    {
        bool CheckUserContext { get; }
        bool WriteMode { get; }
    }

    public interface ITableManagerExternalId<T>
        where T: ITableExternalId
    {
        T GetByExtId(string extId);
        int DeleteByExtId(string extId);
    }
}