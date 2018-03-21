using System;
using ISourceFilesLibrary.Classes.CodeFragment;
using ISourceFilesLibrary.Classes.CodeUnit;
using ISourceFilesLibrary.Classes.CodeUnitsCollection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ICloneTestProject
{
   [TestClass()]
   public class CCodeFragmentTest
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
      public void CCodeFragmentConstructorTest()
      {
         CCodeFragment target = new CCodeFragment(CreateCollection());
         Assert.IsNotNull(target);
      }

      [TestMethod()]
      public void EqualsTest1()
      {
         CCodeFragment target = new CCodeFragment(CreateCollection());
         CCodeFragment other = new CCodeFragment(CreateCollection());

         Assert.IsFalse(Object.ReferenceEquals(target, other));
         Assert.IsTrue(target.Equals(other));

         other.Content.Add(new CExtendedCodeUnit(new CCodeUnit(new CElementPosition(12, 23), ")"), 1, 1));
         Assert.IsFalse(target.Equals(other));
      }

      [TestMethod()]
      public void IsBelongOneLineTest1()
      {
         CCodeFragment target = new CCodeFragment(CreateCollection());
         Assert.IsFalse(target.IsBelongOneLine());

         CCodeUnitsCollection _Content = new CCodeUnitsCollection();
         _Content.Add(new CExtendedCodeUnit(new CCodeUnit(new CElementPosition(12, 23), ")"), 1, 1));
         _Content.Add(new CExtendedCodeUnit(new CCodeUnit(new CElementPosition(12, 33), ")"), 1, 1));

         target = new CCodeFragment(_Content);
         Assert.IsTrue(target.IsBelongOneLine());
      }

      [TestMethod()]
      [ExpectedException(typeof(ArgumentOutOfRangeException))]
      public void IsBelongOneLineTest2()
      {
         CCodeUnitsCollection _Content = new CCodeUnitsCollection();
         CCodeFragment target = new CCodeFragment(_Content);
         bool actual = target.IsBelongOneLine();
      }

      [TestMethod()]
      public void ContentTest()
      {
         CCodeUnitsCollection _Content = new CCodeUnitsCollection();
         CCodeFragment target = new CCodeFragment(_Content);
         Assert.AreEqual(target.Content.Size(), 0);

         _Content = new CCodeUnitsCollection();
         _Content.Add(new CExtendedCodeUnit(new CCodeUnit(new CElementPosition(12, 23), ")"), 1, 1));
         _Content.Add(new CExtendedCodeUnit(new CCodeUnit(new CElementPosition(12, 33), ")"), 1, 1));
         target.Content = _Content;
         Assert.AreEqual(target.Content.Size(), 2);
      }

      [TestMethod()]
      public void IndexEndTest1()
      {
         CCodeFragment target = new CCodeFragment(CreateCollection());

         int expected = 43;
         Assert.AreEqual(expected, target.IndexEnd);

         ++expected;
         target.IndexEnd = expected;
         Assert.AreEqual(expected, target.IndexEnd);
      }

      [TestMethod()]
      [ExpectedException(typeof(ArgumentOutOfRangeException))]
      public void IndexEndTest2()
      {
         CCodeUnitsCollection _Content = new CCodeUnitsCollection();
         CCodeFragment target = new CCodeFragment(_Content);
         int actual = target.IndexEnd;
      }

      [TestMethod()]
      public void IndexStartTest1()
      {
         CCodeFragment target = new CCodeFragment(CreateCollection());

         int expected = 23;
         Assert.AreEqual(expected, target.IndexStart);

         --expected;
         target.IndexStart = expected;
         Assert.AreEqual(expected, target.IndexStart);
      }

      [TestMethod()]
      [ExpectedException(typeof(ArgumentOutOfRangeException))]
      public void IndexStartTest2()
      {
         CCodeUnitsCollection _Content = new CCodeUnitsCollection();
         CCodeFragment target = new CCodeFragment(_Content);
         int actual = target.IndexStart;
      }

      [TestMethod()]
      public void LineEndTest1()
      {
         CCodeFragment target = new CCodeFragment(CreateCollection());

         long expected = 14;
         Assert.AreEqual(expected, target.LineEnd);

         ++expected;
         target.LineEnd = expected;
         Assert.AreEqual(expected, target.LineEnd);
      }

      [TestMethod()]
      [ExpectedException(typeof(ArgumentOutOfRangeException))]
      public void LineEndTest2()
      {
         CCodeUnitsCollection _Content = new CCodeUnitsCollection();
         CCodeFragment target = new CCodeFragment(_Content);
         long actual = target.LineEnd;
      }

      [TestMethod()]
      public void LineStartTest1()
      {
         CCodeFragment target = new CCodeFragment(CreateCollection());

         long expected = 12;
         Assert.AreEqual(expected, target.LineStart);

         --expected;
         target.LineStart = expected;
         Assert.AreEqual(expected, target.LineStart);
      }

      [TestMethod()]
      [ExpectedException(typeof(ArgumentOutOfRangeException))]
      public void LineStartTest2()
      {
         CCodeUnitsCollection _Content = new CCodeUnitsCollection();
         CCodeFragment target = new CCodeFragment(_Content);
         long actual = target.LineStart;
      }
   }
}