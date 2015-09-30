using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace PigeonCms.Shop
{
	//TOCHECK-LOLLO
    public class Coupon : ITable
    {

        public Coupon()
        {
        }

        /// <summary>
        /// Automatic Id as PKey
        /// </summary>
        [DataObjectField(true)]
        public int Id { get; set; }

        public bool Enabled { get; set; }
        public bool IsPercentage { get; set; }

        private string code = "";
        [DataObjectField(true)]
        public string Code
        {
            [DebuggerStepThrough()]
            get { return code; }
            [DebuggerStepThrough()]
            set { code = value; }
        }

        private DateTime dateInserted = DateTime.MinValue;
        [DataObjectField(true)]
        public DateTime DateInserted
        {
            [DebuggerStepThrough()]
            get { return dateInserted; }
            [DebuggerStepThrough()]
            set { dateInserted = value; }
        }

        private DateTime dateUpdated = DateTime.MinValue;
        [DataObjectField(true)]
        public DateTime DateUpdated
        {
            [DebuggerStepThrough()]
            get { return dateUpdated; }
            [DebuggerStepThrough()]
            set { dateUpdated = value; }
        }

        private string userInserted = "";
        [DataObjectField(true)]
        public string UserInserted
        {
            [DebuggerStepThrough()]
            get { return userInserted; }
            [DebuggerStepThrough()]
            set { userInserted = value; }
        }

        private string userUpdated = "";
        [DataObjectField(true)]
        public string UserUpdated
        {
            [DebuggerStepThrough()]
            get { return userUpdated; }
            [DebuggerStepThrough()]
            set { userUpdated = value; }
        }

        private DateTime validFrom = DateTime.MinValue;
        [DataObjectField(true)]
        public DateTime ValidFrom
        {
            [DebuggerStepThrough()]
            get { return validFrom; }
            [DebuggerStepThrough()]
            set { validFrom = value; }
        }

        private DateTime validTo = DateTime.MinValue;
        [DataObjectField(true)]
        public DateTime ValidTo
        {
            [DebuggerStepThrough()]
            get { return validTo; }
            [DebuggerStepThrough()]
            set { validTo = value; }
        }

        private decimal amount = 0m;
        [DataObjectField(true)]
        public decimal Amount
        {
            [DebuggerStepThrough()]
            get { return amount; }
            [DebuggerStepThrough()]
            set { amount = value; }
        }

        private decimal minOrderAmount = 0m;
        [DataObjectField(false)]
        public decimal MinOrderAmount
        {
            [DebuggerStepThrough()]
            get { return minOrderAmount; }
            [DebuggerStepThrough()]
            set { minOrderAmount = value; }
        }

        private string itemType = "";
        [DataObjectField(false)]
        public string ItemType
        {
            [DebuggerStepThrough()]
            get { return itemType; }
            [DebuggerStepThrough()]
            set { itemType = value; }
        }

        private int maxUses = 0;
        [DataObjectField(false)]
        public int MaxUses
        {
            [DebuggerStepThrough()]
            get { return maxUses; }
            [DebuggerStepThrough()]
            set { maxUses = value; }
        }

        private int usesCounter = 0;
        [DataObjectField(false)]
        public int UsesCounter
        {
            [DebuggerStepThrough()]
            get { return usesCounter; }
            [DebuggerStepThrough()]
            set { usesCounter = value; }
        }

        string categoriesIdListString = "";
        [DataObjectField(false)]
        public string CategoriesIdListString
        {
            [DebuggerStepThrough()]
            get { return categoriesIdListString; }
            [DebuggerStepThrough()]
            set { categoriesIdListString = value; }
        }

        string itemsIdListString = "";
        [DataObjectField(false)]
        public string ItemsIdListString
        {
            [DebuggerStepThrough()]
            get { return itemsIdListString; }
            [DebuggerStepThrough()]
            set { itemsIdListString = value; }
        }

        /// <summary>
        /// list of categories id valid for current coupon
        /// </summary>
        public List<int> CategoriesIdList
        {
            get 
            {
                var res = new List<int>();
                var list = Utility.String2List(this.CategoriesIdListString, ",");
                foreach (var item in list)
                {
                    int value = 0;
                    int.TryParse(item, out value);
                    if (value > 0)
                        res.Add(value);
                }
                return res;
            }
        }

        /// <summary>
        /// list of items id valid for current coupon
        /// </summary>
        public List<int> ItemsIdList
        {
            get
            {
                var res = new List<int>();
                var list = Utility.String2List(this.ItemsIdListString, ",");
                foreach (var item in list)
                {
                    int value = 0;
                    int.TryParse(item, out value);
                    if (value > 0)
                        res.Add(value);
                }
                return res;
            }
        }

        public bool IsValid//(decimal currentOrderTotal)
        {
            get
            {
                bool res = true;

                if (this.Enabled == false)
                    res = false;

                if (this.MaxUses > 0 && this.UsesCounter >= this.MaxUses)
                    res = false;

                DateTime now = DateTime.Now.Date;
                if (this.ValidFrom != DateTime.MinValue && now < this.ValidFrom.Date)
                    res = false;

                if (this.ValidTo != DateTime.MinValue && now > this.ValidTo.Date)
                    res = false;

                //if (minOrderAmount > currentOrderTotal)
                //    res = false;

                return res;
            }
        }

    }

    [Serializable]
    public class CouponsFilter
    {
        private int id = 0;
        private string code = "";
        private int ownerUser = 0;
        private DatesRange validFromRange = new DatesRange(DatesRange.RangeType.Always);
        private DatesRange validToRange = new DatesRange(DatesRange.RangeType.Always);
        private Utility.TristateBool enabled = Utility.TristateBool.NotSet;
        private Utility.TristateBool isValid = Utility.TristateBool.NotSet;
        private string itemType = "";
        //private List<int> categoriesIdList = new List<int>();


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
        public string Code
        {
            [DebuggerStepThrough()]
            get { return code; }
            [DebuggerStepThrough()]
            set { code = value; }
        }

        public DatesRange ValidFromRange
        {
            [DebuggerStepThrough()]
            get { return validFromRange; }
            [DebuggerStepThrough()]
            set { validFromRange = value; }
        }

        public DatesRange ValidToRange
        {
            [DebuggerStepThrough()]
            get { return validToRange; }
            [DebuggerStepThrough()]
            set { validToRange = value; }
        }

        public Utility.TristateBool Enabled
        {
            [DebuggerStepThrough()]
            get { return enabled; }
            [DebuggerStepThrough()]
            set { enabled = value; }
        }

        public Utility.TristateBool IsValid
        {
            [DebuggerStepThrough()]
            get { return isValid; }
            [DebuggerStepThrough()]
            set { isValid = value; }
        }

        public string ItemType
        {
            [DebuggerStepThrough()]
            get { return itemType; }
            [DebuggerStepThrough()]
            set { itemType = value; }
        }

        ////list of categories in wich the coupon is valid
        //public List<int> CategoriesIdList
        //{
        //    [DebuggerStepThrough()]
        //    get { return categoriesIdList; }
        //    [DebuggerStepThrough()]
        //    set { categoriesIdList = value; }
        //}

    }

}
