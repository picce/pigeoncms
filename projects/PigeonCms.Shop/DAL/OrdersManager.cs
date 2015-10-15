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


namespace PigeonCms.Shop
{
    public interface IOrdersManager<OT, OF, RT, RF>
        where OT : PigeonCms.Shop.IOrder, new()
        where OF : PigeonCms.Shop.IOrderFilter, new()
        where RT : PigeonCms.Shop.OrderRow, new()
        where RF : PigeonCms.Shop.OrderRowsFilter, new()
    {
        void CalculateSummary(int recordId);
        decimal GetShipAmount(OT order);
        decimal GetCouponAmount(OT order, decimal orderAmount);
        void SetOrderAsConfirmed(int orderId);

        List<OT> GetByFilter(OF filter, string sort);
        OT GetByKey(int orderId);
        OT GetByOrderRef(string orderRef);
        OT Insert(OT newObj);
        int Update(OT theObj);

        List<RT> Rows_GetByFilter(RF filter, string sort);
        RT Rows_GetByKey(int orderId);
        int Rows_DeleteAllRows(int orderId);
        int Rows_DeleteById(int recordId);
        RT Rows_Insert(RT newObj);
        int Rows_Update(RT theObj);

    }


    /// <summary>
    /// DAL for Order obj (table #__shop_orderHeader)
    /// </summary>
    public class OrdersManager<OT, OF, RT, RF>: 
            TableManager<OT, OF, int>,
            IOrdersManager<OT, OF, RT, RF>
        where OT: PigeonCms.Shop.IOrder, new()
        where OF: PigeonCms.Shop.IOrderFilter, new()
        where RT : PigeonCms.Shop.OrderRow, new()
        where RF : PigeonCms.Shop.OrderRowsFilter, new()
    {

        [DebuggerStepThrough()]
        public OrdersManager()
        {
            this.TableName = "#__shop_orderHeader";
            this.KeyFieldName = "Id";
        }

        public override List<OT> GetByFilter(OF filter, string sort)
        {
            DbProviderFactory myProv = Database.ProviderFactory;
            DbConnection myConn = myProv.CreateConnection();
            DbDataReader myRd = null;
            DbCommand myCmd = myConn.CreateCommand();
            string sSql;
            var result = new List<OT>();

            try
            {
                myConn.ConnectionString = Database.ConnString;
                myConn.Open();
                myCmd.Connection = myConn;

                sSql = "SELECT t.id, t.orderRef, t.ownerUser, t.customerId, t.orderDate, "
                + " t.orderDateRequested, t.orderDateShipped, t.dateInserted, t.userInserted, "
                + " t.dateUpdated, t.userUpdated, t.confirmed, t.paid, t.processed, t.invoiced, t.notes, "
                + " t.qtyAmount, t.orderAmount, t.shipAmount, t.totalAmount, t.TotalPaid, t.Currency, "
                + " t.invoiceId, t.invoiceRef, t.ordName, t.ordAddress, t.ordZipCode, t.ordCity, t.ordState, "
                + " t.ordNation, t.ordPhone, t.ordEmail, t.couponCode, t.couponValue, "
                + " t.paymentCode, t.shipCode, t.couponIsPercentage, "
                + " t.JsData, t.Custom1, t.Custom2, t.Custom3 "
                + " FROM [" + this.TableName + "] t "
                + " WHERE 1=1 ";

                if (filter.Id > 0 || filter.Id == -1)
                {
                    sSql += " AND t.Id = @Id ";
                    myCmd.Parameters.Add(Database.Parameter(myProv, "Id", filter.Id));
                }
                if (!string.IsNullOrEmpty(filter.OrderRef))
                {
                    sSql += " AND t.OrderRef = @OrderRef ";
                    myCmd.Parameters.Add(Database.Parameter(myProv, "OrderRef", filter.OrderRef));
                }
                if (!string.IsNullOrEmpty(filter.OwnerUser))
                {
                    sSql += " AND t.OwnerUser = @OwnerUser ";
                    myCmd.Parameters.Add(Database.Parameter(myProv, "OwnerUser", filter.OwnerUser));
                }
                if (filter.CustomerId > 0 || filter.CustomerId == -1)
                {
                    sSql += " AND t.CustomerId = @CustomerId ";
                    myCmd.Parameters.Add(Database.Parameter(myProv, "CustomerId", filter.CustomerId));
                }
                if (filter.Confirmed != Utility.TristateBool.NotSet)
                {
                    sSql += " AND t.Confirmed = @Confirmed ";
                    myCmd.Parameters.Add(Database.Parameter(myProv, "Confirmed", filter.Confirmed));
                }
                if (filter.Paid != Utility.TristateBool.NotSet)
                {
                    sSql += " AND t.Paid = @Paid ";
                    myCmd.Parameters.Add(Database.Parameter(myProv, "Paid", filter.Paid));
                }
                if (filter.Processed != Utility.TristateBool.NotSet)
                {
                    sSql += " AND t.Processed = @Processed ";
                    myCmd.Parameters.Add(Database.Parameter(myProv, "Processed", filter.Processed));
                }
                if (!string.IsNullOrEmpty(filter.CouponCode))
                {
                    sSql += " AND t.CouponCode = @CouponCode ";
                    myCmd.Parameters.Add(Database.Parameter(myProv, "CouponCode", filter.CouponCode));
                }
                if (filter.ExcludeIdList.Count > 0)
                {
                    string excludeParams = "";
                    int expludeParamsId = 0;
                    foreach (var paramValue in filter.ExcludeIdList)
                    {
                        string paramName = "ExcludeIdList" + expludeParamsId.ToString();
                        excludeParams += "@" + paramName + ",";
                        myCmd.Parameters.Add(Database.Parameter(myProv, paramName, paramValue));
                        
                        expludeParamsId++;
                    }
                    if (excludeParams.EndsWith(","))
                        excludeParams = excludeParams.Substring(0, excludeParams.Length - 1);

                    sSql += " AND t.Id NOT IN (" + excludeParams + ") ";
                }
                sSql += " AND ("
                     + Database.AddDatesRangeParameters(myCmd.Parameters, myProv, "t.orderDate", filter.OrderDatesRange)
                     + ")";

                if (!string.IsNullOrEmpty(sort))
                {
                    sSql += " ORDER BY " + sort;
                }
                else
                {
                    sSql += " ORDER BY t.Id ";
                }
                myCmd.CommandText = Database.ParseSql(sSql);
                myRd = myCmd.ExecuteReader();
                while (myRd.Read())
                {
                    var item = new OT();
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

        public override OT GetByKey(int id)
        {
            var result = new OT();
            var list = new List<OT>();
            var filter = new OF();
            filter.Id = id == 0 ? -1 : id;
            list = GetByFilter(filter, "");
            if (list.Count > 0)
                result = list[0];
            return result;
        }

        public OT GetByOrderRef(string orderRef)
        {
            var result = new OT();
            var list = new List<OT>();
            var filter = new OF();

            if (string.IsNullOrEmpty(orderRef))
                return result;

            filter.OrderRef = orderRef;
            list = GetByFilter(filter, "");
            if (list.Count > 0)
                result = list[0];
            return result;
        }

        public override int Update(OT theObj)
        {
            DbProviderFactory myProv = Database.ProviderFactory;
            DbConnection myConn = myProv.CreateConnection();
            DbCommand myCmd = myConn.CreateCommand();
            string sSql;
            int result = 0;

            try
            {

                if (theObj.Id <= 0)
                    return 0;

                myConn.ConnectionString = Database.ConnString;
                myConn.Open();
                myCmd.Connection = myConn;

                theObj.DateUpdated = DateTime.Now;
                theObj.UserUpdated = PgnUserCurrent.UserName;

                sSql = "UPDATE [" + this.TableName + "] "
                + @" SET OrderRef=@OrderRef, OwnerUser=@OwnerUser, CustomerId=@CustomerId, OrderDate=@OrderDate, 
                OrderDateRequested=@OrderDateRequested, OrderDateShipped=@OrderDateShipped, 
                DateUpdated=@DateUpdated, UserUpdated=@UserUpdated, 
                Confirmed=@Confirmed, Paid=@Paid, Processed=@Processed, Invoiced=@Invoiced, Notes=@Notes, 
                QtyAmount=@QtyAmount, OrderAmount=@OrderAmount, ShipAmount=@ShipAmount, TotalAmount=@TotalAmount, TotalPaid=@TotalPaid, Currency=@Currency, 
                InvoiceId=@InvoiceId, InvoiceRef=@InvoiceRef, OrdName=@OrdName, OrdAddress=@OrdAddress, OrdZipCode=@OrdZipCode, 
                OrdCity=@OrdCity, OrdState=@OrdState, OrdNation=@OrdNation, OrdPhone=@OrdPhone, OrdEmail=@OrdEmail, 
                CouponCode=@CouponCode, CouponValue=@CouponValue, 
                PaymentCode=@PaymentCode, ShipCode=@ShipCode, CouponIsPercentage=@CouponIsPercentage, 
                JsData=@JsData, Custom1=@Custom1, Custom2=@Custom2, Custom3=@Custom3 
                WHERE Id = @Id ";
                myCmd.CommandText = Database.ParseSql(sSql);
                myCmd.Parameters.Clear();
                myCmd.Parameters.Add(Database.Parameter(myProv, "Id", theObj.Id));
                myCmd.Parameters.Add(Database.Parameter(myProv, "OrderRef", theObj.OrderRef));
                myCmd.Parameters.Add(Database.Parameter(myProv, "OwnerUser", theObj.OwnerUser));
                myCmd.Parameters.Add(Database.Parameter(myProv, "CustomerId", theObj.CustomerId));
                myCmd.Parameters.Add(Database.Parameter(myProv, "OrderDate", theObj.OrderDate));

                if (theObj.OrderDateRequested != DateTime.MinValue)
                    myCmd.Parameters.Add(Database.Parameter(myProv, "OrderDateRequested", theObj.OrderDateRequested));
                else
                    myCmd.Parameters.Add(Database.Parameter(myProv, "OrderDateRequested", DBNull.Value));

                if (theObj.OrderDateShipped != DateTime.MinValue)
                    myCmd.Parameters.Add(Database.Parameter(myProv, "OrderDateShipped", theObj.OrderDateShipped));
                else
                    myCmd.Parameters.Add(Database.Parameter(myProv, "OrderDateShipped", DBNull.Value));

                myCmd.Parameters.Add(Database.Parameter(myProv, "DateUpdated", theObj.DateUpdated));
                myCmd.Parameters.Add(Database.Parameter(myProv, "UserUpdated", theObj.UserUpdated));
                myCmd.Parameters.Add(Database.Parameter(myProv, "Confirmed", theObj.Confirmed));
                myCmd.Parameters.Add(Database.Parameter(myProv, "Paid", theObj.Paid));
                myCmd.Parameters.Add(Database.Parameter(myProv, "Processed", theObj.Processed));
                myCmd.Parameters.Add(Database.Parameter(myProv, "Invoiced", theObj.Invoiced));
                myCmd.Parameters.Add(Database.Parameter(myProv, "Notes", theObj.Notes));
                myCmd.Parameters.Add(Database.Parameter(myProv, "QtyAmount", theObj.QtyAmount));
                myCmd.Parameters.Add(Database.Parameter(myProv, "OrderAmount", theObj.OrderAmount));
                myCmd.Parameters.Add(Database.Parameter(myProv, "ShipAmount", theObj.ShipAmount));
                myCmd.Parameters.Add(Database.Parameter(myProv, "TotalAmount", theObj.TotalAmount));
                myCmd.Parameters.Add(Database.Parameter(myProv, "TotalPaid", theObj.TotalPaid));
                myCmd.Parameters.Add(Database.Parameter(myProv, "Currency", theObj.Currency));
                myCmd.Parameters.Add(Database.Parameter(myProv, "InvoiceId", theObj.InvoiceId));
                myCmd.Parameters.Add(Database.Parameter(myProv, "InvoiceRef", theObj.InvoiceRef));
                myCmd.Parameters.Add(Database.Parameter(myProv, "OrdName", theObj.OrdName));
                myCmd.Parameters.Add(Database.Parameter(myProv, "OrdAddress", theObj.OrdAddress));
                myCmd.Parameters.Add(Database.Parameter(myProv, "OrdZipCode", theObj.OrdZipCode));
                myCmd.Parameters.Add(Database.Parameter(myProv, "OrdCity", theObj.OrdCity));
                myCmd.Parameters.Add(Database.Parameter(myProv, "OrdState", theObj.OrdState));
                myCmd.Parameters.Add(Database.Parameter(myProv, "OrdNation", theObj.OrdNation));
                myCmd.Parameters.Add(Database.Parameter(myProv, "OrdPhone", theObj.OrdPhone));
                myCmd.Parameters.Add(Database.Parameter(myProv, "OrdEmail", theObj.OrdEmail));
                myCmd.Parameters.Add(Database.Parameter(myProv, "CouponCode", theObj.CouponCode));
                myCmd.Parameters.Add(Database.Parameter(myProv, "CouponValue", theObj.CouponValue));
                myCmd.Parameters.Add(Database.Parameter(myProv, "PaymentCode", theObj.PaymentCode));
                myCmd.Parameters.Add(Database.Parameter(myProv, "ShipCode", theObj.ShipCode));
                myCmd.Parameters.Add(Database.Parameter(myProv, "CouponIsPercentage", theObj.CouponIsPercentage));
                myCmd.Parameters.Add(Database.Parameter(myProv, "JsData", theObj.JsData));
                myCmd.Parameters.Add(Database.Parameter(myProv, "Custom1", theObj.Custom1));
                myCmd.Parameters.Add(Database.Parameter(myProv, "Custom2", theObj.Custom2));
                myCmd.Parameters.Add(Database.Parameter(myProv, "Custom3", theObj.Custom3));
                result = myCmd.ExecuteNonQuery();
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

        public override OT Insert(OT newObj)
        {
            DbProviderFactory myProv = Database.ProviderFactory;
            DbConnection myConn = myProv.CreateConnection();
            DbCommand myCmd = myConn.CreateCommand();
            string sSql;
            var theObj = new OT();

            try
            {
                myConn.ConnectionString = Database.ConnString;
                myConn.Open();
                myCmd.Connection = myConn;

                theObj = newObj;
                theObj.DateInserted = DateTime.Now;
                theObj.UserInserted = PgnUserCurrent.UserName;
                theObj.DateUpdated = DateTime.Now;
                theObj.UserUpdated = PgnUserCurrent.UserName;

                if (theObj.OrderDate == DateTime.MinValue)
                    theObj.OrderDate = DateTime.Now;

                if (string.IsNullOrEmpty(theObj.OrderRef))
                    theObj.OrderRef = theObj.OrderDate.ToString("yyyyMMddHHmmss");


                sSql = "INSERT INTO [" + this.TableName + @"] 
                    (orderRef, ownerUser, customerId, orderDate, orderDateRequested, orderDateShipped, 
                    dateInserted, userInserted, dateUpdated, userUpdated, confirmed, paid, processed, invoiced, 
                    notes, QtyAmount, orderAmount, shipAmount, totalAmount, TotalPaid, Currency, 
                    invoiceId, invoiceRef, ordName, ordAddress, ordZipCode, ordCity, ordState, 
                    ordNation, ordPhone, ordEmail, couponCode, couponValue, 
                    paymentCode, shipCode, couponIsPercentage,
                    JsData, Custom1, Custom2, Custom3)
                    VALUES(@orderRef, @ownerUser, @customerId, @orderDate, @orderDateRequested, @orderDateShipped, 
                    @dateInserted, @userInserted, @dateUpdated, @userUpdated, @confirmed, @paid, @processed, @invoiced, 
                    @notes, @QtyAmount, @orderAmount, @shipAmount, @totalAmount, @TotalPaid, @Currency, 
                    @invoiceId, @invoiceRef, @ordName, @ordAddress, @ordZipCode, @ordCity, @ordState, 
                    @ordNation, @ordPhone, @ordEmail, @couponCode, @couponValue, 
                    @paymentCode, @shipCode, @couponIsPercentage,
                    @JsData, @Custom1, @Custom2, @Custom3)
                    SELECT SCOPE_IDENTITY()";
                myCmd.CommandText = Database.ParseSql(sSql);

                myCmd.Parameters.Add(Database.Parameter(myProv, "OrderRef", theObj.OrderRef));
                myCmd.Parameters.Add(Database.Parameter(myProv, "OwnerUser", theObj.OwnerUser));
                myCmd.Parameters.Add(Database.Parameter(myProv, "CustomerId", theObj.CustomerId));
                myCmd.Parameters.Add(Database.Parameter(myProv, "OrderDate", theObj.OrderDate));

                if (theObj.OrderDateRequested != DateTime.MinValue)
                    myCmd.Parameters.Add(Database.Parameter(myProv, "OrderDateRequested", theObj.OrderDateRequested));
                else
                    myCmd.Parameters.Add(Database.Parameter(myProv, "OrderDateRequested", DBNull.Value));

                if (theObj.OrderDateShipped != DateTime.MinValue)
                    myCmd.Parameters.Add(Database.Parameter(myProv, "OrderDateShipped", theObj.OrderDateShipped));
                else
                    myCmd.Parameters.Add(Database.Parameter(myProv, "OrderDateShipped", DBNull.Value));

                myCmd.Parameters.Add(Database.Parameter(myProv, "DateInserted", theObj.DateInserted));
                myCmd.Parameters.Add(Database.Parameter(myProv, "UserInserted", theObj.UserInserted));
                myCmd.Parameters.Add(Database.Parameter(myProv, "DateUpdated", theObj.DateUpdated));
                myCmd.Parameters.Add(Database.Parameter(myProv, "UserUpdated", theObj.UserUpdated));
                myCmd.Parameters.Add(Database.Parameter(myProv, "Confirmed", theObj.Confirmed));
                myCmd.Parameters.Add(Database.Parameter(myProv, "Paid", theObj.Paid));
                myCmd.Parameters.Add(Database.Parameter(myProv, "Processed", theObj.Processed));
                myCmd.Parameters.Add(Database.Parameter(myProv, "Invoiced", theObj.Invoiced));
                myCmd.Parameters.Add(Database.Parameter(myProv, "Notes", theObj.Notes));
                myCmd.Parameters.Add(Database.Parameter(myProv, "QtyAmount", theObj.QtyAmount));
                myCmd.Parameters.Add(Database.Parameter(myProv, "OrderAmount", theObj.OrderAmount));
                myCmd.Parameters.Add(Database.Parameter(myProv, "ShipAmount", theObj.ShipAmount));
                myCmd.Parameters.Add(Database.Parameter(myProv, "TotalAmount", theObj.TotalAmount));
                myCmd.Parameters.Add(Database.Parameter(myProv, "TotalPaid", theObj.TotalPaid));
                myCmd.Parameters.Add(Database.Parameter(myProv, "Currency", theObj.Currency));
                myCmd.Parameters.Add(Database.Parameter(myProv, "InvoiceId", theObj.InvoiceId));
                myCmd.Parameters.Add(Database.Parameter(myProv, "InvoiceRef", theObj.InvoiceRef));
                myCmd.Parameters.Add(Database.Parameter(myProv, "OrdName", theObj.OrdName));
                myCmd.Parameters.Add(Database.Parameter(myProv, "OrdAddress", theObj.OrdAddress));
                myCmd.Parameters.Add(Database.Parameter(myProv, "OrdZipCode", theObj.OrdZipCode));
                myCmd.Parameters.Add(Database.Parameter(myProv, "OrdCity", theObj.OrdCity));
                myCmd.Parameters.Add(Database.Parameter(myProv, "OrdState", theObj.OrdState));
                myCmd.Parameters.Add(Database.Parameter(myProv, "OrdNation", theObj.OrdNation));
                myCmd.Parameters.Add(Database.Parameter(myProv, "OrdPhone", theObj.OrdPhone));
                myCmd.Parameters.Add(Database.Parameter(myProv, "OrdEmail", theObj.OrdEmail));
                myCmd.Parameters.Add(Database.Parameter(myProv, "CouponCode", theObj.CouponCode));
                myCmd.Parameters.Add(Database.Parameter(myProv, "CouponValue", theObj.CouponValue));
                myCmd.Parameters.Add(Database.Parameter(myProv, "PaymentCode", theObj.PaymentCode));
                myCmd.Parameters.Add(Database.Parameter(myProv, "ShipCode", theObj.ShipCode));
                myCmd.Parameters.Add(Database.Parameter(myProv, "CouponIsPercentage", theObj.CouponIsPercentage));
                myCmd.Parameters.Add(Database.Parameter(myProv, "JsData", theObj.JsData));
                myCmd.Parameters.Add(Database.Parameter(myProv, "Custom1", theObj.Custom1));
                myCmd.Parameters.Add(Database.Parameter(myProv, "Custom2", theObj.Custom2));
                myCmd.Parameters.Add(Database.Parameter(myProv, "Custom3", theObj.Custom3));


                theObj.Id = (int)(decimal)myCmd.ExecuteScalar();
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
            int res = base.DeleteById(recordId);
            Rows_DeleteAllRows(recordId);
            return res;
        }

        /// <summary>
        /// calculate order summary fetching rows, coupon and ship fees
        /// QtyAmount
        /// OrderAmount
        /// TotalAmount
        /// </summary>
        /// <param name="recordId">the order id</param>
        public virtual void CalculateSummary(int recordId)
        {
            if (recordId <= 0)
                return;

            decimal qtyAmount = 0;
            decimal orderAmount = 0;

            var filter = new RF();
            filter.OrderId = recordId;
            var list = Rows_GetByFilter(filter, "");
            foreach (var row in list)
            {
                qtyAmount += row.Qty;
                orderAmount += row.AmountWithTaxes;
            }

            var order = this.GetByKey(recordId);
            order.QtyAmount = qtyAmount;
            order.OrderAmount = orderAmount;
            decimal shipAmount = this.GetShipAmount(order);
            order.ShipAmount = shipAmount;

            decimal couponValue = GetCouponAmount(order, orderAmount);
            order.TotalAmount = orderAmount + shipAmount - couponValue;
            
            this.Update(order);
        }

        public virtual decimal GetShipAmount(OT order)
        {
            decimal res = order.ShipAmount;
            return res;
        }

        public virtual decimal GetCouponAmount(OT order, decimal orderAmount)
        {
            decimal res = order.CouponValue;
            if (order.CouponIsPercentage && res <= 1)
            {
                //from 20150608
                res = orderAmount * res;
            }
            return res;
        }

        public void SetOrderAsConfirmed(int orderId)
        {
            var order = new OT();
            order = this.GetByKey(orderId);
            order.Confirmed = true;
            this.Update(order);
        }


        protected override void FillObject(OT result, DbDataReader myRd)
        {
            if (!Convert.IsDBNull(myRd["Id"]))
                result.Id = (int)myRd["Id"];
            if (!Convert.IsDBNull(myRd["orderRef"]))
                result.OrderRef = (string)myRd["orderRef"];
            if (!Convert.IsDBNull(myRd["ownerUser"]))
                result.OwnerUser = (string)myRd["ownerUser"];
            if (!Convert.IsDBNull(myRd["CustomerId"]))
                result.CustomerId = (int)myRd["CustomerId"];
            if (!Convert.IsDBNull(myRd["OrderDate"]))
                result.OrderDate = (DateTime)myRd["OrderDate"];
            if (!Convert.IsDBNull(myRd["OrderDateRequested"]))
                result.OrderDateRequested = (DateTime)myRd["OrderDateRequested"];
            if (!Convert.IsDBNull(myRd["OrderDateShipped"]))
                result.OrderDateShipped = (DateTime)myRd["OrderDateShipped"];
            if (!Convert.IsDBNull(myRd["DateInserted"]))
                result.DateInserted = (DateTime)myRd["DateInserted"];
            if (!Convert.IsDBNull(myRd["UserInserted"]))
                result.UserInserted = (string)myRd["UserInserted"];
            if (!Convert.IsDBNull(myRd["DateUpdated"]))
                result.DateUpdated = (DateTime)myRd["DateUpdated"];
            if (!Convert.IsDBNull(myRd["UserUpdated"]))
                result.UserUpdated = (string)myRd["UserUpdated"];
            if (!Convert.IsDBNull(myRd["Confirmed"]))
                result.Confirmed = (bool)myRd["Confirmed"];
            if (!Convert.IsDBNull(myRd["Paid"]))
                result.Paid = (bool)myRd["Paid"];
            if (!Convert.IsDBNull(myRd["Processed"]))
                result.Processed = (bool)myRd["Processed"];
            if (!Convert.IsDBNull(myRd["Invoiced"]))
                result.Invoiced = (bool)myRd["Invoiced"];
            if (!Convert.IsDBNull(myRd["Notes"]))
                result.Notes = (string)myRd["Notes"];
            if (!Convert.IsDBNull(myRd["QtyAmount"]))
                result.QtyAmount = (decimal)myRd["QtyAmount"];
            if (!Convert.IsDBNull(myRd["OrderAmount"]))
                result.OrderAmount = (decimal)myRd["OrderAmount"];
            if (!Convert.IsDBNull(myRd["ShipAmount"]))
                result.ShipAmount = (decimal)myRd["ShipAmount"];
            if (!Convert.IsDBNull(myRd["TotalAmount"]))
                result.TotalAmount = (decimal)myRd["TotalAmount"];
            if (!Convert.IsDBNull(myRd["TotalPaid"]))
                result.TotalPaid = (decimal)myRd["TotalPaid"];
            if (!Convert.IsDBNull(myRd["Currency"]))
                result.Currency = (string)myRd["Currency"];
            if (!Convert.IsDBNull(myRd["InvoiceId"]))
                result.InvoiceId = (int)myRd["InvoiceId"];
            if (!Convert.IsDBNull(myRd["InvoiceRef"]))
                result.InvoiceRef = (string)myRd["InvoiceRef"];
            if (!Convert.IsDBNull(myRd["OrdName"]))
                result.OrdName = (string)myRd["OrdName"];
            if (!Convert.IsDBNull(myRd["OrdAddress"]))
                result.OrdAddress = (string)myRd["OrdAddress"];
            if (!Convert.IsDBNull(myRd["OrdZipCode"]))
                result.OrdZipCode = (string)myRd["OrdZipCode"];
            if (!Convert.IsDBNull(myRd["OrdCity"]))
                result.OrdCity = (string)myRd["OrdCity"];
            if (!Convert.IsDBNull(myRd["OrdState"]))
                result.OrdState = (string)myRd["OrdState"];
            if (!Convert.IsDBNull(myRd["OrdNation"]))
                result.OrdNation = (string)myRd["OrdNation"];
            if (!Convert.IsDBNull(myRd["OrdPhone"]))
                result.OrdPhone = (string)myRd["OrdPhone"];
            if (!Convert.IsDBNull(myRd["OrdEmail"]))
                result.OrdEmail = (string)myRd["OrdEmail"];
            if (!Convert.IsDBNull(myRd["CouponCode"]))
                result.CouponCode = (string)myRd["CouponCode"];
            if (!Convert.IsDBNull(myRd["CouponValue"]))
                result.CouponValue = (decimal)myRd["CouponValue"];
            if (!Convert.IsDBNull(myRd["PaymentCode"]))
                result.PaymentCode = (string)myRd["PaymentCode"];
            if (!Convert.IsDBNull(myRd["ShipCode"]))
                result.ShipCode = (string)myRd["ShipCode"];
            if (!Convert.IsDBNull(myRd["CouponIsPercentage"]))
                result.CouponIsPercentage = (bool)myRd["CouponIsPercentage"];
            if (!Convert.IsDBNull(myRd["JsData"]))
                result.JsData = (string)myRd["JsData"];
            if (!Convert.IsDBNull(myRd["Custom1"]))
                result.Custom1 = (string)myRd["Custom1"];
            if (!Convert.IsDBNull(myRd["Custom2"]))
                result.Custom2 = (string)myRd["Custom2"];
            if (!Convert.IsDBNull(myRd["Custom3"]))
                result.Custom3 = (string)myRd["Custom3"];
        }


        #region rows

        const string Rows_TableName = "#__shop_orderRows";

        public List<RT> Rows_GetByFilter(RF filter, string sort)
        {
            DbProviderFactory myProv = Database.ProviderFactory;
            DbConnection myConn = myProv.CreateConnection();
            var p = new DynamicParameters();
            string sSql;
            var result = new List<RT>();

            try
            {
                myConn.ConnectionString = Database.ConnString;
                myConn.Open();

                sSql = "SELECT Id, OrderId, ProductCode, Qty, "
                + " PriceNet, TaxPercentage, RowNotes "
                + " FROM [" + Rows_TableName + "] t "
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

                result = (List<RT>)myConn.Query<RT>(Database.ParseSql(sSql), p);
            }
            finally
            {
                myConn.Dispose();
            }
            return result;
        }

        public RT Rows_GetByKey(int id)
        {
            var result = new RT();
            var list = new List<RT>();
            var filter = new RF();

            filter.Id = id == 0 ? -1 : id;
            list = Rows_GetByFilter(filter, "");
            if (list.Count > 0)
                result = list[0];

            return result;
        }

        public int Rows_Update(RT theObj)
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

                sSql = "UPDATE [" + Rows_TableName + "] "
                + " SET OrderId=@OrderId, ProductCode=@ProductCode, Qty=@Qty, "
                + " PriceNet=@PriceNet, TaxPercentage=@TaxPercentage, "
                + " RowNotes=@RowNotes "
                + " WHERE Id = @Id";

                p.Add("Id", theObj.Id, null, null, null);
                p.Add("OrderId", theObj.OrderId, null, null, null);
                p.Add("ProductCode", theObj.ProductCode, null, null, null);
                p.Add("Qty", theObj.Qty, null, null, null);
                p.Add("PriceNet", theObj.PriceNet, null, null, null);
                p.Add("TaxPercentage", theObj.TaxPercentage, null, null, null);
                p.Add("RowNotes", theObj.RowNotes, null, null, null);
                result = myConn.Execute(Database.ParseSql(sSql), p);

                CalculateSummary(theObj.OrderId);
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

        public RT Rows_Insert(RT newObj)
        {
            DbProviderFactory myProv = Database.ProviderFactory;
            DbConnection myConn = myProv.CreateConnection();
            var p = new DynamicParameters();
            string sSql;
            var theObj = new RT();

            try
            {
                myConn.ConnectionString = Database.ConnString;
                myConn.Open();

                theObj = newObj;

                sSql = "INSERT INTO [" + Rows_TableName + "] "
                    + " (OrderId, ProductCode, Qty, PriceNet, TaxPercentage, RowNotes) "
                    + " VALUES(@OrderId, @ProductCode, @Qty, @PriceNet, @TaxPercentage, @RowNotes)";

                //p.Add("Id", theObj.Id, null, null, null);
                p.Add("OrderId", theObj.OrderId, null, null, null);
                p.Add("ProductCode", theObj.ProductCode, null, null, null);
                p.Add("Qty", theObj.Qty, null, null, null);
                p.Add("PriceNet", theObj.PriceNet, null, null, null);
                p.Add("TaxPercentage", theObj.TaxPercentage, null, null, null);
                p.Add("RowNotes", theObj.RowNotes, null, null, null);
                myConn.Execute(Database.ParseSql(sSql), p);

                CalculateSummary(theObj.OrderId);
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

        public int Rows_DeleteById(int recordId)
        {
            DbProviderFactory myProv = Database.ProviderFactory;
            DbConnection myConn = myProv.CreateConnection();
            var p = new DynamicParameters();
            string sSql = "";
            int res = 0;

            try
            {

                int orderId = Rows_GetByKey(recordId).OrderId;
                
                myConn.ConnectionString = Database.ConnString;
                myConn.Open();

                sSql = "DELETE FROM [" + Rows_TableName + "] WHERE Id = @Id ";
                p.Add("Id", recordId, null, null, null);
                res = myConn.Execute(Database.ParseSql(sSql), p);

                CalculateSummary(orderId);
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

        public int Rows_DeleteAllRows(int orderId)
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

                sSql = "DELETE FROM [" + Rows_TableName + "] WHERE OrderId = @OrderId ";
                p.Add("OrderId", orderId, null, null, null);
                res = myConn.Execute(Database.ParseSql(sSql), p);

                CalculateSummary(orderId);
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

        #endregion

    }//class

}//ns