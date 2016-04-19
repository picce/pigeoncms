using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using PigeonCms.Core.Helpers;
using System.Web;


namespace PigeonCms.UT.Core
{
    [TestClass()]
    public class CookiesManagerTest
    {
		//http://caioproiete.net/en/fake-mock-httpcontext-without-any-special-mocking-framework/


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

        const string COOKIE_PLAIN = "cookie_plain";
        const string COOKIE_SECURE = "cookie_secure";
		string[] VALUES = 
						{ 
							"white", 
							"", 
							"yellow"
						};


        [TestMethod()]
		public void SetValue_Plain()
        {
			var target = new CookiesManager(COOKIE_PLAIN);
			target.SetValue("color0", VALUES[0]);
			target.SetValue("color1", VALUES[1]);
			target.SetValue("color2", VALUES[2]);

            //Assert.IsTrue(actual[0].TagId == TEST_TAGID);
            //Assert.AreEqual("", "");
            //Assert.Inconclusive("Verify the correctness of this test method.");
        }

		[TestMethod()]
		public void SetValue_Secure()
		{
			var target = new CookiesManager(COOKIE_SECURE, true, 60 * 24 * 10);
			target.SetValue("color0", VALUES[0]);
			target.SetValue("color1", VALUES[1]);
			target.SetValue("color2", VALUES[2]);
		}

		[TestMethod()]
		public void IsEmpty_Plain()
		{
			var target = new CookiesManager(COOKIE_PLAIN);

			Assert.IsFalse(target.IsEmpty("color0"));
			Assert.IsTrue(target.IsEmpty("color1"));
			Assert.IsFalse(target.IsEmpty("color2"));
		}

		[TestMethod()]
		public void IsEmpty_Secure()
		{
			var target = new CookiesManager(COOKIE_SECURE);

			Assert.IsFalse(target.IsEmpty("color0"));
			Assert.IsTrue(target.IsEmpty("color1"));
			Assert.IsFalse(target.IsEmpty("color2"));
		}		

		[TestMethod()]
		public void GetValue_Plain()
		{
			var target = new CookiesManager(COOKIE_PLAIN);

			Assert.AreEqual(target.GetValue("color0"), VALUES[0]);
			Assert.AreEqual(target.GetValue("color1"), VALUES[1]);
			Assert.AreEqual(target.GetValue("color2"), VALUES[2]);
		}

		[TestMethod()]
		public void GetValue_Secure()
		{
			var target = new CookiesManager(COOKIE_SECURE, true);

			Assert.AreEqual(target.GetValue("color0"), VALUES[0]);
			Assert.AreEqual(target.GetValue("color1"), VALUES[1]);
			Assert.AreEqual(target.GetValue("color2"), VALUES[2]);
		}


        [TestMethod()]
        public void Clear()
        {
			try
			{
				new CookiesManager(COOKIE_PLAIN).Clear();
				new CookiesManager(COOKIE_SECURE).Clear();
				Assert.IsTrue(true);
			}
			catch
			{
				Assert.Fail();
			}
        }

    }
}
