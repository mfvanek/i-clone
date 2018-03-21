using System.Collections.Generic;
using ICloneSearchLibrary.Classes.ClonedRowsMatrix;
using ICloneSearchLibrary.Interfaces;
using ISourceFilesLibrary.Classes.CodeUnit;

namespace ICloneSearchLibrary.Classes
{
   /// <summary>
   /// Идентификаторы алгоритмов, используемых для поиска клонов
   /// </summary>
   public enum CloneSearchAlgoritms
   {
      /// <summary>
      /// Метод \"грубой силы\"
      /// </summary>
      BruteForceAlgorithm = 0,
      /// <summary>
      /// Метод \"хэш-контейнера\"
      /// </summary>
      HashBucketAlgorithm
   }

   /// <summary>
   /// Абстрактный базовый класс для реализации паттерна "Стратегия" применительно к поиску клонов в коде
   /// </summary>
   public abstract class CBaseCloneSearchStrategy : CCloneSearchReporting, IDuplicatedRowsSearcher, IBaseCloneSearchStrategy
   {
      protected CClonedRowsMatrix m_ClonedRowsMatrix;

      public CBaseCloneSearchStrategy()
      {
         m_ClonedRowsMatrix = new CClonedRowsMatrix();
      }

      /// <summary>
      /// Получить матрицу дублирующихся строк
      /// </summary>
      /// <returns></returns>
      public CClonedRowsMatrix GetClonedRowsMatrix()
      {
         return m_ClonedRowsMatrix;
      }

      #region // Реализация интерфейса IDuplicatedRowsSearcher

      /// <summary>
      /// Выполнить поиск дублирующихся строк программного кода
      /// </summary>
      /// <param name="AllSourceRows"></param>
      public abstract void FindDuplicatedRows(List<CExtendedCodeUnit> AllSourceRows);

      #endregion

      #region // Реализация интерфейса IBaseCloneSearchStrategy

      /// <summary>
      /// Название алгоритма для поиска дублирующихся строк программного кода
      /// </summary>
      /// <returns></returns>
      public abstract string SearchingAlgorithmName();

      /// <summary>
      /// Идентификатор алгоритма
      /// </summary>
      /// <returns></returns>
      public abstract CloneSearchAlgoritms AlgorithmID();

      #endregion

      public override string ToString()
      {
         return SearchingAlgorithmName();
      }

      public override bool Equals(object obj)
      {
         if (obj == null)
         {
            return false;
         }

         CBaseCloneSearchStrategy p = obj as CBaseCloneSearchStrategy;
         if ((object)p == null)
         {
            return false;
         }

         return (AlgorithmID() == p.AlgorithmID());
      }

      public override int GetHashCode()
      {
         return base.GetHashCode();
      }
   }
}
