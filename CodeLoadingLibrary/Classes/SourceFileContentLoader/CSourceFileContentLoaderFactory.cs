using ICloneExtensions.Classes;
using ICloneExtensions.Interfaces;
using ISourceFilesLibrary.Classes.CodeUnit;

namespace CodeLoadingLibrary.Classes.SourceFileContentLoader
{
   /// <summary>
   /// Класс-фабрика для загрузчиков содержимого файла с исходным кодом
   /// </summary>
   public static class CSourceFileContentLoaderFactory
   {
      public static ISourceFileContentLoader Create(CTokenizerParms args, ICloneExtension ext)
      {
         if (CCodeUnit.IsUseTokens())
            return new CTokenSourceFileContentLoader(args, ext);
         else
            return new CEntireRowSourceFileContentLoader(args);
      }
   }
}