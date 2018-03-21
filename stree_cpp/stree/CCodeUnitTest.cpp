#include "TestHarness.h"
#include "CCodeUnit.h"

using namespace source_files;

TEST(test1, CCodeUnit)
{
   CSourceFileID::Ptr src_file_id = CSourceFileID::Generate("some_file.cpp");
   std::string empty_str("");
   CCodeUnit target = CCodeUnit(src_file_id, CElementPosition(12, 12), empty_str);
   CHECK_EQUAL(target.GetText(), empty_str);
}

TEST(test2, CCodeUnit)
{
   CSourceFileID::Ptr src_file_id = CSourceFileID::Generate("some_file.cpp");
   CCodeUnit first = CCodeUnit(src_file_id, CElementPosition(12, 12), ")");
   LONGS_EQUAL(0, first.CompareTo(first));
   CHECK(!(first < first));

   CCodeUnit second = CCodeUnit(src_file_id, CElementPosition(12, 12), ")");
   LONGS_EQUAL(0, first.CompareTo(second));
   LONGS_EQUAL(0, second.CompareTo(first));
   CHECK(!(first < second));

   CCodeUnit third = CCodeUnit(src_file_id, CElementPosition(12, 12), ")");
   LONGS_EQUAL(0, first.CompareTo(third));
   LONGS_EQUAL(0, second.CompareTo(third));
   CHECK(!(first < third));

   second = CCodeUnit(src_file_id, CElementPosition(12, 12), ")!");
   LONGS_EQUAL(-1, first.CompareTo(second));
   LONGS_EQUAL(1, second.CompareTo(first));
   CHECK(!(first < second)); // Внимание! Учитывается только первый символ!
   CHECK(!(second < first)); // Внимание! Учитывается только первый символ!

   second = CCodeUnit(src_file_id, CElementPosition(12, 12, 15, 1234), ")");
   LONGS_EQUAL(-1, first.CompareTo(second));
   LONGS_EQUAL(1, second.CompareTo(first));
   CHECK(!(first < second)); // Внимание! Учитывается только первый символ!
   CHECK(!(second < first)); // Внимание! Учитывается только первый символ!

   second = CCodeUnit(src_file_id, CElementPosition(12, 12, 15, 1234), "A");
   LONGS_EQUAL(-1, first.CompareTo(second));
   LONGS_EQUAL(1, second.CompareTo(first));
   CHECK(first < second); // Внимание! Учитывается только первый символ!
   CHECK(!(second < first)); // Внимание! Учитывается только первый символ!
}

TEST(test3, CCodeUnit)
{
   CSourceFileID::Ptr src_file_id = CSourceFileID::Generate("some_file.cpp");
   CCodeUnit first = CCodeUnit(src_file_id, CElementPosition(12, 12), ")");
   CHECK(first.Equals(first));
   CHECK(first == first);

   CCodeUnit second = CCodeUnit(src_file_id, CElementPosition(12, 12), ")");
   CHECK(first.Equals(second));
   LONGS_EQUAL(first.Equals(second), second.Equals(first));
   CHECK(first == second);

   second = CCodeUnit(src_file_id, CElementPosition(12, 12), "else");
   CHECK(!first.Equals(second));
   CHECK(first != second);

   second = CCodeUnit(src_file_id, CElementPosition(1, 34), ")");
   CHECK(!second.Equals(first));
   CHECK(first == second); // Внимание! Учитывается только первый символ!

   second = CCodeUnit(CSourceFileID::Generate("some_file.cpp"), CElementPosition(12, 12), ")");
   CHECK(!second.Equals(first));
   CHECK(first == second); // Внимание! Учитывается только первый символ!

   second = CCodeUnit(src_file_id, CElementPosition(12, 12), ")if");
   CHECK(!second.Equals(first));
   CHECK(first == second); // Внимание! Учитывается только первый символ!
}

//TEST(test4, CCodeUnit)
//{
//   try
//   {
//      CSourceFileID::Ptr src_file_id = CSourceFileID::Generate("some_file.cpp");
//      CCodeUnit first = CCodeUnit(src_file_id, CElementPosition(12, 12), "else");
//      first.IsSimilar(first, -2.0);
//      FAIL("ExpectedException(typeof(InvalidSimilarityMeasureException))");
//   }
//   catch(const InvalidSimilarityMeasureException&)
//   {}
//}
//
//TEST(test5, CCodeUnit)
//{
//   CSourceFileID::Ptr src_file_id = CSourceFileID::Generate("some_file.cpp");
//   CCodeUnit first = CCodeUnit(src_file_id, CElementPosition(12, 12), "else");
//   first.IsSimilar(first, 1.0);
//}