
namespace ISourceFilesLibrary.Classes.CodeUnit
{
   /// <summary>
   /// Интерфейс для получения позиции некоторого элемента в коде
   /// </summary>
   public interface IElementPosition
   {
      #region // Реализация интерфейса IElementPosition

      /// <summary>
      /// Номер строки, в которой начинается элемент кода
      /// </summary>
      long LineStart
      {
         get;
         set;
      }

      /// <summary>
      /// Номер строки, в которой заканчивается элемент кода
      /// </summary>
      long LineEnd
      {
         get;
         set;
      }

      /// <summary>
      /// Номер позиции в строке, где начинается элемент кода
      /// </summary>
      int IndexStart
      {
         get;
         set;
      }

      /// <summary>
      /// Номер позиции в строке, где заканчивается элемент кода
      /// </summary>
      int IndexEnd
      {
         get;
         set;
      }

      #endregion
   }
}