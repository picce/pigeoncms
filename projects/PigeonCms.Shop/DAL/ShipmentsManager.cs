using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StackExchange.Dapper;
using System.Diagnostics;
using System.Data.Common;

namespace PigeonCms.Shop
{
    /// <summary>
    /// DAL for Order obj (table #__shop_shipments)
    /// </summary>
    public class ShipmentsManager : TableManager<Shipment, ShipmentFilter, string>
    {
        [DebuggerStepThrough()]
        public ShipmentsManager()
        {
            this.TableName = "#__shop_shipments";
            this.KeyFieldName = "ShipCode";
        }

        public override List<Shipment> GetByFilter(ShipmentFilter filter, string sort)
        {
            DbProviderFactory myProv = Database.ProviderFactory;
            DbConnection myConn = myProv.CreateConnection();
            var p = new DynamicParameters();
            string sSql;
            var result = new List<Shipment>();

            try
            {
                myConn.ConnectionString = Database.ConnString;
                myConn.Open();

                sSql = "SELECT ShipCode, Name, AssemblyName, Enabled "
                    + " FROM [" + this.TableName + "] t "
                    + " WHERE 1=1 ";
                if (!string.IsNullOrEmpty(filter.ShipCode))
                {
                    sSql += " AND t.ShipCode = @ShipCode ";
                    p.Add("ShipCode", filter.ShipCode, null, null, null);
                }
                if (filter.Enabled != null)
                {
                    sSql += " AND t.Enabled = @Enabled ";
                    p.Add("Enabled", filter.Enabled == true, null, null, null);
                }
                if (!string.IsNullOrEmpty(sort))
                {
                    sSql += " ORDER BY " + sort;
                }
                else
                {
                    sSql += " ORDER BY t.Name ";
                }

                result = (List<Shipment>)myConn.Query<Shipment>(Database.ParseSql(sSql), p);
            }
            finally
            {
                myConn.Dispose();
            }
            return result;
        }

        public override Shipment GetByKey(string shipCode)
        {
            var result = new Shipment();
            var list = new List<Shipment>();
            var filter = new ShipmentFilter();

            if (string.IsNullOrEmpty(shipCode))
                return result;

            filter.ShipCode = shipCode;
            list = GetByFilter(filter, "");
            if (list.Count > 0)
                result = list[0];

            return result;
        }

        public override int Update(Shipment theObj)
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

                sSql = "UPDATE [" + this.TableName + "] "
                    + " SET Name=@Name, AssemblyName=@AssemblyName, CssClass=@CssClass, IsDebug=@IsDebug, "
                    + " Enabled=@Enabled, PayAccount=@PayAccount, "
                    + " PaySubmitUrl=@PaySubmitUrl, PayCallbackUrl=@PayCallbackUrl, "
                    + " SiteOkUrl=@SiteOkUrl, SiteKoUrl=@SiteKoUrl, "
                    + " MinAmount=@MinAmount, MaxAmount=@MaxAmount, PayParams=@PayParams "
                    + " WHERE PayCode = @PayCode";

                p.Add("PayCode", theObj.ShipCode, null, null, null);
                p.Add("Name", theObj.Name, null, null, null);
                p.Add("AssemblyName", theObj.AssemblyName, null, null, null);
                p.Add("Enabled", theObj.Enabled, null, null, null);

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

        public override Shipment Insert(Shipment theObj)
        {
            DbProviderFactory myProv = Database.ProviderFactory;
            DbConnection myConn = myProv.CreateConnection();
            var p = new DynamicParameters();
            string sSql;

            if (string.IsNullOrEmpty(theObj.ShipCode))
                throw new ArgumentNullException("Invalid Payment key field");

            try
            {
                myConn.ConnectionString = Database.ConnString;
                myConn.Open();

                sSql = "INSERT INTO [" + this.TableName + "] "
                    + " (ShipCode, Name, AssemblyName, Enabled) "
                    + " VALUES(@ShipCode, @Name, @AssemblyName, @Enabled) ";

                p.Add("ShipCode", theObj.ShipCode, null, null, null);
                p.Add("Name", theObj.Name, null, null, null);
                p.Add("AssemblyName", theObj.AssemblyName, null, null, null);
                p.Add("Enabled", theObj.Enabled, null, null, null);

                myConn.Execute(Database.ParseSql(sSql), p);
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
