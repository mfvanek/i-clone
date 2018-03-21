using ISourceFilesLibrary.Classes.HashCodeAlgorithms;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ICloneTestProject
{
    /// <summary>
    ///This is a test class for CSimpleHashCodeAlgorithmTest and is intended
    ///to contain all CSimpleHashCodeAlgorithmTest Unit Tests
    ///</summary>
    [TestClass()]
    public class CSimpleHashCodeAlgorithmTest
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
        ///A test for GetHashCode
        ///</summary>
        [TestMethod()]
        public void GetHashCodeTest1()
        {
            CSimpleHashCodeAlgorithm target = new CSimpleHashCodeAlgorithm();
            string Row1 = "1234567890qwertyQWERTY";
            string Row2 = "1234567890qwertyQWERTY";

            int expected = target.GetHashCode(Row1);
            int actual = target.GetHashCode(Row2);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for GetHashCode
        ///</summary>
        [TestMethod()]
        public void GetHashCodeTest2()
        {
            CSimpleHashCodeAlgorithm target = new CSimpleHashCodeAlgorithm();
            string Row1 = "_1234567890qwertyQWERTY";
            string Row2 = "1234567890qwertyQWERTY";

            int expected = target.GetHashCode(Row1);
            int actual = target.GetHashCode(Row2);
            Assert.AreNotEqual(expected, actual);
        }

        /// <summary>
        ///A test for GetHashCode
        ///</summary>
        [TestMethod()]
        public void GetHashCodeTest3()
        {
            CSimpleHashCodeAlgorithm target = new CSimpleHashCodeAlgorithm();
            string Row1 = "1234567890qwertyQWERTY_1234567890qwertyQWERTY_1234567890qwertyQWERTY_1234567890qwertyQWERTY";
            string Row2 = Row1.Clone() as string;

            int expected = target.GetHashCode(Row1);
            int actual = target.GetHashCode(Row2);
            Assert.AreEqual(expected, actual);
        }
    }
}
