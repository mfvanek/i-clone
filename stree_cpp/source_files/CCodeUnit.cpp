#include "CCodeUnit.h"

namespace source_files
{
   CCodeUnit::CCodeUnit()
      : /*m_SourceFileID(DEFAULT_SOURCE_FILE_ID), */m_Position(CElementPosition(CElementPosition::LINE_NUMBER_LOW_BOUND, CElementPosition::INDEX_NUMBER_LOW_BOUND)), m_Text("")
   {}

   CCodeUnit::CCodeUnit(const CSourceFileID::Ptr& SourceFileID, const CElementPosition& position, const std::string& text)
      : m_SourceFileID(SourceFileID), m_Position(position), m_Text(text)
   {}
   
   std::string CCodeUnit::GetText() const
   {
      return m_Text;
   }

   CSourceFileID::Ptr CCodeUnit::GetSourceFileID() const
   {
      return m_SourceFileID;
   }

   long CCodeUnit::GetLineStart() const
   {
      return m_Position.GetLineStart();
   }

   long CCodeUnit::GetLineEnd() const
   {
      return m_Position.GetLineEnd();
   }

   int CCodeUnit::GetIndexStart() const
   {
      return m_Position.GetIndexStart();
   }

   int CCodeUnit::GetIndexEnd() const
   {
      return m_Position.GetIndexEnd();
   }

   bool CCodeUnit::Equals(const CCodeUnit& other) const
   {
      bool res = (m_SourceFileID->Equals(*other.m_SourceFileID) && m_Position.Equals(other.m_Position) && (m_Text == other.m_Text));
      return res;
   }

    bool CCodeUnit::operator==(const CCodeUnit& other) const
    {
       bool result = (GetChar() == other.GetChar());
       return result;
    }

   int CCodeUnit::CompareTo(const CCodeUnit& other) const
   {
      if(m_Text == other.m_Text)
      {
         if (m_SourceFileID->Equals(*other.m_SourceFileID))
         {
            return m_Position.CompareTo(other.m_Position);
         }
         return m_SourceFileID->CompareTo(*other.m_SourceFileID);
      }
      return m_Text.compare(other.m_Text);
   }

   bool CCodeUnit::operator<(const CCodeUnit& other) const
   {
       bool result = (GetChar() < other.GetChar());
       return result;
   }

   //void CCodeUnit::validate_similarity(double similarity_measure) const
   //{
   //   if(similarity_measure > 1.0 || similarity_measure < 0.0)
   //      throw InvalidSimilarityMeasureException();
   //}

   //bool CCodeUnit::IsSimilar(const CCodeUnit& other, double similarity_measure) const
   //{
   //   validate_similarity(similarity_measure);
   //   // Пока что так, т.е. проверяем на 100% совпадение.
   //   // Потом можно будет добавит ьчто-то более интеллектуальное.
   //   return Equals(other);
   //}

   char CCodeUnit::GetChar() const
   {
      return m_Text[0];
   }
   
   CCodeUnit::operator int() const
   {
      return (int)GetChar();
   }
}