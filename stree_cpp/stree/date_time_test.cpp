#include "TestHarness.h"
#include "date_time.h"

using namespace temporal;

TEST(test1, date_time)
{
   CTime target(3723004);
   LONGS_EQUAL(1, target.Hour());
   LONGS_EQUAL(2, target.Minute());
   LONGS_EQUAL(3, target.Second());
   LONGS_EQUAL(4, target.Millisecond());
   CHECK_EQUAL("01:02:03", target.ToString());
}

TEST(test2, date_time)
{
   CDate target(1, 2, 3);
   LONGS_EQUAL(1, target.Day());
   LONGS_EQUAL(2, target.Month());
   LONGS_EQUAL(3, target.Year());
   CHECK_EQUAL("01.02.0003", target.ToString());
}