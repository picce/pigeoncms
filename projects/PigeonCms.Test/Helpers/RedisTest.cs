using PigeonCms.Geo;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using StackExchange.Redis;
using System.Threading.Tasks;
using PigeonCms.Core.Helpers;

namespace PigeonCms.UT.Core
{


    /// <summary>
    ///dapper integration test
    ///https://github.com/MicrosoftArchive/redis/releases (win server)
    ///https://stackexchange.github.io/StackExchange.Redis/ (c# client)
    
    ///</summary>
    [TestClass()]
    public class RedisTest
    {

        private TestContext testContextInstance;
        private IDatabase redis;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        [ClassInitialize()]
        public static void MyClassInitialize(TestContext testContext)
        {

        }

        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        

        //Use TestInitialize to run code before running each test
        [TestInitialize()]
        public void MyTestInitialize()
        {
            redis = RedisStore.RedisCache;
        }

        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion

        const string TEST_KEY = "pigeoncms.test.key1";
        const string TEST_VALUE = "hello this is a test value";


        /// <summary>
        ///test for StringSet and StringGet methods
        ///</summary>
        [TestMethod()]
        public void WriteRead()
        {
            redis.StringSet(TEST_KEY, TEST_VALUE);
            string actual = redis.StringGet(TEST_KEY);

            Assert.AreEqual(TEST_VALUE, actual);
        }

        [TestMethod()]
        public void CheckDelete()
        {
            redis.StringSet(TEST_KEY, TEST_VALUE);
            redis.KeyDelete(TEST_KEY);
            Assert.IsFalse(redis.KeyExists(TEST_KEY));

            string actual = redis.StringGet(TEST_KEY);
            Assert.IsNull(actual);
        }

        [TestMethod()]
        public void CheckExpiration()
        {
            redis.StringSet(TEST_KEY, TEST_VALUE, new TimeSpan(0, 0, 1), When.Always);
            Assert.IsTrue(redis.KeyExists(TEST_KEY));

            Task.Delay(1100).Wait();
            Assert.IsFalse(redis.KeyExists(TEST_KEY), "key is not expired successfully");

        }


        [TestMethod()]
        public void RedisHashCheck()
        {
            var hashKey = "hashKey";

            HashEntry[] redisBookHash = {
                new HashEntry("title", "My awesome redis"),
                new HashEntry("year", 1980),
                new HashEntry("author", "Pigeonne")
            };

            redis.HashSet(hashKey, redisBookHash);

            if (redis.HashExists(hashKey, "year"))
            {
                var year = redis.HashGet(hashKey, "year");
                Assert.IsTrue(year == 1980);
            }

            var allHash = redis.HashGetAll(hashKey);

            //get all the items
            foreach (var item in allHash)
            {
                //output 
                //key: title, value: Redis for .NET Developers
                //key: year, value: 2016
                //key: author, value: Taswar Bhatti
                if (item.Name == "title")
                    Assert.IsTrue("My awesome redis" == item.Value);
                else if (item.Name == "year")
                    Assert.IsTrue(1980 == item.Value);
                else if (item.Name == "author")
                    Assert.IsTrue("Pigeonne" == item.Value);
            }


            var len = redis.HashLength(hashKey);  //result of len is 3
            Assert.IsTrue(len == 3);

            if (redis.HashExists(hashKey, "year"))
            {
                var year = redis.HashIncrement(hashKey, "year", 1);
                var year2 = redis.HashDecrement(hashKey, "year", 1);

                Assert.IsTrue(year == 1981);
                Assert.IsTrue(year2 == 1980);
            }

        }
    }

}
