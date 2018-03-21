#pragma once

#include <stdexcept>

namespace source_files
{
   class InvalidElementPositionException : public std::out_of_range
   {
   public:
      explicit InvalidElementPositionException(const std::string& what_arg)
         : std::out_of_range(what_arg)
      {}
   };

   class CElementPosition
   {
   public:
      static const long LINE_NUMBER_LOW_BOUND = 1; // Ќижн€€ граница диапазона номеров строк
      static const int INDEX_NUMBER_LOW_BOUND = 0; // Ќижн€€ граница диапазона индексов в строке

   private:
      long m_LineStart;  // Ќомер строки, в которой начинаетс€ единица кода
      int  m_IndexStart; // Ќомер позиции в строке, где начинаетс€ единица кода
      long m_LineEnd;    // Ќомер строки, в которой заканчиваетс€ единица кода
      int  m_IndexEnd;   // Ќомер позиции в строке, где заканчиваетс€ единица кода

      void validate_LineStart() const;
      void validate_IndexStart() const;
      void validate_LineEnd() const;
      void validate_IndexEnd() const;
      void validate_all() const;

      double size() const;

   public:
      CElementPosition(long lineStart, int indexStart, long lineEnd, int indexEnd);
      CElementPosition(long lineStart, int indexStart);

      long GetLineStart() const;
      long GetLineEnd() const;
      int GetIndexStart() const;
      int GetIndexEnd() const;

      bool Equals(const CElementPosition& other) const;

      bool operator==(const CElementPosition& other) const
      {
         return Equals(other);
      }
      
      bool operator!=(const CElementPosition& other) const
      {
         return !(*this == other);
      }

      int CompareTo(const CElementPosition& other) const;

      bool operator<(const CElementPosition& other) const
      {
         return (CompareTo(other) < 0);
      }
   };
}