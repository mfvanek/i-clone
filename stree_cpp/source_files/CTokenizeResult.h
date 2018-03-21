#pragma once

#include "CCodeUnitsCollection.h"
#include "CSourceFileID.h"

namespace source_files
{
   class CTokenizeResult
   {
      CSourceFileID::Ptr m_file_id;
      CCodeUnitsCollection::Type m_tokens;

   public:
      CTokenizeResult(const std::string& fileName);
      CSourceFileID::Ptr GetSourceFileID() const;
      CCodeUnitsCollection::Type& GetCollection();
      std::string GetSourceFileName() const;
   };
}