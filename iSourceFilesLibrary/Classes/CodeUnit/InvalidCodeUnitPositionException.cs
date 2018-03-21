using System;

namespace ISourceFilesLibrary.Classes.CodeUnit
{
   /// <summary>
   /// Исключение, генерируемое, если позиция некоторого элемента кода находится вне допустимого диапазона
   /// </summary>
   [Serializable]
   public class InvalidElementPositionException : ArgumentOutOfRangeException
   {
      /// <summary>
      /// 
      /// </summary>
      /// <param name="paramName"></param>
      public InvalidElementPositionException(string paramName) : base(paramName)
      {}
   }
}