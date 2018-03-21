using System.Collections.Generic;
using ICloneSearchLibrary.Classes.ClonedRowsMatrix;
using ISourceFilesLibrary.Classes.CodeUnit;

namespace ICloneSearchLibrary.Classes
{
   /// <summary>
   /// Метод "грубой силы"
   /// </summary>
   public sealed class CBruteForceAlgorithm : CBaseCloneSearchStrategy
   {
      private const string m_SearchingAlgorithmName = "Метод \"грубой силы\"";

      #region // Реализация интерфейса IBaseCloneSearchStrategy

      /// <summary>
      /// Название алгоритма для поиска дублирующихся строк программного кода
      /// </summary>
      /// <returns></returns>
      public override string SearchingAlgorithmName()
      {
         return m_SearchingAlgorithmName;
      }

      /// <summary>
      /// Идентификатор алгоритма
      /// </summary>
      /// <returns></returns>
      public override CloneSearchAlgoritms AlgorithmID()
      {
         return CloneSearchAlgoritms.BruteForceAlgorithm;
      }

      #endregion

      #region // Реализация интерфейса IDuplicatedRowsSearcher

      /// <summary>
      /// Выполнить поиск дублирующихся строк программного кода
      /// </summary>
      /// <param name="AllSourceRows"></param>
      public override void FindDuplicatedRows(List<CExtendedCodeUnit> AllSourceRows)
      {
         OnCloneSearchStart();

         m_ClonedRowsMatrix = new CClonedRowsMatrix(AllSourceRows.Count);

         if (!GetCancelOperationFlag())
         {
            for (int first_rows_counter = 0; first_rows_counter < AllSourceRows.Count; first_rows_counter++)
            {
               if (!GetCancelOperationFlag())
               {
                  for (int second_rows_counter = first_rows_counter + 1; second_rows_counter < AllSourceRows.Count; second_rows_counter++)
                  {
                     if (!GetCancelOperationFlag())
                     {
                        CExtendedCodeUnit first = AllSourceRows[first_rows_counter];
                        CExtendedCodeUnit second = AllSourceRows[second_rows_counter];

                        if (first.Text.Equals(second.Text))
                        {
                        }
                     }
                     else
                     {
                        break;
                     }
                  }
                  OnCloneSearchProgress();
               }
               else
               {
                  break;
               }
            }
         }

         OnCloneSearchEnd();
      }

      #endregion
   }
}
