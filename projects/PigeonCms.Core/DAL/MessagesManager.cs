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
    /// DAL for Message obj (in table #__messages)
    /// </summary>
    public class MessagesManager : TableManager<Message, MessagesFilter, int>, ITableManager
    {

        private bool checkOwnerUser = true;
        public bool CheckOwnerUser
        {
            get { return checkOwnerUser; }
        }

        /// <summary>
        /// CheckOwnerUser=true
        /// </summary>
        [DebuggerStepThrough()]
        public MessagesManager(): this(true)
        { }

        [DebuggerStepThrough()]
        public MessagesManager(bool checkOwnerUser)
        {
            this.checkOwnerUser = checkOwnerUser;
            this.TableName = "#__messages";
            this.KeyFieldName = "Id";
        }

        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public override List<Message> GetByFilter(MessagesFilter filter, string sort)
        {
            DbProviderFactory myProv = Database.ProviderFactory;
            DbConnection myConn = myProv.CreateConnection();
            DbDataReader myRd = null;
            DbCommand myCmd = myConn.CreateCommand();
            string sSql;
            string sTopRows = "";
            var result = new List<Message>();

            try
            {
                myConn.ConnectionString = Database.ConnString;
                myConn.Open();
                myCmd.Connection = myConn;

                if (this.CheckOwnerUser)
                {
                    if (PgnUserCurrent.IsAuthenticated)
                        filter.OwnerUser = PgnUserCurrent.UserName;
                    else
                        filter.Id = -1; //no results
                }

                if (filter.TopRows > 0)
                    sTopRows = "TOP " + filter.TopRows;

                sSql = "SELECT "+ sTopRows +" t.Id, t.OwnerUser, t.FromUser, "
                    + " t.ToUser, t.Title, t.Description, "
                    + " t.DateInserted, t.Priority, t.IsRead, t.IsStarred, "
                    + " t.Visible, t.ItemId, t.ModuleId "
                    + " FROM ["+ this.TableName +"] t "
                    + " WHERE t.Id > 0 ";
                if (filter.Id > 0 || filter.Id == -1)
                {
                    sSql += " AND t.Id = @Id ";
                    myCmd.Parameters.Add(Database.Parameter(myProv, "Id", filter.Id));
                }
                if (!string.IsNullOrEmpty(filter.OwnerUser))
                {
                    sSql += " AND t.OwnerUser = @OwnerUser ";
                    myCmd.Parameters.Add(Database.Parameter(myProv, "OwnerUser", filter.OwnerUser));
                }
                if (!string.IsNullOrEmpty(filter.FromUser))
                {
                    sSql += " AND t.FromUser = @FromUser ";
                    myCmd.Parameters.Add(Database.Parameter(myProv, "FromUser", filter.FromUser));
                }
                if (!string.IsNullOrEmpty(filter.ToUserLike))
                {
                    sSql += " AND t.ToUser like @ToUserLike ";
                    myCmd.Parameters.Add(Database.Parameter(myProv, "ToUserLike", "%" + filter.ToUserLike + "%"));
                }
                //datesrange filter
                sSql += " AND ("
                     + Database.AddDatesRangeParameters(myCmd.Parameters, myProv, "t.DateInserted", filter.DateInsertedRange)
                     + ")";

                if (filter.ItemId > 0)
                {
                    sSql += " AND t.ItemId = @ItemId ";
                    myCmd.Parameters.Add(Database.Parameter(myProv, "ItemId", filter.ItemId));
                }
                if (filter.ModuleId > 0)
                {
                    sSql += " AND t.ModuleId = @ModuleId ";
                    myCmd.Parameters.Add(Database.Parameter(myProv, "ModuleId", filter.ModuleId));
                }

                if (!string.IsNullOrEmpty(filter.TitleSearch))
                {
                    sSql += " AND t.Title like @TitleSearch ";
                    myCmd.Parameters.Add(Database.Parameter(myProv, "TitleSearch", "%" + filter.TitleSearch + "%"));
                }
                if (!string.IsNullOrEmpty(filter.DescriptionSearch))
                {
                    sSql += " AND t.Description like @DescriptionSearch ";
                    myCmd.Parameters.Add(Database.Parameter(myProv, "DescriptionSearch", "%" + filter.DescriptionSearch + "%"));
                }
                if (!string.IsNullOrEmpty(filter.FullSearch))
                {
                    sSql += " AND (c.Title like @TitleSearch OR c.Description like @DescriptionSearch) ";
                    myCmd.Parameters.Add(Database.Parameter(myProv, "TitleSearch", "%" + filter.FullSearch + "%"));
                    myCmd.Parameters.Add(Database.Parameter(myProv, "DescriptionSearch", "%" + filter.FullSearch + "%"));
                }
                if (filter.IsRead != Utility.TristateBool.NotSet)
                {
                    sSql += " AND t.IsRead = @IsRead ";
                    myCmd.Parameters.Add(Database.Parameter(myProv, "IsRead", filter.IsRead));
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
                    var item = new Message();
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
        public override Message GetByKey(int id)
        {
            var result = new Message();
            var list = new List<Message>();
            var filter = new MessagesFilter();
            filter.Id = id == 0 ? -1 : id;
            filter.TopRows = 0;
            filter.DateInsertedRange = new DatesRange();
            list = GetByFilter(filter, "");
            if (list.Count > 0)
                result = list[0];
            return result;
        }


        public override int Update(Message theObj)
        {
            DbProviderFactory myProv = Database.ProviderFactory;
            DbConnection myConn = myProv.CreateConnection();
            DbCommand myCmd = myConn.CreateCommand();
            string sSql;
            int result = 0;

            if (theObj.DateInserted == DateTime.MinValue)
                theObj.DateInserted = DateTime.Now;

            try
            {
                myConn.ConnectionString = Database.ConnString;
                myConn.Open();
                myCmd.Connection = myConn;

                sSql = "UPDATE [" + this.TableName + "] "
                + " SET Priority=@Priority, IsRead=@IsRead, "
                + " IsStarred=@IsStarred, Visible=@Visible "
                + " WHERE Id = @Id";
                myCmd.CommandText = Database.ParseSql(sSql);
                myCmd.Parameters.Add(Database.Parameter(myProv, "Id", theObj.Id));
                myCmd.Parameters.Add(Database.Parameter(myProv, "Priority", theObj.Priority));
                myCmd.Parameters.Add(Database.Parameter(myProv, "IsRead", theObj.IsRead));
                myCmd.Parameters.Add(Database.Parameter(myProv, "IsStarred", theObj.IsStarred));
                myCmd.Parameters.Add(Database.Parameter(myProv, "Visible", theObj.Visible));

                result = myCmd.ExecuteNonQuery();
            }
            finally
            {
                myConn.Dispose();
            }
            return result;
        }


        [DataObjectMethod(DataObjectMethodType.Insert, false)]
        public override Message Insert(Message newObj)
        {
            DbProviderFactory myProv = Database.ProviderFactory;
            DbConnection myConn = myProv.CreateConnection();
            DbCommand myCmd = myConn.CreateCommand();
            string sSql;
            var result = new PigeonCms.Message();

            try
            {
                myConn.ConnectionString = Database.ConnString;
                myConn.Open();
                myCmd.Connection = myConn;

                result = newObj;
                result.Id = base.GetNextId();
                result.DateInserted = DateTime.Now;

                sSql = "INSERT INTO [" + this.TableName + "]"
                    + "(Id, OwnerUser, FromUser, ToUser, Title, Description, "
                    + " DateInserted, Priority, IsRead, IsStarred, Visible, ItemId, ModuleId) "
                    + " VALUES(@Id, @OwnerUser, @FromUser, @ToUser, @Title, @Description, "
                    + " @DateInserted, @Priority, @IsRead, @IsStarred, @Visible, @ItemId, @ModuleId) ";
                myCmd.CommandText = Database.ParseSql(sSql);

                myCmd.Parameters.Add(Database.Parameter(myProv, "Id", result.Id));
                myCmd.Parameters.Add(Database.Parameter(myProv, "OwnerUser", result.OwnerUser));
                myCmd.Parameters.Add(Database.Parameter(myProv, "FromUser", result.FromUser));
                myCmd.Parameters.Add(Database.Parameter(myProv, "ToUser", result.ToUser));
                myCmd.Parameters.Add(Database.Parameter(myProv, "Title", Utility.Html.GetTextPreview(result.Title, 200, "..")));
                myCmd.Parameters.Add(Database.Parameter(myProv, "Description", result.Description));
                myCmd.Parameters.Add(Database.Parameter(myProv, "DateInserted", result.DateInserted));
                myCmd.Parameters.Add(Database.Parameter(myProv, "Priority", result.Priority));
                myCmd.Parameters.Add(Database.Parameter(myProv, "IsRead", result.IsRead));
                myCmd.Parameters.Add(Database.Parameter(myProv, "IsStarred", result.IsStarred));
                myCmd.Parameters.Add(Database.Parameter(myProv, "Visible", result.Visible));
                myCmd.Parameters.Add(Database.Parameter(myProv, "ItemId", result.ItemId));
                myCmd.Parameters.Add(Database.Parameter(myProv, "ModuleId", result.ModuleId));

                myCmd.ExecuteNonQuery();
            }
            finally
            {
                myConn.Dispose();
            }
            return result;
        }

        public override int DeleteById(int id)
        {
            int res = 0;
            bool delete = true;
            if (this.CheckOwnerUser)
            {
                var message = this.GetByKey(id);
                if (message.Id == 0)
                    delete = false;
            }

            if (delete)
                res = base.DeleteById(id);
            return res;
        }

        protected override void FillObject(Message result, DbDataReader myRd)
        {
            if (!Convert.IsDBNull(myRd["Id"]))
                result.Id = (int)myRd["Id"];
            if (!Convert.IsDBNull(myRd["OwnerUser"]))
                result.OwnerUser = (string)myRd["OwnerUser"];
            if (!Convert.IsDBNull(myRd["FromUser"]))
                result.FromUser = (string)myRd["FromUser"];
            if (!Convert.IsDBNull(myRd["ToUser"]))
                result.ToUser = (string)myRd["ToUser"];
            if (!Convert.IsDBNull(myRd["Title"]))
                result.Title = (string)myRd["Title"];
            if (!Convert.IsDBNull(myRd["Description"]))
                result.Description = (string)myRd["Description"];
            if (!Convert.IsDBNull(myRd["DateInserted"]))
                result.DateInserted = (DateTime)myRd["DateInserted"];
            if (!Convert.IsDBNull(myRd["Priority"]))
                result.Priority = (int)myRd["Priority"];
            if (!Convert.IsDBNull(myRd["IsRead"]))
                result.IsRead = (bool)myRd["IsRead"];
            if (!Convert.IsDBNull(myRd["IsStarred"]))
                result.IsStarred = (bool)myRd["IsStarred"];
            if (!Convert.IsDBNull(myRd["Visible"]))
                result.Visible = (bool)myRd["Visible"];
            if (!Convert.IsDBNull(myRd["ItemId"]))
                result.ItemId = (int)myRd["ItemId"];
            if (!Convert.IsDBNull(myRd["ModuleId"]))
                result.ModuleId = (int)myRd["ModuleId"];
        }
    }
}