using ICloneBaseLibrary.Interfaces;

namespace ICloneBaseLibrary.Classes
{
    /// <summary>
    /// Класс, обеспечивающий возможность прерывания длительных операций
    /// </summary>
    public abstract class CBreakableActions : IBreakableActions
    {
        #region // Поля класса

        /// <summary>
        /// Флаг, сигнализирующий о необходимости прервать выполняемую операцию
        /// </summary>
        protected bool m_CancelOperationFlag;

        #endregion

        /// <summary>
        /// Конструктор по-умолчанию
        /// </summary>
        public CBreakableActions()
        {
            m_CancelOperationFlag = false;
        }

        #region // Реализация интерфейса IBreakableActions

        /// <summary>
        /// Получить флаг, сигнализирующий о необходимости прервать выполняемую операцию
        /// </summary>
        /// <returns></returns>
        public virtual bool GetCancelOperationFlag()
        {
            return m_CancelOperationFlag;
        }

        /// <summary>
        /// Установить флаг, сигнализирующий о необходимости прервать выполняемую операцию
        /// </summary>
        /// <param name="value"></param>
        public virtual void SetCancelOperationFlag(bool value)
        {
            m_CancelOperationFlag = value;
        }

        #endregion
    }
}