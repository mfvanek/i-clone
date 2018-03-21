using System;
using System.ComponentModel;
using ICloneBaseLibrary.Classes;
using ICloneSearchLibrary.Classes;
using ISourceFilesLibrary.Classes;
using CodeLoadingLibrary.Classes;
using ICloneExtensions.Interfaces;

namespace ICloneServices.Classes
{
   public class CBaseCloneSearchExecutor
   {
      #region // Поля класса

      /// <summary>
      /// Вспомогательный объект для управления потоками
      /// </summary>
      private BackgroundWorker m_BackgroundThread;
      /// <summary>
      /// Список файлов с исходным кодом
      /// </summary>
      private CModifiableSourceFilesList m_FilesList;
      private CCloneSearchProvider m_CloneSearchProvider;
      private CLoadFilesOptions m_LoadFilesOptions;
      private MessageEventArgs m_MessageEventArgs;
      private long m_ProcessedClonesCounter;
      private ICloneExtension m_ext;

      // Стратегия уведомлений. Слишком часто - бессмысленно для быстрых алгоритмов
      private int m_ReportingCounter;
      private const int REPORTING_FRECUENCY = 1000;

      #endregion

      #region // События

      public event MessageEventHandler CloneSearchExecutingStart;
      public event MessageEventHandler CloneSearchExecutingProgress;
      public event MessageEventHandler CloneSearchExecutingEnd;

      public void OnCloneSearchExecutingStart()
      {
         if (CloneSearchExecutingStart != null)
         {
            CloneSearchExecutingStart(this, new MessageEventArgs(ICloneServicesLocalization.OperationInProgress));
         }
      }

      public void OnCloneSearchExecutingProgress()
      {
         if (CloneSearchExecutingProgress != null)
         {
            CloneSearchExecutingProgress(this, m_MessageEventArgs);
         }
      }

      public void OnCloneSearchExecutingEnd()
      {
         if (CloneSearchExecutingEnd != null)
         {
            CloneSearchExecutingEnd(this, m_MessageEventArgs);
         }
      }

      #endregion

      #region // Вспомогательные функции для инициализации компонентов

      private void InitBackgroundThread()
      {
         m_BackgroundThread = new BackgroundWorker();
         m_BackgroundThread.WorkerReportsProgress = true;
         m_BackgroundThread.WorkerSupportsCancellation = true;

         m_BackgroundThread.DoWork += (o, e) =>
         {
            m_ProcessedClonesCounter = 0;
            OnCloneSearchExecutingStart();
            m_FilesList.LoadFiles();
            if (!m_CloneSearchProvider.GetCancelOperationFlag())
            {
               m_CloneSearchProvider.FindDuplicatedRows(m_FilesList.GetAllSourceRows());
            }
         };

         m_BackgroundThread.ProgressChanged += (o, e) =>
         {
            OnCloneSearchExecutingProgress();
         };

         m_BackgroundThread.RunWorkerCompleted += (o, e) =>
         {
            if (e.Cancelled || m_FilesList.GetCancelOperationFlag())
            {
               m_MessageEventArgs.Message = ICloneServicesLocalization.OperationCancelled;
            }
            else
            {
               if (e.Error != null)
               {
                  m_MessageEventArgs.Message = ICloneServicesLocalization.OperationtDoneWithErrors;
               }
               else
               {
                  m_MessageEventArgs.Message = ICloneServicesLocalization.OperationDoneSuccesfully;
               }
            }

            OnCloneSearchExecutingEnd();
         };
      }

      #endregion

      //protected CBaseCloneSearchExecutor()
      //   : this(CAvailableCloneSearchAlgorithms.GetAlgorithm(CloneSearchAlgoritms.HashBucketAlgorithm))
      //{}

      public CBaseCloneSearchExecutor(CBaseCloneSearchStrategy search_algorithm, ICloneExtension ext)
      {
         m_ext = ext;
         m_ProcessedClonesCounter = 0;
         m_ReportingCounter = 0;
         m_MessageEventArgs = new MessageEventArgs();
         m_LoadFilesOptions = new CLoadFilesOptions();
         m_FilesList = new CModifiableSourceFilesList(m_ext);
         m_CloneSearchProvider = new CCloneSearchProvider(search_algorithm);
         InitBackgroundThread();
         m_CloneSearchProvider.InitCloneSearchCallbacks(new EventHandler(ReportCloneSearchStart), new EventHandler(ReportCloneSearchProgress), new EventHandler(ReportCloneSearchEnd));
         //m_FilesList.InitFilesListBuildingCallbacks(new EventHandler(ReportFilesListBuildingStart), null, new EventHandler(ReportFilesListBuildingEnd));
         //m_FilesList.InitLoadFilesCallbacks(new EventHandler(ReportLoadFilesStart), new EventHandler(ReportLoadFilesProgress), null);
      }

