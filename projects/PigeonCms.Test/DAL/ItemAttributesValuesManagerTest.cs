using PigeonCms.Geo;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace PigeonCms.UT.Core
{
    [TestClass()]
    public class ItemAttributesValuesManagerTest
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
        const int TEST_ATTRID = 2;
        const int TEST_ATTR_VALUE_ID = 3;


        [TestMethod()]
        public void GetByFilterTest()
        {
            var target = new ItemAttributesValuesManager();
            var filter = new ItemAttributeValueFilter();
            filter.ItemId = TEST_ITEMID;
            filter.AttributeId = TEST_ATTRID;
            filter.AttributeValueId = TEST_ATTR_VALUE_ID;

            var actual = target.GetByFilter(filter, "");
            Assert.IsTrue(actual[0].ItemId == TEST_ITEMID);
            Assert.IsTrue(actual[0].AttributeId == TEST_ATTRID);
            Assert.IsTrue(actual[0].AttributeValueId == TEST_ATTR_VALUE_ID);
            //Assert.AreEqual(expected, actual);
            //Assert.Inconclusive("Verify the correctness of this test method.");
        }

        [TestMethod()]
        public void GetByKey()
        {
            try
            {
                var target = new ItemAttributesValuesManager();
                var actual = target.GetByKey(TEST_ITEMID);
            }
            catch (NotSupportedException)
            {
                Assert.IsTrue(true);
                return;
            }
            Assert.Inconclusive();
        }


        [TestMethod()]
        public void Delete()
        {
            var target = new ItemAttributesValuesManager();
            int actual = target.Delete(TEST_ITEMID,  TEST_ATTRID, TEST_ATTR_VALUE_ID);
            Assert.IsTrue(actual > 0);
        }

        [TestMethod()]
        public void InsertTest()
        {
            var target = new ItemAttributesValuesManager();
            var theObj = new ItemAttributeValue(); ;
            var actual = new ItemAttributeValue();

            target.Delete(TEST_ITEMID, TEST_ATTRID, TEST_ATTR_VALUE_ID);

            theObj.ItemId = TEST_ITEMID;
            theObj.AttributeId = TEST_ATTRID;
            theObj.AttributeValueId = TEST_ATTR_VALUE_ID;
            actual = target.Insert(theObj);
            Assert.AreEqual(theObj.ItemId, actual.ItemId);
            Assert.AreEqual(theObj.AttributeId, actual.AttributeId);
            Assert.AreEqual(theObj.AttributeValueId, actual.AttributeValueId);
        }

    }
}
