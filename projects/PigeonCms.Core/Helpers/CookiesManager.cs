/***************************************************
PigeonCms - Open source Content Management System 
https://github.com/picce/pigeoncms
Copyright © 2016 Nicola Ridolfi - picce@yahoo.it
Licensed under the terms of "GNU General Public License v3"
For the full license text see license.txt or
visit "http://www.gnu.org/licenses/gpl.html"
***************************************************/


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Caching;
using System.Diagnostics;
using System.Collections;
using System.Configuration;

namespace PigeonCms.Core.Helpers
{
    /// <summary>
    /// cookies managment wrapper
    /// </summary>
    public class CookiesManager
    {
        string encryptionKey = "";

        private bool secure = false;
        public bool Secure
        {
            [DebuggerStepThrough()]
            get { return secure; }
        }

        private string cookieName = "";
        public string CookieName
        {
            [DebuggerStepThrough()]
            get { return cookieName; }
        }

		private int minutesToExpire = 0;
		public int MinutesToExpire
		{
			[DebuggerStepThrough()]
			get { return minutesToExpire; }
		}

		//public CookiesManager(string cookieName) : this(cookieName, false)
		//{
		//}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="cookieName">name of the cookie</param>
		/// <param name="secure">crypt or not the cookie content</param>
		/// <param name="minutesToExpire">cookie live in minutes. Default set to 7 days</param>
        public CookiesManager(string cookieName, bool secure = false, int minutesToExpire = 60 * 24 * 7)
        {
            this.cookieName = cookieName;
            this.secure = secure;
			this.minutesToExpire = minutesToExpire;

            if (ConfigurationManager.AppSettings["EncryptKey"] != null)
                encryptionKey = ConfigurationManager.AppSettings["EncryptKey"];
        }

        public string GetValue(int key)
        {
            return this.GetValue(key.ToString());
        }

        public string GetValue(string key)
        {
            return this.getValue(key, true);
        }

        public bool IsEmpty(int key)
        {
            return this.IsEmpty(key.ToString());
        }

        public bool IsEmpty(string key)
        {
            bool res = false;
            var cook = HttpContext.Current.Request.Cookies[this.CookieName];
            if (cook == null)
                res = true;
            else if (cook[key] == null)
                res = true;
            
            return res;
        }

        public void SetValue(int key, string value)
        {
            this.SetValue(key.ToString(), value);
        }

        public void SetValue(string key, string value)
        {
            if (value != null)
            {
                var cook = new HttpCookie(this.CookieName);
				cook.Expires = DateTime.Now.AddMinutes(this.MinutesToExpire);

                try
                {
                    cook.Values[key] = encrypt(value);
                    HttpContext.Current.Response.Cookies.Add(cook);
                    Tracer.Log("CookiesManager.SetValue: key=" + this.CookieName + "_" + key + "; Time=" + DateTime.Now, TracerItemType.Info);
                }
                catch (Exception ex)
                {
                    Tracer.Log("CookiesManager.SetValue: key=" + this.CookieName + "_" + key + "; "
                        + "Time=" + DateTime.Now + "; "
                        +" Err=" + ex.ToString() + "; ", 
                        TracerItemType.Info);
                }
            }
        }


        /// <summary>
        /// remove current cookie
        /// </summary>
        public void Clear()
        {
            HttpContext.Current.Response.Cookies[this.CookieName].Expires = DateTime.Now.AddDays(-1);
            HttpContext.Current.Response.Cookies.Remove(this.CookieName);
            Tracer.Log("CookiesManager.Clear: cookie=" + this.CookieName + "; Time=" + DateTime.Now, TracerItemType.Info);
        }


        private string getValue(string key, bool writeLog)
        {
            string res = "";
            var cook = HttpContext.Current.Request.Cookies[this.CookieName];

            try
            {
                if (cook != null)
                    res = decrypt(cook.Values[key], this.Secure);
            }
			//catch (FormatException)
			//{
			//	//try to read plain version --> 20160414 removed for security reason
			//	res = decrypt(cook[key], false); 
			//}
            catch (Exception ex)
            {
                Tracer.Log("CookiesManager.GetValue: key=" + this.CookieName + "_" + key + "; err=" + ex.ToString(),
                        TracerItemType.Error);
            }

            if (writeLog)
                Tracer.Log("CookiesManager.GetValue: key=" + this.CookieName + "_" + key + "; value=" + res,
                    TracerItemType.Info);

            return res;
        }

        private string encrypt(string value)
        {
            string res = value;

            if (this.Secure)
            {
                res = Utility.Encryption.Encrypt(res, encryptionKey);
            }

            return res;
        }

        private string decrypt(string value, bool isSecure)
        {
            string res = value;

            if (isSecure)
            {
                res = Utility.Encryption.Decrypt(res, encryptionKey);
            }

            return res;
        }
    }
}
