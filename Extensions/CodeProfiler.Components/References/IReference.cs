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
  /// Basic interface for references to projects or assemblies
  /// </summary>
  [Serializable]
  public abstract class IReference {
    public abstract string Name { get; }
    public abstract string FileName { get; }

    public bool Exists {
      get { return System.IO.File.Exists(FileName); }
    }

  }
}
