using System;
using IClone.ICloneReport.Interfaces;

namespace IClone.ICloneReport.Classes
{
    /// <summary>
    /// Класс для работы со схожестью двух объектов в долях единицы
    /// </summary>
    public sealed class CSimilarity : ISimilarity
    {
        public static double MIN_SIMILARITY_VALUE = 0D;
        public static double MAX_SIMILARITY_VALUE = 1D;

        /// <summary>
        /// Схожесть двух объектов в долях единицы
        /// </summary>
        private double m_Similarity;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_Similarity"></param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        private void SetSimilarity(double _Similarity)
        {
            if (_Similarity >= MIN_SIMILARITY_VALUE && _Similarity <= MAX_SIMILARITY_VALUE)
            {
                this.m_Similarity = _Similarity;
            }
            else
            {
                throw new ArgumentOutOfRangeException();
            }
        }

        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
        public CSimilarity()
        {
            m_Similarity = MIN_SIMILARITY_VALUE;
        }

        /// <summary>
        /// Конструктор с параметром
        /// </summary>
        /// <param name="_Similarity"></param>
        public CSimilarity(double _Similarity)
        {
            SetSimilarity(_Similarity);
        }

        #region // Реализация интерфейса ISimilarity

        /// <summary>
        /// Схожесть
        /// </summary>
        public double Similarity
        {
            get
            {
                return m_Similarity;
            }

            set
            {
                SetSimilarity(value);
            }
        }

        #endregion

        /// <summary>
        /// Представить в виде строки
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return m_Similarity.ToString("R");
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            CSimilarity p = obj as CSimilarity;
            if ((object)p == null)
            {
                return false;
            }

            return m_Similarity.Equals(p.m_Similarity);
        }

        public override int GetHashCode()
        {
            return m_Similarity.GetHashCode();
        }
    }
}