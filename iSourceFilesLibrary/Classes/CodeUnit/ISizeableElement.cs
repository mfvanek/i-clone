using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ISourceFilesLibrary.Classes.CodeUnit
{
   /// <summary>
   /// Интерфейс для получения размера элемента
   /// </summary>
   public interface ISizeableElement
   {
      /// <summary>
      /// Размер элемента
      /// </summary>
      /// <returns></returns>
      long Size();
   }
}