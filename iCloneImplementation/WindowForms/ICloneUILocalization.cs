using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IClone.ICloneImplementation.WindowForms
{
    /// <summary>
    /// Интерфейс для поддержки локализации оконных форм
    /// </summary>
    public interface ICloneUILocalization
    {
        #region // Реализация интерфейса ICloneUILocalization

        /// <summary>
        /// Инициализация пользовательского интерфейса
        /// </summary>
        void InitUI();

        #endregion
    }
}
