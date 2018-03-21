#include "TestHarness.h"
#include "repeats_supermax_template.h"
#include "CCodeUnit.h"

using namespace source_files;

static std::vector<CCodeUnit> build_test_data(const char* str)
{
   CSourceFileID::Ptr file_id = CSourceFileID::Generate("some_file.cpp");
   std::vector<CCodeUnit> result;
   for(size_t i = 0; i < strlen(str); ++i)
   {
      result.push_back(CCodeUnit(file_id, CElementPosition(1, 1), std::string(1, str[i])));
   }
   return result;
}

TEST(template_test1, supermax_find_code_unit_)
{
   char *_str = "abcd789xyzcd78123ccdab";
   supermax_node::nodes clones = supermax_find(build_test_data(_str), 1);
   LONGS_EQUAL(2, clones.size());

   LONGS_EQUAL(2, clones.front().M);
   LONGS_EQUAL(2, clones.front().num_leaves);
   LONGS_EQUAL(2, clones.front().num_witness);
   LONGS_EQUAL(0, clones.front().pos[0]);
   LONGS_EQUAL(20, clones.front().pos[1]);

   LONGS_EQUAL(4, clones.back().M);
   LONGS_EQUAL(2, clones.back().num_leaves);
   LONGS_EQUAL(2, clones.front().num_witness);
   LONGS_EQUAL(10, clones.back().pos[0]);
   LONGS_EQUAL(2, clones.back().pos[1]);

   clones = supermax_find(build_test_data(_str), 0);
   LONGS_EQUAL(2, clones.size());

   clones = supermax_find(build_test_data(_str), -222);
   LONGS_EQUAL(2, clones.size());
}

TEST(template_test2, supermax_find_code_unit_)
{
   char *_str = "abcd789xyzcd78123ccd";
   supermax_node::nodes clones = supermax_find(build_test_data(_str), 1);
   LONGS_EQUAL(1, clones.size());

   LONGS_EQUAL(4, clones.front().M);
   LONGS_EQUAL(2, clones.front().num_leaves);
   LONGS_EQUAL(2, clones.front().num_witness);
   LONGS_EQUAL(10, clones.front().pos[0]);
   LONGS_EQUAL(2, clones.front().pos[1]);

   clones = supermax_find(build_test_data(_str), 5);
   LONGS_EQUAL(0, clones.size());
}

TEST(template_test3, supermax_find_code_unit_)
{
   char *_str = "abcd27189xyzcd78123ccd";
   supermax_node::nodes clones = supermax_find(build_test_data(_str), 1);
   LONGS_EQUAL(5, clones.size());

   LONGS_EQUAL(1, clones.front().M);
   LONGS_EQUAL(2, clones.front().num_leaves);
   LONGS_EQUAL(2, clones.front().num_witness);
   LONGS_EQUAL(16, clones.front().pos[0]);
   LONGS_EQUAL(6, clones.front().pos[1]);

   LONGS_EQUAL(2, clones.back().M);
   LONGS_EQUAL(3, clones.back().num_leaves);
   LONGS_EQUAL(3, clones.back().num_witness);
   LONGS_EQUAL(2, clones.back().pos[0]);
   LONGS_EQUAL(12, clones.back().pos[1]);
   LONGS_EQUAL(20, clones.back().pos[2]);

   clones = supermax_find(build_test_data(_str), 2);
   LONGS_EQUAL(1, clones.size());

   clones = supermax_find(build_test_data(_str), 3);
   LONGS_EQUAL(0, clones.size());
}

TEST(template_test4, supermax_find_code_unit_)
{
   char *_str = "1234123567890123";
   supermax_node::nodes clones = supermax_find(build_test_data(_str), 1);
   LONGS_EQUAL(1, clones.size());

   LONGS_EQUAL(3, clones.front().M);
   LONGS_EQUAL(3, clones.front().num_leaves);
   LONGS_EQUAL(3, clones.front().num_witness);
   LONGS_EQUAL(0, clones.front().pos[0]);
   LONGS_EQUAL(4, clones.front().pos[1]);
   LONGS_EQUAL(13, clones.front().pos[2]);

   clones = supermax_find(build_test_data(_str), 2);
   LONGS_EQUAL(1, clones.size());

   clones = supermax_find(build_test_data(_str), 3);
   LONGS_EQUAL(1, clones.size());
}

TEST(template_test5, supermax_find_code_unit_)
{
   char *_str = "1234123567890123999999";
   supermax_node::nodes clones = supermax_find(build_test_data(_str), 1);
   LONGS_EQUAL(2, clones.size());

   LONGS_EQUAL(3, clones.front().M);
   LONGS_EQUAL(3, clones.front().num_leaves);
   LONGS_EQUAL(3, clones.front().num_witness);
   LONGS_EQUAL(0, clones.front().pos[0]);
   LONGS_EQUAL(4, clones.front().pos[1]);
   LONGS_EQUAL(13, clones.front().pos[2]);

   clones = supermax_find(build_test_data(_str), 2);
   LONGS_EQUAL(2, clones.size());

   clones = supermax_find(build_test_data(_str), 3);
   LONGS_EQUAL(2, clones.size());
}