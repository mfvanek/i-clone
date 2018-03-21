using System;
using System.IO;
using CodeLoadingLibrary.Classes;
using ICloneCodeCharacteristicsLibrary.Classes;
using ISourceFilesLibrary.Classes.Languages;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ICloneTestProject
{
   [TestClass()]
   public class CSyntacticUnitsInfoProviderTest
   {
      private CSyntacticInfo calc(string testfile)
      {
         string filename = Path.GetFullPath(testfile);
         Assert.IsTrue(File.Exists(filename));

         LANGUAGES lang = LANGUAGES.LANGUAGE_C_SHARP;
         CLoadFilesOptions load_files_options = new CLoadFilesOptions(filename);
         CSyntacticUnitsInfoProvider target = new CSyntacticUnitsInfoProvider(lang, load_files_options);
         return target.Calculate();
      }

      [TestMethod()]
      [DeploymentItem("Tests\\UnitTestSampleFile.cs")]
      public void CalculateTest1()
      {
         CSyntacticInfo actual = calc("UnitTestSampleFile.cs");

         Assert.AreEqual(103, actual.TotalSyntacticUnitsCount);
         Assert.AreEqual(116, actual.MaxSyntacticUnitSize);
         Assert.AreEqual(7, actual.MinSyntacticUnitSize);
         Assert.AreEqual(21.000, Math.Round(actual.MediumSyntacticUnitSize, 3));
         Assert.AreEqual(11.933, Math.Round(actual.Dispersion, 3));
         Assert.AreEqual(3.45446, Math.Round(actual.MeanSquareDeviation, 5));

         Assert.AreEqual(12, actual.Kmin);
      }

      [TestMethod()]
      [DeploymentItem("Tests\\UnitTestSampleFile2.cs")]
      public void CalculateTest2()
      {
         CSyntacticInfo actual = calc("UnitTestSampleFile2.cs");

         Assert.AreEqual(12, actual.TotalSyntacticUnitsCount);
         Assert.AreEqual(61, actual.MaxSyntacticUnitSize);
         Assert.AreEqual(12, actual.MinSyntacticUnitSize);
         Assert.AreEqual(28.000, Math.Round(actual.MediumSyntacticUnitSize, 3));
         Assert.AreEqual(7.33, Math.Round(actual.Dispersion, 3));
         Assert.AreEqual(2.70747, Math.Round(actual.MeanSquareDeviation, 5));

         Assert.AreEqual(14, actual.Kmin);
      }

      [TestMethod()]
      [DeploymentItem("Tests\\UnitTestSampleFile3.cs")]
      public void CalculateTest3()
      {
         CSyntacticInfo actual = calc("UnitTestSampleFile3.cs");

         Assert.AreEqual(3, actual.TotalSyntacticUnitsCount);
         Assert.AreEqual(26, actual.MaxSyntacticUnitSize);
         Assert.AreEqual(10, actual.MinSyntacticUnitSize);
         Assert.AreEqual(17.000, Math.Round(actual.MediumSyntacticUnitSize, 3));
         Assert.AreEqual(2.519, Math.Round(actual.Dispersion, 3));
         Assert.AreEqual(1.58721, Math.Round(actual.MeanSquareDeviation, 5));

         Assert.AreEqual(6, actual.Kmin);
      }

      [TestMethod()]
      [DeploymentItem("Tests\\MimeTypeDetection.cs")]
      public void CalculateTest4()
      {
         CSyntacticInfo actual = calc("MimeTypeDetection.cs");

         Assert.AreEqual(4, actual.TotalSyntacticUnitsCount);
         Assert.AreEqual(68, actual.MaxSyntacticUnitSize);
         Assert.AreEqual(12, actual.MinSyntacticUnitSize);
         Assert.AreEqual(39.000, Math.Round(actual.MediumSyntacticUnitSize, 3));
         Assert.AreEqual(18.744, Math.Round(actual.Dispersion, 3));
         Assert.AreEqual(4.32939, Math.Round(actual.MeanSquareDeviation, 5));

         Assert.AreEqual(24, actual.Kmin);
      }
   }
}