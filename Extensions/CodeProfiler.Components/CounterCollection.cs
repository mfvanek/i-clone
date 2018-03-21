/*----------------------------------------------------------------------
Prof-It for C#
Copyright (c) 2004 Klaus Lehner, University of Linz

This program is free software; you can redistribute it and/or modify it 
under the terms of the GNU General Public License as published by the 
Free Software Foundation; either version 2, or (at your option) any 
later version.

This program is distributed in the hope that it will be useful, but 
WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY 
or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU General Public License 
for more details.

You should have received a copy of the GNU General Public License along 
with this program; if not, write to the Free Software Foundation, Inc., 
59 Temple Place - Suite 330, Boston, MA 02111-1307, USA.
----------------------------------------------------------------------*/

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

namespace CodeProfiler.Components {

  /// <summary>
  /// A CounterCollection contains the blocks of a File. As an CounterID it holds
  /// the absolute path of the file
  /// </summary>
  [Serializable]
  public class CounterCollection {

    public IList<BasicBlock> blocks = new List<BasicBlock>();
    internal IList<MethodBlock> methods = new List<MethodBlock>();
    internal IList<ClassCounter> classes = new List<ClassCounter>();
    private DateTime lastParseTime;
    // holds the positions and values (strings) of the semantic-preserving elements to insert
    //  into the source code as part of instrumentation
    private Dictionary<int, List<string>> syntaxElemInserts
      = new Dictionary<int, List<string>>();

    [NonSerialized]
    private int numberOfStatements = -1;
    [NonSerialized]
    private int maximumCounter = -1;
    [NonSerialized]
    private int numberOfExecutedStatements = -1;
    [NonSerialized]
    private float codeCoverage = -1;

    // absolute path of the corresponding file, serving as an unique id
    public string FileName { get; set; }

    public static string COUNTER_ID_PLACEHOLDER =
      "__insertCounterIDhere";
    
    public static string COUNTING_STMT_PLACEHOLDER =
      "__insertCounterHere";

    public static string COUNTING_EXP_PLACEHOLDER =
      "__insertCounterHere_Expression";

    public DateTime LastParseTime {
      get { return lastParseTime; }
      set { this.lastParseTime = value; }
    }

    public void AddBlock (BasicBlock block) {
      blocks.Add(block);
    }

    public void AddMethod (MethodBlock block) {
      methods.Add(block);
    }

    public void AddClass (ClassCounter block) {
      classes.Add(block);
    }

    /// <summary>
    /// only show "positive" blocks (blocks where start is less than end position)
    /// </summary>
    public IList<BasicBlock> VisibleBlocks {
      get {
        IList<BasicBlock> list = new List<BasicBlock>();
        foreach (BasicBlock block in blocks) {
          if (block.Start < block.End && block.Statements>0) list.Add(block);
        }
        return list; 
      }
    }

    public IList<MethodBlock> MethodCounters {
      get {
        return this.methods;
      }
    }

    public IList<ClassCounter> ClassCounters {
      get {
        return this.classes;
      }
    }

    /// <summary>
    /// checks the sanity of the blocks currently stored
    /// </summary>
    [ConditionalAttribute("DEBUG")]
    public void CheckBlockValidity() {
      // check whether each block has a Start and an End
      foreach( BasicBlock block in blocks ) {
        Debug.Assert( block.Start != null, "Grammar error: Block without Start" );
        Debug.Assert( block.End != null, "Grammar error: Block without End" );
        Debug.Assert( block.Statements >= 0, "Grammar error" );
      }
    }

    /// <summary>
    /// Removes all basic blocks that don't contain any statements and
    /// are not refered by CalcBlocks
    /// </summary>
    public void RemoveEmptyBlocks() {
      ArrayList negativeBlocks = new ArrayList();
      ArrayList referredBlocks = new ArrayList();

      // first filter out all negative blocks and all referredBlocks
      //  negative blocks don't really have any language content
      foreach (BasicBlock block in blocks) {
        if (block.Start.Equals( block.End ) ) {
          negativeBlocks.Add(block);
        }

        if (block is CalcBlock) {
          referredBlocks.Add(((CalcBlock)block).Ref);
        }
      }

      // copy all delegates among the negative blocks to another list, remove all other ones
      foreach (BasicBlock block in negativeBlocks) {
        if (!referredBlocks.Contains(block)) {
          int indexOfBlock = blocks.IndexOf(block);

          // if not last block in array
          if( indexOfBlock != blocks.Count - 1 ) {
            BasicBlock nextBlock = (BasicBlock)blocks[indexOfBlock + 1];
            
            // if the block to be removed is referred by a method, replace ref with
            //  the next block
            foreach( MethodBlock method in methods ) {
              if( method.Ref == block && method.blocks.Contains( nextBlock ) ) {
                method.Ref = nextBlock;
              }
            }
          }
          blocks.Remove(block);
        }
      }
    }

