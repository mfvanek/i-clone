using ISourceFilesLibrary.Classes.CodeFragment;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using ISourceFilesLibrary.Classes.CodeUnit;
using ICloneBaseLibrary.Classes;

namespace ICloneTestProject
{
   [TestClass()]
   public class CCommentedCodeFragmentTest
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
      public void IsBelongOneLineTest1()
      {
         CCommentedCodeFragment target = new CCommentedCodeFragment(new CElementPosition(12, 123), new CPair<string>("/*", "*/"));
         Assert.AreEqual(true, target.IsBelongOneLine());
      }

      [TestMethod()]
      public void IsBelongOneLineTest2()
      {
         CCommentedCodeFragment target = new CCommentedCodeFragment(new CElementPosition(12, 123), new CPair<string>("/*", "*/"));
         Assert.AreEqual(true, target.IsBelongOneLine(12));
         Assert.AreEqual(false, target.IsBelongOneLine(11));
      }

      [TestMethod()]
      public void SetEndingTest1()
      {
         CCommentedCodeFragment target = new CCommentedCodeFragment(new CElementPosition(12, 123), new CPair<string>("/*", "*/"));

         long _LineEnd = 45;
         int _IndexEnd = 3453;
         target.SetEnding(_LineEnd, _IndexEnd);
         Assert.AreEqual(_LineEnd, target.LineEnd);
         Assert.AreEqual(_IndexEnd, target.IndexEnd);
      }

      [TestMethod()]
      public void CommentSymbolPairTest()
      {
         CPair<string> CommentPair = new CPair<string>("/*", "*/");
         CCommentedCodeFragment target = new CCommentedCodeFragment(new CElementPosition(12, 123), CommentPair);
         Assert.AreEqual(CommentPair, target.CommentSymbolPair);
         
         CommentPair = new CPair<string>("//", string.Empty);
         target.CommentSymbolPair = CommentPair;
         Assert.AreEqual(CommentPair, target.CommentSymbolPair);
      }

      //[TestMethod()]
      //public void IndexEndTest()
      //{
      //   CElementPosition _Position = null; // TODO: Initialize to an appropriate value
      //   CPair<string> CommentPair = null; // TODO: Initialize to an appropriate value
      //   CCommentedCodeFragment target = new CCommentedCodeFragment(_Position, CommentPair); // TODO: Initialize to an appropriate value
      //   int expected = 0; // TODO: Initialize to an appropriate value
      //   int actual;
      //   target.IndexEnd = expected;
      //   actual = target.IndexEnd;
      //   Assert.AreEqual(expected, actual);
      //   Assert.Inconclusive("Verify the correctness of this test method.");
      //}

      //[TestMethod()]
      //public void IndexStartTest()
      //{
      //   CElementPosition _Position = null; // TODO: Initialize to an appropriate value
      //   CPair<string> CommentPair = null; // TODO: Initialize to an appropriate value
      //   CCommentedCodeFragment target = new CCommentedCodeFragment(_Position, CommentPair); // TODO: Initialize to an appropriate value
      //   int expected = 0; // TODO: Initialize to an appropriate value
      //   int actual;
      //   target.IndexStart = expected;
      //   actual = target.IndexStart;
      //   Assert.AreEqual(expected, actual);
      //   Assert.Inconclusive("Verify the correctness of this test method.");
      //}

      //[TestMethod()]
      //public void LineEndTest()
      //{
      //   CElementPosition _Position = null; // TODO: Initialize to an appropriate value
      //   CPair<string> CommentPair = null; // TODO: Initialize to an appropriate value
      //   CCommentedCodeFragment target = new CCommentedCodeFragment(_Position, CommentPair); // TODO: Initialize to an appropriate value
      //   long expected = 0; // TODO: Initialize to an appropriate value
      //   long actual;
      //   target.LineEnd = expected;
      //   actual = target.LineEnd;
      //   Assert.AreEqual(expected, actual);
      //   Assert.Inconclusive("Verify the correctness of this test method.");
      //}

      //[TestMethod()]
      //public void LineStartTest()
      //{
      //   CElementPosition _Position = null; // TODO: Initialize to an appropriate value
      //   CPair<string> CommentPair = null; // TODO: Initialize to an appropriate value
      //   CCommentedCodeFragment target = new CCommentedCodeFragment(_Position, CommentPair); // TODO: Initialize to an appropriate value
      //   long expected = 0; // TODO: Initialize to an appropriate value
      //   long actual;
      //   target.LineStart = expected;
      //   actual = target.LineStart;
      //   Assert.AreEqual(expected, actual);
      //   Assert.Inconclusive("Verify the correctness of this test method.");
      //}
   }
}