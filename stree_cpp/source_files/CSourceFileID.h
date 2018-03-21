#pragma once

#include "common_pointers.h"
#include <string>
#include <stack>

namespace source_files
{
   class CSourceFileID
   {
   public:
      typedef DefSmartPtr<CSourceFileID>::Ptr Ptr;
      //typedef Loki::SmartPtr<CSourceFileID> Ptr;
      typedef std::vector<CSourceFileID::Ptr> Collection;

      static const long DEFAULT_SOURCE_FILE_ID = 0; // Идентификатор файла по умолчанию
      static CSourceFileID::Ptr Generate(const std::string& fileName);

   private:
      std::string m_fileName;
      long m_SourceFileID; // Идентификатор файла
      CSourceFileID(const std::string& fileName);

   public:
      ~CSourceFileID();
      long GetSourceFileID() const;
      std::string GetSourceFileName() const;

      bool Equals(const CSourceFileID& other) const;

      bool operator==(const CSourceFileID& other) const
      {
         return Equals(other);
      }
     
      bool operator!=(const CSourceFileID& other) const
      {
         return !(*this == other);
      }

      int CompareTo(const CSourceFileID& other) const;

      bool operator<(const CSourceFileID& other) const
      {
         return (CompareTo(other) < 0);
      }

   private:
      static long m_SourceFileIDGenerator;
      static std::stack<long> m_MissingSourceFileIDs;

      static long GetUniqueID(); // Получить уникальный идентификатор файла
      static void RestoreUniqueID(long FileID); // Освободить уникальный номер файла для повторного использования
   };
}