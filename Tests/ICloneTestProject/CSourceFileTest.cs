using ISourceFilesLibrary.Classes.SourceFile;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Text;
using ISourceFilesLibrary.Classes.CodeFragment;
using ISourceFilesLibrary.Classes.CodeUnit;
using ISourceFilesLibrary.Classes.CodeUnitsCollection;

namespace ICloneTestProject
{
   [TestClass()]
   public class CSourceFileTest
   {
      private CCodeUnitsCollection CreateCollection()
      {
         CCodeUnitsCollection target = new CCodeUnitsCollection();
         target.Add(new CExtendedCodeUnit(new CCodeUnit(new CElementPosition(12, 23), ")"), 1, 1));
         target.Add(new CExtendedCodeUnit(new CCodeUnit(new CElementPosition(13, 33), ")"), 1, 1));
         target.Add(new CExtendedCodeUnit(new CCodeUnit(new CElementPosition(14, 43), ")"), 1, 1));
         return target;
      }

      [TestMethod()]
      public void CSourceFileConstructorTest1()
      {
         CSourceFileID _FileID = new CSourceFileID();
         string FullPathAndFileName = "somefile.ext";
         Encoding file_encoding = Encoding.GetEncoding(866);
         CCodeFragment _Content = new CCodeFragment();
         CSourceFile target = new CSourceFile(_FileID, FullPathAndFileName, file_encoding, _Content);
         Assert.IsNotNull(target);
      }

      [TestMethod()]
      public void CSourceFileConstructorTest2()
      {
         string FullPathAndFileName = "somefile.ext";
         Encoding file_encoding = Encoding.GetEncoding(866);
         CCodeFragment _Content = new CCodeFragment();
         CSourceFile target = new CSourceFile(FullPathAndFileName, file_encoding, _Content);
         Assert.IsNotNull(target);
      }

      [TestMethod()]
      [ExpectedException(typeof(ArgumentNullException))]
      public void CSourceFileConstructorTest3()
      {
         string FullPathAndFileName = string.Empty;
         Encoding file_encoding = Encoding.GetEncoding(866);
         CCodeFragment _Content = new CCodeFragment();
         CSourceFile target = new CSourceFile(FullPathAndFileName, file_encoding, _Content);
      }

      [TestMethod()]
      public void SizeTest()
      {
         string FullPathAndFileName = "somefile.ext";
         Encoding file_encoding = Encoding.GetEncoding(866);
         CCodeFragment _Content = new CCodeFragment(CreateCollection());
         CSourceFile target = new CSourceFile(FullPathAndFileName, file_encoding, _Content);

         Assert.AreEqual(3, target.Size());
      }

      [TestMethod()]
      public void ContentTest()
      {
         string FullPathAndFileName = "somefile.ext";
         Encoding file_encoding = Encoding.GetEncoding(866);
         CCodeFragment _Content = new CCodeFragment();
         CSourceFile target = new CSourceFile(FullPathAndFileName, file_encoding, _Content);
         Assert.AreEqual(0, target.Size());

         target.Content = new CCodeFragment(CreateCollection());
         Assert.AreEqual(3, target.Size());
      }

      [TestMethod()]
      public void FileEncodingTest()
      {
         string FullPathAndFileName = "somefile.ext";
         Encoding file_encoding = Encoding.GetEncoding(866);
         CCodeFragment _Content = new CCodeFragment();
         CSourceFile target = new CSourceFile(FullPathAndFileName, file_encoding, _Content);

         Encoding expected = Encoding.GetEncoding(866);
         Assert.IsTrue(target.FileEncoding.Equals(expected));
      }

      [TestMethod()]
      public void FilePathTest()
      {
         string FullPath = "dfgdfgdfg\\dfgdfg\\dfgdfg\\";
         Encoding file_encoding = Encoding.GetEncoding(866);
         CCodeFragment _Content = new CCodeFragment();
         CSourceFile target = new CSourceFile(FullPath + "somefile.ext", file_encoding, _Content);
         Assert.AreEqual(FullPath, target.FilePath);

         FullPath = "dfsdfsdfsd\\fgdfgdfg\\tyryrt";
         target.FilePath = FullPath;
         Assert.AreEqual(FullPath, target.FilePath);
      }

      [TestMethod()]
      public void NameTest()
      {
         string FullPathAndFileName = "sdfsdf\\sdfsdf\\sdfsdf\\somefile.ext";
         Encoding file_encoding = Encoding.GetEncoding(866);
         CCodeFragment _Content = new CCodeFragment();
         CSourceFile target = new CSourceFile(FullPathAndFileName, file_encoding, _Content);
         Assert.AreEqual("somefile.ext", target.Name);
      }

      [TestMethod()]
      public void SourceFileIDTest()
      {
         CSourceFileID _FileID = new CSourceFileID();
         string FullPathAndFileName = "somefile.ext";
         Encoding file_encoding = Encoding.GetEncoding(866);
         CCodeFragment _Content = new CCodeFragment();
         CSourceFile target = new CSourceFile(_FileID, FullPathAndFileName, file_encoding, _Content);
         Assert.AreEqual(_FileID.SourceFileID, target.SourceFileID);
      }
   }
}