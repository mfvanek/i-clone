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

namespace CodeProfiler.Components {

  /// <summary>
  /// This classes performs internal changes to solutions, projects and files
  /// </summary>
  public class SolutionController {
    protected static Solution currentSolution;
    protected static ProfilingResult lastParsedCollection = new ProfilingResult();
    public static event ChangedHandler Changed;
    public delegate void ChangedHandler(ProfItAction action, object eventObj);

    /// <summary>
    /// Returns the currently opened solution
    /// </summary>
    public static Solution CurrentSolution {
      get { return currentSolution; }
    }

    /// <summary>
    /// Raises a Changed-Event
    /// </summary>
    /// <param name="action">the action that has been performed</param>
    /// <param name="eventObj">the object that is responsible for the event</param>
    protected static void PerformChanged(ProfItAction action, object eventObj) {
      Changed(action, eventObj);
    }


    /// <summary>
    /// Resets all counters in the current solution and raises a CounterReset-Event
    /// </summary>
    public static void ResetCounters() {
      if (currentSolution != null) {
        currentSolution.ResetCounters();
        Changed(ProfItAction.CounterReset, currentSolution);
      }
    }

    /// <summary>
    /// Resets the counters for one file and raises a CounterReset-Event
    /// </summary>
    public static void ResetCounters(SourceFile file) {
      if (currentSolution != null) {
        if (file.Counters != null) {
          file.Counters.ResetCounters();
        }
      }
      Changed(ProfItAction.CounterReset, file);
    }

    /// <summary>
    /// Sets the state of the file (include in profiling or not) and
    /// makes the build of the project invalid.
    /// Raises a BlocksMerged-Event
    /// </summary>
    /// <param name="file">source file</param>
    /// <param name="value">true, if file should be included in profiling</param>
    public static void SetProfile(SourceFile file, bool value) {
      if (currentSolution != null) {
        file.SetProfile(value);
        currentSolution.GetProjectForFile(file.FileName).BuildExists = false;
        Changed(ProfItAction.BlocksMerged, file);
      }
    }

    /// <summary>
    /// Imports a new ProfilingResult into the current solution and raises
    /// an CountersImported event
    /// </summary>
    /// <returns>A list of all fileNames whose files have been changed since the last parse time</returns>
    public static ArrayList ImportProfilingResults(ProfilingResult sfc) {
      ArrayList list = currentSolution.ImportCounters(sfc);
      Changed(ProfItAction.CountersImported, sfc);
      return list;
    }

    /// <summary>
    /// Updates the profiling results of the solution
    /// </summary>
    protected static void UpdateProfilingResult(SourceFile file) {
      if (currentSolution != null) {
        currentSolution.UpdateProfilingResult();
        Changed(ProfItAction.BlocksMerged, file);
      }
    }

    /// <summary>
    /// Updates the profiling results of the solution
    /// </summary>
    protected static void UpdateProfilingResult() {
      UpdateProfilingResult(null);
    }

    /// <summary>
    /// Updates the solution properties of the currentSolution.
    /// </summary>
    /// <param name="solution">new solution</param>
    public static void UpdateSolutionProperties(Solution solution) {
      if (solution != null) {
        for (int i=0; i<currentSolution.Projects.Count; i++) {
          Project project = (Project)currentSolution.Projects[i];
          project.StartParameters = ((Project)solution.Projects[i]).StartParameters;
          project.OutputType = ((Project)solution.Projects[i]).OutputType;

          project.ClearReferences();
          foreach (IReference reference in ((Project)solution.Projects[i]).References) {
            if (reference is ProjectReference) {
              project.AddReference(new ProjectReference(currentSolution.GetProject(((ProjectReference)reference).Project.Name)));
            } else {
              project.AddReference(reference);
            }
          }
          project.OutputDirectory = ((Project)solution.Projects[i]).RelativeOutputDirectory;
          project.AskForStartParameters = ((Project)solution.Projects[i]).AskForStartParameters;
          project.ExecuteInOwnWindow = ((Project)solution.Projects[i]).ExecuteInOwnWindow;
          project.setWorkingDirectory(((Project)solution.Projects[i]).workingDir, ((Project)solution.Projects[i]).workingDirectory);
          if (solution.StartProject == (Project)solution.Projects[i])
            currentSolution.StartProject = project;
        }
      }
      Changed(ProfItAction.SolutionPropertiesChanged, currentSolution);
    }

    /// <summary>
    /// Adds a file to a project
    /// </summary>
    /// <param name="project">the project</param>
    /// <param name="fileName">absolute path of the new file</param>
    public static void AddFile(Project project, string fileName) {
      project.AddFile(fileName);
      Changed(ProfItAction.NewFile, fileName);
    }

    /// <summary>
    /// Removes a file from the solution
    /// </summary>
    /// <param name="fileName">absolute path of the file to remove</param>
    public static void RemoveFile(string fileName) {
      try {
        Project project = currentSolution.GetProjectForFile(currentSolution.FindFile(fileName).FileName);
        SourceFile file = currentSolution.FindFile(fileName);
        project.RemoveFile(file);
        UpdateProfilingResult();
        Changed(ProfItAction.FileRemoved, file);
      } catch {}
    }

    /// <summary>
    /// Removes the specified project from the solution and also
    /// removes all references to that project
    /// </summary>
    /// <param name="project">the project to delete</param>
    public static void RemoveProject(Project project) {
      foreach (Project pr in currentSolution.Projects) {
        int i=0;
        while (i < pr.References.Count) {
          if (pr.References[i] is ProjectReference &&
            ((ProjectReference)pr.References[i]).Project == project) {
            pr.References.RemoveAt(i);
          } else i++;
        }
      }
      currentSolution.Projects.Remove(project);
      Changed(ProfItAction.ProjectAdded, currentSolution);
    }

    /// <summary>
    /// Renames a project. Raises an event with ProfItAction.ProjectAdded
    /// </summary>
    /// <param name="project">The project to rename</param>
    /// <param name="newName">the new name of the project</param>
    public static void RenameProject(Project project, string newName) {
      project.SetName(newName);
      Changed(ProfItAction.ProjectAdded, project);
    }

  }

  public enum ProfItAction { 
    NewFile, 
    FileRemoved,
    BlocksMerged, 
    CounterReset, 
    ProjectAdded,
    SolutionClosed, 
    SolutionOpened, 
    SolutionSaved,
    CountersImported,
    SolutionPropertiesChanged,
    RangedColorsChanged
  }

}
