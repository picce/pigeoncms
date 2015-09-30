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
    public class ApplicationManager<T>
    {
        private string keyPrefix = "";
        public string KeyPrefix
        {
            [DebuggerStepThrough()]
            get { return keyPrefix; }
        }

        public ApplicationManager(string keyPrefix)
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
            if (HttpContext.Current.Application[this.KeyPrefix + "_" + key] == null)
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
                HttpContext.Current.Application[this.KeyPrefix + "_" + key] = obj;
                Tracer.Log("ApplicationManager.Insert: key=" + this.KeyPrefix + "_" + key + "; Time=" + DateTime.Now, TracerItemType.Info);
            }
        }

        /// <summary>
        /// remove Application entry with current key (and keyprefix)
        /// </summary>
        /// <param name="key">cache entry key</param>
        public void Remove(string key)
        {
            this.remove(key, false);
        }

        /// <summary>
        /// remove all Application vars with current keyprefix
        /// </summary>
        public void Clear()
        {
            foreach (string key in HttpContext.Current.Application.AllKeys)
            {
                if (key.StartsWith(this.KeyPrefix + "_"))
                {
                    this.remove(key, true);
                }
            }
            Tracer.Log("ApplicationManager.Clear: key=" + this.KeyPrefix + "; Time=" + DateTime.Now, TracerItemType.Info);
        }

        private void remove(string key, bool isFullKey)
        {
            string fullKey = "";
            if (!isFullKey)
                fullKey = this.KeyPrefix + "_" + key;
            else
                fullKey = key;
            HttpContext.Current.Application.Remove(fullKey);
            Tracer.Log("ApplicationManager.Remove: key=" + fullKey, TracerItemType.Info);
        }

        private T getValue(string key, bool writeLog)
        {
            if (writeLog)
                Tracer.Log("ApplicationManager.GetValue: key=" + this.KeyPrefix + "_" + key, TracerItemType.Info);
            var res = (T)HttpContext.Current.Application[this.KeyPrefix + "_" + key];
            return res;
        }
    }
}
