#include "date_time.h"
#include "Windows.h"
#include <sstream>

namespace temporal
{
   void CTimeSpan::convert_from_Millisecond(int& _Hour, int& _Minute, int& _Second, int& _Milliseconds) const
   {
      _Hour = m_Millisecond / 3600000;
      int buff = m_Millisecond % 3600000;
      _Minute = buff / 60000;
      buff = buff % 60000;
      _Second = buff / 1000;
      _Milliseconds = buff % 1000;
   }

   CTimeSpan::CTimeSpan()
      : m_Millisecond(0)
   {}

   CTimeSpan::CTimeSpan(int _Milliseconds)
      : m_Millisecond(_Milliseconds)
   {}

   int CTimeSpan::Hour() const
   {
      int _Hour = 0;
      int _Minute = 0;
      int _Second = 0;
      int _Milliseconds = 0;
      convert_from_Millisecond(_Hour, _Minute, _Second, _Milliseconds);
      return _Hour;
   }

   int CTimeSpan::Minute() const
   {
      int _Hour = 0;
      int _Minute = 0;
      int _Second = 0;
      int _Milliseconds = 0;
      convert_from_Millisecond(_Hour, _Minute, _Second, _Milliseconds);
      return _Minute;
   }

   int CTimeSpan::Second() const
   {
      int _Hour = 0;
      int _Minute = 0;
      int _Second = 0;
      int _Milliseconds = 0;
      convert_from_Millisecond(_Hour, _Minute, _Second, _Milliseconds);
      return _Second;
   }

   int CTimeSpan::Millisecond() const
   {
      int _Hour = 0;
      int _Minute = 0;
      int _Second = 0;
      int _Milliseconds = 0;
      convert_from_Millisecond(_Hour, _Minute, _Second, _Milliseconds);
      return _Milliseconds;
   }

   std::string CTimeSpan::ToString() const
   {
      std::stringstream sst;
      sst.fill('0');
      sst << ToShortString() << ".";
      sst.width(3);
      sst << Millisecond();
      return sst.str();
   }

   std::string CTimeSpan::ToShortString() const
   {
      std::stringstream sst;
      sst.fill('0');
      sst.width(2);
      sst << Hour() << ":";
      sst.width(2);
      sst << Minute() << ":";
      sst.width(2);
      sst << Second();
      return sst.str();
   }

   int CTime::convert_to_Millisecond(int _Hour, int _Minute, int _Second) const
   {
      int result = (_Second + 60 * _Minute + 60 * 60 *_Hour) * 1000;
      return result;
   }

   CTime::CTime(int _Milliseconds)
      : m_timespan(_Milliseconds)
   {}

   CTime::CTime(int _Hour, int _Minute, int _Second, int _Milliseconds)
      : m_timespan(convert_to_Millisecond(_Hour, _Minute, _Second) + _Milliseconds)
   {}

   int CTime::Hour() const
   {
      return m_timespan.Hour();
   }

   int CTime::Minute() const
   {
      return m_timespan.Minute();
   }

   int CTime::Second() const
   {
      return m_timespan.Second();
   }

   int CTime::Millisecond() const
   {
      return m_timespan.Millisecond();
   }

   std::string CTime::ToString() const
   {
      return m_timespan.ToShortString();
   }

   CTime CTime::Now()
   {
      SYSTEMTIME now;
      GetLocalTime(&now);
      return CTime(now.wHour, now.wMinute, now.wSecond, now.wMilliseconds);
   }

   CDate::CDate(int _Day, int _Month, int _Year)
      : m_Day(_Day), m_Month(_Month), m_Year(_Year)
   {}

   int CDate::Day() const
   {
      return m_Day;
   }

   int CDate::Month() const
   {
      return m_Month;
   }

   int CDate::Year() const
   {
      return m_Year;
   }

   std::string CDate::ToString() const
   {
      std::stringstream sst;
      sst.fill('0');
      sst.width(2);
      sst << Day() << ".";
      sst.width(2);
      sst << Month() << ".";
      sst.width(4);
      sst << Year();
      return sst.str();
   }

   CDate CDate::Now()
   {
      SYSTEMTIME now;
      GetLocalTime(&now);
      return CDate(now.wDay, now.wMonth, now.wYear);
   }

   CTimeStamp::CTimeStamp(const CTime& _Time, const CDate& _Date)
      : m_Time(_Time), m_Date(_Date)
   {}

   CTime CTimeStamp::Time() const
   {
      return m_Time;
   }

   CDate CTimeStamp::Date() const
   {
      return m_Date;
   }

   std::string CTimeStamp::ToString() const
   {
      std::string result = m_Date.ToString();
      result.append(" ");
      result.append(m_Time.ToString());
      return result;
   }

   CTimeStamp CTimeStamp::Now()
   {
      return CTimeStamp(CTime::Now(), CDate::Now());
   }

   CTimer::CTimer()
      : m_start(0), m_end(0)
   {}

   void CTimer::Start()
   {
      m_end = 0;
      m_start = GetTickCount();
   }

   void CTimer::Stop()
   {
      m_end = GetTickCount();
   }

   CTimeSpan CTimer::GetSpan() const
   {
      if(0 == m_end) throw std::logic_error("Timer isn't stopprd");
      return CTimeSpan(m_end - m_start);
   }
}