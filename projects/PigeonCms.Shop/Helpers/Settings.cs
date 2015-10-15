using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using PigeonCms;


namespace PigeonCms.Shop
{
    public class Settings
    {
        private PigeonCms.AppSettingsProvider provider = new AppSettingsProvider("PigeonCms.Shop");

        /// <summary>
        /// section linked to shop data (categories and items)
        /// -1 if not set
        /// </summary>
        public int SectionId
        {
            get
            {
                string value = provider.GetValue("SectionId", "0");
                int res = 0;
                int.TryParse(value, out res);
                if (res == 0)
                    res = -1;

                return res;
            }
        }

        /// <summary>
        /// category linked for email templates
        /// -1 if not set
        /// </summary>
        public int EmailTemplatesCatId
        {
            get
            {
                string value = provider.GetValue("EmailTemplatesCatId", "0");
                int res = 0;
                int.TryParse(value, out res);
                if (res == 0)
                    res = -1;

                return res;
            }
        }

        Currency currencyDefault = null;
        /// <summary>
        /// shop default currency
        /// </summary>
        public Currency CurrencyDefault
        {
            get
            {
                if (currencyDefault == null)
                {
                    string value = provider.GetValue("CurrencyDefault");
                    currencyDefault = new Currency(value);
                }
                return currencyDefault;
            }
        }

        /// <summary>
        /// product weight unit of measure
        /// </summary>
        public string WeightUnit
        {
            get
            {
                return provider.GetValue("WeightUnit", "Kg");
            }
        }

        [Obsolete("Use CurrencyDefault instead")]
        public string ShopCurrency
        {
            get
            {
                string res = "";
                try
                {
                    res = ConfigurationManager.AppSettings["ShopCurrency"].ToString();
                }
                catch { }
                return res;
            }
        }
    }
}
