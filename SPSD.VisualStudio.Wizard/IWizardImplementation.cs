using System;
using System.Collections.Generic;
using System.Windows.Forms;
using EnvDTE;
using Microsoft.VisualStudio.TemplateWizard;
using System.Xml;
using System.IO;
using Microsoft.VisualStudio.SharePoint;

namespace SPSD.VisualStudio.Wizard
{
  public static class GlobalData
  {
  }

  public class WizardImplementation : IWizard
  {
    private WizardForm inputForm;
    private string customMessage;

    private List<string> targetProjects;
    private List<string> additionalWSPs; 

    // This method is called before opening any item that 
    // has the OpenInEditor attribute.
    public void BeforeOpeningFile(ProjectItem projectItem)
    {
    }

    public void ProjectFinishedGenerating(Project project)
    {
      //add the copy task to all existing projects
      if (targetProjects != null)
      {
        foreach (Project targetProject in Helpers.GetAllProjects(project.DTE))
        {
          if (targetProjects.Contains(targetProject.Name))
          {
            MigrateProject(targetProject, project.Name);
          }
        }
       }

      if (additionalWSPs != null)
      {
        if (additionalWSPs.Count > 0)
        {
          ProjectItem deploymentFiles = Helpers.GetProjectItemByName(project.ProjectItems, "Batches");
          if (deploymentFiles == null)
          {
            deploymentFiles = project.ProjectItems.AddFolder("Batches");
          }

          ProjectItem solutionFiles = Helpers.GetProjectItemByName(deploymentFiles.ProjectItems, "Solutions");
          if (solutionFiles == null)
          {
            solutionFiles = deploymentFiles.ProjectItems.AddFolder("Solutions");
          }

          ProjectItem sandboxedSolutionFiles = Helpers.GetProjectItemByName(deploymentFiles.ProjectItems, "SandboxedSolutions");
          if (sandboxedSolutionFiles == null)
          {
            sandboxedSolutionFiles = deploymentFiles.ProjectItems.AddFolder("SandboxedSolutions");
          }

          if (solutionFiles != null)
          {
            foreach (string wspfile in additionalWSPs)
            {
              if (File.Exists(wspfile))
              {
                solutionFiles.ProjectItems.AddFromFile(wspfile);
              }
            }
          }
        }
      }
    }

    private void MigrateProject(Project project, string nameOfSolutionDeploymentProject)
    {
      Helpers.EnsureCheckout(project.DTE, project);

      string fileName = project.FullName;
      Helpers.SelectProject(project);
      DTE service = project.DTE;

      service.Documents.CloseAll(vsSaveChanges.vsSaveChangesPrompt);
      try
      {
        service.ExecuteCommand("File.SaveAll", string.Empty);
        service.ExecuteCommand("Project.UnloadProject", string.Empty);
        MigrateFile(fileName, nameOfSolutionDeploymentProject);
        service.ExecuteCommand("Project.ReloadProject", string.Empty);
      }
      catch { }

    }

    private void MigrateFile(string csprojfilepath, string nameOfSolutionDeploymentProject)
    {
      string contentToAdd = SPSD.VisualStudio.Wizard.Properties.Resources.ProjectContent;
      contentToAdd = contentToAdd.Replace("[NameOfSolutionDeploymentProject]", nameOfSolutionDeploymentProject);

      XmlDocument csprojfile = new XmlDocument();
      csprojfile.Load(csprojfilepath);

      XmlNamespaceManager newnsmgr = new XmlNamespaceManager(csprojfile.NameTable);
      newnsmgr.AddNamespace("ns", "http://schemas.microsoft.com/developer/msbuild/2003");

      XmlNode nodeProject = csprojfile.SelectSingleNode("/ns:Project", newnsmgr);

      XmlDocumentFragment docFrag = csprojfile.CreateDocumentFragment();
      docFrag.InnerXml = contentToAdd;
      nodeProject.AppendChild(docFrag);
      CheckNamespaces(csprojfile);
      csprojfile.Save(csprojfilepath);
    }

