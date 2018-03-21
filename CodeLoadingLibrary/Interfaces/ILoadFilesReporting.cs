
namespace CodeLoadingLibrary.Interfaces
{
    /// <summary>
    /// Интерфейс для индикации процесса загрузки файлов
    /// </summary>
    public interface ILoadFilesReporting
    {
        #region // Реализация интерфейса ILoadFilesReporting

        #region // OnLoadFiles

        /// <summary>
        /// Уведомить о начале загрузки файлов
        /// </summary>
        void OnLoadFilesStart();

        /// <summary>
        /// Уведомить о загрузке файла
        /// </summary>
        void OnLoadFilesProgress();

        /// <summary>
        /// Уведомить о завершении загрузки файлов
        /// </summary>
        void OnLoadFilesEnd();

        #endregion

        #region // OnFilesListBuilding

        /// <summary>
        /// Уведомить о начале построения списка файлов
        /// </summary>
        void OnFilesListBuildingStart();

        /// <summary>
        /// Уведомить о добавлении файла в список
        /// </summary>
        void OnFilesListBuildingProgress();

        /// <summary>
        /// Уведомить о завершении построения списка файлов
        /// </summary>
        void OnFilesListBuildingEnd();

        #endregion

        #endregion
    }
}
