using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ISourceFilesLibrary.Classes.CodeUnit
{
   /// <summary>
   /// 
   /// </summary>
   public interface ICodeUnit
   {
      #region // ICodeUnit

      /// <summary>
      /// Содержимое единицы кода
      /// </summary>
      string Text
      {
         get;
         set;
      }

      #endregion
   }
}
