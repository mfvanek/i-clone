using System;

namespace CodeProfiler.Components
{
   /// <summary>
   /// Represents a position in the source code
   /// </summary>
   [Serializable]
   public class Position : IComparable
   {
      private int absByChars;
      private int instrOffset;
      private int absByBytes;
      private int line;
      private int col;

      /// <summary>
      /// 
      /// </summary>
      /// <param name="absByChars">absolute position in the original source code in characters 
      ///  (unicode meaning) starting at 0 including line breaks</param>
      /// <param name="instrOffset">positional difference in characters from the original to the
      ///   instrumented source code version (will have inserted braces)</param>
      /// <param name="line">line starting at 1</param>
      /// <param name="col">column by characters starting at 1</param>
      /// <param name="offset">in characters (unicode meaning)</param>
      public Position(int absByChars, int instrOffset, int absByBytes, int line, int col)
      {
         this.absByChars = absByChars;
         this.instrOffset = instrOffset;
         this.absByBytes = absByBytes;
         this.line = line;
         this.col = col;
      }

      public int Line
      {
         get
         {
            return line;
         }
      }

      public int Column
      {
         get
         {
            return col;
         }
      }

      /// <summary>
      /// Returns the absolute position in characters (unicode meaning)
      ///  - including all line breaks
      ///  - starting with 0
      ///  - BOM bytes (unicode file signature) do NOT count
      ///  - line ending chars count (CRLF in Windows results in a count of 2!)
      /// </summary>
      public int AbsByChars
      {
         get
         {
            return absByChars;
         }
      }

      /// <summary>
      /// Returns the absolute position in characters (unicode meaning)
      ///  in the instrumented version of the source file
      ///  - including all line breaks
      ///  - starting with 0
      ///  - BOM bytes (unicode file signature) do NOT count
      ///  - line ending chars count (CRLF in Windows results in a count of 2!)
      /// </summary>
      public int InstrAbsByChars
      {
         get
         {
            return absByChars + instrOffset;
         }
      }

      /// <summary>
      /// Returns the absolute position in bytes in the UNinstrumented source
      ///  and starting with 0
      ///  TODO: does not correspond to the char position (see Grammar.EndBlock)
      ///        (off by the token length)
      /// </summary>
      public int AbsByBytes
      {
         get
         {
            return absByBytes;
         }
      }

      /// <summary>
      /// -------------------   DEPRECATED -----------------------
      /// Returns the absolute position without all line breaks
      ///  (TODO: does this really work? check constructor!)
      /// </summary>
      public int AbsByCharsNoNL
      {
         get
         {
            // TODO: line breaks have two bytes in Windows? how can this work?
            //   Coco replaces (Scanner.NextCh()) lonely \r's but doesn't collapse \r\n?
            // TODO: why +1 !?
            return this.AbsByChars - line + 1;
         }
      }

      #region CompareTo, ToString, Equals, HashCode
      public int CompareTo(object p1)
      {
         Position p = (Position)p1;
         try
         {
            if (this.AbsByCharsNoNL < p.AbsByCharsNoNL) return -1;
            if (this.AbsByCharsNoNL > p.AbsByCharsNoNL) return 1;
         }
         catch (Exception e)
         {
            Console.WriteLine(e.StackTrace);
         }
         return 0;
      }

      public override string ToString()
      {
         return "(abs: " + absByChars + "), line: " + this.line + ", col: " + this.col;
      }

      public override bool Equals(object obj)
      {
         // see documentation:
         //  -> x.Equals(null) returns false.

         //Check for null and compare run-time types.
         if (obj == null || GetType() != obj.GetType())
            return false;

         Position pos = (Position)obj;
         return (pos.absByChars == this.absByChars);
      }

      public override int GetHashCode()
      {
         return absByChars;
      }
      #endregion

      #region operators
      public static bool operator <(Position p1, Position p2)
      {
         IComparable itfComp = (IComparable)p1;
         return (itfComp.CompareTo(p2) < 0);
      }

      public static bool operator >(Position p1, Position p2)
      {
         IComparable itfComp = (IComparable)p1;
         return (itfComp.CompareTo(p2) > 0);
      }

      public static bool operator <=(Position p1, Position p2)
      {
         IComparable itfComp = (IComparable)p1;
         return (itfComp.CompareTo(p2) <= 0);
      }

      public static bool operator >=(Position p1, Position p2)
      {
         IComparable itfComp = (IComparable)p1;
         return (itfComp.CompareTo(p2) >= 0);
      }

      //public static Position operator + (Position p1, int i) {
      //  // -1 is unsupported?
      //  Position p = new Position(p1.AbsByChars +i, -1, 0);
      //  p.abs2 = p1.abs2 + i;
      //  return p;
      //}
      #endregion
   }
}