using System;
using System.Collections;
using System.Linq;
using System.Collections.Generic;

namespace CodeProfiler.Components
{
   /// <summary>
   /// This class represents one method in the instrumented source code
   /// and referes as a CalcBlock to the first block of the method / property / lambda expression
   /// </summary>
   [Serializable]
   public class MethodBlock : CalcBlock
   {
      public ArrayList blocks = new ArrayList();
      private string name;

      private static HashSet<string> MODIFIERS =
        new HashSet<string>();

      static MethodBlock()
      {
         MODIFIERS.Add("new");
         MODIFIERS.Add("public");
         MODIFIERS.Add("protected");
         MODIFIERS.Add("internal");
         MODIFIERS.Add("private");
         MODIFIERS.Add("unsafe");
         MODIFIERS.Add("static");
         MODIFIERS.Add("readonly");
         MODIFIERS.Add("volatile");
         MODIFIERS.Add("virtual");
         MODIFIERS.Add("sealed");
         MODIFIERS.Add("override");
         MODIFIERS.Add("abstract");
         MODIFIERS.Add("extern");
      }

      public MethodBlock(Position start)
         : base(start, null)
      {
      }

      // eg: public static void main(String[] args)
      public string Name
      {
         get { return name; }
         set { name = value; }
      }

      // name without modifiers
      //  eg: void main(String[] args)
      public string ShortName
      {
         get
         {
            string trimmedName = Name.Trim();
            // Split( null ) -> splits on whitespace
            //  then recombine them with ONE single space, thus reducing all duplicate whitespace
            //  to one
            //  eg: "public    static\t\t  hello   (  int   i )" would become "public static hello ( int i )"
            string shortName = trimmedName
              .Split(null)
              .Where(x => x.Length > 0)
              .Aggregate((x, y) => x + " " + y);

            // get rid of leading keywords
            //  cannot replace => eg "publicFunc" is a valid non-keyword ident
            for (; ; )
            {
               shortName = shortName.Trim();
               var parts = shortName.Split(null);

               if (parts.Length <= 1 ||
                   !MODIFIERS.Contains(parts[0]) ||
                   !Char.IsWhiteSpace(shortName[parts[0].Length]))
               {
                  break;
               }

               shortName = shortName.Substring(parts[0].Length);
            }

            return shortName;
         }
      }

      public void AddBlock(BasicBlock block)
      {
         blocks.Add(block);
      }

      public override int Statements
      {
         get
         {
            int result = 0;
            foreach (BasicBlock block in blocks)
            {
               result += block.Statements;
            }
            return result;
         }
      }

      public float Coverage
      {
         get
         {
            float total = 0;
            float executed = 0;
            foreach (BasicBlock block in blocks)
            {
               total += block.Statements;
               if (block.Counter > 0) executed += block.Statements;
            }
            if (total == 0) return 0;
            return executed / total * 100;
         }
      }

      public override int Executions
      {
         get
         {
            int result = 0;
            foreach (BasicBlock block in blocks)
            {
               result += block.Executions;
            }
            return result;
         }
      }


      public override string ToString()
      {
         return "MethodBlock for " + Name;
      }
   }
}