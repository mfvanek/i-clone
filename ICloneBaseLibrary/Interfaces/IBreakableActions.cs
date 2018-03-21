
namespace ICloneBaseLibrary.Interfaces
{
    /// <summary>
    /// Интерфейс, обеспечивающий возможность прерывания длительных операций
    /// </summary>
    public interface IBreakableActions
    {
        #region // Реализация интерфейса IBreakableActions

        /// <summary>
        /// Получить флаг, сигнализирующий о необходимости прервать выполняемую операцию
        /// </summary>
        /// <returns></returns>
        bool GetCancelOperationFlag();

        /// <summary>
        /// Установить флаг, сигнализирующий о необходимости прервать выполняемую операцию
        /// </summary>
        /// <param name="value"></param>
        void SetCancelOperationFlag(bool value);

        #endregion
    }
}
