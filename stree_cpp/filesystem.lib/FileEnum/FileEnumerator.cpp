#include "FileEnumerator.h"

#include <new>
#include <iostream>
#include <tchar.h>
#include <queue>

class CFileFinder
{
public:
   CFileFinder(const char* lpcszInitDir, FindFileData& fileInfo)
      : m_fileInfo(fileInfo)
   {
      Init(lpcszInitDir);
   }

   virtual ~CFileFinder() {}

public:
   bool FindFirst()
   {
      return EnumCurDirFirst();
   }

   bool FindCurDirNext()
   {
      bool bRet = ::FindNextFile(m_hFind, &m_fileInfo) != FALSE;
      if ( bRet )
      {
         m_szPathBuffer.resize(m_nFolderLen);
         m_szPathBuffer += m_fileInfo.cFileName;

      }
      else
      {
         ::FindClose(m_hFind);
         m_hFind = INVALID_HANDLE_VALUE;
      }
      return bRet;
   }

   virtual bool Finish() const
   {
      return INVALID_HANDLE_VALUE == m_hFind;
   }

   const char* GetPath() const
   {
      return m_szPathBuffer.c_str();
   }

   const FindFileData& GetFileInfo() const
   {
      return m_fileInfo;
   }

   bool IsDirectory() const
   {
      return (m_fileInfo.dwFileAttributes & FILE_ATTRIBUTE_DIRECTORY) == FILE_ATTRIBUTE_DIRECTORY;
   }

   bool IsDot() const
   {
      return (m_fileInfo.cFileName[0] == '.') && ((m_fileInfo.cFileName[1] == '.') || (m_fileInfo.cFileName[1] == '\0'));
   }

protected:
   virtual bool EnumCurDirFirst()
   {
      m_szPathBuffer.resize(m_nFolderLen+2);
      m_szPathBuffer[m_nFolderLen++] = '\\';
      m_szPathBuffer[m_nFolderLen] = '*';

      HANDLE hFind = ::FindFirstFile(m_szPathBuffer.c_str(), &m_fileInfo);
      bool bRet = hFind != INVALID_HANDLE_VALUE;
      if ( bRet )
      {
         m_hFind = hFind;
         m_szPathBuffer.resize(m_nFolderLen);
         m_szPathBuffer += m_fileInfo.cFileName;
      }
      return bRet;
   }

   void Init(const char* lpcszInitDir)
   {
      m_nFolderLen = _tcslen(lpcszInitDir);
      m_szPathBuffer = lpcszInitDir;

      if ( m_szPathBuffer[m_nFolderLen-1] == _T('\\') )
      {
         m_szPathBuffer.erase(m_nFolderLen-1);
         --m_nFolderLen;
      }
   }

protected:
   FindFileData& m_fileInfo;
   std::string m_szPathBuffer;

   UINT m_nFolderLen;
   HANDLE m_hFind;

private:
   CFileFinder& operator=(const CFileFinder&);
};

//////////////////////////////////////////////////////////////////////
// CFileEnumeratorBase
//////////////////////////////////////////////////////////////////////

#define	IsStopEventSignaled()		(::WaitForSingleObject(hStopEvent, 0) == WAIT_OBJECT_0)

CFileEnumeratorBase::CFileEnumeratorBase()
   : m_dwLastError(ERROR_SUCCESS)
{
   ResetCounter();
}

#define IncreaseFindFolderCounter()			(++m_nFindFolderCount)
#define IncreaseAcceptedFolderCounter()		(++m_nAcceptedFolderCount)
#define IncreaseFindFileCounter()			(++m_nFindFileCount)
#define IncreaseAcceptedFindFileCounter()	(++m_nAcceptedFileCount)

typedef CFileFinder*					FileFindPtr;
#define DELETE_FILEFINDER(_pFinder)		delete _pFinder

typedef std::queue<FileFindPtr>				FileFindQueue;

