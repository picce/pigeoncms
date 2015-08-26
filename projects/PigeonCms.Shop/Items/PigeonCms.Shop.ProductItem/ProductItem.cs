using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace PigeonCms.Shop
{
    public class ProductItem: Item
    {
        public enum ProductTypeEnum
        {
            NotSet = 0,
            Simple = 1,
            Configurable
        }

        public ProductItem() 
        {
            base.ItemTypeName = "PigeonCms.Shop.ProductItem";
        }

        [ItemFieldMapAttribute(ItemFieldMapAttribute.CustomFields.CustomString1)]
        public string SKU
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

        [ItemFieldMapAttribute(ItemFieldMapAttribute.CustomFields.CustomInt1)]
        public int Availability
        {
            [DebuggerStepThrough()]
            get { return base.CustomInt1; }
            [DebuggerStepThrough()]
            set { base.CustomInt1 = value; }
        }

        ////modificare in Decimal
        //[ItemFieldMapAttribute(ItemFieldMapAttribute.CustomFields.CustomInt2)]
        //public int Review
        //{
        //    [DebuggerStepThrough()]
        //    get { return base.CustomInt2; }
        //    [DebuggerStepThrough()]
        //    set { base.CustomInt2 = value; }
        //}

        [ItemFieldMapAttribute(ItemFieldMapAttribute.CustomFields.CustomInt2)]
        public int AttributeSet
        {
            [DebuggerStepThrough()]
            get { return base.CustomInt2; }
            [DebuggerStepThrough()]
            set { base.CustomInt2 = value; }
        }

        [ItemFieldMapAttribute(ItemFieldMapAttribute.CustomFields.CustomInt3)]
        public ProductItem.ProductTypeEnum ProductType
        {
            [DebuggerStepThrough()]
            get 
            {
                ProductItem.ProductTypeEnum res = ProductTypeEnum.NotSet;
                try
                {
                    res = (ProductItem.ProductTypeEnum)base.CustomInt3;
                }
                catch { }
                return res;
            }
            [DebuggerStepThrough()]
            set { base.CustomInt3 = (int)value; }
        }

        [ItemFieldMapAttribute(ItemFieldMapAttribute.CustomFields.CustomBool1)]
        public bool IsDraft
        {
            [DebuggerStepThrough()]
            get { return base.CustomBool1; }
            [DebuggerStepThrough()]
            set { base.CustomBool1 = value; }
        }

        [ItemFieldMapAttribute(ItemFieldMapAttribute.CustomFields.CustomBool2)]
        public bool InStock
        {
            [DebuggerStepThrough()]
            get { return base.CustomBool2; }
            [DebuggerStepThrough()]
            set { base.CustomBool2 = value; }
        }

    }

    [Serializable]
    public class ProductItemFilter : ItemsFilter
    {
        private Utility.TristateBool customBool1 = Utility.TristateBool.NotSet;
        private Utility.TristateBool customBool2 = Utility.TristateBool.NotSet;
        private Utility.TristateBool customBool3 = Utility.TristateBool.NotSet;

        public ProductItemFilter()
        {
            this.ItemType = "PigeonCms.Shop.ProductItem";
        }

        public string SKU
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

        public Utility.TristateBool InStock
        {
            [DebuggerStepThrough()]
            get { return customBool2; }
            [DebuggerStepThrough()]
            set { customBool2 = value; }
        }

        public Utility.TristateBool Enabled
        {
            [DebuggerStepThrough()]
            get { return base.Enabled; }
            [DebuggerStepThrough()]
            set { base.Enabled = value; }
        }

        public int ProductType
        {
            [DebuggerStepThrough()]
            get { return base.CustomInt3; }
            [DebuggerStepThrough()]
            set { base.CustomInt3 = value; }
        }

        public int AttributeSet
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
