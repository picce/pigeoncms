using PigeonCms.Geo;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace PigeonCms.Test
{
    
    
    /// <summary>
    ///This is a test class for CountriesManagerTest and is intended
    ///to contain all CountriesManagerTest Unit Tests
    ///</summary>
    [TestClass()]
    public class CountriesManagerTest
    {


        private TestContext testContextInstance;

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
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion

        const string TEST_CODE = "TT";


        /// <summary>
        ///A test for GetByFilter
        ///</summary>
        [TestMethod()]
        public void GetByFilterTest()
        {
            CountriesManager target = new CountriesManager(); // TODO: Initialize to an appropriate value
            CountriesFilter filter = new CountriesFilter(); ; // TODO: Initialize to an appropriate value
            filter.Code = TEST_CODE;
            List<Country> actual;

            actual = target.GetByFilter(filter, "");
            Assert.IsTrue(actual[0].Code == TEST_CODE);
            //Assert.AreEqual(expected, actual);
            //Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for GetByKey
        ///</summary>
        [TestMethod()]
        public void GetByKeyTest()
        {
            CountriesManager target = new CountriesManager(); 
            //Country expected = null; // TODO: Initialize to an appropriate value
            Country actual;
            actual = target.GetByKey(TEST_CODE);
            Assert.IsTrue(actual.Code == TEST_CODE);
        }

        /// <summary>
        ///A test for GetList
        ///</summary>
        [TestMethod()]
        public void GetListTest()
        {
            CountriesManager target = new CountriesManager(); // TODO: Initialize to an appropriate value
            Dictionary<string, string> actual;
            actual = target.GetList();
            //Assert.AreEqual(expected, actual);
            Assert.IsTrue(actual.Count > 0, "no countries in table");
        }

        [TestMethod()]
        public void DeleteTest()
        {
            CountriesManager target = new CountriesManager();
            int actual = target.DeleteById(TEST_CODE);
            Assert.IsTrue(actual > 0);
        }

        /// <summary>
        ///A test for Insert
        ///</summary>
        [TestMethod()]
        public void DeleteInsertTest()
        {
            CountriesManager target = new CountriesManager();
            Country theObj = new Country(); ;
            Country actual;
            theObj.Code = TEST_CODE;
            theObj.Iso3 = "TES";
            actual = target.Insert(theObj);
            Assert.AreEqual(theObj.Code, actual.Code);
        }

        /// <summary>
        ///A test for Update
        ///</summary>
        [TestMethod()]
        public void UpdateTest()
        {
            CountriesManager target = new CountriesManager(); // TODO: Initialize to an appropriate value
            Country theObj = new Country(); ;
            int actual;
            theObj = target.GetByKey(TEST_CODE);
            theObj.Iso3 = "TTT";
            actual = target.Update(theObj);
            Assert.IsTrue(actual > 0);
        }
    }
}
