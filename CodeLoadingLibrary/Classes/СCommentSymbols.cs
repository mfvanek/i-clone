using System;
using System.Collections.Generic;
using ICloneBaseLibrary.Classes;

namespace CodeLoadingLibrary.Classes
{
    /// <summary>
    /// Класс для хранения информации о возможных символах комментариев
    /// </summary>
    public sealed class СCommentSymbols
    {
        #region // Поля класса

        /// <summary>
        /// Список всех возможных комментариев (парные комментарии разбиты по парам)
        /// </summary>
        private List<CPair<string>> m_CommentSymbols;

        /// <summary>
        /// Спиок всех возможных символов, встречающихся в комментариях
        /// </summary>
        private HashSet<char> m_CommentChars;

        /// <summary>
        /// Минимальная длина комментария
        /// </summary>
        private int m_MinCommentLength;

        /// <summary>
        /// Максимальная длина комментария
        /// </summary>
        private int m_MaxCommentLength;

        #endregion

        #region // Конструкторы

        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
        public СCommentSymbols()
        {
            m_CommentSymbols = new List<CPair<string>>();
            m_CommentChars = new HashSet<char>();
            Clear();
        }

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="_CommentSymbols">Все возможные символы комментариев. Группы символов перечисляются через точку с запятой.
        /// Парные символы входят в одну группу и разделяются пробелом.</param>
        /// <exception cref="ArgumentException">
        /// Исключение генерируется, если параметр CommentSymbols не задан или пуст
        /// </exception>
        public СCommentSymbols(string _CommentSymbols)
        {
            m_CommentSymbols = new List<CPair<string>>();
            m_CommentChars = new HashSet<char>();
            Clear();

            SetCommentSymbols(_CommentSymbols);
        }

        #endregion

        #region // Закрытые вспомогательные функции

        private void SetCommentLength(string[] pair)
        {
            foreach (string comment in pair)
            {
                m_MinCommentLength = (m_MinCommentLength > -1) ? Math.Min(comment.Length, m_MinCommentLength) : comment.Length;
                m_MaxCommentLength = (m_MaxCommentLength > -1) ? Math.Max(comment.Length, m_MaxCommentLength) : comment.Length;
            }
        }

        private void Clear()
        {
            m_CommentSymbols.Clear();
            m_CommentChars.Clear();

            m_MinCommentLength = -1;
            m_MaxCommentLength = -1;
        }

        #endregion

        /// <summary>
        /// Установить возможные символы комментариев
        /// </summary>
        /// <param name="_CommentSymbols">Все возможные символы комментариев. Группы символов перечисляются через точку с запятой.
        /// Парные символы входят в одну группу и разделяются пробелом.</param>
        /// <exception cref="ArgumentException">
        /// Исключение генерируется, если параметр CommentSymbols не задан или пуст
        /// </exception>
        public void SetCommentSymbols(string _CommentSymbols)
        {
            const char GroupsDelimiter = ';';
            const char InGroupDelimiter = ' ';

            Clear();

            if (!String.IsNullOrEmpty(_CommentSymbols))
            {
                // Построим список символов, встречающихся в комментариях
                foreach (char symbol in _CommentSymbols)
                {
                    if (symbol != GroupsDelimiter && symbol != InGroupDelimiter && !m_CommentChars.Contains(symbol))
                    {
                        m_CommentChars.Add(symbol);
                    }
                }

                string[] groups = _CommentSymbols.Split(GroupsDelimiter);
                foreach (string group in groups)
                {
                    string[] pair = group.Split(InGroupDelimiter);
                    SetCommentLength(pair);

                    if (pair.Length == 1)
                    {
                        m_CommentSymbols.Add(new CPair<string>(pair[0].Trim(), string.Empty));
                    }
                    else
                    {
                        if (pair.Length >= 2)
                        {
                            m_CommentSymbols.Add(new CPair<string>(pair[0].Trim(), pair[1].Trim()));
                        }
                        else
                        {
                            // косяк какой-то...
                        }
                    }
                }
            }
            else
            {
                throw new ArgumentException("Проверьте параметр _CommentSymbols");
            }
        }

        /// <summary>
        /// Получить список символов комментариев
        /// </summary>
        /// <returns></returns>
        public List<CPair<string>> GetCommentSymbolsList()
        {
            return m_CommentSymbols;
        }

        /// <summary>
        /// Добавить символ (или пару символов) комментария в список
        /// </summary>
        /// <param name="item"></param>
        public void AddCommentInList(CPair<string> item)
        {
            m_CommentSymbols.Add(item);
        }

        /// <summary>
        /// Представить в виде массива
        /// </summary>
        /// <returns></returns>
        public string[] ToArray()
        {
            List<string> comments_list = new List<string>();

            foreach (CPair<string> pair in m_CommentSymbols)
            {
                comments_list.Add(pair.First);
                comments_list.Add(pair.Second);
            }

            return comments_list.ToArray();
        }

        /// <summary>
        /// Встречается ли данный знак в комментариях
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool IsCommentChar(char item)
        {
            return m_CommentChars.Contains(item);
        }

        /// <summary>
        /// Является ли указанная строка символом комментария?
        /// Если это комментарий, то парный ли он?
        /// </summary>
        /// <param name="symbol"></param>
        /// <param name="IsNotPair"></param>
        /// <returns></returns>
        public bool IsCommentSymbol(string symbol, ref bool IsNotPair)
        {
            bool retval = false;
            IsNotPair = true;

            foreach (CPair<string> pair in m_CommentSymbols)
            {
                if (pair.First == symbol)
                {
                    retval = true;
                    IsNotPair = String.IsNullOrEmpty(pair.Second);
                    break;
                }

                if (pair.Second == symbol)
                {
                    retval = true;
                    IsNotPair = false;
                    break;
                }
            }

            return retval;
        }

        /// <summary>
        /// Минимальная длина комментария
        /// </summary>
        public int MinCommentLength
        {
            get
            {
                return m_MinCommentLength;
            }
        }

        /// <summary>
        /// Максимальная длина комментария
        /// </summary>
        public int MaxCommentLength
        {
            get
            {
                return m_MaxCommentLength;
            }
        }

        #region // Переопределение метода Equals

        private bool IsSymbolsListsEquals(List<CPair<string>> other)
        {
            if (other.Count != m_CommentSymbols.Count)
            {
                return false;
            }
            else
            {
                for (int counter = 0; counter < other.Count; counter++)
                {
                    if (!other[counter].Equals(m_CommentSymbols[counter]))
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        private bool IsCommentCharsEquals(HashSet<char> other)
        {
            if (other.Count != m_CommentChars.Count)
            {
                return false;
            }
            else
            {
                foreach (char item in other)
                {
                    if (!m_CommentChars.Contains(item))
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        /// <summary>
        /// Определяет, равны ли два экземпляра
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            // If parameter is null return false.
            if (obj == null)
            {
                return false;
            }

            // If parameter cannot be cast to Point return false.
            СCommentSymbols p = obj as СCommentSymbols;
            if ((object)p == null)
            {
                return false;
            }

            // Return true if the fields match:
            return (m_MinCommentLength == p.MinCommentLength) && (m_MaxCommentLength == p.MaxCommentLength) &&
                IsSymbolsListsEquals(p.m_CommentSymbols) && IsCommentCharsEquals(p.m_CommentChars);
        }

        /// <summary>
        /// Хэш-код для текущего объекта
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        #endregion
    }
}
