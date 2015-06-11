using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace PigeonCms.Shop
{
    public class ProductItem: Item
    {       
        public ProductItem() 
        {
            base.ItemTypeName = "Shop.ProductItem";
        }

        [ItemFieldMapAttribute(ItemFieldMapAttribute.CustomFields.CustomString1)]
        public string ProductCode
        {
            [DebuggerStepThrough()]
            get { return base.CustomString1; }
            [DebuggerStepThrough()]
            set { base.CustomString1 = value; }
        }

        [ItemFieldMapAttribute(ItemFieldMapAttribute.CustomFields.CustomDecimal1)]
        public decimal RegularPrice
        {
            [DebuggerStepThrough()]
            get { return base.CustomDecimal1; }
            [DebuggerStepThrough()]
            set { base.CustomDecimal1 = value; }
        }

        [ItemFieldMapAttribute(ItemFieldMapAttribute.CustomFields.CustomDecimal2)]
        public decimal SalePrice
        {
            [DebuggerStepThrough()]
            get { return base.CustomDecimal2; }
            [DebuggerStepThrough()]
            set { base.CustomDecimal2 = value; }
        }

        [ItemFieldMapAttribute(ItemFieldMapAttribute.CustomFields.CustomDecimal3)]
        public decimal Weight
        {
            [DebuggerStepThrough()]
            get { return base.CustomDecimal3; }
            [DebuggerStepThrough()]
            set { base.CustomDecimal3 = value; }
        }

        [ItemFieldMapAttribute(ItemFieldMapAttribute.CustomFields.CustomString2)]
        public string Dimensions
        {
            [DebuggerStepThrough()]
            get { return base.CustomString2; }
            [DebuggerStepThrough()]
            set { base.CustomString2 = value; }
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

        [ItemFieldMapAttribute(ItemFieldMapAttribute.CustomFields.CustomBool1)]
        public bool IsDraft
        {
            [DebuggerStepThrough()]
            get { return base.CustomBool1; }
            [DebuggerStepThrough()]
            set { base.CustomBool1 = value; }
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

    public class ProductItemsManager : ItemsManager<ProductItem, ProductItemFilter>
    {
        public ProductItemsManager()
            : base()
        { }

    }

}
