using System;
using System.Collections.Generic;
using System.Text;
using ICloneBaseLibrary.Classes;
//using ISourceFilesLibrary.Classes.CodePreProcessing;

namespace CodeLoadingLibrary.Classes
{
   /// <summary>
   /// Класс опций загрузки файлов
   /// </summary>
   public sealed class CLoadFilesOptions
   {
      #region // Поля класса

      /// <summary>
      /// Список расширений файлов для загрузки
      /// </summary>
      private СFileExtensions m_FileExtensions;
      /// <summary>
      /// Каталог, в котором находятся исходные файлы
      /// </summary>
      private string m_Directory;
      /// <summary>
      /// Загружать один файл, или несколько
      /// </summary>
      private bool m_LoadOnlyOneFile;
      /// <summary>
      /// Полный путь к выбранному файлу
      /// </summary>
      private string m_PathToFile;
      /// <summary>
      /// Кодировка файлов
      /// </summary>
      private Encoding m_FileEncoding;
      /// <summary>
      /// Использовать ли предварительную обработку файла при загрузке (удаление комментариев, пустых строк и т.п.)
      /// </summary>
      private bool m_IsPreProcessingFile;
      /// <summary>
      /// Параметры предварительной обработки файла
      /// </summary>
      private CCodePreProcessingOptions m_PreProcessingOptions;
      /// <summary>
      /// Список каталогов, которые не нужно обрабатывать (через точку с запятой)
      /// </summary>
      private string m_SkippingFolders;
      /// <summary>
      /// Использовать библиотеку параллельных задач
      /// </summary>
      private bool m_IsUseParallelExtensions;

      //private long m_Kmin;

      /// <summary>
      /// Вспомогательный объект для синхронизации потоков
      /// </summary>
      private object m_SyncRoot = new object();

      #endregion

      #region // Закрытые вспомогательные функции

      /// <summary>
      /// Проинициализировать поля класса
      /// </summary>
      private void InitMembers()
      {
         lock (m_SyncRoot)
         {
            m_FileExtensions = new СFileExtensions();
            m_Directory = string.Empty;
            m_LoadOnlyOneFile = false;
            m_PathToFile = string.Empty;
            m_FileEncoding = Encoding.GetEncoding("windows-1251");
            m_IsPreProcessingFile = false;
            m_PreProcessingOptions = new CCodePreProcessingOptions();
            m_SkippingFolders = string.Empty;
            m_IsUseParallelExtensions = false;
         }
      }

      #endregion

      #region // Конструкторы

      /// <summary>
      /// Конструктор по умолчанию
      /// </summary>
      public CLoadFilesOptions()
      {
         InitMembers();
      }

      /// <summary>
      /// Конструктор, задающий путь к обрабатываемому файлу
      /// </summary>
      /// <param name="path_to_file">Полный путь к обрабатываемому файлу</param>
      public CLoadFilesOptions(string path_to_file)
      {
         InitMembers();
         SetPathToFile(path_to_file);
      }

      /// <summary>
      /// Конструктор, задающий каталог с файлами и список расширений, которые будут использоваться для отбора файлов
      /// </summary>
      /// <param name="directory">Каталог, в котором находятся исходные файлы</param>
      /// <param name="file_extensions">Расширения файлов для загрузки (через точку с запятой)</param>
      public CLoadFilesOptions(string directory, string file_extensions)
      {
         InitMembers();
         SetDirectory(directory);
         SetFileExtensions(file_extensions);
      }

      /// <summary>
      /// Конструктор, задающий каталог с файлами, их кодировку и список расширений, которые будут использоваться для отбора файлов
      /// </summary>
      /// <param name="directory">Каталог, в котором находятся исходные файлы</param>
      /// <param name="file_extensions">Расширения файлов для загрузки (через точку с запятой)</param>
      /// <param name="files_encoding">Кодировка файлов</param>
      public CLoadFilesOptions(string directory, string file_extensions, Encoding files_encoding)
      {
         InitMembers();
         SetDirectory(directory);
         SetFileExtensions(file_extensions);
         SetFileEncoding(files_encoding);
      }

      /// <summary>
      /// Конструктор
      /// </summary>
      /// <param name="directory">Каталог, в котором находятся исходные файлы</param>
      /// <param name="file_extensions">Расширения файлов для загрузки (через точку с запятой)</param>
      /// <param name="files_encoding">Кодировка файлов</param>
      /// <param name="Options">Параметры предварительной обработки файла</param>
      public CLoadFilesOptions(string directory, string file_extensions, Encoding files_encoding, CCodePreProcessingOptions Options)
      {
         InitMembers();
         SetDirectory(directory);
         SetFileExtensions(file_extensions);
         SetFileEncoding(files_encoding);
         SetIsPreProcessingFile(true);
         SetPreProcessingOptions(Options);
      }

      #endregion

      /// <summary>
      /// Каталог, в котором находятся исходные файлы
      /// </summary>
      public string Directory
      {
         get
         {
            lock (m_SyncRoot)
            {
               return m_Directory;
            }
         }
      }

