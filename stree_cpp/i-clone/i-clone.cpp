#include <stdio.h>
#include <tchar.h>
#include <conio.h>
#include <iostream>

#include "clone_search_strategy.h"
#include "date_time.h"
#include "IniFile.h"
#include "CExtensionFactory.h"

using namespace filesystem;
using namespace source_files;

class SimpleProgressBar
{
public:
   bool Progress(const std::string& message);
};

bool SimpleProgressBar::Progress(const std::string& message)
{
   std::cout << temporal::CTime::Now().ToString() << "  " << message << std::endl;
   return true;
}

static std::string ini_file_name = "i-clone.ini";
static std::string ini_cmn_section = "COMMON";
static std::string ini_src_dir = "ROOT_DIRECTORY";
static std::string ini_src_ext = "FILES_EXTENSIONS";
static std::string ini_src_lang = "LANGUAGE_ID";
static std::string ini_src_Kmin = "K_MIN";
static std::string ini_cpp_section = "C++";
static std::string ini_cs_section = "C#";
static std::string ini_src_skippedfiles = "SKIPPED_FILES";

static CIniFile::Ptr get_ini_file()
{
   CIniFile::Ptr options;
   try
   {
      options = new CIniFile(ini_file_name);
   }
   catch(const std::ios_base::failure&)
   {
      CIniFile::Create(ini_file_name);
      options = new CIniFile(ini_file_name);
      options->SetValue(RecordLocation(ini_cmn_section, ini_src_dir), "");
      options->SetValue(RecordLocation(ini_cmn_section, ini_src_lang), ini_cpp_section);
      options->SetIntValue(RecordLocation(ini_cmn_section, ini_src_Kmin), 60);

      options->SetValue(RecordLocation(ini_cpp_section, ini_src_ext), "*.cpp;*.c;*.cc;*.cxx;*.h;*.hpp;*.hh;*.hxx");
      options->SetValue(RecordLocation(ini_cpp_section, ini_src_skippedfiles), "");

      options->SetValue(RecordLocation(ini_cs_section, ini_src_ext), "*.cs");
      options->SetValue(RecordLocation(ini_cs_section, ini_src_skippedfiles), "AssemblyInfo.cs");
   }
   return options;
}

int _tmain(int, _TCHAR*)
{
   try
   {
      CIniFile::Ptr options = get_ini_file();
      std::string dir = options->GetValue(RecordLocation(ini_cmn_section, ini_src_dir));
      std::string lang = options->GetValue(RecordLocation(ini_cmn_section, ini_src_lang));
      LANGUAGES lang_id = CExtensionFactory::GetLanguage(lang);
      std::string ext = options->GetValue(RecordLocation(lang, ini_src_ext));
      int K_min = options->GetIntValue(RecordLocation(ini_cmn_section, ini_src_Kmin));
      std::string skipped_files = options->GetValue(RecordLocation(lang, ini_src_skippedfiles));
      clone_search::CBaseCloneSearchStrategy::InputParams prm(dir, ext, lang_id, K_min, skipped_files);
      clone_search::CBaseCloneSearchStrategy searcher(prm);
      SimpleProgressBar progress_bar;
      searcher.search_event.attach(&progress_bar, &SimpleProgressBar::Progress);
      searcher.FindClones();
      searcher.PrintClones();
   }
   catch(const std::exception& ex)
   {
      std::cout << temporal::CTime::Now().ToString() << "  Exception!!! " << ex.what() << std::endl;
   }

   _getch();
	return 0;
}