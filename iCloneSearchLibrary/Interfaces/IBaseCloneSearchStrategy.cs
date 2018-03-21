
namespace ICloneSearchLibrary.Interfaces
{
    public interface IBaseCloneSearchStrategy
    {
        #region // Реализация интерфейса IBaseCloneSearchStrategy

        /// <summary>
        /// Название алгоритма для поиска дублирующихся строк программного кода
        /// </summary>
        /// <returns></returns>
        string SearchingAlgorithmName();

        #endregion
    }
}
