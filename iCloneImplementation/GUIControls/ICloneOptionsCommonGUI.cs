using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IClone.ICloneImplementation.GUIControls
{
    interface ICloneOptionsCommonGUI
    {
        #region // Реализация ICloneOptionsCommonGUI

        /// <summary>
        /// Загрузить параметры
        /// </summary>
        void LoadSettings();

        /// <summary>
        /// Сохранить параметры
        /// </summary>
        void SaveSettings();

        #endregion
    }
}
