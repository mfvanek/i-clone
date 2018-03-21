#include "IniFile.h"
#include <fstream>
#include <algorithm>
#include <sstream>

using namespace std;

namespace filesystem
{
   struct RecordSectionIs : std::unary_function<CIniFile::Record, bool>
   {
      std::string section_;

      RecordSectionIs(const std::string& section): section_(section){}

      bool operator()(const CIniFile::Record& rec) const
      {
         return rec.Section == section_;
      }
   };

   struct RecordSectionKeyIs : std::unary_function<CIniFile::Record, bool>
   {
      std::string section_;
      std::string key_;

      RecordSectionKeyIs(const std::string& section, const std::string& key): section_(section),key_(key){}

      bool operator()(const CIniFile::Record& rec) const
      {
         return ((rec.Section == section_)&&(rec.Key == key_));
      }
   };

   // A function to trim whitespace from both sides of a given std::string
   void Trim(std::string& str, const std::string & ChrsToTrim = " \t\n\r", int TrimDir = 0)
   {
      size_t startIndex = str.find_first_not_of(ChrsToTrim);
      if (startIndex == std::string::npos){str.erase(); return;}
      if (TrimDir < 2) str = str.substr(startIndex, str.size()-startIndex);
      if (TrimDir!=1) str = str.substr(0, str.find_last_not_of(ChrsToTrim) + 1);
   }

   void CIniFile::Create(const std::string& FileName)
   {
      std::ofstream outFile(FileName.c_str());
      outFile.exceptions(outFile.failbit);
   }

   CIniFile::CIniFile(const std::string& FileName)
      : m_FileName(FileName)
   {
      Load();
   }

   void CIniFile::AddSection(const std::string& SectionName)
   {
      m_content.push_back(Record(SectionName));
      Save();
   }

   void CIniFile::SaveRecord(const Record& rec, std::ofstream& outFile) const
   {
      outFile << rec.Comments;
      if(rec.Key == "")
         outFile << "[" << rec.Section << "]" << std::endl;
      else
         outFile << rec.Key << "=" << rec.Value << std::endl;
   }

   void CIniFile::Save() const
   {
      std::ofstream m_stream(m_FileName);
      if (!m_stream.is_open()) throw std::ios_base::failure("file not opened");

      for(std::vector<Record>::const_iterator it = m_content.begin(); it != m_content.end(); ++it)
      {
         SaveRecord(*it, m_stream);
      }
   }

   void CIniFile::Load()
   {
      std::string s;																// Holds the current line from the ini file
      std::string CurrentSection;													// Holds the current section name
      std::string comments = "";													// A std::string to store comments in

      std::ifstream m_stream(m_FileName);
      if (!m_stream.is_open()) throw std::ios_base::failure("file not opened");

      while(!std::getline(m_stream, s).eof())
      {
         Trim(s);															// Trim whitespace from the ends
         if(!s.empty())														// Make sure its not a blank line
         {
            Record r;														// Define a new record

            if((s[0]=='#')||(s[0]==';'))									// Is this a commented line?
            {
               //if ((s.find('[')==std::string::npos)&&							// If there is no [ or =
               //   (s.find('=')==std::string::npos))							// Then it's a comment
               //{
                  comments += s + '\n';									// Add the comment to the current comments std::string
                  continue;
               //}
               //else
               //{
               //   s.erase(s.begin());										// Remove the comment for further processing
               //   Trim(s);
               //}
            }

            if(s.find('[')!=std::string::npos)									// Is this line a section?
            {		
               s.erase(s.begin());											// Erase the leading bracket
               s.erase(s.find(']'));										// Erase the trailing bracket
               r.Comments = comments;										// Add the comments std::string (if any)
               comments = "";												// Clear the comments for re-use
               r.Section = s;												// Set the Section value
               r.Key = "";													// Set the Key value
               r.Value = "";												// Set the Value value
               CurrentSection = s;
            }

            if(s.find('=')!=std::string::npos)									// Is this line a Key/Value?
            {
               r.Comments = comments;										// Add the comments std::string (if any)
               comments = "";												// Clear the comments for re-use
               r.Section = CurrentSection;									// Set the section to the current Section
               r.Key = s.substr(0,s.find('='));							// Set the Key value to everything before the = sign
               r.Value = s.substr(s.find('=')+1);							// Set the Value to everything after the = sign
            }
            if(comments == "")												// Don't add a record yet if its a comment line
               m_content.push_back(r);										// Add the record to content
         }
      }
   }

