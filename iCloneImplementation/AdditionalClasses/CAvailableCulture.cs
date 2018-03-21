using System.Collections.Generic;

namespace IClone.ICloneImplementation.AdditionalClasses
{
    /// <summary>
    /// Доступные языки интерфейса
    /// </summary>
    public static class CAvailableCulture
    {
        /// <summary>
        /// Список поддерживаемых языков интерфейса
        /// </summary>
        private static Dictionary<string, string> m_AvailableCulture;

        /// <summary>
        /// Конструктор
        /// </summary>
        static CAvailableCulture()
        {
            m_AvailableCulture = new Dictionary<string, string>();
            m_AvailableCulture.Add("Русский", "ru-RU");
            m_AvailableCulture.Add("English", "en-US");
        }

        /// <summary>
        /// Получить список поддерживаемых языков интерфейса
        /// </summary>
        /// <returns></returns>
        public static Dictionary<string, string> GetAvailableCultureList()
        {
            return m_AvailableCulture;
        }

        /// <summary>
        /// Получить идентификатор культуры для заданного языка
        /// </summary>
        /// <param name="Language"></param>
        /// <returns></returns>
        public static string GetCultureID(string Language)
        {
            string retval = string.Empty;

            try
            {
                retval = m_AvailableCulture[Language];
            }
            catch
            {
            }

            return retval;
        }
    }
}