using System;
using System.Collections.Generic;
using ICloneBaseLibrary.Classes;
using ISourceFilesLibrary.Classes;

namespace CodeLoadingLibrary.Classes
{
    /// <summary>
    /// Опции предварительной обработки программного кода
    /// </summary>
    public sealed class CCodePreProcessingOptions
    {
        #region // Поля класса

        /// <summary>
        /// Флаг, сигнализирующий о необходимости удаления пустых строк
        /// </summary>
        private bool m_DeleteEmptyLines;
        /// <summary>
        /// Флаг, сигнализирующий о необходимости удаления незначащих пробелов в строке
        /// </summary>
        private bool m_DeleteWhiteSpaces;
        /// <summary>
        /// Флаг, сигнализирующий о необходимости удаления комментариев из кода
        /// </summary>
        private bool m_DeleteComments;
        /// <summary>
        /// Все возможные символы комментариев для определённого языка программирования
        /// </summary>
        private СCommentSymbols m_CommentSymbols;
        /// <summary>
        /// Список парных символов комментариев
        /// </summary>
        private Dictionary<string, string> m_PairCommentDictionary;

        #endregion

        #region // Закрытые вспомогательные функции

        private void BuildPairCommentDictionary()
        {
            m_PairCommentDictionary.Clear();

            if (m_CommentSymbols != null)
            {
                foreach (CPair<string> pair in m_CommentSymbols.GetCommentSymbolsList())
                {
                    if (!String.IsNullOrEmpty(pair.Second))
                    {
                        // Символ комментария парный
                        m_PairCommentDictionary.Add(pair.First, pair.Second);
                    }
                }
            }
        }

        #endregion

        #region // Конструкторы

        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
        public CCodePreProcessingOptions()
        {
            m_DeleteEmptyLines = true;
            m_DeleteWhiteSpaces = true;
            m_DeleteComments = true;
            m_CommentSymbols = new СCommentSymbols();
            m_PairCommentDictionary = new Dictionary<string, string>();
        }

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="_CommentSymbols"></param>
        public CCodePreProcessingOptions(string _CommentSymbols)
        {
            m_DeleteEmptyLines = true;
            m_DeleteWhiteSpaces = true;
            m_DeleteComments = true;
            m_CommentSymbols = new СCommentSymbols(_CommentSymbols);
            m_PairCommentDictionary = new Dictionary<string, string>();
            BuildPairCommentDictionary();
        }

        #endregion

        /// <summary>
        /// Признак необходимости удаления пустых строк
        /// </summary>
        public bool DeleteEmptyLines
        {
            get
            {
                return m_DeleteEmptyLines;
            }
        }

        /// <summary>
        /// Установить признак необходимости удаления пустых строк
        /// </summary>
        /// <param name="value"></param>
        public void SetDeleteEmptyLines(bool value)
        {
            m_DeleteEmptyLines = value;
        }

        /// <summary>
        /// Признак необходимости удаления незначащих пробелов в строке
        /// </summary>
        public bool DeleteWhiteSpaces
        {
            get
            {
                return m_DeleteWhiteSpaces;
            }
        }

        /// <summary>
        /// Установить признак необходимости удаления незначащих пробелов в строке
        /// </summary>
        /// <param name="value"></param>
        public void SetDeleteWhiteSpaces(bool value)
        {
            m_DeleteWhiteSpaces = value;
        }

        /// <summary>
        /// Признак необходимости удаления комментариев из кода
        /// </summary>
        public bool DeleteComments
        {
            get
            {
                return m_DeleteComments;
            }
        }

        /// <summary>
        /// Установить признак необходимости удаления комментариев из кода
        /// </summary>
        /// <param name="value"></param>
        public void SetDeleteComments(bool value)
        {
            m_DeleteComments = value;
        }

        /// <summary>
        /// Все возможные символы комментариев. Группы символов перечисляются через точку с запятой.
        /// </summary>
        public СCommentSymbols CommentSymbols
        {
            get
            {
                return m_CommentSymbols;
            }
        }

        /// <summary>
        /// Задать все возможные символы комментариев
        /// </summary>
        /// <param name="value"></param>
        public void SetCommentSymbols(string value)
        {
            m_CommentSymbols.SetCommentSymbols(value);
            BuildPairCommentDictionary();
        }

        /// <summary>
        /// Список парных символов комментариев
        /// </summary>
        public Dictionary<string, string> PairCommentDictionary
        {
            get
            {
                return m_PairCommentDictionary;
            }
        }
    }
}