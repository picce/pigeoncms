using PigeonCms.Geo;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace PigeonCms.UT.Core
{
    [TestClass()]
    public class TagTypesManagerTest
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


        [TestMethod()]
        public void GetByFilterTest()
        {
            var target = new TagTypesManager();
            var filter = new TagTypesFilter();
            filter.Id = 0;
            filter.ItemType = "";
            filter.ExtId = TEST_EXTID;

            var actual = target.GetByFilter(filter, "");
            Assert.IsTrue(actual[0].ExtId == TEST_EXTID);
            //Assert.AreEqual(expected, actual);
            //Assert.Inconclusive("Verify the correctness of this test method.");
        }

        [TestMethod()]
        public void GetByExtId()
        {
            var target = new TagTypesManager();
            var actual = target.GetByExtId(TEST_EXTID);
            Assert.IsTrue(actual.ExtId == TEST_EXTID);
        }


        [TestMethod()]
        public void DeleteByExtIdTest()
        {
            var target = new TagTypesManager();
            int actual = target.DeleteByExtId(TEST_EXTID);
            Assert.IsTrue(actual > 0);
        }

        [TestMethod()]
        public void InsertTest()
        {
            var target = new TagTypesManager();
            var theObj = new TagType(); ;
            var actual = new TagType();

            target.DeleteByExtId(TEST_EXTID);

            theObj.TitleTranslations.Add(Config.CultureDefault, "title-test");
            theObj.DescriptionTranslations.Add(Config.CultureDefault, "desc-test");
            theObj.ExtId = TEST_EXTID;
            actual = target.Insert(theObj);
            Assert.AreEqual(theObj.ExtId, actual.ExtId);
        }

        [TestMethod()]
        public void UpdateTest()
        {
            var target = new TagTypesManager();
            var theObj = new TagType(); ;
            theObj = target.GetByExtId(TEST_EXTID);
            theObj.ItemType = "type-ut";
            int actual = target.Update(theObj);
            Assert.IsTrue(actual > 0);
        }
    }
}
