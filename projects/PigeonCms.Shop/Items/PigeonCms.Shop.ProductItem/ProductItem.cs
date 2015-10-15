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

        List<ProductItem> threadItems = null;
        public List<ProductItem> ThreadItems
        {
            get
            {
                if (threadItems == null)
                {
                    var filter = new ProductItemFilter();
                    filter.ThreadId = this.Id;
                    filter.ShowOnlyRootItems = false;
                    threadItems = new ProductItemsManager().GetByFilter(filter, "");
                }
                return threadItems;
            }
        }

        bool? hasThreads = null;
        public bool? HasThreads
        {
            get
            {
                if (hasThreads == null)
                {
                    hasThreads = (this.ThreadItems.Count > 1);
                }
                return hasThreads;
            }
        }

        List<Attribute> attributes = null;
        public List<Attribute> Attributes
        {
            get
            {
                if (this.attributes == null)
                {
                    var man = new AttributeSetsManager();
                    var aman = new AttributesManager();
                    this.attributes = new List<Attribute>();
                    var set = man.GetByKey(this.AttributeSet);
                    foreach (int attributeId in set.AttributesList)
                    {
                        var a = aman.GetByKey(attributeId);
                        if (!a.AllowCustomValue)
                            this.attributes.Add(a);
                    }
                }
                return this.attributes;
            }
        }

        List<AttributeValue> attributeValues = null;
        public List<AttributeValue> AttributeValues
        {
            get
            {
                if (this.attributeValues == null)
                {
                    var man = new ItemAttributesValuesManager();
                    var vman = new AttributeValuesManager();
                    var items = man.GetByItemId(this.Id);
                    this.attributeValues = new List<AttributeValue>();
                    foreach (var item in items)
                    {
                        if (item.AttributeValueId > 0)
                        {
                            this.attributeValues.Add(vman.GetByKey(item.AttributeValueId));
                        }
                        
                    }
                }
                return this.attributeValues;
            }
        }

        List<Attribute> customAttributes = null;
        public List<Attribute> CustomAttributes
        {
            get
            {
                if (this.customAttributes == null)
                {
                    var man = new AttributeSetsManager();
                    var aman = new AttributesManager();
                    this.customAttributes = new List<Attribute>();
                    var set = man.GetByKey(this.AttributeSet);
                    foreach (int attributeId in set.AttributesList)
                    {
                        var a = aman.GetByKey(attributeId);
                        if (a.AllowCustomValue)
                            this.customAttributes.Add(a);
                    }
                }
                return this.customAttributes;
            }
        }

        List<string> customAttributeValues = null;
        public List<string> CustomAttributeValues
        {
            get
            {
                if (this.customAttributeValues == null)
                {
                    var man = new ItemAttributesValuesManager();
                    var items = man.GetByItemId(this.Id);
                    this.customAttributeValues = new List<string>();
                    foreach (var item in items)
                    {
                        if (item.AttributeValueId == 0)
                        {
                            this.customAttributeValues.Add(item.CustomValueString);
                        }

                    }
                }
                return this.customAttributeValues;
            }
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
        /// <summary>
        /// CheckUserContext=false
        /// WriteMode=false
        /// </summary>
        [DebuggerStepThrough()]
        public ProductItemsManager()
            : this(false, false)
        { }

        public ProductItemsManager(bool checkUserContext, bool writeMode)
            : base(checkUserContext, writeMode)
        { }

        public ProductItem GetBySku(string Sku)
        {
            var res = new ProductItem();
            if (!string.IsNullOrEmpty(Sku))
            {
                var filter = new ProductItemFilter();
                filter.SKU = Sku;
                var list = this.GetByFilter(filter, "");
                if (list.Count > 0)
                    res = list[0];
            }
            return res;
        }
    }

}
