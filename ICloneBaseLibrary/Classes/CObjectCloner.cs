using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace ICloneBaseLibrary.Classes
{
    /// <summary>
    /// Вспомогательный класс для глубокого копирования объектов
    /// </summary>
    public static class CObjectCloner
    {
        /// <summary>
        /// Обобщенный метод для глубокого копирования объектов
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static T DeepCopy<T>(T value) where T : class
        {
            Type t = typeof(T);
            System.Diagnostics.Trace.Assert(t.IsSerializable, "Тип " + t.Name + " не сериализуем");

            using (MemoryStream stream = new MemoryStream())
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(stream, value);
                stream.Flush();
                stream.Seek(0, SeekOrigin.Begin);
                return (T)formatter.Deserialize(stream);
            }
        }
    }
}
