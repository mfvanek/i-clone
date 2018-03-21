#include "CElementPosition.h"
#include <cmath>

namespace source_files
{
   CElementPosition::CElementPosition(long lineStart, int indexStart, long lineEnd, int indexEnd)
      : m_LineStart(lineStart), m_IndexStart(indexStart), m_LineEnd(lineEnd), m_IndexEnd(indexEnd)
   {
      validate_all();
   }

   CElementPosition::CElementPosition(long lineStart, int indexStart)
      : m_LineStart(lineStart), m_IndexStart(indexStart), m_LineEnd(lineStart), m_IndexEnd(indexStart)
   {
      validate_all();
   }

   void CElementPosition::validate_LineStart() const
   {
      if (m_LineStart < LINE_NUMBER_LOW_BOUND || m_LineStart > m_LineEnd)
         throw InvalidElementPositionException("LineStart");
   }

   void CElementPosition::validate_IndexStart() const
   {
      if (m_IndexStart < INDEX_NUMBER_LOW_BOUND)
         throw InvalidElementPositionException("IndexStart");

      if (m_LineStart == m_LineEnd && m_IndexStart > m_IndexEnd)
         throw InvalidElementPositionException("IndexStart");
   }

   void CElementPosition::validate_LineEnd() const
   {
      if (m_LineEnd < LINE_NUMBER_LOW_BOUND || m_LineEnd < m_LineStart)
         throw InvalidElementPositionException("LineEnd");
   }

   void CElementPosition::validate_IndexEnd() const
   {
      if (m_IndexEnd < INDEX_NUMBER_LOW_BOUND)
         throw InvalidElementPositionException("IndexEnd");

      if (m_LineStart == m_LineEnd && m_IndexEnd < m_IndexStart)
         throw InvalidElementPositionException("IndexEnd");
   }

   void CElementPosition::validate_all() const
   {
      validate_LineStart();
      validate_IndexStart();
      validate_LineEnd();
      validate_IndexEnd();
   }

   long CElementPosition::GetLineStart() const
   {
      return m_LineStart;
   }

   long CElementPosition::GetLineEnd() const
   {
      return m_LineEnd;
   }

   int CElementPosition::GetIndexStart() const
   {
      return m_IndexStart;
   }

   int CElementPosition::GetIndexEnd() const
   {
      return m_IndexEnd;
   }

   bool CElementPosition::Equals(const CElementPosition& other) const
   {
      bool res = (m_LineStart == other.m_LineStart && m_LineEnd == other.m_LineEnd && m_IndexStart == other.m_IndexStart && m_IndexEnd == other.m_IndexEnd);
      return res;
   }
   
   double CElementPosition::size() const
   {
      double res = sqrt(pow((double)(m_LineEnd - m_LineStart), 2.0) + pow((double)(m_IndexEnd - m_IndexStart), 2.0));
      return res;
   }

   int CElementPosition::CompareTo(const CElementPosition& other) const
   {
      int result = 0;
      if(!Equals(other))
      {
         double size_this = size();
         double size_other = other.size();
         if(size_this != size_other)
            result = (size_this > size_other) ? 1 : -1;
      }
      return result;
   }
}