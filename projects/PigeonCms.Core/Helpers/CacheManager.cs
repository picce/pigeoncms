/***************************************************
PigeonCms - Open source Content Management System 
https://github.com/picce/pigeoncms
Copyright © 2015 Nicola Ridolfi - picce@yahoo.it
version: 2.0.0
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

namespace PigeonCms.Core.Helpers
{
    public static class CacheManager
    {
        public static bool AppUseCache
        {
            get
            {
                bool res = false;
                bool.TryParse(AppSettingsManager.GetValue("UseCache"), out res);
                return res;
            }
        }
    }

    public class CacheManager<T>
    {
        private bool checkUserContext = false;
        public bool CheckUserContext
        {
            get { return checkUserContext; }
        }

        private string keyPrefix = "";
        public string KeyPrefix
        {
            [DebuggerStepThrough()]
            get { return keyPrefix; }
        }

        private int cacheduration;
        public int CacheDuration
        {
            [DebuggerStepThrough()]
            get { return cacheduration; }
        }

        public CacheManager(string keyPrefix) 
            : this(keyPrefix, Config.DefaultCacheValue, false)
        { }

        public CacheManager(string keyPrefix, bool checkUserContext)
            : this(keyPrefix, Config.DefaultCacheValue, checkUserContext)
        { }

        public CacheManager(string keyPrefix, int duration)
            : this(keyPrefix, duration, false)
        { }

        public CacheManager(string keyPrefix, int duration, bool checkUserContext)
        {
            this.keyPrefix = keyPrefix;
            this.cacheduration = duration;
            this.checkUserContext = checkUserContext;
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
            if (this.getValue(key, false) == null)
                res = true;
            return res;
        }

        public void Insert(int key, T obj)
        {
            this.Insert(key.ToString(), obj);
        }

        public void Insert(string key, T obj)
        {
            this.Insert(key, obj, System.Web.Caching.CacheItemPriority.Normal);
        }

        public void Insert(string key, T obj, System.Web.Caching.CacheItemPriority priority)
        {
            if (obj != null)
            {
                DateTime expiration = DateTime.Now.AddMinutes(this.CacheDuration);
                HttpContext.Current.Cache.Add(this.KeyPrefix + "_" + key, obj, null, expiration, TimeSpan.Zero, priority, null);
                Tracer.Log("CacheManager.Insert: key=" + this.KeyPrefix + "_" + key + "; Time=" + DateTime.Now, TracerItemType.Info);
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
            foreach (DictionaryEntry d in HttpContext.Current.Cache)
            {
                if (d.Key.ToString().StartsWith(this.KeyPrefix + "_"))
                {
                    this.remove(d.Key.ToString(), true);
                }
            }
            Tracer.Log("CacheManager.Clear: key=" + this.KeyPrefix + "; Time=" + DateTime.Now, TracerItemType.Info);
        }

        private void remove(string key, bool isFullKey)
        {
            string fullKey = "";
            if (!isFullKey)
                fullKey = this.KeyPrefix + "_" + key;
            else
                fullKey = key;
            HttpContext.Current.Cache.Remove(fullKey);
            Tracer.Log("CacheManager.Remove: key=" + fullKey, TracerItemType.Info);
        }

        private T getValue(string key, bool writeLog)
        {
            if (writeLog)
                Tracer.Log("CacheManager.GetValue: key=" + this.KeyPrefix + "_" + key, TracerItemType.Info);
            var res = (T)HttpContext.Current.Cache[this.KeyPrefix + "_" + key];
            if (this.CheckUserContext && res != null)
            {
                if (typeof(ITableWithPermissions).IsAssignableFrom(res.GetType()))
                {
                    if (new PermissionProvider().IsItemNotAllowed((ITableWithPermissions)res))
                        res = default(T);
                }
            }
            return res;
        }
    }
}
