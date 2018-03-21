
namespace ISourceFilesLibrary.Classes.StringEqualityAlgorithms
{
    /// <summary>
    /// Абстрактный базовый класс для реализации семейства алгоритмов, определяющих эквивалентность строк
    /// </summary>
    public abstract class CBaseStringEqualityStrategy
    {
        /// <summary>
        /// Метод, выполняющий сравнение двух строк
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <returns></returns>
        public abstract bool Equals(string first, string second);
    }
}