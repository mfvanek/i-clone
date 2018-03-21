using System.IO;
using ICloneExtensions.Classes;
using ISourceFilesLibrary.Classes.CodeUnit;
using ISourceFilesLibrary.Classes.CodeUnitsCollection;

namespace CodeLoadingLibrary.Classes.SourceFileContentLoader
{
   /// <summary>
   /// Вспомогательный класс для загрузки содержимого файла с исходным кодом
   /// </summary>
   public sealed class CEntireRowSourceFileContentLoader : ISourceFileContentLoader
   {
      CTokenizerParms m_args;

      public CEntireRowSourceFileContentLoader(CTokenizerParms args)
      {
         m_args = args;
      }

      /// <summary>
      /// Загрузить содержимое файла с исходным кодом и получить набор единиц кода
      /// </summary>
      /// <returns></returns>
      public CCodeUnitsCollection Load()
      {
         if (File.Exists(m_args.GetPath()))
         {
            using (FileStream f_stream = new FileStream(m_args.GetPath(), FileMode.Open))
            {
               using (StreamReader sreader = new StreamReader(f_stream, m_args.GetEncoding()))
               {
                  CCodeUnitsCollection collection = new CCodeUnitsCollection();
                  string source_str = null;
                  long line_number = CElementPosition.LINE_NUMBER_LOW_BOUND;
                  while ((source_str = sreader.ReadLine()) != null)
                  {
                     CCodeUnit unit = new CCodeUnit(new CElementPosition(line_number, CElementPosition.INDEX_NUMBER_LOW_BOUND, line_number, source_str.Length), source_str);
                     collection.Add(new CExtendedCodeUnit(unit, m_args.GetFileID().SourceFileID));
                     ++line_number;
                  }
                  return collection;
               }
            }
         }
         else
            throw new FileNotFoundException("File not found", m_args.GetPath());
      }
   }
}