using System.Collections;
using System.Collections.Generic;
using CodeProfiler.CocoCS;

namespace ICloneExtensions.Cs
{
   public class CTokensIterator : IEnumerable
   {
      List<Token> collection;

      public CTokensIterator(Scanner lex)
      {
         collection = new List<Token>();
         Token token = lex.Scan();
         while (token != null && token.kind != 0)
         {
            collection.Add(token);
            token = lex.Scan();
         }
      }

      public IEnumerator GetEnumerator()
      {
         foreach (Token t in collection)
            yield return t;
      }
   }
}
