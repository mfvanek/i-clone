//using System;
//using IClone.ICloneReport.Classes;
//using ISourceFilesLibrary.Classes.CodeFragment;
//using ISourceFilesLibrary.Classes.SourceRow;
//using ISourceFilesLibrary.Classes.SourceRows;
//using Microsoft.VisualStudio.TestTools.UnitTesting;

//namespace ICloneTestProject
//{
//   /// <summary>
//   ///This is a test class for CRelatedCloneFragmentsTest and is intended
//   ///to contain all CRelatedCloneFragmentsTest Unit Tests
//   ///</summary>
//   [TestClass()]
//   public class CRelatedCloneFragmentsTest
//   {
//      //private long LineStart = 34;
//      //private int IndexStart = 2;
//      //long LineEnd = 3334;
//      //int IndexEnd = 223;
//      private TestContext testContextInstance;

//      /// <summary>
//      ///Gets or sets the test context which provides
//      ///information about and functionality for the current test run.
//      ///</summary>
//      public TestContext TestContext
//      {
//         get
//         {
//            return testContextInstance;
//         }
//         set
//         {
//            testContextInstance = value;
//         }
//      }

//      CRelatedCloneFragments BaseTest()
//      {
//         CCodeFragment _FirstFragment = null;// new CCodeFragment(LineStart, IndexStart);
//         CCodeFragment _SecondFragment = null;// new CCodeFragment(LineStart, IndexStart);
//         CSimilarity _Similarity = new CSimilarity(0.345D);
//         CRelatedCloneFragments target = new CRelatedCloneFragments(_FirstFragment, _SecondFragment, _Similarity);
//         Assert.AreEqual(target.FirstFragment, _FirstFragment);
//         Assert.AreEqual(target.SecondFragment, _SecondFragment);
//         Assert.AreEqual(target.Similarity, _Similarity);

//         return target;
//      }

//      /// <summary>
//      ///A test for CRelatedCloneFragments Constructor
//      ///</summary>
//      [TestMethod()]
//      [ExpectedException(typeof(ArgumentNullException))]
//      public void CRelatedCloneFragmentsConstructorTest()
//      {
//         CRelatedCloneFragments target = BaseTest();
//         target = null;// new CRelatedCloneFragments(new CCodeFragment(LineStart, IndexStart), null, new CSimilarity());
//      }

//      [TestMethod()]
//      [ExpectedException(typeof(ArgumentNullException))]
//      public void CRelatedCloneFragmentsConstructorTest1()
//      {
//         CCodeFragment _FirstFragment = null;//new CCodeFragment(LineStart, IndexStart);
//         CCodeFragment _SecondFragment = null;//new CCodeFragment(LineStart, IndexStart);
//         CRelatedCloneFragments target = new CRelatedCloneFragments(_FirstFragment, _SecondFragment);
//         Assert.AreEqual(target.FirstFragment, _FirstFragment);
//         Assert.AreEqual(target.SecondFragment, _SecondFragment);
//         Assert.AreEqual(target.Similarity, new CSimilarity());

//         target = new CRelatedCloneFragments(null, _SecondFragment);
//      }

//      [TestMethod()]
//      public void FirstFragmentTest()
//      {
//         CRelatedCloneFragments target = null;//new CRelatedCloneFragments(new CCodeFragment(LineStart, IndexStart), new CCodeFragment(LineStart, IndexStart));
//         CCodeFragment expected = null;//new CCodeFragment(LineStart, IndexStart, LineEnd, IndexEnd);
//         Assert.AreNotEqual(expected, target.FirstFragment);
//         target.FirstFragment = expected;
//         Assert.AreEqual(expected, target.FirstFragment);

//         CBaseSourceRows content = new CBaseSourceRows();
//         content.Add(new CBaseSourceRow(12, "fsdfsdfsd", 123));
//         expected = null;//new CCodeFragment(LineStart, IndexStart, LineEnd, IndexEnd, content);
//         Assert.AreNotEqual(expected, target.FirstFragment);
//         target.FirstFragment = expected;
//         Assert.AreEqual(expected, target.FirstFragment);
//      }

//      [TestMethod()]
//      public void SecondFragmentTest()
//      {
//         CRelatedCloneFragments target = new CRelatedCloneFragments(new CCodeFragment(), new CCodeFragment(LineStart, IndexStart));
//         CCodeFragment expected = null;//new CCodeFragment(LineStart, IndexStart, LineEnd, IndexEnd);
//         Assert.AreNotEqual(expected, target.SecondFragment);
//         target.SecondFragment = expected;
//         Assert.AreEqual(expected, target.SecondFragment);
//      }

//      /// <summary>
//      ///A test for Similarity
//      ///</summary>
//      [TestMethod()]
//      public void SimilarityTest()
//      {
//         CRelatedCloneFragments target = null;//new CRelatedCloneFragments(new CCodeFragment(LineStart, IndexStart), new CCodeFragment(LineStart, IndexStart));
//         CSimilarity expected = new CSimilarity();
//         Assert.AreEqual(expected, target.Similarity);

//         expected = new CSimilarity(0.444543783);
//         Assert.AreNotEqual(expected, target.Similarity);
//         target.Similarity = expected;
//         Assert.AreEqual(expected, target.Similarity);
//      }
//   }
//}