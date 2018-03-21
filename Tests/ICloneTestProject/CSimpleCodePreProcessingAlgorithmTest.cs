using CodeLoadingLibrary.Classes;
using ISourceFilesLibrary.Classes.CodeUnit;
using ISourceFilesLibrary.Classes.CodeUnitsCollection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ICloneTestProject
{
   [TestClass()]
   public class CSimpleCodePreProcessingAlgorithmTest
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

      private string[] original_code =
{
"start 1. ==============",//1
"//*",//2
"int a = 5;          not        commented                 // commented",//3
"//*/",//4
"2. ==============",
"/* commented */ not       commented /* again commented */ not           commented /* again commented */",
"3. ==============",
"/*",//8
"commented   int a = 5; // commented",
"//*/",
"4. ==============",//11
"not commented /* commented",
"commented",
"commented",
"commented */ not commented",
"5. ==============",//16
"not commented /* commented",
"commented",
"commented",
"commented */",
"6. ============== end"//21
};

      private string[] processed_code =
{
"start 1. ==============",//1
"int a = 5;          not        commented                 ",
"2. ==============",
" not       commented  not           commented ",
"3. ==============",
"4. ==============",
"not commented ",
" not commented",
"5. ==============",
"not commented ",
"6. ============== end"//11
};

      private CCodeUnitsCollection CreateTestCollection1()
      {
         CCodeUnitsCollection collect = new CCodeUnitsCollection();
         for (int counter = 0; counter < original_code.Length; ++counter)
         {
            collect.Add(new CExtendedCodeUnit(new CCodeUnit(new CElementPosition(counter + 1, 0, counter + 1, original_code[counter].Length), original_code[counter]), 1, counter + 1));
         }
         return collect;
      }

      private CCodeUnitsCollection CreateExpectedCollection1()
      {
         CCodeUnitsCollection collect = new CCodeUnitsCollection();
         for (int counter = 0; counter < processed_code.Length; ++counter)
         {
            collect.Add(new CExtendedCodeUnit(new CCodeUnit(new CElementPosition(counter + 1, 0, counter + 1, processed_code[counter].Length), processed_code[counter]), 1, counter + 1));
         }
         return collect;
      }

      [TestMethod()]
      public void PreProcessingTest1()
      {
         CSimpleCodePreProcessingAlgorithm target = new CSimpleCodePreProcessingAlgorithm();
         CCodePreProcessingOptions PreProcessingOptions = new CCodePreProcessingOptions("//;/* */;///");
         PreProcessingOptions.SetDeleteWhiteSpaces(false);

         CCodeUnitsCollection OriginalCodeUnitsCollection = CreateTestCollection1();
         Assert.AreEqual(OriginalCodeUnitsCollection.Size(), 21);

         CCodeUnitsCollection expected = CreateExpectedCollection1();
         Assert.AreEqual(expected.Size(), 11);

         CCodeUnitsCollection actual = target.PreProcessing(PreProcessingOptions, OriginalCodeUnitsCollection);
         Assert.AreEqual(actual.Size(), expected.Size());

         for (int counter = 0; counter < actual.Size(); ++counter)
         {
            Assert.AreEqual(actual[counter].Text, expected[counter].Text);
         }
      }

      [TestMethod()]
      public void CodePreProcessingAlgorithmNameTest()
      {
         CSimpleCodePreProcessingAlgorithm target = new CSimpleCodePreProcessingAlgorithm();
         string expected = "SimpleCodePreProcessingAlgorithm";
         Assert.AreEqual(expected, target.CodePreProcessingAlgorithmName());
      }
   }
}