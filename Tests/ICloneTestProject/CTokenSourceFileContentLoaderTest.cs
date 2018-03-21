using System.IO;
using System.Text;
using CodeLoadingLibrary.Classes.SourceFileContentLoader;
using ICloneExtensions;
using ICloneExtensions.Classes;
using ISourceFilesLibrary.Classes.CodeUnitsCollection;
using ISourceFilesLibrary.Classes.Languages;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ISourceFilesLibrary.Classes.SourceFile;

namespace ICloneTestProject
{
   [TestClass()]
   public class CTokenSourceFileContentLoaderTest
   {
      [TestMethod()]
      [DeploymentItem("Tests\\UnitTestSampleFile.cs")]
      public void LoadTest1()
      {
         string filename = Path.GetFullPath("UnitTestSampleFile.cs");
         Assert.IsTrue(File.Exists(filename));

         CTokenizerParms args = new CTokenizerParms(filename, new CSourceFileID());
         CTokenSourceFileContentLoader target = new CTokenSourceFileContentLoader(args, CAvailableExtentions.GetExtention(LANGUAGES.LANGUAGE_C_SHARP));
         CCodeUnitsCollection actual = target.Load();
         Assert.AreEqual(6483, actual.Size());
      }

      [TestMethod()]
      [DeploymentItem("Tests\\UnitTestSampleFile2.cs")]
      public void LoadTest2()
      {
         string filename = Path.GetFullPath("UnitTestSampleFile2.cs");
         Assert.IsTrue(File.Exists(filename));

         CTokenizerParms args = new CTokenizerParms(filename, new CSourceFileID());
         CTokenSourceFileContentLoader target = new CTokenSourceFileContentLoader(args, CAvailableExtentions.GetExtention(LANGUAGES.LANGUAGE_C_SHARP));
         CCodeUnitsCollection actual = target.Load();
         Assert.AreEqual(350, actual.Size());
      }

      [TestMethod()]
      [DeploymentItem("Tests\\MimeTypeDetection.cs")]
      public void LoadTest3()
      {
         string filename = Path.GetFullPath("MimeTypeDetection.cs");
         Assert.IsTrue(File.Exists(filename));

         CTokenizerParms args = new CTokenizerParms(filename, new CSourceFileID());
         CTokenSourceFileContentLoader target = new CTokenSourceFileContentLoader(args, CAvailableExtentions.GetExtention(LANGUAGES.LANGUAGE_C_SHARP));
         CCodeUnitsCollection actual = target.Load();
         Assert.AreEqual(796, actual.Size());
      }
   }
}