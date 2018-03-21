using ISourceFilesLibrary.Classes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

using ICloneExtensions.Cpp;
using CodeLoadingLibrary.Classes;

namespace ICloneTestProject
{
    /// <summary>
    ///This is a test class for CModifiableSourceFileTest and is intended
    ///to contain all CModifiableSourceFileTest Unit Tests
    ///</summary>
    [TestClass()]
    public class CModifiableSourceFileTest
    {
        private const string TestCodeOriginal = @"/* adler32.c -- compute the Adler-32 checksum of a data stream
 * Copyright (C) 1995-2004 Mark Adler
 * For conditions of distribution and use, see copyright notice in zlib.h
 */

/* @(#) $Id$ */

/* NMAX is the largest n such that 255n(n+1)/2 + (n+1)(BASE-1) <= 2^32-1 */
/* ========================================================================= */
uLong ZEXPORT adler32(uLong adler, const Bytef *buf, uInt len)
{
    unsigned long sum2;
    unsigned n;

    /* initial Adler-32 value (deferred check for len == 1 speed) */
    if (buf == Z_NULL)
        return 1L;

    /* do length NMAX blocks -- requires just one modulo operation */
    while (len >= NMAX) {
        len -= NMAX;
        n = NMAX / 16;          /* NMAX is divisible by 16 */
        do {
            DO16(buf);          /* 16 sums unrolled */
            buf += 16;
        } while (--n);
        MOD(adler);
        MOD(sum2);
    }

	//*
 int       a =          5; // not commented
//*/

/*
int            a = 5; // commented
//*/

    /* return recombined sums */
    return adler | (sum2 << 16);
}

/* ========================================================================= */
";

        private const string TestCodeExpected = @"uLong ZEXPORT adler32(uLong adler, const Bytef *buf, uInt len)
{
unsigned long sum2;
unsigned n;
if (buf == Z_NULL)
return 1L;
while (len >= NMAX) {
len -= NMAX;
n = NMAX / 16;
do {
DO16(buf);
buf += 16;
} while (--n);
MOD(adler);
MOD(sum2);
}
int a = 5;
return adler | (sum2 << 16);
}";
        private TestContext testContextInstance;

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

        ///// <summary>
        /////A test for PreProcessing
        /////</summary>
        //[TestMethod()]
        //public void PreProcessingTest()
        //{
        //    CModifiableSourceFile target = new CModifiableSourceFile();
        //    Assert.IsNotNull(target);

        //    target.LargeString = TestCodeOriginal;
        //    Assert.AreEqual(target.LargeString, TestCodeOriginal);

        //    CCodePreProcessingStrategy PreProcessingStrategy = new CSimpleCodePreProcessingAlgorithm();
        //    string CommentSymbols = (new CCppICloneExtension()).GetCommentSymbols();
        //    target.PreProcessing(PreProcessingStrategy, CommentSymbols);

        //    CModifiableSourceFile expected = new CModifiableSourceFile();
        //    Assert.IsNotNull(expected);
        //    expected.LargeString = TestCodeExpected;
        //    Assert.AreEqual(expected.LargeString, TestCodeExpected);
        //    expected.PreProcessing(PreProcessingStrategy, CommentSymbols);

        //    Assert.AreEqual(expected.LargeString, target.LargeString);
        //}

        ///// <summary>
        /////A test1 for PreProcessing
        /////</summary>
        //[TestMethod()]
        //public void PreProcessingTest1()
        //{
        //    CModifiableSourceFile target = new CModifiableSourceFile();
        //    Assert.IsNotNull(target);

        //    target.LargeString = TestCodeOriginal;
        //    Assert.AreEqual(target.LargeString, TestCodeOriginal);

        //    CCodePreProcessingStrategy PreProcessingStrategy = new CSimpleCodePreProcessingAlgorithm();
        //    string CommentSymbols = (new CCppICloneExtension()).GetCommentSymbols();
        //    CCodePreProcessingOptions Options = new CCodePreProcessingOptions(CommentSymbols);
        //    Assert.IsNotNull(Options);

        //    target.PreProcessing(PreProcessingStrategy, Options);

        //    CModifiableSourceFile expected = new CModifiableSourceFile();
        //    Assert.IsNotNull(expected);
        //    expected.LargeString = TestCodeExpected;
        //    Assert.AreEqual(expected.LargeString, TestCodeExpected);
        //    expected.PreProcessing(PreProcessingStrategy, CommentSymbols);

        //    Assert.AreEqual(expected.LargeString, target.LargeString);
        //}
    }
}
