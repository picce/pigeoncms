using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Diagnostics;
using System.ComponentModel;
using System.Data.Common;
using StackExchange.Dapper;
using System.Collections.Generic;

namespace PigeonCms
{
    /// <summary>
    /// meta info about user
    /// </summary>
    public class PngUserMeta : ITable
    {
        [DataObjectField(true)]
        public int Id { get; set; } = 0;

        public string Username { get; set; } = "";

        public string MetaKey { get; set; } = "";

        public string MetaValue { get; set; } = "";
    }

    public class PngUserMetaFilter : ITable
    {
        [DataObjectField(true)]
        public int Id { get; set; } = 0;

        public string Username { get; set; } = "";

        public string MetaKey { get; set; } = "";

        public string MetaValue { get; set; } = "";

        public string MetaValueLike { get; set; } = "";

    }

    public class PgnUserMetaManager :
        TableManager<PngUserMeta, PngUserMetaFilter, int>, ITableManager
    {
        [DebuggerStepThrough()]
        public PgnUserMetaManager()
        {
            this.TableName = "#__memberUsers_Meta";
            this.KeyFieldName = "Id";
        }

        public override List<PngUserMeta> GetByFilter(PngUserMetaFilter filter, string sort)
        {
            DbProviderFactory myProv = Database.ProviderFactory;
            DbConnection myConn = myProv.CreateConnection();
            var p = new DynamicParameters();
            string sSql;
            var result = new List<PngUserMeta>();

            try
            {
                myConn.ConnectionString = Database.ConnString;
                myConn.Open();

                sSql = "SELECT Id, Username, MetaKey, MetaValue "
                    + " FROM [" + this.TableName + "] t "
                    + " WHERE 1=1 ";
                if (filter.Id > 0 || filter.Id == -1)
                {
                    sSql += " AND t.Id = @Id ";
                    p.Add("Id", filter.Id, null, null, null);
                }
                if (!string.IsNullOrEmpty(filter.Username))
                {
                    sSql += " AND t.Username = @Username ";
                    p.Add("Username", filter.Username, null, null, null);
                }
                if (!string.IsNullOrEmpty(filter.MetaKey))
                {
                    sSql += " AND t.MetaKey = @MetaKey ";
                    p.Add("MetaKey", filter.MetaKey, null, null, null);
                }
                if (!string.IsNullOrEmpty(filter.MetaValue))
                {
                    sSql += " AND t.MetaValue = @MetaValue ";
                    p.Add("MetaValue", filter.MetaValue, null, null, null);
                }
                if (!string.IsNullOrEmpty(filter.MetaValueLike))
                {
                    sSql += " AND t.MetaValue like @MetaValueLike ";
                    p.Add("MetaValueLike", "%" + filter.MetaValueLike + "%", null, null, null);
                }

                if (!string.IsNullOrEmpty(sort))
                {
                    sSql += " ORDER BY " + sort;
                }
                else
                {
                    sSql += " ORDER BY t.Username, t.MetaKey ";
                }

                result = (List<PngUserMeta>)myConn.Query<PngUserMeta>(Database.ParseSql(sSql), p);
            }
            finally
            {
                myConn.Dispose();
            }
            return result;
        }

        public override PngUserMeta GetByKey(int id)
        {
            var result = new PngUserMeta();
            var list = new List<PngUserMeta>();
            var filter = new PngUserMetaFilter();

            if (id <= 0)
                return result;

            filter.Id = id;
            list = GetByFilter(filter, "");
            if (list.Count > 0)
                result = list[0];

            return result;
        }

        public PngUserMeta GetByKey(string username, string metaKey)
        {
            var result = new PngUserMeta();
            var list = new List<PngUserMeta>();
            var filter = new PngUserMetaFilter();

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(metaKey))
                return result;

            filter.Username = username;
            filter.MetaKey = metaKey;
            list = GetByFilter(filter, "");
            if (list.Count > 0)
                result = list[0];

            return result;
        }

