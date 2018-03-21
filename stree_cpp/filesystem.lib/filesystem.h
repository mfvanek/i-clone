#pragma once

#include <vector>
#include <string>

namespace filesystem
{
   typedef std::vector<std::string> files_collection;
   files_collection get_files(const std::string& directory);
   files_collection get_files(const std::string& directory, const std::string& extensions);
   files_collection get_files(const std::string& directory, const std::string& extensions, const std::string& skipped_files);

   class Path
   {
   public:
      static std::string Combine(const std::string& directory, const std::string& filename);
   };

   class Directory
   {
   public:
      static std::string Current();
   };
}