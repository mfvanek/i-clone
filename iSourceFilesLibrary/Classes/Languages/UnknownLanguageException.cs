using System;

namespace ISourceFilesLibrary.Classes.Languages
{
   /// <summary>
   /// Исключение генерируется при попытке использования неизвестного или неподдерживаемого языка программирования
   /// </summary>
   [Serializable]
   public class UnknownLanguageException : Exception
   {
      public UnknownLanguageException()
         : base("Язык программирования не известен или не поддерживается")
      { }

      public UnknownLanguageException(LANGUAGES LanguageID)
         : base("Язык программирования " + LanguageID.ToString() + " не известен или не поддерживается")
      { }
   }
}