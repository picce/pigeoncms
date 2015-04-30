using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.DirectoryServices;
using System.Linq;
using System.Text;
using System.Threading;

namespace PigeonCms
{
    class AttributeValue : ITableWithOrdering
    {
        const string DefaultItemType = "PigeonCms.AttributeValue";
        private int id = 0;
        private int attributeId = 0;
        private string itemType = "";
        //private string value = "";

        private Dictionary<string, string> valueTranslations = new Dictionary<string, string>();

        #region fields

        /// <summary>
        /// Automatic Id as PKey
        /// </summary>
        [DataObjectField(true)]
        public int Id { get; set; }

        /// <summary>
        /// Item specific type name. Ex. PigeonCms.CustomItem
        /// </summary>
        [DataObjectField(false)]
        public string ItemTypeName
        {
            [DebuggerStepThrough()]
            get
            {
                if (!string.IsNullOrEmpty(itemType))
                    return itemType;
                else
                    return DefaultItemType;
            }
            [DebuggerStepThrough()]
            set { itemType = value; }
        }

        /// <summary>
        /// AttributeId which value is related.
        /// </summary>
        [DataObjectField(false)]
        public int AttributeId
        {
            [DebuggerStepThrough()]
            get { return attributeId; }
            set { attributeId = value; }
        }

        /// <summary>
        /// Value in current culture
        /// </summary>
        [DataObjectField(false)]
        public string Value
        {
            get
            {
                string res = "";
                valueTranslations.TryGetValue(Thread.CurrentThread.CurrentCulture.Name, out res);
                if (Utility.IsEmptyFckField(res))
                    valueTranslations.TryGetValue(Config.CultureDefault, out res);
                return res;
            }
        }

        /// <summary>
        /// Value in different culture
        /// </summary>
        [DataObjectField(false)]
        public Dictionary<string, string> ValueTranslations
        {
            [DebuggerStepThrough()]
            get { return valueTranslations; }
            [DebuggerStepThrough()]
            set { valueTranslations = value; }
        }

        public bool IsValueTranslated
        {
            get
            {
                bool res = true;
                string val = "";
                valueTranslations.TryGetValue(Thread.CurrentThread.CurrentCulture.Name, out val);
                if (Utility.IsEmptyFckField(val))
                    res = false;
                return res;
            }
        }

        #endregion

        #region methods

        public AttributeValue() { }

        #endregion

        public class ItemComparer : IComparer<Item>
        {
            private string sortExpression = "";
            private SortDirection sortDirection;


            public ItemComparer(string sortExpression, SortDirection sortDirection)
            {
                this.sortExpression = sortExpression;
                this.sortDirection = sortDirection;
            }

            public int Compare(Item lhs, Item rhs)
            {
                if (this.sortDirection == SortDirection.Descending)
                    return rhs.CompareTo(lhs, sortExpression);
                else
                    return lhs.CompareTo(rhs, sortExpression);
            }

            public bool Equals(Item lhs, Item rhs)
            {
                return this.Compare(lhs, rhs) == 0;
            }

            public int GetHashCode(Item e)
            {
                return e.GetHashCode();
            }
        }
    }
}
