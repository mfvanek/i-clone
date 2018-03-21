using System.Text.RegularExpressions;

namespace ICloneBaseLibrary.Classes
{
    /// <summary>
    /// Вспомогательный класс для обработки строк
    /// </summary>
    public static class CStringConverter
    {
        /// <summary>
        /// Сделал членом класс после профилирования, т.к. построение этого объекта "кушало" 22% времени
        /// </summary>
        private static Regex m_rgx;

        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
        static CStringConverter()
        {
            m_rgx = new Regex(@"\s+");
        }

        /// <summary>
        /// Удалить из указанной строки повторяющиеся пробелы
        /// </summary>
        /// <param name="InputString">Строка с повторяющимися пробелами</param>
        /// <returns>Строка, в которой нет повторяющихся пробелов</returns>
        public static string RemoveMultipleWhiteSpaces(string InputString)
        {
            return m_rgx.Replace(InputString, " ");
        }
    }
}