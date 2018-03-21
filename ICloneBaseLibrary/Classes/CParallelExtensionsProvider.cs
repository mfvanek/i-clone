using System;

namespace ICloneBaseLibrary.Classes
{
    /// <summary>
    /// Вспомогательный класс для управления возможностью работы с библиотекой параллельных задач
    /// </summary>
    public static class CParallelExtensionsProvider
    {
        private const int MinimumProcesorsCountNeededToUseParallelExtensions = 2;

        /// <summary>
        /// Можно ли использовать библиотеку параллельных задач?
        /// </summary>
        /// <returns></returns>
        public static bool IsCanUseParallelExtensions()
        {
            return (Environment.ProcessorCount >= MinimumProcesorsCountNeededToUseParallelExtensions);
        }
    }
}
