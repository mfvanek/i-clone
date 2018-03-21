using System;

namespace ICloneBaseLibrary.Classes
{
    /// <summary>
    /// Мой собственный класс для хранения пар значений
    /// </summary>
    /// <typeparam name="T">Тип объектов, составляющих пару</typeparam>
    [Serializable]
    public sealed class CPair<T> : IEquatable<CPair<T>>
    {
        /// <summary>
        /// Первый элемент пары
        /// </summary>
        private T m_First;
        /// <summary>
        /// Второй элемент пары
        /// </summary>
        private T m_Second;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="_First">Первый элемент пары</param>
        /// <param name="_Second">Второй элемент пары</param>
        public CPair(T _First, T _Second)
        {
            m_First = _First;
            m_Second = _Second;
        }

        /// <summary>
        /// Первый элемент пары
        /// </summary>
        public T First
        {
            get
            {
                return m_First;
            }

            set
            {
                m_First = value;
            }
        }

        /// <summary>
        /// Второй элемент пары
        /// </summary>
        public T Second
        {
            get
            {
                return m_Second;
            }

            set
            {
                m_Second = value;
            }
        }

        /// <summary>
        /// Представить пару в виде массива
        /// </summary>
        public T[] ToArray()
        {
            T[] pair_as_array = { m_First, m_Second };
            return pair_as_array;
        }

        #region // Реализация интерфейса IEquatable<T>

        /// <summary>
        /// Определяет, равны ли два экземпляра
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(CPair<T> other)
        {
            if (other == null)
            {
                // this заведомо не null.
                return false;
            }

            if (Object.ReferenceEquals(this, other))
            {
                // обе ссылки ссылаются на один и тот же объект.
                return true;
            }

            return (m_First.Equals(other.First) && m_Second.Equals(other.Second));
        }

        #endregion

        /// <summary>
        /// Определяет, равны ли два экземпляра
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            return Equals(obj as CPair<T>);
        }

        /// <summary>
        /// Хэш-код для текущего объекта
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
