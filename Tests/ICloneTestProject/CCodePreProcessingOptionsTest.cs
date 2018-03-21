
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using ISourceFilesLibrary.Classes;
using System.Collections.Generic;
using CodeLoadingLibrary.Classes;

namespace ICloneTestProject
{
    /// <summary>
    ///This is a test class for CCodePreProcessingOptionsTest and is intended
    ///to contain all CCodePreProcessingOptionsTest Unit Tests
    ///</summary>
    [TestClass()]
    public class CCodePreProcessingOptionsTest
    {
        private const string comments = "//;/* */;///";
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

        private CCodePreProcessingOptions BaseTest()
        {
            CCodePreProcessingOptions target = new CCodePreProcessingOptions(comments);
            Assert.IsNotNull(target);
            return target;
        }

        /// <summary>
        ///A test for CCodePreProcessingOptions Constructor
        ///</summary>
        [TestMethod()]
        public void CCodePreProcessingOptionsConstructorTest()
        {
            BaseTest();
        }

        /// <summary>
        ///A test for DeleteEmptyLines
        ///</summary>
        [TestMethod()]
        public void DeleteEmptyLinesTest()
        {
            CCodePreProcessingOptions target = BaseTest();
            Assert.IsTrue(target.DeleteEmptyLines);
        }

        /// <summary>
        ///A test for DeleteWhiteSpaces
        ///</summary>
        [TestMethod()]
        public void DeleteWhiteSpacesTest()
        {
            CCodePreProcessingOptions target = BaseTest();
            Assert.IsTrue(target.DeleteWhiteSpaces);
        }

        /// <summary>
        ///A test for DeleteComments
        ///</summary>
        [TestMethod()]
        public void DeleteCommentsTest()
        {
            CCodePreProcessingOptions target = BaseTest();
            Assert.IsTrue(target.DeleteComments);
        }

        /// <summary>
        ///A test for CommentSymbols
        ///</summary>
        [TestMethod()]
        public void CommentSymbolsTest()
        {
            CCodePreProcessingOptions target = BaseTest();
            СCommentSymbols expected = new СCommentSymbols(comments);
            СCommentSymbols actual = target.CommentSymbols;
            Assert.AreEqual(actual, expected);
        }

        /// <summary>
        ///A test for SetDeleteWhiteSpaces
        ///</summary>
        [TestMethod()]
        public void SetDeleteWhiteSpacesTest()
        {
            CCodePreProcessingOptions target = BaseTest();
            bool value = false;
            target.SetDeleteWhiteSpaces(value);
            Assert.AreEqual(value, target.DeleteWhiteSpaces);
        }

        /// <summary>
        ///A test for SetDeleteComments
        ///</summary>
        [TestMethod()]
        public void SetDeleteCommentsTest()
        {
            CCodePreProcessingOptions target = BaseTest();
            bool value = false;
            target.SetDeleteComments(value);
            Assert.AreEqual(value, target.DeleteComments);
        }

        /// <summary>
        ///A test for SetDeleteEmptyLines
        ///</summary>
        [TestMethod()]
        public void SetDeleteEmptyLinesTest()
        {
            CCodePreProcessingOptions target = BaseTest();
            bool value = false;
            target.SetDeleteEmptyLines(value);
            Assert.AreEqual(value, target.DeleteEmptyLines);
        }

        private void TestPairCommentDictionary(Dictionary<string, string> actual)
        {
            Dictionary<string, string> expected = new Dictionary<string, string>();
            expected.Add("/*", "*/");
            Assert.AreEqual(actual.Count, expected.Count);

            foreach (KeyValuePair<string, string> value in actual)
            {
                Assert.AreEqual(value.Value, expected[value.Key]);
            }
        }

        /// <summary>
        ///A test for SetCommentSymbols
        ///</summary>
        [TestMethod()]
        public void SetCommentSymbolsTest()
        {
            CCodePreProcessingOptions target = new CCodePreProcessingOptions();
            target.SetCommentSymbols(comments);
            Dictionary<string, string> actual = target.PairCommentDictionary;
            TestPairCommentDictionary(actual);
        }

        /// <summary>
        ///A test for PairCommentDictionary
        ///</summary>
        [TestMethod()]
        public void PairCommentDictionaryTest()
        {
            CCodePreProcessingOptions target = BaseTest();
            Dictionary<string, string> actual = target.PairCommentDictionary;
            TestPairCommentDictionary(actual);
        }
    }
}
