
namespace IClone.ICloneReport.Interfaces
{
    public interface ICloneExtractReporting
    {
        #region // Реализация интерфейса ICloneExtractReporting

        #region // OnCloneExtract

        /// <summary>
        /// Уведомить о начале извлечения клон-фрагментов
        /// </summary>
        void OnCloneExtractStart();

        /// <summary>
        /// Уведомить о прогрессе извлечения клон-фрагментов
        /// </summary>
        void OnCloneExtractProgress();

        /// <summary>
        /// Уведомить о завершении извлечения клон-фрагментов
        /// </summary>
        void OnCloneExtractEnd();

        #endregion

        #endregion
    }
}
