using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace PigeonCms.Shop
{
    class ProductItem: Item
    {       
        public ProductItem() 
        {
            base.ItemTypeName = "Shop.ProductItem";
        }

        [ItemFieldMapAttribute(ItemFieldMapAttribute.CustomFields.CustomString1)]
        public decimal Code
        {
            [DebuggerStepThrough()]
            get { return base.CustomDecimal1; }
            [DebuggerStepThrough()]
            set { base.CustomDecimal1 = value; }
        }

        [ItemFieldMapAttribute(ItemFieldMapAttribute.CustomFields.CustomDecimal1)]
        public decimal Price
        {
            [DebuggerStepThrough()]
            get { return base.CustomDecimal1; }
            [DebuggerStepThrough()]
            set { base.CustomDecimal1 = value; }
        }

        [ItemFieldMapAttribute(ItemFieldMapAttribute.CustomFields.CustomDecimal2)]
        public decimal OfferPrice
        {
            [DebuggerStepThrough()]
            get { return base.CustomDecimal2; }
            [DebuggerStepThrough()]
            set { base.CustomDecimal2 = value; }
        }

        [ItemFieldMapAttribute(ItemFieldMapAttribute.CustomFields.CustomInt1)]
        public int Availability
        {
            [DebuggerStepThrough()]
            get { return base.CustomInt1; }
            [DebuggerStepThrough()]
            set { base.CustomInt1 = value; }
        }

        [ItemFieldMapAttribute(ItemFieldMapAttribute.CustomFields.CustomInt2)]
        public int Review
        {
            [DebuggerStepThrough()]
            get { return base.CustomInt2; }
            [DebuggerStepThrough()]
            set { base.CustomInt2 = value; }
        }

    }

    [Serializable]
    public class ProductItemFilter : ItemsFilter
    {
        public ProductItemFilter()
        {
            this.ItemType = "Shop.ProductItem";
        }

        public string Code
        {
            [DebuggerStepThrough()]
            get { return base.CustomString1; }
            [DebuggerStepThrough()]
            set { base.CustomString1 = value; }
        }

        //public decimal Price
        //{
        //    [DebuggerStepThrough()]
        //    get { return base.CustomDecimal1; }
        //    [DebuggerStepThrough()]
        //    set { base.CustomDecimal1 = value; }
        //}

        //public decimal OfferPrice
        //{
        //    [DebuggerStepThrough()]
        //    get { return base.CustomDecimal2; }
        //    [DebuggerStepThrough()]
        //    set { base.CustomDecimal2 = value; }
        //}

        public int Availability
        {
            [DebuggerStepThrough()]
            get { return base.CustomInt1; }
            [DebuggerStepThrough()]
            set { base.CustomInt1 = value; }
        }

        public int Review
        {
            [DebuggerStepThrough()]
            get { return base.CustomInt2; }
            [DebuggerStepThrough()]
            set { base.CustomInt2 = value; }
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

}
