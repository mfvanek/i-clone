using System.IO;
using ICloneExtensions.Classes;
using ICloneExtensions.Interfaces;
using ISourceFilesLibrary.Classes.CodeUnitsCollection;

namespace CodeLoadingLibrary.Classes.SourceFileContentLoader
{
   public sealed class CTokenSourceFileContentLoader : ISourceFileContentLoader
   {
      CTokenizerParms m_args;
      ICloneExtension m_ext;

      public CTokenSourceFileContentLoader(CTokenizerParms args, ICloneExtension ext)
      {
         m_args = args;
         m_ext = ext;
      }

      /// <summary>
      /// Загрузить содержимое файла с исходным кодом и получить набор единиц кода
      /// </summary>
      /// <returns></returns>
      public CCodeUnitsCollection Load()
      {
         if (File.Exists(m_args.GetPath()))
         {
            return m_ext.Tokenize(m_args);
         }
         else
            throw new FileNotFoundException("File not found", m_args.GetPath());
      }
   }
}