/***************************************************
PigeonCms - Open source Content Management System 
https://github.com/picce/pigeoncms
Copyright © 2017 Nicola Ridolfi - picce@yahoo.it
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
using StackExchange.Redis;
using System.Configuration;

namespace PigeonCms.Core.Helpers
{
    public class RedisStore
    {
        private static readonly Lazy<ConnectionMultiplexer> lazyConnection;

        static RedisStore()
        {
            string redisConnection = Config.RedisConnection;

            //var configurationOptions = new ConfigurationOptions
            //{
            //    EndPoints = { redisConnection }
            //};

            //lazyConnection = new Lazy<ConnectionMultiplexer>(() => ConnectionMultiplexer.Connect(configurationOptions));
            lazyConnection = new Lazy<ConnectionMultiplexer>(() => ConnectionMultiplexer.Connect(redisConnection));
        }

        public static ConnectionMultiplexer Connection => lazyConnection.Value;

        public static IDatabase RedisCache => Connection.GetDatabase();

    }

    public class RedisProvider
    {
        private string appName = null;
        public string AppName
        {
            get
            {
                if (appName == null)
                {
                    appName = Config.RedisAppName;
                }
                return appName;
            }
        }

        public TimeSpan DefaultExpire { get; set; }


        public string KeyPrefix { get; }

        public RedisProvider(string keyPrefix, TimeSpan? defaultExpire = null)
        {
            this.KeyPrefix = keyPrefix;

            if (defaultExpire.HasValue)
                this.DefaultExpire = defaultExpire.Value;
            else
                this.DefaultExpire = new TimeSpan(0, 0, Config.RedisDefaultExpireSeconds);
        }

        /// <summary>
        /// generate complex key with format: AppName.KeyPrefix:key[field]#id
        /// </summary>
        /// <param name="key"></param>
        /// <param name="field"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public RedisKey K(string key, string field = "", string id = "")
        {
            string res = $"{this.AppName}.{this.KeyPrefix}:{key}";

            if (!string.IsNullOrEmpty(field))
                res += $"[{field}]";

            if (!string.IsNullOrEmpty(id))
                res += $"#{id}";

            return res;
        }

        public TimeSpan Exp(TimeSpan? t = null)
        {
            TimeSpan res = this.DefaultExpire;

            if (t.HasValue)
                res = t.Value;

            return res;
        }
    }

}
