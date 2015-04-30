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
    class Attribute: ITableWithOrdering
    {
        const string DefaultItemType = "PigeonCms.Attribute";
        private int id = 0;
        private string itemType = "";
        private int attributeType = 0;
        private bool allowCustomValue;

        private Dictionary<string, string> nameTranslations = new Dictionary<string, string>();

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
        /// Name in current culture
        /// </summary>
        [DataObjectField(false)]
        public string Name
        {
            get
            {
                string res = "";
                nameTranslations.TryGetValue(Thread.CurrentThread.CurrentCulture.Name, out res);
                if (Utility.IsEmptyFckField(res))
                    nameTranslations.TryGetValue(Config.CultureDefault, out res);
                return res;
            }
        }

        /// <summary>
        /// Name in different culture
        /// </summary>
        [DataObjectField(false)]
        public Dictionary<string, string> NameTranslations
        {
            [DebuggerStepThrough()]
            get { return nameTranslations; }
            [DebuggerStepThrough()]
            set { nameTranslations = value; }
        }

        public bool IsNameTranslated
        {
            get
            {
                bool res = true;
                string val = "";
                nameTranslations.TryGetValue(Thread.CurrentThread.CurrentCulture.Name, out val);
                if (Utility.IsEmptyFckField(val))
                    res = false;
                return res;
            }
        }

        #endregion

        #region methods

        public Attribute() { }

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
