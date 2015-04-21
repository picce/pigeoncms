using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.ComponentModel;
using System.Collections.Generic;
using System.IO;
using System.Data.Common;
using System.Diagnostics;
using PigeonCms;
using PigeonCms.Core.Helpers;


namespace PigeonCms.Shop.OrdersProvider
{
    public class CurrentOrder<OH, OR>
        where OH : OrdersManager, new()
        where OR : OrderRowsManager<OH>, new()
    {
        private const string COOKIE_NAME = "pgnshop";
        private const string COOKIE_KEY_ORDERID = "oid";
        private CookiesManager ckMan = new CookiesManager(COOKIE_NAME, true);
        private OH ordMan = new OH();
        private OR rowMan = new OR(); 


        private int orderId = 0;
        public int OrderId
        {
            get { return this.orderId; }
        }

        private Order order = null;
        public Order Order
        {
            get
            {
                if (this.OrderId <= 0)
                    order = null;

                if (order == null)
                {
                    order = new Order();
                    if (this.OrderId > 0)
                    {
                        order = ordMan.GetByKey(this.OrderId);
                    }
                }
                return order;
            }
        }

        public CurrentOrder()
        {
            this.orderId = this.orderCookie;
        }

        private int orderCookie
        {
            get
            {
                int res = 0;
                if (!ckMan.IsEmpty(COOKIE_KEY_ORDERID))
                {
                    int.TryParse(ckMan.GetValue(COOKIE_KEY_ORDERID), out res);
                    if (res > 0)
                    {
                        //check if order exists
                        var man = new OrdersManager();
                        var ord = man.GetByKey(res);

                        //cause new order and new cookie policies
                        if (ord.Id == 0)
                            res = 0;
                        
                        //if (ord.Confirmed)
                        if (ord.Paid)
                            res = 0;
                    }
                }
                return res;
            }

            set
            {
                if (value <= 0)
                {
                    ckMan.Clear();
                    this.orderId = 0;
                }
                else
                {
                    ckMan.SetValue(COOKIE_KEY_ORDERID, value.ToString());
                    this.orderId = value;
                }
            }
        }

        public void AddRow(string productCode, decimal qty)
        {
            var row = new OrderRow();
            row.ProductCode = productCode;
            
            //TODO retrieve product data

            row.Qty = qty;

            this.AddRow(row);
        }

        public void AddRow(OrderRow row)
        {
            if (this.OrderId == 0)
            {
                //create new order
                var ord = new Order();
                ord.Currency = "EUR";   //TODO
                ord = ordMan.Insert(ord);
                this.orderCookie = ord.Id;
            }

            row.OrderId = this.OrderId;
            rowMan.Insert(row);
        }

        public int RemoveRow(int rowId)
        {
            int res = 0;
            if (checkRowId(rowId))
            {
                res = rowMan.DeleteById(rowId);
            }
            return res;
        }

        public int IncRow(int rowId, int incValue)
        {
            int res = 0;
            if (checkRowId(rowId))
            {
                var row = rowMan.GetByKey(rowId);
                row.Qty += incValue;
                if (row.Qty < 0)
                    return 0;

                res = rowMan.Update(row);
                if (row.Qty == 0)
                {
                    RemoveRow(rowId);
                }
            }
            return res;
        }

        public int UpdateRow(int rowId, decimal qty)
        {
            int res = 0;
            if (checkRowId(rowId))
            {
                var row = rowMan.GetByKey(rowId);
                row.Qty = qty;
                res = rowMan.Update(row);
            }
            return res;
        }

        public int SetOwner(string ownerUser, int customerId)
        {
            int res = 0;

            this.Order.OwnerUser = ownerUser;
            this.Order.CustomerId = customerId;
            res = ordMan.Update(this.Order);

            return res;
        }

        public int Update()
        {
            int res = ordMan.Update(this.Order);
            ordMan.CalculateSummary(this.OrderId);
            return res;
        }

        /// <summary>
        /// check if rowId if in current order
        /// </summary>
        private bool checkRowId(int rowId)
        {
            bool res = false;
            var filter = new OrderRowsFilter();
            filter.OrderId = this.OrderId > 0 ? this.OrderId : -1;
            filter.Id = rowId > 0 ? rowId : -1;
            //sec check
            var list = rowMan.GetByFilter(filter, "");
            if (list.Count == 1)
                res = true;

            return res; 
        }

    }//class
}//ns