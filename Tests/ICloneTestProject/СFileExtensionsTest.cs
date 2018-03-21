using ICloneBaseLibrary.Classes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace ICloneTestProject
{
    /// <summary>
    ///This is a test class for СFileExtensionsTest and is intended
    ///to contain all СFileExtensionsTest Unit Tests
    ///</summary>
    [TestClass()]
    public class СFileExtensionsTest
    {
        private const string extensions = "*.s362;*.c;*.cc;*.cpp;*.h;*.hh;*.hpp;*.hss";
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


        private СFileExtensions BaseTest()
        {
            СFileExtensions target = new СFileExtensions(extensions);
            Assert.IsNotNull(target);

            return target;
        }

        /// <summary>
        ///A test for СFileExtensions Constructor
        ///</summary>
        [TestMethod()]
        public void СFileExtensionsConstructorTest()
        {
            СFileExtensions target = BaseTest();
        }

        /// <summary>
        ///A test for Add
        ///</summary>
        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AddTest()
        {
            СFileExtensions actual = new СFileExtensions();
            СFileExtensions expected = BaseTest();
            string[] items = {"*.s362","*.c","*.cc","*.cpp","*.h","*.hh","*.hpp","*.hss"};
            foreach (string item in items)
            {
                actual.Add(item);
            }
            Assert.AreEqual(expected, actual);

            actual.Add(string.Empty);
        }

        /// <summary>
        ///A test for SetFileExtensions
        ///</summary>
        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void SetFileExtensionsTest()
        {
            СFileExtensions target = new СFileExtensions();
            target.SetFileExtensions(extensions);
            СFileExtensions actual = BaseTest();
            Assert.AreEqual(target, actual);

            target.SetFileExtensions(string.Empty);
        }
    }
}
