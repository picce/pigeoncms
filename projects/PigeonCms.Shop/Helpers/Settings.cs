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

        public string ItemWeightUnit
        {
            get { return AppSettingsManager.GetValue("PigeonCms.Shop_ItemWeightUnit", "Kg"); }
        }
    }
}
