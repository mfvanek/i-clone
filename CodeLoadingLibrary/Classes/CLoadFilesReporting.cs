using System;
using System.Collections.Generic;
using CodeLoadingLibrary.Interfaces;
using ICloneBaseLibrary.Classes;

namespace CodeLoadingLibrary.Classes
{
   /// <summary>
   /// Класс для индикации процесса загрузки файлов
   /// </summary>
   public abstract class CLoadFilesReporting : CBreakableActions, ILoadFilesReporting, ILoadFilesCallbacks
   {
      #region // События

      /// <summary>
      /// Событие, возникающее при старте загрузки файлов
      /// </summary>
      public virtual event EventHandler LoadFilesStart;
      /// <summary>
      /// Событие, возникающее при завершении загрузки файлов
      /// </summary>
      public virtual event EventHandler LoadFilesEnd;
      /// <summary>
      /// Событие, возникающее при загрузке файла
      /// </summary>
      public virtual event EventHandler LoadFilesProgress;
      /// <summary>
      /// Событие, возникающее при начале построения списка файлов
      /// </summary>
      public virtual event EventHandler FilesListBuildingStart;
      /// <summary>
      /// Событие, возникающее при добавлении файла в список
      /// </summary>
      public virtual event EventHandler FilesListBuildingProgress;
      /// <summary>
      /// Событие, возникающее при завершении построения списка файлов
      /// </summary>
      public virtual event EventHandler FilesListBuildingEnd;

      #endregion

      #region // Реализация интерфейса ILoadFilesCallbacks

      /// <summary>
      /// 
      /// </summary>
      /// <param name="ReportLoadFilesStart"></param>
      /// <param name="ReportLoadFilesProgress"></param>
      /// <param name="ReportLoadFilesEnd"></param>
      public void InitLoadFilesCallbacks(EventHandler ReportLoadFilesStart, EventHandler ReportLoadFilesProgress, EventHandler ReportLoadFilesEnd)
      {
         LoadFilesStart += ReportLoadFilesStart;
         LoadFilesProgress += ReportLoadFilesProgress;
         LoadFilesEnd += ReportLoadFilesEnd;
      }

      /// <summary>
      /// 
      /// </summary>
      /// <param name="ReportFilesListBuildingStart"></param>
      /// <param name="ReportFilesListBuildingProgress"></param>
      /// <param name="ReportFilesListBuildingEnd"></param>
      public void InitFilesListBuildingCallbacks(EventHandler ReportFilesListBuildingStart, EventHandler ReportFilesListBuildingProgress, EventHandler ReportFilesListBuildingEnd)
      {
         FilesListBuildingStart += ReportFilesListBuildingStart;
         FilesListBuildingProgress += ReportFilesListBuildingProgress;
         FilesListBuildingEnd += ReportFilesListBuildingEnd;
      }

      #endregion

      #region // Реализация интерфейса ILoadFilesReporting

      #region // OnLoadFiles

      /// <summary>
      /// Уведомить о начале загрузки файлов
      /// </summary>
      public virtual void OnLoadFilesStart()
      {
         if (LoadFilesStart != null)
         {
            LoadFilesStart(this, new EventArgs());
         }
      }

      /// <summary>
      /// Уведомить о загрузке файла
      /// </summary>
      public virtual void OnLoadFilesProgress()
      {
         if (LoadFilesProgress != null)
         {
            LoadFilesProgress(this, new EventArgs());
         }
      }

      /// <summary>
      /// Уведомить о завершении загрузки файлов
      /// </summary>
      public virtual void OnLoadFilesEnd()
      {
         if (LoadFilesEnd != null)
         {
            LoadFilesEnd(this, new EventArgs());
         }
      }

      #endregion

      #region // OnFilesListBuilding

      /// <summary>
      /// Уведомить о начале построения списка файлов
      /// </summary>
      public virtual void OnFilesListBuildingStart()
      {
         if (FilesListBuildingStart != null)
         {
            FilesListBuildingStart(this, new EventArgs());
         }
      }

      /// <summary>
      /// Уведомить о добавлении файла в список
      /// </summary>
      public virtual void OnFilesListBuildingProgress()
      {
         if (FilesListBuildingProgress != null)
         {
            FilesListBuildingProgress(this, new EventArgs());
         }
      }

      /// <summary>
      /// Уведомить о завершении построения списка файлов
      /// </summary>
      public virtual void OnFilesListBuildingEnd()
      {
         if (FilesListBuildingEnd != null)
         {
            FilesListBuildingEnd(this, new EventArgs());
         }
      }

      #endregion

      #endregion
   }

   /// <summary>
   /// Вспомогательный класс для реализации операций, так или иначе связанных с загрузкой файлов
   /// </summary>
   public abstract class CLoadFilesProcessor : CLoadFilesReporting
   {
      /// <summary>
      /// Параметры загрузки файлов
      /// </summary>
      protected CLoadFilesOptions m_LoadOptions;

      protected CLoadFilesProcessor(CLoadFilesOptions _LoadOptions)
      {
         m_LoadOptions = _LoadOptions;
      }

      private void ReportFilesListBuildingProgress(object sender, EventArgs e)
      {
         OnFilesListBuildingProgress();
      }

      /// <summary>
      /// Получить список файлов
      /// </summary>
      /// <returns></returns>
      private List<string> GetFiles()
      {
         OnFilesListBuildingStart();
         CFilesListBuilder FilesListBuilder = new CFilesListBuilder(m_LoadOptions, new EventHandler(ReportFilesListBuildingProgress));
         List<string> FilesToLoad = FilesListBuilder.GetFiles();
         OnFilesListBuildingEnd();
         return FilesToLoad;
      }

      protected abstract void LoadOneFile(string filename);

      protected virtual void LoadFilesToMemory(List<string> FilesToLoad)
      {
         foreach (string filename in FilesToLoad)
         {
            if (!GetCancelOperationFlag())
            {
               LoadOneFile(filename);
               OnLoadFilesProgress();
            }
         }
      }

      protected void ProcessLoad()
      {
         List<string> FilesToLoad = GetFiles();
         if (!GetCancelOperationFlag())
         {
            OnLoadFilesStart();
            LoadFilesToMemory(FilesToLoad);
            OnLoadFilesEnd();
         }
      }
   }
}
