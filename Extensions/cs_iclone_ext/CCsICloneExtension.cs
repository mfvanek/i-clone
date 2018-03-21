using CodeProfiler.CocoCS;
using CodeProfiler.Components;
using ICloneExtensions.Classes;
using ISourceFilesLibrary.Classes.CodeUnitsCollection;
using ISourceFilesLibrary.Classes.Languages;
using ISourceFilesLibrary.Classes.SyntacticUnit;

namespace ICloneExtensions.Cs
{
   /// <summary>
   /// Класс-плагин для обработки кода, написанного на языке C#
   /// </summary>
   public sealed class CCsICloneExtension : CLanguageCloneExtension
   {
      #region // Реализация интерфейса ICloneExtension

      /// <summary>
      /// Идентификатор языка программирования
      /// </summary>
      /// <returns></returns>
      public override LANGUAGES LanguageID()
      {
         return LANGUAGES.LANGUAGE_C_SHARP;
      }

      /// <summary>
      /// Получить возможные расширения файлов с исходным кодом для данного языка программирования
      /// </summary>
      /// <returns>Список возможных расширений через точку с запятой</returns>
      public override string GetSourceFileExtentions()
      {
         return "*.cs";
      }

      /// <summary>
      /// Получить возможные символы комментариев. Группы символов перечисляются через точку с запятой.
      /// Парные символы входят в одну группу и разделяются пробелом.
      /// </summary>
      /// <returns></returns>
      public override string GetCommentSymbols()
      {
         return "//;/* */;///";
      }

      /// <summary>
      /// Представить исходный код, содержащийся в файле, в виде набора токенов
      /// </summary>
      /// <param name="args"></param>
      /// <returns></returns>
      public override CCodeUnitsCollection Tokenize(CTokenizerParms args)
      {
         CCodeUnitsCollection result_collection = new CCodeUnitsCollection();
         Scanner lex = new Scanner(args.GetPath());
         CTokensIterator iter = new CTokensIterator(lex);
         foreach(Token token in iter)
         {
            result_collection.Add(ExtCodeUnitCreator.FromToken(token, args));
         }
         return result_collection;
      }

      /// <summary>
      /// Представить исходный код, содержащийся в файле, в виде набора синтаксических единиц
      /// </summary>
      /// <param name="args"></param>
      /// <returns></returns>
      public override CSyntacticUnitsCollection Syntacticize(CTokenizerParms args)
      {
         CSyntacticUnitsCollection syntactic_collection = new CSyntacticUnitsCollection();
         Parser parser = new Parser(new Scanner(args.GetPath()), string.Empty);
         CounterCollection counters = parser.Parse();

         if (parser.errors.count > 0)
         {
            //throw new System.Exception(args.GetPath() + " parse error");
         }
         else
         {
            SyntUnitCreator synt_creator = new SyntUnitCreator(args);
            foreach (MethodBlock mb in counters.MethodCounters)
            {
               syntactic_collection.Add(synt_creator.FromMethodBlock(mb));
            }
         }
         return syntactic_collection;
      }

      #endregion
   }
}