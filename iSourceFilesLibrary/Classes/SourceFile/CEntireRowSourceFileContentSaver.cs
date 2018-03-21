using System;
using System.IO;
using System.Text;
using ISourceFilesLibrary.Classes.CodeUnit;

namespace ISourceFilesLibrary.Classes.SourceFile
{
   /// <summary>
   /// 
   /// </summary>
   public sealed class CEntireRowSourceFileContentSaver : ISourceFileContentSaver
   {
      /// <summary>
      /// Кодировка файла
      /// </summary>
      private Encoding m_Encoding;

      private string m_Path;

      /// <summary>
      /// 
      /// </summary>
      /// <param name="_Path"></param>
      /// <param name="_Encoding"></param>
      public CEntireRowSourceFileContentSaver(string _Path, Encoding _Encoding)
      {
         if (!string.IsNullOrEmpty(_Path))
            m_Path = _Path;
         else
            throw new ArgumentNullException("_Path");

         m_Encoding = _Encoding;
      }

      /// <summary>
      /// Сохранить файл на диск
      /// </summary>
      /// <param name="source_file"></param>
      public void Save(CSourceFile source_file)
      {
         using (FileStream f_stream = new FileStream(m_Path, FileMode.CreateNew))
         {
            using (StreamWriter s_writer = new StreamWriter(f_stream, m_Encoding))
            {
               foreach (CExtendedCodeUnit unit in source_file.Content.Content)
               {
                  s_writer.WriteLine(unit.Text);
               }
            }
         }
      }
   }
}