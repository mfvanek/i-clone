
namespace ICloneSearchLibrary.Interfaces
{
    public interface ICloneSearchReporting
    {
        #region // ICloneSearchReporting

        #region // OnCloneSearch

        /// <summary>
        /// Уведомить о начале поиска клонов
        /// </summary>
        void OnCloneSearchStart();

        /// <summary>
        /// Уведомить о прогрессе поиска клонов
        /// </summary>
        void OnCloneSearchProgress();

        /// <summary>
        /// Уведомить о завершении поиска клонов
        /// </summary>
        void OnCloneSearchEnd();

        #endregion

        #endregion
    }
}
