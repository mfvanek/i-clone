using ISourceFilesLibrary.Classes.CodeUnitsCollection;

namespace CodeLoadingLibrary.Classes.SourceFileContentLoader
{
   /// <summary>
   /// 
   /// </summary>
   public interface ISourceFileContentLoader
   {
      /// <summary>
      /// Загрузить содержимое файла с исходным кодом и получить набор единиц кода
      /// </summary>
      /// <returns></returns>
      CCodeUnitsCollection Load();
   }
}
