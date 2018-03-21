using System.Collections;
using System.Collections.Generic;
using System.Threading;

namespace ISourceFilesLibrary.Classes.SourceFile
{
   /// <summary>
   /// 
   /// </summary>
   public sealed class CSourceFilesCollection : IEnumerable
   {
      #region // Поля класса

      /// <summary>
      /// Список файлов
      /// </summary>
      private Dictionary<long, CSourceFile> m_SourceFilesList;

      ///// <summary>
      ///// Объект, который создает токен отмены и запрос на отмену для всех копий этого токена
      ///// </summary>
      //protected CancellationTokenSource m_CancellationToken;

      /// <summary>
      /// Количество строк исходного кода в файлах
      /// </summary>
      private long m_CountOfLinesInOriginFiles;

      /// <summary>
      /// Вспомогательный объект для синхронизации потоков
      /// </summary>
      private object m_SyncRoot = new object();

      #endregion

      #region // Конструкторы

      /// <summary>
      /// Конструктор по умолчанию
      /// </summary>
      public CSourceFilesCollection()
      {
         lock (m_SyncRoot)
         {
            m_SourceFilesList = new Dictionary<long, CSourceFile>();
            //m_LoadFilesOptions = new CLoadFilesOptions();
            //m_CancelOperationFlag = false;
            //m_CancellationToken = new CancellationTokenSource();
            m_CountOfLinesInOriginFiles = 0;
         }
      }

      #endregion

      #region // Реализация интерфейса IDictionary<TKey, TValue>

      /// <summary>
      /// Получить список ключей
      /// </summary>
      public ICollection<long> Keys
      {
         get
         {
            lock (m_SyncRoot)
            {
               return m_SourceFilesList.Keys;
            }
         }
      }

      /// <summary>
      /// Получить список значений
      /// </summary>
      public ICollection<CSourceFile> Values
      {
         get
         {
            lock (m_SyncRoot)
            {
               return m_SourceFilesList.Values;
            }
         }
      }

      /// <summary>
      /// Получает или устанавливает элемент с указанным ключом
      /// </summary>
      /// <param name="key">The key of the element to get or set</param>
      /// <returns>The element with the specified key</returns>
      public CSourceFile this[long key]
      {
         get
         {
            lock (m_SyncRoot)
            {
               return m_SourceFilesList[key];
            }
         }

         set
         {
            lock (m_SyncRoot)
            {
               m_SourceFilesList[key] = value;
            }
         }
      }

      /// <summary>
      /// Добавляет элемент в список
      /// </summary>
      /// <param name="key">The object to use as the key of the element to add</param>
      /// <param name="value">The object to use as the value of the element to add</param>
      public void Add(long key, CSourceFile value)
      {
         lock (m_SyncRoot)
         {
            m_SourceFilesList.Add(key, value);
         }
      }

      /// <summary>
      /// Определяет, есть ли файл с заданным идентификатором в списке
      /// </summary>
      /// <param name="key"></param>
      /// <returns></returns>
      public bool ContainsKey(long key)
      {
         lock (m_SyncRoot)
         {
            return m_SourceFilesList.ContainsKey(key);
         }
      }

      /// <summary>
      /// Удаляет файл с указанным идентификатором из списка
      /// </summary>
      /// <param name="key"></param>
      /// <returns></returns>
      public bool Remove(long key)
      {
         lock (m_SyncRoot)
         {
            return m_SourceFilesList.Remove(key);
         }
      }

      /// <summary>
      /// Gets the value associated with the specified key.
      /// </summary>
      /// <param name="key">The key whose value to get.</param>
      /// <param name="value"></param>
      /// <returns></returns>
      public bool TryGetValue(long key, out CSourceFile value)
      {
         lock (m_SyncRoot)
         {
            return m_SourceFilesList.TryGetValue(key, out value);
         }
      }

      #endregion

      #region // Реализация интерфейса ICollection<KeyValuePair<TKey, TValue>>

      /// <summary>
      /// Получить количество файлов в списве
      /// </summary>
      public int Count
      {
         get
         {
            lock (m_SyncRoot)
            {
               return m_SourceFilesList.Count;
            }
         }
      }

      /// <summary>
      /// Очистить список
      /// </summary>
      public void Clear()
      {
         lock (m_SyncRoot)
         {
            m_SourceFilesList.Clear();
            //m_CancelOperationFlag = false;
         }
      }

      #endregion

      #region // Реализация интерфейса IEnumerable

      /// <summary>
      /// Returns an enumerator that iterates through a collection
      /// </summary>
      /// <returns>An System.Collections.IEnumerator object that can be used to iterate through the collection.</returns>
      public IEnumerator GetEnumerator()
      {
         lock (m_SyncRoot)
         {
            return m_SourceFilesList.GetEnumerator();
         }
      }

      #endregion

      /// <summary>
      /// Увеличить счётчик
      /// </summary>
      /// <param name="value"></param>
      public void IncreaseCountOfLinesInOriginFiles(long value)
      {
         Interlocked.Add(ref m_CountOfLinesInOriginFiles, value);
      }

      /// <summary>
      /// Получить количество строк
      /// </summary>
      /// <returns></returns>
      public long GetCountOfLinesInOriginFiles()
      {
         lock (m_SyncRoot)
         {
            return m_CountOfLinesInOriginFiles;
         }
      }
   }
}