#pragma once

#include "CElementPosition.h"
#include "CSourceFileID.h"

namespace source_files
{
   class CCodeUnit
   {
   private:
      CSourceFileID::Ptr m_SourceFileID; // Идентификатор файла, которому принадлежит единица кода
      CElementPosition m_Position; // Местоположение единицы кода
      std::string m_Text; // Содержимое единицы кода

      char GetChar() const;

   public:
      CCodeUnit();
      CCodeUnit(const CSourceFileID::Ptr& SourceFileID, const CElementPosition& position, const std::string& text);

      std::string GetText() const;
      CSourceFileID::Ptr GetSourceFileID() const;

      long GetLineStart() const;
      long GetLineEnd() const;
      int GetIndexStart() const;
      int GetIndexEnd() const;

      bool Equals(const CCodeUnit& other) const;
      bool operator==(const CCodeUnit& other) const;
      bool operator!=(const CCodeUnit& other) const
      {
         return !(*this == other);
      }

      int CompareTo(const CCodeUnit& other) const;
      bool operator<(const CCodeUnit& other) const;

      operator int() const;
   };
}