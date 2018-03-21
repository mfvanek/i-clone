#include "TestHarness.h"
#include "CExtensionFactory.h"
#include "filesystem.h"

using namespace source_files;
using namespace filesystem;

TEST(test1, coco_cs)
{
   ILanguageExtension::Ptr cs_ext = CExtensionFactory::Create(LANGUAGE_C_SHARP);
   CTokenizeResult target(Path::Combine(Directory::Current(), "MimeTypeDetection.cs"));
   cs_ext->Tokenize(target);
   LONGS_EQUAL(796, target.GetCollection().size());
}

TEST(test2, coco_cs)
{
   ILanguageExtension::Ptr cs_ext = CExtensionFactory::Create(LANGUAGE_C_SHARP);
   CTokenizeResult target(Path::Combine(Directory::Current(), "UnitTestSampleFile.cs"));
   cs_ext->Tokenize(target);
   LONGS_EQUAL(6483, target.GetCollection().size());
}

TEST(test3, coco_cs)
{
   ILanguageExtension::Ptr cs_ext = CExtensionFactory::Create(LANGUAGE_C_SHARP);
   CTokenizeResult target(Path::Combine(Directory::Current(), "UnitTestSampleFile2.cs"));
   cs_ext->Tokenize(target);
   LONGS_EQUAL(350, target.GetCollection().size());
}

TEST(test4, coco_cs)
{
   ILanguageExtension::Ptr cs_ext = CExtensionFactory::Create(LANGUAGE_C_SHARP);
   CTokenizeResult target(Path::Combine(Directory::Current(), "UnitTestSampleFile3.cs"));
   cs_ext->Tokenize(target);
   LONGS_EQUAL(67, target.GetCollection().size());
}