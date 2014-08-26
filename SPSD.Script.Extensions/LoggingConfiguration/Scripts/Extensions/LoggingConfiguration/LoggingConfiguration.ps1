
# Ref: http://blog.andersdissing.com/2010/09/managing-custom-areas-and-categories.html

#EventSeverity		Description
#	None			Indicates no event entries are written. 
#	ErrorCritical	Indicates a problem state that needs the immediate attention of an administrator.
#	Error			Indicates a problem state requiring attention by a site administrator. 
#	Warning			Indicates conditions that are not immediately significant but that may eventually cause failure.
#	Information		Contains noncritical information provided for the administrator.
#	Verbose
 
#TraceSeverity		Description
#	None			Writes no trace information to the trace log file.
#	Unexpected		Represents an unexpected code path and actions that should be monitored.
#	Monitorable		Represents an unusual code path and actions that should be monitored. 
#	High			Writes high-level detail to the trace log file.
#	Medium			Writes medium-level detail to the trace log file.
#	Verbose			Writes low-level detail to the trace log file.

function Add-SPLoggingConfiguration($parameters, [System.Xml.XmlElement]$xml,[string]$extId, [string]$extensionPath){
    #Create a List<DiagnosticsArea> to collect the Diagnostics Area
    Log "Adding Logging Configuration" -type $SPSD.LogTypes.Normal -Indent
    try{
        # check if spg.codeplex.com assemblies are present and load them
        try{
            [System.Reflection.Assembly]::LoadWithPartialName("Microsoft.Practices.ServiceLocation") | Out-Null
            [System.Reflection.Assembly]::LoadWithPartialName("Microsoft.Practices.SharePoint.Common") | Out-Null
            New-Object 'System.Collections.Generic.List[Microsoft.Practices.SharePoint.Common.Logging.DiagnosticsArea]' | Out-Null
        }
        catch{
            # otherwise load from extension path
            Add-Type -Path ($extensionPath  + "\ExternalAssemblies\Microsoft.Practices.ServiceLocation.dll")
            Add-Type -Path ($extensionPath  + "\ExternalAssemblies\Microsoft.Practices.SharePoint.Common.dll")
        }

        $areaCollection = New-Object 'System.Collections.Generic.List[Microsoft.Practices.SharePoint.Common.Logging.DiagnosticsArea]'
        foreach($area in $xml.Areas.Area){
            #Add a DiagnosticsArea with the name Custom Area
            $customArea = New-Object 'Microsoft.Practices.SharePoint.Common.Logging.DiagnosticsArea' -ArgumentList $area.Name
            foreach($category in $area.Categories.Category){
                $customArea.DiagnosticsCategories.Add((New-Object 'Microsoft.Practices.SharePoint.Common.Logging.DiagnosticsCategory'($category.Name, ([Enum]::Parse([Microsoft.SharePoint.Administration.EventSeverity], $category.EventSeverity)), ([Enum]::Parse([Microsoft.SharePoint.Administration.TraceSeverity], $category.TraceSeverity)))))
            }
            $areaCollection.Add($customArea)

        }
        #Get a IConfigManager from the SharePointServiceLocator
        $configManager =[Microsoft.Practices.SharePoint.Common.ServiceLocation.SharePointServiceLocator]::GetCurrent().GetInstance([Microsoft.Practices.SharePoint.Common.Configuration.IConfigManager])

        #Create a new DiagnosticsAreaCollection with the IConfigManager as parameter
        $configuredAreas = New-Object 'Microsoft.Practices.SharePoint.Common.Logging.DiagnosticsAreaCollection' $configManager

        #loop through the newly collection of diagnosticsareas
        $areaCollection | % {
 
	        #get the instance from the current configuration
	        $existingArea = $configuredAreas[$_.Name]
 
	        #if the area the is null, we add add the area from the List<DiagnosticsArea> collection
	        #else we loop through the collection we got from current configuration
	        if($existingArea -eq $null)
	        {
                Log ("Adding Area `"{0}`"" -f $_.Name) -type $SPSD.LogTypes.Normal
		        $configuredAreas.Add($_)
	        }
	        else
	        {	
                Log ("Updating Area `"{0}`"" -f $_.Name) -type $SPSD.LogTypes.Normal -Indent
		        #loop through the collection we got from current configuration
		        $customArea.DiagnosticsCategories | % {
 
			        #get the category instance from the $existingArea (DiagnosticsArea)
			        $existingCategory = $existingArea.DiagnosticsCategories[$_.Name]
			        #if the category is null, we add to the current configuration
			        if($existingCategory -eq $null)
			        {
                        Log ("Adding Category `"{0}`"" -f $_.Name) -type $SPSD.LogTypes.Normal

				        $existingArea.DiagnosticsCategories.Add($_);
			        }
		        }
                LogOutdent
	        }
        }
        #save the current configuration
        $configuredAreas.SaveConfiguration()

        #Ref http://patrickboom.wordpress.com/2010/09/16/using-powershell-to-register-spguidance-diagnostic-areas/
        [Microsoft.Practices.SharePoint.Common.Logging.DiagnosticsAreaEventSource]::EnsureConfiguredAreasRegistered()
    }
    finally{
        LogOutdent
    }
}

function Remove-SPLoggingConfiguration($parameters, [System.Xml.XmlElement]$xml,[string]$extId, [string]$extensionPath){
    Log "Removing Logging Configuration" -type $SPSD.LogTypes.Normal -Indent
    try{
        # check if spg.codeplex.com assemblies are present and load them
        try{
            [System.Reflection.Assembly]::LoadWithPartialName("Microsoft.Practices.ServiceLocation") | Out-Null
            [System.Reflection.Assembly]::LoadWithPartialName("Microsoft.Practices.SharePoint.Common") | Out-Null
            New-Object 'System.Collections.Generic.List[Microsoft.Practices.SharePoint.Common.Logging.DiagnosticsArea]' | Out-Null
        }
        catch{
            # otherwise load from extension path
            Add-Type -Path ($extensionPath  + "\ExternalAssemblies\Microsoft.Practices.ServiceLocation.dll")
            Add-Type -Path ($extensionPath  + "\ExternalAssemblies\Microsoft.Practices.SharePoint.Common.dll")
        }
        #Get a IConfigManager from the SharePointServiceLocator
        $configManager =[Microsoft.Practices.SharePoint.Common.ServiceLocation.SharePointServiceLocator]::GetCurrent().GetInstance([Microsoft.Practices.SharePoint.Common.Configuration.IConfigManager])

        #Create a new DiagnosticsAreaCollection with the IConfigManager as parameter
        $configuredAreas = New-Object 'Microsoft.Practices.SharePoint.Common.Logging.DiagnosticsAreaCollection' $configManager

        foreach($area in $xml.Areas.Area){

	        $existingArea = $configuredAreas[$area.Name]

	        if($existingArea -ne $null)
	        {
                Log ("Removing Area `"{0}`"..." -f $area.Name) -type $SPSD.LogTypes.Normal -Indent -NoNewline
                $removed = $configuredAreas.Remove($existingArea)
                if($removed){
                    Log "Ok" -type $SPSD.LogTypes.Success -NoIndent -Outdent
                }
                else{
                    Log "Failed" -type $SPSD.LogTypes.Warning -Outdent
                }
            }
        }
        $configuredAreas.SaveConfiguration()

        #Ref http://patrickboom.wordpress.com/2010/09/16/using-powershell-to-register-spguidance-diagnostic-areas/
        [Microsoft.Practices.SharePoint.Common.Logging.DiagnosticsAreaEventSource]::EnsureConfiguredAreasRegistered()
    }
    finally{
        LogOutdent
    }
}
