namespace ICloneBaseLibrary.Interfaces
{
    /// <summary>
    /// Специальный интерфейс для реализации сравненя единиц кода
    /// </summary>
    public interface IEquatableObject
    {
        #region // Реализация интерфейса IEquatableObject

        /// <summary>
        /// Указывает, равен ли текущий объект другому объекту
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        bool EqualsObject(object obj);

        #endregion
    }

    /// <summary>
    /// Специальный интерфейс для реализации сравненя единиц кода
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IEquatableObject<T>
    {
        #region // Реализация интерфейса IEquatableObject<T>

        /// <summary>
        /// Указывает, равен ли текущий объект другому объекту того же типа
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        bool EqualsObject(T other);
        
        #endregion
    }
}