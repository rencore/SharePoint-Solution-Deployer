using System;
using System.IO;
using System.Windows.Forms;
using System.Globalization;
using EnvDTE;
using System.Collections.Generic;
using System.Text;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.ComponentModel.Design;
using System.Xml;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.VisualStudio;
using System.Security.Permissions;
using System.Security;
using Microsoft.VisualStudio.Shell;
using EnvDTE80;
using Microsoft.VisualStudio.SharePoint;


namespace SPSD.VisualStudio.Wizard
{
  internal static class Helpers
  {
    const string _SPVSTemplateGuid = "{BB1F664B-9266-4FD6-B973-E1E44974B511}";
    internal static ISharePointProjectService GetSharePointProjectService(DTE dte)
    {
      EnvDTE80.DTE2 dte2 = (EnvDTE80.DTE2)dte;

      ServiceProvider serviceProvider = new ServiceProvider(dte2 as Microsoft.VisualStudio.OLE.Interop.IServiceProvider);
      
      Microsoft.VisualStudio.SharePoint.ISharePointProjectService projectService = serviceProvider.GetService(typeof(Microsoft.VisualStudio.SharePoint.ISharePointProjectService)) as Microsoft.VisualStudio.SharePoint.ISharePointProjectService;
      if (projectService == null)
      {
        projectService = Package.GetGlobalService(typeof(Microsoft.VisualStudio.SharePoint.ISharePointProjectService)) as Microsoft.VisualStudio.SharePoint.ISharePointProjectService;
      }
      
      return projectService;
    }

    public static ProjectItem GetProjectItemByName(ProjectItems pitems, string name)
    {
      foreach (ProjectItem pitem in pitems)
      {
        if (pitem.Name.ToUpper() == name.ToUpper())
        {
          return pitem;
        }
      }
      return null;
    }

    public static void SelectProject(Project project)
    {
      UIHierarchy tree = project.DTE.Windows.Item("{3AE79031-E1BC-11D0-8F78-00A0C9110057}").Object as UIHierarchy;
      SelectProject(tree, project);
      //tree.DoDefaultAction();
    }

    public static void SelectProject(UIHierarchy tree, Project CoreRecipes)
    {
      foreach (UIHierarchyItem subnode in tree.UIHierarchyItems)
      {
        SelectProject(subnode, CoreRecipes);
      }
    }

    internal static void EnsureCheckout(DTE dte, Project project)
    {
      try
      {
        string projectname = project.Properties.Item("FullPath").Value.ToString();
        Helpers.EnsureCheckout(dte, project.FullName);
      }
      catch (Exception ex)
      {
      }
    }

    internal static void EnsureCheckout(DTE dte, string itemname)
    {
      if (dte.SourceControl.IsItemUnderSCC(itemname))
      {
        if (!dte.SourceControl.IsItemCheckedOut(itemname))
        {
          LogMessage(dte, dte, "Checking out item " + itemname);
          dte.SourceControl.CheckOutItem(itemname);
          LogMessage(dte, dte, "Checking out item finished");
        }
      }
    }

    public static void LogMessage(DTE dte, object sender, string message)
    {
      string logmessage = message;
      WriteToOutputWindow(dte, logmessage);
    }

    public static void WriteToOutputWindow(DTE dte, string message)
    {
      WriteToOutputWindow(dte, message, false);
    }

    private static OutputWindowPane OWP = null;

    public static void WriteToOutputWindow(DTE dte, string message, bool clearBefore)
    {
      try
      {
        Window win = dte.Windows.Item("{34E76E81-EE4A-11D0-AE2E-00A0C90FFFC3}");
        OutputWindow comwin = (OutputWindow)win.Object;
        if (OWP == null)
        {
          foreach (OutputWindowPane w in comwin.OutputWindowPanes)
          {
            if (w.Name == "SharePoint Solution Deployer (SPSD)")
            {
              OWP = w;
            }
          }
        }
        if (OWP == null)
        {
          OWP = comwin.OutputWindowPanes.Add("SharePoint Solution Deployer (SPSD)");
        }
        if (clearBefore)
        {
          OWP.Clear();
        }
        OWP.Activate();


        Application.DoEvents();
        OWP.OutputString(message + Environment.NewLine);
        OWP.ForceItemsToTaskList();
      }
      catch (Exception)
      {
      }
    }

