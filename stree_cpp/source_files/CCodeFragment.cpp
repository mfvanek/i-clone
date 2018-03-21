#include "CCodeFragment.h"

namespace source_files
{
   CCodeFragment::CCodeFragment()
   {}

   CCodeFragment::CCodeFragment(const CCodeUnitsCollection& newContent)
      : m_Content(newContent)
   {}

   const CCodeUnitsCollection& CCodeFragment::GetContent() const
   {
      return m_Content;
   }

   void CCodeFragment::SetContent(const CCodeUnitsCollection& newContent)
   {
      m_Content = newContent;
   }

   long CCodeFragment::GetLineStart() const
   {
      return m_Content.front().GetLineStart();
   }

   long CCodeFragment::GetLineEnd() const
   {
      return m_Content.back().GetLineEnd();
   }

   int CCodeFragment::GetIndexStart() const
   {
      return m_Content.front().GetIndexStart();
   }

   int CCodeFragment::GetIndexEnd() const
   {
      return m_Content.back().GetIndexEnd();
   }

   size_t CCodeFragment::size() const
   {
      return m_Content.size();
   }

   bool CCodeFragment::Equals(const CCodeFragment& other) const
   {
      return m_Content.Equals(other.m_Content);
   }
}