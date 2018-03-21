using System.Collections.Generic;
using ICloneSearchLibrary.Classes.ClonedRowsMatrix;
using ISourceFilesLibrary.Classes.CodeUnit;

namespace ICloneSearchLibrary.Classes
{
   public sealed class CHashBucketAlgorithm : CBaseCloneSearchStrategy
   {
      private const string m_SearchingAlgorithmName = "Метод \"хэш-контейнера\"";
      private CClonedRowsContainer m_ClonedRowsContainer;

      public CHashBucketAlgorithm()
      {
         m_ClonedRowsContainer = new CClonedRowsContainer();
      }

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
         return CloneSearchAlgoritms.HashBucketAlgorithm;
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

         if (!GetCancelOperationFlag())
         {
            for (int rows_counter = 0; rows_counter < AllSourceRows.Count; rows_counter++)
            {
               if (!GetCancelOperationFlag())
               {
                  try
                  {
                     m_ClonedRowsContainer.Add(AllSourceRows[rows_counter]);
                  }
                  catch
                  {
                     System.Diagnostics.Debug.Assert(false, "Исключение в FindDuplicatedRows()");
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