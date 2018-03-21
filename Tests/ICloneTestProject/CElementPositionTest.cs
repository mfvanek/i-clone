using ISourceFilesLibrary.Classes.CodeUnit;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ICloneTestProject
{
   [TestClass()]
   public class CCodeUnitPositionTest
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
      public void CCodeUnitPositionConstructorTest()
      {
         long line_start = 1;
         int index_start = 0;
         long line_end = 45;
         int index_end = 345;
         CElementPosition target = new CElementPosition(line_start, index_start, line_end, index_end);
         Assert.AreEqual(target.LineStart, line_start);
         Assert.AreEqual(target.LineEnd, line_end);
         Assert.AreEqual(target.IndexStart, index_start);
         Assert.AreEqual(target.IndexEnd, index_end);
      }

      [TestMethod()]
      public void CCodeUnitPositionConstructorTest1()
      {
         long line_start = 1;
         int index_start = 0;
         CElementPosition target = new CElementPosition(line_start, index_start);
         Assert.AreEqual(target.LineStart, line_start);
         Assert.AreEqual(target.LineEnd, line_start);
         Assert.AreEqual(target.IndexStart, index_start);
         Assert.AreEqual(target.IndexEnd, index_start);
      }

      [TestMethod()]
      [ExpectedException(typeof(InvalidElementPositionException))]
      public void CCodeUnitPositionConstructorTest2()
      {
         long line_start = -1;
         int index_start = 0;
         CElementPosition target = new CElementPosition(line_start, index_start);
      }

      [TestMethod()]
      [ExpectedException(typeof(InvalidElementPositionException))]
      public void CCodeUnitPositionConstructorTest3()
      {
         long line_start = 11;
         int index_start = -1;
         CElementPosition target = new CElementPosition(line_start, index_start);
      }

      [TestMethod()]
      [ExpectedException(typeof(InvalidElementPositionException))]
      public void CCodeUnitPositionConstructorTest4()
      {
         long line_start = 12;
         int index_start = 0;
         long line_end = 11;
         int index_end = 345;
         CElementPosition target = new CElementPosition(line_start, index_start, line_end, index_end);
      }

      [TestMethod()]
      [ExpectedException(typeof(InvalidElementPositionException))]
      public void CCodeUnitPositionConstructorTest5()
      {
         long line_start = 12;
         int index_start = 23;
         long line_end = 12;
         int index_end = 22;
         CElementPosition target = new CElementPosition(line_start, index_start, line_end, index_end);
      }

      [TestMethod()]
      public void CompareToTest()
      {
         long line_start = 9999999999;
         int index_start = 999999999;
         CElementPosition first = new CElementPosition(line_start, index_start);
         CElementPosition second = new CElementPosition(line_start, index_start);
         Assert.AreEqual(first.CompareTo(second), 0);

         second = new CElementPosition(line_start + 1, index_start);
         Assert.AreEqual(first.CompareTo(second), 0);

         second = new CElementPosition(line_start, index_start, line_start + 1, index_start);
         Assert.AreEqual(first.CompareTo(second), -1);
      }

      [TestMethod()]
      public void EqualsTest()
      {
         long line_start = 9999999999;
         int index_start = 999999999;
         CElementPosition first = new CElementPosition(line_start, index_start);
         CElementPosition second = new CElementPosition(line_start, index_start);
         Assert.IsTrue(first.Equals(second));
      }

      [TestMethod()]
      public void IndexEndTest1()
      {
         long line_start = 1;
         int index_start = 11;
         CElementPosition target = new CElementPosition(line_start, index_start);
         int expected = index_start + 1;
         target.IndexEnd = expected;
         Assert.AreEqual(expected, target.IndexEnd);
      }

      [TestMethod()]
      [ExpectedException(typeof(InvalidElementPositionException))]
      public void IndexEndTest2()
      {
         long line_start = 1;
         int index_start = 11;
         CElementPosition target = new CElementPosition(line_start, index_start);
         int expected = index_start - 1;
         target.IndexEnd = expected;
      }

      [TestMethod()]
      public void IndexEndTest3()
      {
         long line_start = 1;
         int index_start = 11;
         CElementPosition target = new CElementPosition(line_start, index_start, line_start + 1, index_start);
         int expected = index_start - 1;
         target.IndexEnd = expected;
         Assert.AreEqual(expected, target.IndexEnd);
      }

      [TestMethod()]
      public void IndexStartTest1()
      {
         long line_start = 1;
         int index_start = 11;
         CElementPosition target = new CElementPosition(line_start, index_start);
         int expected = index_start - 1;
         target.IndexStart = expected;
         Assert.AreEqual(expected, target.IndexStart);
      }

      [TestMethod()]
      [ExpectedException(typeof(InvalidElementPositionException))]
      public void IndexStartTest2()
      {
         long line_start = 1;
         int index_start = 11;
         CElementPosition target = new CElementPosition(line_start, index_start);
         int expected = index_start + 1;
         target.IndexStart = expected;
      }

      [TestMethod()]
      public void IndexStartTest3()
      {
         long line_start = 1;
         int index_start = 11;
         CElementPosition target = new CElementPosition(line_start, index_start, line_start + 1, index_start);
         int expected = index_start + 1;
         target.IndexStart = expected;
         Assert.AreEqual(expected, target.IndexStart);
      }

      [TestMethod()]
      public void LineEndTest1()
      {
         long line_start = 1;
         int index_start = 11;
         CElementPosition target = new CElementPosition(line_start, index_start);
         long expected = 99999999999;
         target.LineEnd = expected;
         Assert.AreEqual(expected, target.LineEnd);
      }

      [TestMethod()]
      [ExpectedException(typeof(InvalidElementPositionException))]
      public void LineEndTest2()
      {
         long line_start = 99999999999;
         int index_start = 11;
         CElementPosition target = new CElementPosition(line_start, index_start);
         long expected = 5;
         target.LineEnd = expected;
      }

      [TestMethod()]
      public void LineStartTest1()
      {
         long line_start = 9999999999;
         int index_start = 11;
         CElementPosition target = new CElementPosition(line_start, index_start);
         long expected = 56;
         target.LineStart = expected;
         Assert.AreEqual(expected, target.LineStart);
      }

      [TestMethod()]
      [ExpectedException(typeof(InvalidElementPositionException))]
      public void LineStartTest2()
      {
         long line_start = 444;
         int index_start = 11;
         CElementPosition target = new CElementPosition(line_start, index_start);
         long expected = 555;
         target.LineStart = expected;
      }

      [TestMethod()]
      public void CElementPositionConstructorTest()
      {
         long line_start = 3;
         int index_start = 11;
         CElementPosition other = new CElementPosition(line_start, index_start);
         CElementPosition target = new CElementPosition(other);
         other.LineEnd++;
         other.IndexStart--;
         other.LineStart--;
         Assert.AreEqual(other.LineEnd, line_start + 1);
         Assert.AreEqual(target.LineStart, line_start);
         Assert.AreEqual(target.LineEnd, line_start);
         Assert.AreEqual(target.IndexStart, index_start);
         Assert.AreEqual(target.IndexEnd, index_start);
      }
   }
}
