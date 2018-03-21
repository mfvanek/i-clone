#include "CSourceFileID.h"

namespace source_files
{
   long CSourceFileID::m_SourceFileIDGenerator = CSourceFileID::DEFAULT_SOURCE_FILE_ID;
   std::stack<long> CSourceFileID::m_MissingSourceFileIDs;
   
   long CSourceFileID::GetUniqueID()
   {
      if(!m_MissingSourceFileIDs.empty())
      {
         long id = m_MissingSourceFileIDs.top();
         m_MissingSourceFileIDs.pop();
         return id;
      }
      return ++m_SourceFileIDGenerator;
   }

   void CSourceFileID::RestoreUniqueID(long FileID)
   {
      if(FileID == m_SourceFileIDGenerator)
         --m_SourceFileIDGenerator;
      else
         m_MissingSourceFileIDs.push(FileID);
   }

   CSourceFileID::CSourceFileID(const std::string& fileName)
      : m_fileName(fileName), m_SourceFileID(GetUniqueID())
   {}

   CSourceFileID::~CSourceFileID()
   {
      RestoreUniqueID(m_SourceFileID);
   }

   long CSourceFileID::GetSourceFileID() const
   {
      return m_SourceFileID;
   }

   std::string CSourceFileID::GetSourceFileName() const
   {
      return m_fileName;
   }

   CSourceFileID::Ptr CSourceFileID::Generate(const std::string& fileName)
   {
      CSourceFileID::Ptr obj = new CSourceFileID(fileName);
      return obj;
   }

   bool CSourceFileID::Equals(const CSourceFileID& other) const
   {
      bool result = (m_SourceFileID == other.m_SourceFileID && m_fileName == other.m_fileName);
      return result;
   }

   int CSourceFileID::CompareTo(const CSourceFileID& other) const
   {
      if (m_SourceFileID == other.m_SourceFileID)
      {
         return m_fileName.compare(other.m_fileName);
      }
      return (m_SourceFileID > other.m_SourceFileID) ? 1 : -1;
   }
}