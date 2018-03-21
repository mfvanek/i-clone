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
  /// This class represents one class or struct in the instrumented source code
  /// and referes as to all MethodCounters of the class
	/// </summary>
	[Serializable]
	public class ClassCounter : BasicBlock {
    private ArrayList methods = new ArrayList();
    private string name;
    private string nameSpace;
    private Position bodyStart;

    public ClassCounter(Position start) : base(start) {
    }

    public override int Counter {
      get { 
        int result = 0;
        foreach (MethodBlock ctr in methods) {
          result += ctr.Counter;
        }
        return result;
      }
    }

    public override int Statements {
      get {         
        int result = 0;
        foreach (MethodBlock ctr in methods) {
          result += ctr.Statements;
        }
        return result; 
      }
    }

    public float Coverage {
      get {
        float total = 0;
        float executed = 0;
        foreach (MethodBlock ctr in methods) {
          foreach (BasicBlock block in ctr.blocks) {
            total += block.Statements;
            if (block.Counter > 0) executed += block.Statements;
          }
        }
        if (total==0) return 0;
        return executed / total * 100;
      }
    }

    public override int Executions {
      get {
        int result = 0;
        foreach (MethodBlock ctr in methods) {
          result += ctr.Executions;
        }
        return result;
      }
    }

    public void AddMethod(MethodBlock mctr) {
      methods.Add(mctr);
    }

    /// <summary>
    /// represents the class' name
    /// </summary>
    public string Name {
      get { return name; }
      set { name = value; }
    }

    /// <summary>
    /// the namespace the class resides in
    /// </summary>
    public string Namespace {
      get {
        return nameSpace;
      }
      set {
        nameSpace = value;
      }
    }

    /// <summary>
    /// like 'Namespace.ClassName'
    /// e.g.: 'System.Data.Constraint' , where 'System.Data' is the namespace
    /// </summary>
    public string FullyQualifiedName {
      get {
        if( Namespace.Length == 0 ) {
          return Name;
        }
        else {
          return Namespace + "." + Name;
        }
      }
    }

    public Position BodyStart {
      get { return bodyStart; }
      set { bodyStart = value; }
    }

    public override string ToString() {
      return "ClassCounter for " + Name;
    }
	}
}