      private void InitFileListWithGarbageCollecting()
      {
         #region // Чудеса в решете и шаманство с освобождением памяти
         m_FilesList.Clear();
         // Соберём мусор
         GC.Collect();
         // Нужно ещё сбросить всякие EventHandler'ы
         m_FilesList = new CModifiableSourceFilesList(m_ext);
         m_FilesList.SetLoadFilesOptions(m_LoadFilesOptions);
         m_FilesList.InitFilesListBuildingCallbacks(new EventHandler(ReportFilesListBuildingStart), null, new EventHandler(ReportFilesListBuildingEnd));
         m_FilesList.InitLoadFilesCallbacks(new EventHandler(ReportLoadFilesStart), new EventHandler(ReportLoadFilesProgress), null);
         #endregion
      }

      public void SetLoadFilesOptions(CLoadFilesOptions LoadFilesOptions)
      {
         m_LoadFilesOptions = LoadFilesOptions;
         m_FilesList.SetLoadFilesOptions(LoadFilesOptions);
      }

      public void CancelAsync()
      {
         m_MessageEventArgs.Message = ICloneServicesLocalization.OperationCancellingInProgress;
         OnCloneSearchExecutingProgress();
         m_FilesList.SetCancelOperationFlag(true);
         m_CloneSearchProvider.SetCancelOperationFlag(true);
         m_BackgroundThread.CancelAsync();
      }

      private void ReportProgress()
      {
         m_BackgroundThread.ReportProgress(1);
      }

      public bool IsBusy()
      {
         return m_BackgroundThread.IsBusy;
      }

      public void Run()
      {
         if (!m_BackgroundThread.IsBusy)
         {
            m_CloneSearchProvider.SetCancelOperationFlag(false);
            InitFileListWithGarbageCollecting();
            m_BackgroundThread.RunWorkerAsync();
         }
      }

      #region // ReportCloneSearch

      private void ReportCloneSearchStart(object sender, EventArgs e)
      {
         m_ProcessedClonesCounter = 0;
         m_MessageEventArgs.Message = ICloneServicesLocalization.CloneSearchInProgress;
         ReportProgress();
      }

      private void ReportCloneSearchProgress(object sender, EventArgs e)
      {
         --m_ReportingCounter;
         ++m_ProcessedClonesCounter;
         if (m_ReportingCounter <= 0)
         {
            m_ReportingCounter = REPORTING_FRECUENCY;
            ReportProgress();
         }
      }

      private void ReportCloneSearchEnd(object sender, EventArgs e)
      {
         m_MessageEventArgs.Message = ICloneServicesLocalization.CloneSearchIsDone;
         ReportProgress();
      }

      #endregion

      #region // ReportFilesListBuilding

      private void ReportFilesListBuildingStart(object sender, EventArgs e)
      {
         m_MessageEventArgs.Message = ICloneServicesLocalization.StartFilesListBuilding;
         ReportProgress();
      }

      private void ReportFilesListBuildingProgress(object sender, EventArgs e)
      {
         ReportProgress();
      }

      private void ReportFilesListBuildingEnd(object sender, EventArgs e)
      {
         m_MessageEventArgs.Message = ICloneServicesLocalization.EndFilesListBuilding;
         ReportProgress();
      }

      #endregion

      #region // ReportLoadFiles

      private void ReportLoadFilesStart(object sender, EventArgs e)
      {
         m_MessageEventArgs.Message = ICloneServicesLocalization.LoadFilesInProgress;
         ReportProgress();
      }

      private void ReportLoadFilesProgress(object sender, EventArgs e)
      {
         ReportProgress();
      }

      #endregion

      /// <summary>
      /// Количество обработанных строк
      /// </summary>
      public long ProcessedClonesCounter
      {
         get
         {
            return m_ProcessedClonesCounter;
         }
      }

      public int FilesCount()
      {
         return m_FilesList.Count;
      }

      public long GetCountOfLinesInOriginFiles()
      {
         return m_FilesList.GetCountOfLinesInOriginFiles();
      }

      public long GetCountOfLinesInModifiedFiles()
      {
         return m_FilesList.GetCountOfLinesInModifiedFiles();
      }

      public ICloneExtension Extension
      {
         set
         {
            m_ext = value;
         }
      }
   }
}