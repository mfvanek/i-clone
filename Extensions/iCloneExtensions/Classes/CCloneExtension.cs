using System;
using System.IO;
using System.Reflection;
using ICloneExtensions.Interfaces;
using ISourceFilesLibrary.Classes.CodeUnitsCollection;
using ISourceFilesLibrary.Classes.Languages;
using ISourceFilesLibrary.Classes.SyntacticUnit;

namespace ICloneExtensions.Classes
{
   /// <summary>
   /// Абстрактный класс для создания языковых плагинов
   /// </summary>
   public abstract class CLanguageCloneExtension : ICloneExtension
   {
      #region // Реализация интерфейса ICloneExtension

      /// <summary>
      /// Идентификатор языка программирования
      /// </summary>
      /// <returns></returns>
      public abstract LANGUAGES LanguageID();

      /// <summary>
      /// Получить название языка программирования
      /// </summary>
      /// <returns></returns>
      public string LanguageName()
      {
         return CSupportingLanguages.LanguageName(LanguageID());
      }

      /// <summary>
      /// Имя расширения
      /// </summary>
      /// <returns></returns>
      public virtual string ExtensionName()
      {
         object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
         if (attributes.Length > 0)
         {
            AssemblyTitleAttribute titleAttribute = (AssemblyTitleAttribute)attributes[0];
            if (!String.IsNullOrEmpty(titleAttribute.Title))
            {
               return titleAttribute.Title;
            }
         }
         return Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().CodeBase);
      }

      /// <summary>
      /// Версия расширения
      /// </summary>
      /// <returns></returns>
      public virtual string ExtensionVersion()
      {
         return Assembly.GetExecutingAssembly().GetName().Version.ToString();
      }

      /// <summary>
      /// Имя автора, создавшего расширение
      /// </summary>
      /// <returns></returns>
      public virtual string AuthorName()
      {
         object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCompanyAttribute), false);
         if (attributes.Length == 0)
         {
            return "";
         }
         return ((AssemblyCompanyAttribute)attributes[0]).Company;
      }

      /// <summary>
      /// Описание расширения
      /// </summary>
      /// <returns></returns>
      public virtual string Description()
      {
         object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false);
         if (attributes.Length == 0)
         {
            return "";
         }
         return ((AssemblyDescriptionAttribute)attributes[0]).Description;
      }

      /// <summary>
      /// Получить возможные расширения файлов с исходным кодом для данного языка программирования
      /// </summary>
      /// <returns>Список возможных расширений через точку с запятой</returns>
      public virtual string GetSourceFileExtentions()
      {
         return "*.s362;*.c;*.cc;*.cpp;*.hpp;*.h;*.hh;*.hss;*.java;*.cs";
      }

      /// <summary>
      /// Получить возможные символы комментариев. Группы символов перечисляются через точку с запятой.
      /// Парные символы входят в одну группу и разделяются пробелом.
      /// </summary>
      /// <returns></returns>
      public virtual string GetCommentSymbols()
      {
         return "//;/* */;///;<!-- -->;/** */";
      }

      /// <summary>
      /// Представить исходный код, содержащийся в файле, в виде набора токенов
      /// </summary>
      /// <param name="args"></param>
      /// <returns></returns>
      public abstract CCodeUnitsCollection Tokenize(CTokenizerParms args);

      /// <summary>
      /// Представить исходный код, содержащийся в файле, в виде набора синтаксических единиц
      /// </summary>
      /// <param name="args"></param>
      /// <returns></returns>
      public abstract CSyntacticUnitsCollection Syntacticize(CTokenizerParms args);

      #endregion

      /// <summary>
      /// Представить в виде строки
      /// </summary>
      /// <returns></returns>
      public override string ToString()
      {
         return LanguageName();
      }
   }
}