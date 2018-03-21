#ifndef FILE_ENUMERATOR_H_
#define FILE_ENUMERATOR_H_

#include <windows.h>
#include <string>
#include <vector>
#include <assert.h>
#include <stdio.h>
#include <stdarg.h>
#include <tchar.h>

typedef WIN32_FIND_DATA			FindFileData, *PFileInfo;

class CFileEnumeratorBase
{
public:
   CFileEnumeratorBase();
   virtual ~CFileEnumeratorBase() {}

public:
   virtual bool Enumerate(const char* lpcszInitDir, bool bFindSubDir = true, HANDLE hStopEvent = NULL);

   unsigned long GetLastError() const				{ return m_dwLastError; }

   inline UINT	GetFindFolderCount() const			{ return m_nFindFolderCount; }
   inline UINT	GetAcceptedFolderCount() const		{ return m_nAcceptedFolderCount; }
   inline UINT	GetFindFileCount() const			{ return m_nFindFileCount; }
   inline UINT	GetAcceptedFileCount() const		{ return m_nAcceptedFileCount; }
   inline void ResetCounter()						{ m_nAcceptedFileCount = m_nFindFileCount = m_nFindFolderCount = m_nAcceptedFolderCount = 0; }

protected:
   // return true to enumerating files, or false to skit it
   virtual bool CheckUseFile(const FindFileData& /*ffd*/)		{return true;}

   // return true to enumerate a sub directory, or false to skip it
   virtual bool CheckUseDir(const FindFileData& /*ffd*/)		{return true;}

   // default handler for a file that currently enumerated
   virtual void HandleFile(const char* /*lpcszPath*/, const FindFileData& /*ffd*/) = 0;

   // default handler for a directory that currently enumerated
   virtual void HandleDir(const char* /*lpcszPath*/, const FindFileData& /*ffd*/)			{}

   // Called when all files and/or sub-directories within a directory has been finished visiting.
   virtual void FinishedDir(const char* /*lpcszPath*/)										{}

   // return true to ignore error and continue enumerating (if possible).
   virtual bool OnError(const char* /*lpcszPath = NULL*/)
   {
      return true;
   }

   // Breadth-first search
   bool EnumerateBFS(const char* lpcszInitDir, FindFileData& findFileData, HANDLE hStopEvent = NULL);

protected:
   unsigned long	m_dwLastError;

   UINT	m_nFindFolderCount;
   UINT	m_nAcceptedFolderCount;
   UINT	m_nFindFileCount;
   UINT	m_nAcceptedFileCount;
};

class CFilteredFileEnumerator : public CFileEnumeratorBase
{
public:
   typedef std::vector<std::string>	stringlist;

   CFilteredFileEnumerator();

   void SetFilterPatterns(const char* lpcszFileIncPattern = nullptr, 
      const char* lpcszFileExcPattern = nullptr, 
      const char* lpcszDirIncPattern = nullptr, 
      const char* lpcszDirExcPattern = nullptr);

   stringlist get_files() const;

protected:
   virtual bool CheckUseFile( const FindFileData& ffd);
   virtual bool CheckUseDir(const FindFileData& ffd);
   void Tokenize(stringlist& plsTokenized, std::string& sPattern);
   bool CompareList(stringlist& plsPattern, std::string& sFileName);
   virtual void HandleFile(const char* lpcszPath, const FindFileData&);

   stringlist		m_slFileIncludePattern;
   stringlist		m_slFileExcludePattern;
   stringlist		m_slDirIncludePattern;
   stringlist		m_slDirExcludePattern;

   stringlist enumerated_files;
};

#endif // #ifndef FILE_ENUMERATOR_H_