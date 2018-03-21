using CodeProfiler.CocoCS;
using CodeProfiler.Components;
using ICloneExtensions.Classes;
using ISourceFilesLibrary.Classes.CodeFragment;
using ISourceFilesLibrary.Classes.CodeUnit;
using ISourceFilesLibrary.Classes.CodeUnitsCollection;
using ISourceFilesLibrary.Classes.SyntacticUnit;

namespace ICloneExtensions.Cs
{
   internal class ExtCodeUnitCreator
   {
      public static CExtendedCodeUnit FromToken(Token token, CTokenizerParms args)
      {
         CElementPosition pos = new CElementPosition(token.line, token.col, token.line, token.col + token.val.Length);
         CCodeUnit simple_unit = new CCodeUnit(pos, token.val);
         CExtendedCodeUnit unit = new CExtendedCodeUnit(simple_unit, args.GetFileID().SourceFileID);
         return unit;
      }
   }

   internal class SyntUnitCreator
   {
      CTokenizerParms m_args;
      CTokensIterator iter;
      CCodeUnitsCollection code_units_collection;

      public SyntUnitCreator(CTokenizerParms args)
      {
         m_args = args;
         iter = new CTokensIterator(new Scanner(args.GetPath()));
      }

      private bool IsTokenBelongMethodBlock(Token token, MethodBlock mb)
      {
         if (token.line == mb.Start.Line && token.col >= mb.Start.Column)
            return true;

         if (token.line > mb.Start.Line && token.line < mb.Ref.End.Line)
            return true;

         if (token.line == mb.Ref.End.Line && token.col <= mb.Ref.End.Column)
            return true;

         return false;
      }

      private bool IsTokenAfterMethodBlock(Token token, MethodBlock mb)
      {
         if (token.line == mb.Ref.End.Line && token.col > mb.Ref.End.Column)
            return true;

         if (token.line > mb.Ref.End.Line)
            return true;

         return false;
      }

      private void BuildCCodeUnitsCollection(MethodBlock mb)
      {
         code_units_collection = new CCodeUnitsCollection();
         foreach (Token token in iter)
         {
            if (IsTokenBelongMethodBlock(token, mb))
               code_units_collection.Add(ExtCodeUnitCreator.FromToken(token, m_args));
            else
            {
               if (IsTokenAfterMethodBlock(token, mb))
               {
                  const string METHOD_END_TOKEN = "}";
                  if ((token.val == METHOD_END_TOKEN) && (code_units_collection.back().Text != METHOD_END_TOKEN))
                     code_units_collection.Add(ExtCodeUnitCreator.FromToken(token, m_args));
                  break;
               }
            }
         }
      }

      public CSyntacticUnit FromMethodBlock(MethodBlock mb)
      {
         BuildCCodeUnitsCollection(mb);
         CCodeFragment fragment = new CCodeFragment(code_units_collection);
         CSyntacticUnit unit = new CSyntacticUnit(fragment);
         return unit;
      }
   }
}
