﻿using ISourceFilesLibrary.Classes.StringEqualityAlgorithms;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ICloneTestProject
{
    /// <summary>
    ///This is a test class for CSimpleStringEqualityAlgorithmTest and is intended
    ///to contain all CSimpleStringEqualityAlgorithmTest Unit Tests
    ///</summary>
    [TestClass()]
    public class CSimpleStringEqualityAlgorithmTest
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
        ///A test for Equals
        ///</summary>
        [TestMethod()]
        public void EqualsTest()
        {
            CSimpleStringEqualityAlgorithm target = new CSimpleStringEqualityAlgorithm();

            Assert.IsTrue(target.Equals(null, null));
            Assert.IsFalse(target.Equals(null, ""));
            Assert.IsFalse(target.Equals("", null));

            Assert.IsTrue(target.Equals(string.Empty, string.Empty));
            Assert.IsTrue(target.Equals("qwerty", "qwerty"));
            Assert.IsFalse(target.Equals("qwerty", "123456"));
        }
    }
}
