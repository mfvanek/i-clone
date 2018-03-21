using System.Collections.Generic;
using ICloneSearchLibrary.Classes.ClonedRowsMatrix;

namespace IClone.ICloneReport.Classes
{
   /// <summary>
   /// Абстрактный базовый класс для получения дублирующихся фрагментов кода
   /// </summary>
   public abstract class CBaseCloneExtractor : CCloneExtractReporting
   {
      protected List<CRelatedCloneFragments> m_CloneFragmentsList = new List<CRelatedCloneFragments>();
      protected abstract void BuildClonedFragmentsList(CBaseClonedRowsMatrix ClonedRowsMatrix);

      /// <summary>
      /// Извлечь дублирующиеся фрагменты кода
      /// </summary>
      /// <param name="ClonedRowsMatrix"></param>
      public void Extract(CBaseClonedRowsMatrix ClonedRowsMatrix)
      {
         BuildClonedFragmentsList(ClonedRowsMatrix);
      }

      /// <summary>
      /// Получить список дублирующихся фрагментов кода
      /// </summary>
      public List<CRelatedCloneFragments> CloneFragmentsList
      {
         get
         {
            return m_CloneFragmentsList;
         }
      }
   }
}