using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using PigeonCms;
using System.Diagnostics;


namespace PigeonCms.Shop
{
    public interface IOrderRow : ITable
    {
        int Id { get; set; }
        int OrderId { get; set; }
        string ProductCode { get; set; }
        decimal Qty { get; set; }
        decimal PriceNet { get; set; }
        decimal TaxPercentage { get; set; }
        string RowNotes { get; set; }
        PigeonCms.Shop.IOrder Order { get; }
        decimal PriceWithTaxes { get; }
        decimal AmountNet { get; }
        decimal AmountWithTaxes { get; }
    }

    public interface IOrderRowsFilter
    {
        int Id { get; set; }
        int OrderId { get; set; }
    }

    public class OrderRow : IOrderRow
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

        private decimal qty = 0m;
        [DataObjectField(false)]
        public decimal Qty
        {
            [DebuggerStepThrough()]
            get { return qty; }
            [DebuggerStepThrough()]
            set { qty = value; }
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

        private decimal taxPercentage = 0.0m;
        [DataObjectField(false)]
        public decimal TaxPercentage
        {
            [DebuggerStepThrough()]
            get { return taxPercentage; }
            [DebuggerStepThrough()]
            set { taxPercentage = value; }
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

        private IOrder order = null;
        public IOrder Order
        {
            get
            {
                if (order == null)
                {
                    var man = new OrdersManager<Order, OrdersFilter, OrderRow, OrderRowsFilter>();
                    order = man.GetByKey(this.OrderId);
                }
                return order;
            }
        }

        /// <summary>
        /// PriceNet + (PriceNet * TaxPercentage / 100)
        /// </summary>
        [DataObjectField(false)]
        public decimal PriceWithTaxes
        {
            get
            {
                return this.PriceNet + 
                    (this.PriceNet * this.TaxPercentage / 100);
            }
        }

        /// <summary>
        /// Qty * PriceNet
        /// </summary>
        [DataObjectField(false)]
        public decimal AmountNet
        {
            get
            {
                return this.Qty * this.PriceNet;
            }
        }

        /// <summary>
        /// Qty * PriceWithTaxes
        /// </summary>
        public decimal AmountWithTaxes
        {
            get
            {
                return this.Qty * this.PriceWithTaxes;
            }
        }
    }

    [Serializable]
    public class OrderRowsFilter : IOrderRowsFilter
    {
        private int id = 0;
        private int orderId = 0;

        public void Reset()
        {
            id = 0;
            orderId = 0;
        }

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
