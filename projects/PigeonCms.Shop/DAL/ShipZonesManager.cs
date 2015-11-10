using StackExchange.Dapper;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace PigeonCms.Shop
{
    // with dapper power ;)
    public class ShipZonesManager : TableManager<ShipZones, ShipZonesFilter, int>, ITableManager
    {
        [DebuggerStepThrough()]
        public ShipZonesManager()
        {
            this.TableName = "#__shop_shipZones";
            this.KeyFieldName = "Code";
        }

        public override List<ShipZones> GetByFilter(ShipZonesFilter filter, string sort)
        {
            DbProviderFactory myProv = Database.ProviderFactory;
            DbConnection myConn = myProv.CreateConnection();

            var p = new DynamicParameters();
            string sSql;
            List<ShipZones> result = new List<ShipZones>();

            try
            {
                myConn.ConnectionString = Database.ConnString;
                myConn.Open();

                sSql = "SELECT t.Code, t.Title"
                    + " FROM [" + this.TableName + "] t "
                    + " WHERE 1=1 ";

                if (!string.IsNullOrEmpty(filter.Code))
                {
                    sSql += " AND t.Code = @Code ";
                    p.Add("Code", filter.Code, null, null, null);
                }

                result = (List<ShipZones>)myConn.Query<ShipZones>(Database.ParseSql(sSql), p);
            }
            finally
            {
                myConn.Dispose();
            }
            return result;
        }

        public ShipZones GetByKey(string code)
        {
            var result = new ShipZones();
            var list = new List<ShipZones>();
            var filter = new ShipZonesFilter();
            if (!string.IsNullOrEmpty(code))
            {
                filter.Code = code;
                list = this.GetByFilter(filter, "");
                if (list.Count > 0)
                    result = list[0];
            }
            return result;
        }

        public override int Update(ShipZones theObj)
        {
            DbProviderFactory myProv = Database.ProviderFactory;
            DbConnection myConn = myProv.CreateConnection();
            var p = new DynamicParameters();
            string sSql;
            int result = 0;

            try
            {
                myConn.ConnectionString = Database.ConnString;
                myConn.Open();

                sSql = "UPDATE " + this.TableName + " SET Title=@Title"
                + " WHERE " + this.KeyFieldName + " = @Code";
                p.Add("Code", theObj.Code, null, null, null);
                p.Add("Title", theObj.Title, null, null, null);

                result = myConn.Execute(Database.ParseSql(sSql), p);
            }
            finally
            {
                myConn.Dispose();
            }
            return result;
        }

        public override ShipZones Insert(ShipZones newObj)
        {
            DbProviderFactory myProv = Database.ProviderFactory;
            DbConnection myConn = myProv.CreateConnection();
            var p = new DynamicParameters();
            string sSql;
            var result = new ShipZones();

            try
            {
                myConn.ConnectionString = Database.ConnString;
                myConn.Open();

                result.Code = newObj.Code;
                result.Title = newObj.Title;

                sSql = "INSERT INTO " + this.TableName + "(Code, Title) "
                + " VALUES(@Code, @Title) "
                + " SELECT SCOPE_IDENTITY() ";

                p.Add("Code", result.Code, null, null, null);
                p.Add("Title", result.Title, null, null, null);

                myConn.ExecuteScalar(Database.ParseSql(sSql), p, null, null, null);
            }
            finally
            {
                myConn.Dispose();
            }
            return result;
        }

        public int DeleteById(string code)
        {
            DbProviderFactory myProv = Database.ProviderFactory;
            DbConnection myConn = myProv.CreateConnection();
            var p = new DynamicParameters();

            string sSql;
            int res = 0;

            try
            {
                var currObj = this.GetByKey(code);

                myConn.ConnectionString = Database.ConnString;
                myConn.Open();

                sSql = "DELETE FROM " + this.TableName + " WHERE " + this.KeyFieldName + " = @Code ";
                p.Add("Code", code, null, null, null);

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
