using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace PigeonCms.Shop
{
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

        private int userInserted = 0;
        [DataObjectField(true)]
        public int UserInserted
        {
            [DebuggerStepThrough()]
            get { return userInserted; }
            [DebuggerStepThrough()]
            set { userInserted = value; }
        }

        private int userUpdated = 0;
        [DataObjectField(true)]
        public int UserUpdated
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

        //private PigeonCms.Utility.TristateBool enabled = PigeonCms.Utility.TristateBool.NotSet;
        //[DataObjectField(true)]
        //public PigeonCms.Utility.TristateBool Enabled
        //{
        //    [DebuggerStepThrough()]
        //    get { return enabled; }
        //    [DebuggerStepThrough()]
        //    set { enabled = value; }
        //}

        private decimal amount = 0m;
        [DataObjectField(true)]
        public decimal Amount
        {
            [DebuggerStepThrough()]
            get { return amount; }
            [DebuggerStepThrough()]
            set { amount = value; }
        }

        //private PigeonCms.Utility.TristateBool isPercentage = PigeonCms.Utility.TristateBool.NotSet;
        //[DataObjectField(true)]
        //public PigeonCms.Utility.TristateBool IsPercentage
        //{
        //    [DebuggerStepThrough()]
        //    get { return isPercentage; }
        //    [DebuggerStepThrough()]
        //    set { isPercentage = value; }
        //}

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
        private string itemType = "";
        //private List<int> excludeIdList = new List<int>();


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

        public int OwnerUser
        {
            [DebuggerStepThrough()]
            get { return ownerUser; }
            [DebuggerStepThrough()]
            set { ownerUser = value; }
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

        public string ItemType
        {
            [DebuggerStepThrough()]
            get { return itemType; }
            [DebuggerStepThrough()]
            set { itemType = value; }
        }

        ////orders id to exclude from selection
        //public List<int> ExcludeIdList
        //{
        //    [DebuggerStepThrough()]
        //    get { return excludeIdList; }
        //    [DebuggerStepThrough()]
        //    set { excludeIdList = value; }
        //}

    }

}
