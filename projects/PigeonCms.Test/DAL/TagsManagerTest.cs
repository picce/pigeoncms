using PigeonCms.Geo;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace PigeonCms.UT.Core
{


    /// <summary>
    ///This is a test class for CountriesManagerTest and is intended
    ///to contain all CountriesManagerTest Unit Tests
    ///</summary>
    [TestClass()]
    public class TagsManagerTest
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

        const string TEST_EXTID = "ut";


        /// <summary>
        ///A test for GetByFilter
        ///</summary>
        [TestMethod()]
        public void GetByFilterTest()
        {
            var target = new TagsManager();
            var filter = new TagsFilter();
            filter.Id = 0;
            filter.TagTypeId = 0;
            filter.ExtId = TEST_EXTID;

            var actual = target.GetByFilter(filter, "");
            Assert.IsTrue(actual[0].ExtId == TEST_EXTID);
            //Assert.AreEqual(expected, actual);
            //Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for GetByKey
        ///</summary>
        [TestMethod()]
        public void GetByExtId()
        {
            var target = new TagsManager();
            var actual = target.GetByExtId(TEST_EXTID);
            Assert.IsTrue(actual.ExtId == TEST_EXTID);
        }


        [TestMethod()]
        public void DeleteByExtIdTest()
        {
            var target = new TagsManager();
            int actual = target.DeleteByExtId(TEST_EXTID);
            Assert.IsTrue(actual > 0);
        }

        /// <summary>
        ///A test for Insert
        ///</summary>
        [TestMethod()]
        public void InsertTest()
        {
            var target = new TagsManager();
            var theObj = new Tag(); ;
            var actual = new Tag();

            target.DeleteByExtId(TEST_EXTID);

            theObj.TitleTranslations.Add(Config.CultureDefault, "title-test");
            theObj.DescriptionTranslations.Add(Config.CultureDefault, "desc-test");
            theObj.ExtId = TEST_EXTID;
            actual = target.Insert(theObj);
            Assert.AreEqual(theObj.ExtId, actual.ExtId);
        }

        /// <summary>
        ///A test for Update
        ///</summary>
        [TestMethod()]
        public void UpdateTest()
        {
            var target = new TagsManager();
            var theObj = new Tag(); ;
            theObj = target.GetByExtId(TEST_EXTID);
            theObj.TagTypeId = 1;
            int actual = target.Update(theObj);
            Assert.IsTrue(actual > 0);
        }
    }
}
