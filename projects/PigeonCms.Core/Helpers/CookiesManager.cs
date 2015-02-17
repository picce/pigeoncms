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

        public CookiesManager(string cookieName) : this(cookieName, false)
        {
        }

        public CookiesManager(string cookieName, bool secure)
        {
            this.cookieName = cookieName;
            this.secure = secure;

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
                //cook.Expires = 
                try
                {
                    cook[key] = encrypt(value);
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
        /// TODO
        /// remove cache entry with current key (and keyprefix)
        /// </summary>
        /// <param name="key">cookie entry key</param>
        //public void Remove(string key)
        //{
        //    this.remove(key, false);
        //}

        /// <summary>
        /// remove current cookie
        /// </summary>
        public void Clear()
        {
            HttpContext.Current.Response.Cookies[this.CookieName].Expires = DateTime.Now.AddDays(-1);
            HttpContext.Current.Response.Cookies.Remove(this.CookieName);
            Tracer.Log("CookiesManager.Clear: cookie=" + this.CookieName + "; Time=" + DateTime.Now, TracerItemType.Info);
        }

        /// <summary>
        /// todo
        /// </summary>
        /// <param name="key"></param>
        /// <param name="isFullKey"></param>
        private void remove(string key, bool isFullKey)
        {
            throw new NotImplementedException();

            string fullKey = "";
            if (!isFullKey)
                fullKey = this.CookieName + "_" + key;
            else
                fullKey = key;
            //HttpContext.Current.Response.Cookies.Remove(
            HttpContext.Current.Session.Remove(fullKey);
            Tracer.Log("CookiesManager.Remove: key=" + fullKey, TracerItemType.Info);
        }

        private string getValue(string key, bool writeLog)
        {
            string res = "";
            var cook = HttpContext.Current.Request.Cookies[this.CookieName];

            try
            {
                if (cook != null)
                    res = decrypt(cook[key], this.Secure);
            }
            catch (FormatException)
            {
                //try to read plain version
                res = decrypt(cook[key], false);
            }
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
