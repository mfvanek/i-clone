#include "TestHarness.h"
#include "filesystem.h"

using namespace filesystem;

TEST(test1, get_files)
{
   files_collection target = get_files(Directory::Current(), "*.cs");
   LONGS_EQUAL(4, target.size());
   
   target = get_files(Directory::Current(), "*.cs", "MimeTypeDetection.cs");
   LONGS_EQUAL(3, target.size());

   target = get_files(Directory::Current(), "*.cs", "MimeTypeDetection.cs;UnitTestSampleFile.cs");
   LONGS_EQUAL(2, target.size());
}