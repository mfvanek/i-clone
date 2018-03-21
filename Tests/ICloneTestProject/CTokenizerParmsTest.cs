using System;
using System.Text;
using ICloneExtensions.Classes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ISourceFilesLibrary.Classes.SourceFile;

namespace ICloneTestProject
{
   [TestClass()]
   public class CTokenizerParmsTest
   {
      const string _Path = "..\\fdgsdfgdfgsdfgsdfg.cs";
      Encoding _Encoding = Encoding.GetEncoding(866);
      CSourceFileID FileID = new CSourceFileID();

      [TestMethod()]
      [ExpectedException(typeof(ArgumentNullException))]
      public void CTokenizerParmsConstructorTest1()
      {
         CTokenizerParms target = new CTokenizerParms(string.Empty, _Encoding, FileID);
      }

      [TestMethod()]
      public void GetEncodingTest()
      {
         CTokenizerParms target = new CTokenizerParms(_Path, _Encoding, FileID);
         Assert.AreEqual(_Encoding, target.GetEncoding());
      }

      [TestMethod()]
      public void GetPathTest()
      {
         CTokenizerParms target = new CTokenizerParms(_Path, _Encoding, FileID);
         Assert.AreEqual(_Path, target.GetPath());
      }

      [TestMethod()]
      public void GetSourceFileIDTest()
      {
         CTokenizerParms target = new CTokenizerParms(_Path, _Encoding, FileID);
         Assert.AreEqual(FileID, target.GetFileID());
      }
   }
}