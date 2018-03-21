#include "TestHarness.h"
#include "CCodeUnitsCollection.h"

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

TEST(test1, CCodeUnitsCollection)
{
   CCodeUnitsCollection target;
   LONGS_EQUAL(0, target.size());
}

TEST(test2, CCodeUnitsCollection)
{
   CSourceFileID::Ptr file_id = CSourceFileID::Generate("some_file.cpp");
   CCodeUnitsCollection target;
   CCodeUnit item(file_id, CElementPosition(12, 23), ")");
   target.add(item);
   LONGS_EQUAL(1, target.size());
   target.add(item);
   LONGS_EQUAL(2, target.size());
}

TEST(test3, CCodeUnitsCollection)
{
   CSourceFileID::Ptr file_id = CSourceFileID::Generate("some_file.cpp");
   CCodeUnitsCollection target = CreateCollection(file_id);
   LONGS_EQUAL(ITEMS_COUNT, target.size());
   CHECK(target[0].Equals(first_item(file_id)));
   CHECK(target[ITEMS_COUNT-1].Equals(last_item(file_id)));
}

TEST(test4, CCodeUnitsCollection)
{
   try
   {
      CCodeUnitsCollection target;
      LONGS_EQUAL(0, target.size());
      CCodeUnit item = target[0];
      FAIL("ExpectedException(typeof(std::out_of_range))");
   }
   catch(const std::out_of_range&)
   {}
}

TEST(test5, CCodeUnitsCollection)
{
   try
   {
      CCodeUnitsCollection target;
      LONGS_EQUAL(0, target.size());
      CCodeUnit unit = target.front();
      FAIL("ExpectedException(typeof(std::out_of_range))");
   }
   catch(const std::out_of_range&)
   {}
}

TEST(test6, CCodeUnitsCollection)
{
   CSourceFileID::Ptr file_id = CSourceFileID::Generate("some_file.cpp");
   CCodeUnitsCollection target = CreateCollection(file_id);
   CHECK(target.back().Equals(last_item(file_id)));
}

TEST(test7, CCodeUnitsCollection)
{
   try
   {
      CCodeUnitsCollection target;
      LONGS_EQUAL(0, target.size());
      CCodeUnit unit = target.back();
      FAIL("ExpectedException(typeof(std::out_of_range))");
   }
   catch(const std::out_of_range&)
   {}
}

TEST(test8, CCodeUnitsCollection)
{
   CSourceFileID::Ptr file_id = CSourceFileID::Generate("some_file.cpp");
   CCodeUnitsCollection target = CreateCollection(file_id);
   LONGS_EQUAL(ITEMS_COUNT, target.size());
   target.addRange(CreateCollection(file_id));
   LONGS_EQUAL(2*ITEMS_COUNT, target.size());
}

TEST(test9, CCodeUnitsCollection)
{
   CCodeUnitsCollection first;
   CHECK(first.Equals(first));
   CHECK(first == first);

   CCodeUnitsCollection second;
   CHECK(first.Equals(second));
   CHECK(first == second);
}

TEST(test10, CCodeUnitsCollection)
{
   CSourceFileID::Ptr file_id = CSourceFileID::Generate("some_file.cpp");
   CCodeUnitsCollection first = CreateCollection(file_id);
   CCodeUnitsCollection second;
   CHECK(!first.Equals(second));
   CHECK(first != second);

   second.add(first_item(file_id));
   CHECK(first != second);

   second.add(middle_item(file_id));
   CHECK(first != second);

   second.add(last_item(file_id));
   CHECK(first == second);

   second.add(last_item(file_id));
   CHECK(first != second);
}