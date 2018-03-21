using System;
using System.Collections.Generic;
using ICloneBaseLibrary.Classes;
using ISourceFilesLibrary.Classes.CodeFragment;
using ISourceFilesLibrary.Classes.CodeUnit;
using ISourceFilesLibrary.Classes.CodeUnitsCollection;

namespace CodeLoadingLibrary.Classes
{
   /// <summary>
   /// Простейший алгоритм предобрабоки кода
   /// </summary>
   public sealed class CSimpleCodePreProcessingAlgorithm : CCodePreProcessingStrategy
   {
      #region // Члены класса

      private CCodeUnitsCollection m_ModifiedCollection;

      private const char carriage_return = '\r';

      /// <summary>
      /// Стек для поиска и хранения закомментированных фрагментов кода
      /// </summary>
      private Stack<CCommentedCodeFragment> m_PairCommentStack;
      /// <summary>
      /// Список фрагментов комментариев, принадлежащих одной строке
      /// </summary>
      private List<CCommentedCodeFragment> m_OnelineCodeFragments;
      /// <summary>
      /// Обрабатываемый символ комментария
      /// </summary>
      private string m_ProcessingCommentSymbol;
      /// <summary>
      /// Индекс в строке, с которого начинается обрабатываемый в данный момент символ комментария
      /// </summary>
      private int m_IndexOfCommentSymbolBeginning;

      #endregion

      #region // Закрытые вспомогательные функции

      private void ClearProcessingCommentSymbol()
      {
         m_ProcessingCommentSymbol = string.Empty;
         m_IndexOfCommentSymbolBeginning = CElementPosition.INDEX_NUMBER_LOW_BOUND - 1;
      }

      /// <summary>
      /// Проинициализировать поля класса
      /// </summary>
      private void InitMembers()
      {
         m_ModifiedCollection = new CCodeUnitsCollection();
         m_PairCommentStack = new Stack<CCommentedCodeFragment>();
         m_OnelineCodeFragments = new List<CCommentedCodeFragment>();
         ClearProcessingCommentSymbol();
      }

      private void ClearMembers()
      {
         m_PairCommentStack.Clear();
         m_OnelineCodeFragments.Clear();
         ClearProcessingCommentSymbol();
      }

      private void AddFragment(int _LineEnd, int _IndexEnd)
      {
         if (m_PairCommentStack.Count > 0)
         {
            CCommentedCodeFragment ready_fragment = m_PairCommentStack.Pop();
            ready_fragment.SetEnding(_LineEnd, _IndexEnd);
            if (ready_fragment.IsBelongOneLine())
            {
               m_OnelineCodeFragments.Add(ready_fragment);
            }
            else
            {
               CCommentedCodeFragment new_fragment = new CCommentedCodeFragment(new CElementPosition(_LineEnd, 0, _LineEnd, _IndexEnd), ready_fragment.CommentSymbolPair);
               m_OnelineCodeFragments.Add(new_fragment);
            }
         }
      }

      private int GetRealLineNumber(int rows_counter)
      {
         return rows_counter + 1;
      }

      #endregion

      /// <summary>
      /// Конструктор по умолчанию
      /// </summary>
      public CSimpleCodePreProcessingAlgorithm()
      {
         InitMembers();
      }

      /// <summary>
      /// Название алгоритма предварительной обработки программного кода
      /// </summary>
      /// <returns></returns>
      public override string CodePreProcessingAlgorithmName()
      {
         return "SimpleCodePreProcessingAlgorithm";
      }

