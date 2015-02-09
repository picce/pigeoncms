using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using PigeonCms;
using System.Diagnostics;
using System.Web;


namespace PigeonCms.Shop
{

    public class Order : ITable
    {
        public Order()
        {
        }

        /// <summary>
        /// Automatic Id as PKey
        /// </summary>
        [DataObjectField(true)]
        public int Id { get; set; }


        private string orderRef = "";
        [DataObjectField(false)]
        public string OrderRef
        {
            [DebuggerStepThrough()]
            get { return orderRef; }
            [DebuggerStepThrough()]
            set { orderRef = value; }
        }

        private string ownerUser = "";
        /// <summary>
        /// order owner
        /// </summary>
        [DataObjectField(false)]
        public string OwnerUser
        {
            [DebuggerStepThrough()]
            get { return ownerUser; }
            [DebuggerStepThrough()]
            set { ownerUser = value; }
        }

        int customerId = 0;
        [DataObjectField(true)]
        public int CustomerId
        {
            [DebuggerStepThrough()]
            get { return customerId; }
            [DebuggerStepThrough()]
            set { customerId = value; }
        }

        private DateTime orderDate;
        [DataObjectField(false)]
        public DateTime OrderDate
        {
            [DebuggerStepThrough()]
            get { return orderDate; }
            [DebuggerStepThrough()]
            set { orderDate = value; }
        }

        private DateTime orderDateRequested;
        [DataObjectField(false)]
        public DateTime OrderDateRequested
        {
            [DebuggerStepThrough()]
            get { return orderDateRequested; }
            [DebuggerStepThrough()]
            set { orderDateRequested = value; }
        }

        private DateTime orderDateShipped;
        [DataObjectField(false)]
        public DateTime OrderDateShipped
        {
            [DebuggerStepThrough()]
            get { return orderDateShipped; }
            [DebuggerStepThrough()]
            set { orderDateShipped = value; }
        }

        private DateTime dateInserted;
        [DataObjectField(false)]
        public DateTime DateInserted
        {
            [DebuggerStepThrough()]
            get { return dateInserted; }
            [DebuggerStepThrough()]
            set { dateInserted = value; }
        }

        private string userInserted = "";
        [DataObjectField(false)]
        public string UserInserted
        {
            [DebuggerStepThrough()]
            get { return userInserted; }
            [DebuggerStepThrough()]
            set { userInserted = value; }
        }

        private DateTime dateUpdated;
        [DataObjectField(false)]
        public DateTime DateUpdated
        {
            [DebuggerStepThrough()]
            get { return dateUpdated; }
            [DebuggerStepThrough()]
            set { dateUpdated = value; }
        }

        private string userUpdated = "";
        [DataObjectField(false)]
        public string UserUpdated
        {
            [DebuggerStepThrough()]
            get { return userUpdated; }
            [DebuggerStepThrough()]
            set { userUpdated = value; }
        }

        private bool confirmed = false;
        [DataObjectField(false)]
        public bool Confirmed
        {
            [DebuggerStepThrough()]
            get { return confirmed; }
            [DebuggerStepThrough()]
            set { confirmed = value; }
        }

        private bool paid = false;
        [DataObjectField(false)]
        public bool Paid
        {
            [DebuggerStepThrough()]
            get { return paid; }
            [DebuggerStepThrough()]
            set { paid = value; }
        }

        private bool processed = false;
        [DataObjectField(false)]
        public bool Processed
        {
            [DebuggerStepThrough()]
            get { return processed; }
            [DebuggerStepThrough()]
            set { processed = value; }
        }

        private bool invoiced = false;
        [DataObjectField(false)]
        public bool Invoiced
        {
            [DebuggerStepThrough()]
            get { return invoiced; }
            [DebuggerStepThrough()]
            set { invoiced = value; }
        }

        private string notes = "";
        [DataObjectField(false)]
        public string Notes
        {
            [DebuggerStepThrough()]
            get { return notes; }
            [DebuggerStepThrough()]
            set { notes = value; }
        }

        private decimal qtyAmount = 0;
        [DataObjectField(false)]
        public decimal QtyAmount
        {
            [DebuggerStepThrough()]
            get { return qtyAmount; }
            [DebuggerStepThrough()]
            set { qtyAmount = value; }
        }

        private decimal orderAmount = 0;
        [DataObjectField(false)]
        public decimal OrderAmount
        {
            [DebuggerStepThrough()]
            get { return orderAmount; }
            [DebuggerStepThrough()]
            set { orderAmount = value; }
        }

        private decimal shipAmount = 0;
        [DataObjectField(false)]
        public decimal ShipAmount
        {
            [DebuggerStepThrough()]
            get { return shipAmount; }
            [DebuggerStepThrough()]
            set { shipAmount = value; }
        }

        private decimal totalAmount = 0;
        [DataObjectField(false)]
        public decimal TotalAmount
        {
            [DebuggerStepThrough()]
            get { return totalAmount; }
            [DebuggerStepThrough()]
            set { totalAmount = value; }
        }

        private decimal totalPaid = 0;
        [DataObjectField(false)]
        public decimal TotalPaid
        {
            [DebuggerStepThrough()]
            get { return totalPaid; }
            [DebuggerStepThrough()]
            set { totalPaid = value; }
        }

        private string currency = "";
        [DataObjectField(false)]
        public string Currency
        {
            [DebuggerStepThrough()]
            get { return currency; }
            [DebuggerStepThrough()]
            set { currency = value; }
        }

        private int vatPercentage = 0;
        [DataObjectField(false)]
        public int VatPercentage
        {
            [DebuggerStepThrough()]
            get { return vatPercentage; }
            [DebuggerStepThrough()]
            set { vatPercentage = value; }
        }