#pragma warning(disable: 4127)
// Breadth-first searching, BFS:
bool CFileEnumeratorBase::EnumerateBFS(const char* lpcszInitDir, FindFileData& findFileData, HANDLE hStopEvent /*= NULL*/ )
{
   FileFindPtr finder = NULL;
   try
   {
      finder = new CFileFinder(lpcszInitDir, findFileData);
   }
   catch(const std::bad_alloc&)
   {
      DELETE_FILEFINDER(finder);
      return false;
   }

   bool bRet = true;
   FileFindQueue finderQueue;

   if ( !finder->FindFirst() )
   {
      m_dwLastError = ::GetLastError();
      OnError(finder->GetPath());
      DELETE_FILEFINDER(finder);
      return false;
   }
   else
   {
      while( !finder->Finish() && !IsStopEventSignaled() )
      {
         const FindFileData& fileInfo = finder->GetFileInfo();

         if( finder->IsDirectory() )
         {
            if ( !finder->IsDot() )
            {
               IncreaseFindFolderCounter();
               if ( CheckUseDir(fileInfo) )
               {
                  HandleDir(finder->GetPath(), fileInfo);
                  IncreaseAcceptedFolderCounter();

                  FileFindPtr newFinder = NULL;
                  try
                  {
                     newFinder = new CFileFinder(finder->GetPath(), findFileData);
                     finderQueue.push(newFinder);
                  }
                  catch(const std::bad_alloc&)
                  {
                     DELETE_FILEFINDER(newFinder);
                  }
               }
            }
         }
         else
         {
            IncreaseFindFileCounter();
            if ( CheckUseFile(fileInfo) )
            {
               HandleFile(finder->GetPath(), fileInfo);
               IncreaseAcceptedFindFileCounter();
            }
         }
         if ( !finder->FindCurDirNext() )
         {
            FinishedDir( finder->GetPath() );
            if ( finderQueue.empty() )
               break;
            else
            {
               while ( !IsStopEventSignaled() )
               {
                  FileFindPtr nextFinder = finderQueue.front();
                  finderQueue.pop();

                  DELETE_FILEFINDER(finder);

                  finder = nextFinder;

                  if ( !finder->FindFirst() )
                  {
                     m_dwLastError = ::GetLastError();
                     if ( !OnError(finder->GetPath()) )
                     {
                        bRet = false;
                        goto CleanupRet;
                     }
                  }
                  else
                     break;
               }
            }
         }
      }
   }
CleanupRet:
   while (1)
   {
      DELETE_FILEFINDER(finder);
      if ( finderQueue.empty() )
         break;
      else
      {
         finder = finderQueue.front();
         finderQueue.pop();
      }
   }

   return bRet;
}
#pragma warning(default: 4127)

bool CFileEnumeratorBase::Enumerate( const char* lpcszInitDir, bool bFindSubDir /*= true*/, HANDLE hStopEvent /*= NULL*/ )
{
   if ( !lpcszInitDir || !*lpcszInitDir )
      return false;

   bool bRet = true;

   FindFileData findFileData;

   if ( bFindSubDir )
   {
      bRet = EnumerateBFS(lpcszInitDir, findFileData, hStopEvent);
   }
   else
   {
      CFileFinder fileFinder(lpcszInitDir, findFileData);
      if ( !fileFinder.FindFirst() )
      {
         m_dwLastError = ::GetLastError();
         OnError(lpcszInitDir);
         bRet = false;
      }
      else
      {
         for ( ; !fileFinder.Finish() && !IsStopEventSignaled(); fileFinder.FindCurDirNext() )
         {
            const FindFileData& fileInfo = fileFinder.GetFileInfo();
            if ( !fileFinder.IsDirectory() )
            {
               if ( CheckUseFile(fileInfo) )
               {
                  HandleFile(fileFinder.GetPath(), fileInfo);
                  IncreaseFindFileCounter();
               }
            }
         }
      }
   }
   return bRet;
}

