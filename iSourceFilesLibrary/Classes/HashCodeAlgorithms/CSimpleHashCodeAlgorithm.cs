using System;

namespace ISourceFilesLibrary.Classes.HashCodeAlgorithms
{
    /// <summary>
    /// Класс, использующий для вычисления хэша стандартный механизм C#
    /// </summary>
    [Serializable]
    public sealed class CSimpleHashCodeAlgorithm : CBaseHashCodeGenerateStrategy
    {
        /// <summary>
        /// Вычислить хэш для указанной строки
        /// </summary>
        /// <param name="Row"></param>
        /// <returns></returns>
        public override int GetHashCode(string Row)
        {
            if(Row == null)
            {
                return 0;
            }

            return Row.GetHashCode();
        }
    }
}
