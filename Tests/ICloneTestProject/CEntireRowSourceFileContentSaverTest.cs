using ISourceFilesLibrary.Classes.SourceFile;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Text;
using ISourceFilesLibrary.Classes.CodeUnitsCollection;

namespace ICloneTestProject
{
   [TestClass()]
   public class CEntireRowSourceFileContentSaverTest
   {
      [TestMethod()]
      [ExpectedException(typeof(ArgumentNullException))]
      public void CEntireRowSourceFileContentSaverConstructorTest()
      {
         string _Path = string.Empty;
         Encoding _Encoding = null;
         CEntireRowSourceFileContentSaver target = new CEntireRowSourceFileContentSaver(_Path, _Encoding);
      }

      [TestMethod()]
      public void SaveTest()
      {
         string _Path = string.Empty; // TODO: Initialize to an appropriate value
         Encoding _Encoding = null; // TODO: Initialize to an appropriate value
         CEntireRowSourceFileContentSaver target = new CEntireRowSourceFileContentSaver(_Path, _Encoding); // TODO: Initialize to an appropriate value
         target.Save(null);
         Assert.Inconclusive("A method that does not return a value cannot be verified.");
      }
   }
}