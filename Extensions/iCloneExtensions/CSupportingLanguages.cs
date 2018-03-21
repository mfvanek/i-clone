using System;
using System.Collections.Generic;
using ISourceFilesLibrary.Classes.Languages;

namespace ICloneExtensions
{
   /// <summary>
   /// Поддерживаемые языки программирования
   /// </summary>
   public static class CSupportingLanguages
   {
      /// <summary>
      /// Названия поддерживаемых языков программирования
      /// </summary>
      private static Dictionary<LANGUAGES, string> m_LanguageNames;
      /// <summary>
      /// Строковые идентификаторы языков программирования
      /// </summary>
      private static Dictionary<LANGUAGES, string> m_LanguagePrefixes;

      /// <summary>
      /// Обязательная часть имени плагина
      /// </summary>
      private static string m_PartOfDllName = "_iclone_ext.dll";

      /// <summary>
      /// Конструктор по умолчанию
      /// </summary>
      static CSupportingLanguages()
      {
         m_LanguageNames = new Dictionary<LANGUAGES, string>();
         m_LanguageNames.Add(LANGUAGES.LANGUAGE_C_SHARP, "C#");
         m_LanguageNames.Add(LANGUAGES.LANGUAGE_C_PLUS_PLUS, "C++");
         m_LanguageNames.Add(LANGUAGES.LANGUAGE_C, "C");
         m_LanguageNames.Add(LANGUAGES.LANGUAGE_JAVA, "Java");
         m_LanguageNames.Add(LANGUAGES.LANGUAGE_UNKNOWN, string.Empty);

         m_LanguagePrefixes = new Dictionary<LANGUAGES, string>();
         m_LanguagePrefixes.Add(LANGUAGES.LANGUAGE_C_SHARP, "cs");
         m_LanguagePrefixes.Add(LANGUAGES.LANGUAGE_C_PLUS_PLUS, "cpp");
         m_LanguagePrefixes.Add(LANGUAGES.LANGUAGE_C, "c");
         m_LanguagePrefixes.Add(LANGUAGES.LANGUAGE_JAVA, "java");
         m_LanguagePrefixes.Add(LANGUAGES.LANGUAGE_UNKNOWN, string.Empty);
      }

      /// <summary>
      /// Получить название языка программирования с заданным идентификатором
      /// </summary>
      /// <param name="Language">Идентификатор языка программирования</param>
      /// <returns>Название языка программирования</returns>
      public static string LanguageName(LANGUAGES Language)
      {
         return m_LanguageNames[Language];
      }

      /// <summary>
      /// Получить строковый идентификатор языка программирования
      /// </summary>
      /// <param name="Language">Идентификатор языка программирования</param>
      /// <returns>Строковый идентификатор</returns>
      public static string LanguagePrefix(LANGUAGES Language)
      {
         return m_LanguagePrefixes[Language];
      }

      /// <summary>
      /// Получить имя плагина для поддержки данного языка программирования
      /// </summary>
      /// <param name="Language">Идентификатор языка программирования</param>
      /// <returns>Имя плагина</returns>
      public static string DllExtentionName(LANGUAGES Language)
      {
         if (Language == LANGUAGES.LANGUAGE_UNKNOWN)
         {
            return string.Empty;
         }

         return m_LanguagePrefixes[Language] + m_PartOfDllName;
      }

      /// <summary>
      /// Получить идентификатор языка программирования по имени плагина
      /// </summary>
      /// <param name="_DllExtentionName">Имя плагина</param>
      /// <returns>Идентификатор языка программирования</returns>
      public static LANGUAGES LanguageID(string _DllExtentionName)
      {
         int index = _DllExtentionName.IndexOf(m_PartOfDllName, StringComparison.OrdinalIgnoreCase);
         if (index > -1)
         {
            string prefix = _DllExtentionName.Substring(0, index);

            foreach (KeyValuePair<LANGUAGES, string> key_pair in m_LanguagePrefixes)
            {
               if (key_pair.Value.Equals(prefix, StringComparison.OrdinalIgnoreCase))
               {
                  return key_pair.Key;
               }
            }
         }

         return LANGUAGES.LANGUAGE_UNKNOWN;
      }

      /// <summary>
      /// Получить идентификатор языка программирования по его имени
      /// </summary>
      /// <param name="_LanguageName">Название языка программирования</param>
      /// <returns>Идентификатор языка программирования</returns>
      public static LANGUAGES GetLanguageIDByLanguageName(string _LanguageName)
      {
         foreach (KeyValuePair<LANGUAGES, string> key_pair in m_LanguageNames)
         {
            if (key_pair.Value.Equals(_LanguageName, StringComparison.OrdinalIgnoreCase))
            {
               return key_pair.Key;
            }
         }

         return LANGUAGES.LANGUAGE_UNKNOWN;
      }

      /// <summary>
      /// Маска, которая будет использоваться для загрузки расширений
      /// </summary>
      /// <returns></returns>
      public static string GetMaskOfDllExtensions()
      {
         return "*" + m_PartOfDllName;
      }
   }
}