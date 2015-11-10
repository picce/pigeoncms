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
    public class ShipGeoZonesManager : TableManager<ShipGeoZones, ShipGeoZonesFilter, int>, ITableManager
    {
        [DebuggerStepThrough()]
        public ShipGeoZonesManager()
        {
            this.TableName = "#__shop_shipGeoZones";
            this.KeyFieldName = "Id";
        }

        public override List<ShipGeoZones> GetByFilter(ShipGeoZonesFilter filter, string sort)
        {
            DbProviderFactory myProv = Database.ProviderFactory;
            DbConnection myConn = myProv.CreateConnection();

            var p = new DynamicParameters();
            string sSql;
            var result = new List<ShipGeoZones>();

            try
            {
                myConn.ConnectionString = Database.ConnString;
                myConn.Open();

                sSql = "SELECT t.Id, t.ZoneCode, t.CountryCode, t.CityCode, t.Continent"
                    + " FROM [" + this.TableName + "] t "
                    + " WHERE 1=1 ";

                if (filter.Id > 0)
                {
                    sSql += " AND t.Id = @Id ";
                    p.Add("Id", filter.Id, null, null, null);
                }
                if (!string.IsNullOrEmpty(filter.ZoneCode))
                {
                    sSql += " AND t.ZoneCode = @ZoneCode ";
                    p.Add("ZoneCode", filter.ZoneCode, null, null, null);
                }
                if (!string.IsNullOrEmpty(filter.CountryCode))
                {
                    sSql += " AND t.CountryCode = @CountryCode ";
                    p.Add("CountryCode", filter.CountryCode, null, null, null);
                }
                if (!string.IsNullOrEmpty(filter.CityCode))
                {
                    sSql += " AND t.CityCode = @CityCode ";
                    p.Add("CityCode", filter.CityCode, null, null, null);
                }
                if (!string.IsNullOrEmpty(filter.Continent))
                {
                    sSql += " AND t.Continent = @Continent ";
                    p.Add("Continent", filter.Continent, null, null, null);
                }
                result = (List<ShipGeoZones>)myConn.Query<ShipGeoZones>(Database.ParseSql(sSql), p);
            }
            finally
            {
                myConn.Dispose();
            }
            return result;
        }

        public override ShipGeoZones GetByKey(int id)
        {
            var result = new ShipGeoZones();
            var list = new List<ShipGeoZones>();
            var filter = new ShipGeoZonesFilter();
            if (id > 0)
            {
                filter.Id = id;
                list = this.GetByFilter(filter, "");
                if (list.Count > 0)
                    result = list[0];
            }
            return result;
        }

        public override int Update(ShipGeoZones theObj)
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

                sSql = "UPDATE " + this.TableName
                + " SET ZoneCode=@ZoneCode, CountryCode=@CountryCode, "
                + " CityCode=@CityCode, Continent=@Continent"
                + " WHERE " + this.KeyFieldName + " = @Id";
                p.Add("Id", theObj.Id, null, null, null);
                p.Add("ZoneCode", theObj.ZoneCode, null, null, null);
                p.Add("CountryCode", theObj.CountryCode, null, null, null);
                p.Add("CityCode", theObj.CityCode, null, null, null);
                p.Add("Continent", theObj.Continent, null, null, null);
                result = myConn.Execute(Database.ParseSql(sSql), p);
            }
            finally
            {
                myConn.Dispose();
            }
            return result;
        }

        public override ShipGeoZones Insert(ShipGeoZones newObj)
        {
            DbProviderFactory myProv = Database.ProviderFactory;
            DbConnection myConn = myProv.CreateConnection();
            var p = new DynamicParameters();
            string sSql;
            var result = new ShipGeoZones();

            try
            {
                myConn.ConnectionString = Database.ConnString;
                myConn.Open();

                result.ZoneCode = newObj.ZoneCode;
                result.CountryCode = newObj.CountryCode;
                result.CityCode = newObj.CityCode;
                result.Continent = newObj.Continent;

                sSql = "INSERT INTO " + this.TableName + "(ZoneCode, CountryCode, CityCode, Continent) "
                + " VALUES(@ZoneCode, @CountryCode, @CityCode, @Continent) "
                + " SELECT SCOPE_IDENTITY() ";

                p.Add("ZoneCode", result.ZoneCode, null, null, null);
                p.Add("CountryCode", result.CountryCode, null, null, null);
                p.Add("CityCode", result.CityCode, null, null, null);
                p.Add("Continent", result.Continent, null, null, null);
                myConn.ExecuteScalar(Database.ParseSql(sSql), p, null, null, null);
            }
            finally
            {
                myConn.Dispose();
            }
            return result;
        }

        public override int DeleteById(int id)
        {
            DbProviderFactory myProv = Database.ProviderFactory;
            DbConnection myConn = myProv.CreateConnection();
            var p = new DynamicParameters();

            string sSql;
            int res = 0;

            try
            {
                var currObj = this.GetByKey(id);

                myConn.ConnectionString = Database.ConnString;
                myConn.Open();

                sSql = "DELETE FROM " + this.TableName + " WHERE " + this.KeyFieldName + " = @Id ";
                p.Add("Id", id, null, null, null);

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
