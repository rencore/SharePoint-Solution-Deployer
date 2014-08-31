SPSD.Extensions.Client for SharePoint 2013
==========================================

Custom PowerShell command-lets using SharePoint Client Side Object Model (CSOM) for deployment. Currently provided command-lets:
- Add-SPFile: Deploys a file, files or folders to SharePoint. Can be used to deploy client-side code, master pages, display templates or everything that is deployed to a document library
- Add-SPWorkflowDefinition: Deploys a Workflow Manager (WFM, SharePoint 2013 only) workflow definition. Supports site, list and reusable workflows.
- Add-SPWorkflowSubscription: Deploys a Workflow Manager (WFM, SharePoint 2013 only) workflow subscription. Supports site, list and reusable workflows.

The PowerShell command-lets can be used with or without SPSD. All SPSD plumbing is written. Simply go the SPSD.Extensions.Client.xml, add the files you want to deploy and set the site and library variables in the default.xml.