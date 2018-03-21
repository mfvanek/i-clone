#include "CCodeUnitsCollection.h"

namespace source_files
{
   CCodeUnitsCollection::CCodeUnitsCollection()
   {
      m_Collection.reserve(DEFAULT_COLLECTION_SIZE);
   }

   size_t CCodeUnitsCollection::size() const
   {
      return m_Collection.size();
   }

   void CCodeUnitsCollection::add(const CCodeUnit& item)
   {
      m_Collection.push_back(item);
   }

   const CCodeUnit& CCodeUnitsCollection::operator[](size_t n) const
   {
      return m_Collection.at(n);
   }

   const CCodeUnit& CCodeUnitsCollection::front() const
   {
      return m_Collection.at(0);
   }

   const CCodeUnit& CCodeUnitsCollection::back() const
   {
      return m_Collection.at(m_Collection.size() - 1);
   }

   void CCodeUnitsCollection::addRange(const CCodeUnitsCollection& other)
   {
      m_Collection.insert(m_Collection.end(), other.m_Collection.begin(), other.m_Collection.end());
   }

   bool CCodeUnitsCollection::Equals(const CCodeUnitsCollection& other) const
   {
      if(m_Collection.size() == other.m_Collection.size())
      {
         for(size_t counter = 0; counter < m_Collection.size(); ++counter)
         {
            if(m_Collection[counter] != other.m_Collection[counter])
               return false;
         }
         return true;
      }
      return false;
   }

   CCodeUnitsCollection::Type& CCodeUnitsCollection::GetCollection()
   {
      return m_Collection;
   }
}