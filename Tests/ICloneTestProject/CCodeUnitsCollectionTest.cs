using System;
using System.Collections.Generic;
using ISourceFilesLibrary.Classes.CodeUnit;
using ISourceFilesLibrary.Classes.CodeUnitsCollection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ICloneTestProject
{
   [TestClass()]
   public class CCodeUnitsCollectionTest
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

      private CCodeUnitsCollection CreateObj()
      {
         CCodeUnitsCollection target = new CCodeUnitsCollection();
         target.Add(new CExtendedCodeUnit(new CCodeUnit(new CElementPosition(12, 23), ")"), 1, 1));
         target.Add(new CExtendedCodeUnit(new CCodeUnit(new CElementPosition(13, 33), ")"), 1, 1));
         target.Add(new CExtendedCodeUnit(new CCodeUnit(new CElementPosition(14, 43), ")"), 1, 1));
         return target;
      }

      [TestMethod()]
      public void CCodeUnitsCollectionConstructorTest()
      {
         CCodeUnitsCollection target = new CCodeUnitsCollection();
         Assert.IsNotNull(target);
         Assert.AreEqual(target.Size(), 0);
      }

      [TestMethod()]
      public void AddTest()
      {
         CCodeUnitsCollection target = new CCodeUnitsCollection();
         CExtendedCodeUnit item = new CExtendedCodeUnit(new CCodeUnit(new CElementPosition(12, 23), ")"), 1, 1);
         target.Add(item);
         Assert.AreEqual(target.Size(), 1);
         Assert.IsTrue(target[0].EqualsObject(item));
      }

      [TestMethod()]
      public void ClearTest()
      {
         CCodeUnitsCollection target = CreateObj();
         Assert.AreEqual(target.Size(), 3);
         target.Clear();
         Assert.AreEqual(target.Size(), 0);
      }

      [TestMethod()]
      public void EqualsTest()
      {
         CCodeUnitsCollection first = CreateObj();
         CCodeUnitsCollection second = new CCodeUnitsCollection();
         Assert.IsFalse(first.Equals(second));

         second.Add(new CExtendedCodeUnit(new CCodeUnit(new CElementPosition(12, 23), ")"), 1, 1));
         second.Add(new CExtendedCodeUnit(new CCodeUnit(new CElementPosition(13, 33), ")"), 1, 1));
         second.Add(new CExtendedCodeUnit(new CCodeUnit(new CElementPosition(14, 43), ")"), 1, 1));
         Assert.IsTrue(first.Equals(second));

         second.Add(new CExtendedCodeUnit(new CCodeUnit(new CElementPosition(12, 23), ")"), 1, 1));
         Assert.IsFalse(first.Equals(second));
      }

      [TestMethod()]
      public void RemoveRowTest()
      {
         CCodeUnitsCollection target = CreateObj();
         Assert.AreEqual(target.Size(), 3);
         target.RemoveRow(1);
         Assert.AreEqual(target.Size(), 2);
      }

      [TestMethod()]
      public void SetSourceRowsTest()
      {
         CCodeUnitsCollection target = new CCodeUnitsCollection();
         Assert.AreEqual(target.Size(), 0);

         List<CExtendedCodeUnit> units = new List<CExtendedCodeUnit>();
         units.Add(new CExtendedCodeUnit(new CCodeUnit(new CElementPosition(12, 23), ")"), 1, 1));
         units.Add(new CExtendedCodeUnit(new CCodeUnit(new CElementPosition(12, 23), ")"), 1, 1));
         units.Add(new CExtendedCodeUnit(new CCodeUnit(new CElementPosition(12, 23), ")"), 1, 1));
         Assert.AreEqual(units.Count, 3);

         target.SetCodeUnits(units);
         Assert.AreEqual(target.Size(), units.Count);
      }

      [TestMethod()]
      public void ToArrayTest()
      {
         CCodeUnitsCollection target = CreateObj();
         CExtendedCodeUnit[] actual;
         actual = target.ToArray();
         Assert.AreEqual(actual.Length, 3);
      }

      [TestMethod()]
      public void ToListTest()
      {
         CCodeUnitsCollection target = CreateObj();
         Assert.AreEqual(target.ToList().Count, 3);
      }

      [TestMethod()]
      public void ItemTest()
      {
         CCodeUnitsCollection target = CreateObj();
         Assert.IsTrue(target[1].EqualsObject(new CExtendedCodeUnit(new CCodeUnit(new CElementPosition(13, 33), ")"), 1, 1)));

         CExtendedCodeUnit expected = new CExtendedCodeUnit(new CCodeUnit(new CElementPosition(12, 23), ")"), 1, 1);
         target[1] = expected;
         Assert.IsTrue(target[1].EqualsObject(expected));
      }

      [TestMethod()]
      public void frontTest1()
      {
         CCodeUnitsCollection target = CreateObj();
         Assert.IsTrue(target.front().EqualsObject(new CExtendedCodeUnit(new CCodeUnit(new CElementPosition(12, 23), ")"), 1, 1)));
      }

      [TestMethod()]
      [ExpectedException(typeof(ArgumentOutOfRangeException))]
      public void frontTest2()
      {
         CCodeUnitsCollection target = new CCodeUnitsCollection();
         CExtendedCodeUnit unit = target.front();
      }

      [TestMethod()]
      public void backTest1()
      {
         CCodeUnitsCollection target = CreateObj();
         Assert.IsTrue(target.back().EqualsObject(new CExtendedCodeUnit(new CCodeUnit(new CElementPosition(14, 43), ")"), 1, 1)));
      }

      [TestMethod()]
      [ExpectedException(typeof(ArgumentOutOfRangeException))]
      public void backTest2()
      {
         CCodeUnitsCollection target = new CCodeUnitsCollection();
         CExtendedCodeUnit unit = target.back();
      }

      [TestMethod()]
      public void GetEnumeratorTest()
      {
         CCodeUnitsCollection target = CreateObj();
         string actual = string.Empty;
         foreach (CExtendedCodeUnit unit in target)
         {
            actual += unit.LineStart.ToString();
         }
         Assert.AreEqual(actual, "121314");
      }
   }
}