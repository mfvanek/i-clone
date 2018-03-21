using ICloneCodeCharacteristicsLibrary.Classes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ICloneTestProject
{
   [TestClass()]
   public class CSyntacticInfoTest
   {
      [TestMethod()]
      public void DateTest()
      {
         CSyntacticInfo target = new CSyntacticInfo();
         DateTime expected = DateTime.Now;
         target.Date = expected;
         Assert.AreEqual(expected, target.Date);
      }

      [TestMethod()]
      public void DispersionTest1()
      {
         CSyntacticInfo target = new CSyntacticInfo();
         target.MinSyntacticUnitSize = 10;
         target.MediumSyntacticUnitSize = 15;
         double expected = 0.0025;
         target.Dispersion = expected;
         Assert.AreEqual(expected, target.Dispersion);
         Assert.AreEqual(0.05, target.MeanSquareDeviation);
         Assert.AreEqual(5, target.Kmin);
      }

      [TestMethod()]
      public void DispersionTest2()
      {
         CSyntacticInfo target = new CSyntacticInfo();
         target.MinSyntacticUnitSize = 15;
         target.MediumSyntacticUnitSize = 15;
         double expected = 0;
         target.Dispersion = expected;
         Assert.AreEqual(expected, target.Dispersion);
         Assert.AreEqual(0, target.MeanSquareDeviation);
         Assert.AreEqual(15, target.Kmin);
      }

      [TestMethod()]
      public void MaxSyntacticUnitSizeTest1()
      {
         CSyntacticInfo target = new CSyntacticInfo();
         long expected = 456554;
         target.MaxSyntacticUnitSize = expected;
         Assert.AreEqual(expected, target.MaxSyntacticUnitSize);
      }

      [TestMethod()]
      [ExpectedException(typeof(ArgumentOutOfRangeException))]
      public void MaxSyntacticUnitSizeTest2()
      {
         CSyntacticInfo target = new CSyntacticInfo();
         target.MaxSyntacticUnitSize = 0;
      }

      [TestMethod()]
      public void MediumSyntacticUnitSizeTest1()
      {
         CSyntacticInfo target = new CSyntacticInfo();
         double expected = 345.546456;
         target.MediumSyntacticUnitSize = expected;
         Assert.AreEqual(expected, target.MediumSyntacticUnitSize);
      }

      [TestMethod()]
      [ExpectedException(typeof(ArgumentOutOfRangeException))]
      public void MediumSyntacticUnitSizeTest2()
      {
         CSyntacticInfo target = new CSyntacticInfo();
         target.MediumSyntacticUnitSize = 0;
      }

      [TestMethod()]
      public void MinSyntacticUnitSizeTest1()
      {
         CSyntacticInfo target = new CSyntacticInfo();
         long expected = 345;
         target.MinSyntacticUnitSize = expected;
         Assert.AreEqual(expected, target.MinSyntacticUnitSize);
      }

      [TestMethod()]
      [ExpectedException(typeof(ArgumentOutOfRangeException))]
      public void MinSyntacticUnitSizeTest2()
      {
         CSyntacticInfo target = new CSyntacticInfo();
         target.MinSyntacticUnitSize = 0;
      }

      [TestMethod()]
      public void TotalSyntacticUnitsCountTest1()
      {
         CSyntacticInfo target = new CSyntacticInfo();
         long expected = 345;
         target.TotalSyntacticUnitsCount = expected;
         Assert.AreEqual(expected, target.TotalSyntacticUnitsCount);
      }

      [TestMethod()]
      [ExpectedException(typeof(ArgumentOutOfRangeException))]
      public void TotalSyntacticUnitsCountTest2()
      {
         CSyntacticInfo target = new CSyntacticInfo();
         target.TotalSyntacticUnitsCount = 0;
      }
   }
}