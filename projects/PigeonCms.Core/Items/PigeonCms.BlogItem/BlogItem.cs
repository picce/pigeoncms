using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PigeonCms;
using System.Data.Common;
using System.ComponentModel;
using System.Threading;
using System.Diagnostics;
using System.Web.Script.Serialization;
//using Newtonsoft.Json.Linq;


namespace PigeonCms
{
    /// <summary>
    /// generic blog post item
    /// </summary>
    public class BlogItem: PigeonCms.Item
    {
        public class StoreAddress
        {
            public string Address = "";
            public string City = "";
            public string State = "";
            public string ZipCode = "";
            public string NationCode = "";
            public decimal Latitude = 0;
            public decimal Longitude = 0;


            public StoreAddress(string jsonAddress = "")
            {
                if (!string.IsNullOrEmpty(jsonAddress))
                {
                    try
                    {
                        JObject jsData = JObject.Parse(jsonAddress);


                        this.Address = parseString("Address", jsData);
                        this.City = parseString("City", jsData);
                        this.State = parseString("State", jsData);
                        this.ZipCode = parseString("ZipCode", jsData);
                        this.NationCode = parseString("NationCode", jsData);
                        this.Latitude = parseDecimal("Latitude", jsData);
                        this.Longitude = parseDecimal("Longitude", jsData);
                    }
                    catch (Exception)
                    {
                        this.Address = jsonAddress;
                        PigeonCms.Tracer.Log("Invalid Json Address parse [" + this.ToString() + "]");
                    }
                }
            }

            public string ToJson()
            {
                string res = new JavaScriptSerializer().Serialize(this);
                return res;
            }

            public override string ToString()
            {
                string res = this.Address + ", " 
                    + this.City + ", "
                    + this.State;
                return res;
            }

            private string parseString(string fieldName, JObject jsData)
            {
                string res = "";
                try
                {
                    res = (string)jsData[fieldName];
                }
                catch (Exception)
                {
                    PigeonCms.Tracer.Log("Invalid Json "+ fieldName +" field [" + this.ToString() + "]");
                }
                return res;
            }

            private decimal parseDecimal(string fieldName, JObject jsData)
            {
                decimal res = 0;
                try
                {
                    res = (decimal)jsData[fieldName];
                }
                catch (Exception)
                {
                    PigeonCms.Tracer.Log("Invalid Json " + fieldName + " field [" + this.ToString() + "]");
                }
                return res;
            }

        }

        public BlogItem()
        {
            this.ItemTypeName = "PigeonCms.BlogItem";
        }

        public StoreAddress Address
        {
            get
            {
                return new StoreAddress(this.AddressString);
            }
        }

        [ItemFieldMapAttribute(ItemFieldMapAttribute.CustomFields.CustomString1)]
        public string AddressString
        {
            [DebuggerStepThrough()]
            get { return base.CustomString1; }
            [DebuggerStepThrough()]
            set { this.CustomString1 = value; }
        }

        public string Phone
        {
            get { return GetParamValue("Phone"); }
            set { SetParamValue("Phone", value); }

        }

        public string Fax
        {
            get { return GetParamValue("Fax"); }
            set { SetParamValue("Fax", value); }

        }

        public string Email
        {
            get { return GetParamValue("Email"); }
            set { SetParamValue("Email", value); }

        }

        public string Website
        {
            get { return GetParamValue("Website"); }
            set { SetParamValue("Website", value); }

        }

        protected string GetParamValue(string paramKey)
        {
            string res = "";
            if (base.Params.ContainsKey(paramKey))
                res = base.Params[paramKey];
            return res;
        }

        protected void SetParamValue(string paramKey, string paramValue)
        {
            //TODO
        }

    }


    [Serializable]
    public class StoreItemsFilter: PigeonCms.ItemsFilter
    {
        public StoreItemsFilter()
        {
            this.ItemType = "Aquest.StoreItem";
        }

    }


    public class StoreItemsManager: PigeonCms.ItemsManager<StoreItem, StoreItemsFilter>
    {
        public StoreItemsManager(bool checkUserContext, bool writeMode)
            : base(checkUserContext, writeMode)
        { }

    }

}
