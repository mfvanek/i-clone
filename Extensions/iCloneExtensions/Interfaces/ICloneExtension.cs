using ICloneExtensions.Classes;
using ISourceFilesLibrary.Classes.CodeUnitsCollection;
using ISourceFilesLibrary.Classes.Languages;
using ISourceFilesLibrary.Classes.SyntacticUnit;

namespace ICloneExtensions.Interfaces
{
   /// <summary>
   /// Интерфейс класса, представляющего плагин для поддрежки ещё одного языка программирования
   /// </summary>
   public interface ICloneExtension
   {
      #region // Реализация интерфейса ICloneExtension

      /// <summary>
      /// Идентификатор языка программирования
      /// </summary>
      /// <returns></returns>
      LANGUAGES LanguageID();

      /// <summary>
      /// Получить название языка программирования
      /// </summary>
      /// <returns></returns>
      string LanguageName();

      /// <summary>
      /// Имя расширения
      /// </summary>
      /// <returns></returns>
      string ExtensionName();

      /// <summary>
      /// Версия расширения
      /// </summary>
      /// <returns></returns>
      string ExtensionVersion();

      /// <summary>
      /// Имя автора, создавшего расширение
      /// </summary>
      /// <returns></returns>
      string AuthorName();

      /// <summary>
      /// Описание расширения
      /// </summary>
      /// <returns></returns>
      string Description();

      /// <summary>
      /// Получить возможные расширения файлов с исходным кодом для данного языка программирования
      /// </summary>
      /// <returns>Список возможных расширений через точку с запятой</returns>
      string GetSourceFileExtentions();

      /// <summary>
      /// Получить возможные символы комментариев. Группы символов перечисляются через точку с запятой.
      /// Парные символы входят в одну группу и разделяются пробелом.
      /// </summary>
      /// <returns></returns>
      string GetCommentSymbols();

      /// <summary>
      /// Представить исходный код, содержащийся в файле, в виде набора токенов
      /// </summary>
      /// <param name="args"></param>
      /// <returns></returns>
      CCodeUnitsCollection Tokenize(CTokenizerParms args);

      /// <summary>
      /// Представить исходный код, содержащийся в файле, в виде набора синтаксических единиц
      /// </summary>
      /// <param name="args"></param>
      /// <returns></returns>
      CSyntacticUnitsCollection Syntacticize(CTokenizerParms args);

      #endregion
   }
}