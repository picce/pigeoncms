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


namespace PigeonCms.Shop
{
    /// <summary>
    /// DAL for Order obj (table #__shop_orderHeader)
    /// </summary>
    public class OrdersManager : TableManager<Order, OrdersFilter, int>
    {
        [DebuggerStepThrough()]
        public OrdersManager()
        {
            this.TableName = "#__shop_orderHeader";
            this.KeyFieldName = "Id";
        }

        public override List<Order> GetByFilter(OrdersFilter filter, string sort)
        {
            DbProviderFactory myProv = Database.ProviderFactory;
            DbConnection myConn = myProv.CreateConnection();
            DbDataReader myRd = null;
            DbCommand myCmd = myConn.CreateCommand();
            string sSql;
            var result = new List<Order>();

            try
            {
                myConn.ConnectionString = Database.ConnString;
                myConn.Open();
                myCmd.Connection = myConn;

                sSql = "SELECT t.id, t.orderRef, t.ownerUser, t.customerId, t.orderDate, "
                + " t.orderDateRequested, t.orderDateShipped, t.dateInserted, t.userInserted, "
                + " t.dateUpdated, t.userUpdated, t.confirmed, t.paid, t.processed, t.invoiced, t.notes, "
                + " t.qtyAmount, t.orderAmount, t.shipAmount, t.totalAmount, t.currency, t.vatPercentage, "
                + " t.invoiceId, t.invoiceRef, t.ordName, t.ordAddress, t.ordZipCode, t.ordCity, t.ordState, "
                + " t.ordNation, t.ordPhone, t.ordEmail, t.codeCoupon, t.paymentCode, t.shipCode "
                + " FROM [" + this.TableName + "] t "
                + " WHERE 1=1 ";

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
                    var item = new Order();
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

        public override Order GetByKey(int id)
        {
            var result = new Order();
            var list = new List<Order>();
            var filter = new OrdersFilter();
            filter.Id = id == 0 ? -1 : id;
            list = GetByFilter(filter, "");
            if (list.Count > 0)
                result = list[0];
            return result;
        }

        public override int Update(Order theObj)
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

                theObj.DateUpdated = DateTime.Now;
                theObj.UserUpdated = PgnUserCurrent.UserName;

                sSql = "UPDATE [" + this.TableName + "] "
                + @" SET OrderRef=@OrderRef, OwnerUser=@OwnerUser, CustomerId=@CustomerId, OrderDate=@OrderDate, 
                OrderDateRequested=@OrderDateRequested, OrderDateShipped=@OrderDateShipped, 
                DateUpdated=@DateUpdated, UserUpdated=@UserUpdated, 
                Confirmed=@Confirmed, Paid=@Paid, Processed=@Processed, Invoiced=@Invoiced, Notes=@Notes, 
                QtyAmount=@QtyAmount, OrderAmount=@OrderAmount, ShipAmount=@ShipAmount, TotalAmount=@TotalAmount, Currency=@Currency, VatPercentage=@VatPercentage, 
                InvoiceId=@InvoiceId, InvoiceRef=@InvoiceRef, OrdName=@OrdName, OrdAddress=@OrdAddress, OrdZipCode=@OrdZipCode, 
                OrdCity=@OrdCity, OrdState=@OrdState, OrdNation=@OrdNation, OrdPhone=@OrdPhone, OrdEmail=@OrdEmail, 
                CodeCoupon=@CodeCoupon, PaymentCode=@PaymentCode, ShipCode=@ShipCode 
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
                myCmd.Parameters.Add(Database.Parameter(myProv, "Currency", theObj.Currency));
                myCmd.Parameters.Add(Database.Parameter(myProv, "VatPercentage", theObj.VatPercentage));
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
                myCmd.Parameters.Add(Database.Parameter(myProv, "CodeCoupon", theObj.CodeCoupon));
                myCmd.Parameters.Add(Database.Parameter(myProv, "PaymentCode", theObj.PaymentCode));
                myCmd.Parameters.Add(Database.Parameter(myProv, "ShipCode", theObj.ShipCode));

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

        public override Order Insert(Order newObj)
        {
            DbProviderFactory myProv = Database.ProviderFactory;
            DbConnection myConn = myProv.CreateConnection();
            DbCommand myCmd = myConn.CreateCommand();
            string sSql;
            var theObj = new Order();

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
                    notes, QtyAmount, orderAmount, shipAmount, totalAmount, currency, vatPercentage, 
                    invoiceId, invoiceRef, ordName, ordAddress, ordZipCode, ordCity, ordState, 
                    ordNation, ordPhone, ordEmail, codeCoupon, paymentCode, shipCode)
                    VALUES(@orderRef, @ownerUser, @customerId, @orderDate, @orderDateRequested, @orderDateShipped, 
                    @dateInserted, @userInserted, @dateUpdated, @userUpdated, @confirmed, @paid, @processed, @invoiced, 
                    @notes, @QtyAmount, @orderAmount, @shipAmount, @totalAmount, @currency, @vatPercentage, 
                    @invoiceId, @invoiceRef, @ordName, @ordAddress, @ordZipCode, @ordCity, @ordState, 
                    @ordNation, @ordPhone, @ordEmail, @codeCoupon, @paymentCode, @shipCode)
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
                myCmd.Parameters.Add(Database.Parameter(myProv, "Currency", theObj.Currency));
                myCmd.Parameters.Add(Database.Parameter(myProv, "VatPercentage", theObj.VatPercentage));
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
                myCmd.Parameters.Add(Database.Parameter(myProv, "CodeCoupon", theObj.CodeCoupon));
                myCmd.Parameters.Add(Database.Parameter(myProv, "PaymentCode", theObj.PaymentCode));
                myCmd.Parameters.Add(Database.Parameter(myProv, "ShipCode", theObj.ShipCode));

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
            var rowMan = new OrderRowsManager();
            rowMan.DeleteAllRows(recordId);

            return res;
        }

        /// <summary>
        /// calculate order summary fetching rows
        /// QtyAmount
        /// OrderAmount
        /// TotalAmount
        /// </summary>
        /// <param name="recordId">the order id</param>
        public void CalculateSummary(int recordId)
        {
            if (recordId <= 0)
                return;

            decimal qtyAmount = 0;
            decimal orderAmount = 0;

            var rowMan = new OrderRowsManager();
            var filter = new OrderRowsFilter();
            filter.OrderId = recordId;
            var list = rowMan.GetByFilter(filter, "");
            foreach (var row in list)
            {
                qtyAmount += row.Qty;
                orderAmount += row.RowPrice;
            }

            var order = this.GetByKey(recordId);
            order.QtyAmount = qtyAmount;
            order.OrderAmount = orderAmount;
            order.TotalAmount = orderAmount + order.ShipAmount;
            this.Update(order);
        }

        public void SetOrderAsConfirmed(int orderId)
        {
            var man = new OrdersManager();
            var order = new Order();
            order = man.GetByKey(orderId);
            order.Confirmed = true;
            man.Update(order);
        }


        protected override void FillObject(Order result, DbDataReader myRd)
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
            if (!Convert.IsDBNull(myRd["Currency"]))
                result.Currency = (string)myRd["Currency"];
            if (!Convert.IsDBNull(myRd["VatPercentage"]))
                result.VatPercentage = (int)myRd["VatPercentage"];
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
            if (!Convert.IsDBNull(myRd["CodeCoupon"]))
                result.CodeCoupon = (string)myRd["CodeCoupon"];
            if (!Convert.IsDBNull(myRd["PaymentCode"]))
                result.PaymentCode = (string)myRd["PaymentCode"];
            if (!Convert.IsDBNull(myRd["ShipCode"]))
                result.ShipCode = (string)myRd["ShipCode"];
        }

    }//class
}//ns