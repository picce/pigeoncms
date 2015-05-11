using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;

namespace PigeonCms
{
    public class ItemAttributeValue: ITable
    {
        private int itemId;
	    private int attributeId;
	    private int attributeValueId;
        private string customValueString;

        private Dictionary<string, string> customValueTranslations = new Dictionary<string, string>();

        #region fields

        /// <summary>
        /// Item id, owner of Attribute
        /// </summary>
        [DataObjectField(true)]
        public int ItemId
        {
            get { return itemId; }
            set { itemId = value; }
        }

        /// <summary>
        /// Attribute id, define type of AttributeValue
        /// </summary>
        [DataObjectField(true)]
        public int AttributeId
        {
            get { return attributeId; }
            set { attributeId = value; }
        }

        /// <summary>
        /// Attribute Value Id, define the value of Attribute
        /// </summary>
        [DataObjectField(false)]
        public int AttributeValueId
        {
            get { return attributeValueId; }
            set { attributeValueId = value; }
        }

        /// <summary>
        /// CustomValue in Json format
        /// </summary>
        [DataObjectField(false)]
        public string CustomValueString
        {
            get { return customValueString; }
            set { customValueString = value; }
        }

        /// <summary>
        /// CustomValue in current culture
        /// </summary>
        [DataObjectField(false)]
        public string CustomValue
        {
            get
            {
                string res = "";
                CustomValueTranslations.TryGetValue(Thread.CurrentThread.CurrentCulture.Name, out res);
                if (Utility.IsEmptyFckField(res))
                    CustomValueTranslations.TryGetValue(Config.CultureDefault, out res);
                return res;
            }
        }

        /// <summary>
        /// CustomValue in different culture
        /// </summary>
        [DataObjectField(false)]
        public Dictionary<string, string> CustomValueTranslations
        {
            [DebuggerStepThrough()]
            get { return toDictionary(CustomValueString); }
            [DebuggerStepThrough()]
            set { customValueTranslations = value; }
        }

        /// <summary>
        /// Convert a json string into Dictionary<string, string>
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        private Dictionary<string, string> toDictionary(string json)
        {
            var serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            return serializer.Deserialize<Dictionary<string, string>>(json);
        }

        /// <summary>
        /// Convert a Dictionary<string,string> into Json string
        /// </summary>
        /// <param name="dictionary"></param>
        /// <returns></returns>
        private string toJson(Dictionary<string, string> dictionary)
        {
            var serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            return serializer.Serialize(dictionary);
        }

        #endregion

        #region methods

        public ItemAttributeValue() { }

        #endregion

    }


    /// <summary>
    /// Filter used in search
    /// </summary>
    /// <remarks></remarks>
    [Serializable]
    public class ItemAttributeValueFilter
    {
        #region fields definition

        private int itemId = 0;
        private int attributeId = 0;
        private int attributeValueId = 0;

        public int ItemId
        {
            [DebuggerStepThrough()]
            get { return itemId; }
            [DebuggerStepThrough()]
            set { itemId = value; }
        }

        public int AttributeId
        {
            [DebuggerStepThrough()]
            get { return attributeId; }
            [DebuggerStepThrough()]
            set { attributeId = value; }
        }

        public int AttributeValueId
        {
            [DebuggerStepThrough()]
            get { return attributeValueId; }
            [DebuggerStepThrough()]
            set { attributeValueId = value; }
        }

        #endregion

    }


}
