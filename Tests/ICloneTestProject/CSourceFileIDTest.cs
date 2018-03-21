using ISourceFilesLibrary.Classes.SourceFile;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ICloneTestProject
{
   [TestClass()]
   public class CSourceFileIDTest
   {
      [TestMethod()]
      public void SourceFileIDTest()
      {
         System.GC.Collect();
         System.GC.WaitForPendingFinalizers();

         CSourceFileID first = new CSourceFileID();
         long FileIDLowBound = first.SourceFileID;
         Assert.AreEqual(FileIDLowBound, first.SourceFileID);

         CSourceFileID second = new CSourceFileID();
         Assert.AreEqual(FileIDLowBound + 1, second.SourceFileID);

         CSourceFileID third = new CSourceFileID();
         Assert.AreEqual(FileIDLowBound + 2, third.SourceFileID);

         second = null;
         System.GC.Collect();
         System.GC.WaitForPendingFinalizers();

         CSourceFileID fourth = new CSourceFileID();
         Assert.AreEqual(FileIDLowBound + 1, fourth.SourceFileID);
      }
   }
}
