using System;
using ISourceFilesLibrary.Classes.CodeUnit;
using ISourceFilesLibrary.Classes.CodeUnitsCollection;
using ICloneBaseLibrary.Interfaces;

namespace ISourceFilesLibrary.Classes.CodeFragment
{
   /// <summary>
   /// Базовый класс, представляющий фрагмент исходного кода
   /// </summary>
   [Serializable]
   public sealed class CCodeFragment : IElementPosition, ISizeableElement, IEquatable<CCodeFragment>, IEquatableObject<CCodeFragment>
   {
      #region // Статические члены

      private static long m_Kmin = 20;

      /// <summary>
      /// Минимально допустимый размер дублирующегося фрагмента кода
      /// </summary>
      public static long Kmin
      {
         get
         {
            return m_Kmin;
         }
      }

      /// <summary>
      /// Задать минимально допустимый размер дублирующегося фрагмента кода
      /// </summary>
      /// <param name="value"></param>
      public static void SetKmin(long value)
      {
         if (value < 1)
            throw new ArgumentOutOfRangeException();
      }

      #endregion

      #region // Поля класса

      /// <summary>
      /// Содержимое фрагмента кода
      /// </summary>
      private CCodeUnitsCollection m_Content;

      #endregion

      #region // Конструкторы

      /// <summary>
      /// 
      /// </summary>
      public CCodeFragment()
      {
         m_Content = new CCodeUnitsCollection();
      }

      /// <summary>
      /// 
      /// </summary>
      /// <param name="_Content"></param>
      public CCodeFragment(CCodeUnitsCollection _Content)
      {
         m_Content = _Content;
      }

      #endregion

      #region // Реализация интерфейса IElementPosition

      /// <summary>
      /// Номер строки, в которой начинается фрагмент кода
      /// </summary>
      public long LineStart
      {
         get
         {
            return m_Content.front().LineStart;
         }

         set
         {
            m_Content.front().LineStart = value;
         }
      }

      /// <summary>
      /// Номер строки, в которой заканчивается фрагмент кода
      /// </summary>
      public long LineEnd
      {
         get
         {
            return m_Content.back().LineEnd;
         }

         set
         {
            m_Content.back().LineEnd = value;
         }
      }

      /// <summary>
      /// Номер позиции в строке, где начинается фрагмент кода
      /// </summary>
      public int IndexStart
      {
         get
         {
            return m_Content.front().IndexStart;
         }

         set
         {
            m_Content.front().IndexStart = value;
         }
      }

      /// <summary>
      /// Номер позиции в строке, где заканчивается фрагмент кода
      /// </summary>
      public int IndexEnd
      {
         get
         {
            return m_Content.back().IndexEnd;
         }

         set
         {
            m_Content.back().IndexEnd = value;
         }
      }

      #endregion

      /// <summary>
      /// Проверяет, принадлежит ли этот фрагмент кода одной строке или нескольким
      /// </summary>
      /// <returns>Возвращает истину, если фрагмент кода принадлежит одной строке</returns>
      public bool IsBelongOneLine()
      {
         return (LineStart == LineEnd);
      }

      /// <summary>
      /// Содержимое фрагмента кода
      /// </summary>
      public CCodeUnitsCollection Content
      {
         get
         {
            return m_Content;
         }

         set
         {
            m_Content = value;
         }
      }

      /// <summary>
      /// Размер фрагмента (в единицах кода)
      /// </summary>
      /// <returns></returns>
      public long Size()
      {
         return m_Content.Size();
      }

      #region // Реализация интерфейса IEquatable<T>

      /// <summary>
      /// Определяет, равны ли 2 фрагмента кода
      /// </summary>
      /// <param name="other"></param>
      /// <returns></returns>
      public bool Equals(CCodeFragment other)
      {
         if (other == null)
            return false;

         if (Object.ReferenceEquals(this, other))
            return true;

         return m_Content.Equals(other.m_Content);
      }

      #endregion

      #region // Реализация интерфейса IEquatableObject<T>

      /// <summary>
      /// Указывает, равен ли текущий объект другому объекту того же типа
      /// </summary>
      /// <param name="other"></param>
      /// <returns></returns>
      public bool EqualsObject(CCodeFragment other)
      {
         return Equals(other);
      }

      #endregion

      /// <summary>
      /// Равны ли 2 фрагмента кода
      /// </summary>
      /// <param name="obj"></param>
      /// <returns></returns>
      public override bool Equals(object obj)
      {
         return Equals(obj as CCodeFragment);
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