      /// <summary>
      /// Задать каталог, в котором находятся исходные файлы
      /// </summary>
      /// <param name="value"></param>
      /// <exception cref="ArgumentNullException">
      /// 
      /// </exception>
      public void SetDirectory(string value)
      {
         if (value != null)
         {
            lock (m_SyncRoot)
            {
               m_Directory = value;
               m_LoadOnlyOneFile = false;
            }
         }
         else
         {
            throw new ArgumentNullException();
         }
      }

      /// <summary>
      /// Полный путь к выбранному файлу
      /// </summary>
      public string PathToFile
      {
         get
         {
            lock (m_SyncRoot)
            {
               return m_PathToFile;
            }
         }
      }

      /// <summary>
      /// Задать полный путь к выбранному файлу
      /// </summary>
      /// <param name="value"></param>
      /// <exception cref="ArgumentNullException">
      /// 
      /// </exception>
      public void SetPathToFile(string value)
      {
         if (!String.IsNullOrEmpty(value))
         {
            lock (m_SyncRoot)
            {
               m_PathToFile = value;
               m_LoadOnlyOneFile = true;
            }
         }
         else
         {
            throw new ArgumentNullException();
         }
      }

      /// <summary>
      /// Расширения файлов для загрузки
      /// </summary>
      public List<string> FileExtensions
      {
         get
         {
            lock (m_SyncRoot)
            {
               return m_FileExtensions.FileExtensions;
            }
         }
      }

      /// <summary>
      /// Задать расширения файлов для загрузки
      /// </summary>
      /// <param name="value">Расширения файлов для загрузки (через точку с запятой)</param>
      /// <exception cref="ArgumentNullException">
      /// 
      /// </exception>
      public void SetFileExtensions(string value)
      {
         lock (m_SyncRoot)
         {
            m_FileExtensions.SetFileExtensions(value);
         }
      }

      /// <summary>
      /// Работаем с одним файлом или нет
      /// </summary>
      public bool MustWorkWithOneFile
      {
         get
         {
            lock (m_SyncRoot)
            {
               return m_LoadOnlyOneFile;
            }
         }
      }

      /// <summary>
      /// Кодировка файлов
      /// </summary>
      public Encoding FileEncoding
      {
         get
         {
            lock (m_SyncRoot)
            {
               return m_FileEncoding;
            }
         }
      }

      /// <summary>
      /// Задать кодировку файлов
      /// </summary>
      /// <param name="value">Кодировка файлов</param>
      public void SetFileEncoding(Encoding value)
      {
         lock (m_SyncRoot)
         {
            if (value != null)
            {
               m_FileEncoding = value;
            }
            else
            {
               throw new ArgumentNullException();
            }
         }
      }

      /// <summary>
      /// Использовать ли предварительную обработку файла при загрузке (удаление комментариев, пустых строк и т.п.)
      /// </summary>
      public bool IsPreProcessingFile
      {
         get
         {
            lock (m_SyncRoot)
            {
               return m_IsPreProcessingFile;
            }
         }
      }

      /// <summary>
      /// Задать флаг использования предварительной обработки файла при загрузке
      /// </summary>
      /// <param name="value"></param>
      public void SetIsPreProcessingFile(bool value)
      {
         lock (m_SyncRoot)
         {
            m_IsPreProcessingFile = value;
         }
      }

      /// <summary>
      /// Параметры предварительной обработки файла
      /// </summary>
      public CCodePreProcessingOptions PreProcessingOptions
      {
         get
         {
            lock (m_SyncRoot)
            {
               return m_PreProcessingOptions;
            }
         }
      }

      /// <summary>
      /// Задать параметры предварительной обработки файла
      /// </summary>
      /// <param name="value"></param>
      public void SetPreProcessingOptions(CCodePreProcessingOptions value)
      {
         if (value != null)
         {
            lock (m_SyncRoot)
            {
               m_PreProcessingOptions = value;
            }
         }
         else
         {
            throw new ArgumentNullException();
         }
      }

      /// <summary>
      /// Список каталогов, которые не нужно обрабатывать (через точку с запятой)
      /// </summary>
      public string SkippingFolders
      {
         get
         {
            lock (m_SyncRoot)
            {
               return m_SkippingFolders;
            }
         }
      }

      /// <summary>
      /// Задать список каталогов, которые не нужно обрабатывать (через точку с запятой)
      /// </summary>
      /// <param name="value"></param>
      public void SetSkippingFolders(string value)
      {
         lock (m_SyncRoot)
         {
            if (value != null)
            {
               m_SkippingFolders = value;
            }
            else
            {
               throw new ArgumentNullException();
            }
         }
      }

      /// <summary>
      /// Использовать библиотеку параллельных задач
      /// </summary>
      public bool IsUseParallelExtensions
      {
         get
         {
            lock (m_SyncRoot)
            {
               return m_IsUseParallelExtensions;
            }
         }
      }

      /// <summary>
      /// Задать флаг "Использовать библиотеку параллельных задач"
      /// </summary>
      /// <param name="value"></param>
      public void SetIsUseParallelExtensions(bool value)
      {
         lock (m_SyncRoot)
         {
            m_IsUseParallelExtensions = value;
         }
      }

      ///// <summary>
      ///// Минимально допустимый размер дублирующегося фрагмента кода
      ///// </summary>
      //public long Kmin
      //{
      //   get
      //   {
      //      lock (m_SyncRoot)
      //      {
      //         return m_Kmin;
      //      }
      //   }
      //}
   }
}