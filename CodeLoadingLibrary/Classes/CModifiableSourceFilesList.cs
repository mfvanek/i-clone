using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CodeLoadingLibrary.Classes.SourceFileContentLoader;
using ICloneBaseLibrary.Classes;
using ICloneExtensions.Classes;
using ICloneExtensions.Interfaces;
using ISourceFilesLibrary.Classes.CodeFragment;
using ISourceFilesLibrary.Classes.CodeUnit;
using ISourceFilesLibrary.Classes.SourceFile;

namespace CodeLoadingLibrary.Classes
{
   /// <summary>
   /// Список файлов для дальнейшей модификации
   /// </summary>
   public sealed class CModifiableSourceFilesList : CLoadFilesProcessor
   {
      /// <summary>
      /// Количество единиц кода в обработанных файлах
      /// </summary>
      private long m_CodeUnitsCountInModifiedFiles;

      private CSourceFilesCollection m_FilesCollection;

      /// <summary>
      /// Объект, который создает токен отмены и запрос на отмену для всех копий этого токена
      /// </summary>
      private CancellationTokenSource m_CancellationToken;

      private ICloneExtension m_ext;

      /// <summary>
      /// Вспомогательный объект для синхронизации потоков
      /// </summary>
      private object m_SyncRoot = new object();

      #region // Конструкторы

      /// <summary>
      /// Конструктор по умолчанию
      /// </summary>
      public CModifiableSourceFilesList(ICloneExtension ext)
         : base(new CLoadFilesOptions())
      {
         m_CodeUnitsCountInModifiedFiles = 0;
         m_CancellationToken = new CancellationTokenSource();
         m_FilesCollection = new CSourceFilesCollection();
         m_ext = ext;
      }

      #endregion

      #region // Функции для загрузки файлов

      /// <summary>
      /// Загрузить один файл
      /// </summary>
      /// <param name="filename"></param>
      protected override void LoadOneFile(string filename)
      {
         CSourceFileID FileID = new CSourceFileID();
         CTokenizerParms parms = new CTokenizerParms(filename, LoadFilesOptions.FileEncoding, FileID);
         ISourceFileContentLoader loader = CSourceFileContentLoaderFactory.Create(parms, m_ext);
         CSourceFile source_file = new CSourceFile(FileID, filename, LoadFilesOptions.FileEncoding, new CCodeFragment(loader.Load()));
         m_FilesCollection.IncreaseCountOfLinesInOriginFiles(source_file.Size());
         if (LoadFilesOptions.IsPreProcessingFile)
         {
            source_file.Content.Content = new CSimpleCodePreProcessingAlgorithm().PreProcessing(LoadFilesOptions.PreProcessingOptions, source_file.Content.Content);
         }
         m_FilesCollection.Add(source_file.SourceFileID, source_file);
         IncreaseCountOfLinesInModifiedFiles(source_file.Size());
      }

      /// <summary>
      /// Считать файлы с жёсткого диска и загрузить в память
      /// </summary>
      /// <param name="FilesToLoad"></param>
      protected override void LoadFilesToMemory(List<string> FilesToLoad)
      {
         if (LoadFilesOptions.IsUseParallelExtensions && CParallelExtensionsProvider.IsCanUseParallelExtensions())
         {
            ParallelOptions po = new ParallelOptions();
            po.CancellationToken = m_CancellationToken.Token;
            po.MaxDegreeOfParallelism = Environment.ProcessorCount;

            try
            {
               Parallel.ForEach(FilesToLoad, po, (any_file_name) =>
                   {
                      if (!GetCancelOperationFlag())
                      {
                         try
                         {
                            LoadOneFile(any_file_name);
                            OnLoadFilesProgress();
                         }
                         catch
                         {
                            // пока что так. потом нужно будет сделать журнал и записать туда ошибку.
                            // и потом продолжить чтение файлов
                            throw;
                         }
                      }
                      else
                      {
                         po.CancellationToken.ThrowIfCancellationRequested();
                      }
                   });
            }
            catch (OperationCanceledException)
            {
            }
         }
         else
         {
            foreach (string any_file_name in FilesToLoad)
            {
               if (!GetCancelOperationFlag())
               {
                  try
                  {
                     LoadOneFile(any_file_name);
                     OnLoadFilesProgress();
                  }
                  catch
                  {
                     // пока что так. потом нужно будет сделать журнал и записать туда ошибку.
                     // и потом продолжить чтение файлов
                     throw;
                  }
               }
               else
               {
                  break;
               }
            }
         }
      }

      /// <summary>
      /// Загрузить файлы, используя параметры по умолчанию
      /// </summary>
      public void LoadFiles()
      {
         m_FilesCollection.Clear();
         ProcessLoad();
      }

      #endregion

      /// <summary>
      /// Очистить список
      /// </summary>
      public void Clear()
      {
         m_FilesCollection.Clear();
         lock (m_SyncRoot)
         {
            m_CancelOperationFlag = false;
         }
      }

      /// <summary>
      /// Получить количество файлов в списке
      /// </summary>
      public int Count
      {
         get
         {
            return m_FilesCollection.Count;
         }
      }

      /// <summary>
      /// Получить количество строк
      /// </summary>
      /// <returns></returns>
      public long GetCountOfLinesInOriginFiles()
      {
         return m_FilesCollection.GetCountOfLinesInOriginFiles();
      }

      #region // Работа с параметрами загрузки файлов

      /// <summary>
      /// Параметры загрузки файлов
      /// </summary>
      private CLoadFilesOptions LoadFilesOptions
      {
         get
         {
            lock (m_SyncRoot)
            {
               return m_LoadOptions;
            }
         }
      }

      /// <summary>
      /// Задать параметры загрузки файлов
      /// </summary>
      /// <param name="value">Параметры загрузки файлов</param>
      public void SetLoadFilesOptions(CLoadFilesOptions value)
      {
         lock (m_SyncRoot)
         {
            m_LoadOptions = value;
         }
      }

      #endregion

      /// <summary>
      /// 
      /// </summary>
      /// <param name="value"></param>
      public void IncreaseCountOfLinesInModifiedFiles(long value)
      {
         Interlocked.Add(ref m_CodeUnitsCountInModifiedFiles, value);
      }

      /// <summary>
      /// Получить количество строк в файлах после обработки
      /// </summary>
      /// <returns></returns>
      public long GetCountOfLinesInModifiedFiles()
      {
         lock (m_SyncRoot)
         {
            return m_CodeUnitsCountInModifiedFiles;
         }
      }

      #region

      /// <summary>
      /// Получить набор всех строк исходного кода
      /// </summary>
      /// <returns></returns>
      public List<CExtendedCodeUnit> GetAllSourceRows()
      {
         long line_counter = CElementPosition.LINE_NUMBER_LOW_BOUND;
         List<CExtendedCodeUnit> rows = new List<CExtendedCodeUnit>();
         foreach (CSourceFile file in m_FilesCollection.Values)
         {
            foreach (CExtendedCodeUnit unit in file.Content.Content)
            {
               unit.NumberInGlobalWorkspace = line_counter;
               ++line_counter;
               rows.Add(unit);
            }
         }

         return rows;
      }

      #endregion
   }
}