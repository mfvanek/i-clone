using System;
using System.Text;
using ISourceFilesLibrary.Classes.CodeUnitsCollection;
using ISourceFilesLibrary.Classes.CodeUnit;

namespace ISourceFilesLibrary.Classes.SourceFile
{
   /// <summary>
   /// 
   /// </summary>
   public static class CSourceFileContentSaverFactory
   {
      /// <summary>
      /// 
      /// </summary>
      /// <param name="_Path"></param>
      /// <param name="_Encoding"></param>
      /// <returns></returns>
      public static ISourceFileContentSaver Create(string _Path, Encoding _Encoding)
      {
         if (CCodeUnit.IsUseTokens())
            throw new NotImplementedException();
         else
            return new CEntireRowSourceFileContentSaver(_Path, _Encoding);
      }
   }
}