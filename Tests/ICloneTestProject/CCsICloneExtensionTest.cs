using ICloneExtensions.Classes;
using ICloneExtensions.Cs;
using ISourceFilesLibrary.Classes.Languages;
using ISourceFilesLibrary.Classes.SyntacticUnit;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Text;
using ISourceFilesLibrary.Classes.SourceFile;

namespace ICloneTestProject
{
    [TestClass()]
    public class CCsICloneExtensionTest
    {
        [TestMethod()]
        public void GetCommentSymbolsTest()
        {
            CCsICloneExtension target = new CCsICloneExtension();
            string actual = target.GetCommentSymbols();
            Assert.AreNotEqual(string.Empty, actual);
        }

        [TestMethod()]
        public void GetSourceFileExtentionsTest()
        {
            CCsICloneExtension target = new CCsICloneExtension();
            string actual = target.GetSourceFileExtentions();
            Assert.AreNotEqual(string.Empty, actual);
        }

        [TestMethod()]
        public void LanguageIDTest()
        {
            CCsICloneExtension target = new CCsICloneExtension();
            Assert.AreEqual(LANGUAGES.LANGUAGE_C_SHARP, target.LanguageID());
        }

        [TestMethod()]
        [DeploymentItem("Tests\\UnitTestSampleFile2.cs")]
        public void SyntacticizeTest2()
        {
           string filename = Path.GetFullPath("UnitTestSampleFile2.cs");
           Assert.IsTrue(File.Exists(filename));

           CCsICloneExtension target = new CCsICloneExtension();
           CTokenizerParms args = new CTokenizerParms(filename, new CSourceFileID());
           CSyntacticUnitsCollection actual = target.Syntacticize(args);
           Assert.AreEqual(12, actual.Size());
           Assert.AreEqual(37, actual[0].Size());
        }

        [TestMethod()]
        [DeploymentItem("Tests\\UnitTestSampleFile3.cs")]
        public void SyntacticizeTest3()
        {
           string filename = Path.GetFullPath("UnitTestSampleFile3.cs");
           Assert.IsTrue(File.Exists(filename));

           CCsICloneExtension target = new CCsICloneExtension();
           CTokenizerParms args = new CTokenizerParms(filename, new CSourceFileID());
           CSyntacticUnitsCollection actual = target.Syntacticize(args);
           Assert.AreEqual(3, actual.Size());
           Assert.AreEqual(10, actual[0].Size());
           Assert.AreEqual(26, actual[1].Size());
           Assert.AreEqual(16, actual[2].Size());
        }
    }
}
