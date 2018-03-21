
namespace ISourceFilesLibrary.Classes.SourceFile
{
   /// <summary>
   /// 
   /// </summary>
   public interface ISourceFileContentSaver
   {
      /// <summary>
      /// Сохранить файл на диск
      /// </summary>
      /// <param name="source_file"></param>
      void Save(CSourceFile source_file);
   }
}