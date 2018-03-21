using ISourceFilesLibrary.Classes.CodeUnit;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using ISourceFilesLibrary.Classes;

namespace ICloneTestProject
{
   [TestClass()]
   public class CExtendedCodeUnitTest
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

      [TestMethod()]
      public void CExtendedCodeUnitConstructorTest1()
      {
         CExtendedCodeUnit target = new CExtendedCodeUnit(new CCodeUnit(new CElementPosition(12, 456), ")"), 11, 3);
         Assert.IsNotNull(target);
      }

      [TestMethod()]
      [ExpectedException(typeof(ArgumentOutOfRangeException))]
      public void CExtendedCodeUnitConstructorTest2()
      {
         CExtendedCodeUnit target = new CExtendedCodeUnit(new CCodeUnit(new CElementPosition(12, 456), ")"), CExtendedCodeUnit.DEFAULT_SOURCE_FILE_ID, 3);
      }

      [TestMethod()]
      [ExpectedException(typeof(ArgumentOutOfRangeException))]
      public void CExtendedCodeUnitConstructorTest3()
      {
         CExtendedCodeUnit target = new CExtendedCodeUnit(new CCodeUnit(new CElementPosition(12, 456), ")"), CExtendedCodeUnit.DEFAULT_SOURCE_FILE_ID + 1, CElementPosition.LINE_NUMBER_LOW_BOUND - 1);
      }

      [TestMethod()]
      public void CloneTest()
      {
         CExtendedCodeUnit target = new CExtendedCodeUnit(new CCodeUnit(new CElementPosition(12, 456), ")"), 11, 3);
         object actual = target.Clone();
         Assert.IsNotNull(actual);
         Assert.IsTrue(target.Equals(actual));
         Assert.IsTrue(target.EqualsObject(actual));
      }

      [TestMethod()]
      public void CompareToTest()
      {
         CExtendedCodeUnit first = new CExtendedCodeUnit(new CCodeUnit(new CElementPosition(12, 456), ")"), CExtendedCodeUnit.DEFAULT_SOURCE_FILE_ID + 1, 3);
         Assert.AreEqual(first.CompareTo(first), 0);

         CExtendedCodeUnit second = new CExtendedCodeUnit(new CCodeUnit(new CElementPosition(12, 456), ")"), CExtendedCodeUnit.DEFAULT_SOURCE_FILE_ID + 1, 3);
         Assert.AreEqual(first.CompareTo(second), 0);

         second = new CExtendedCodeUnit(new CCodeUnit(new CElementPosition(12, 456), ")"), CExtendedCodeUnit.DEFAULT_SOURCE_FILE_ID + 1, 4);
         Assert.AreEqual(first.CompareTo(second), -1);
         Assert.AreEqual(second.CompareTo(first), 1);
      }

      [TestMethod()]
      public void EqualsTest()
      {
         CExtendedCodeUnit target = new CExtendedCodeUnit(new CCodeUnit(new CElementPosition(12, 456), ")"), 11, 3);
         CExtendedCodeUnit other = new CExtendedCodeUnit(new CCodeUnit(new CElementPosition(54, 678), ")"), 22, 44);
         Assert.IsTrue(target.Equals(other));
      }

      [TestMethod()]
      public void EqualsObjectTest()
      {
         CExtendedCodeUnit target = new CExtendedCodeUnit(new CCodeUnit(new CElementPosition(12, 456), ")"), 11, 3);
         CExtendedCodeUnit other = new CExtendedCodeUnit(new CCodeUnit(new CElementPosition(54, 678), ")"), 22, 44);
         Assert.IsFalse(target.EqualsObject(other));

         other = new CExtendedCodeUnit(new CCodeUnit(new CElementPosition(12, 456), ")"), 11, 3);
         Assert.IsTrue(target.EqualsObject(other));
      }

      [TestMethod()]
      public void IndexEndTest()
      {
         int expected = 678585678; 
         CExtendedCodeUnit target = new CExtendedCodeUnit(new CCodeUnit(new CElementPosition(12, expected), ")"), 11, 3);
         Assert.AreEqual(expected, target.IndexEnd);

         ++expected;
         target.IndexEnd = expected;
         Assert.AreEqual(expected, target.IndexEnd);
      }

      [TestMethod()]
      public void IndexStartTest()
      {
         int expected = 678585678;
         CExtendedCodeUnit target = new CExtendedCodeUnit(new CCodeUnit(new CElementPosition(12, expected), ")"), 11, 3);
         Assert.AreEqual(expected, target.IndexStart);

         --expected;
         target.IndexStart = expected;
         Assert.AreEqual(expected, target.IndexStart);
      }

      [TestMethod()]
      public void LineEndTest()
      {
         long expected = 126785685;
         CExtendedCodeUnit target = new CExtendedCodeUnit(new CCodeUnit(new CElementPosition(expected, 456), ")"), 11, 3);
         Assert.AreEqual(expected, target.LineEnd);

         ++expected;
         target.LineEnd = expected;
         Assert.AreEqual(expected, target.LineEnd);
      }

      [TestMethod()]
      public void LineStartTest()
      {
         long expected = 126785685;
         CExtendedCodeUnit target = new CExtendedCodeUnit(new CCodeUnit(new CElementPosition(expected, 456), ")"), 11, 3);
         Assert.AreEqual(expected, target.LineStart);

         --expected;
         target.LineStart = expected;
         Assert.AreEqual(expected, target.LineStart);
      }

      [TestMethod()]
      public void SourceFileIDTest()
      {
         long expected = 34564544664;
         CExtendedCodeUnit target = new CExtendedCodeUnit(new CCodeUnit(new CElementPosition(12, 456), ")"), expected, 3);
         Assert.AreEqual(expected, target.SourceFileID);
      }

      [TestMethod()]
      public void TextTest1()
      {
         string expected = ")))))";
         CExtendedCodeUnit target = new CExtendedCodeUnit(new CCodeUnit(new CElementPosition(12, 456), expected), 11, 3);
         Assert.AreEqual(expected, target.Text);

         expected = "(((((";
         target.Text = expected;
         Assert.AreEqual(expected, target.Text);
      }

      [TestMethod()]
      [ExpectedException(typeof(ArgumentNullException))]
      public void TextTest2()
      {
         CExtendedCodeUnit target = new CExtendedCodeUnit(new CCodeUnit(new CElementPosition(12, 456), ")"), 11, 3);
         target.Text = null;
      }

      [TestMethod()]
      public void TextTest3()
      {
         CExtendedCodeUnit target = new CExtendedCodeUnit(new CCodeUnit(new CElementPosition(12, 456), ")"), 11, 3);
         target.Text = string.Empty;
         Assert.AreEqual(string.Empty, target.Text);
      }

      /// <summary>
      ///A test for CExtendedCodeUnit Constructor
      ///</summary>
      [TestMethod()]
      public void CExtendedCodeUnitConstructorTest()
      {
         long FileID = 11;
         CExtendedCodeUnit other = new CExtendedCodeUnit(new CCodeUnit(new CElementPosition(12, 456), ")"), FileID, 3);
         CExtendedCodeUnit target = new CExtendedCodeUnit(other);
         Assert.AreEqual(target.SourceFileID, FileID);
      }
   }
}
