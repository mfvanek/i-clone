#pragma once

#include "common_pointers.h"
#include <string>
#include <vector>

namespace filesystem
{
   struct RecordLocation
   {
      std::string SectionName;
      std::string KeyName;

      RecordLocation(const std::string& _SectionName, const std::string& _KeyName)
         : SectionName(_SectionName), KeyName(_KeyName)
      {}
   };

   class CIniFile
   {
   public:
      typedef DefSmartPtr<CIniFile>::Ptr Ptr;

      struct Record
      {
         std::string Comments;
         std::string Section;
         std::string Key;
         std::string Value;

         Record()
         {}

         Record(const std::string& _SectionName)
            : Section(_SectionName)	
         {}

         Record(const std::string& _SectionName, const std::string& _KeyName, const std::string& _Value)
            : Section(_SectionName), Key(_KeyName), Value(_Value)
         {}

         bool IsValid() const
         {
            return !Section.empty();
         }

         bool IsSection() const
         {
            return Key.empty();
         }
      };

      static void Create(const std::string& FileName);

      CIniFile(const std::string& FileName);

      void AddSection(const std::string& SectionName);
      std::vector<Record> GetSection(const std::string& SectionName) const;
      std::vector<std::string> GetSectionNames() const;
      bool IsSectionExists(const std::string& SectionName) const;
      void DeleteSection(const std::string& SectionName);

      Record GetRecord(const RecordLocation& location) const;
      bool IsRecordExists(const RecordLocation& location) const;
      std::string GetValue(const RecordLocation& location) const;
      int GetIntValue(const RecordLocation& location) const;
      void SetValue(const RecordLocation& location, const std::string& Value);
      void SetIntValue(const RecordLocation& location, int Value);
      void DeleteRecord(const RecordLocation& location);

   private:
      std::string m_FileName;
      std::vector<Record> m_content;

      void Load();
      void Save() const;
      void SaveRecord(const Record& rec, std::ofstream& outFile) const;
   };
}