Register Custom Indexing Connector for SharePoint 2013
======================================================

Use this extension to register a custom indexing connector on servers with indexing role in a SharePoint 2013 farm.

The custom indexing connector consists of one .NET assembly deployed to GAC and a BDC model file (note - this is not a BCS .NET assembly connector).

See this sample on how to implement a custom indexing connector: https://code.msdn.microsoft.com/office/SharePoint-2013-MyFileConne-79d2ea26

The extension registers your custom indexing connector and does the following:

- Adds the protocol handler to the registry
- Registers the custom indexing connector with the search service application
- Creates and configures a search metadata category for the indexing connector

All these operations can be execute once or more.

To actually deploy the connector files you can use standard SPSD functionality:

- Add a RestartService section to the deployment actions for the OSearch15 service
- Copy the BDC model in the CustomTargets.ps1
- Install the connector assembly(ies) in GAC using CustomTargets.ps1 (you might want to use the Add-GacAssembly from the PowerShell GAC project - https://powershellgac.codeplex.com/)

TODO:

- Support for unregistering the indexing connector
- Make creating the metadata category optional