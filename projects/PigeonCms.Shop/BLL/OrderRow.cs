using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using PigeonCms;
using System.Diagnostics;


namespace PigeonCms.Shop
{

    public class OrderRow : ITable
    {
        public OrderRow()
        {
        }

        /// <summary>
        /// Automatic Id as PKey
        /// </summary>
        [DataObjectField(true)]
        public int Id { get; set; }

        [DataObjectField(false)]
        public int OrderId { get; set; }

        private string productCode = "";
        [DataObjectField(false)]
        public string ProductCode
        {
            [DebuggerStepThrough()]
            get { return productCode; }
            [DebuggerStepThrough()]
            set { productCode = value; }
        }

        private decimal priceFull = 0.0m;
        [DataObjectField(false)]
        public decimal PriceFull
        {
            [DebuggerStepThrough()]
            get { return priceFull; }
            [DebuggerStepThrough()]
            set { priceFull = value; }
        }

        private decimal priceNet = 0.0m;
        [DataObjectField(false)]
        public decimal PriceNet
        {
            [DebuggerStepThrough()]
            get { return priceNet; }
            [DebuggerStepThrough()]
            set { priceNet = value; }
        }

        private decimal qty = 0m;
        [DataObjectField(false)]
        public decimal Qty
        {
            [DebuggerStepThrough()]
            get { return qty; }
            [DebuggerStepThrough()]
            set { qty = value; }
        }

        private string rowNotes = "";
        [DataObjectField(false)]
        public string RowNotes
        {
            [DebuggerStepThrough()]
            get { return rowNotes; }
            [DebuggerStepThrough()]
            set { rowNotes = value; }
        }

        public decimal RowPrice
        {
            get
            {
                return this.PriceFull * this.qty;
            }
        }


        Order order = null;
        public Order Order
        {
            get
            {
                if (order == null)
                {
                    order = new OrdersManager().GetByKey(
                        this.OrderId);
                }
                return order;
            }
        }

        //DroidCatalogue.DroidItem droidItem = null;
        //public DroidCatalogue.DroidItem DroidItem
        //{
        //    get
        //    {
        //        if (droidItem == null)
        //        {
        //            droidItem = new DroidCatalogue.DroidItemsManager(false, false).GetByKey(
        //                this.DroidItemId);
        //        }
        //        return droidItem;
        //    }
        //}

        [DataObjectField(false)]
        public decimal Amount
        {
            get
            {
                return this.Qty * this.PriceNet;
            }
        }
    }

    [Serializable]
    public class OrderRowsFilter
    {
        private int id = 0;
        private int orderId = 0;

        public int Id
        {
            [DebuggerStepThrough()]
            get { return id; }
            [DebuggerStepThrough()]
            set { id = value; }
        }

        public int OrderId
        {
            [DebuggerStepThrough()]
            get { return orderId; }
            [DebuggerStepThrough()]
            set { orderId = value; }
        }
    }


}
