using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using ICloneExtensions.Interfaces;
using ISourceFilesLibrary.Classes.Languages;

namespace ICloneExtensions
{
   /// <summary>
   /// Доступные расширения (плагины)
   /// </summary>
   public static class CAvailableExtentions
   {
      /// <summary>
      /// Каталог с плагинами
      /// </summary>
      private static string m_ExtentionsFolder;

      /// <summary>
      /// Список доступных плагинов
      /// </summary>
      private static readonly Dictionary<LANGUAGES, ICloneExtension> m_ExtentionsList;

      /// <summary>
      /// Проверка загруженной сборки и создание экземпляра класса,
      /// реализующего интерфейс <see cref="ICloneExtensions.Interfaces.ICloneExtension"/>, если такой найден
      /// </summary>
      /// <param name="assembly">Сборка</param>
      /// <param name="Language">Идентификатор языка программирования, для которого загружается плагин</param>
      private static void ExamineAssembly(Assembly assembly, LANGUAGES Language)
      {
         foreach (Type type in assembly.GetTypes())
         {
            if (type.IsPublic)
            {
               if ((type.Attributes & TypeAttributes.Abstract) != TypeAttributes.Abstract)
               {
                  Type iface = type.GetInterface("ICloneExtensions.Interfaces.ICloneExtension", true);

                  if (iface != null)
                  {
                     ICloneExtension extention = (ICloneExtension)Activator.CreateInstance(type);
                     m_ExtentionsList.Add(Language, extention);
                     break;
                  }
               }
            }
         }
      }

      /// <summary>
      /// Конструктор
      /// </summary>
      static CAvailableExtentions()
      {
         m_ExtentionsList = new Dictionary<LANGUAGES, ICloneExtension>();
         m_ExtentionsFolder = AppDomain.CurrentDomain.BaseDirectory;
         string[] DllFilesInFolder = Directory.GetFiles(m_ExtentionsFolder, CSupportingLanguages.GetMaskOfDllExtensions());
         AppDomain NewDomainForAssemlyTesting = AppDomain.CreateDomain("NewCAvailableExtentionsDomain", null);

         try
         {
            foreach (string DllFileName in DllFilesInFolder)
            {
               try
               {
                  AssemblyName assembly_name = AssemblyName.GetAssemblyName(DllFileName);
                  Assembly assembly = NewDomainForAssemlyTesting.Load(assembly_name);
                  FileInfo dll_file = new FileInfo(DllFileName);
                  ExamineAssembly(assembly, CSupportingLanguages.LanguageID(dll_file.Name));
               }
               catch (TypeInitializationException)
               {
                  throw;
               }
               catch (FileLoadException)
               {
                  throw;
               }
            }
         }
         finally
         {
            AppDomain.Unload(NewDomainForAssemlyTesting);
         }
      }

      /// <summary>
      /// Получить список доступных расширений
      /// </summary>
      /// <returns></returns>
      public static Dictionary<LANGUAGES, ICloneExtension> GetExtentionsList()
      {
         return m_ExtentionsList;
      }

      /// <summary>
      /// Получить плагин по его идентификатору
      /// </summary>
      /// <param name="LanguageID"></param>
      /// <returns></returns>
      public static ICloneExtension GetExtention(LANGUAGES LanguageID)
      {
         try
         {
            return m_ExtentionsList[LanguageID];
         }
         catch (KeyNotFoundException)
         {
            throw new UnknownLanguageException(LanguageID);
         }
      }
   }
}