#pragma once

#include <string>

namespace temporal
{
   // Класс для представления произвольного интервала времени
   class CTimeSpan
   {
   private:
      int m_Millisecond;

      void convert_from_Millisecond(int& _Hour, int& _Minute, int& _Second, int& _Millisecond) const;

   public:
      explicit CTimeSpan();
      explicit CTimeSpan(int _Milliseconds);

      int Hour() const;
      int Minute() const;
      int Second() const;
      int Millisecond() const;
      std::string ToString() const;
      std::string ToShortString() const;
   };

   // Класс для представления времени суток
   class CTime
   {
   private:
      CTimeSpan m_timespan;

      int convert_to_Millisecond(int _Hour, int _Minute, int _Second) const;

   public:
      explicit CTime(int _Milliseconds);
      CTime(int _Hour, int _Minute, int _Second, int _Milliseconds);

      int Hour() const;
      int Minute() const;
      int Second() const;
      int Millisecond() const;
      std::string ToString() const;
      static CTime Now();
   };

   class CDate
   {
   private:
      int m_Day;
      int m_Month;
      int m_Year;

   public:
      CDate(int _Day, int _Month, int _Year);

      int Day() const;
      int Month() const;
      int Year() const;
      std::string ToString() const;
      static CDate Now();
   };

   class CTimeStamp
   {
   private:
      CTime m_Time;
      CDate m_Date;

   public:
      CTimeStamp(const CTime& _Time, const CDate& _Date);

      CTime Time() const;
      CDate Date() const;
      std::string ToString() const;
      static CTimeStamp Now();
   };

   // Простейший таймер
   class CTimer
   {
      unsigned long m_start;
      unsigned long m_end;

   public:
      CTimer();
      void Start();
      void Stop();
      CTimeSpan GetSpan() const;
   };
}