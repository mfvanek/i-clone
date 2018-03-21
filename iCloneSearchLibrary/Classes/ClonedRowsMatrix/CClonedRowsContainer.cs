using System;
using System.Collections.Generic;
using ISourceFilesLibrary.Classes.CodeUnit;

namespace ICloneSearchLibrary.Classes.ClonedRowsMatrix
{
   /// <summary>
   /// Вспомогательный класс-контейнер для группировки дублирующихся-строк
   /// </summary>
   public class CClonedRowsContainer : CBaseClonedRowsMatrix
   {
      private readonly Dictionary<CExtendedCodeUnit, List<CExtendedCodeUnit>> m_UnitsContainer;

      public CClonedRowsContainer()
      {
         m_UnitsContainer = new Dictionary<CExtendedCodeUnit, List<CExtendedCodeUnit>>();
      }

      public void Add(CExtendedCodeUnit item)
      {
         if (item != null)
         {
            if (m_UnitsContainer.ContainsKey(item))
            {
               m_UnitsContainer[item].Add(item);
            }
            else
            {
               List<CExtendedCodeUnit> list = new List<CExtendedCodeUnit>() { item };
               m_UnitsContainer.Add(item, list);
            }
         }
         else
            throw new ArgumentNullException("item");
      }

      public List<CExtendedCodeUnit> GetEqualRows(CExtendedCodeUnit item)
      {
         return m_UnitsContainer[item];
      }

      public int Count
      {
         get
         {
            return m_UnitsContainer.Count;
         }
      }
   }
}