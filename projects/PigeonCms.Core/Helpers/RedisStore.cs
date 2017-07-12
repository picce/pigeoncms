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
            string redisConnection = ConfigurationManager.AppSettings["RedisConnection"].ToString();

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

}
