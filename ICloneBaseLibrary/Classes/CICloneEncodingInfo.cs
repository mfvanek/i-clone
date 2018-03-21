using System.Text;

namespace ICloneBaseLibrary.Classes
{
    /// <summary>
    /// Вспомогательный класс для работы с кодировками
    /// </summary>
    public class CICloneEncodingInfo
    {
        /// <summary>
        /// Информация о кодировке
        /// </summary>
        private EncodingInfo m_EncodingInfo;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="encoding_info"></param>
        public CICloneEncodingInfo(EncodingInfo encoding_info)
        {
            SetEncodingInfo(encoding_info);
        }

        /// <summary>
        /// Представить в виде строки
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return m_EncodingInfo.CodePage.ToString() + " - " + m_EncodingInfo.DisplayName;
        }

        /// <summary>
        /// Получить информацию о кодировке
        /// </summary>
        /// <returns></returns>
        public EncodingInfo GetEncodingInfo()
        {
            return m_EncodingInfo;
        }

        /// <summary>
        /// Установить информацию о кодировке
        /// </summary>
        /// <param name="encoding_info"></param>
        public void SetEncodingInfo(EncodingInfo encoding_info)
        {
            m_EncodingInfo = encoding_info;
        }

        /// <summary>
        /// Идентификатор кодировки
        /// </summary>
        public int CodePage
        {
            get
            {
                return m_EncodingInfo.CodePage;
            }
        }
    }
}