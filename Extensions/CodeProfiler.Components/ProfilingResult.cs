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

namespace CodeProfiler.Components {

  /// <summary>
  /// This class collects all SourceFiles with blocks in it and 
  /// is used at runtime to manage all counters. This classes
  /// will be serialized by the Incrementor and will then be
  /// deserialized by the Profiler
  /// </summary>
  [Serializable]
  public class ProfilingResult {
    private Hashtable counters = new Hashtable();

    [NonSerialized]
    private DateTime lastModification;
    [NonSerialized]
    private int numberOfBlocks = -1;
    [NonSerialized]
    private int numberOfStatements = -1;
    [NonSerialized]
    private int numberOfMethods = -1;
    [NonSerialized]
    private int numberOfClasses = -1;
    [NonSerialized]
    private int maximumCounter = -1;
    [NonSerialized]
    private float codeCoverage = -1;

    public ProfilingResult() {
    }

    public ProfilingResult(ArrayList list) {
      foreach (CounterCollection cc in list) {
        Add(cc);
      }
    }

    public void Add(CounterCollection cnt) {
      this.counters.Add(cnt.FileName, cnt);
    }

    public void RemoveCounters(string fileName) {
      this.counters.Remove(fileName);
    }

    /// <summary>
    /// This indexer returns the SourceFile with the specified ID
    /// It is necessary for a quick access by the incrementor
    /// </summary>
    public CounterCollection this[string fileName] {
      get {return (CounterCollection)counters[fileName]; }
    }


    public ICollection GetCounters() {
      return counters.Values;
    }

    public int NumberOfStatementBlocks() {
      int result = 0;
      foreach (CounterCollection cc in counters.Values) {
        foreach (BasicBlock block in cc.blocks) {
          if (block is StatementBlock) result++;
        }
      }
      return result;
    }

    public void UpdateFile(SourceFile file) {
      counters.Remove(file.FileName);
      counters.Add(file.FileName, file.Counters);
    }

    public void ResetCounters() {
      foreach (CounterCollection cc in counters.Values) {
        cc.ResetCounters();
      }
    }

    public void ResetCounters(SourceFile file) {
      if (counters[file.FileName] != null) {
        ((CounterCollection)counters[file.FileName]).ResetCounters();
      }
    }

    /// <summary>
    /// Statistical values like number of methods are only calculated once
    /// to increase performance
    /// </summary>
    public void CalculateStatisticalValues() {
      this.numberOfBlocks = 0;
      this.numberOfStatements = 0;
      this.numberOfMethods = 0;
      this.numberOfClasses = 0;
      this.maximumCounter = 0;
      float executed = 0;
      foreach (CounterCollection cc in counters.Values) {
        cc.CalculateStatisticalValues();
        numberOfBlocks += cc.NumberOfBlocks;
        numberOfStatements += cc.NumberOfStatements;
        numberOfMethods += cc.NumberOfMethods;
        numberOfClasses += cc.NumberOfClasses;
        if (cc.MaximumCounter > maximumCounter) maximumCounter = cc.MaximumCounter;
        executed += cc.NumberOfExecutedStatements;
      }
      this.codeCoverage = (executed / NumberOfStatements) * 100;
    }

    #region statistical functions
    public DateTime LastModification {
      get { return this.lastModification; }
      set { this.lastModification = value; }
    }

    public int NumberOfBlocks     { get { return numberOfBlocks; } }
    public int NumberOfStatements { get { return numberOfStatements; } }
    public int NumberOfMethods    { get { return numberOfMethods;  } }
    public int NumberOfClasses    { get { return numberOfClasses; } }
    public int MaximumCounter     { get { return maximumCounter; } }
    public float CodeCoverage     { get { return codeCoverage;  } }

    #endregion
  }
}
