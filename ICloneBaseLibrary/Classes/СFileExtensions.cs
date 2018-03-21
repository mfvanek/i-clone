using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace ICloneBaseLibrary.Classes
{
   /// <summary>
   /// Класс для работы со списком расширений файлов
   /// </summary>
   public sealed class СFileExtensions
   {
      /// <summary>
      /// Список расширений файлов
      /// </summary>
      private List<string> m_FileExtensions;

      /// <summary>
      /// 
      /// </summary>
      /// <remarks>Вынес сюда для повышения производительности</remarks>
      private static Regex m_rgx = new Regex(@"\*");

      /// <summary>
      /// Конструктор по умолчанию
      /// </summary>
      public СFileExtensions()
      {
         m_FileExtensions = new List<string>();
      }

      /// <summary>
      /// Конструктор
      /// </summary>
      /// <param name="files_extensions"></param>
      public СFileExtensions(string files_extensions)
      {
         m_FileExtensions = new List<string>();
         SetFileExtensions(files_extensions);
      }

      /// <summary>
      /// Расширения файлов для загрузки
      /// </summary>
      public List<string> FileExtensions
      {
         get
         {
            return m_FileExtensions;
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
         if (!String.IsNullOrEmpty(value))
         {
            m_FileExtensions.Clear();

            string[] Extensions = value.Split(';');
            foreach (string item in Extensions)
            {
               Add(item);
            }
         }
         else
         {
            throw new ArgumentNullException();
         }
      }

      /// <summary>
      /// Добавить
      /// </summary>
      /// <param name="item"></param>
      /// <exception cref="ArgumentNullException">
      /// 
      /// </exception>
      public void Add(string item)
      {
         string buffer = m_rgx.Replace(item.Trim(), "");
         if (!String.IsNullOrEmpty(buffer))
         {
            m_FileExtensions.Add(buffer);
         }
         else
         {
            throw new ArgumentNullException();
         }
      }

      /// <summary>
      /// Определяет, равны ли два экземпляра
      /// </summary>
      /// <param name="obj"></param>
      /// <returns></returns>
      public override bool Equals(object obj)
      {
         if (obj == null)
         {
            return false;
         }

         // If parameter cannot be cast to Point return false.
         СFileExtensions p = obj as СFileExtensions;
         if ((object)p == null)
         {
            return false;
         }

         if (m_FileExtensions.Count != p.m_FileExtensions.Count)
         {
            return false;
         }

         for (int counter = 0; counter < m_FileExtensions.Count; counter++)
         {
            if (!m_FileExtensions[counter].Equals(p.m_FileExtensions[counter]))
            {
               return false;
            }
         }

         return true;
      }

      /// <summary>
      /// Хэш-код для текущего объекта
      /// </summary>
      /// <returns></returns>
      public override int GetHashCode()
      {
         return base.GetHashCode();
      }
   }
}