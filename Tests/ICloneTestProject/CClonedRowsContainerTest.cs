using System.Collections.Generic;
using ICloneSearchLibrary.Classes.ClonedRowsMatrix;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ISourceFilesLibrary.Classes.CodeUnit;

namespace ICloneTestProject
{

   /// <summary>
   ///This is a test class for CClonedRowsContainerTest and is intended
   ///to contain all CClonedRowsContainerTest Unit Tests
   ///</summary>
   [TestClass()]
   public class CClonedRowsContainerTest
   {
      private TestContext testContextInstance;
      private const long FileID = long.MaxValue;

      /// <summary>
      ///Gets or sets the test context which provides
      ///information about and functionality for the current test run.
      ///</summary>
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
      public void CClonedRowsContainerConstructorTest()
      {
         CClonedRowsContainer target = new CClonedRowsContainer();
         Assert.IsNotNull(target);
      }

      private void AddAndTestValue(ref CClonedRowsContainer target, CExtendedCodeUnit item, int number)
      {
         Assert.IsNotNull(item);
         target.Add(item);

         List<CExtendedCodeUnit> rows = target.GetEqualRows(item);
         Assert.AreEqual(rows.Count, number);
         Assert.AreEqual(rows[number - 1], item);
      }

      /// <summary>
      ///A test for Add
      ///</summary>
      [TestMethod()]
      public void AddTest1()
      {
         const int top_limit = 111;

         CClonedRowsContainer target = new CClonedRowsContainer();
         for (long counter = CElementPosition.LINE_NUMBER_LOW_BOUND; counter < top_limit; ++counter)
         {
            CExtendedCodeUnit item = new CExtendedCodeUnit(new CCodeUnit(new CElementPosition(counter, 0), counter.ToString()), FileID, counter);
            AddAndTestValue(ref target, item, 1);
         }

         Assert.AreEqual(target.Count, top_limit - 1);
      }

      /// <summary>
      ///A test for Add
      ///</summary>
      [TestMethod()]
      public void AddTest2()
      {
         const int max_iteration = 111;
         int first_row = int.MaxValue / 2;
         int second_row = int.MaxValue;
         CClonedRowsContainer target = new CClonedRowsContainer();

         for (long counter = CElementPosition.LINE_NUMBER_LOW_BOUND; counter < max_iteration; counter++)
         {
            CExtendedCodeUnit item1 = new CExtendedCodeUnit(new CCodeUnit(new CElementPosition(counter, 0), first_row.ToString()), FileID, counter);//(counter, first_row.ToString(), FileID, counter);
            AddAndTestValue(ref target, item1, (int)counter);

            CExtendedCodeUnit item2 = new CExtendedCodeUnit(new CCodeUnit(new CElementPosition(counter, 0), second_row.ToString()), FileID, counter);//(counter, second_row.ToString(), FileID, counter);
            AddAndTestValue(ref target, item2, (int)counter);
         }

         Assert.AreEqual(target.Count, 2);
      }
   }
}