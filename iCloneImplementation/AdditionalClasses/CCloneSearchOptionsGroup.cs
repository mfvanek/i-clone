using System.Text;
using System.Windows.Forms;
using IClone.ICloneImplementation.GUIControls;
using ICloneBaseLibrary.Classes;

namespace IClone.ICloneImplementation.AdditionalClasses
{
    public class CCloneSearchOptionsGroup : CICloneOptionsGroup
    {
        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
        public CCloneSearchOptionsGroup()
            : base(new CCloneSearchOptionsUserControl())
        {
        }

        /// <summary>
        /// Получить название группы параметров
        /// </summary>
        /// <returns></returns>
        public override string GetName()
        {
            return ICloneLocalization.CSW_WindowHeader;
        }

        /// <summary>
        /// Вспомогательный метод для инициализации списка кодировок текста
        /// </summary>
        /// <param name="DefaultEncoding"></param>
        /// <param name="EncodingComboBox"></param>
        public static void InitCodeEncodingComboBox(ref ComboBox EncodingComboBox)
        {
            Encoding DefaultEncoding = Encoding.GetEncoding(Properties.Settings.Default.CSWLastChoosenEncoding);
            int DefaultIndex = -1;
            EncodingInfo[] AvailableEncodings = Encoding.GetEncodings();

            foreach (EncodingInfo EncodeInfo in AvailableEncodings)
            {
                if (DefaultEncoding.CodePage == EncodeInfo.CodePage)
                {
                    DefaultIndex = EncodingComboBox.Items.Count;
                }
                EncodingComboBox.Items.Add(new CICloneEncodingInfo(EncodeInfo));
            }
            EncodingComboBox.SelectedIndex = DefaultIndex;
        }
    }
}
