using System;
using ICloneBaseLibrary.Classes;
using ICloneBaseLibrary.Interfaces;
using ISourceFilesLibrary.Classes.HashCodeAlgorithms;
using ISourceFilesLibrary.Classes.StringEqualityAlgorithms;

namespace ISourceFilesLibrary.Classes.CodeUnit
{
   /// <summary>
   /// Единица кода
   /// </summary>
   [Serializable]
   public sealed class CCodeUnit : ICodeUnit, IElementPosition, ICloneable, IComparable, IEquatable<CCodeUnit>, IEquatableObject, IEquatableObject<CCodeUnit>
   {
      #region // Глобальные параметры

      private static bool m_UseTokens = true;// false;

      /// <summary>
      /// Использовать токены для представления единиц кода
      /// </summary>
      /// <returns></returns>
      public static bool IsUseTokens()
      {
         return m_UseTokens;
      }

      #endregion

      #region // Поля класса

      /// <summary>
      /// Местоположение единицы кода
      /// </summary>
      private CElementPosition m_Position;

      /// <summary>
      /// Содержимое единицы кода
      /// </summary>
      private string m_Text;

      /// <summary>
      /// Алгоритм вычисления хэш-кода
      /// </summary>
      private static CBaseHashCodeGenerateStrategy m_HashCodeGenerator = new CSimpleHashCodeAlgorithm();

      /// <summary>
      /// Алгоритм сравнения строк
      /// </summary>
      private static CBaseStringEqualityStrategy m_StringEqualizer = new CSimpleStringEqualityAlgorithm();

      #endregion

      /// <summary>
      /// 
      /// </summary>
      /// <param name="position"></param>
      /// <param name="text"></param>
      public CCodeUnit(CElementPosition position, string text)
      {
         m_Position = position;
         Text = text;
      }

      /// <summary>
      /// 
      /// </summary>
      /// <param name="other"></param>
      public CCodeUnit(CCodeUnit other)
      {
         m_Position = new CElementPosition(other.m_Position);
         Text = other.Text;
      }

      #region // ICodeUnit

      /// <summary>
      /// Содержимое единицы кода
      /// </summary>
      public string Text
      {
         get
         {
            return m_Text;
         }

         set
         {
            if (value == null)
               throw new ArgumentNullException("Text");
            m_Text = value;
         }
      }

      #endregion

      #region // Реализация интерфейса IElementPosition

      /// <summary>
      /// Номер строки, в которой начинается элемент кода
      /// </summary>
      public long LineStart
      {
         get
         {
            return m_Position.LineStart;
         }

         set
         {
            m_Position.LineStart = value;
         }
      }

      /// <summary>
      /// Номер строки, в которой заканчивается элемент кода
      /// </summary>
      public long LineEnd
      {
         get
         {
            return m_Position.LineEnd;
         }

         set
         {
            m_Position.LineEnd = value;
         }
      }

      /// <summary>
      /// Номер позиции в строке, где начинается элемент кода
      /// </summary>
      public int IndexStart
      {
         get
         {
            return m_Position.IndexStart;
         }

         set
         {
            m_Position.IndexStart = value;
         }
      }

      /// <summary>
      /// Номер позиции в строке, где заканчивается элемент кода
      /// </summary>
      public int IndexEnd
      {
         get
         {
            return m_Position.IndexEnd;
         }

         set
         {
            m_Position.IndexEnd = value;
         }
      }

      #endregion

      #region // Реализация интерфейса IEquatable<T>

      /// <summary>
      /// Определяет, равны ли две единицы исходного кода.
      /// Для сравнения может использоваться один из семейства алгоритмов <see cref="CBaseStringEqualityStrategy"/>
      /// </summary>
      /// <param name="other"></param>
      /// <returns></returns>
      public bool Equals(CCodeUnit other)
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

         return m_StringEqualizer.Equals(m_Text, other.m_Text);
      }

      #endregion

      #region // Реализация интерфейса IEquatableObject

