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
using System.IO;
using System.Linq;
using System.Xml;
using System.Collections.Generic;

namespace CodeProfiler.Components {

  /// <summary>
  /// This class represents one SourceFile and all of its blocks.
  /// It does not contain the content of the file itself!!
  /// </summary>
  [Serializable]
  public class SourceFile {

    private string filename;
    private BuildAction buildAction;
    private bool profile = true;
    private Project project;

    /// <summary>
    /// The counters of a source file aren't stored in the project file.
    /// </summary>
    [NonSerialized]
    private CounterCollection counters;

    public SourceFile(Project project, string filename) {
      this.project = project;
      this.filename = filename;
    }

    /// <summary>
    /// Returns the current CounterCollection, if the SourceFile hasn't changed
    /// since the last parsing; null otherwise.
    /// </summary>
    public CounterCollection Counters {
      get {
        if (IsParsingNecessary)
          return null;
        else
          return this.counters;
      }
    }

    /// <summary>
    /// Sets the corresponding project of the SourceFile
    /// </summary>
    internal void SetProject(Project project) {
      this.project = project;
    }

    /// <summary>
    /// Returns the absolute path of the filename
    /// </summary>
    public string FileName {
      get { return project.RootPath + "\\" + filename; }
    }

    public string ProfiledFileName {
      get {
        string  extension = Path.GetExtension( FileName );
        if( extension == null  ) {
          return FileName;
        }
        else {
          // file type remains the same, but add some hint that its instrumented
          return  Path.GetDirectoryName( FileName ) + 
                  Path.DirectorySeparatorChar + 
                  Path.GetFileNameWithoutExtension( FileName ) +
                  ".profit"
                  + extension;
        }
     }
    }

    /// <summary>
    /// Returns the most specific (smallest) block at a specified position in the file
    /// </summary>
    /// <param name="absCharPos">the position of the block in absolute characters from the 
    ///   start of the file, beginning with 0 not including line breaks</param>
    /// <returns>null, if IsParsingNecessary is false or no block can be
    /// found at the specified position</returns>
    public BasicBlock GetBlock(int absCharPos) {
      if( absCharPos < 0 ) {
        throw new ArgumentException( "absCharPos mustn't be negative" );
      }

      if (IsParsingNecessary)
        return null;

      // find all blocks that this pos is part of
      var  matchingBlocks = new List<BasicBlock>();
      Func<BasicBlock, bool> incorpPosFunc = block => block.IncorporatesPos( absCharPos );

      matchingBlocks.AddRange( counters.blocks.Where( incorpPosFunc ) );
      matchingBlocks.AddRange( counters.methods.Cast<BasicBlock>().Where( incorpPosFunc ) );
      matchingBlocks.AddRange( counters.classes.Cast<BasicBlock>().Where( incorpPosFunc ) );

      if( matchingBlocks.Count == 0 ) {
        return null;
      }

      // sort the matching blocks according to their length
      matchingBlocks.Sort( new Comparison<BasicBlock>( ( x, y ) => x.Length - y.Length ) );

      // smallest is the best match
      return matchingBlocks[0];
    }

    /// <summary>
    /// returns the block that the given character position is part of
    /// </summary>
    /// <param name="absCharPos">starting at 0</param>
    /// <param name="list"></param>
    /// <returns></returns>
    private BasicBlock findBlock(int absCharPos, IEnumerable<BasicBlock> list) {
      foreach (BasicBlock block in list) {
        if( block.Start.AbsByCharsNoNL <= absCharPos && block.End.AbsByCharsNoNL >= absCharPos )
          return block;
      }
      return null;
    }

    /// <summary>
    /// Returns true if the file has to be included in profiling
    /// </summary>
    public bool Profile {
      get { return this.profile; }
    }

    /// <summary>
    /// Sets the value whether the file has to be profiled or not
    /// </summary>
    internal void SetProfile(bool value) { 
      this.profile = value; 
    }

    /// <summary>
    /// Returns true, if the file has been modified since the last parsing.
    /// </summary>
    public bool IsParsingNecessary {
      get {
        if (counters == null) return true;
        DateTime time = File.GetLastWriteTime(this.FileName);
        if (counters.FileName == null || counters.FileName.Length == 0)
          return true;
        return (time >= this.counters.LastParseTime);
      }
    }

    public Project Project { get { return project; } }

    public void SetCounters(CounterCollection c) {
      if( c == null ) {
        c = new CounterCollection();
      }

      counters = c;
      counters.FileName = FileName;
      counters.LastParseTime = c.LastParseTime;
    }

    /// <summary>
    /// Gets or sets the BuildAction for this file. (Compile, EmbeddedResource, Content)
    /// This is needed for VS.NET projects.
    /// </summary>
    public BuildAction BuildAction {
      get { return buildAction; }
      set { buildAction = value; }
    }


    #region Implementation of Equals and HashCode
    /// <summary>
    /// A CSFile is equal to another if it has the same FileName
    /// </summary>
    /// <param name="obj">object to compare, must be a CSFile</param>
    /// <returns>true if filenames are identical</returns>
    public override bool Equals(object obj) {
      if (!(obj is SourceFile)) return false;
      return this.FileName == ((SourceFile)obj).FileName;
    }

    public override int GetHashCode() {
      return FileName.GetHashCode ();
    }
    #endregion

    #region Print methods
    /// <summary>
    /// Prints all blocks of the file to the console
    /// </summary>
    public void PrintBlocks() {
      foreach (BasicBlock block in counters.blocks) {
        Console.WriteLine(block.ToString());
      }
    }

    public override string ToString() {
      return FileName;
    }
    #endregion
  }

  public enum BuildAction { Compile, EmbeddedResource, Content }
}
