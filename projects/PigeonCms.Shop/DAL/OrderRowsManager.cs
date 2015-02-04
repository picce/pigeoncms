using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.ComponentModel;
using System.Collections.Generic;
using System.IO;
using System.Data.Common;
using PigeonCms;
using System.Diagnostics;
using StackExchange.Dapper;
using System.Collections;


namespace PigeonCms.Shop
{

    /// <summary>
    /// DAL for Order obj (table #__shop_orderRows)
    /// </summary>
    public class OrderRowsManager : TableManager<OrderRow, OrderRowsFilter, int>
    {
        [DebuggerStepThrough()]
        public OrderRowsManager()
        {
            this.TableName = "#__shop_orderRows";
            this.KeyFieldName = "Id";
        }

        public override List<OrderRow> GetByFilter(OrderRowsFilter filter, string sort)
        {
            DbProviderFactory myProv = Database.ProviderFactory;
            DbConnection myConn = myProv.CreateConnection();
            var p = new DynamicParameters();
            string sSql;
            var result = new List<OrderRow>();

            try
            {
                myConn.ConnectionString = Database.ConnString;
                myConn.Open();

                sSql = "SELECT Id, OrderId, ProductCode, Qty, PriceFull, PriceNet "
                + " FROM ["+ this.TableName +"] t "
                + " WHERE 1=1 ";
                if (filter.Id > 0 || filter.Id == -1)
                {
                    sSql += " AND t.Id = @Id ";
                    p.Add("Id", filter.Id, null, null, null);
                }
                if (filter.OrderId > 0 || filter.OrderId == -1)
                {
                    sSql += " AND t.OrderId = @OrderId ";
                    p.Add("OrderId", filter.OrderId, null, null, null);
                }
                if (!string.IsNullOrEmpty(sort))
                {
                    sSql += " ORDER BY " + sort;
                }
                else
                {
                    sSql += " ORDER BY t.Id ";
                }

                result = (List<OrderRow>)myConn.Query<OrderRow>(Database.ParseSql(sSql), p);
            }
            finally
            {
                myConn.Dispose();
            }
            return result;
        }

        public override OrderRow GetByKey(int id)
        {
            var result = new OrderRow();
            var list = new List<OrderRow>();
            var filter = new OrderRowsFilter();
            filter.Id = id == 0 ? -1 : id;
            list = GetByFilter(filter, "");
            if (list.Count > 0)
                result = list[0];
            return result;
        }

        public override int Update(OrderRow theObj)
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
                + " SET OrderId=@OrderId, ProductCode=@ProductCode, Qty=@Qty, "
                + " PriceFull=@PriceFull, PriceNet=@PriceNet "
                + " WHERE Id = @Id";
                
                p.Add("Id", theObj.Id, null, null, null);
                p.Add("OrderId", theObj.OrderId, null, null, null);
                p.Add("ProductCode", theObj.ProductCode, null, null, null);
                p.Add("Qty", theObj.Qty, null, null, null);
                p.Add("PriceFull", theObj.PriceFull, null, null, null);
                p.Add("PriceNet", theObj.PriceNet, null, null, null);
                result = myConn.Execute(Database.ParseSql(sSql), p);

                calculateSummary(theObj.OrderId);
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

        public override OrderRow Insert(OrderRow newObj)
        {
            DbProviderFactory myProv = Database.ProviderFactory;
            DbConnection myConn = myProv.CreateConnection();
            var p = new DynamicParameters();
            string sSql;
            var theObj = new OrderRow();

            try
            {
                myConn.ConnectionString = Database.ConnString;
                myConn.Open();

                theObj = newObj;

                sSql = "INSERT INTO [" + this.TableName + "] "
                    + " (OrderId, ProductCode, Qty, PriceFull, PriceNet) "
                    + " VALUES(@OrderId, @ProductCode, @Qty, @PriceFull, @PriceNet)";

                //p.Add("Id", theObj.Id, null, null, null);
                p.Add("OrderId", theObj.OrderId, null, null, null);
                p.Add("ProductCode", theObj.ProductCode, null, null, null);
                p.Add("Qty", theObj.Qty, null, null, null);
                p.Add("PriceFull", theObj.PriceFull, null, null, null);
                p.Add("PriceNet", theObj.PriceNet, null, null, null);
                myConn.Execute(Database.ParseSql(sSql), p);

                calculateSummary(theObj.OrderId);
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

        public override int DeleteById(int recordId)
        {
            int orderId = this.GetByKey(recordId).OrderId;
            int res = base.DeleteById(recordId);
            calculateSummary(orderId);
            
            return res;
        }

        public int DeleteAllRows(int orderId)
        {
            DbProviderFactory myProv = Database.ProviderFactory;
            DbConnection myConn = myProv.CreateConnection();
            var p = new DynamicParameters();
            string sSql = "";
            int res = 0;

            try
            {
                myConn.ConnectionString = Database.ConnString;
                myConn.Open();

                sSql = "DELETE FROM [" + this.TableName + "] WHERE OrderId = @OrderId ";
                p.Add("OrderId", orderId, null, null, null);
                res = myConn.Execute(Database.ParseSql(sSql), p);

                calculateSummary(orderId);
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                myConn.Dispose();
            }
            return res;
        }

        private void calculateSummary(int orderId)
        {
            var man = new OrdersManager();
            man.CalculateSummary(orderId);
        }

    }
}