using System.Collections.Generic;
using System.Threading;
using ISourceFilesLibrary.Classes.CodeUnit;

namespace ISourceFilesLibrary.Classes.SourceFile
{
   /// <summary>
   /// 
   /// </summary>
   public sealed class CSourceFileID
   {
      #region // Статические поля и функции класса

      /// <summary>
      /// Генератор уникальных номеров
      /// </summary>
      private static long m_SourceFileIDGenerator = CExtendedCodeUnit.DEFAULT_SOURCE_FILE_ID;
      /// <summary>
      /// Пропущенные (освобождённые) уникальные номера файлов
      /// </summary>
      private static Stack<long> m_MissingSourceFileIDs = new Stack<long>();

      /// <summary>
      /// Получить уникальный идентификатор файла
      /// </summary>
      /// <returns></returns>
      private static long GetUniqueID()
      {
         if (m_MissingSourceFileIDs.Count > 0)
         {
            lock (m_MissingSourceFileIDs)
            {
               return m_MissingSourceFileIDs.Pop();
            }
         }

         return Interlocked.Increment(ref m_SourceFileIDGenerator);
      }

      /// <summary>
      /// Освободить уникальный номер файла для повторного использования
      /// </summary>
      /// <param name="FileID"></param>
      private static void RestoreUniqueID(long FileID)
      {
         if (FileID == m_SourceFileIDGenerator)
         {
            System.Threading.Interlocked.Decrement(ref m_SourceFileIDGenerator);
         }
         else
         {
            lock (m_MissingSourceFileIDs)
            {
               m_MissingSourceFileIDs.Push(FileID);
            }
         }
      }

      #endregion

      #region // Члены класса

      /// <summary>
      /// Уникальный идентификатор файла
      /// </summary>
      private long m_SourceFileID;

      #endregion

      /// <summary>
      /// 
      /// </summary>
      public CSourceFileID()
      {
         m_SourceFileID = GetUniqueID();
      }

      #region // Деструктор

      /// <summary>
      /// Пользовательский деструктор
      /// </summary>
      ~CSourceFileID()
      {
         RestoreUniqueID(m_SourceFileID);
      }

      #endregion

      /// <summary>
      /// Уникальный идентификатор файла
      /// </summary>
      public long SourceFileID
      {
         get
         {
            return m_SourceFileID;
         }
      }
   };
}