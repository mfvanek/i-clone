#include "TestHarness.h"
#include "CSourceFileID.h"

using namespace source_files;

TEST(test1, CSourceFileID)
{
   std::string fileName = "some_file_name.cpp";
   CSourceFileID::Ptr file_id = CSourceFileID::Generate(fileName);
   CHECK_EQUAL(fileName, file_id->GetSourceFileName());
   LONGS_EQUAL(1, file_id->GetSourceFileID());

   CSourceFileID::Ptr file_id_2 = CSourceFileID::Generate(fileName);
   LONGS_EQUAL(2, file_id_2->GetSourceFileID());

   CSourceFileID::Ptr file_id_3 = CSourceFileID::Generate(fileName);
   LONGS_EQUAL(3, file_id_3->GetSourceFileID());

   file_id = nullptr;

   CSourceFileID::Ptr file_id_4 = CSourceFileID::Generate(fileName);
   LONGS_EQUAL(1, file_id_4->GetSourceFileID());

   file_id = CSourceFileID::Generate(fileName);
   LONGS_EQUAL(4, file_id->GetSourceFileID());
}