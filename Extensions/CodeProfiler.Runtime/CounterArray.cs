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

namespace CodeProfiler.Runtime
{
   /// <summary>
   /// This class is only a wrapper for an integer array and is
   /// used at runtime
   /// </summary>
   [Serializable]
   public class CounterArray
   {

      public int[] cnt;

      public int Length
      {
         get { return cnt.Length; }
      }

      public void SetArrayLength(int length)
      {
         cnt = new int[length];
      }

      [NonSerialized]
      public ArrayList Counters;
   }
}