        public PngUserMeta GetFirstByMetaKeyValue(string metaKey, string metaValue)
        {
            var result = new PngUserMeta();
            var list = new List<PngUserMeta>();
            var filter = new PngUserMetaFilter();

            if (string.IsNullOrEmpty(metaKey) || string.IsNullOrEmpty(metaValue))
                return result;

            filter.MetaKey = metaKey;
            filter.MetaValue = metaValue;
            list = GetByFilter(filter, "");
            if (list.Count > 0)
                result = list[0];

            return result;
        }

        public int DeleteByUsername(string username)
        {
            if (string.IsNullOrEmpty(username))
                throw new ArgumentException("Invalid username", "username");

            DbProviderFactory myProv = Database.ProviderFactory;
            DbConnection myConn = myProv.CreateConnection();
            var p = new DynamicParameters();
            string sSql;
            int res = 0;

            try
            {
                myConn.ConnectionString = Database.ConnString;
                myConn.Open();

                sSql = "DELETE FROM [" + this.TableName + "] WHERE username = @username ";
                p.Add("username", username, null, null, null);
                res = myConn.Execute(Database.ParseSql(sSql), p);
            }
            finally
            {
                myConn.Dispose();
            }
            return res;
        }

        public int DeleteByKey(string username, string metaKey)
        {
            if (string.IsNullOrEmpty(username))
                throw new ArgumentException("Invalid username", "username");

            if (string.IsNullOrEmpty(metaKey))
                throw new ArgumentException("Invalid metaKey", "metaKey");

            DbProviderFactory myProv = Database.ProviderFactory;
            DbConnection myConn = myProv.CreateConnection();
            var p = new DynamicParameters();
            string sSql;
            int res = 0;

            try
            {
                myConn.ConnectionString = Database.ConnString;
                myConn.Open();

                sSql = "DELETE FROM [" + this.TableName + "] "
                    + " WHERE username = @username "
                    + " AND metaKey = @metaKey ";
                p.Add("username", username, null, null, null);
                p.Add("metaKey", metaKey, null, null, null);
                res = myConn.Execute(Database.ParseSql(sSql), p);
            }
            finally
            {
                myConn.Dispose();
            }
            return res;
        }

        public int UpdateById(int id, string metaValue)
        {
            DbProviderFactory myProv = Database.ProviderFactory;
            DbConnection myConn = myProv.CreateConnection();
            var p = new DynamicParameters();
            string sSql;
            int result = 0;

            if (id <= 0)
                return result;  

            try
            {
                myConn.ConnectionString = Database.ConnString;
                myConn.Open();

                sSql = "UPDATE [" + this.TableName + "] "
                    + " SET MetaValue=@MetaValue "
                    + " WHERE Id = @Id";

                p.Add("MetaValue", metaValue, null, null, null);
                p.Add("Id", id, null, null, null);

                result = myConn.Execute(Database.ParseSql(sSql), p);
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

        public override PngUserMeta Insert(PngUserMeta theObj)
        {
            DbProviderFactory myProv = Database.ProviderFactory;
            DbConnection myConn = myProv.CreateConnection();
            var p = new DynamicParameters();
            string sSql;

            if (string.IsNullOrEmpty(theObj.Username))
                throw new ArgumentNullException("Invalid Username");

            if (string.IsNullOrEmpty(theObj.MetaKey))
                throw new ArgumentNullException("Invalid MetaKey");

            try
            {
                myConn.ConnectionString = Database.ConnString;
                myConn.Open();

                //double check to avoid errors
                this.DeleteByKey(theObj.Username, theObj.MetaKey);

                sSql = "INSERT INTO [" + this.TableName + "] "
                    + " (Username, MetaKey, MetaValue) "
                    + " VALUES(@Username, @MetaKey, @MetaValue) ";

                p.Add("Username", theObj.Username, null, null, null);
                p.Add("MetaKey", theObj.MetaKey, null, null, null);
                p.Add("MetaValue", theObj.MetaValue, null, null, null);

                theObj.Id = myConn.ExecuteScalar<int>(Database.ParseSql(sSql), p, null, null, null);
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                myConn.Dispose();
            }
            return theObj;
        }


    }
}
