###############################################################################
# SharePoint Solution Deployer (SPSD)
# SPSD.Extensions.Client
# Wrapper for client (CSOM) PowerShell deployment extensions
# Allows you currently to deploy files and workflows to SharePoint on-prem and Online
# Version          : 15.0.0.0
# Creator          : Bernd Rickenberg
###############################################################################

function Deploy($parameters, [System.Xml.XmlElement]$data, [string]$extId, [string]$extensionPath) {
    import-module $extensionPath\SPSD.Extensions.Client.dll

	# Add-SPFile
	$deployItems = $data.SelectNodes("//AddSPFileItem")
	foreach($item in $deployItems) {
		Log -message "Parameters:" -indent -type $SPSD.LogTypes.Normal
			Log -message ("SiteUrl: "+ $item.SiteUrl) -type $SPSD.LogTypes.Normal
			Log -message ("RelativeDocLibUrl: "+ $item.RelativeDocLibUrl) -type $SPSD.LogTypes.Normal
			Log -message ("SourcePath: "+ $item.SourcePath) -type $SPSD.LogTypes.Normal
			Log -message ("NoOverwrite: "+ $item.NoOverwrite) -type $SPSD.LogTypes.Normal
			Log -message ("AddUpdateOnly: "+ $item.AddUpdateOnly) -type $SPSD.LogTypes.Normal
		LogOutdent

		# implement your extension here
		$noOverwrite = [System.Convert]::ToBoolean($item.NoOverwrite)
		$addUpdateOnly = [System.Convert]::ToBoolean($item.AddUpdateOnly)
		$sourcePath = [System.IO.Path]::Combine($extensionPath, $item.SourcePath)

		Get-Item $sourcePath | Add-SPFile `
			-SiteUrl $item.SiteUrl `
			-RelativeDocLibUrl $item.RelativeDocLibUrl `
			-NoOverwrite $noOverwrite `
			-AddUpdateOnly $addUpdateOnly `
			-Username $item.Username `
			-Password $item.Password
	}

	# Add-SPWorkflowDefinition
	$deployItems = $data.SelectNodes("//AddSPWorkflowDefinitionItem")
	foreach($item in $deployItems) {
		Log -message "Parameters:" -indent -type $SPSD.LogTypes.Normal
			Log -message ("SiteUrl: "+ $item.SiteUrl) -type $SPSD.LogTypes.Normal
			Log -message ("SourcePath: "+ $item.SourcePath) -type $SPSD.LogTypes.Normal
			Log -message ("Id: "+ $item.Id) -type $SPSD.LogTypes.Normal
			Log -message ("DisplayName: "+ $item.DisplayName) -type $SPSD.LogTypes.Normal
		LogOutdent

		# implement your extension here
		$publish = [System.Convert]::ToBoolean($item.Publish)
		$requiresInitiationForm = [System.Convert]::ToBoolean($item.RequiresInitiationForm)
		$requiresAssociationForm = [System.Convert]::ToBoolean($item.RequiresAssociationForm)
		$sourcePath = [System.IO.Path]::Combine($extensionPath, $item.SourcePath)

		Get-Item $sourcePath | Add-SPWorkflowDefinition `
			-SiteUrl $item.SiteUrl `
			-Publish $publish `
			-DisplayName $item.DisplayName `
			-Id $item.Id `
			-RestrictToType $item.RestrictToType `
			-RestrictToListName $item.RestrictToListName `
			-RequiresInitiationForm $requiresInitiationForm `
			-RequiresAssociationForm $requiresAssociationForm `
			-Description $item.Description `
			-DraftVersion $item.DraftVersion `
			-FormField $item.FormField `
			-AssociationUrl $item.AssociationUrl `
			-RestrictToScope $item.RestrictToScope `
			-InitiationUrl $item.InitiationUrl `
			-Username $item.Username `
			-Password $item.Password
	}

	# Add-SPWorkflowSubscription
	$deployItems = $data.SelectNodes("//AddSPWorkflowSubscriptionItem")
	foreach($item in $deployItems) {
		Log -message "Parameters:" -indent -type $SPSD.LogTypes.Normal
			Log -message ("SiteUrl: "+ $item.SiteUrl) -type $SPSD.LogTypes.Normal
			Log -message ("Id: "+ $item.Id) -type $SPSD.LogTypes.Normal
			Log -message ("Name: "+ $item.Name) -type $SPSD.LogTypes.Normal
			Log -message ("DefinitionId: "+ $item.DefinitionId) -type $SPSD.LogTypes.Normal
		LogOutdent

		# implement your extension here
		$enabled = [System.Convert]::ToBoolean($item.Enabled)
		$createLists = [System.Convert]::ToBoolean($item.CreateLists)
		$manualStartBypassesActivationLimit = [System.Convert]::ToBoolean($item.ManualStartBypassesActivationLimit)
		if ($item.EventTypes -ne $null) {
			$eventTypes = $item.EventTypes.Split(",")
		}

		Add-SPWorkflowSubscription `
			-SiteUrl $item.SiteUrl `
			-DefinitionId $item.DefinitionId `
			-Enabled $enabled `
			-EventSourceName $item.EventSourceName `
			-EventTypes $eventTypes `
			-HistoryListName $item.HistoryListName `
			-Id $item.Id `
			-Name $item.Name `
			-StatusFieldName $item.StatusFieldName `
			-TaskListName $item.TaskListName `
			-Username $item.Username `
			-Password $item.Password
	}
}
