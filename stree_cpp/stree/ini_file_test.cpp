#include "TestHarness.h"
#include "IniFile.h"
#include <cstdio>
#include <iostream>
#include "filesystem.h"

using namespace filesystem;

class CFileManager
{
   std::string m_FileName;

public:
   CFileManager(const std::string FileName);
   ~CFileManager();
};

CFileManager::CFileManager(const std::string FileName)
   : m_FileName(FileName)
{
   CIniFile::Create(m_FileName);
}

CFileManager::~CFileManager()
{
   remove(m_FileName.c_str());
}

TEST(test1, ini_file)
{
   try
   {
      std::string FileName = "$%*?<>/.ini";
      CIniFile::Create(FileName);
      FAIL("ExpectedException(typeof(std::ios_base::failure))");
   }
   catch(const std::ios_base::failure&)
   {}
}

TEST(test2, ini_file)
{
	std::string FileName = "test.ini";
	CFileManager file_creator(FileName);

   CIniFile target(FileName);
   CHECK(!target.IsSectionExists("MySection"));
   target.AddSection("MySection");
   CHECK(target.IsSectionExists("MySection"));
   target.AddSection("MySection2");
   target.AddSection("MySection3");
   target.AddSection("MySection4");
   LONGS_EQUAL(4, target.GetSectionNames().size());
}

TEST(test3, ini_file)
{
	std::string FileName = "test.ini";
	CFileManager file_creator(FileName);

   CIniFile target(FileName);
   LONGS_EQUAL(0, target.GetSectionNames().size());
   LONGS_EQUAL(0, target.GetSection("MySection").size());
   CHECK(!target.GetRecord(RecordLocation("","")).IsValid());
   CHECK(!target.GetRecord(RecordLocation("MySection","")).IsValid());
   CHECK(!target.GetRecord(RecordLocation("MySection","MyKey")).IsValid());
}

TEST(test4, ini_file)
{
	std::string FileName = "test.ini";
	CFileManager file_creator(FileName);

   CIniFile target(FileName);
   target.AddSection("MySection");
   target.AddSection("MySection2");
   target.AddSection("MySection3");
   target.AddSection("MySection4");
   LONGS_EQUAL(4, target.GetSectionNames().size());
   target.SetValue(RecordLocation("MySection","MyKey"), "MyValue");
   target.SetValue(RecordLocation("MySection","MyKey1"), "MyValue");
   target.SetValue(RecordLocation("MySection","MyKey2"), "MyValue");
   target.SetValue(RecordLocation("MySection","MyKey3"), "MyValue");
   target.SetValue(RecordLocation("MySection","MyKey4"), "MyValue");
   LONGS_EQUAL(5, target.GetSection("MySection").size());
   target.DeleteRecord(RecordLocation("MySection","MyKey"));
   LONGS_EQUAL(4, target.GetSection("MySection").size());
   target.DeleteRecord(RecordLocation("MySection","MyKey1"));
   LONGS_EQUAL(3, target.GetSection("MySection").size());
   target.DeleteSection("MySection");
   LONGS_EQUAL(3, target.GetSectionNames().size());
   LONGS_EQUAL(0, target.GetSection("MySection").size());
}

TEST(test5, ini_file)
{
	std::string FileName = "test_ini_file.ini";
   CIniFile target(Path::Combine(Directory::Current(), FileName));
   CHECK_EQUAL("MyValue4", target.GetValue(RecordLocation("MySection","MyKey")));
}