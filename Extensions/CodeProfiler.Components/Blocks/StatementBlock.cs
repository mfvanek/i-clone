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

namespace CodeProfiler.Components {

  /// <summary>
  /// A StatementBlock represents a basic block that can't be calculated
  /// from other blocks. An increment-statement for a counter has to be
  /// inserted at this position.
  /// </summary>
  [Serializable]
  public class StatementBlock : BasicBlock {
    private int counter = 0;
    private int counterId = -1;

    public StatementBlock( Position start )
      : base(start) {
    
    }

    /// <summary>
    /// necessary to identify the block to count its executions
    /// </summary>
    public int CounterID {
      get { return this.counterId; }
      set { this.counterId = value; }
    }

    /// <summary>
    /// The counter represents the execution frequency of the block
    /// </summary>
    public override int Counter {
      get { return this.counter; }
    }

    /// <summary>
    /// Resets its counter to zero.
    /// </summary>
    public virtual void ResetCounter() {
      this.counter = 0;
    }

    public void SetCounter(int cnt) {
      this.counter = cnt;
    }
  }
}