    private void CheckNamespaces(XmlNode node)
    {
      if (node.Attributes != null)
      {
        //hat die Node ein Attribute
        if (node.Attributes.Count > 0)
        {
          foreach (XmlAttribute attrib in node.Attributes)
          {
            if (attrib.Name == "xmlns")
            {
              if (attrib.Value == node.NamespaceURI)
              {
                //duplicate
                node.Attributes.Remove(attrib);
                break;
              }
            }
          }
        }
      }
      foreach (XmlNode subnode in node.ChildNodes)
      {
        CheckNamespaces(subnode);
      }
    }

    // This method is only called for item templates,
    // not for project templates.
    public void ProjectItemFinishedGenerating(ProjectItem
        projectItem)
    {
    }

    // This method is called after the project is created.
    public void RunFinished()
    {
    }

    public void RunStarted(object automationObject,
        Dictionary<string, string> replacementsDictionary,
        WizardRunKind runKind, object[] customParams)
    {

      // Display a form to the user. The form collects 
      // input for the custom message.
      inputForm = new WizardForm(automationObject);
      if (inputForm.ShowDialog() != DialogResult.OK)
      {
        throw new WizardCancelledException("Adding the project for Solution Deployment cancelled");
      }

      targetProjects = inputForm.TargetProjects;
      additionalWSPs = inputForm.AdditionalWSPs;

      try
      {
        //// Add custom parameters.
        //replacementsDictionary.Add("$SharePointVersion$", inputForm.SharePointVersion);

        //replacementsDictionary.Add("$ForceSolutionDeployment$", inputForm.ForceSolutionDeployment);
        ////replacementsDictionary.Add("$DeployToAllContentUrls$", inputForm.DeployToAllContentUrls);
        //replacementsDictionary.Add("$OverwriteExistingSolutions$", inputForm.OverwriteExistingSolutions);
        //replacementsDictionary.Add("$AllowGACDeployment$", inputForm.AllowGACDeployment);
        //replacementsDictionary.Add("$AllowCASPolicies$", inputForm.AllowCASPolicies);
        //replacementsDictionary.Add("$DisplayWizards$", inputForm.DisplayWizards);
        
        //replacementsDictionary.Add("$RestartSPTimer$", inputForm.RestartSPTimer);
        //replacementsDictionary.Add("$RestartSPAdmin$", inputForm.RestartSPAdmin);
        //replacementsDictionary.Add("$RestartSPUserCodeHost$", inputForm.RestartSPUserCodeHost);
        //replacementsDictionary.Add("$RestartIIS$", inputForm.RestartIIS);

        //replacementsDictionary.Add("$MachineName$", Environment.MachineName);

        string debuggingSite = "http://" + Environment.MachineName.ToLower();
        if(automationObject is DTE)
        {
          debuggingSite = GetDebuggingSite(automationObject as DTE);
        }
        
        replacementsDictionary.Add("$DebuggingWebApp$", debuggingSite);


        globalDictionary = new Dictionary<string, string>();
        globalDictionary.Add("$custommessage$", customMessage);
      }
      catch { }

    }

    private string GetDebuggingSite(DTE dte)
    {
      try
      {        
        ISharePointProjectService projectService = Helpers.GetSharePointProjectService(dte);

        if (projectService != null)
        {
          //set url for all projects
          foreach (ISharePointProject sharePointProject in projectService.Projects)
          {
            try
            {
              if (sharePointProject.SiteUrl != null)
              {                
                if(sharePointProject.SiteUrl.ToString() != "")
                {
                  return sharePointProject.SiteUrl.ToString();
                }                                  
              }
            }
            catch (Exception ex)
            {
                
            }
          }
        }       
      }
      catch { }      
      return "";
    }

    public static Dictionary<string, string> globalDictionary;

    // This method is only called for item templates,
    // not for project templates.
    public bool ShouldAddProjectItem(string filePath)
    {
      return true;
    }    
  }
}