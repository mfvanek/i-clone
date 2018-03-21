#include "TestHarness.h"
#include "CCodeFragment.h"

using namespace source_files;

static const int ITEMS_COUNT = 3;

static CCodeUnit first_item(const CSourceFileID::Ptr& file_id)
{
   return CCodeUnit(file_id, CElementPosition(12, 23), ")");
}

static CCodeUnit middle_item(const CSourceFileID::Ptr& file_id)
{
   return CCodeUnit(file_id, CElementPosition(13, 33), ")");
}

static CCodeUnit last_item(const CSourceFileID::Ptr& file_id)
{
   return CCodeUnit(file_id, CElementPosition(14, 43), ")");
}

static CCodeUnitsCollection CreateCollection(const CSourceFileID::Ptr& file_id)
{
   CCodeUnitsCollection target;
   target.add(first_item(file_id));
   target.add(middle_item(file_id));
   target.add(last_item(file_id));
   return target;
}

TEST(test1, CCodeFragment)
{
   CCodeFragment target;
   LONGS_EQUAL(0, target.size());
   LONGS_EQUAL(0, target.GetContent().size());
   CHECK(target.Equals(target));
   CHECK(target == target);
}

TEST(test2, CCodeFragment)
{
   CSourceFileID::Ptr file_id = CSourceFileID::Generate("some_file.cpp");
   CCodeFragment target(CreateCollection(file_id));
   LONGS_EQUAL(ITEMS_COUNT, target.size());
   LONGS_EQUAL(ITEMS_COUNT, target.GetContent().size());
   CHECK(target.Equals(target));
   CHECK(target == target);
}

TEST(test3, CCodeFragment)
{
   CSourceFileID::Ptr file_id = CSourceFileID::Generate("some_file.cpp");
   CCodeUnitsCollection collection = CreateCollection(file_id);
   CCodeFragment first(collection);
   CCodeFragment second(collection);
   CHECK(first.Equals(second));
   CHECK(first == second);

   collection.addRange(collection);
   second = CCodeFragment(collection);
   LONGS_EQUAL(2*ITEMS_COUNT, second.size());
   CHECK(!first.Equals(second));
   CHECK(first != second);
}

TEST(test4, CCodeFragment)
{
   CSourceFileID::Ptr file_id = CSourceFileID::Generate("some_file.cpp");
   CCodeFragment target(CreateCollection(file_id));
   LONGS_EQUAL(first_item(file_id).GetLineStart(), target.GetLineStart());
   LONGS_EQUAL(last_item(file_id).GetLineEnd(), target.GetLineEnd());
   LONGS_EQUAL(first_item(file_id).GetIndexStart(), target.GetIndexStart());
   LONGS_EQUAL(last_item(file_id).GetIndexEnd(), target.GetIndexEnd());
}

TEST(test5, CCodeFragment)
{
   try
   {
      CCodeFragment target;
      target.GetLineStart();
      FAIL("ExpectedException(typeof(std::out_of_range))");
   }
   catch(const std::out_of_range&)
   {}
}

TEST(test6, CCodeFragment)
{
   try
   {
      CCodeFragment target;
      target.GetLineEnd();
      FAIL("ExpectedException(typeof(std::out_of_range))");
   }
   catch(const std::out_of_range&)
   {}
}

TEST(test7, CCodeFragment)
{
   try
   {
      CCodeFragment target;
      target.GetIndexStart();
      FAIL("ExpectedException(typeof(std::out_of_range))");
   }
   catch(const std::out_of_range&)
   {}
}

TEST(test8, CCodeFragment)
{
   try
   {
      CCodeFragment target;
      target.GetIndexEnd();
      FAIL("ExpectedException(typeof(std::out_of_range))");
   }
   catch(const std::out_of_range&)
   {}
}

TEST(test9, CCodeFragment)
{
   CSourceFileID::Ptr file_id = CSourceFileID::Generate("some_file.cpp");
   CCodeUnitsCollection collection = CreateCollection(file_id);
   CCodeFragment target(collection);
   LONGS_EQUAL(ITEMS_COUNT, target.size());

   collection.addRange(collection);
   target.SetContent(collection);
   LONGS_EQUAL(2*ITEMS_COUNT, target.size());
   CHECK(target.GetContent() == collection);
}