using ISourceFilesLibrary.Classes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Text;
using CodeLoadingLibrary.Classes;
using System.Collections.Generic;

namespace ICloneTestProject
{
    /// <summary>
    ///This is a test class for CLoadFilesOptionsTest and is intended
    ///to contain all CLoadFilesOptionsTest Unit Tests
    ///</summary>
    [TestClass()]
    public class CLoadFilesOptionsTest
    {
        private TestContext testContextInstance;
        private static string directory = Environment.CurrentDirectory;
        private static string file_extensions = "*.xxx";
        private static int file_extensions_count = 1;
        private static int CODE_PAGE = 866;
        private static Encoding files_encoding = Encoding.GetEncoding(CODE_PAGE);
        private static string path_to_file = Path.Combine(directory, "dsfdf.xxx");

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

        private void BaseTest(CLoadFilesOptions target)
        {
            Assert.IsNotNull(target);
            Assert.AreEqual(target.Directory, directory);
            Assert.AreEqual(target.FileExtensions.Count, file_extensions_count);
            Assert.IsFalse(target.MustWorkWithOneFile);
        }

        private void BaseTestEx(CLoadFilesOptions target)
        {
            BaseTest(target);
            Assert.AreEqual(target.FileEncoding, files_encoding);
        }

        /// <summary>
        ///A test for CLoadFilesOptions Constructor
        ///</summary>
        [TestMethod()]
        public void CLoadFilesOptionsConstructorTest()
        {
            CLoadFilesOptions target = new CLoadFilesOptions(directory, file_extensions);
            BaseTest(target);
        }

        /// <summary>
        ///A test for CLoadFilesOptions Constructor
        ///</summary>
        [TestMethod()]
        public void CLoadFilesOptionsConstructorTest1()
        {
            CLoadFilesOptions target = new CLoadFilesOptions(directory, file_extensions, files_encoding);
            BaseTestEx(target);
        }

        /// <summary>
        ///A test for CLoadFilesOptions Constructor
        ///</summary>
        [TestMethod()]
        public void CLoadFilesOptionsConstructorTest2()
        {
            CCodePreProcessingOptions Options = new CCodePreProcessingOptions();
            CLoadFilesOptions target = new CLoadFilesOptions(directory, file_extensions, files_encoding, Options);
            BaseTestEx(target);
            Assert.AreEqual(target.PreProcessingOptions, Options);
        }

        /// <summary>
        ///A test for CLoadFilesOptions Constructor
        ///</summary>
        [TestMethod()]
        public void CLoadFilesOptionsConstructorTest3()
        {
            CLoadFilesOptions target = new CLoadFilesOptions(path_to_file);
            Assert.IsNotNull(target);
            Assert.IsTrue(target.MustWorkWithOneFile);
            Assert.AreEqual(target.PathToFile, path_to_file);
        }

        private CLoadFilesOptions CommotTest()
        {
            CLoadFilesOptions target = new CLoadFilesOptions();
            Assert.IsNotNull(target);
            Assert.IsFalse(target.MustWorkWithOneFile);
            Assert.AreEqual(target.Directory, string.Empty);
            Assert.AreEqual(target.PathToFile, string.Empty);

            return target;
        }

        /// <summary>
        ///A test for CLoadFilesOptions Constructor
        ///</summary>
        [TestMethod()]
        public void CLoadFilesOptionsConstructorTest4()
        {
            CLoadFilesOptions target = CommotTest();
        }

        /// <summary>
        ///A test for SetDirectory
        ///</summary>
        [TestMethod()]
        public void SetDirectoryTest()
        {
            CLoadFilesOptions target = CommotTest();
            target.SetDirectory(directory);
            Assert.AreEqual(target.Directory, directory);
            Assert.IsFalse(target.MustWorkWithOneFile);
        }

        /// <summary>
        ///A test for SetFileEncoding
        ///</summary>
        [TestMethod()]
        public void SetFileEncodingTest()
        {
            CLoadFilesOptions target = CommotTest();
            Encoding value = Encoding.GetEncoding(CODE_PAGE);
            target.SetFileEncoding(value);
            Assert.AreEqual(target.FileEncoding, files_encoding);
        }

        /// <summary>
        ///A test for SetFileExtensions
        ///</summary>
        [TestMethod()]
        public void SetFileExtensionsTest()
        {
            CLoadFilesOptions target = CommotTest();
            target.SetFileExtensions(file_extensions);
            Assert.AreEqual(target.FileExtensions.Count, file_extensions_count);
            Assert.AreEqual(target.FileExtensions[0], ".xxx");
        }

        /// <summary>
        ///A test for SetIsPreProcessingFile
        ///</summary>
        [TestMethod()]
        public void SetIsPreProcessingFileTest()
        {
            CLoadFilesOptions target = CommotTest();
            Assert.IsFalse(target.IsPreProcessingFile);

            target.SetIsPreProcessingFile(true);
            Assert.IsTrue(target.IsPreProcessingFile);
        }

        /// <summary>
        ///A test for SetIsUseParallelExtensions
        ///</summary>
        [TestMethod()]
        public void SetIsUseParallelExtensionsTest()
        {
            CLoadFilesOptions target = CommotTest();

            bool value = !target.IsUseParallelExtensions;
            target.SetIsUseParallelExtensions(value);
            Assert.AreEqual(value, target.IsUseParallelExtensions);
        }

        /// <summary>
        ///A test for SetPathToFile
        ///</summary>
        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void SetPathToFileTest()
        {
            CLoadFilesOptions target = CommotTest();
            target.SetPathToFile(path_to_file);
            Assert.AreEqual(target.PathToFile, path_to_file);
            Assert.AreEqual(target.Directory, string.Empty);
            Assert.IsTrue(target.MustWorkWithOneFile);

            target.SetPathToFile(string.Empty);
        }

        /// <summary>
        ///A test for SetPreProcessingOptions
        ///</summary>
        [TestMethod()]
        public void SetPreProcessingOptionsTest()
        {
            CLoadFilesOptions target = CommotTest();
            CCodePreProcessingOptions value = new CCodePreProcessingOptions();
            target.SetPreProcessingOptions(value);
            Assert.AreEqual(value, target.PreProcessingOptions);
        }

        /// <summary>
        ///A test for SetSkippingFolders
        ///</summary>
        [TestMethod()]
        public void SetSkippingFoldersTest()
        {
            CLoadFilesOptions target = CommotTest();
            Assert.AreEqual(target.SkippingFolders, string.Empty);

            string value = "rttt, dfsdf, sdfsdf";
            target.SetSkippingFolders(value);
            Assert.AreEqual(target.SkippingFolders, value);
        }
    }
}
