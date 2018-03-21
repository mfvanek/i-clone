using ISourceFilesLibrary.Classes.CodeUnit;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ICloneTestProject
{
   [TestClass()]
   public class CCodeUnitTest
   {
      [TestMethod()]
      public void CCodeUnitConstructorTest1()
      {
         CCodeUnit target = new CCodeUnit(new CElementPosition(12, 12), ")");
         Assert.IsNotNull(target);
      }

      [TestMethod()]
      public void CCodeUnitConstructorTest2()
      {
         CCodeUnit target = new CCodeUnit(new CElementPosition(12, 12), string.Empty);
         Assert.IsNotNull(target);
         Assert.AreEqual(target.Text, string.Empty);
      }

      [TestMethod()]
      [ExpectedException(typeof(ArgumentNullException))]
      public void CCodeUnitConstructorTest3()
      {
         CCodeUnit target = new CCodeUnit(new CElementPosition(12, 12), null);
      }

      [TestMethod()]
      public void CloneTest()
      {
         CCodeUnit target = new CCodeUnit(new CElementPosition(12, 12), ")");

         object actual = target.Clone();

         Assert.IsNotNull(actual);
         Assert.IsFalse(target == actual);
         Assert.IsTrue(actual.Equals(target));
         Assert.IsTrue(target.EqualsObject(actual));
         Assert.AreEqual(target, actual);
      }

      [TestMethod()]
      public void CompareToTest()
      {
         CCodeUnit first = new CCodeUnit(new CElementPosition(12, 12), ")");
         Assert.AreEqual(0, first.CompareTo(first));

         CCodeUnit second = new CCodeUnit(new CElementPosition(12, 12), ")");
         Assert.AreEqual(0, first.CompareTo(second));
         Assert.AreEqual(0, second.CompareTo(first));

         CCodeUnit third = new CCodeUnit(new CElementPosition(12, 12), ")");
         Assert.AreEqual(0, first.CompareTo(third));
         Assert.AreEqual(0, second.CompareTo(third));

         second = new CCodeUnit(new CElementPosition(12, 12), ")!");
         Assert.AreEqual(-1, first.CompareTo(second));
         Assert.AreEqual(1, second.CompareTo(first));

         second = new CCodeUnit(new CElementPosition(12, 12, 15, 1234), ")");
         Assert.AreEqual(-1, first.CompareTo(second));
         Assert.AreEqual(1, second.CompareTo(first));
      }

      [TestMethod()]
      public void EqualsTest()
      {
         CCodeUnit first = new CCodeUnit(new CElementPosition(12, 12), ")");
         CCodeUnit second = new CCodeUnit(new CElementPosition(12, 12), ")");

         Assert.IsFalse(first.Equals(null));
         Assert.IsTrue(first.Equals(first));

         Assert.IsTrue(first.Equals(second));
         Assert.AreEqual(first.Equals(second), second.Equals(first));

         second = new CCodeUnit(new CElementPosition(12, 12), "else");
         Assert.IsFalse(first.Equals(second));

         second = new CCodeUnit(new CElementPosition(1, 34), ")");
         Assert.IsTrue(second.Equals(first));
      }

      [TestMethod()]
      public void EqualsObjectTest1()
      {
         CCodeUnit first = new CCodeUnit(new CElementPosition(12, 12), ")");
         CCodeUnit second = new CCodeUnit(new CElementPosition(12, 12), ")");

         Assert.IsFalse(first.EqualsObject(null));
         Assert.IsTrue(first.EqualsObject(first));

         Assert.IsTrue(first.EqualsObject(second));
         Assert.AreEqual(first.EqualsObject(second), second.Equals(first));

         second = new CCodeUnit(new CElementPosition(12, 12), "else");
         Assert.IsFalse(first.EqualsObject(second));

         second = new CCodeUnit(new CElementPosition(1, 34), ")");
         Assert.IsFalse(second.EqualsObject(first));
      }

      [TestMethod()]
      public void EqualsObjectTest2()
      {
         CCodeUnit first = new CCodeUnit(new CElementPosition(12, 12), ")");
         object second = new CCodeUnit(new CElementPosition(12, 12), ")");

         Assert.IsFalse(first.EqualsObject(null));
         Assert.IsTrue(first.EqualsObject(first as object));

         Assert.IsTrue(first.EqualsObject(second));
         Assert.AreEqual(first.EqualsObject(second), second.Equals(first));

         second = new CCodeUnit(new CElementPosition(12, 12), "else");
         Assert.IsFalse(first.EqualsObject(second));
      }

      [TestMethod()]
      public void GetHashCodeTest()
      {
         string text = "))))dsfg dfgsdfhigndfkgsdfkgl sdfgjhsdfkgskl dfgsdfgjsdlf gsdflg";
         CCodeUnit target = new CCodeUnit(new CElementPosition(12, 12), text);
         Assert.AreEqual(text.GetHashCode(), target.GetHashCode());
      }

      [TestMethod()]
      public void TextTest1()
      {
         CCodeUnit target = new CCodeUnit(new CElementPosition(12, 12), ")");
         string text = "))))dsfg dfgsdfhigndfkgsdfkgl sdfgjhsdfkgskl dfgsdfgjsdlf gsdflg";
         target.Text = text;
         Assert.AreEqual(target.Text, text);
      }

      [TestMethod()]
      [ExpectedException(typeof(ArgumentNullException))]
      public void TextTest2()
      {
         CCodeUnit target = new CCodeUnit(new CElementPosition(12, 12), ")");
         target.Text = null;
      }

      [TestMethod()]
      public void TextTest3()
      {
         CCodeUnit target = new CCodeUnit(new CElementPosition(12, 12), ")");
         target.Text = string.Empty;
         Assert.AreEqual(target.Text, string.Empty);
      }

      [TestMethod()]
      public void CCodeUnitConstructorTest()
      {
         string expected = ")";
         CCodeUnit other = new CCodeUnit(new CElementPosition(12, 12), expected);
         CCodeUnit target = new CCodeUnit(other);
         other.Text = "(";
         Assert.AreEqual(target.Text, expected);
      }
   }
}