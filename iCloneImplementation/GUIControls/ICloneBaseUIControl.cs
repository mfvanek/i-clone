using System.Windows.Forms;
using IClone.ICloneImplementation.WindowForms;

namespace IClone.ICloneImplementation.GUIControls
{
    public partial class CICloneBaseUIControl : UserControl, ICloneUILocalization, ICloneOptionsCommonGUI
    {
        public CICloneBaseUIControl()
        {
            InitializeComponent();
        }

        #region // Реализация интерфейса ICloneUILocalization

        /// <summary>
        /// Инициализация пользовательского интерфейса
        /// </summary>
        public virtual void InitUI()
        {
        }

        #endregion

        #region // Реализация ICloneOptionsCommonGUI

        /// <summary>
        /// Загрузить параметры
        /// </summary>
        public virtual void LoadSettings()
        {
        }

        /// <summary>
        /// Сохранить параметры
        /// </summary>
        public virtual void SaveSettings()
        {
        }

        #endregion
    }
}
