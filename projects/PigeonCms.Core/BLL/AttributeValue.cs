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
    public class AttributeValue : ITable
    {
        private int attributeId = 0;
        private string valueString = "";

        private Dictionary<string, string> valueTranslations = new Dictionary<string, string>();

        #region fields

        /// <summary>
        /// Automatic Id as PKey
        /// </summary>
        [DataObjectField(true)]
        public int Id { get; set; }

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
        /// Value in json format
        /// </summary>
        [DataObjectField(false)]
        public string ValueString
        {
            get { return valueString; }
            set { valueString = toJson(ValueTranslations); }
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
                ValueTranslations.TryGetValue(Thread.CurrentThread.CurrentCulture.Name, out res);
                if (Utility.IsEmptyFckField(res))
                    ValueTranslations.TryGetValue(Config.CultureDefault, out res);
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
            get { return toDictionary(ValueString); }
            [DebuggerStepThrough()]
            set { valueTranslations = value; }
        }

        public bool IsValueTranslated
        {
            get
            {
                bool res = true;
                string val = "";
                ValueTranslations.TryGetValue(Thread.CurrentThread.CurrentCulture.Name, out val);
                if (Utility.IsEmptyFckField(val))
                    res = false;
                return res;
            }
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

        public AttributeValue() { }

        #endregion

    }

    /// <summary>
    /// Filter used in search
    /// </summary>
    /// <remarks></remarks>
    [Serializable]
    public class AttributeValueFilter
    {
        #region fields definition

        private int id = 0;
        private int attributeId = 0;

        public int Id
        {
            [DebuggerStepThrough()]
            get { return id; }
            [DebuggerStepThrough()]
            set { id = value; }
        }

        public int AttributeId
        {
            [DebuggerStepThrough()]
            get { return attributeId; }
            [DebuggerStepThrough()]
            set { attributeId = value; }
        }

        #endregion

    }
}
