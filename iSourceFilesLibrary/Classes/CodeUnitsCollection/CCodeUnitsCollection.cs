using System;
using System.Collections.Generic;
using ISourceFilesLibrary.Classes.CodeUnit;

namespace ISourceFilesLibrary.Classes.CodeUnitsCollection
{
   /// <summary>
   /// Коллекция единиц кода
   /// </summary>
   [Serializable]
   public sealed class CCodeUnitsCollection : TUnitsCollection<CExtendedCodeUnit>
   {
      /// <summary>
      /// Конструктор по умолчанию
      /// </summary>
      public CCodeUnitsCollection()
      {
      }

      /// <summary>
      /// 
      /// </summary>
      /// <param name="units"></param>
      public CCodeUnitsCollection(List<CExtendedCodeUnit> units)
         : base(units)
      {
      }
   }
}