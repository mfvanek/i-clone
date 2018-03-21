#include "CTokenizeResult.h"

namespace source_files
{
   CTokenizeResult::CTokenizeResult(const std::string& fileName)
      : m_file_id(CSourceFileID::Generate(fileName))
   {}

   CSourceFileID::Ptr CTokenizeResult::GetSourceFileID() const
   {
      return m_file_id;
   }

   CCodeUnitsCollection::Type& CTokenizeResult::GetCollection()
   {
      return m_tokens;
   }

   std::string CTokenizeResult::GetSourceFileName() const
   {
      return m_file_id->GetSourceFileName();
   }
}