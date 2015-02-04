using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Diagnostics;
using System.ComponentModel;
using System.IO;
using PigeonCms;
using System.Collections.Generic;
using System.Threading;



namespace PigeonCms
{
    public class TicketItem: Item
    {
        public TicketItem() 
        {
            base.ItemTypeName = "PigeonCms.TicketItem";        
        }

        public enum TicketStatusEnum
        {
            Open = 0,
            WorkInProgress,
            Closed, 
            Locked
        }

        public enum TicketPriorityEnum
        {
            Low = 0,
            Medium,
            High
        }

        [ItemFieldMapAttribute(ItemFieldMapAttribute.CustomFields.CustomInt1)]
        public int Status
        {
            [DebuggerStepThrough()]
            get { return base.CustomInt1; }
            [DebuggerStepThrough()]
            set { base.CustomInt1 = value; }
        }

        [ItemFieldMapAttribute(ItemFieldMapAttribute.CustomFields.CustomInt2)]
        public int Priority
        {
            [DebuggerStepThrough()]
            get { return base.CustomInt2; }
            [DebuggerStepThrough()]
            set { base.CustomInt2 = value; }
        }

        [ItemFieldMapAttribute(ItemFieldMapAttribute.CustomFields.CustomInt3)]
        public int CustomerId
        {
            [DebuggerStepThrough()]
            get { return base.CustomInt3; }
            [DebuggerStepThrough()]
            set { base.CustomInt3 = value; }
        }

        [ItemFieldMapAttribute(ItemFieldMapAttribute.CustomFields.CustomBool1)]
        public bool Flagged
        {
            [DebuggerStepThrough()]
            get { return base.CustomBool1; }
            [DebuggerStepThrough()]
            set { base.CustomBool1 = value; }
        }

        [ItemFieldMapAttribute(ItemFieldMapAttribute.CustomFields.CustomString1)]
        public string UserAssigned
        {
            [DebuggerStepThrough()]
            get { return base.CustomString1; }
            [DebuggerStepThrough()]
            set { base.CustomString1 = value; }
        }

        [ItemFieldMapAttribute(ItemFieldMapAttribute.CustomFields.CustomString2)]
        public string Link
        {
            [DebuggerStepThrough()]
            get { return base.CustomString2; }
        }

        [ItemFieldMapAttribute(ItemFieldMapAttribute.CustomFields.CustomString3)]
        public string Tags
        {
            [DebuggerStepThrough()]
            get { return base.CustomString3; }
        }

        private Customer customer = null;
        public Customer Customer
        {
            get
            {
                if (customer == null)
                {
                    customer = new Customer();
                    if (this.CustomerId > 0)
                        customer = new CustomersManager().GetByKey(this.CustomerId);
                }
                return customer;
            }
        }

        public int CompareTo(TicketItem obj, string field)
        {
            int res = 0;

            switch (field.ToLower())
            {
                case "id":
                    res = this.Id.CompareTo(obj.Id);
                    break;
                case "title":
                    res = this.Title.CompareTo(obj.Title);
                    break;
                case "dateinserted":
                    res = this.DateInserted.CompareTo(obj.DateInserted);
                    break;
                case "dateupdated":
                    res = this.DateUpdated.CompareTo(obj.DateUpdated);
                    break;
                case "status":
                    res = this.Status.CompareTo(obj.Status);
                    break;
                case "priority":
                    res = this.Priority.CompareTo(obj.Priority);
                    break;
                case "flagged":
                    res = this.Flagged.CompareTo(obj.Flagged);
                    break;
                default:
                    res = this.Id.CompareTo(obj.Id);
                    break;
            }
            return res;
        }

        public class TicketItemComparer : IComparer<TicketItem>
        {
            private string sortExpression = "";
            private SortDirection sortDirection;


            public TicketItemComparer(string sortExpression, SortDirection sortDirection)
            {
                this.sortExpression = sortExpression;
                this.sortDirection = sortDirection;
            }

            public int Compare(TicketItem lhs, TicketItem rhs)
            {
                if (this.sortDirection == SortDirection.Descending)
                    return rhs.CompareTo(lhs, sortExpression);
                else
                    return lhs.CompareTo(rhs, sortExpression);
            }

            public bool Equals(TicketItem lhs, TicketItem rhs)
            {
                return this.Compare(lhs, rhs) == 0;
            }

            public int GetHashCode(TicketItem e)
            {
                return e.GetHashCode();
            }
        }
    }

    [Serializable]
    public class TicketItemFilter : ItemsFilter
    {
        public TicketItemFilter() 
        {
            this.ItemType = "PigeonCms.TicketItem";
        }

        public int Status
        {
            [DebuggerStepThrough()]
            get { return base.CustomInt1; }
            [DebuggerStepThrough()]
            set { base.CustomInt1 = value; }
        }

        public int Priority
        {
            [DebuggerStepThrough()]
            get { return base.CustomInt2; }
            [DebuggerStepThrough()]
            set { base.CustomInt2 = value; }
        }

        public int CustomerId
        {
            [DebuggerStepThrough()]
            get { return base.CustomInt3; }
            [DebuggerStepThrough()]
            set { base.CustomInt3 = value; }
        }

        public Utility.TristateBool Flagged
        {
            [DebuggerStepThrough()]
            get { return base.CustomBool1; }
            [DebuggerStepThrough()]
            set { base.CustomBool1 = value; }
        }

        public string UserAssigned
        {
            [DebuggerStepThrough()]
            get { return base.CustomString1; }
            [DebuggerStepThrough()]
            set { base.CustomString1 = value; }
        }

        private DatesRange itemInsertedRange = new DatesRange(DatesRange.RangeType.Always);
        public DatesRange ItemInsertedRange
        {
            [DebuggerStepThrough()]
            get { return itemInsertedRange; }
            [DebuggerStepThrough()]
            set { itemInsertedRange = value; }
        }

    }

    public class TicketItemsManager : ItemsManager<TicketItem, TicketItemFilter>
    {
        public TicketItemsManager(bool checkUserContext, bool writeMode)
            : base(checkUserContext, writeMode)
        { }

        public override List<TicketItem> GetByFilter(TicketItemFilter filter, string sort)
        {
            filter.ItemType = "PigeonCms.TicketItem";
            var list = new List<TicketItem>();
            var fullList = base.GetByFilter(filter, sort);
            foreach (var item in fullList)
            {
                //apply additional filters
                bool bAdd = true;

                if (filter.Status > -1)
                    if (item.Status != filter.Status) bAdd = false;

                if (filter.Priority > -1)
                    if (item.Priority != filter.Priority) bAdd = false;

                if (filter.ItemInsertedRange.DateRangeType != DatesRange.RangeType.Always)
                {
                    if (filter.ItemInsertedRange.InitDate != DateTime.MinValue)
                        if (item.DateInserted < filter.ItemInsertedRange.InitDate) bAdd = false;

                    if (filter.ItemInsertedRange.EndDate != DateTime.MaxValue)
                        if (item.DateInserted > filter.ItemInsertedRange.EndDate.AddDays(1)) bAdd = false;
                }

                if (bAdd)
                    list.Add(item);
            }
            return list;
        }
    }
}