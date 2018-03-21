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
using System.Globalization;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace CodeProfiler.Components {

  /// <summary>
  /// This class provides basic functionality for the handling with source files.
  /// </summary>
  public class FileUtility {

    /// <summary>
    /// Converts a long file name like 'c:\examples\test\Test.cs' to 'Test.cs'
    /// </summary>
    /// <param name="longFileName">the long filename (including the path)</param>
    /// <returns>only the name of the file (without its path)</returns>
    public static string ToShortFileName(string longFileName) {
      return longFileName.Substring(longFileName.LastIndexOf("\\")+1, longFileName.Length - longFileName.LastIndexOf("\\") - 1);
    }

    /// <returns>path with trailing backslash</returns>
    public static string GetPath(string longFileName) {
      return longFileName.Substring(0, FormatPath(longFileName).LastIndexOf("\\")+1);
    }

    public static string FormatPath(string path) {
      string newPath = path.Replace("/", "\\");
      newPath = newPath.Replace("\\\\\\", "\\");
      newPath = newPath.Replace("\\\\", "\\");
      return newPath;
    }

    /// <summary>
    /// Loads the text of a file
    /// </summary>
    /// <param name="fileName">the entire filename, including its path</param>
    /// <returns>the content of the file</returns>
    public static string LoadTextFile(string fileName) {
      // System.Text.Encoding.Default is very important!!
      try {
        System.IO.StreamReader sr = new System.IO.StreamReader(fileName, System.Text.Encoding.Default);
        string result = sr.ReadToEnd();
        sr.Close();
        return result;
      } catch {
        return "Could not find '" + fileName + "'.";
      }
    }


    /// <summary>
    /// This method shortens an absolute path, if it exceeds a specified limit
    /// </summary>
    /// <param name="path">absolute path</param>
    /// <param name="id">the id </param>
    /// <param name="max">maximum number of characters</param>
    /// <param name="extensionLength">the length of the extension of the file to remove</param>
    /// <returns>
    /// if id=1, path= c:\myProjects\AVeryLongDirectoryName\Test.cs and max=30 then
    /// this method returns:
    ///  
    /// 1: c:\myProjects\..\Test.cs
    /// </returns>
    public static string ConvertPathToMenuItem(string path, int id, int max, int extensionLength) {
      string shortPath = path.Substring(0, path.Length-extensionLength);
      shortPath = FormatPath(shortPath);
      int firstIndex = shortPath.IndexOf('\\');
      if (firstIndex > 0) {
        int secondIndex = shortPath.IndexOf('\\', firstIndex+1);
        while (shortPath.Length > max && secondIndex > 0) {
          shortPath = shortPath.Substring(0, firstIndex+1) + ".." + shortPath.Substring(secondIndex);
          secondIndex = shortPath.IndexOf('\\', firstIndex+4);
        }
      }
      if (id == -1) {
        return shortPath;
      } else {
        return "&" + id + ": " + shortPath;
      }
    }
  }

  public class CopyExpert {
    
    /// <summary>
    /// Uses a MemoryStream to create a DeepCopy of an object
    /// </summary>
    /// <param name="o">the object to copy, must be serializable</param>
    /// <returns>a deepcopy of o</returns>
    public static object DeepCopy(object o) {
      MemoryStream memoryStream = new MemoryStream();
      object result = null;
      try {
         
        BinaryFormatter binaryFormatter = new BinaryFormatter();
  
        binaryFormatter.Serialize(memoryStream, o);
        memoryStream.Seek(0, SeekOrigin.Begin);

        result =  binaryFormatter.Deserialize(memoryStream);
      } catch {
        result = null;
      } finally {
        memoryStream.Close();
      }
      return result;
    }
  }

  /// <summary>
  /// This class is used to format large numbers
  /// </summary>
  public class FormatString {
    public static System.Globalization.NumberFormatInfo numberFormat;

    static FormatString() {
      System.Threading.Thread.CurrentThread.CurrentCulture = CultureInfo.InstalledUICulture;
      numberFormat = (NumberFormatInfo)NumberFormatInfo.CurrentInfo.Clone();
    }

    private static string Int {        get { return "###,###,###,##0";    } }
    private static string Percentage { get { return             "##0.00"; } }

    public static int GetInt(string text) {
      return int.Parse(text, NumberStyles.AllowThousands, numberFormat);
    }
    public static double GetDouble(string text) {
      return double.Parse(text, NumberStyles.AllowThousands | NumberStyles.Float, numberFormat);
    }

    public static string GetString(int integerValue) {
      return integerValue.ToString(Int, numberFormat);
    }

    public static string GetString(double doubleValue) {
      return doubleValue.ToString(Percentage, numberFormat);
    }

    public static System.Globalization.NumberFormatInfo NumberFormat {
      get { return numberFormat; }
    }
  }

  public class SystemInfo {
    public static string ExeDirectory;
    public static bool CreateProfiledSourceFiles;
  }

  public class InstrumentationUtils {
    public static string COUNTER_ARRAY_NAME =
      "___profIt_Array";

    // a GUID-generated name suffix to avoid clashes with normal functions
    public static string LAMBDA_HELPER_FUNC_NAME =
      "LAMBDA_HELPER_A7605EB9_62E5_4231_9A50_B61BD0401865";

    // handles expressions evaluating to all but "void" (thus returning the value)
    public static string LAMBDA_HELPER_FUNC =
@"    private static RET " + LAMBDA_HELPER_FUNC_NAME + @"<RET>( int  counterID, System.Func<RET> ret ) {
      " + COUNTER_ARRAY_NAME + @"[counterID]++;
      return ret.Invoke();
    }

";

    // handles expressions evaluating to "void"
    public static string LAMBDA_HELPER_ACTION =
@"    private static void " + LAMBDA_HELPER_FUNC_NAME + @"( int  counterID, System.Action ret ) {
      " + COUNTER_ARRAY_NAME + @"[counterID]++;
      ret.Invoke();
    }

";

    // helper function for instrumenting lambda expressions with expression bodies
    public static string LAMBDA_HELPER_FUNC_CODE =
         LAMBDA_HELPER_FUNC +
         LAMBDA_HELPER_ACTION;


    public static string GetCountingExpression( int counterID ) {
      return COUNTER_ARRAY_NAME + "[" + counterID + "]++";
    }

    public static string GetCountingStatement( int counterID ) {
      return GetCountingExpression( counterID ) + ";";
    }
  }
}