      /// <summary>
      /// 
      /// </summary>
      /// <param name="PreProcessingOptions"></param>
      /// <param name="OriginalCodeUnitsCollection"></param>
      /// <returns></returns>
      public override CCodeUnitsCollection PreProcessing(CCodePreProcessingOptions PreProcessingOptions, CCodeUnitsCollection OriginalCodeUnitsCollection)
      {
         ClearMembers();
         m_ModifiedCollection.Clear();

         string ScanningSymbol = string.Empty;

         for (int units_counter = 0; units_counter < OriginalCodeUnitsCollection.Size(); ++units_counter)
         {
            CExtendedCodeUnit current_unit = OriginalCodeUnitsCollection[units_counter];
            CExtendedCodeUnit new_codeunit = new CExtendedCodeUnit(current_unit);

            #region // Удаление комментариев из кода

            if (PreProcessingOptions.DeleteComments)
            {
               m_OnelineCodeFragments.Clear();
               ClearProcessingCommentSymbol();

               // Суть алгоритма в следующем: перебираем все строки; в каждой строке перебираем все символы.
               // Если текущий символ является одним из возможных символов комментариев, то добавляем его в строку.
               // Если длина этой строки лежит в пределах от минимальной до максимальной из возможных длин комментариев, то проверяем является ли эта строка комментарием.
               for (int char_counter = 0; char_counter < current_unit.Text.Length; char_counter++)
               {
                  char current_char = current_unit.Text[char_counter];

                  if (PreProcessingOptions.CommentSymbols.IsCommentChar(current_char))
                  {
                     m_ProcessingCommentSymbol += current_char;
                     m_IndexOfCommentSymbolBeginning = (m_IndexOfCommentSymbolBeginning == CElementPosition.INDEX_NUMBER_LOW_BOUND - 1) ? char_counter : m_IndexOfCommentSymbolBeginning;

                     if (m_ProcessingCommentSymbol.Length >= PreProcessingOptions.CommentSymbols.MinCommentLength)
                     {
                        if (m_ProcessingCommentSymbol.Length <= PreProcessingOptions.CommentSymbols.MaxCommentLength)
                        {
                           if (!String.IsNullOrEmpty(ScanningSymbol))
                           {
                              if (m_ProcessingCommentSymbol == ScanningSymbol)
                              {
                                 AddFragment(GetRealLineNumber(units_counter), char_counter);
                                 ScanningSymbol = string.Empty;
                                 ClearProcessingCommentSymbol();
                              }
                              else
                              {
                                 if (m_ProcessingCommentSymbol.Length >= ScanningSymbol.Length)
                                 {
                                    ClearProcessingCommentSymbol();
                                 }
                              }
                           }
                           else
                           {
                              // Попробуем  поискать такой символ комментария, при этом нужно узнать парный ли он.
                              bool IsNotPair = false;

                              if (PreProcessingOptions.CommentSymbols.IsCommentSymbol(m_ProcessingCommentSymbol, ref IsNotPair))
                              {
                                 if (IsNotPair)
                                 {
                                    // Если комментарий непарный, то нужно удалить всё до конца строки
                                    CCommentedCodeFragment new_fragment = new CCommentedCodeFragment(new CElementPosition(GetRealLineNumber(units_counter), m_IndexOfCommentSymbolBeginning, GetRealLineNumber(units_counter), current_unit.Text.Length - 1), new CPair<string>(m_ProcessingCommentSymbol, string.Empty));
                                    m_OnelineCodeFragments.Add(new_fragment);
                                    break;
                                 }
                                 else
                                 {
                                    if (PreProcessingOptions.PairCommentDictionary.ContainsKey(m_ProcessingCommentSymbol))
                                    {
                                       m_PairCommentStack.Push(new CCommentedCodeFragment(new CElementPosition(GetRealLineNumber(units_counter), m_IndexOfCommentSymbolBeginning), new CPair<string>(m_ProcessingCommentSymbol, PreProcessingOptions.PairCommentDictionary[m_ProcessingCommentSymbol])));
                                       ClearProcessingCommentSymbol();
                                    }
                                    else
                                    {
                                       AddFragment(GetRealLineNumber(units_counter), char_counter);
                                       ClearProcessingCommentSymbol();
                                    }
                                 }
                              }
                           }
                        }
                        else
                        {
                           ClearProcessingCommentSymbol();
                        }
                     }
                  }
                  else
                  {
                     ClearProcessingCommentSymbol();
                  }
               }


               if (m_OnelineCodeFragments.Count > 0)
               {
                  new_codeunit.Text = string.Empty;

                  int starting_index = 0;
                  foreach (CCommentedCodeFragment fragment in m_OnelineCodeFragments)
                  {
                     //try
                     //{
                     new_codeunit.Text = new_codeunit.Text + current_unit.Text.Substring(starting_index, fragment.IndexStart - starting_index);
                     //}
                     //catch (ArgumentOutOfRangeException)
                     //{
                     //   throw;
                     //}
                     starting_index = fragment.IndexEnd + 1;
                  }

                  if (starting_index < current_unit.Text.Length)
                  {
                     //try
                     //{
                     new_codeunit.Text = new_codeunit.Text + current_unit.Text.Substring(starting_index);
                     //}
                     //catch (ArgumentOutOfRangeException)
                     //{
                     //   throw;
                     //}
                  }
               }

               if (m_PairCommentStack.Count > 0)
               {
                  CCommentedCodeFragment open_fragment = m_PairCommentStack.Peek();
                  ScanningSymbol = open_fragment.CommentSymbolPair.Second;

                  if (open_fragment.IsBelongOneLine(GetRealLineNumber(units_counter)))
                  {
                     int index = new_codeunit.Text.IndexOf(open_fragment.CommentSymbolPair.First);
                     new_codeunit.Text = new_codeunit.Text.Substring(0, index);
                  }
                  else
                  {
                     new_codeunit.Text = string.Empty;
                  }
               }
            }

            #endregion

            if (PreProcessingOptions.DeleteWhiteSpaces)
            {
               new_codeunit.Text = CStringConverter.RemoveMultipleWhiteSpaces(new_codeunit.Text.Trim());
            }

            if (!PreProcessingOptions.DeleteEmptyLines || !String.IsNullOrEmpty(new_codeunit.Text))
            {
               m_ModifiedCollection.Add(new_codeunit);
            }
         }
         return m_ModifiedCollection;
      }
   }
}