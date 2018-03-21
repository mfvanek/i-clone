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
  /// This class describes a reference to another assembly
  /// </summary>
  [Serializable]
  public class AssemblyReference : IReference {
    private string fileName;
    private string name;

    public AssemblyReference(string fileName) {
      this.fileName = fileName;
      this.name = shortName(fileName);
    }

    private static string shortName(string longName) {
      int i = longName.LastIndexOf('\\');
      if (i == -1) return longName;
      return longName.Substring(i+1, longName.Length - i -5);
    }


    #region IReference Members

    public override string Name {
      get { return this.name; }
    }

    public override string FileName {
      get { return this.fileName; }
    }

    public override string ToString() {
      return System.IO.Path.GetFileName(fileName);
    }


    #endregion
  }
}
