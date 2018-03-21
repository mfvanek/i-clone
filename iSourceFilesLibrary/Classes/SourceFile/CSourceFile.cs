using System;
using System.IO;
using System.Text;
using ISourceFilesLibrary.Classes.CodeFragment;
using ISourceFilesLibrary.Classes.CodeUnit;

namespace ISourceFilesLibrary.Classes.SourceFile
{
   /// <summary>
   /// Класс, представляющий файл с исходным кодом на любом языке программирования
   /// </summary>
   public sealed class CSourceFile : ISizeableElement
   {
      #region // Члены класса

      /// <summary>
      /// Уникальный идентификатор файла
      /// </summary>
      private CSourceFileID m_SourceFileID;

      /// <summary>
      /// Имя файла с исходным кодом без указания пути
      /// </summary>
      private string m_FileName;

      /// <summary>
      /// Путь к файлу с исходным кодом
      /// </summary>
      private string m_FilePath;

      /// <summary>
      /// Кодировка файла
      /// </summary>
      private Encoding m_FileEncoding;

      /// <summary>
      /// Содержимое файла
      /// </summary>
      private CCodeFragment m_Content;

      #endregion

      #region // Закрытые вспомогательные функции

      /// <summary>
      /// Получить и установить имя файла с исходным кодом и путь к нему
      /// </summary>
      /// <param name="FullPath"></param>
      private void SetPathAndFileNameFromString(string FullPath)
      {
         string[] PathAndName = GetPathAndFileNameFromFullPath(FullPath);

         this.Name = PathAndName[0];
         this.FilePath = PathAndName[1];
      }

      /// <summary>
      /// Выделить из полного пути имя файла и сам путь
      /// </summary>
      /// <param name="FullPath"></param>
      /// <returns></returns>
      private string[] GetPathAndFileNameFromFullPath(string FullPath)
      {
         string[] retval = new string[2];

         string[] splitted_str = FullPath.Split('\\');
         retval[0] = splitted_str[splitted_str.Length - 1];
         retval[1] = FullPath.Substring(0, FullPath.Length - retval[0].Length);

         return retval;
      }

      #endregion

      #region // Конструкторы

      /// <summary>
      /// 
      /// </summary>
      /// <param name="_FileID"></param>
      /// <param name="FullPathAndFileName"></param>
      /// <param name="file_encoding"></param>
      /// <param name="_Content"></param>
      public CSourceFile(CSourceFileID _FileID, string FullPathAndFileName, Encoding file_encoding, CCodeFragment _Content)
      {
         m_SourceFileID = _FileID;
         m_FileEncoding = file_encoding;
         m_Content = _Content;

         if (!String.IsNullOrEmpty(FullPathAndFileName))
         {
            SetPathAndFileNameFromString(FullPathAndFileName);
         }
         else
            throw new ArgumentNullException("FullPathAndFileName");
      }

      /// <summary>
      /// 
      /// </summary>
      /// <param name="FullPathAndFileName">Полный путь к файлу, включая имя файла</param>
      /// <param name="file_encoding">Кодировка файла</param>
      /// <param name="_Content"></param>
      public CSourceFile(string FullPathAndFileName, Encoding file_encoding, CCodeFragment _Content)
         : this(new CSourceFileID(), FullPathAndFileName, file_encoding, _Content)
      {}

      #endregion

      #region

      /// <summary>
      /// Уникальный идентификатор файла
      /// </summary>
      public long SourceFileID
      {
         get
         {
            return m_SourceFileID.SourceFileID;
         }
      }

      /// <summary>
      /// Имя файла
      /// </summary>
      public string Name
      {
         get
         {
            return m_FileName;
         }

         set
         {
            m_FileName = value;
         }
      }

      /// <summary>
      /// Путь к файлу
      /// </summary>
      public string FilePath
      {
         get
         {
            return m_FilePath;
         }

         set
         {
            m_FilePath = value;
         }
      }

      /// <summary>
      /// Размер файла (в единицах кода)
      /// </summary>
      /// <returns></returns>
      public long Size()
      {
         return m_Content.Size();
      }

      /// <summary>
      /// Кодировка файла
      /// </summary>
      public Encoding FileEncoding
      {
         get
         {
            return m_FileEncoding;
         }
      }

      /// <summary>
      /// Сохранить файл на диске, используя имя и каталог, заданные по умолчанию
      /// </summary>
      /// <exception cref="IOException">
      /// Исключение генерируется, если не удалось создать файл на диске
      /// </exception>
      /// <exception cref="ArgumentNullException">
      /// Исключение генерируется, если не заданы имя файла и путь к нему
      /// </exception>
      /// <exception cref="Exception">
      /// Исключение генерируется, если не удалось записать данные в файл
      /// </exception>
      public void ToFile()
      {
         ToFile(System.IO.Path.Combine(m_FilePath + m_FileName));
      }

      /// <summary>
      /// Сохранить файл на диске, используя указанный путь.
      /// Будет использовано имя файла, заданное по умолчанию
      /// </summary>
      /// <param name="NewFilePath">Новый путь к файлу</param>
      /// <exception cref="IOException">
      /// Исключение генерируется, если не удалось создать файл на диске
      /// </exception>
      /// <exception cref="ArgumentNullException">
      /// Исключение генерируется, если параметр "NewFilePath" не задан
      /// </exception>
      /// <exception cref="Exception">
      /// Исключение генерируется, если не удалось записать данные в файл
      /// </exception>
      public void ToFile(string NewFilePath)
      {
         ToFile(NewFilePath, m_FileName);
      }

      /// <summary>
      /// Сохранить файл на диске, используя указанные путь и имя файла
      /// </summary>
      /// <param name="NewFilePath">Новый путь к файлу</param>
      /// <param name="NewFileName">Новое имя файла</param>
      /// <exception cref="IOException">
      /// Исключение генерируется, если не удалось создать файл на диске
      /// </exception>
      /// <exception cref="ArgumentNullException">
      /// Исключение генерируется, если параметры "NewFilePath" или "NewFileName" не заданы
      /// </exception>
      /// <exception cref="Exception">
      /// Исключение генерируется, если не удалось записать данные в файл
      /// </exception>
      public void ToFile(string NewFilePath, string NewFileName)
      {
         ISourceFileContentSaver saver = CSourceFileContentSaverFactory.Create(Path.Combine(NewFilePath, NewFileName), m_FileEncoding);
         saver.Save(this);
      }

      #endregion

      /// <summary>
      /// 
      /// </summary>
      public CCodeFragment Content
      {
         get
         {
            return m_Content;
         }

         set
         {
            m_Content = value;
         }
      }
   }
}