    public static void SelectProject(UIHierarchyItem node, Project CoreRecipes)
    {
      foreach (UIHierarchyItem subnode in node.UIHierarchyItems)
      {
        if (subnode.Object is Project)
        {
          Project p = subnode.Object as Project;
          if (p.Name == CoreRecipes.Name)
          {
            subnode.Select(vsUISelectionType.vsUISelectionTypeSelect);
          }
        }
        if (subnode.Object is ProjectItem)
        {
          ProjectItem p = subnode.Object as ProjectItem;
          if (p.SubProject != null)
          {
            if (p.SubProject.Name == CoreRecipes.Name)
            {
              subnode.Select(vsUISelectionType.vsUISelectionTypeSelect);
            }
          }
        }
        SelectProject(subnode, CoreRecipes);
      }
    }

    public static bool IsVSTemplate(DTE dte, Project selectedProject)
    {
      string projectTypeGuids = GetProjectTypeGuids(selectedProject);
      if (projectTypeGuids.ToUpper().Contains(_SPVSTemplateGuid))
      {
        return true;
      }
      return false;
    }

    internal static string GetProjectTypeGuids(EnvDTE.Project proj)
    {
      try
      {
        string projectTypeGuids = "";
        object service = null;
        Microsoft.VisualStudio.Shell.Interop.IVsSolution solution = null;
        Microsoft.VisualStudio.Shell.Interop.IVsHierarchy hierarchy = null;
        Microsoft.VisualStudio.Shell.Interop.IVsAggregatableProject aggregatableProject = null;
        int result = 0;

        service = GetService(proj.DTE, typeof(Microsoft.VisualStudio.Shell.Interop.IVsSolution));
        solution = (Microsoft.VisualStudio.Shell.Interop.IVsSolution)service;

        result = solution.GetProjectOfUniqueName(proj.UniqueName, out hierarchy);

        if (result == 0)
        {
          aggregatableProject = (Microsoft.VisualStudio.Shell.Interop.IVsAggregatableProject)hierarchy;
          result = aggregatableProject.GetAggregateProjectTypeGuids(out projectTypeGuids);
        }

        return projectTypeGuids;
      }
      catch { }
      return "";
    }

    internal static object GetService(object serviceProvider, System.Type type)
    {
      return GetService(serviceProvider, type.GUID);
    }

    internal static object GetService(object serviceProviderObject, System.Guid guid)
    {
      object service = null;
      Microsoft.VisualStudio.OLE.Interop.IServiceProvider serviceProvider = null;
      IntPtr serviceIntPtr;
      int hr = 0;
      Guid SIDGuid;
      Guid IIDGuid;

      SIDGuid = guid;
      IIDGuid = SIDGuid;
      serviceProvider = (Microsoft.VisualStudio.OLE.Interop.IServiceProvider)serviceProviderObject;
      hr = serviceProvider.QueryService(ref SIDGuid, ref IIDGuid, out serviceIntPtr);

      if (hr != 0)
      {
        System.Runtime.InteropServices.Marshal.ThrowExceptionForHR(hr);
      }
      else if (!serviceIntPtr.Equals(IntPtr.Zero))
      {
        service = System.Runtime.InteropServices.Marshal.GetObjectForIUnknown(serviceIntPtr);
        System.Runtime.InteropServices.Marshal.Release(serviceIntPtr);
      }

      return service;
    }

    public static List<Project> GetAllProjects(DTE vs)
    {
      List<Project> all = new List<Project>();

      foreach (Project project in vs.Solution.Projects)
      {
        if (project.Object is SolutionFolder)
        {
          SolutionFolder x = (SolutionFolder)project.Object;
          foreach (ProjectItem pitem in x.Parent.ProjectItems)
          {
            if (pitem.Object != null && 
                pitem.Object is Project &&
                IsVSTemplate(vs, pitem.Object as Project))
                {
                  all.Add(pitem.Object as Project);
                }
          }
        }
        else
        {
          if (IsVSTemplate(vs, project))
          {
            all.Add(project);
          }
        }
      }
      return all;
    }
  }
}
