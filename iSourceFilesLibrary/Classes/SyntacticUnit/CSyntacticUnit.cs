using ISourceFilesLibrary.Classes.CodeFragment;
using ISourceFilesLibrary.Classes.CodeUnit;
using ICloneBaseLibrary.Interfaces;
using System;

namespace ISourceFilesLibrary.Classes.SyntacticUnit
{
   /// <summary>
   /// Синтаксическая единица кода
   /// </summary>
   public sealed class CSyntacticUnit : IElementPosition, ISizeableElement, IEquatableObject<CSyntacticUnit>
   {
      private CCodeFragment m_Content;

      /// <summary>
      /// 
      /// </summary>
      /// <param name="_Content"></param>
      public CSyntacticUnit(CCodeFragment _Content)
      {
         m_Content = _Content;
      }

      #region // Реализация интерфейса IElementPosition

      /// <summary>
      /// Номер строки, в которой начинается синтаксическая единица
      /// </summary>
      public long LineStart
      {
         get
         {
            return m_Content.LineStart;
         }

         set
         {
            m_Content.LineStart = value;
         }
      }

      /// <summary>
      /// Номер строки, в которой заканчивается синтаксическая единица
      /// </summary>
      public long LineEnd
      {
         get
         {
            return m_Content.LineEnd;
         }

         set
         {
            m_Content.LineEnd = value;
         }
      }

      /// <summary>
      /// Номер позиции в строке, где начинается синтаксическая единица
      /// </summary>
      public int IndexStart
      {
         get
         {
            return m_Content.IndexStart;
         }

         set
         {
            m_Content.IndexStart = value;
         }
      }

      /// <summary>
      /// Номер позиции в строке, где заканчивается синтаксическая единица
      /// </summary>
      public int IndexEnd
      {
         get
         {
            return m_Content.IndexEnd;
         }

         set
         {
            m_Content.IndexEnd = value;
         }
      }

      #endregion

      /// <summary>
      /// Размер синтаксической единицы (в единицах кода)
      /// </summary>
      /// <returns></returns>
      public long Size()
      {
         return m_Content.Size();
      }

      #region // Реализация интерфейса IEquatableObject<T>

      /// <summary>
      /// Указывает, равен ли текущий объект другому объекту того же типа
      /// </summary>
      /// <param name="other"></param>
      /// <returns></returns>
      public bool EqualsObject(CSyntacticUnit other)
      {
         if (other == null)
            return false;

         if (object.ReferenceEquals(this, other))
            return true;

         return m_Content.EqualsObject(other.m_Content);
      }

      #endregion
   }
}