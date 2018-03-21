using System;
using ICloneBaseLibrary.Classes;
using ISourceFilesLibrary.Classes.CodeUnit;

namespace ISourceFilesLibrary.Classes.CodeFragment
{
   /// <summary>
   /// Класс, представляющий закомментированный фрагмент исходного кода
   /// </summary>
   [Serializable]
   public sealed class CCommentedCodeFragment : IElementPosition, IEquatable<CCommentedCodeFragment>
   {
      #region // Поля класса

      /// <summary>
      /// Символы комментариев, которые начинают и завершают этот фрагмент кода
      /// </summary>
      private CPair<string> m_CommentSymbolPair;

      private CElementPosition m_Position;

      #endregion

      #region // Конструкторы

      /// <summary>
      /// 
      /// </summary>
      /// <param name="_Position"></param>
      /// <param name="CommentPair">Символы комментариев, которые начинают и завершают этот фрагмент кода</param>
      public CCommentedCodeFragment(CElementPosition _Position, CPair<string> CommentPair)
      {
         m_CommentSymbolPair = CommentPair;
         m_Position = _Position;
      }

      #endregion

      #region

      /// <summary>
      /// 
      /// </summary>
      /// <param name="_LineEnd"></param>
      /// <param name="_IndexEnd"></param>
      public void SetEnding(long _LineEnd, int _IndexEnd)
      {
         m_Position.LineEnd = _LineEnd;
         m_Position.IndexEnd = _IndexEnd;
      }

      /// <summary>
      /// Символы комментариев, которые начинают и завершают этот фрагмент кода
      /// </summary>
      public CPair<string> CommentSymbolPair
      {
         get
         {
            return m_CommentSymbolPair;
         }

         set
         {
            m_CommentSymbolPair = value;
         }
      }

      #endregion

      /// <summary>
      /// Проверяет, принадлежит ли этот фрагмент кода одной строке или нескольким.
      /// Параметр _LineEnd определяет номер строки, в которой заканчивается фрагмент кода.
      /// </summary>
      /// <param name="_LineEnd">Номер строки, в которой заканчивается фрагмент кода</param>
      /// <returns>Возвращает истину, если фрагмент кода принадлежит одной строк</returns>
      public bool IsBelongOneLine(long _LineEnd)
      {
         return (m_Position.LineStart == _LineEnd);
      }

      /// <summary>
      /// Проверяет, принадлежит ли этот фрагмент кода одной строке или нескольким
      /// </summary>
      /// <returns>Возвращает истину, если фрагмент кода принадлежит одной строке</returns>
      public bool IsBelongOneLine()
      {
         return (m_Position.LineStart == m_Position.LineEnd);
      }

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
      /// Определяет, равны ли 2 фрагмента кода
      /// </summary>
      /// <param name="other"></param>
      /// <returns></returns>
      public bool Equals(CCommentedCodeFragment other)
      {
         if (other == null)
            return false;

         if (Object.ReferenceEquals(this, other))
            return true;

         return (m_CommentSymbolPair.Equals(other.m_CommentSymbolPair) && m_Position.Equals(other.m_Position));
      }

      #endregion

      /// <summary>
      /// Определяет, равны ли 2 фрагмента кода
      /// </summary>
      /// <param name="obj"></param>
      /// <returns></returns>
      public override bool Equals(object obj)
      {
         return Equals(obj as CCommentedCodeFragment);
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