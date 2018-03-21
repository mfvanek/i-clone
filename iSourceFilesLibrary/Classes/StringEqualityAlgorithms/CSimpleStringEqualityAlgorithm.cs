
namespace ISourceFilesLibrary.Classes.StringEqualityAlgorithms
{
    /// <summary>
    /// Простейший алгоритм сравнения строк
    /// </summary>
    public sealed class CSimpleStringEqualityAlgorithm : CBaseStringEqualityStrategy
    {
        /// <summary>
        /// Метод, выполняющий сравнение двух строк
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <returns></returns>
        public override bool Equals(string first, string second)
        {
            if (first != null)
            {
                return first.Equals(second);
            }
            else
            {
                if (second == null)
                {
                    return true;
                }
            }

            return false;
        }
    }
}