    /// <summary>
    /// Resets the counters of all blocks in the file
    /// </summary>
    public void ResetCounters() {
      foreach (BasicBlock block in blocks) {
        if (block is StatementBlock) {
          ((StatementBlock)block).ResetCounter();
        }
      }
      CalculateStatisticalValues();
    }

    /// <summary>
    /// adds a semantic-preserving syntax element in the form of a string at the absCharPos position
    ///  eg: in if-blocks without braces it is necessary to insert braces to preserve the semantics
    /// </summary>
    /// <param name="position">absolute character position</param>
    /// <param name="value">should be a normal C# grammar frase</param>
    public void AddSyntaxElement(int absCharPos, string  value ) {
      if( value.Equals( COUNTING_STMT_PLACEHOLDER ) ) {
        throw new ArgumentException( COUNTING_STMT_PLACEHOLDER + " is reserved for internal use!",
          "value");
      }
      if( value.Equals( COUNTING_EXP_PLACEHOLDER ) ) {
        throw new ArgumentException( COUNTING_EXP_PLACEHOLDER + " is reserved for internal use!",
          "value");
      }
      DoAddSyntaxElement( absCharPos, value );
    }

    private void DoAddSyntaxElement( int absCharPos, string value ) {
      List<string> curValues;

      // if an element already exists at this position, append it to the list
      //  (keep insert order)
      if( syntaxElemInserts.TryGetValue( absCharPos, out curValues ) ) {
        curValues.Add( value );
      }
      else {
        curValues = new List<string> { value };
        syntaxElemInserts.Add( absCharPos, curValues );
      }
    }

    /// <summary>
    /// instead of inserting the counter statement after all added syntax elements
    ///  and before the normal statements, insert it at the current position BETWEEN
    ///  the syntax elements. eg needed for lambdas
    ///  normal:  x => x+1
    ///  instrumented normally:  x => { return counter[x]++; x+1; } // DOESN'T work
    ///  instead:  x => { counter[x]++; return x+1; } //works
    ///                   ^^^^^^^^^^^^
    ///   note the different counter position, achieved by calling this method between
    ///   the insertions of "{" and "return"
    /// </summary>
    /// <param name="absCharPos">position in the original source file</param>
    public void ForceCounterStatementHere( int absCharPos ) {
      // to be later replace with the real counter (and index, etc)
      DoAddSyntaxElement( absCharPos, COUNTING_STMT_PLACEHOLDER );
    }

    // @see ForceCounterStatementHere but here basically the trailing ";" is omitted
    public void ForceCounterExpressionHere( int absCharPos ) {
      // to be later replace with the real counter (and index, etc)
      DoAddSyntaxElement( absCharPos, COUNTING_EXP_PLACEHOLDER );
    }

    // creates a new counter and inserts its id at the absCharPos
    public void ForceNewCounterIDHere( int absCharPos ) {
      // to be later replace with the real counter (and index, etc)
      DoAddSyntaxElement( absCharPos, COUNTER_ID_PLACEHOLDER );
    }

    // returns a list of semantic-preserving elements and their respective positions
    //  where they need to be inserted
    public IDictionary<int,List<string>> SyntaxElements {
      get {
        // create and return a deep copy
        var copy = new Dictionary<int, List<string>>( syntaxElemInserts.Count );

        foreach( var entry in syntaxElemInserts ) {
          var  valueCopy = new List<string>( entry.Value );
          copy.Add( entry.Key, valueCopy );
        }
        return copy;
      }
    }

    #region statistical functions
    public void CalculateStatisticalValues() {
      this.numberOfStatements = 0;
      this.maximumCounter = 0;
      this.numberOfExecutedStatements = 0;

      foreach (BasicBlock block in blocks) {
        this.numberOfStatements += block.Statements;
        if (block.Counter > maximumCounter) maximumCounter = block.Counter;
        if (block.Counter > 0) numberOfExecutedStatements += block.Statements;
      }
      this.codeCoverage = ((float)NumberOfExecutedStatements / (float)NumberOfStatements) * 100;
    }

    public int NumberOfBlocks     { get { return VisibleBlocks.Count; } }
    public int NumberOfStatements { get { return this.numberOfStatements; } }
    public int NumberOfMethods    { get { return methods.Count; } }
    public int NumberOfClasses    { get { return classes.Count; } }
    public int MaximumCounter     { get { return maximumCounter; } }
    public float CodeCoverage     { get { return this.codeCoverage; } }
    public int NumberOfExecutedStatements { get { return this.numberOfExecutedStatements; } }
    #endregion
	}
}
