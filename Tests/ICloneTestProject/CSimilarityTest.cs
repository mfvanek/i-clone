using IClone.ICloneReport.Classes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ICloneTestProject
{
    /// <summary>
    ///This is a test class for CSimilarityTest and is intended
    ///to contain all CSimilarityTest Unit Tests
    ///</summary>
    [TestClass()]
    public class CSimilarityTest
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

        private CSimilarity BaseTest()
        {
            CSimilarity target = new CSimilarity();
            double expected = CSimilarity.MIN_SIMILARITY_VALUE;
            Assert.AreEqual(expected, target.Similarity);
            return target;
        }

        /// <summary>
        ///A test for Similarity
        ///</summary>
        [TestMethod()]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void SimilarityTest()
        {
            CSimilarity target = BaseTest();
            double expected = CSimilarity.MAX_SIMILARITY_VALUE;
            target.Similarity = CSimilarity.MAX_SIMILARITY_VALUE;
            Assert.AreEqual(expected, target.Similarity);

            target.Similarity = 1.000000000001D;
        }

        /// <summary>
        ///A test for CSimilarity Constructor
        ///</summary>
        [TestMethod()]
        public void CSimilarityConstructorTest()
        {
            BaseTest();
        }

        /// <summary>
        ///A test for CSimilarity Constructor
        ///</summary>
        [TestMethod()]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void CSimilarityConstructorTest1()
        {
            double _Similarity = 0.340000000000000056D;
            CSimilarity target = new CSimilarity(_Similarity);
            Assert.IsNotNull(target);
            Assert.AreEqual(target.Similarity, _Similarity);

            CSimilarity second = new CSimilarity(_Similarity);
            Assert.AreEqual(second, target);

            _Similarity = -1.340000000000000056D;
            target = new CSimilarity(_Similarity);
        }
    }
}
