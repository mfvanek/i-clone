using System.IO;
using System.Threading;
using CodeLoadingLibrary.Classes;
using CodeLoadingLibrary.Classes.SourceFileContentLoader;
using ICloneCodeCharacteristicsLibrary.Classes;
using ICloneExtensions.Classes;
using ISourceFilesLibrary.Classes.CodeUnitsCollection;
using ISourceFilesLibrary.Classes.Languages;
using ISourceFilesLibrary.Classes.SourceFile;

namespace IClone.IDetectCodeSize
{
   /// <summary>
   /// Класс для подсчёта количества исходных файлов и строк в них в указанной папке.
   /// Поиск файлов осуществляется по маске.
   /// Может работать в отдельном потоке.
   /// </summary>
   public sealed class CCodeSizeDetector : CSyntacticUnitsInfoProvider 
   {
      /// <summary>
      /// Количество обработанных файлов
      /// </summary>
      private long m_CountOfFiles;

      /// <summary>
      /// Количество строк
      /// </summary>
      private long m_CountOfLines;

      /// <summary>
      /// Общее количество токенов
      /// </summary>
      private long m_CountOfTokens;

      /// <summary>
      /// Вспомогательный объект для синхронизации потоков
      /// </summary>
      private object m_SyncRoot = new object();

      #region // Конструкторы

      /// <summary>
      /// Конструктор по умолчанию
      /// </summary>
      public CCodeSizeDetector(LANGUAGES LanguageID, CLoadFilesOptions load_files_options)
         : base(LanguageID, load_files_options)
      {
         ClearCounters();
      }

      #endregion

      /// <summary>
      /// Метод получает и возвращает количество строк в указанном файле
      /// </summary>
      /// <param name="FileName"></param>
      /// <returns></returns>
      private long GetCountOfLines(string FileName)
      {
         return File.ReadAllLines(FileName).Length;
      }

      private void IncreaseCountOfLines(string filename)
      {
         // Нам нужно реальное количество строк в файле!
         CSourceFileID FileID = new CSourceFileID();
         CTokenizerParms args = new CTokenizerParms(filename, FileID);
         ISourceFileContentLoader loader = new CEntireRowSourceFileContentLoader(args);
         CCodeUnitsCollection collection = loader.Load();
         Interlocked.Add(ref m_CountOfLines, collection.Size());
      }

      private void IncreaseCountOfTokens(string filename)
      {
         try
         {
            CSourceFileID FileID = new CSourceFileID();
            CTokenizerParms args = new CTokenizerParms(filename, FileID);
            ISourceFileContentLoader loader = new CTokenSourceFileContentLoader(args, m_Extension);
            CCodeUnitsCollection collection = loader.Load();
            Interlocked.Add(ref m_CountOfTokens, collection.Size());
         }
         catch
         {
            // Если поддержка токенизации отсутствует, то ничего не делаем
         }
      }

      /// <summary>
      /// Обработать одни файл
      /// </summary>
      /// <param name="filename"></param>
      protected override void LoadOneFile(string filename)
      {
         Interlocked.Increment(ref m_CountOfFiles);
         IncreaseCountOfLines(filename);
         IncreaseCountOfTokens(filename);
         lock (m_SyncRoot)
         {
            base.LoadOneFile(filename);
         }
      }

      /// <summary>
      /// Сброс счётчиков количества файлов и количества строк
      /// </summary>
      private void ClearCounters()
      {
         lock (m_SyncRoot)
         {
            m_CountOfFiles = 0;
            m_CountOfLines = 0;
            m_CancelOperationFlag = false;
            m_CountOfTokens = 0;
         }
      }

      /// <summary>
      /// Метод выполняет сканирование указанного каталога на наличие файлов с учетом масок
      /// и подсчитывает количество строк в них
      /// </summary>
      /// <returns></returns>
      public override CSyntacticInfo Calculate()
      {
         ClearCounters();
         return base.Calculate();
      }

      /// <summary>
      /// Получить текущее значение счётчика файлов
      /// </summary>
      public long CountOfFiles
      {
         get
         {
            lock (m_SyncRoot)
            {
               return m_CountOfFiles;
            }
         }
      }

      /// <summary>
      /// Получить текущее значение счётчика строк кода
      /// </summary>
      public long CountOfLines
      {
         get
         {
            lock (m_SyncRoot)
            {
               return m_CountOfLines;
            }
         }
      }

      /// <summary>
      /// Общее количество токенов
      /// </summary>
      public long CountOfTokens
      {
         get
         {
            lock (m_SyncRoot)
            {
               return m_CountOfTokens;
            }
         }
      }

      /// <summary>
      /// Количество синтаксических единиц в коде
      /// </summary>
      public long CountOfSyntacticUnits
      {
         get
         {
            lock (m_SyncRoot)
            {
               return m_CountOfSyntacticUnits;
            }
         }
      }

      #region // Реализация интерфейса ILoadFilesReporting

      /// <summary>
      /// Получить флаг, сигнализирующий о необходимости прервать выполняемую операцию
      /// </summary>
      /// <returns></returns>
      public override bool GetCancelOperationFlag()
      {
         lock (m_SyncRoot)
         {
            return base.GetCancelOperationFlag();
         }
      }

      /// <summary>
      /// Установить флаг, сигнализирующий о необходимости прервать выполняемую операцию
      /// </summary>
      /// <param name="value"></param>
      public override void SetCancelOperationFlag(bool value)
      {
         lock (m_SyncRoot)
         {
            base.SetCancelOperationFlag(value);
         }
      }

      #endregion
   }
}