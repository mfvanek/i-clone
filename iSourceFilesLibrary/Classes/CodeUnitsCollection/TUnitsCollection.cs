using System;
using System.Collections;
using System.Collections.Generic;
using ICloneBaseLibrary.Interfaces;
using ISourceFilesLibrary.Classes.CodeUnit;

namespace ISourceFilesLibrary.Classes.CodeUnitsCollection
{
   /// <summary>
   /// Обобщенная коллекция единиц кода
   /// </summary>
   [Serializable]
   public class TUnitsCollection<T> : IEnumerable<T>, ISizeableElement where T : IEquatableObject<T>
   {
      /// <summary>
      /// Список единиц кода
      /// </summary>
      private List<T> m_Collection;

      /// <summary>
      /// Конструктор по умолчанию
      /// </summary>
      public TUnitsCollection()
      {
         m_Collection = new List<T>();
      }

      /// <summary>
      /// 
      /// </summary>
      /// <param name="units"></param>
      public TUnitsCollection(List<T> units)
      {
         m_Collection = units;
      }

      #region

      /// <summary>
      /// 
      /// </summary>
      /// <param name="index"></param>
      /// <returns></returns>
      public virtual T this[int index]
      {
         get
         {
            return m_Collection[index];
         }

         set
         {
            m_Collection[index] = value;
         }
      }

      /// <summary>
      /// Представить в виде массива
      /// </summary>
      /// <returns></returns>
      public virtual T[] ToArray()
      {
         return m_Collection.ToArray();
      }

      /// <summary>
      /// Представить в виде списка
      /// </summary>
      /// <returns></returns>
      public virtual List<T> ToList()
      {
         return m_Collection;
      }

      /// <summary>
      /// Задать новый список единиц кода
      /// </summary>
      /// <param name="units"></param>
      public virtual void SetCodeUnits(List<T> units)
      {
         m_Collection = units;
      }

      /// <summary>
      /// Добавить строку исходного кода в коллекцию
      /// </summary>
      /// <param name="item"></param>
      public virtual void Add(T item)
      {
         m_Collection.Add(item);
      }

      /// <summary>
      /// Удалить строку с заданным номером
      /// </summary>
      /// <param name="index">Номер строки, которую требуется удалить</param>
      public virtual void RemoveRow(int index)
      {
         m_Collection.RemoveAt(index);
      }

      /// <summary>
      /// Размер коллекции (в единицах кода)
      /// </summary>
      /// <returns></returns>
      public long Size()
      {
         return m_Collection.Count;
      }

      /// <summary>
      /// Очистить
      /// </summary>
      public void Clear()
      {
         m_Collection.Clear();
      }

      /// <summary>
      /// Добавить набор элементов
      /// </summary>
      /// <param name="collection"></param>
      public void AddRange(IEnumerable<T> collection)
      {
         m_Collection.AddRange(collection);
      }

      #endregion

      #region // Вспомогательные методы

      /// <summary>
      /// Получить первый элемент в коллекции
      /// </summary>
      /// <returns></returns>
      public T front()
      {
         return m_Collection[0];
      }

      /// <summary>
      /// Получить последний элемент в коллекции
      /// </summary>
      /// <returns></returns>
      public T back()
      {
         return m_Collection[m_Collection.Count - 1];
      }

      #endregion

      /// <summary>
      /// 
      /// </summary>
      /// <returns></returns>
      public IEnumerator<T> GetEnumerator()
      {
         foreach (T unit in m_Collection)
            yield return unit;
      }

      System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
      {
         return this.GetEnumerator();
      }

      /// <summary>
      /// Определяет, равны ли 2 набора единиц кода. Используется точное равенство.
      /// </summary>
      /// <param name="obj"></param>
      /// <returns></returns>
      public override bool Equals(object obj)
      {
         if (obj == null)
            return false;

         TUnitsCollection<T> p = obj as TUnitsCollection<T>;
         if ((object)p == null)
            return false;

         if (m_Collection.Count != p.m_Collection.Count)
            return false;
         else
         {
            for (int counter = 0; counter < m_Collection.Count; ++counter)
            {
               if (!m_Collection[counter].EqualsObject(p.m_Collection[counter]))
               {
                  return false;
               }
            }
         }

         return true;
      }

      /// <summary>
      /// Хэш-код
      /// </summary>
      /// <returns></returns>
      public override int GetHashCode()
      {
         return base.GetHashCode();
      }
   }
}