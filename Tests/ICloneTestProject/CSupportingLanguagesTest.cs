using ICloneExtensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using ISourceFilesLibrary.Classes.Languages;

namespace ICloneTestProject
{
    [TestClass()]
    public class CSupportingLanguagesTest
    {
        [TestMethod()]
        public void DllExtentionNameTest()
        {
            string actual = CSupportingLanguages.DllExtentionName(LANGUAGES.LANGUAGE_JAVA);
            Assert.AreEqual("java_iclone_ext.dll", actual);

            actual = CSupportingLanguages.DllExtentionName(LANGUAGES.LANGUAGE_UNKNOWN);
            Assert.AreEqual(string.Empty, actual);
        }

        /// <summary>
        ///A test for GetLanguageIDByLanguageName
        ///</summary>
        [TestMethod()]
        public void GetLanguageIDByLanguageNameTest()
        {
            Assert.AreEqual(LANGUAGES.LANGUAGE_JAVA, CSupportingLanguages.GetLanguageIDByLanguageName("JAVA"));
            Assert.AreEqual(LANGUAGES.LANGUAGE_UNKNOWN, CSupportingLanguages.GetLanguageIDByLanguageName("Java1"));
        }

        /// <summary>
        ///A test for LanguageID
        ///</summary>
        [TestMethod()]
        public void LanguageIDTest()
        {
            LANGUAGES actual = CSupportingLanguages.LanguageID("java_iclone_ext.dll");
            Assert.AreEqual(LANGUAGES.LANGUAGE_JAVA, actual);

            actual = CSupportingLanguages.LanguageID("CS_Iclone_eXt.DLL");
            Assert.AreEqual(LANGUAGES.LANGUAGE_C_SHARP, actual);

            actual = CSupportingLanguages.LanguageID("java_iclone_ext1.dll");
            Assert.AreEqual(LANGUAGES.LANGUAGE_UNKNOWN, actual);

            actual = CSupportingLanguages.LanguageID("java1_iclone_ext.dll");
            Assert.AreEqual(LANGUAGES.LANGUAGE_UNKNOWN, actual);

            actual = CSupportingLanguages.LanguageID("basic_iclone_ext.dll");
            Assert.AreEqual(LANGUAGES.LANGUAGE_UNKNOWN, actual);
        }

        /// <summary>
        ///A test for LanguageName
        ///</summary>
        [TestMethod()]
        public void LanguageNameTest()
        {
            string actual = CSupportingLanguages.LanguageName(LANGUAGES.LANGUAGE_C_PLUS_PLUS);
            Assert.AreEqual("C++", actual);

            actual = CSupportingLanguages.LanguageName(LANGUAGES.LANGUAGE_UNKNOWN);
            Assert.AreEqual(string.Empty, actual);
        }

        /// <summary>
        ///A test for LanguagePrefix
        ///</summary>
        [TestMethod()]
        public void LanguagePrefixTest()
        {
            string actual = CSupportingLanguages.LanguagePrefix(LANGUAGES.LANGUAGE_C);
            Assert.AreEqual("c", actual);

            actual = CSupportingLanguages.LanguagePrefix(LANGUAGES.LANGUAGE_UNKNOWN);
            Assert.AreEqual(string.Empty, actual);
        }
    }
}
