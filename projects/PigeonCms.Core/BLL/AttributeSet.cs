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
    public class AttributeSet : ITable
    {
        private int id = 0;
        private string name = "";
        private string stringList = "";
        private List<int> attributesList = new List<int>(); 

        #region fields

        /// <summary>
        /// Automatic Id as PKey
        /// </summary>
        [DataObjectField(true)]
        public int Id { get; set; }

        /// <summary>
        /// Name of attribute.
        /// </summary>
        /// 
        [DataObjectField(false)]
        public string Name
        {
            [DebuggerStepThrough()]
            get { return name; }
            [DebuggerStepThrough()]
            set { name = value;}
        }

        /// <summary>
        /// List of selected attributes.
        /// </summary>
        [DataObjectField(false)]
        public List<int> AttributesList
        {
            get 
            {
                if (!string.IsNullOrEmpty(AttributesString))
                {
                    attributesList = AttributesString.Split(',').Select(Int32.Parse).ToList();
                }
                return attributesList;
            }
            set
            {
                if (value.Count > 0)
                {
                    AttributesString = string.Join(", ", value.Select(id => id.ToString()).ToArray());
                }
            }
        }

        /// <summary>
        /// List of selected attributes.
        /// </summary>
        [DataObjectField(false)]
        public string AttributesString
        {
            get { return stringList;}
            set { stringList = value;}
        }

        #endregion

        #region methods

        public AttributeSet() { }

        #endregion

    }

    /// <summary>
    /// Filter used in search
    /// </summary>
    /// <remarks></remarks>
    [Serializable]
    public class AttributeSetFilter
    {
        #region fields definition

        private int id = 0;
        //private string stringList = "";
        //private List<int> attributesList = new List<int>(); 

        public int Id
        {
            [DebuggerStepThrough()]
            get { return id; }
            [DebuggerStepThrough()]
            set { id = value; }
        }

        //public int AttributeType
        //{
        //    [DebuggerStepThrough()]
        //    get { return attributeType; }
        //    [DebuggerStepThrough()]
        //    set { attributeType = value; }
        //}

        #endregion

    }

}
