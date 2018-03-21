using System;
using System.IO;
using ICloneCodeCharacteristicsLibrary.Classes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ICloneTestProject
{
   [TestClass()]
   public class CSyntacticInfoLoaderTest
   {
      [TestMethod()]
      [DeploymentItem("Tests\\syntactic_info.icch")]
      public void LoadTest()
      {
         string filename = Path.GetFullPath("syntactic_info.icch");
         Assert.IsTrue(File.Exists(filename));

         CSyntacticInfoLoader target = new CSyntacticInfoLoader(Path.GetDirectoryName(filename));
         CSyntacticInfo actual = target.Load();
         Assert.AreEqual(new DateTime(2012, 05, 06, 18, 55, 41), actual.Date);
         Assert.AreEqual(3, actual.MinSyntacticUnitSize);
         Assert.AreEqual(3658, actual.MaxSyntacticUnitSize);
         Assert.AreEqual(25677, actual.TotalSyntacticUnitsCount);
         Assert.AreEqual(27, actual.Kmin);
      }
   }
}