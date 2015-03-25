###############################################################################
# SharePoint Solution Deployer (SPSD)
# SecureStore Extension
# Use this extension to create a new target application in the Secure Store with a configurable number of fields.
# Version          : 15.0.0.0
# Creator          : Bernd Rickenberg
###############################################################################

function Add-SecureStoreApplication($parameters, [System.Xml.XmlElement]$data, [string]$extId, [string]$extensionPath) {
	if ($parameters -eq $null) {
		Log -message "No parameters defined. Cannot create custom crawl connector." -type $SPSD.LogTypes.Error
		return 
	}

	if ($data -eq $null -or $data.SelectNodes("//SecureStoreApplicationField").Count -le 0) {
		Log -message "No fields specified. Cannot create secure store application." -type $SPSD.LogTypes.Error
		return;
	}

	$adminUser = $parameters["TargetApplicationAdmin"]
	$ownerUser = $parameters["TargetApplicationOwner"]
	$targetApplicationDisplayName = $parameters["TargetApplicationName"]
	if ($targetApplicationDisplayName -ne $null) {
		$targetApplicationName = $targetApplicationDisplayName.Replace(" ", "")
	}

	Log -message "Parameters:" -indent -type $SPSD.LogTypes.Normal
		Log -message ("TargetApplicationAdmin: "+ $adminUser) -type $SPSD.LogTypes.Normal
		Log -message ("TargetApplicationOwner: "+ $ownerUser) -type $SPSD.LogTypes.Normal
		Log -message ("TargetApplicationName: "+ $targetApplicationDisplayName) -type $SPSD.LogTypes.Normal
	LogOutdent

	if ($adminUser -eq $null) {
		Log -message "No admin user specified. Cannot create secure store application." -type $SPSD.LogTypes.Error
		return
	}

	if ($ownerUser -eq $null) {
		Log -message "No owner user specified. Cannot create secure store application." -type $SPSD.LogTypes.Error
		return
	}

	if ($targetApplicationName -eq $null) {
		Log -message "No name specified for the target application. Cannot create secure store application." -type $SPSD.LogTypes.Error
		return
	}

	$adminPrincipal = New-SPClaimsPrincipal -Identity $adminUser -IdentityType WindowsSamAccountName
	$memberPrincipal = New-SPClaimsPrincipal -Identity $ownerUser -IdentityType WindowsSamAccountName
	$serviceContext  = (Get-spwebapplication -includecentraladministration | where {$_.IsAdministrationWebApplication}).url.tostring()

	$app = Get-SPSecureStoreApplication –ServiceContext $serviceContext –Name $targetApplicationName -ErrorAction SilentlyContinue

	$fields =  @()
	if ($app -eq $null) {
		$targetApp = New-SPSecureStoreTargetApplication –Name $targetApplicationName –FriendlyName $targetApplicationDisplayName –ApplicationType Group
		foreach($fieldDescriptor in $data.SelectNodes("//SecureStoreApplicationField")) {
			if ($fieldDescriptor.Masked -ne $null) {
				$isMasked = [System.Convert]::ToBoolean($fieldDescriptor.Masked)
			}
			$field = New-SPSecureStoreApplicationField –Name $fieldDescriptor.Name -Type $fieldDescriptor.Type –Masked:$isMasked
			$fields += $field
		}

		New-SPSecureStoreApplication –ServiceContext $serviceContext –TargetApplication $targetApp –Fields $fields –Administrator $adminPrincipal -CredentialsOwnerGroup  $memberPrincipal | Out-Null

		Log -message "Secure Store target application created." -type $SPSD.LogTypes.Success
	}
	else {
		Log -message "Secure Store target application already exists." -type $SPSD.LogTypes.Warning
	}
}