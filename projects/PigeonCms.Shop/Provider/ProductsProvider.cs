using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PigeonCms;
using PigeonCms.Core.Helpers;

namespace PigeonCms.Shop.ProductsProvider
{
    public class CurrentProduct<P, PM, PF>
        where P : PigeonCms.Shop.ProductItem, new()
        where PM : PigeonCms.Shop.ProductItemsManager, new()
        where PF : PigeonCms.Shop.ProductItemFilter
    {

        private PM man = new PM();

        private int productId = 0;
        public int ProductId
        {
            get { return this.productId; }
        }

        private P product = default(P);
        public P Product
        {
            get
            {
                if (this.ProductId <= 0)
                    product = new P();

                if (product == null || product.Id == 0)
                {
                    product = new P();
                    if (this.ProductId > 0)
                    {
                        product = (P)man.GetByKey(this.ProductId);
                    }
                }
                return product;
            }
        }

        public CurrentProduct(int productId)
        {
            this.productId = productId;
        }

        public List<AttributeValue> GetDefaultAttribute()
        {
            var defaultAttribute = new List<AttributeValue>();
            int defaultAttributeId = 0;

            // default is first of set.
            if(this.Product.Attributes.Count > 0)
                defaultAttributeId = this.Product.Attributes[0].Id;

            foreach (var t in this.Product.ThreadItems)
            {
                // find the default
                AttributeValue d = t.AttributeValues.Find(x => x.AttributeId == defaultAttributeId);
                if (!defaultAttribute.Exists(x => x.Id == d.Id))
                    defaultAttribute.Add(d);
            }
            return defaultAttribute;
        }

        public List<AttributeValue> GetNextAttribute(List<int> attributeValuesId)
        {
            var nextAttribute = new List<AttributeValue>();
            var currents = new List<AttributeValue>();
            var man = new AttributeValuesManager();

            foreach (int attributeValueId in attributeValuesId)
            {
                var current = man.GetByKey(attributeValueId);
                currents.Add(current);
            }
            
            foreach (var t in this.Product.ThreadItems)
            {
                bool notFound = false;
                int i = 0;
                for (; i < currents.Count; i++)
                {
                    //&& t.AttributeValues.IndexOf(currents[i]) + 1 < t.AttributeValues.Count to check?
                    if (!t.AttributeValues.Contains(currents[i]))
                    {
                        notFound = true;
                        break;
                    }
                }

                if (!notFound)
                {
                    AttributeValue next = t.AttributeValues[i];
                    nextAttribute.Add(next);
                }

            }

            return nextAttribute;

        }

        public P GetProductByAttributeValues(List<int> attributeValuesId)
        {
            foreach (var t in this.Product.ThreadItems)
            {
                var notFound = false;
                for (int i = 0; i < t.AttributeValues.Count; i++)
                {
                    if (t.AttributeValues[i].Id != attributeValuesId[i])
                    {
                        notFound = true;
                        break;
                    }
                }
                if (!notFound)
                    return (P)t;
            }

            return null;
        }


    }//class

}//ns
