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
    public class Attribute : ITable, ITableWithOrdering
    {
        private int id = 0;
        private string name = "";
        private bool allowCustomValue;

        public int Ordering { get; set; }

        #region fields

        /// <summary>
        /// Automatic Id as PKey
        /// </summary>
        [DataObjectField(true)]
        public int Id { get; set; }

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


        public override bool Equals(System.Object obj)
        {
            // If parameter is null return false.
            if (obj == null)
            {
                return false;
            }
            // If parameter cannot be cast to Point return false.
            PigeonCms.Attribute p = obj as PigeonCms.Attribute;
            if ((System.Object)p == null)
            {
                return false;
            }
            // Return true if the fields match:
            return (this.Id.Equals(p.Id));
        }

        public override int GetHashCode()
        {
            return this.Id.GetHashCode();
        }


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
        private Utility.TristateBool allowCustomValue = Utility.TristateBool.NotSet;

        public int Id
        {
            [DebuggerStepThrough()]
            get { return id; }
            [DebuggerStepThrough()]
            set { id = value; }
        }

        public Utility.TristateBool AllowCustomValue
        {
            [DebuggerStepThrough()]
            get { return allowCustomValue; }
            [DebuggerStepThrough()]
            set { allowCustomValue = value; }
        }

        #endregion

    }

}
