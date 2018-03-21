using System;

namespace CodeProfiler.Components
{
   /// <summary>
   /// This class represents a Basic Block whose counter can be calculated
   /// from another one 
   /// </summary>
   [Serializable]
   public class CalcBlock : BasicBlock
   {
      private BasicBlock refBlock;

      /// <summary>
      /// Creates a new block whose counter refers to another counter
      /// </summary>
      /// <param name="start">the start position of the block</param>
      /// <param name="refBlock">the block whose counter should be returned</param>
      public CalcBlock(Position start, BasicBlock refBlock)
         : base(start)
      {
         this.refBlock = refBlock;
      }

      public override int Counter
      {
         get { return refBlock.Counter; }
      }

      /// <summary>
      /// for methods and accessors this references to the body, which is sensible as they are executed equally often
      /// </summary>
      public BasicBlock Ref
      {
         get { return this.refBlock; }
         set { this.refBlock = value; }
      }
   }
}