using ICloneExtensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using ICloneExtensions.Interfaces;
using System.Collections.Generic;
using ISourceFilesLibrary.Classes.Languages;

namespace ICloneTestProject
{
   [TestClass()]
   public class CAvailableExtentionsTest
   {
      [TestMethod()]
      public void GetExtentionsListTest()
      {
         const int DllCount = 2;
         Dictionary<LANGUAGES, ICloneExtension> actual = CAvailableExtentions.GetExtentionsList();
         Assert.AreEqual(DllCount, actual.Count);

         foreach (var extension in actual.Values)
         {
            Assert.AreNotEqual(string.Empty, extension.GetCommentSymbols());
            Assert.AreNotEqual(string.Empty, extension.GetSourceFileExtentions());
         }
      }

      [TestMethod()]
      public void GetExtentionTest1()
      {
         LANGUAGES Lang = LANGUAGES.LANGUAGE_C_PLUS_PLUS;
         ICloneExtension actual = CAvailableExtentions.GetExtention(Lang);
         Assert.IsNotNull(actual);
         Assert.AreEqual(actual.LanguageID(), Lang);

         Lang = LANGUAGES.LANGUAGE_C_SHARP;
         actual = CAvailableExtentions.GetExtention(Lang);
         Assert.IsNotNull(actual);
         Assert.AreEqual(actual.LanguageID(), Lang);
      }

      [TestMethod()]
      [ExpectedException(typeof(UnknownLanguageException))]
      public void GetExtentionTest2()
      {
         ICloneExtension actual = CAvailableExtentions.GetExtention(LANGUAGES.LANGUAGE_UNKNOWN);
      }

      [TestMethod()]
      [ExpectedException(typeof(UnknownLanguageException))]
      public void GetExtentionTest3()
      {
         ICloneExtension actual = CAvailableExtentions.GetExtention(LANGUAGES.LANGUAGE_C);
      }

      [TestMethod()]
      [ExpectedException(typeof(UnknownLanguageException))]
      public void GetExtentionTest4()
      {
         ICloneExtension actual = CAvailableExtentions.GetExtention(LANGUAGES.LANGUAGE_JAVA);
      }
   }
}