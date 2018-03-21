using System;
using ICloneBaseLibrary.Classes;
using ICloneBaseLibrary.Interfaces;
using ISourceFilesLibrary.Classes.StringEqualityAlgorithms;

namespace ISourceFilesLibrary.Classes.CodeUnit
{
   /// <summary>
   /// "Расширенная" единица кода
   /// </summary>
   [Serializable]
   public sealed class CExtendedCodeUnit : ICodeUnit, IElementPosition, ICloneable, IComparable, IEquatable<CExtendedCodeUnit>, IEquatableObject, IEquatableObject<CExtendedCodeUnit>
   {
      /// <summary>
      /// Идентификатор файла по умолчанию.
      /// </summary>
      public const long DEFAULT_SOURCE_FILE_ID = 0;

      #region // Поля класса

      /// <summary>
      /// Идентификатор файла, которому принадлежит единица кода
      /// </summary>
      private long m_SourceFileID;

      /// <summary>
      /// Единица кода
      /// </summary>
      private CCodeUnit m_CodeUnit;

      /// <summary>
      /// Уникальный номер единицы кода в общем рабочем пространстве
      /// </summary>
      private long m_NumberInGlobalWorkspace;

      #endregion

      public CExtendedCodeUnit(CCodeUnit _CodeUnit, long _SourceFileID, long _NumberInGlobalWorkspace)
      {
         m_CodeUnit = _CodeUnit;

         if (_SourceFileID > DEFAULT_SOURCE_FILE_ID)
            m_SourceFileID = _SourceFileID;
         else
            throw new ArgumentOutOfRangeException("SourceFileID");

         if (_NumberInGlobalWorkspace >= CElementPosition.LINE_NUMBER_LOW_BOUND)
            m_NumberInGlobalWorkspace = _NumberInGlobalWorkspace;
         else
            throw new ArgumentOutOfRangeException("NumberInGlobalWorkspace");
      }

      public CExtendedCodeUnit(CCodeUnit _CodeUnit, long _SourceFileID)
         : this(_CodeUnit, _SourceFileID, CElementPosition.LINE_NUMBER_LOW_BOUND)
      { }

      /// <summary>
      /// Копирующий конструктор
      /// </summary>
      /// <param name="other"></param>
      public CExtendedCodeUnit(CExtendedCodeUnit other)
      {
         m_CodeUnit = new CCodeUnit(other.m_CodeUnit);
         m_SourceFileID = other.m_SourceFileID;
         m_NumberInGlobalWorkspace = other.m_NumberInGlobalWorkspace;
      }

      #region // ICodeUnit

      /// <summary>
      /// Содержимое единицы кода
      /// </summary>
      public string Text
      {
         get
         {
            return m_CodeUnit.Text;
         }

         set
         {
            m_CodeUnit.Text = value;
         }
      }

      #endregion

      /// <summary>
      /// Идентификатор файла, которому принадлежит единица кода
      /// </summary>
      public long SourceFileID
      {
         get
         {
            return m_SourceFileID;
         }
         //set
         //{
         //   if (value > CSourceFile.DEFAULT_SOURCE_FILE_ID)
         //      m_SourceFileID = value;
         //   else
         //      throw new ArgumentOutOfRangeException("SourceFileID");
         //}
      }

      public long NumberInGlobalWorkspace
      {
         get
         {
            return m_NumberInGlobalWorkspace;
         }

         set
         {
            m_NumberInGlobalWorkspace = value;
         }
      }

      #region // Реализация интерфейса IElementPosition

      /// <summary>
      /// Номер строки, в которой начинается элемент кода
      /// </summary>
      public long LineStart
      {
         get
         {
            return m_CodeUnit.LineStart;
         }

         set
         {
            m_CodeUnit.LineStart = value;
         }
      }

      /// <summary>
      /// Номер строки, в которой заканчивается элемент кода
      /// </summary>
      public long LineEnd
      {
         get
         {
            return m_CodeUnit.LineEnd;
         }

         set
         {
            m_CodeUnit.LineEnd = value;
         }
      }

      /// <summary>
      /// Номер позиции в строке, где начинается элемент кода
      /// </summary>
      public int IndexStart
      {
         get
         {
            return m_CodeUnit.IndexStart;
         }

         set
         {
            m_CodeUnit.IndexStart = value;
         }
      }

      /// <summary>
      /// Номер позиции в строке, где заканчивается элемент кода
      /// </summary>
      public int IndexEnd
      {
         get
         {
            return m_CodeUnit.IndexEnd;
         }

         set
         {
            m_CodeUnit.IndexEnd = value;
         }
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
         CExtendedCodeUnit other = obj as CExtendedCodeUnit;
         if (other != null)
         {
            if (!EqualsObject(other))
            {
               if (m_SourceFileID == other.m_SourceFileID)
               {
                  if (m_NumberInGlobalWorkspace == other.m_NumberInGlobalWorkspace)
                     return m_CodeUnit.CompareTo(other.m_CodeUnit);
                  else
                     return m_NumberInGlobalWorkspace.CompareTo(other.m_NumberInGlobalWorkspace);
               }
               else
                  return m_SourceFileID.CompareTo(other.m_SourceFileID);
            }
         }
         else
            throw new ArgumentException();

         return 0;
      }

      #endregion

      #region // Реализация интерфейса IEquatable<T>

      /// <summary>
      /// Определяет, равны ли две единицы исходного кода.
      /// Для сравнения может использоваться один из семейства алгоритмов <see cref="CBaseStringEqualityStrategy"/>
      /// </summary>
      /// <param name="other"></param>
      /// <returns></returns>
      public bool Equals(CExtendedCodeUnit other)
      {
         if (other == null)
         {
            return false;
         }
         return m_CodeUnit.Equals(other.m_CodeUnit);
      }

      #endregion

      #region // Реализация интерфейса IEquatableObject<T>

      /// <summary>
      /// Указывает, равен ли текущий объект другому объекту того же типа
      /// </summary>
      /// <param name="other"></param>
      /// <returns></returns>
      public bool EqualsObject(CExtendedCodeUnit other)
      {
         if (other == null)
            return false;

         if (Object.ReferenceEquals(this, other))
            return true;

         return (m_SourceFileID == other.m_SourceFileID &&
                 m_NumberInGlobalWorkspace == other.m_NumberInGlobalWorkspace &&
                 m_CodeUnit.EqualsObject(other.m_CodeUnit));
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
         if (obj == null)
            return false;

         CExtendedCodeUnit p = obj as CExtendedCodeUnit;
         if (p == null)
            return false;

         return EqualsObject(p);
      }

      #endregion

      /// <summary>
      /// 
      /// </summary>
      /// <param name="other"></param>
      /// <returns></returns>
      public override bool Equals(object other)
      {
         return Equals(other as CExtendedCodeUnit);
      }

      /// <summary>
      /// 
      /// </summary>
      /// <returns></returns>
      public override int GetHashCode()
      {
         return m_CodeUnit.GetHashCode();
      }
   }
}