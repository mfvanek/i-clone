#include "TestHarness.h"
#include "CElementPosition.h"

using namespace source_files;

TEST(test1, CElementPosition)
{
   long line_start = 1;
   int index_start = 0;
   long line_end = 45;
   int index_end = 345;
   CElementPosition target(line_start, index_start, line_end, index_end);

   LONGS_EQUAL(target.GetLineStart(), line_start);
   LONGS_EQUAL(target.GetLineEnd(), line_end);
   LONGS_EQUAL(target.GetIndexStart(), index_start);
   LONGS_EQUAL(target.GetIndexEnd(), index_end);
}

TEST(test2, CElementPosition)
{
   long line_start = 1;
   int index_start = 0;
   CElementPosition target(line_start, index_start);
   LONGS_EQUAL(target.GetLineStart(), line_start);
   LONGS_EQUAL(target.GetLineEnd(), line_start);
   LONGS_EQUAL(target.GetIndexStart(), index_start);
   LONGS_EQUAL(target.GetIndexEnd(), index_start);
}

TEST(test3, CElementPosition)
{
   try
   {
      long line_start = -1;
      int index_start = 0;
      CElementPosition target(line_start, index_start);
      FAIL("ExpectedException(typeof(InvalidElementPositionException))");
   }
   catch(const InvalidElementPositionException&)
   {}
}

TEST(test4, CElementPosition)
{
   try
   {
      long line_start = 11;
      int index_start = -1;
      CElementPosition target(line_start, index_start);
      FAIL("ExpectedException(typeof(InvalidElementPositionException))");
   }
   catch(const InvalidElementPositionException&)
   {}
}

TEST(test5, CElementPosition)
{
   try
   {
      long line_start = 12;
      int index_start = 0;
      long line_end = 11;
      int index_end = 345;
      CElementPosition target(line_start, index_start, line_end, index_end);
      FAIL("ExpectedException(typeof(InvalidElementPositionException))");
   }
   catch(const InvalidElementPositionException&)
   {}
}

TEST(test6, CElementPosition)
{
   try
   {
      long line_start = 12;
      int index_start = 23;
      long line_end = 12;
      int index_end = 22;
      CElementPosition target(line_start, index_start, line_end, index_end);
      FAIL("ExpectedException(typeof(InvalidElementPositionException))");
   }
   catch(const InvalidElementPositionException&)
   {}
}

TEST(test7, CElementPosition)
{
   long line_start = 999999999;
   int index_start = 999999999;
   CElementPosition first = CElementPosition(line_start, index_start);
   LONGS_EQUAL(first.CompareTo(first), 0);
   CHECK(!(first < first));

   CElementPosition second = CElementPosition(line_start, index_start);
   LONGS_EQUAL(first.CompareTo(second), 0);
   CHECK(!(first < second));

   second = CElementPosition(line_start + 1, index_start);
   LONGS_EQUAL(first.CompareTo(second), 0);
   CHECK(!(second < first));

   second = CElementPosition(line_start, index_start, line_start + 1, index_start);
   LONGS_EQUAL(first.CompareTo(second), -1);
   CHECK(first < second);
   CHECK(!(second < first));
}

TEST(test8, CElementPosition)
{
   long line_start = 999999999;
   int index_start = 999999999;
   CElementPosition first = CElementPosition(line_start, index_start);
   CHECK(first.Equals(first));
   CHECK(first == first);

   CElementPosition second = CElementPosition(line_start, index_start);
   CHECK(first.Equals(second));
   CHECK(first == second);

   second = CElementPosition(line_start + 1, index_start);
   CHECK(!first.Equals(second));
   CHECK(first != second);
}