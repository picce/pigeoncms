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
    public class ShipZonesWeightManager : TableManager<ShipZonesWeight, ShipZonesWeightFilter, int>, ITableManager
    {
        [DebuggerStepThrough()]
        public ShipZonesWeightManager()
        {
            this.TableName = "#__shop_shipZonesWeight";
            this.KeyFieldName = "Id";
        }

        public override List<ShipZonesWeight> GetByFilter(ShipZonesWeightFilter filter, string sort)
        {
            DbProviderFactory myProv = Database.ProviderFactory;
            DbConnection myConn = myProv.CreateConnection();

            var p = new DynamicParameters();
            string sSql;
            var result = new List<ShipZonesWeight>();

            try
            {
                myConn.ConnectionString = Database.ConnString;
                myConn.Open();

                sSql = "SELECT t.Id, t.ZoneCode, t.WeightFrom, t.WeightTo, t.ShippingPrice"
                    + " FROM [" + this.TableName + "] t "
                    + " WHERE 1 = 1";

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
                if (filter.WeightFrom > 0)
                {
                    sSql += " AND t.WeightFrom = @WeightFrom ";
                    p.Add("WeightFrom", filter.WeightFrom, null, null, null);
                }
                if (filter.WeightTo > 0 && filter.WeightTo > filter.WeightFrom)
                {
                    sSql += " AND t.WeightTo = @WeightTo ";
                    p.Add("WeightTo", filter.WeightTo, null, null, null);
                }

                result = (List<ShipZonesWeight>)myConn.Query<ShipZonesWeight>(Database.ParseSql(sSql), p);
            }
            finally
            {
                myConn.Dispose();
            }
            return result;
        }

        public override ShipZonesWeight GetByKey(int id)
        {
            var result = new ShipZonesWeight();
            var list = new List<ShipZonesWeight>();
            var filter = new ShipZonesWeightFilter();
            if (id > 0)
            {
                filter.Id = id;
                list = this.GetByFilter(filter, "");
                if (list.Count > 0)
                    result = list[0];
            }
            return result;
        }

        public override int Update(ShipZonesWeight theObj)
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
                + " SET ZoneCode=@ZoneCode, WeightFrom=@WeightFrom, "
                + " WeightTo=@WeightTo, ShippingPrice=@ShippingPrice"
                + " WHERE " + this.KeyFieldName + " = @Id";
                
                p.Add("Id", theObj.Id, null, null, null);
                p.Add("ZoneCode", theObj.ZoneCode, null, null, null);
                p.Add("WeightFrom", theObj.WeightFrom, null, null, null);
                p.Add("WeightTo", theObj.WeightTo, null, null, null);
                p.Add("ShippingPrice", theObj.ShippingPrice, null, null, null);
                
                result = myConn.Execute(Database.ParseSql(sSql), p);
            }
            finally
            {
                myConn.Dispose();
            }
            return result;
        }

        public override ShipZonesWeight Insert(ShipZonesWeight newObj)
        {
            DbProviderFactory myProv = Database.ProviderFactory;
            DbConnection myConn = myProv.CreateConnection();
            var p = new DynamicParameters();
            string sSql;
            var result = new ShipZonesWeight();

            try
            {
                myConn.ConnectionString = Database.ConnString;
                myConn.Open();

                result.ZoneCode = newObj.ZoneCode;
                result.WeightFrom = newObj.WeightFrom;
                result.WeightTo = newObj.WeightTo;
                result.ShippingPrice = newObj.ShippingPrice;

                sSql = "INSERT INTO " + this.TableName + "(ZoneCode, WeightFrom, WeightTo, ShippingPrice) "
                + " VALUES(@ZoneCode, @WeightFrom, @WeightTo, @ShippingPrice) "
                + " SELECT SCOPE_IDENTITY() ";

                p.Add("ZoneCode", result.ZoneCode, null, null, null);
                p.Add("WeightFrom", result.WeightFrom, null, null, null);
                p.Add("WeightTo", result.WeightTo, null, null, null);
                p.Add("ShippingPrice", result.ShippingPrice, null, null, null);
                myConn.ExecuteScalar(Database.ParseSql(sSql), p, null, null, null);
            }
            finally
            {
                myConn.Dispose();
            }
            return result;
        }

        //TOREMOVE
        //public override int DeleteById(int id)
        //{
        //    DbProviderFactory myProv = Database.ProviderFactory;
        //    DbConnection myConn = myProv.CreateConnection();
        //    var p = new DynamicParameters();
        //    string sSql;
        //    int res = 0;

        //    try
        //    {
        //        myConn.ConnectionString = Database.ConnString;
        //        myConn.Open();

        //        sSql = "DELETE FROM " + this.TableName + " WHERE " + this.KeyFieldName + " = @Id ";
        //        p.Add("Id", id, null, null, null);

        //        myConn.Execute(Database.ParseSql(sSql), p);
        //    }
        //    finally
        //    {
        //        myConn.Dispose();
        //    }
        //    return res;
        //}

    }
}
