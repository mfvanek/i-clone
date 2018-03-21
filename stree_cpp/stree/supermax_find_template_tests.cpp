#include "TestHarness.h"
#include "repeats_supermax_template.h"

TEST(template_test1, supermax_find_template_)
{
   char *_str = "abcd789xyzcd78123ccdab";
   std::vector<char> str(_str, _str + strlen(_str));
   supermax_node::nodes clones = supermax_find(str, 1);
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

   clones = supermax_find(str, 0);
   LONGS_EQUAL(2, clones.size());

   clones = supermax_find(str, -222);
   LONGS_EQUAL(2, clones.size());
}

TEST(template_test2, supermax_find_template_)
{
   char *_str = "abcd789xyzcd78123ccd";
   std::vector<char> str(_str, _str + strlen(_str));
   supermax_node::nodes clones = supermax_find(str, 1);
   LONGS_EQUAL(1, clones.size());

   LONGS_EQUAL(4, clones.front().M);
   LONGS_EQUAL(2, clones.front().num_leaves);
   LONGS_EQUAL(2, clones.front().num_witness);
   LONGS_EQUAL(10, clones.front().pos[0]);
   LONGS_EQUAL(2, clones.front().pos[1]);

   clones = supermax_find(str, 5);
   LONGS_EQUAL(0, clones.size());
}

TEST(template_test3, supermax_find_template_)
{
   char *_str = "abcd27189xyzcd78123ccd";
   std::vector<char> str(_str, _str + strlen(_str));
   supermax_node::nodes clones = supermax_find(str, 1);
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

   clones = supermax_find(str, 2);
   LONGS_EQUAL(1, clones.size());

   clones = supermax_find(str, 3);
   LONGS_EQUAL(0, clones.size());
}

TEST(template_test4, supermax_find_template_)
{
   char *_str = "1234123567890123";
   std::vector<char> str(_str, _str + strlen(_str));
   supermax_node::nodes clones = supermax_find(str, 1);
   LONGS_EQUAL(1, clones.size());

   LONGS_EQUAL(3, clones.front().M);
   LONGS_EQUAL(3, clones.front().num_leaves);
   LONGS_EQUAL(3, clones.front().num_witness);
   LONGS_EQUAL(0, clones.front().pos[0]);
   LONGS_EQUAL(4, clones.front().pos[1]);
   LONGS_EQUAL(13, clones.front().pos[2]);

   clones = supermax_find(str, 2);
   LONGS_EQUAL(1, clones.size());

   clones = supermax_find(str, 3);
   LONGS_EQUAL(1, clones.size());
}

TEST(template_test5, supermax_find_template_)
{
   //char *_str = "1234123567890123";
   std::vector<int> str;
   str.push_back(1);
   str.push_back(2);
   str.push_back(3);
   str.push_back(4);
   str.push_back(1);
   str.push_back(2);
   str.push_back(3);
   str.push_back(5);
   str.push_back(6);
   str.push_back(7);
   str.push_back(8);
   str.push_back(9);
   str.push_back(0);
   str.push_back(1);
   str.push_back(2);
   str.push_back(3);
   //str.push_back(99);
   //str.push_back(99);
   //str.push_back(99);

   supermax_node::nodes clones = supermax_find(str, 1);
   LONGS_EQUAL(1, clones.size());
   LONGS_EQUAL(3, clones.front().M);
   LONGS_EQUAL(3, clones.front().num_leaves);
   LONGS_EQUAL(3, clones.front().num_witness);
   LONGS_EQUAL(0, clones.front().pos[0]);
   LONGS_EQUAL(4, clones.front().pos[1]);
   LONGS_EQUAL(13, clones.front().pos[2]);
}

//const int TARGET_SIZE = 30000;
//const bool enable_perfomance_test = false;
//
//static void prepare_data(std::vector<char>& str)
//{
//   str.reserve(TARGET_SIZE);
//
//   char *_str1 = "1234567890";
//   char *_str2 = "qwertyuiop";
//   int _str1_len = strlen(_str1);
//   int _str2_len = strlen(_str2);
//   unsigned int size_bound = TARGET_SIZE - 2*_str2_len;
//
//   str.insert(str.end(), _str2, _str2 + _str2_len);
//   while(str.size() <= size_bound)
//   {
//      str.insert(str.end(), _str1, _str1 + _str1_len);
//   }
//   str.insert(str.end(), _str2, _str2 + _str2_len);
//}
//
//TEST(template_perfomance, supermax_find_template_)
//{
//   if(enable_perfomance_test)
//   {
//      std::vector<char> str;
//      prepare_data(str);
//      LONGS_EQUAL(TARGET_SIZE, str.size());
//
//      supermax_node::nodes clones = supermax_find(str, 1);
//      LONGS_EQUAL(2, clones.size());
//   }
//}

// EOF