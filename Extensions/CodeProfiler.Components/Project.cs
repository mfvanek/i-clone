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
using System.Reflection;
using System.Collections;
using System.IO;

namespace CodeProfiler.Components {

  /// <summary>
  /// A Project is included in exactly one solution and contains one or
  /// more SoureFiles
  /// </summary>
  [Serializable]
  public class Project {
    private string name;
    private string dir;
    private static string outputDirectory = "bin" + Path.DirectorySeparatorChar + "Profiled";
    private string startParameters;
    private ArrayList references = new ArrayList();
    private string externalId;
    private string externalFileName;

    private bool askForStartParameters = true;
    internal string workingDirectory = "";
    internal WDir workingDir = WDir.Root;
    private bool executeInOwnWindow = false;
    private OutputType outputType;
    private Solution solution;

    private ArrayList files = new ArrayList();

    /// <summary>
    /// Creates a new project with the specified name
    /// </summary>
    /// <param name="name">the name of the project</param>
    public Project(string name) {
      this.name = name;
    }

    /// <summary>
    /// Creates a new project with the specified name and parent solution
    /// </summary>
    /// <param name="name">the name of the project</param>
    /// <param name="solution">the solution that contains the project</param>
    public Project(string name, Solution solution) {
      this.name = name;
      this.solution = solution;
    }

    /// <summary>
    /// Adds a new file to the project only if the file is located in the 
    /// root path of the project or in a sub directory of the root path.
    /// </summary>
    /// <param name="filename">filename of the new file</param>
    public void AddFile(string filename) {
      if (FileUtility.FormatPath(filename).StartsWith(FileUtility.FormatPath(this.RootPath))) {
        SourceFile file =
          new SourceFile(this, filename.Remove(0, FileUtility.FormatPath(this.RootPath).Length));
        this.files.Add(file);
      } else {
        Console.WriteLine("error in adding file");
      }
    }

    /// <summary>
    /// Adds a new file to the project
    /// </summary>
    /// <param name="file">a new file</param>
    public void AddFile(SourceFile file) {
      this.files.Add(file);
    }

    public SourceFile FindFile(string fileName) {
      foreach (SourceFile file in files) {
        if (file.FileName == fileName) return file;
      }
      return null;
    }

    public void RemoveFile(SourceFile file) {
      files.Remove(file);
    }

    public string[] FileNames {
      get { string[] result = new string[files.Count];
        int i=0;
        foreach (SourceFile file in files) {
          result[i++] = file.FileName;
        }
        return result;
      }
    }

    public string RootPath {
      get { return FileUtility.FormatPath( solution.RootPath + Path.DirectorySeparatorChar + this.dir ); }
    }

    /// <summary>
    /// without trailing directory separator
    /// </summary>
    public string DebugOutDir {
      get {
        return RootPath + Path.DirectorySeparatorChar +
          "bin" + Path.DirectorySeparatorChar + "Debug";
      }
    }

    /// <summary>
    /// without trailing directory separator
    /// </summary>
    public string ReleaseOutDir {
      get {
        return RootPath + Path.DirectorySeparatorChar +
          "bin" + Path.DirectorySeparatorChar + "Release";
      }
    }

    public void SetRelativeDirectory(string dir) {
      this.dir = dir;
    }

    public string Name {
      get { return this.name; }
    }

    internal void SetName(string name) {
      this.name = name;
    }

    public ArrayList Files {
      get { return this.files; }
    }

    public string OutputDirectory {
      get {
        return solution.RootPath + Path.DirectorySeparatorChar +
          this.dir + Path.DirectorySeparatorChar + outputDirectory;
      }
      set { 
        outputDirectory = value; 
      }
    }

    public string RelativeOutputDirectory {
      get { return outputDirectory; }
    }

    public OutputType OutputType {
      get { return this.outputType; }
      set { this.outputType = value; }
    }

    public string OutputFilePath {
      get { 
        if (this.outputType == OutputType.Library)
          return this.OutputDirectory + Path.DirectorySeparatorChar + this.Name + ".dll";
        else
          return this.OutputDirectory + Path.DirectorySeparatorChar + this.Name + ".exe";
      }
    }

    public string OutputFileName {
      get {
        return Path.GetFileName( OutputFilePath );
      }
    }

    public void setWorkingDirectory(WDir dir, string path) {
      this.workingDir = dir;
      this.workingDirectory = path;
    }

    public string StartParameters { 
      get { return this.startParameters; }
      set { startParameters = value; }
    }

    public bool AskForStartParameters {
      get { return this.askForStartParameters;  } 
      set { this.askForStartParameters = value; }
    }

    public bool ExecuteInOwnWindow {
      get { return this.executeInOwnWindow; }
      set { this.executeInOwnWindow = value; }
    }

    public string WorkingDirectory {
      get { 
        switch (workingDir) {
          case WDir.Output: return FileUtility.FormatPath(OutputDirectory);
          case WDir.Root:   return FileUtility.FormatPath(this.RootPath);
          case WDir.Browse: return FileUtility.FormatPath(this.workingDirectory);
          default:          return FileUtility.FormatPath(this.workingDirectory);
        }
      }
    }

    public Solution Solution { get { return solution; } }

    public WDir WorkingDirectoryType {
      get { return workingDir; }
    }

    /// <summary>
    /// Sets all counters to zero.
    /// </summary>
    public void ResetCounters() {
      foreach (SourceFile file in this.files) {
        if (file.Counters != null)  file.Counters.ResetCounters();
      }
    }


    [NonSerialized]
    bool buildExists = false;

    public bool BuildExists {
      get { return System.IO.File.Exists(this.OutputFilePath) && buildExists; }
      set { buildExists = value; }
    }


    /// <summary>
    /// Adds a new reference to the project
    /// </summary>
    /// <param name="reference">the new reference</param>
    public void AddReference(IReference reference) {
      this.references.Add(reference);
    }

    /// <summary>
    /// Adds a list of references to the project
    /// </summary>
    /// <param name="references">a collection of IReferences</param>
    public void AddReferences(ICollection references) {
      this.references.AddRange(references);
    }

    /// <summary>
    /// Removes all references from the project
    /// </summary>
    public void ClearReferences() {
      this.references.Clear();
    }

    /// <summary>
    /// Removes one reference from the project
    /// </summary>
    /// <param name="reference">the reference to remove</param>
    public void RemoveReference(IReference reference) {
      this.references.Remove(reference);
    }

    /// <summary>
    /// Returns the list of references
    /// </summary>
    public ArrayList References {
      get { return references; }
    }

    public string ExternalId {
      get { return this.externalId; }
      set { this.externalId = value; }
    }

    public string ExternalFileName {
      get { return this.externalFileName; }
      set { this.externalFileName = value; }
    }

    internal void SetSolution(Solution solution) {
      this.solution = solution;
    }

    /// <returns>The name of the project</returns>
    public override string ToString() {
      return this.Name;
    }
  }

  /// <summary>
  /// This enum is used to determine if a project is compiled to
  /// an executable or to a library
  /// </summary>
  public enum OutputType { Library, WinExe }
}
