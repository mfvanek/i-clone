using System.IO;
using System.Text;
using CodeLoadingLibrary.Classes.SourceFileContentLoader;
using ICloneExtensions.Classes;
using ISourceFilesLibrary.Classes.CodeUnitsCollection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ISourceFilesLibrary.Classes.SourceFile;

namespace ICloneTestProject
{
   [TestClass()]
   public class CEntireRowSourceFileContentLoaderTest
   {
      [TestMethod()]
      [DeploymentItem("Tests\\UnitTestSampleFile.cs")]
      public void LoadTest1()
      {
         string filename = Path.GetFullPath("UnitTestSampleFile.cs");
         Assert.IsTrue(File.Exists(filename));

         CTokenizerParms args = new CTokenizerParms(filename, new CSourceFileID());
         CEntireRowSourceFileContentLoader target = new CEntireRowSourceFileContentLoader(args);
         CCodeUnitsCollection actual = target.Load();
         Assert.AreEqual(1458, actual.Size());
      }

      [TestMethod()]
      [DeploymentItem("Tests\\UnitTestSampleFile2.cs")]
      public void LoadTest2()
      {
         string filename = Path.GetFullPath("UnitTestSampleFile2.cs");
         Assert.IsTrue(File.Exists(filename));

         CTokenizerParms args = new CTokenizerParms(filename, new CSourceFileID());
         CEntireRowSourceFileContentLoader target = new CEntireRowSourceFileContentLoader(args);
         CCodeUnitsCollection actual = target.Load();
         Assert.AreEqual(76, actual.Size());
      }
   }
}