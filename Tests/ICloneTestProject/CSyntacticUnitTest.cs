using ISourceFilesLibrary.Classes.CodeFragment;
using ISourceFilesLibrary.Classes.CodeUnit;
using ISourceFilesLibrary.Classes.CodeUnitsCollection;
using ISourceFilesLibrary.Classes.SyntacticUnit;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ICloneTestProject
{
   [TestClass()]
   public class CSyntacticUnitTest
   {
      private TestContext testContextInstance;

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

      private CCodeUnitsCollection CreateCollection()
      {
         CCodeUnitsCollection target = new CCodeUnitsCollection();
         target.Add(new CExtendedCodeUnit(new CCodeUnit(new CElementPosition(12, 23), ")"), 1, 1));
         target.Add(new CExtendedCodeUnit(new CCodeUnit(new CElementPosition(13, 33), ")"), 1, 1));
         target.Add(new CExtendedCodeUnit(new CCodeUnit(new CElementPosition(14, 43), ")"), 1, 1));
         return target;
      }

      [TestMethod()]
      public void SizeTest()
      {
         CCodeFragment Content = new CCodeFragment(CreateCollection());
         CSyntacticUnit target = new CSyntacticUnit(Content);
         Assert.AreEqual(3, target.Size());
      }

      [TestMethod()]
      public void IndexEndTest()
      {
         CCodeFragment Content = new CCodeFragment(CreateCollection());
         CSyntacticUnit target = new CSyntacticUnit(Content);
         Assert.AreEqual(43, target.IndexEnd);
      }

      [TestMethod()]
      public void IndexStartTest()
      {
         CCodeFragment Content = new CCodeFragment(CreateCollection());
         CSyntacticUnit target = new CSyntacticUnit(Content);
         Assert.AreEqual(23, target.IndexStart);
      }

      [TestMethod()]
      public void LineEndTest()
      {
         CCodeFragment Content = new CCodeFragment(CreateCollection());
         CSyntacticUnit target = new CSyntacticUnit(Content);
         Assert.AreEqual(14, target.LineEnd);
      }

      [TestMethod()]
      public void LineStartTest()
      {
         CCodeFragment Content = new CCodeFragment(CreateCollection());
         CSyntacticUnit target = new CSyntacticUnit(Content);
         Assert.AreEqual(12, target.LineStart);
      }
   }
}