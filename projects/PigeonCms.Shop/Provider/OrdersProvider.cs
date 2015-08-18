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
    public class CurrentOrder<OM, OT, OF, RT, RF>
        where OM : IOrdersManager<OT, OF, RT, RF>, new()
        where OT : IOrder, new()
        where OF : PigeonCms.Shop.IOrderFilter, new()
        where RT : PigeonCms.Shop.OrderRow, new()
        where RF : PigeonCms.Shop.OrderRowsFilter, new()
    {
        private const string COOKIE_NAME = "pgnshop";
        private const string COOKIE_KEY_ORDERID = "oid";
        private CookiesManager ckMan = new CookiesManager(COOKIE_NAME, true);
        private OM ordMan = new OM();


        private int orderId = 0;
        public int OrderId
        {
            get { return this.orderId; }
        }

        private OT order = default(OT);
        public OT Order
        {
            get
            {
                if (this.OrderId <= 0)
                    order = new OT();

                if (order == null || order.Id == 0)
                {
                    order = new OT();
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
                        var man = new OM();
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
            var row = new RT();
            row.ProductCode = productCode;
            
            //TODO retrieve product data

            row.Qty = qty;

            this.AddRow(row);
        }

        public void AddRow(RT row)
        {
            if (this.OrderId == 0)
            {
                var settings = new PigeonCms.Shop.Settings();

                //create new order
                var ord = new OT();
                ord.Currency = settings.ShopCurrency;
                ord = ordMan.Insert(ord);
                this.orderCookie = ord.Id;
            }

            row.OrderId = this.OrderId;
            ordMan.Rows_Insert(row);
        }

        public int RemoveRow(int rowId)
        {
            int res = 0;
            if (checkRowId(rowId))
            {
                res = ordMan.Rows_DeleteById(rowId);
            }
            return res;
        }

        public int IncRow(int rowId, int incValue)
        {
            int res = 0;
            if (checkRowId(rowId))
            {
                var row = ordMan.Rows_GetByKey(rowId);
                row.Qty += incValue;
                if (row.Qty < 0)
                    return 0;

                res = ordMan.Rows_Update(row);
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
                var row = ordMan.Rows_GetByKey(rowId);
                row.Qty = qty;
                res = ordMan.Rows_Update(row);
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
        /// check if rowId is in current order
        /// </summary>
        private bool checkRowId(int rowId)
        {
            bool res = false;
            var filter = new RF();
            filter.OrderId = this.OrderId > 0 ? this.OrderId : -1;
            filter.Id = rowId > 0 ? rowId : -1;
            //sec check
            var list = ordMan.Rows_GetByFilter(filter, "");
            if (list.Count == 1)
                res = true;

            return res; 
        }

    }//class
}//ns