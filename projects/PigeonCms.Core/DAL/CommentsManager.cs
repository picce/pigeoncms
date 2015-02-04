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
    /// DAL for Comment obj (in table #__comments)
    /// </summary>
    public class CommentsManager : TableManager<CommentItem, CommentFilter, int>, ITableManager
    {
        [DebuggerStepThrough()]
        public CommentsManager()
        {
            this.TableName = "#__comments";
            this.KeyFieldName = "Id";
        }

        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public override List<CommentItem> GetByFilter(CommentFilter filter, string sort)
        {
            DbProviderFactory myProv = Database.ProviderFactory;
            DbConnection myConn = myProv.CreateConnection();
            DbDataReader myRd = null;
            DbCommand myCmd = myConn.CreateCommand();
            string sSql;
            var result = new List<CommentItem>();

            try
            {
                myConn.ConnectionString = Database.ConnString;
                myConn.Open();
                myCmd.Connection = myConn;

                sSql = "SELECT t.Id, t.GroupId, t.DateInserted, t.UserInserted, t.UserHostAddress, "
                + " t.Name, t.Email, t.Comment, t.Status "
                + " FROM ["+ this.TableName +"] t "
                + " WHERE t.Id > 0 ";
                if (filter.Id > 0 || filter.Id == -1)
                {
                    sSql += " AND t.Id = @Id ";
                    myCmd.Parameters.Add(Database.Parameter(myProv, "Id", filter.Id));
                }
                if (filter.GroupId > 0)
                {
                    sSql += " AND t.GroupId = @GroupId ";
                    myCmd.Parameters.Add(Database.Parameter(myProv, "GroupId", filter.GroupId));
                }
                if (!string.IsNullOrEmpty(filter.UserInserted))
                {
                    sSql += " AND t.UserInserted = @UserInserted ";
                    myCmd.Parameters.Add(Database.Parameter(myProv, "UserInserted", filter.UserInserted));
                }
                if (!string.IsNullOrEmpty(filter.Name))
                {
                    sSql += " AND t.Name = @Name ";
                    myCmd.Parameters.Add(Database.Parameter(myProv, "Name", filter.Name));
                }
                if (!string.IsNullOrEmpty(filter.Email))
                {
                    sSql += " AND t.Email = @Email ";
                    myCmd.Parameters.Add(Database.Parameter(myProv, "Email", filter.Email));
                }
                if (!string.IsNullOrEmpty(filter.UserHostAddressPart))
                {
                    sSql += " AND t.UserHostAddressPart like @UserHostAddressPart ";
                    myCmd.Parameters.Add(Database.Parameter(myProv, "UserHostAddressPart", "%" + filter.UserHostAddressPart + "%"));
                }
                if (filter.FilterStatus)
                {
                    sSql += " AND t.Status = @Status ";
                    myCmd.Parameters.Add(Database.Parameter(myProv, "Status", (int)filter.Status));
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
                    CommentItem item = new CommentItem();
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
        public override CommentItem GetByKey(int id)
        {
            CommentItem result = new CommentItem();
            var list = new List<CommentItem>();
            CommentFilter filter = new CommentFilter();
            filter.Id = id;
            list = GetByFilter(filter, "");
            if (list.Count > 0)
                result = list[0];
            return result;
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<CommentItem> GetByGroupId(int groupId)
        {
            var result = new List<CommentItem>();
            var filter = new CommentFilter();
            filter.GroupId = groupId;
            result = GetByFilter(filter, "");
            return result;
        }

        [DataObjectMethod(DataObjectMethodType.Update, false)]
        public override int Update(CommentItem theObj)
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
                + " SET GroupId=@GroupId, UserHostAddress=@UserHostAddress, "
                + " Name=@Name, Email=@Email, Comment=@Comment, Status=@Status"
                + " WHERE Id = @Id";
                myCmd.CommandText = Database.ParseSql(sSql);
                myCmd.Parameters.Add(Database.Parameter(myProv, "Id", theObj.Id));
                myCmd.Parameters.Add(Database.Parameter(myProv, "GroupId", theObj.GroupId));
                myCmd.Parameters.Add(Database.Parameter(myProv, "UserHostAddress", theObj.UserHostAddress));
                myCmd.Parameters.Add(Database.Parameter(myProv, "Name", theObj.Name));
                myCmd.Parameters.Add(Database.Parameter(myProv, "Email", theObj.Email));
                myCmd.Parameters.Add(Database.Parameter(myProv, "Comment", theObj.Comment));
                myCmd.Parameters.Add(Database.Parameter(myProv, "Status", (int)theObj.Status));

                result = myCmd.ExecuteNonQuery();
            }
            finally
            {
                myConn.Dispose();
            }
            return result;
        }

        [DataObjectMethod(DataObjectMethodType.Insert, false)]
        public override CommentItem Insert(CommentItem newObj)
        {
            DbProviderFactory myProv = Database.ProviderFactory;
            DbConnection myConn = myProv.CreateConnection();
            DbCommand myCmd = myConn.CreateCommand();
            string sSql;
            var result = new PigeonCms.CommentItem();

            try
            {
                myConn.ConnectionString = Database.ConnString;
                myConn.Open();
                myCmd.Connection = myConn;

                result = newObj;
                result.DateInserted = DateTime.Now;
                result.UserInserted = PgnUserCurrent.UserName;
                result.Id = base.GetNextId();
                if (result.GroupId == 0)
                    result.GroupId = base.GetNextProgressive(this.TableName, "GroupId");

                sSql = "INSERT INTO [" + this.TableName + "]"
                + "(Id, GroupId, DateInserted, UserInserted, UserHostAddress, "
                + " Name, Email, Comment, Status) "
                + " VALUES(@Id, @GroupId, @DateInserted, @UserInserted,@UserHostAddress, "
                + " @Name, @Email, @Comment, @Status) ";
                myCmd.CommandText = Database.ParseSql(sSql);

                myCmd.Parameters.Add(Database.Parameter(myProv, "Id", result.Id));
                myCmd.Parameters.Add(Database.Parameter(myProv, "GroupId", result.GroupId));
                myCmd.Parameters.Add(Database.Parameter(myProv, "DateInserted", result.DateInserted));
                myCmd.Parameters.Add(Database.Parameter(myProv, "UserInserted", result.UserInserted));
                myCmd.Parameters.Add(Database.Parameter(myProv, "UserHostAddress", result.UserHostAddress));
                myCmd.Parameters.Add(Database.Parameter(myProv, "Name", result.Name));
                myCmd.Parameters.Add(Database.Parameter(myProv, "Email", result.Email));
                myCmd.Parameters.Add(Database.Parameter(myProv, "Comment", result.Comment));
                myCmd.Parameters.Add(Database.Parameter(myProv, "Status", (int)result.Status));

                myCmd.ExecuteNonQuery();
            }
            finally
            {
                myConn.Dispose();
            }
            return result;
        }

        [DataObjectMethod(DataObjectMethodType.Delete, false)]
        public int DeleteByGroupId(int groupId)
        {
            DbProviderFactory myProv = Database.ProviderFactory;
            DbConnection myConn = myProv.CreateConnection();
            DbCommand myCmd = myProv.CreateCommand();
            string sSql;
            int res = 0;

            try
            {
                myConn.ConnectionString = Database.ConnString;
                myConn.Open();
                myCmd.Connection = myConn;

                sSql = "DELETE FROM ["+ this.TableName +"] "
                + " WHERE GroupId=@GroupId ";
                myCmd.Parameters.Add(Database.Parameter(myProv, "GroupId", groupId));
                myCmd.CommandText = Database.ParseSql(sSql);
                res = myCmd.ExecuteNonQuery();
            }
            finally
            {
                myConn.Dispose();
            }
            return res;
        }

        protected override void FillObject(CommentItem result, DbDataReader myRd)
        {
            if (!Convert.IsDBNull(myRd["Id"]))
                result.Id = (int)myRd["Id"];
            if (!Convert.IsDBNull(myRd["GroupId"]))
                result.GroupId = (int)myRd["GroupId"];
            if (!Convert.IsDBNull(myRd["DateInserted"]))
                result.DateInserted = (DateTime)myRd["DateInserted"];
            if (!Convert.IsDBNull(myRd["UserInserted"]))
                result.UserInserted = (string)myRd["UserInserted"];
            if (!Convert.IsDBNull(myRd["Name"]))
                result.Name = (string)myRd["Name"];
            if (!Convert.IsDBNull(myRd["Email"]))
                result.Email = (string)myRd["Email"];
            if (!Convert.IsDBNull(myRd["UserHostAddress"]))
                result.UserHostAddress = (string)myRd["UserHostAddress"];
            if (!Convert.IsDBNull(myRd["Comment"]))
                result.Comment = (string)myRd["Comment"];
            if (!Convert.IsDBNull(myRd["Status"]))
                result.Status = (CommentStatus)int.Parse(myRd["Status"].ToString());
        }
    }
}