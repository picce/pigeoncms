using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PigeonCms;


namespace Acme
{
	/// <summary>
	/// settings for ACME sample project
	/// </summary>
	public class Settings
	{
        private PigeonCms.AppSettingsProvider provider = new AppSettingsProvider("Acme.Site");


        /// <summary>
        /// Section that contains website contents (categories and items)
        /// -1 if not set
        /// </summary>
        public int ContentsSectionId
        {
            get
            {
                string value = provider.GetValue("ContentsSectionId", "0");
                int res = 0;
                int.TryParse(value, out res);
                if (res == 0)
                    res = -1;

                return res;
            }
        }

        /// <summary>
        /// Static pages category
        /// -1 if not set
        /// </summary>
        public int StaticPagesCatId
        {
            get
            {
                string value = provider.GetValue("StaticPagesCatId", "0");
                int res = 0;
                int.TryParse(value, out res);
                if (res == 0)
                    res = -1;

                return res;
            }
        }

        /// <summary>
        /// News category
        /// -1 if not set
        /// </summary>
        public int NewsCatId
        {
            get
            {
                string value = provider.GetValue("NewsCatId", "0");
                int res = 0;
                int.TryParse(value, out res);
                if (res == 0)
                    res = -1;

                return res;
            }
        }

        /// <summary>
        /// Blog category
        /// -1 if not set
        /// </summary>
        public int BlogCatId
        {
            get
            {
                string value = provider.GetValue("BlogCatId", "0");
                int res = 0;
                int.TryParse(value, out res);
                if (res == 0)
                    res = -1;

                return res;
            }
        }

    }
}