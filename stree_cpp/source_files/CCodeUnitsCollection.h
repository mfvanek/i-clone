#pragma once

#include "CCodeUnit.h"
#include <vector>

namespace source_files
{
   class CCodeUnitsCollection
   {
   public:
      typedef std::vector<CCodeUnit> Type;

   private:
      typedef Type::const_iterator Iterator;

      static const size_t DEFAULT_COLLECTION_SIZE = 30;

      Type m_Collection;

   public:
      CCodeUnitsCollection();

      size_t size() const;
      void add(const CCodeUnit& item);
      const CCodeUnit& operator[](size_t n) const;
      const CCodeUnit& front() const;
      const CCodeUnit& back() const;

      Type& GetCollection();
      void addRange(const CCodeUnitsCollection& other);

      bool Equals(const CCodeUnitsCollection& other) const;

      bool operator==(const CCodeUnitsCollection& other) const
      {
         return Equals(other);
      }

      bool operator!=(const CCodeUnitsCollection& other) const
      {
         return !(*this == other);
      }
   };
}