//////////////////////////////////////////////////////////////////////
// CFilteredFileEnumerator
//////////////////////////////////////////////////////////////////////
void CFilteredFileEnumerator::SetFilterPatterns(const char* lpcszFileIncPattern, const char* lpcszFileExcPattern, const char* lpcszDirIncPattern, const char* lpcszDirExcPattern)
{
   std::string strFileIncPattern = lpcszFileIncPattern ? lpcszFileIncPattern : _T("");
   std::string strFileExcPattern = lpcszFileExcPattern ? lpcszFileExcPattern : _T("");
   std::string strDirIncPattern = lpcszDirIncPattern ? lpcszDirIncPattern : _T("");
   std::string strDirExcPattern = lpcszDirExcPattern ? lpcszDirExcPattern : _T("");

   if (!strFileIncPattern.empty())
      Tokenize(m_slFileIncludePattern,  strFileIncPattern);
   if (!strFileExcPattern.empty())
      Tokenize(m_slFileExcludePattern, strFileExcPattern);
   if (!strDirIncPattern.empty())
      Tokenize(m_slDirIncludePattern,  strDirIncPattern);
   if (!strDirExcPattern.empty())
      Tokenize(m_slDirExcludePattern, strDirExcPattern);
}

bool CFilteredFileEnumerator::CheckUseFile(const FindFileData& ffd )
{
   std::string strFile = ffd.cFileName;
   return ( (m_slFileExcludePattern.empty() || !CompareList(m_slFileExcludePattern, strFile)) 
      && (m_slFileIncludePattern.empty() || CompareList(m_slFileIncludePattern, strFile)) 
      );
}

bool CFilteredFileEnumerator::CheckUseDir(const FindFileData& ffd )
{
   std::string strFolder = ffd.cFileName;
   return ( (m_slDirExcludePattern.empty() || !CompareList(m_slDirExcludePattern, strFolder)) 
      && (m_slDirIncludePattern.empty() || CompareList(m_slDirIncludePattern, strFolder)) 
      );
}

#pragma warning(disable: 4127)
void CFilteredFileEnumerator::Tokenize( stringlist& plsTokenized, std::string& sPattern )
{
   // search strings are tokenized by ';' (semicolon) character
   std::string::size_type position = 0;
   std::string::size_type length = 0;

   while (true)
   {
      position = sPattern.find_first_not_of(';', length);
      length = sPattern.find_first_of(';', length + 1);
      if ( position == std::string::npos )
         break;

      plsTokenized.push_back(sPattern.substr(position, length - position));
   }
}
#pragma warning(default: 4127)

bool CompareStrings(const char* sPattern, const char* sFileName, bool bNoCase = true)
{
   char temp1[2] = _T("");
   char temp2[2] = _T("");
   const char* pStar  = 0;
   const char* pName  = 0;

   while(*sFileName)
   {
      switch (*sPattern)
      {
      case '?':
         ++sFileName; ++sPattern;
         continue;
      case '*':
         if (!*++sPattern) return 1;
         pStar = sPattern;
         pName = sFileName + 1;
         continue;
      default:
         if(bNoCase) 
         {
            // _tcsicmp works with strings not chars !!
            *temp1 = *sFileName;
            *temp2 = *sPattern;
            if (!_tcsicmp(temp1, temp2))     // if equal
            {
               ++sFileName; 
               ++sPattern; 
               continue;
            }
         }
         else if (*sFileName == *sPattern)    // bNoCase == false, 
         {                                    // compare chars directly
            ++sFileName; 
            ++sPattern; 
            continue;
         }

         // chars are NOT equal, 
         // if there was no '*' thus far, strings don't match
         if(!pStar) return 0;

         // go back to last '*' character
         sPattern = pStar;
         sFileName = pName++;
         continue;
      }
   }
   // check is there anything left after last '*'
   while (*sPattern == '*') ++sPattern;
   return (!*sPattern);
}

bool CFilteredFileEnumerator::CompareList( stringlist& plsPattern, std::string& sFileName )
{
   stringlist::iterator iter = plsPattern.begin();

   for(; iter != plsPattern.end(); ++iter)
   {
      if (CompareStrings(iter->c_str(), sFileName.c_str()) )
         return true;
   }
   return false;
}

void CFilteredFileEnumerator::HandleFile(const char* lpcszPath, const FindFileData&)
{
   enumerated_files.push_back(lpcszPath);
}

CFilteredFileEnumerator::CFilteredFileEnumerator()
{
   enumerated_files.reserve(20);
}

CFilteredFileEnumerator::stringlist CFilteredFileEnumerator::get_files() const
{
   return enumerated_files;
}