using System;

namespace ISourceFilesLibrary.Classes.Exceptions
{
    /// <summary>
    /// Исключение, генерируемое при несовпадении идентификаторов файлов у нескольких строк в фрагменте кода
    /// </summary>
    [Serializable]
    public class SourceFileIDMismatchException : ArgumentException
    {
    }
}