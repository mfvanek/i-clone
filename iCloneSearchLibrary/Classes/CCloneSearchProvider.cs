using System;
using System.Collections.Generic;
using ICloneBaseLibrary.Interfaces;
using ICloneSearchLibrary.Classes.ClonedRowsMatrix;
using ICloneSearchLibrary.Interfaces;
using ISourceFilesLibrary.Classes.CodeUnit;

namespace ICloneSearchLibrary.Classes
{
   public sealed class CCloneSearchProvider : IDuplicatedRowsSearcher, IBreakableActions
   {
      private CBaseCloneSearchStrategy m_CloneSearchStrategy;
      private CClonedRowsMatrix m_ClonedRowsMatrix;

      public CCloneSearchProvider(CBaseCloneSearchStrategy _CloneSearchStrategy)
      {
         m_CloneSearchStrategy = _CloneSearchStrategy;
         m_ClonedRowsMatrix = new CClonedRowsMatrix();
      }

      #region // Реализация интерфейса IDuplicatedRowsSearcher

      /// <summary>
      /// Выполнить поиск дублирующихся строк программного кода
      /// </summary>
      /// <param name="AllSourceRows"></param>
      public void FindDuplicatedRows(List<CExtendedCodeUnit> AllSourceRows)
      {
         m_CloneSearchStrategy.FindDuplicatedRows(AllSourceRows);
         m_ClonedRowsMatrix = m_CloneSearchStrategy.GetClonedRowsMatrix();
      }

      #endregion

      #region // Реализация интерфейса IBreakableActions

      /// <summary>
      /// Получить флаг, сигнализирующий о необходимости прервать выполняемую операцию
      /// </summary>
      /// <returns></returns>
      public bool GetCancelOperationFlag()
      {
         return m_CloneSearchStrategy.GetCancelOperationFlag();
      }

      /// <summary>
      /// Установить флаг, сигнализирующий о необходимости прервать выполняемую операцию
      /// </summary>
      /// <param name="value"></param>
      public void SetCancelOperationFlag(bool value)
      {
         m_CloneSearchStrategy.SetCancelOperationFlag(value);
      }

      #endregion

      public void InitCloneSearchCallbacks(EventHandler CloneSearchStartEventHandler, EventHandler CloneSearchProgressEventHandler, EventHandler CloneSearchEndEventHandler)
      {
         m_CloneSearchStrategy.InitCloneSearchCallbacks(CloneSearchStartEventHandler, CloneSearchProgressEventHandler, CloneSearchEndEventHandler);
      }
   }
}