using ICloneBaseLibrary.Classes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ICloneTestProject
{
    /// <summary>
    ///This is a test class for CPairTest and is intended
    ///to contain all CPairTest Unit Tests
    ///</summary>
    [TestClass()]
    public class CPairTest
    {
        string _First = "dgfsdfgsdfg hrth rhrthrh rыалвопыва апывап45п78нцук8пр укпварпы вап";
        string _Second = "5685terhgbjdfjk fgg54gtgrthfh";

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


        [TestMethod()]
        public void ToArrayTest()
        {
            CPair<string> target = new CPair<string>(_First, _Second);
            string[] expected = { _First, _Second };
            string[] actual = target.ToArray();
            Assert.AreEqual(expected.Length, actual.Length);
            for (int i = 0; i < actual.Length; i++)
            {
                Assert.AreEqual(expected[i], actual[i]);
            }
        }

        [TestMethod()]
        public void EqualsTest()
        {
            CPair<string> target = new CPair<string>(_First, _Second);
            object obj = null;
            Assert.IsFalse(target.Equals(obj));
            obj = new CPair<string>(_First, _Second);
            Assert.IsTrue(target.Equals(obj));
            obj = new CPair<int>(34, 3434);
            Assert.IsFalse(target.Equals(obj));
        }

        [TestMethod()]
        public void EqualsTest1()
        {
            CPair<string> target = new CPair<string>(_First, _Second);
            CPair<string> other = new CPair<string>(_First, _Second);
            Assert.IsTrue(target.Equals(other));
            target.Second = "dfghgfgh";
            Assert.IsFalse(target.Equals(other));
        }

        [TestMethod()]
        public void FirstTest()
        {
            CPair<string> target = new CPair<string>(_First, _Second);
            string expected = "fghddfgdfgsdf dgdfg56ghfghfgh";
            Assert.AreEqual(_First, target.First);
            target.First = expected;
            Assert.AreEqual(expected, target.First);
        }

        [TestMethod()]
        public void SecondTest()
        {
            CPair<string> target = new CPair<string>(_First, _Second);
            string expected = "fghddfghfghfgh";
            Assert.AreEqual(_Second, target.Second);
            target.Second = expected;
            Assert.AreEqual(expected, target.Second);
        }
    }
}
