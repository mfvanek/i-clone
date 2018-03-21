using ISourceFilesLibrary.Classes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using ICloneBaseLibrary.Classes;
using System.Collections.Generic;
using CodeLoadingLibrary.Classes;

namespace ICloneTestProject
{
    /// <summary>
    ///This is a test class for СCommentSymbolsTest and is intended
    ///to contain all СCommentSymbolsTest Unit Tests
    ///</summary>
    [TestClass()]
    public class СCommentSymbolsTest
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

        private СCommentSymbols BaseTest()
        {
            СCommentSymbols target = new СCommentSymbols(comments);
            Assert.IsNotNull(target);

            return target;
        }

        private void CommentListTest()
        {
            СCommentSymbols actual = new СCommentSymbols();
            Assert.IsNotNull(actual);
            actual.AddCommentInList(new CPair<string>("//", string.Empty));
            actual.AddCommentInList(new CPair<string>("/*", "*/"));
            actual.AddCommentInList(new CPair<string>("///", string.Empty));

            СCommentSymbols expected = BaseTest();
            Assert.IsNotNull(expected);
            Assert.AreEqual(expected.GetCommentSymbolsList().Count, 3);
            Assert.AreEqual(expected.GetCommentSymbolsList().Count, actual.GetCommentSymbolsList().Count);

            for (int counter = 0; counter < expected.GetCommentSymbolsList().Count; counter++)
            {
                Assert.AreEqual(expected.GetCommentSymbolsList()[counter], actual.GetCommentSymbolsList()[counter]);
            }
        }

        /// <summary>
        ///A test for СCommentSymbols Constructor
        ///</summary>
        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void СCommentSymbolsConstructorTest()
        {
            СCommentSymbols target = BaseTest();
            target = new СCommentSymbols(string.Empty);
        }

        /// <summary>
        ///A test for AddCommentInList
        ///</summary>
        [TestMethod()]
        public void AddCommentInListTest()
        {
            CommentListTest();
        }

        /// <summary>
        ///A test for ToArray
        ///</summary>
        [TestMethod()]
        public void ToArrayTest()
        {
            СCommentSymbols target = BaseTest();
            string[] expected = {"//", string.Empty, "/*", "*/", "///", string.Empty };
            string[] actual = target.ToArray();
            Assert.AreEqual(expected.Length, actual.Length);
            for (int counter = 0; counter < actual.Length; counter++)
            {
                Assert.AreEqual(expected[counter], actual[counter]);
            }
        }

        /// <summary>
        ///A test for SetCommentSymbols
        ///</summary>
        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void SetCommentSymbolsTest()
        {
            СCommentSymbols target = new СCommentSymbols();
            Assert.IsNotNull(target);
            target.SetCommentSymbols(comments);
            Assert.AreEqual(target.MinCommentLength, 2);
            Assert.AreEqual(target.MaxCommentLength, 3);

            target.SetCommentSymbols(string.Empty);
        }

        /// <summary>
        ///A test for IsCommentSymbol
        ///</summary>
        [TestMethod()]
        public void IsCommentSymbolTest()
        {
            СCommentSymbols target = BaseTest();
            
            string[] pair_comment_symbols = { "/*", "*/" };
            string[] not_pair_comment_symbols = { "//", "///" };
            string[] non_comment_symbols = { "\\\\", "////", "rttt" };
            bool IsNotPair = false;

            foreach (string symbol in pair_comment_symbols)
            {
                Assert.IsTrue(target.IsCommentSymbol(symbol, ref IsNotPair));
                Assert.IsFalse(IsNotPair);
            }

            foreach (string symbol in not_pair_comment_symbols)
            {
                Assert.IsTrue(target.IsCommentSymbol(symbol, ref IsNotPair));
                Assert.IsTrue(IsNotPair);
            }

            foreach (string symbol in non_comment_symbols)
            {
                Assert.IsFalse(target.IsCommentSymbol(symbol, ref IsNotPair));
                Assert.IsTrue(IsNotPair);
            }
        }

        /// <summary>
        ///A test for IsCommentChar
        ///</summary>
        [TestMethod()]
        public void IsCommentCharTest()
        {
            СCommentSymbols target = BaseTest();

            char[] comment_items = { '*', '/'};
            foreach (char item in comment_items)
            {
                Assert.IsTrue(target.IsCommentChar(item));
            }

            char[] non_comment_items = { '\r', '\\', '&' };
            foreach (char item in non_comment_items)
            {
                Assert.IsFalse(target.IsCommentChar(item));
            }
        }

        /// <summary>
        ///A test for GetCommentSymbolsList
        ///</summary>
        [TestMethod()]
        public void GetCommentSymbolsListTest()
        {
            CommentListTest();
        }

        /// <summary>
        ///A test for MaxCommentLength
        ///</summary>
        [TestMethod()]
        public void MaxCommentLengthTest()
        {
            СCommentSymbols target = BaseTest();
            Assert.AreEqual(target.MaxCommentLength, 3);
        }

        /// <summary>
        ///A test for MinCommentLength
        ///</summary>
        [TestMethod()]
        public void MinCommentLengthTest()
        {
            СCommentSymbols target = BaseTest();
            Assert.AreEqual(target.MinCommentLength, 2);
        }
    }
}
