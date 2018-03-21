#pragma once

//#include "CObject.h"
#include "CCodeUnitsCollection.h"

namespace source_files
{
   class CCodeFragment
   {
   private:
      CCodeUnitsCollection m_Content;

   public:
      CCodeFragment();
      CCodeFragment(const CCodeUnitsCollection& newContent);

      const CCodeUnitsCollection& GetContent() const;
      void SetContent(const CCodeUnitsCollection& newContent);
      
      long GetLineStart() const;
      long GetLineEnd() const;
      int GetIndexStart() const;
      int GetIndexEnd() const;

      size_t size() const;

      bool Equals(const CCodeFragment& other) const;
      bool operator==(const CCodeFragment& other) const
      {
         return Equals(other);
      }

      bool operator!=(const CCodeFragment& other) const
      {
         return !(*this == other);
      }
   };
}