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
    public class EventsManager<T,F>: TableManager<T,F, int>, ITableManager
        where T: Event, new()
        where F: EventsFilter, new()
    {
        [DebuggerStepThrough()]
        public EventsManager()
        {
            this.TableName = "#__events";
            this.KeyFieldName = "Id";
        }

        public override Dictionary<string, string> GetList()
        {
            Dictionary<string, string> res = new Dictionary<string, string>();
            F filter = new F();
            filter.EventStart = DateTime.Now;
            List<T> list = GetByFilter(filter, "");
            foreach (T item in list)
            {
                res.Add(item.Id.ToString(), item.Name);
            }
            return res;
        }

        public override List<T> GetByFilter(F filter, string sort)
        {
            DbProviderFactory myProv = Database.ProviderFactory;
            DbConnection myConn = myProv.CreateConnection();
            DbDataReader myRd = null;
            DbCommand myCmd = myConn.CreateCommand();
            string sSql;
            List<T> result = new List<T>();

            try
            {
                myConn.ConnectionString = Database.ConnString;
                myConn.Open();
                myCmd.Connection = myConn;

                sSql = "SELECT t.Id, t.Name, t.EventStart, t.EventEnd, t.ResourceId, t.Status, "
                    + " t.GroupId, t.OrderId, t.Description "
                    + " FROM ["+ this.TableName +"] t "
                    + " WHERE t.Id > 0 ";
                if (filter.Id > 0 || filter.Id == -1)
                {
                    sSql += " AND t.Id = @Id ";
                    myCmd.Parameters.Add(Database.Parameter(myProv, "Id", filter.Id));
                }
                if (filter.ResourceId > 0)
                {
                    sSql += " AND t.ResourceId = @ResourceId ";
                    myCmd.Parameters.Add(Database.Parameter(myProv, "ResourceId", filter.ResourceId));
                }
                if (!string.IsNullOrEmpty(filter.NameSearch))
                {
                    sSql += " AND (t.Name like @NameSearch) ";
                    myCmd.Parameters.Add(Database.Parameter(myProv, "NameSearch", "%" + filter.NameSearch + "%"));
                }
                if (filter.EventStart != DateTime.MinValue)
                {
                    sSql += " AND t.EventStart >= @EventStart ";
                    myCmd.Parameters.Add(Database.Parameter(myProv, "EventStart", filter.EventStart));
                }
                if (filter.EventEnd != DateTime.MinValue)
                {
                    sSql += " AND t.EventEnd <= @EventEnd ";
                    myCmd.Parameters.Add(Database.Parameter(myProv, "EventEnd", filter.EventEnd));
                }
                if (filter.FilterStatus)
                {
                    sSql += " AND t.Status = @Status ";
                    myCmd.Parameters.Add(Database.Parameter(myProv, "Status", (int)filter.Status));
                }
                if (filter.GroupId > 0)
                {
                    sSql += " AND t.GroupId = @GroupId ";
                    myCmd.Parameters.Add(Database.Parameter(myProv, "GroupId", filter.GroupId));
                }
                if (filter.OrderId > 0)
                {
                    sSql += " AND t.OrderId = @OrderId ";
                    myCmd.Parameters.Add(Database.Parameter(myProv, "OrderId", filter.OrderId));
                }
                if (!string.IsNullOrEmpty(sort))
                {
                    sSql += " ORDER BY " + sort;
                }
                else
                {
                    sSql += " ORDER BY t.EventStart ";
                }

                myCmd.CommandText = Database.ParseSql(sSql);
                myRd = myCmd.ExecuteReader();
                while (myRd.Read())
                {
                    T item = new T();
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

        public override T GetByKey(int id)
        {
            T result = new T();
            List<T>resultList = new List<T>();
            F filter = new F();
            filter.Id = id;
            resultList = GetByFilter(filter, "");
            if (resultList.Count > 0)
                result = resultList[0];
            return result;
        }

        public override int Update(T theObj)
        {
            return Update(theObj, false);
        }

        public int Update(T theObj, bool onlyIfAvailable)
        {
            DbProviderFactory myProv = Database.ProviderFactory;
            DbConnection myConn = myProv.CreateConnection();
            DbCommand myCmd = myConn.CreateCommand();
            string sSql;
            int result = 0;

            if (onlyIfAvailable)
            {
                //check if available period
                EventsFilter filter = new EventsFilter();
                filter.EventStart = theObj.EventStart;
                filter.EventEnd = theObj.EventEnd;
                filter.ResourceId = theObj.ResourceId;
                filter.Id = theObj.Id;
                if (!IsAvailable(filter))
                    throw new ArgumentException("period not available", "NotAvailable");
            }

            try
            {
                myConn.ConnectionString = Database.ConnString;
                myConn.Open();
                myCmd.Connection = myConn;

                sSql = "UPDATE [" + this.TableName + "] "
                + " SET Name=@Name, EventStart=@EventStart, EventEnd=@EventEnd, "
                + " ResourceId=@ResourceId, Status=@Status, GroupId=@GroupId, "
                + " OrderId=@OrderId, Description=@Description "
                + " WHERE Id = @Id";
                myCmd.CommandText = Database.ParseSql(sSql);
                myCmd.Parameters.Add(Database.Parameter(myProv, "Id", theObj.Id));
                myCmd.Parameters.Add(Database.Parameter(myProv, "Name", theObj.Name));
                myCmd.Parameters.Add(Database.Parameter(myProv, "Status", theObj.Status));
                myCmd.Parameters.Add(Database.Parameter(myProv, "ResourceId", theObj.ResourceId));
                myCmd.Parameters.Add(Database.Parameter(myProv, "GroupId", theObj.GroupId));
                myCmd.Parameters.Add(Database.Parameter(myProv, "OrderId", theObj.OrderId));
                myCmd.Parameters.Add(Database.Parameter(myProv, "Description", theObj.Description));

                if (theObj.EventStart == DateTime.MinValue)
                    myCmd.Parameters.Add(Database.Parameter(myProv, "EventStart", DBNull.Value));
                else
                    myCmd.Parameters.Add(Database.Parameter(myProv, "EventStart", theObj.EventStart));

                if (theObj.EventEnd == DateTime.MinValue)
                    myCmd.Parameters.Add(Database.Parameter(myProv, "EventEnd", DBNull.Value));
                else
                    myCmd.Parameters.Add(Database.Parameter(myProv, "EventEnd", theObj.EventEnd));

                result = myCmd.ExecuteNonQuery();
            }
            finally
            {
                myConn.Dispose();
            }
            return result;
        }

        /// <summary>
        /// check if the period is busy by another event with the same resourceId
        /// </summary>
        /// <param name="theObj"></param>
        /// <returns></returns>
        public bool IsAvailable(EventsFilter filter)
        {
            DbProviderFactory myProv = Database.ProviderFactory;
            DbConnection myConn = myProv.CreateConnection();
            DbDataReader myRd = null;
            DbCommand myCmd = myConn.CreateCommand();
            string sSql;
            bool res = true;

            try
            {
                myConn.ConnectionString = Database.ConnString;
                myConn.Open();
                myCmd.Connection = myConn;

                sSql = "SELECT t.Id, t.EventStart, t.EventEnd, t.ResourceId, t.Status, t.Name "
                    + " FROM [" + this.TableName + "] t  WHERE "
                    + " ((eventstart between @EventStart and @EventEnd) "
                    + " OR (eventend between @EventStart and @EventEnd) "
                    + " OR (@EventStart between eventstart and eventend) "
                    + " OR (@EventEnd between eventstart and eventend)) "
                    + " AND eventstart <> @EventEnd AND eventend <> @EventStart ";

                myCmd.Parameters.Add(Database.Parameter(myProv, "EventStart", filter.EventStart));
                myCmd.Parameters.Add(Database.Parameter(myProv, "EventEnd", filter.EventEnd));

                if (filter.ResourceId > 0)
                {
                    sSql += " AND t.ResourceId = @ResourceId ";
                    myCmd.Parameters.Add(Database.Parameter(myProv, "ResourceId", filter.ResourceId));
                }
                if (filter.Id > 0)
                {
                    //exclude current event
                    sSql += " AND t.Id != @Id ";
                    myCmd.Parameters.Add(Database.Parameter(myProv, "Id", filter.Id));
                }

                if (filter.FilterStatus)
                {
                    sSql += " AND t.Status = @Status ";
                    myCmd.Parameters.Add(Database.Parameter(myProv, "Status", (int)filter.Status));
                }

                myCmd.CommandText = Database.ParseSql(sSql);
                myRd = myCmd.ExecuteReader();
                if (myRd.Read())
                {
                    res = false;
                }
                myRd.Close();
            }
            finally
            {
                myConn.Dispose();
            }
            return res;
        }

        public T Insert(T newObj, bool onlyIfAvailable)
        {
            DbProviderFactory myProv = Database.ProviderFactory;
            DbConnection myConn = myProv.CreateConnection();
            DbCommand myCmd = myConn.CreateCommand();
            string sSql;
            T result = new T();

            if (onlyIfAvailable)
            {
                //check if available period
                EventsFilter filter = new EventsFilter();
                filter.EventStart = newObj.EventStart;
                filter.EventEnd = newObj.EventEnd;
                filter.ResourceId = newObj.ResourceId;
                if (!IsAvailable(filter))
                    throw new ArgumentException("period not available", "NotAvailable");
            }

            try
            {
                myConn.ConnectionString = Database.ConnString;
                myConn.Open();
                myCmd.Connection = myConn;

                result = newObj;
                result.Id = base.GetNextId();
                //TODO same GroupId on multiple insert 
                //create method: Insert(List<T>newObj){;}
                result.GroupId = base.GetNextProgressive(this.TableName, "GroupId");

                sSql = "INSERT INTO [" + this.TableName + "](Id, Name, EventStart, EventEnd, ResourceId, "
                + " Status, GroupId, OrderId, Description) "
                + " VALUES(@Id, @Name, @EventStart, @EventEnd, @ResourceId, "
                + " @Status, @GroupId, @OrderId, @Description) ";
                myCmd.CommandText = Database.ParseSql(sSql);

                myCmd.Parameters.Add(Database.Parameter(myProv, "Id", result.Id));
                myCmd.Parameters.Add(Database.Parameter(myProv, "Name", result.Name));
                myCmd.Parameters.Add(Database.Parameter(myProv, "Status", result.Status));
                myCmd.Parameters.Add(Database.Parameter(myProv, "ResourceId", result.ResourceId));
                myCmd.Parameters.Add(Database.Parameter(myProv, "GroupId", result.GroupId));
                myCmd.Parameters.Add(Database.Parameter(myProv, "OrderId", result.OrderId));
                myCmd.Parameters.Add(Database.Parameter(myProv, "Description", result.Description));

                if (result.EventStart == DateTime.MinValue)
                    myCmd.Parameters.Add(Database.Parameter(myProv, "EventStart", DBNull.Value));
                else
                    myCmd.Parameters.Add(Database.Parameter(myProv, "EventStart", result.EventStart));
                if (result.EventEnd == DateTime.MinValue)
                    myCmd.Parameters.Add(Database.Parameter(myProv, "EventEnd", DBNull.Value));
                else
                    myCmd.Parameters.Add(Database.Parameter(myProv, "EventEnd", result.EventEnd));


                myCmd.ExecuteNonQuery();
            }
            finally
            {
                myConn.Dispose();
            }
            return result;
        }

        public override T Insert(T newObj)
        {
            return Insert(newObj, false);
        }

        /// <summary>
        /// delete a group of events
        /// </summary>
        /// <param name="groupId"></param>
        /// <returns>number of records affected</returns>
        [DataObjectMethod(DataObjectMethodType.Delete, false)]
        public int DeleteByGroupId(int groupId)
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

                sSql = "DELETE FROM [" + this.TableName + "] WHERE GroupId = @GroupId ";
                myCmd.CommandText = Database.ParseSql(sSql);
                myCmd.Parameters.Add(Database.Parameter(myProv, "GroupId", groupId));
                res = myCmd.ExecuteNonQuery();
            }
            finally
            {
                myConn.Dispose();
            }
            return res;
        }

        /// <summary>
        /// delete events linked to OrderId
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns>number of records affected</returns>
        [DataObjectMethod(DataObjectMethodType.Delete, false)]
        public int DeleteByOrderId(int orderId)
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

                sSql = "DELETE FROM [" + this.TableName + "] WHERE OrderId = @OrderId ";
                myCmd.CommandText = Database.ParseSql(sSql);
                myCmd.Parameters.Add(Database.Parameter(myProv, "OrderId", orderId));
                res = myCmd.ExecuteNonQuery();
            }
            finally
            {
                myConn.Dispose();
            }
            return res;
        }

        protected override void FillObject(T result, DbDataReader myRd)
        {
            if (!Convert.IsDBNull(myRd["Id"]))
                result.Id = (int)myRd["Id"];
            if (!Convert.IsDBNull(myRd["Name"]))
                result.Name = (string)myRd["Name"];
            if (!Convert.IsDBNull(myRd["EventStart"]))
                result.EventStart = (DateTime)myRd["EventStart"];
            if (!Convert.IsDBNull(myRd["EventEnd"]))
                result.EventEnd = (DateTime)myRd["EventEnd"];
            if (!Convert.IsDBNull(myRd["ResourceId"]))
                result.ResourceId = (int)myRd["ResourceId"];
            if (!Convert.IsDBNull(myRd["Status"]))
                result.Status = (Event.EventStatusEnum)int.Parse(myRd["Status"].ToString());
            if (!Convert.IsDBNull(myRd["GroupId"]))
                result.GroupId = (int)myRd["GroupId"];
            if (!Convert.IsDBNull(myRd["OrderId"]))
                result.OrderId = (int)myRd["OrderId"];
            if (!Convert.IsDBNull(myRd["Description"]))
                result.Description = (string)myRd["Description"];
        }
    }
}