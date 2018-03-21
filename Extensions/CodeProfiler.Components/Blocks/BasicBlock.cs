using System;

namespace CodeProfiler.Components
{
   /// <summary>
   /// This is the basic class for each Basic Block
   ///  http://en.wikipedia.org/wiki/Basic_block
   /// </summary>
   [Serializable]
   public abstract class BasicBlock
   {
      private Position start;
      private Position end;
      private int statements;


      public BasicBlock(Position start)
      {
         this.start = start;
      }

      /// <summary>
      /// the first character INCLUDED in the block
      /// </summary>
      public Position Start
      {
         get { return this.start; }
         set { this.start = value; }
      }

      /// <summary>
      /// the position ONE PAST the last character included in the block
      ///  necessary to differentiate one-character blocks and empty blocks
      /// </summary>
      public Position End
      {
         get { return this.end; }
         set
         {
            if (Start > value)
            {
               throw new ArgumentException("Error (ATG): Block End is before Start");
            }

            this.end = value;
         }
      }

      /// <summary>
      /// end - start in characters including newline characters
      /// </summary>
      public int Length
      {
         get
         {
            return End.AbsByChars - Start.AbsByChars;
         }
      }

      /// <returns>true if and only if the character specified at absCharPos is part of the block</returns>
      public bool IncorporatesPos(int absCharPos)
      {
         if (absCharPos < 0)
         {
            throw new ArgumentException("absCharPos mustn't be negative");
         }
         return Start.AbsByCharsNoNL <= absCharPos && End.AbsByCharsNoNL >= absCharPos;
      }

      /// <summary>
      /// Increments the number of statements a block contains by one.
      ///  Note that the expressions inside if(_), while(_), etc also count as "statements" here
      ///  TODO: fix the concept of a "statement" in the overall project
      /// </summary>
      public void AddStatement()
      {
         statements++;
      }

      /// <summary>
      /// the number of statements in the block
      /// </summary>
      public virtual int Statements
      {
         get { return statements; }
      }

      /// <summary>
      /// The number of statement executions. can be calculated as Counter*Statements
      /// </summary>
      public virtual int Executions
      {
         get { return Statements * Counter; }
      }

      public static BasicBlock operator +(BasicBlock b1, BasicBlock b2)
      {
         return new CompoundBlock(b1, b2, 1);
      }

      public static BasicBlock operator -(BasicBlock b1, BasicBlock b2)
      {
         return new CompoundBlock(b1, b2, -1);
      }

      public abstract int Counter { get; }

      public override string ToString()
      {
         string type = "[" + this.GetType().ToString() + "]";
         if (start != null && end != null) return type + " Start " + start.ToString() + " / End " + end.ToString();
         if (end == null && start != null) return type + " Start " + start.ToString();
         else return type;
      }


      [Serializable]
      private class CompoundBlock : BasicBlock
      {
         private BasicBlock b1, b2;
         int factor;

         public CompoundBlock(BasicBlock b1, BasicBlock b2, int factor)
            : base(null)
         {
            this.b1 = b1;
            this.b2 = b2;
            this.factor = factor;
         }

         public override int Counter
         {
            get
            {
               return b1.Counter + (factor * b2.Counter);
            }
         }
      }
   }
}