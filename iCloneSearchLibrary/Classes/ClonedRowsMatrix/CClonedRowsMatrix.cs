
namespace ICloneSearchLibrary.Classes.ClonedRowsMatrix
{
    /// <summary>
    /// Матрица найденных клон-строк
    /// </summary>
    public class CClonedRowsMatrix : CBaseClonedRowsMatrix
    {
        private bool[,] m_ClonedRowsMatrix;
        private const int DEFAULT_MATRIX_SIZE = 1;

        /// <summary>
        /// 
        /// </summary>
        public CClonedRowsMatrix()
        {
            m_ClonedRowsMatrix = new bool[DEFAULT_MATRIX_SIZE, DEFAULT_MATRIX_SIZE];
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="RowsCount"></param>
        public CClonedRowsMatrix(int RowsCount)
        {
            m_ClonedRowsMatrix = new bool[RowsCount, RowsCount];
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="FirstRowIndex"></param>
        /// <param name="SecondRowIndex"></param>
        public void AddClonedRowsPair(int FirstRowIndex, int SecondRowIndex)
        {
            m_ClonedRowsMatrix[FirstRowIndex, SecondRowIndex] = true;
        }
    }
}