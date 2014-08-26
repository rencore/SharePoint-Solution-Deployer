function Add-SPBlockedFileExtensions($parameters, [System.Xml.XmlElement]$xml,[string]$extId, [string]$extensionPath){
	Log -message "Adding Blocked File Types" -type $SPSD.LogTypes.Information -indent
	$webApps = Get-SPWebApplication
    $blockedFileExtensions = $parameters["BlockedFileExtensions"].Split(';')
	foreach($webApp in $webApps)
	{	
        Log -message "Web Application: $($webApp.Url)" -type $SPSD.LogTypes.Normal -indent
        foreach($blockedFileExtension in $blockedFileExtensions)
        {
            Log -message "$blockedFileExtension..." -type $SPSD.LogTypes.Normal -NoNewline 
            if($webApp.BlockedFileExtensions.ContainExtension($blockedFileExtension) -eq $false)
            {
                $webapp.BlockedFileExtensions.Add($blockedFileExtension)
                $webApp.Update()
                Log -message "Added" -type $SPSD.LogTypes.Success -NoIndent
            }
            else
            {
                Log -message "Exists" -type $SPSD.LogTypes.Success -NoIndent
            }
        }
        LogOutDent
    }
	LogOutdent
}

function Remove-SPBlockedFileExtensions($parameters, [System.Xml.XmlElement]$xml,[string]$extId, [string]$extensionPath){

	Log -message "Removing Blocked File Types" -type $SPSD.LogTypes.Information -indent
	$webApps = Get-SPWebApplication
    $blockedFileExtensions = $parameters["BlockedFileExtensions"].Split(';')
	foreach($webApp in $webApps)
	{	
        Log -message "Web Application: $($webApp.Url)" -type $SPSD.LogTypes.Normal -indent
        foreach($blockedFileExtension in $blockedFileExtensions)
        {
            Log -message "$blockedFileExtension..." -type $SPSD.LogTypes.Normal -NoNewline 
            if($webApp.BlockedFileExtensions.ContainExtension($blockedFileExtension) -eq $true)
            {
                $removed = $webapp.BlockedFileExtensions.Remove($blockedFileExtension)
				if($removed)
				{
					Log -message "Removed" -type $SPSD.LogTypes.Success -NoIndent					
				}
				else
				{
					Log -message "Failed" -type $SPSD.LogTypes.Warning -NoIndent
				}
                $webApp.Update()

            }
            else
            {
                Log -message "Not found" -type $SPSD.LogTypes.Success -NoIndent
            }
            
        }
        LogOutDent
    }
	LogOutdent
}