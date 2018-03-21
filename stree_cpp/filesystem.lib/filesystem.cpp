#include "filesystem.h"
#include "FileEnum\FileEnumerator.h"
#include <windows.h>

namespace filesystem
{
   files_collection get_files(const std::string& directory, const std::string& extensions, const std::string& skipped_files)
   {
      CFilteredFileEnumerator fileEnum;
      fileEnum.SetFilterPatterns(extensions.c_str(), skipped_files.c_str(), nullptr, nullptr);
      fileEnum.Enumerate(directory.c_str());
      return fileEnum.get_files();
   }

   files_collection get_files(const std::string& directory, const std::string& extensions)
   {
      return get_files(directory, extensions, "");
   }

   files_collection get_files(const std::string& directory)
   {
      return get_files(directory, "*.*", "");
   }

   std::string Path::Combine(const std::string& directory, const std::string& filename)
   {
      std::string result = directory;
      if(result.back() != '\\')
         result.push_back('\\');
      result.append(filename);
      return result;
   }

   std::string Directory::Current()
   {
      char buffer[MAX_PATH] = "";
      GetModuleFileName( NULL, buffer, MAX_PATH );
      std::string buff_str(buffer);
      std::string::size_type pos = buff_str.find_last_of( "\\/" );
      return buff_str.substr(0, pos);
   }
}