using PigeonCms.Geo;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace PigeonCms.UT.Core
{
    [TestClass()]
    public class ItemTagsManagerTest
    {


        private TestContext testContextInstance;
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

        const int TEST_ITEMID = 1;
        const int TEST_TAGID = 1;


        [TestMethod()]
        public void GetByFilterTest()
        {
            var target = new ItemTagsManager();
            var filter = new ItemTagsFilter();
            filter.ItemId = TEST_ITEMID;
            filter.TagId = TEST_TAGID;

            var actual = target.GetByFilter(filter, "");
            Assert.IsTrue(actual[0].ItemId == TEST_ITEMID);
            Assert.IsTrue(actual[0].TagId == TEST_TAGID);
            //Assert.AreEqual(expected, actual);
            //Assert.Inconclusive("Verify the correctness of this test method.");
        }

        [TestMethod()]
        public void GetByKey()
        {
            var target = new ItemTagsManager();
            var actual = target.GetByKey(TEST_ITEMID, TEST_TAGID);
            Assert.IsTrue(actual.ItemId == TEST_ITEMID);
            Assert.IsTrue(actual.TagId == TEST_TAGID);
        }


        [TestMethod()]
        public void DeleteByIdTest()
        {
            var target = new ItemTagsManager();
            int actual = target.DeleteById(TEST_ITEMID, TEST_TAGID);
            Assert.IsTrue(actual > 0);
        }

        [TestMethod()]
        public void InsertTest()
        {
            var target = new ItemTagsManager();
            var theObj = new ItemTag(); ;
            var actual = new ItemTag();

            target.DeleteById(TEST_ITEMID, TEST_TAGID);

            theObj.ItemId = TEST_ITEMID;
            theObj.TagId = TEST_TAGID;
            actual = target.Insert(theObj);
            Assert.AreEqual(theObj.ItemId, actual.ItemId);
            Assert.AreEqual(theObj.TagId, actual.TagId);
        }

        [TestMethod()]
        public void UpdateTest()
        {
            var target = new ItemTagsManager();
            var theObj = new ItemTag();
            theObj = target.GetByKey(TEST_ITEMID, TEST_TAGID);
            try
            {
                int actual = target.Update(theObj);
            }
            catch (NotSupportedException)
            {
                Assert.IsTrue(true);
                return;
            }
            Assert.Fail();
        }
    }
}
