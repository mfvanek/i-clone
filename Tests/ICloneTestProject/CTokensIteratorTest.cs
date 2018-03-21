using System.IO;
using CodeProfiler.CocoCS;
using ICloneExtensions.Cs;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ICloneTestProject
{
   [TestClass()]
   public class CTokensIteratorTest
   {
      const int TOKENS_COUNT = 350;

      [TestMethod()]
      [DeploymentItem("Tests\\UnitTestSampleFile2.cs")]
      public void CTokensIteratorConstructorTest()
      {
         string filename = Path.GetFullPath("UnitTestSampleFile2.cs");
         Assert.IsTrue(File.Exists(filename));

         Scanner lex = new Scanner(filename);
         CTokensIterator target = new CTokensIterator(lex);
         int counter = 0;
         foreach (Token t in target)
            ++counter;
         Assert.AreEqual(counter, TOKENS_COUNT);
      }
   }
}
