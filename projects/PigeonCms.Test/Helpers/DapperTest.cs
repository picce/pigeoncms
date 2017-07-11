using PigeonCms.Geo;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using StackExchange.Redis;


namespace PigeonCms.UT.Core
{


    /// <summary>
    ///dapper integration test
    ///https://github.com/MicrosoftArchive/redis/releases (win server)
    ///https://stackexchange.github.io/StackExchange.Redis/ (c# client)
    
    ///</summary>
    [TestClass()]
    public class DapperTest
    {


        private TestContext testContextInstance;
        private ConnectionMultiplexer redis;

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
            if (redis == null)
            {
                redis = ConnectionMultiplexer.Connect("localhost:6379");
            }
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
            IDatabase db = redis.GetDatabase();

            db.StringSet(TEST_KEY, TEST_VALUE);

            string actual = db.StringGet(TEST_KEY);

            Assert.AreEqual(TEST_VALUE, actual);
        }

    }
}
