using System;
using System.Collections.Generic;
using ISourceFilesLibrary.Classes.CodeUnitsCollection;

namespace ISourceFilesLibrary.Classes.SyntacticUnit
{
   /// <summary>
   /// Коллекция синтаксических единиц кода
   /// </summary>
   [Serializable]
   public sealed class CSyntacticUnitsCollection : TUnitsCollection<CSyntacticUnit>
   {
      public CSyntacticUnitsCollection()
      {
      }

      public CSyntacticUnitsCollection(List<CSyntacticUnit> units)
         : base(units)
      {
      }
   }
}
