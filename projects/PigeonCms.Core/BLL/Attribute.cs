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
    class Attribute: ITable
    {
        private int id = 0;
        private string itemType = "";
        private string name = "";
        private int attributeType = 0;
        private bool allowCustomValue;

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
        public string ItemType
        {
            [DebuggerStepThrough()]
            get { return itemType; }
            [DebuggerStepThrough()]
            set { itemType = value; }
        }

        /// <summary>
        /// Name of attribute.
        /// </summary>
        [DataObjectField(false)]
        public string Name
        {
            get { return name; }
            set { name = value;  }
        }

        /// <summary>
        /// Type of Attribute.
        /// </summary>
        [DataObjectField(false)]
        public int AttributeType
        {
            get { return attributeType; }
            set { attributeType = value; }
        }

        /// <summary>
        /// Allow Custom Value, for value non in list.
        /// </summary>
        [DataObjectField(false)]
        public bool AllowCustomValue
        {
            get { return allowCustomValue; }
            set { allowCustomValue = value; }
        }

        #endregion

        #region methods

        public Attribute() { }

        #endregion

    }

    /// <summary>
    /// Filter used in search
    /// </summary>
    /// <remarks></remarks>
    [Serializable]
    public class AttributeFilter
    {
        #region fields definition

        private int id = 0;

        public int Id
        {
            [DebuggerStepThrough()]
            get { return id; }
            [DebuggerStepThrough()]
            set { id = value; }
        }

        #endregion

    }

}
