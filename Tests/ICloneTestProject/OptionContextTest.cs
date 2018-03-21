using NDesk.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ICloneTestProject
{
    /// <summary>
    ///This is a test class for OptionContextTest and is intended
    ///to contain all OptionContextTest Unit Tests
    ///</summary>
    [TestClass()]
    public class OptionContextTest
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

        /// <summary>
        ///A test for Option
        ///</summary>
        [TestMethod()]
        public void OptionTest()
        {
            //OptionSet p = new OptionSet() {
            //    { "a=", v => { /* ignore */ } },
            //};
            //OptionContext c = new OptionContext(p);
            //Utils.AssertException(typeof(InvalidOperationException),
            //        "OptionContext.Option is null.",
            //        c, v => { string ignore = v.OptionValues[0]; });
            //c.Option = p[0];
            //Utils.AssertException(typeof(ArgumentOutOfRangeException),
            //        "Argument is out of range.\nParameter name: index",
            //        c, v => { string ignore = v.OptionValues[2]; });
            //c.OptionName = "-a";
            //Utils.AssertException(typeof(OptionException),
            //        "Missing required value for option '-a'.",
            //        c, v => { string ignore = v.OptionValues[0]; });
        }
    }
}
