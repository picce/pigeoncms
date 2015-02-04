using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Caching;
using System.Diagnostics;
using System.Collections;

namespace PigeonCms.Core.Helpers
{
    public class SessionManager<T>
    {
        private string keyPrefix = "";
        public string KeyPrefix
        {
            [DebuggerStepThrough()]
            get { return keyPrefix; }
        }

        public SessionManager(string keyPrefix)
        {
            this.keyPrefix = keyPrefix;
        }

        public T GetValue(int key)
        {
            return this.GetValue(key.ToString());
        }

        public T GetValue(string key)
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
            if (HttpContext.Current.Session[this.KeyPrefix + "_" + key] == null)
                res = true;
            return res;
        }

        public void Insert(int key, T obj)
        {
            this.Insert(key.ToString(), obj);
        }

        public void Insert(string key, T obj)
        {
            if (obj != null)
            {
                HttpContext.Current.Session[this.KeyPrefix + "_" + key] = obj;
                Tracer.Log("SessionManager.Insert: key=" + this.KeyPrefix + "_" + key + "; Time=" + DateTime.Now, TracerItemType.Info);
            }
        }

        /// <summary>
        /// remove cache entry with current key (and keyprefix)
        /// </summary>
        /// <param name="key">cache entry key</param>
        public void Remove(string key)
        {
            this.remove(key, false);
        }

        /// <summary>
        /// remove all cache with current keyprefix
        /// </summary>
        public void Clear()
        {
            foreach (DictionaryEntry d in HttpContext.Current.Session)
            {
                if (d.Key.ToString().StartsWith(this.KeyPrefix + "_"))
                {
                    this.remove(d.Key.ToString(), true);
                }
            }
            Tracer.Log("SessionManager.Clear: key=" + this.KeyPrefix + "; Time=" + DateTime.Now, TracerItemType.Info);
        }

        private void remove(string key, bool isFullKey)
        {
            string fullKey = "";
            if (!isFullKey)
                fullKey = this.KeyPrefix + "_" + key;
            else
                fullKey = key;
            HttpContext.Current.Session.Remove(fullKey);
            Tracer.Log("SessionManager.Remove: key=" + fullKey, TracerItemType.Info);
        }

        private T getValue(string key, bool writeLog)
        {
            if (writeLog)
                Tracer.Log("SessionManager.GetValue: key=" + this.KeyPrefix + "_" + key, TracerItemType.Info);
            var res = (T)HttpContext.Current.Session[this.KeyPrefix + "_" + key];
            return res;
        }
    }
}
