using System;

namespace ISourceFilesLibrary.Classes.HashCodeAlgorithms
{
    /// <summary>
    /// Базовый класс для реализации семейства алгоритмов, вычисляющих хэш для строки программного кода
    /// </summary>
    [Serializable]
    public abstract class CBaseHashCodeGenerateStrategy
    {
        /// <summary>
        /// Вычислить хэш для указанной строки
        /// </summary>
        /// <param name="Row"></param>
        /// <returns></returns>
        public abstract int GetHashCode(string Row);
    }
}