using System;

namespace ISourceFilesLibrary.Classes.CodeUnit
{
   /// <summary>
   /// Местоположение элемента кода
   /// </summary>
   [Serializable]
   public sealed class CElementPosition : IElementPosition, IComparable, IEquatable<CElementPosition>
   {
      #region // Константы

      /// <summary>
      /// Нижняя граница диапазона номеров строк
      /// </summary>
      public const long LINE_NUMBER_LOW_BOUND = 1;

      /// <summary>
      /// Нижняя граница диапазона индексов в строке
      /// </summary>
      public const int INDEX_NUMBER_LOW_BOUND = 0;

      #endregion

      #region // Поля класса

      /// <summary>
      /// Номер строки, в которой начинается единица кода
      /// </summary>
      private long m_LineStart;

      /// <summary>
      /// Номер позиции в строке, где начинается единица кода
      /// </summary>
      private int m_IndexStart;

      /// <summary>
      /// Номер строки, в которой заканчивается единица кода
      /// </summary>
      private long m_LineEnd;

      /// <summary>
      /// Номер позиции в строке, где заканчивается единица кода
      /// </summary>
      private int m_IndexEnd;

      #endregion

      #region // Конструкторы

      public CElementPosition(long line_start, int index_start, long line_end, int index_end)
      {
         m_LineEnd = long.MaxValue;
         m_IndexEnd = int.MaxValue;

         LineStart = line_start;
         IndexStart = index_start;
         LineEnd = line_end;
         IndexEnd = index_end;
      }

      public CElementPosition(long line_start, int index_start)
         : this(line_start, index_start, line_start, index_start)
      { }

      /// <summary>
      /// Копирующий конструктор
      /// </summary>
      /// <param name="other"></param>
      public CElementPosition(CElementPosition other)
      {
         m_LineStart = other.m_LineStart;
         m_IndexStart = other.m_IndexStart;
         m_LineEnd = other.m_LineEnd;
         m_IndexEnd = other.m_IndexEnd;
      }

      #endregion

      private double size()
      {
         return Math.Sqrt(Math.Pow(m_LineEnd - m_LineStart, 2.0) + Math.Pow(m_IndexEnd - m_IndexStart, 2.0));
      }

      #region // Реализация интерфейса IElementPosition

      /// <summary>
      /// Номер строки, в которой начинается единица кода
      /// </summary>
      public long LineStart
      {
         get
         {
            return m_LineStart;
         }

         set
         {
            if (value >= LINE_NUMBER_LOW_BOUND && value <= m_LineEnd)
               m_LineStart = value;
            else
               throw new InvalidElementPositionException("LineStart");
         }
      }

      /// <summary>
      /// Номер строки, в которой заканчивается единица кода
      /// </summary>
      public long LineEnd
      {
         get
         {
            return m_LineEnd;
         }

         set
         {
            if (value >= LINE_NUMBER_LOW_BOUND && value >= m_LineStart)
               m_LineEnd = value;
            else
               throw new InvalidElementPositionException("LineEnd");
         }
      }

      /// <summary>
      /// Номер позиции в строке, где начинается единица кода
      /// </summary>
      public int IndexStart
      {
         get
         {
            return m_IndexStart;
         }

         set
         {
            if (value >= INDEX_NUMBER_LOW_BOUND)
            {
               if (m_LineStart == m_LineEnd && value > m_IndexEnd)
               {
                  throw new InvalidElementPositionException("IndexStart");
               }

               m_IndexStart = value;
            }
            else
               throw new InvalidElementPositionException("IndexStart");
         }
      }

      /// <summary>
      /// Номер позиции в строке, где заканчивается единица кода
      /// </summary>
      public int IndexEnd
      {
         get
         {
            return m_IndexEnd;
         }

         set
         {
            if (value >= INDEX_NUMBER_LOW_BOUND)
            {
               if (m_LineStart == m_LineEnd && value < m_IndexStart)
               {
                  throw new InvalidElementPositionException("IndexEnd");
               }

               m_IndexEnd = value;
            }
            else
               throw new InvalidElementPositionException("IndexEnd");
         }
      }

      #endregion

      #region // Реализация интерфейса IEquatable<T>

      /// <summary>
      /// Определяет, равны ли позиции двух единиц кода
      /// </summary>
      /// <param name="other"></param>
      /// <returns></returns>
      public bool Equals(CElementPosition other)
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

         return (m_LineStart == other.m_LineStart && m_LineEnd == other.m_LineEnd && m_IndexStart == other.m_IndexStart && m_IndexEnd == other.m_IndexEnd);
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
         CElementPosition other = obj as CElementPosition;
         if (other != null)
         {
            if (!Equals(other))
            {
               return size().CompareTo(other.size());
            }
         }
         else
            throw new ArgumentException();

         return 0;
      }

      #endregion
   }
}