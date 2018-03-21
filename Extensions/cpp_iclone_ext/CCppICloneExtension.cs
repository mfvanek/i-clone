using ICloneExtensions.Classes;
using ISourceFilesLibrary.Classes.CodeUnit;
using ISourceFilesLibrary.Classes.CodeUnitsCollection;
using ISourceFilesLibrary.Classes.Languages;
using ISourceFilesLibrary.Classes.SyntacticUnit;

namespace ICloneExtensions.Cpp
{
   /// <summary>
   /// Класс-плагин для обработки кода, написанного на языке C++
   /// </summary>
   public sealed class CCppICloneExtension : CLanguageCloneExtension
   {
      #region // Реализация интерфейса ICloneExtension

      /// <summary>
      /// Идентификатор языка программирования
      /// </summary>
      /// <returns></returns>
      public override LANGUAGES LanguageID()
      {
         return LANGUAGES.LANGUAGE_C_PLUS_PLUS;
      }

      /// <summary>
      /// Получить возможные расширения файлов с исходным кодом для данного языка программирования
      /// </summary>
      /// <returns>Список возможных расширений через точку с запятой</returns>
      public override string GetSourceFileExtentions()
      {
         return "*.s362;*.c;*.cc;*.cpp;*.h;*.hh;*.hpp;*.hss";
      }

      /// <summary>
      /// Получить возможные символы комментариев. Группы символов перечисляются через точку с запятой.
      /// Парные символы входят в одну группу и разделяются пробелом.
      /// </summary>
      /// <returns></returns>
      public override string GetCommentSymbols()
      {
         return "//;/* */";
      }

      /// <summary>
      /// Представить исходный код, содержащийся в файле, в виде набора токенов
      /// </summary>
      /// <param name="args"></param>
      /// <returns></returns>
      public override CCodeUnitsCollection Tokenize(CTokenizerParms args)
      {
         throw new System.NotImplementedException();
         /*
         ICharStream input_stream = new ANTLRFileStream(args.GetPath(), args.GetEncoding());
         Lexer lex = GetLexer(input_stream);
         CommonTokenStream tokens = new CommonTokenStream(lex);

         CCodeUnitsCollection result_collection = new CCodeUnitsCollection();
         foreach (IToken token in tokens.GetTokens())
         {
            CElementPosition pos = new CElementPosition(token.Line, token.CharPositionInLine, token.Line, token.CharPositionInLine + token.Text.Length);
            CCodeUnit simple_unit = new CCodeUnit(pos, token.Text);
            CExtendedCodeUnit unit = new CExtendedCodeUnit(simple_unit, args.GetSourceFileID());
            result_collection.Add(unit);
         }
         return result_collection;
         */
      }

      /// <summary>
      /// Представить исходный код, содержащийся в файле, в виде набора синтаксических единиц
      /// </summary>
      /// <param name="args"></param>
      /// <returns></returns>
      public override CSyntacticUnitsCollection Syntacticize(CTokenizerParms args)
      {
         throw new System.NotImplementedException();
      }

      #endregion
   }
}