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
using System.Reflection;
using CodeProfiler.Runtime;

namespace CodeProfiler.Components {
  
  /// <summary>
  /// This class describes the basic construct of each Prof-It session: a solution.
  /// </summary>
  [Serializable]
  public class Solution {

    private string name;
    private ArrayList projects = new ArrayList();
    private Project startProject;
    private string rootPath;

    [NonSerialized]
    private ProfilingResult sfc = new ProfilingResult();

    [NonSerialized]
    private bool changed = false;

    public Solution(string name) {
      this.name = name;
    }

    public void AddProject(Project project) {
      projects.Add(project);
      project.SetSolution(this);
    }

    public string Name {
      get { return this.name; }
    }

    public ArrayList Projects {
      get { return projects; }
    }

    public Project StartProject {
      get { return startProject; }
      set { startProject = value; }
    }

    public SourceFile FindFile(string fileName) {
      foreach (Project project in projects) {
        SourceFile file = project.FindFile(fileName);
        if (file != null && File.Exists(file.FileName)) return file;
      }
      return null;
    }

    public string ExecutableFileName {
      get {
        return StartProject.OutputFilePath;
      }
    }

    public string ExecutablePath {
      get {
        string outputFileName = StartProject.OutputFilePath.Replace("/", "\\");
        return StartProject.OutputFilePath.Substring(0, outputFileName.LastIndexOf("\\"));
      }
    }

    public string CounterFileName {
      get {
        if( StartProject != null ) {
          return StartProject.WorkingDirectory + "\\" + name + "_runtime.counters";
        }
        else {
          return RootPath + "\\" + name + "_runtime.counters";
        }
      }
    }

    public string CounterFileName2 {
      get {
        if( StartProject != null ) {
          return StartProject.WorkingDirectory + "\\" + name + "_static.counters";
        }
        else {
          return RootPath + "\\" + name + "_static.counters";
        }
      }
    } 

    public string RootPath {
      get { return rootPath; }
      set { this.rootPath = value; }
    }


    public ArrayList GetFilesToProfile() {
      ArrayList result = new ArrayList();
      foreach (Project project in projects) {
        foreach (SourceFile file in project.Files) {
          if (file.Profile) result.Add(file);
        }
      }
      return result;
    }

    public Project GetProjectForFile(string fileName) {
      foreach (Project project in projects) {
        if (project.FindFile(fileName) != null) return project;
      }
      return null;
    }

    public Project GetProject(string projectName) {
      foreach (Project project in projects) {
        if (project.Name == projectName) return project;
      }
      return null;
    }


    internal void ResetCounters() {
      foreach (Project project in projects) {
        project.ResetCounters();
      }
      if (sfc != null) sfc.CalculateStatisticalValues();
    }

    public ProfilingResult GetLastProfilingResult() {
      return sfc;
    }

    public string StartParameters {
      get { return this.startProject.StartParameters; }
    }

    public bool AskForStartParameters {
      get { return this.startProject.AskForStartParameters; }
    }

    public bool ExecuteInOwnWindow {
      get { return this.startProject.ExecuteInOwnWindow; }
    }

    internal void UpdateProfilingResult() {
      sfc = new ProfilingResult();
      sfc.LastModification = DateTime.Now;
      foreach (Project project in projects) {
        foreach (SourceFile file in project.Files) {
          if (file.Profile && file.Counters != null) sfc.Add(file.Counters);
        }
      }
      sfc.CalculateStatisticalValues();
    }

    /// <summary>
    /// This method completely deletes all previous counters
    /// and imports the new profiling result.
    /// </summary>
    /// <param name="sfc">the new profiling results</param>
    internal ArrayList ImportCounters(ProfilingResult sfc) {
      ArrayList changedFiles = new ArrayList();
      foreach (Project project in projects) {
        foreach (SourceFile file in project.Files) {
          CounterCollection cc = sfc[file.FileName];
          if (cc == null) continue;
          if (File.GetLastWriteTime(file.FileName) < sfc[file.FileName].LastParseTime) {
            file.SetCounters(sfc[file.FileName]);
          } else {
            sfc.RemoveCounters(file.FileName);
            changedFiles.Add(file.FileName);
          }
        }
      }
      this.sfc = sfc;
      sfc.CalculateStatisticalValues();
      return changedFiles;
    }

    public bool CheckCounters(ProfilingResult sfc) {
      foreach (CounterCollection cc in sfc.GetCounters()) {
        SourceFile file = this.FindFile(cc.FileName);
        if (file == null) return false;
        if (System.IO.File.GetLastWriteTime(file.FileName) >= sfc.LastModification) return false;
      }
      return true;
    }

    /// <summary>
    /// This method checks the EntryPoint of the current solution by reflection.
    /// </summary>
    /// <returns>true, if main-method is declared with parameters; false otherwise</returns>
    public bool RequiresStartParameters() {
      if (File.Exists(this.ExecutableFileName)) {
        
        //Can't use this method because it would lock the executable
        //see http://blogs.blackmarble.co.uk/bm-bloggers/Rss.aspx?CategoryID=6
        //Assembly ass = Assembly.LoadFrom(this.ExecutableFileName);
        
        FileStream fs = new FileStream(this.ExecutableFileName,FileMode.Open);
        byte[] b = new byte[fs.Length];
        fs.Read(b,0,(int)fs.Length);
        Assembly asm = Assembly.Load(b);
        fs.Close();
        return asm.EntryPoint.GetParameters().Length > 0;
      } else return false;
    }

    /// <summary>
    /// Returns true if the solution has been changed since it has been
    /// stored the last time.
    /// </summary>
    public bool Changed {
      get { return changed; }
      set { changed = true; }
    }


    [NonSerialized]
    public CounterArray CounterArray;



  }

  public enum WDir { Root, Output, Browse }
}
