using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Diagnostics;
using System.Linq;
using System.Text;
using StackExchange.Dapper;

namespace PigeonCms
{
    public class DbVersionsManager : 
        TableManager<DbVersion, DbVersionsFilter, int>, 
        ITableManager
    {
        private string componentFullName = "";

        [DebuggerStepThrough()]
        public DbVersionsManager(string componentFullName)
        {
            if (string.IsNullOrEmpty(componentFullName))
                throw new ArgumentException("Invalid componentFullName");

            this.TableName = "#__dbVersion";
            this.KeyFieldName = "VersionId";
            this.componentFullName = componentFullName;
        }

        public override List<PigeonCms.DbVersion> GetByFilter(DbVersionsFilter filter, string sort)
        {
            DbProviderFactory myProv = Database.ProviderFactory;
            DbConnection myConn = myProv.CreateConnection();
            var p = new DynamicParameters();
            string sSql;
            var result = new List<PigeonCms.DbVersion>();

            try
            {
                myConn.ConnectionString = Database.ConnString;
                myConn.Open();

                if (!string.IsNullOrEmpty(componentFullName))
                    filter.ComponentFullName = componentFullName;

                sSql = "SELECT componentFullName, versionId, versionDate, "
                + " versionDev, versionNotes, dateUpdated, userUpdated "
                + " FROM [" + this.TableName + "] WHERE 1=1 ";

                if (!string.IsNullOrEmpty(filter.ComponentFullName))
                {
                    sSql += " AND ComponentFullName = @ComponentFullName ";
                    p.Add("ComponentFullName", filter.ComponentFullName, null, null, null);
                }
                if (filter.VersionId > 0)
                {
                    sSql += " AND VersionId = @VersionId ";
                    p.Add("VersionId", filter.VersionId, null, null, null);
                }
                if (!string.IsNullOrEmpty(sort))
                {
                    sSql += " ORDER BY " + sort;
                }
                else
                {
                    sSql += " ORDER BY componentFullName, versionId ";
                }

                result = (List<PigeonCms.DbVersion>)myConn.Query
                    <PigeonCms.DbVersion>(Database.ParseSql(sSql), p);
            }
            finally
            {
                myConn.Dispose();
            }
            return result;
        }

        public int GetLastVersionId()
        {
            DbProviderFactory myProv = Database.ProviderFactory;
            DbConnection myConn = myProv.CreateConnection();
            var p = new DynamicParameters();
            int res = 0;

            try
            {
                myConn.ConnectionString = Database.ConnString;
                myConn.Open();

                string sSql = "SELECT MAX(versionId)maxVersion "
                + " FROM [" + this.TableName + "] "
                + " WHERE ComponentFullName=@ComponentFullName ";

                p.Add("ComponentFullName", componentFullName, null, null, null);
                res = (int)myConn.ExecuteScalar<decimal>(Database.ParseSql(sSql), p, null, null, null);
            }
            finally
            {
                myConn.Dispose();
            }
            
            return res;
        }

        public override PigeonCms.DbVersion GetByKey(int versionId)
        {
            var result = new PigeonCms.DbVersion();
            var list = new List<PigeonCms.DbVersion>();
            var filter = new DbVersionsFilter();

            if (string.IsNullOrEmpty(componentFullName))
                return result;
            if (versionId <= 0)
                return result;

            filter.ComponentFullName = componentFullName;
            filter.VersionId = versionId;
            list = this.GetByFilter(filter, "");
            if (list.Count > 0)
                result = list[0];

            return result;
        }

        public override int Update(DbVersion theObj)
        {
            DbProviderFactory myProv = Database.ProviderFactory;
            DbConnection myConn = myProv.CreateConnection();
            var p = new DynamicParameters();
            string sSql;
            int result = 0;

            theObj.DateUpdated = DateTime.Now;
            if (string.IsNullOrEmpty(theObj.UserUpdated))
                theObj.UserUpdated = PgnUserCurrent.UserName;

            try
            {
                myConn.ConnectionString = Database.ConnString;
                myConn.Open();

                sSql = "UPDATE [" + this.TableName + "] "
                + " SET VersionDate=@VersionDate, "
                + " VersionDev=@VersionDev, VersionNotes=@VersionNotes, "
                + " DateUpdated=@DateUpdated, UserUpdated=@UserUpdated "
                + " WHERE ComponentFullName = @ComponentFullName "
                + " AND VersionId = @VersionId";

                p.Add("ComponentFullName", theObj.ComponentFullName, null, null, null);
                p.Add("VersionId", theObj.VersionId, null, null, null);

                p.Add("VersionDate", theObj.VersionDate, null, null, null);
                p.Add("VersionDev", theObj.VersionDev, null, null, null);
                p.Add("VersionNotes", theObj.VersionNotes, null, null, null);
                p.Add("DateUpdated", theObj.DateUpdated, null, null, null);
                p.Add("UserUpdated", theObj.UserUpdated, null, null, null);

                result = myConn.Execute(Database.ParseSql(sSql), p);
            }
            finally
            {
                myConn.Dispose();
            }
            return result;
        }

        public override DbVersion Insert(DbVersion theObj)
        {
            DbProviderFactory myProv = Database.ProviderFactory;
            DbConnection myConn = myProv.CreateConnection();
            var p = new DynamicParameters();
            string sSql = "";

            theObj.DateUpdated = DateTime.Now;
            if (string.IsNullOrEmpty(theObj.UserUpdated))
                theObj.UserUpdated = PgnUserCurrent.UserName;

            try
            {
                myConn.ConnectionString = Database.ConnString;
                myConn.Open();

                sSql = "INSERT INTO [" + this.TableName + "]"
                + " (ComponentFullName, VersionId, VersionDate, "
                + " VersionDev, VersionNotes, DateUpdated, UserUpdated) "
                + " VALUES(@ComponentFullName, @VersionId, @VersionDate, "
                + " @VersionDev, @VersionNotes, @DateUpdated, @UserUpdated) ";

                p.Add("ComponentFullName", theObj.ComponentFullName, null, null, null);
                p.Add("VersionId", theObj.VersionId, null, null, null);
                p.Add("VersionDate", theObj.VersionDate, null, null, null);
                p.Add("VersionDev", theObj.VersionDev, null, null, null);
                p.Add("VersionNotes", theObj.VersionNotes, null, null, null);
                p.Add("DateUpdated", theObj.DateUpdated, null, null, null);
                p.Add("UserUpdated", theObj.UserUpdated, null, null, null);

                int count = myConn.Execute(Database.ParseSql(sSql), p);
            }
            finally
            {
                myConn.Dispose();
            }
            return theObj;
        }

        public override int DeleteById(int versionId)
        {
            DbProviderFactory myProv = Database.ProviderFactory;
            DbConnection myConn = myProv.CreateConnection();
            var p = new DynamicParameters();

            string sSql;
            int res = 0;

            try
            {
                var currObj = this.GetByKey(versionId);

                myConn.ConnectionString = Database.ConnString;
                myConn.Open();

                sSql = "DELETE FROM [" + this.TableName + "] "
                + " WHERE ComponentFullName = @ComponentFullName " 
                + " AND VersionId = @VersionId ";
                p.Add("ComponentFullName", componentFullName, null, null, null);
                p.Add("VersionId", versionId, null, null, null);

                myConn.Execute(Database.ParseSql(sSql), p);
            }
            finally
            {
                myConn.Dispose();
            }
            return res;
        }

    }
}
