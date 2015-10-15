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
    public interface IOrder : ITable
    {
        int Id { get; set; }
        string OrderRef { get; set; }
        string OwnerUser { get; set; }
        int CustomerId { get; set; }
        DateTime OrderDate { get; set; }
        DateTime OrderDateRequested { get; set; }
        DateTime OrderDateShipped { get; set; }
        DateTime DateInserted { get; set; }
        string UserInserted { get; set; }
        DateTime DateUpdated { get; set; }
        string UserUpdated { get; set; }
        bool Confirmed { get; set; }
        bool Paid { get; set; }
        bool Processed { get; set; }
        bool Invoiced { get; set; }
        string Notes { get; set; }
        decimal QtyAmount { get; set; }
        decimal OrderAmount { get; set; }
        decimal ShipAmount { get; set; }
        decimal TotalAmount { get; set; }
        decimal TotalPaid { get; set; }
        string Currency { get; set; }
        int InvoiceId { get; set; }
        string InvoiceRef { get; set; }
        
        //ORDER customer details HERE
        string OrdName { get; set; }
        string OrdAddress { get; set; }
        string OrdZipCode { get; set; }
        string OrdCity { get; set; }
        string OrdState { get; set; }
        string OrdNation { get; set; }
        string OrdPhone { get; set; }
        string OrdEmail { get; set; }

        string CouponCode { get; set; }
        decimal CouponValue { get; set; }
        bool CouponIsPercentage { get; set; }
        string PaymentCode { get; set; }
        string ShipCode { get; set; }
        string JsData { get; set; }
        string Custom1 { get; set; }
        string Custom2 { get; set; }
        string Custom3 { get; set; }
    }

    public interface IOrderFilter
    {
        int Id { get; set; }
        string OrderRef { get; set; }
        string OwnerUser { get; set; }
        int CustomerId { get; set; }
        Utility.TristateBool Confirmed { get; set; }
        Utility.TristateBool Paid { get; set; }
        Utility.TristateBool Processed { get; set; }
        string CouponCode { get; set; }
        List<int> ExcludeIdList { get; set; }
        DatesRange OrderDatesRange { get; set; }
        
        void Reset();
    }


    public class Order : IOrder
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
        /// <summary>
        /// sum of rows AmountWithTaxes
        /// </summary>
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
        /// <summary>
        /// Calculated by OM.CalculateSummary(orderId)
        /// ex. OrderAmount + ShipAmount - OM.GetCouponAmount;
        /// </summary>
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


        private string couponCode = "";
        [DataObjectField(false)]
        public string CouponCode
        {
            [DebuggerStepThrough()]
            get { return couponCode; }
            [DebuggerStepThrough()]
            set { couponCode = value; }
        }

        private decimal couponValue = 0;
        [DataObjectField(false)]
        public decimal CouponValue
        {
            [DebuggerStepThrough()]
            get { return couponValue; }
            [DebuggerStepThrough()]
            set { couponValue = value; }
        }

        private bool couponIsPercentage = false;
        /// <summary>
        /// allow couponValue as percentage (ex: 0.03 = 3%)
        /// if true, couponValue is <= 1
        /// </summary>
        [DataObjectField(false)]
        public bool CouponIsPercentage
        {
            [DebuggerStepThrough()]
            get { return couponIsPercentage; }
            [DebuggerStepThrough()]
            set { couponIsPercentage = value; }
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

        private string jsData = "";
        /// <summary>
        /// json serialized obj
        /// </summary>
        public string JsData
        {
            [DebuggerStepThrough()]
            get { return jsData; }
            [DebuggerStepThrough()]
            set { jsData = value; }
        }

        private string custom1 = "";
        public string Custom1
        {
            [DebuggerStepThrough()]
            get { return custom1; }
            [DebuggerStepThrough()]
            set { custom1 = value; }
        }

        private string custom2 = "";
        public string Custom2
        {
            [DebuggerStepThrough()]
            get { return custom2; }
            [DebuggerStepThrough()]
            set { custom2 = value; }
        }

        private string custom3 = "";
        public string Custom3
        {
            [DebuggerStepThrough()]
            get { return custom3; }
            [DebuggerStepThrough()]
            set { custom3 = value; }
        }


        List<OrderRow> rows = null;
        public List<OrderRow> Rows
        {
            get
            {
                if (rows == null)
                {
                    var filter = new OrderRowsFilter();
                    filter.OrderId = (this.Id > 0 ? this.Id : -1);
                    var man = new OrdersManager<Order, OrdersFilter, OrderRow, OrderRowsFilter>();
                    rows = man.Rows_GetByFilter(filter, "");
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

    }

    [Serializable]
    public class OrdersFilter: IOrderFilter
    {
        private int id = 0;
        private string orderRef = "";
        private string ownerUser = "";
        private int customerId = 0;
        private Utility.TristateBool confirmed = Utility.TristateBool.NotSet;
        private Utility.TristateBool paid = Utility.TristateBool.NotSet;
        private Utility.TristateBool processed = Utility.TristateBool.NotSet;
        private string couponCode = "";
        private List<int> excludeIdList = new List<int>();
        private DatesRange orderDatesRange = new DatesRange(DatesRange.RangeType.Always);


        public void Reset()
        {
            id = 0;
            orderRef = "";
            ownerUser = "";
            customerId = 0;
            confirmed = Utility.TristateBool.NotSet;
            paid = Utility.TristateBool.NotSet;
            processed = Utility.TristateBool.NotSet;
            couponCode = "";
            excludeIdList = new List<int>();
            orderDatesRange = new DatesRange(DatesRange.RangeType.Always);
        }

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

        public string CouponCode
        {
            [DebuggerStepThrough()]
            get { return couponCode; }
            [DebuggerStepThrough()]
            set { couponCode = value; }
        }

        //orders id to exclude from selection
        public List<int> ExcludeIdList
        {
            [DebuggerStepThrough()]
            get { return excludeIdList; }
            [DebuggerStepThrough()]
            set { excludeIdList = value; }
        }

        public DatesRange OrderDatesRange
        {
            [DebuggerStepThrough()]
            get { return orderDatesRange; }
            [DebuggerStepThrough()]
            set { orderDatesRange = value; }
        }


    }


}
