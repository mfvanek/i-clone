using System.Collections.Generic;
using ISourceFilesLibrary.Classes.CodeUnit;

namespace ICloneSearchLibrary.Interfaces
{
   public interface IDuplicatedRowsSearcher
   {
      #region // Реализация интерфейса IDuplicatedRowsSearcher

      /// <summary>
      /// Выполнить поиск дублирующихся строк программного кода
      /// </summary>
      /// <param name="AllSourceRows"></param>
      void FindDuplicatedRows(List<CExtendedCodeUnit> AllSourceRows);

      #endregion
   }
}
