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
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;


namespace CodeProfiler.Runtime
{
   /// <summary>
   /// This class holds the CounterArray, serializes and deserializes it,
   /// which makes it possible to increment counters at runtime.
   /// </summary>
   public class Incrementor
   {
      private static CounterArray counters;
      private static string _fileName;

      private static Incrementor incr = new Incrementor();

      private Incrementor()
      {
      }

      public static CounterArray Get(string fileName)
      {
         _fileName = fileName;
         if (counters != null) return counters;
         //Console.WriteLine("[Profiler] Trying to read " + fileName);
         try
         {
            using (FileStream myStream = File.OpenRead(fileName))
            {
               BinaryFormatter formatter = new BinaryFormatter();
               counters = (CounterArray)formatter.Deserialize(myStream);
               //Console.WriteLine("Using Counterarray with " + counters.Length + " counters");
            }
         }
         catch
         {
            Console.WriteLine("[Profiler] ERROR at reading counter file");
            Environment.Exit(-1);
         }
         return counters;
      }

      // TODO maybe think about another possibility
      ~Incrementor()
      {
         storeFile();
      }

      private void storeFile()
      {
         if (counters != null)
         {
            using (FileStream myStream = File.Create(_fileName))
            {
               BinaryFormatter formatter = new BinaryFormatter();
               formatter.Serialize(myStream, counters);
            }
         }
      }

   }
}