        private int invoiceId = 0;
        [DataObjectField(false)]
        public int InvoiceId
        {
            [DebuggerStepThrough()]
            get { return invoiceId; }
            [DebuggerStepThrough()]
            set { invoiceId = value; }
        }

        private string invoiceRef = "";
        [DataObjectField(false)]
        public string InvoiceRef
        {
            [DebuggerStepThrough()]
            get { return invoiceRef; }
            [DebuggerStepThrough()]
            set { invoiceRef = value; }
        }

        //ORDER customer details HERE
        private string ordName = "";
        public string OrdName
        {
            [DebuggerStepThrough()]
            get { return ordName; }
            [DebuggerStepThrough()]
            set { ordName = value; }
        }

        private string ordAddress = "";
        public string OrdAddress
        {
            [DebuggerStepThrough()]
            get { return ordAddress; }
            [DebuggerStepThrough()]
            set { ordAddress = value; }
        }

        private string ordZipCode = "";
        public string OrdZipCode
        {
            [DebuggerStepThrough()]
            get { return ordZipCode; }
            [DebuggerStepThrough()]
            set { ordZipCode = value; }
        }

        private string ordCity = "";
        public string OrdCity
        {
            [DebuggerStepThrough()]
            get { return ordCity; }
            [DebuggerStepThrough()]
            set { ordCity = value; }
        }

        private string ordState = "";
        public string OrdState
        {
            [DebuggerStepThrough()]
            get { return ordState; }
            [DebuggerStepThrough()]
            set { ordState = value; }
        }

        private string ordNation = "";
        public string OrdNation
        {
            [DebuggerStepThrough()]
            get { return ordNation; }
            [DebuggerStepThrough()]
            set { ordNation = value; }
        }

        private string ordPhone = "";
        public string OrdPhone
        {
            [DebuggerStepThrough()]
            get { return ordPhone; }
            [DebuggerStepThrough()]
            set { ordPhone = value; }
        }

        private string ordEmail = "";
        public string OrdEmail
        {
            [DebuggerStepThrough()]
            get { return ordEmail; }
            [DebuggerStepThrough()]
            set { ordEmail = value; }
        }


        private string codeCoupon = "";
        [DataObjectField(false)]
        public string CodeCoupon
        {
            [DebuggerStepThrough()]
            get { return codeCoupon; }
            [DebuggerStepThrough()]
            set { codeCoupon = value; }
        }

        private string paymentCode = "";
        [DataObjectField(false)]
        public string PaymentCode
        {
            [DebuggerStepThrough()]
            get { return paymentCode; }
            [DebuggerStepThrough()]
            set { paymentCode = value; }
        }

        private string shipCode = "";
        [DataObjectField(false)]
        public string ShipCode
        {
            [DebuggerStepThrough()]
            get { return shipCode; }
            [DebuggerStepThrough()]
            set { shipCode = value; }
        }



        List<OrderRow> rows = null;
        public List<OrderRow> Rows
        {
            get
            {
                if (rows == null)
                {
                    var filter = new OrderRowsFilter();
                    filter.OrderId = this.Id;
                    rows = new OrderRowsManager().GetByFilter(filter, "");
                }
                return rows;
            }
        }

        decimal? itemsCount = null;
        public decimal? ItemsCount
        {
            get
            {
                if (itemsCount == null)
                {
                    itemsCount = 0;
                    foreach (var item in this.Rows)
                        itemsCount += (decimal)item.Qty;
                }
                return itemsCount;
            }
        }


        //DroidCatalogue.Customer customer = null;
        //public DroidCatalogue.Customer Customer
        //{
        //    get
        //    {
        //        if (customer == null)
        //        {
        //            customer = new DroidCatalogue.CustomersManager(
        //                this.UserInserted).GetByKey(this.CustomerId);
        //        }
        //        return customer;
        //    }
        //}
    }

    [Serializable]
    public class OrdersFilter
    {
        private int id = 0;
        private string orderRef = "";
        private string ownerUser = "";
        private int customerId = 0;
        private Utility.TristateBool confirmed = Utility.TristateBool.NotSet;
        private Utility.TristateBool paid = Utility.TristateBool.NotSet;
        private Utility.TristateBool processed = Utility.TristateBool.NotSet;


        /// <summary>
        /// PKey
        /// </summary>
        public int Id
        {
            [DebuggerStepThrough()]
            get { return id; }
            [DebuggerStepThrough()]
            set { id = value; }
        }

        /// <summary>
        /// IX unique key
        /// </summary>
        public string OrderRef
        {
            [DebuggerStepThrough()]
            get { return orderRef; }
            [DebuggerStepThrough()]
            set { orderRef = value; }
        }

        public string OwnerUser
        {
            [DebuggerStepThrough()]
            get { return ownerUser; }
            [DebuggerStepThrough()]
            set { ownerUser = value; }
        }

        public int CustomerId
        {
            [DebuggerStepThrough()]
            get { return customerId; }
            [DebuggerStepThrough()]
            set { customerId = value; }
        }

        public Utility.TristateBool Confirmed
        {
            [DebuggerStepThrough()]
            get { return confirmed; }
            [DebuggerStepThrough()]
            set { confirmed = value; }
        }

        public Utility.TristateBool Paid
        {
            [DebuggerStepThrough()]
            get { return paid; }
            [DebuggerStepThrough()]
            set { paid = value; }
        }

        public Utility.TristateBool Processed
        {
            [DebuggerStepThrough()]
            get { return processed; }
            [DebuggerStepThrough()]
            set { processed = value; }
        }
    }


}
