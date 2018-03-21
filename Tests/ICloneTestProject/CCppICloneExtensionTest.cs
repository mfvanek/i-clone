using ICloneExtensions;
using ICloneExtensions.Cpp;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ISourceFilesLibrary.Classes.Languages;

namespace ICloneTestProject
{
    /// <summary>
    ///This is a test class for CCppICloneExtensionTest and is intended
    ///to contain all CCppICloneExtensionTest Unit Tests
    ///</summary>
    [TestClass()]
    public class CCppICloneExtensionTest
    {
        [TestMethod()]
        public void GetCommentSymbolsTest()
        {
            CCppICloneExtension target = new CCppICloneExtension();
            Assert.AreNotEqual(string.Empty, target.GetCommentSymbols());
        }

        /// <summary>
        ///A test for GetSourceFileExtentions
        ///</summary>
        [TestMethod()]
        public void GetSourceFileExtentionsTest()
        {
            CCppICloneExtension target = new CCppICloneExtension();
            Assert.AreNotEqual(string.Empty, target.GetSourceFileExtentions());
        }

        /// <summary>
        ///A test for LanguageID
        ///</summary>
        [TestMethod()]
        public void LanguageIDTest()
        {
            CCppICloneExtension target = new CCppICloneExtension();
            Assert.AreEqual(LANGUAGES.LANGUAGE_C_PLUS_PLUS, target.LanguageID());
        }
    }
}