   CIniFile::Record CIniFile::GetRecord(const RecordLocation& location) const
   {
      Record rec;
      std::vector<Record>::const_iterator it = std::find_if(m_content.begin(), m_content.end(), RecordSectionKeyIs(location.SectionName, location.KeyName));
      if (it != m_content.end())
         rec = *it;
      return rec;
   }

   std::string CIniFile::GetValue(const RecordLocation& location) const
   {
      Record rec(GetRecord(location));
      if(rec.IsValid())
         return rec.Value;
      return "";
   }

   int CIniFile::GetIntValue(const RecordLocation& location) const
   {
      Record rec(GetRecord(location));
      if(!rec.IsValid())
         return 0;
      return atoi(rec.Value.c_str());
   }

   std::vector<CIniFile::Record> CIniFile::GetSection(const std::string& SectionName) const
   {
      std::vector<Record> data;
      for(std::vector<Record>::const_iterator it = m_content.begin(); it != m_content.end(); ++it)
      {
         if((it->Section == SectionName) && (it->Key != ""))
            data.push_back(*it);
      }
      return data;
   }

   std::vector<std::string> CIniFile::GetSectionNames() const
   {
      std::vector<std::string> data;
      for(std::vector<Record>::const_iterator it = m_content.begin(); it != m_content.end(); ++it)
      {
         if(it->Key == "")
            data.push_back(it->Section);
      }
      return data;
   }

   bool CIniFile::IsSectionExists(const std::string& SectionName) const
   {
      std::vector<Record>::const_iterator it = std::find_if(m_content.begin(), m_content.end(), RecordSectionIs(SectionName));
      if (it == m_content.end()) return false;
      return true;
   }

   bool CIniFile::IsRecordExists(const RecordLocation& location) const
   {
      std::vector<Record>::const_iterator it = std::find_if(m_content.begin(), m_content.end(), RecordSectionKeyIs(location.SectionName, location.KeyName));
      if (it == m_content.end()) return false;
      return true;
   }

   void CIniFile::SetValue(const RecordLocation& location, const std::string& Value)
   {
      if(!IsSectionExists(location.SectionName))
      {
         Record s(location.SectionName);
         Record r(location.SectionName, location.KeyName, Value);
         m_content.push_back(s);
         m_content.push_back(r);
      }
      else
      {
         if(!IsRecordExists(location))
         {
            std::vector<Record>::iterator it = std::find_if(m_content.begin(), m_content.end(), RecordSectionIs(location.SectionName));
            ++it;
            Record r(location.SectionName, location.KeyName, Value);
            m_content.insert(it, r);
         }
         else
         {
            std::vector<Record>::iterator it = std::find_if(m_content.begin(), m_content.end(), RecordSectionKeyIs(location.SectionName, location.KeyName));
            it->Value = Value;
         }
      }
      Save();
   }

   void CIniFile::SetIntValue(const RecordLocation& location, int Value)
   {
      std::stringstream sst;
      sst << Value;
      SetValue(location, sst.str());
   }

   void CIniFile::DeleteSection(const std::string& SectionName)
   {
      for(std::vector<Record>::iterator it = m_content.begin(); it != m_content.end(); )
      {
         if(it->Section == SectionName)
            it = m_content.erase(it);
         else
            ++it;
      }
      Save();
   }

   void CIniFile::DeleteRecord(const RecordLocation& location)
   {
      std::vector<Record>::iterator it = std::find_if(m_content.begin(), m_content.end(), RecordSectionKeyIs(location.SectionName, location.KeyName));
      if (it != m_content.end())
         m_content.erase(it);
      Save();
   }
}