      /// <summary>
      /// Указывает, равен ли текущий объект другому объекту
      /// </summary>
      /// <param name="obj"></param>
      /// <returns></returns>
      public bool EqualsObject(object obj)
      {
         return EqualsObject(obj as CCodeUnit);
      }

      #endregion

      #region // Реализация интерфейса IEquatableObject<T>

      /// <summary>
      /// Указывает, равен ли текущий объект другому объекту того же типа
      /// </summary>
      /// <param name="other"></param>
      /// <returns></returns>
      public bool EqualsObject(CCodeUnit other)
      {
         if (other == null)
            return false;

         if (Object.ReferenceEquals(this, other))
            return true;

         return (m_Position.Equals(other.m_Position) && Equals(other));
      }

      #endregion

      #region // Реализация интерфейса ICloneable

      /// <summary>
      /// Клонирование объекта
      /// </summary>
      /// <returns></returns>
      public object Clone()
      {
         return (object)CObjectCloner.DeepCopy(this);
      }

      #endregion

      #region // Реализация интерфейса IComparable

      /// <summary>
      /// Сравнивает текущий экземпляр с другим объектом того же типа и возвращает целое число, которое показывает,
      /// расположен ли текущий экземпляр перед, после или на той же позиции в порядке сортировки, что и другой объект.
      /// </summary>
      /// <remarks>
      /// Для любых объектов A, B и C должно быть верно следующее.
      /// Метод A.CompareTo(A) должен возвращать нуль.
      /// Если метод A.CompareTo(B) возвращает нуль, то метод B.CompareTo(A) должен возвращать нуль.
      /// Если методы A.CompareTo(B) и B.CompareTo(C) возвращают нуль, то метод A.CompareTo(C) должен возвращать нуль.
      /// Если метод A.CompareTo(B) возвращает значение, отличное от нуля, то метод B.CompareTo(A) должен возвращать значение с противоположным знаком.
      /// Если вызов метода A.CompareTo(B) возвращает значение x, отличное от нуля, а вызов метода B.CompareTo(C) возвращает значение y с тем же знаком, что и значение x, то вызов метода A.CompareTo(C) должен возвращать значение с тем же знаком, что и у значений x и y.
      /// </remarks>
      /// <param name="obj">Объект для сравнения с данным экземпляром</param>
      /// <returns>Значение, указывающее, каков относительный порядок сравниваемых объектов.
      /// Меньше нуля - Этот экземпляр меньше параметра obj.
      /// Нуль - Этот экземпляр и параметр obj равны.
      /// Больше нуля - Этот экземпляр больше параметра obj.</returns>
      /// <exception cref="ArgumentException">Тип значения параметра obj отличается от типа данного экземпляра</exception>
      public int CompareTo(object obj)
      {
         CCodeUnit other = obj as CCodeUnit;
         if (other != null)
         {
            if (!EqualsObject(other))
            {
               if (m_Position.Equals(other.m_Position))
               {
                  return m_Text.CompareTo(other.m_Text);
               }
               else
                  return m_Position.CompareTo(other.m_Position);
            }
         }
         else
            throw new ArgumentException();

         return 0;
      }

      #endregion

      /// <summary>
      /// Определяет, равны ли две единицы кода
      /// Для сравнения может использоваться один из семейства алгоритмов <see cref="CBaseStringEqualityStrategy"/>
      /// </summary>
      /// <param name="obj"></param>
      /// <returns></returns>
      public override bool Equals(object obj)
      {
         if (obj == null)
         {
            return false;
         }

         CCodeUnit p = obj as CCodeUnit;
         if ((object)p == null)
         {
            return false;
         }

         return Equals(p);
      }

      /// <summary>
      /// Хэш для данной единицы кода
      /// Для вычисления хэша может использоваться один из семейства алгоритмов <see cref="CBaseHashCodeGenerateStrategy"/>
      /// </summary>
      /// <returns></returns>
      public override int GetHashCode()
      {
         return m_HashCodeGenerator.GetHashCode(m_Text);
      }
   }
}