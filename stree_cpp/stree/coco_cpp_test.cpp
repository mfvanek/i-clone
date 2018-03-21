#include "TestHarness.h"
#include "CExtensionFactory.h"
#include "filesystem.h"

using namespace source_files;
using namespace filesystem;

TEST(test1, coco_cpp)
{
   ILanguageExtension::Ptr cpp_ext = CExtensionFactory::Create(LANGUAGE_C_PLUS_PLUS);
   CTokenizeResult target(Path::Combine(Directory::Current(), "test_sample_1.c"));
   cpp_ext->Tokenize(target);
   LONGS_EQUAL(389, target.GetCollection().size());
}

TEST(test2, coco_cpp)
{
   ILanguageExtension::Ptr cpp_ext = CExtensionFactory::Create(LANGUAGE_C_PLUS_PLUS);
   CTokenizeResult target(Path::Combine(Directory::Current(), "test_sample_2.c"));
   cpp_ext->Tokenize(target);
   LONGS_EQUAL(1242, target.GetCollection().size());
}