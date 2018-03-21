using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ICloneSearchLibrary.Classes
{
    /// <summary>
    /// Вспомогательный класс для представления коллекции клон-поисковых алгоритмов
    /// </summary>
    public static class CAvailableCloneSearchAlgorithms
    {
        private static readonly Dictionary<CloneSearchAlgoritms, CBaseCloneSearchStrategy> m_CloneSearchAlgorithms;

        static CAvailableCloneSearchAlgorithms()
        {
            m_CloneSearchAlgorithms = new Dictionary<CloneSearchAlgoritms, CBaseCloneSearchStrategy>();
            CBaseCloneSearchStrategy algorithm = new CBruteForceAlgorithm();
            m_CloneSearchAlgorithms.Add(algorithm.AlgorithmID(), algorithm);
            algorithm = new CHashBucketAlgorithm();
            m_CloneSearchAlgorithms.Add(algorithm.AlgorithmID(), algorithm);
        }

        public static Dictionary<CloneSearchAlgoritms, CBaseCloneSearchStrategy> GetAlgorithmsList()
        {
            return m_CloneSearchAlgorithms;
        }

        public static CBaseCloneSearchStrategy GetAlgorithm(CloneSearchAlgoritms AlgorithmID)
        {
            CBaseCloneSearchStrategy value = null;

            switch(AlgorithmID)
            {
                case CloneSearchAlgoritms.BruteForceAlgorithm:
                    value = new CBruteForceAlgorithm();
                    break;

                case CloneSearchAlgoritms.HashBucketAlgorithm:
                    value = new CHashBucketAlgorithm();
                    break;
            }

            System.Diagnostics.Debug.Assert(value != null, "Error! value is null!");
            return value;
        }
    }
}