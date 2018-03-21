using System;
using System.Text;
using ISourceFilesLibrary.Classes.SourceFile;

namespace ICloneExtensions.Classes
{
   /// <summary>
   /// Параметры для загрузки сожержимого файла с исходным кодом
   /// </summary>
   public sealed class CTokenizerParms
   {
      string m_Path;
      Encoding m_Encoding;
      CSourceFileID m_FileID;

      public CTokenizerParms(string _Path, Encoding _Encoding, CSourceFileID FileID)
      {
         if (!string.IsNullOrEmpty(_Path))
            m_Path = _Path;
         else
            throw new ArgumentNullException("_Path");

         m_Encoding = _Encoding;
         m_FileID = FileID;
      }

      public CTokenizerParms(string _Path, CSourceFileID FileID)
         : this(_Path, Encoding.GetEncoding(1251), FileID)
      {
      }

      public string GetPath()
      {
         return m_Path;
      }

      public Encoding GetEncoding()
      {
         return m_Encoding;
      }

      public CSourceFileID GetFileID()
      {
         return m_FileID;
      }
   }
}