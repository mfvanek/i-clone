using System;
using System.Collections.Generic;
using System.IO;

namespace CodeLoadingLibrary.Classes
{
   /// <summary>
   /// Класс для получения списка файлов в заданной директории
   /// </summary>
   public sealed class CFilesListBuilder
   {
      /// <summary>
      /// Событие, возникающее при добавлении файла в список
      /// </summary>
      private event EventHandler FilesListBuildingProgress;

      /// <summary>
      /// Список файлов
      /// </summary>
      private List<string> m_Files;
      /// <summary>
      /// Каталоги, которые можно пропустить и не обрабатывать
      /// </summary>
      private HashSet<string> m_SkippingFolders;
      /// <summary>
      /// Параметры загрузки файлов
      /// </summary>
      private CLoadFilesOptions m_LoadFilesOptions;

      #region // Закрытые вспомогательные функции

      /// <summary>
      /// Уведомить о добавлении файла в список
      /// </summary>
      void OnFilesListBuildingProgress()
      {
         if (FilesListBuildingProgress != null)
         {
            FilesListBuildingProgress(this, new EventArgs());
         }
      }

      /// <summary>
      /// Обработать каталог
      /// </summary>
      /// <param name="RootDirectory"></param>
      private void ParseFolder(string RootDirectory)
      {
         try
         {
            string[] Folders = Directory.GetDirectories(RootDirectory, "*", SearchOption.TopDirectoryOnly);

            foreach (string Folder in Folders)
            {
               if (!IsSkippingFolder(Folder))
               {
                  ParseFolder(Folder);
               }
            }
         }
         catch
         {
         }

         try
         {
            string[] FilesInRootDirectory = Directory.GetFiles(RootDirectory, "*.*", SearchOption.TopDirectoryOnly);
            foreach (string any_file_name in FilesInRootDirectory)
            {
               foreach (string extension in m_LoadFilesOptions.FileExtensions)
               {
                  if (any_file_name.EndsWith(extension, StringComparison.OrdinalIgnoreCase))
                  {
                     m_Files.Add(any_file_name);
                     OnFilesListBuildingProgress();
                     break;
                  }
               }
            }
         }
         catch
         {
         }
      }

      /// <summary>
      /// Построить список каталогов, которые необходимо пропускать при обработке
      /// </summary>
      /// <param name="SkippingFolders"></param>
      private void BuildSkippingFoldersList(string SkippingFolders)
      {
         m_SkippingFolders.Clear();
         const char Delimiter = ';';

         string[] FoldersList = SkippingFolders.Split(Delimiter);

         foreach (string Folder in FoldersList)
         {
            string buffer = Folder.Trim();
            if (!String.IsNullOrEmpty(buffer))
            {
               try
               {
                  m_SkippingFolders.Add(buffer);
               }
               catch
               {
               }
            }
         }
      }

      /// <summary>
      /// Нужно ли пропустить указанный каталог?
      /// </summary>
      /// <param name="Folder"></param>
      /// <returns></returns>
      private bool IsSkippingFolder(string Folder)
      {
         const char Delimiter = '\\';
         string[] PathElements = Folder.Split(Delimiter);

         if (PathElements.Length > 0)
         {
            return m_SkippingFolders.Contains(PathElements[PathElements.Length - 1]);
         }

         return false;
      }

      #endregion

      #region // Конструкторы

      /// <summary>
      /// Конструктор
      /// </summary>
      /// <param name="load_files_options"></param>
      public CFilesListBuilder(CLoadFilesOptions load_files_options)
      {
         m_Files = new List<string>();
         m_SkippingFolders = new HashSet<string>();
         m_LoadFilesOptions = load_files_options;
         BuildSkippingFoldersList(m_LoadFilesOptions.SkippingFolders);
      }

      /// <summary>
      /// 
      /// </summary>
      /// <param name="load_files_options"></param>
      /// <param name="ReportFilesListBuildingProgress"></param>
      public CFilesListBuilder(CLoadFilesOptions load_files_options, EventHandler ReportFilesListBuildingProgress)
         : this(load_files_options)
      {
         FilesListBuildingProgress += ReportFilesListBuildingProgress;
      }

      #endregion

      /// <summary>
      /// Получить список файлов в указанной директории
      /// </summary>
      /// <returns></returns>
      public List<string> GetFiles()
      {
         m_Files.Clear();

         if (m_LoadFilesOptions.MustWorkWithOneFile)
         {
            m_Files.Add(m_LoadFilesOptions.PathToFile);
            OnFilesListBuildingProgress();
         }
         else
         {
            ParseFolder(m_LoadFilesOptions.Directory);
         }

         return m_Files;
      }
   }
}