###############################################################################
# Create and remove SharePoint ContentType hub PowerShell cmdlet
# Url              : http://gallery.technet.microsoft.com/Create-and-remove-e2825aee
# Creator          : Matthias Einig, www.matthiaseinig.de
###############################################################################

# Description: Creates a ContentType hub site collection and configures the managed metadata service application
#
# hubURI:    URL of the ContentType hub site collection (mandatory)
# hubOwner:  Owner of the hub (default: current user) 
# MMSAName:  Name of the Managed Metadata service application (default: first found instance of MMS)
# Overwrite: Overwrites hub site collection if it already exists (default: false)
# Confrim:   Confirm when existing hub is removed (default: true)
#
# Examples: 
#
# Create-SPContentTypeHub -hubURI "http://<siteurl>" -MMSAName "Managed Metadata Service"
# Create-SPContentTypeHub -hubURI "http://<siteurl>" -hubOwner "domain\user"

function Create-SPContentTypeHubExtension($parameters, [System.Xml.XmlElement]$xml,[string]$extId, [string]$extensionPath){

    Log "Creating ContentType Hub" -type $SPSD.LogTypes.Normal -Indent
    $hubUri = $parameters["HubUrl"]
    $hubOwner = $parameters["Owner"]
    $MMSAName = $parameters["MMSName"]
    $Overwrite = [System.Convert]::ToBoolean($parameters["Overwrite"])
    $Confirm = [System.Convert]::ToBoolean($parameters["Confirm"])

    Create-SPContentTypeHub $hubUri $hubOwner $MMSAName $Overwrite $Confirm

    if($parameters["AfterDeployExtensionDelegate"] -ne $null -and $parameters["AfterDeployExtensionDelegate"] -ne ""){
        & $parameters["AfterDeployExtensionDelegate"]
    }

    LogOutdent
}

function Remove-SPContentTypeHubExtension($parameters, [System.Xml.XmlElement]$xml,[string]$extId, [string]$extensionPath){

    Log "Creating ContentType Hub" -type $SPSD.LogTypes.Normal -Indent
    $hubUri = $parameters["HubUrl"]
    $MMSAName = $parameters["MMSName"]
    $Confirm = $parameters["Confirm"] -as [bool]

    Remove-SPContentTypeHub $hubUri $MMSAName $Confirm

    LogOutdent

}

function Create-SPContentTypeHub {
	[CmdletBinding()]
	param(
		[parameter(Mandatory=$true, Position=0, ValueFromPipeline=$false, ValueFromPipelineByPropertyName=$false)]
		[string]$hubURI, 
		[parameter(Mandatory=$false, Position=1, ValueFromPipeline=$false, ValueFromPipelineByPropertyName=$false)]
		[string]$hubOwner = $env:USERDOMAIN+"\"+$env:USERNAME, # default is current user
		[parameter(Mandatory=$false, Position=2, ValueFromPipeline=$false, ValueFromPipelineByPropertyName=$false)]
		[string]$MMSAName,
		[parameter(Mandatory=$false, Position=3, ValueFromPipeline=$false, ValueFromPipelineByPropertyName=$false)]
		[switch]$Overwrite = $false,  # if set to true then an existing hub will be recreated
		[parameter(Mandatory=$false, Position=4, ValueFromPipeline=$false, ValueFromPipelineByPropertyName=$false)]
		[switch]$Confirm = $true # if set to false the script will not ask when removing an existing hub
	) 
	
	begin{

    }
	
	process {
        try{
            Start-SPAssignment -Global

            # create content type hub site collection
            Log "Checking if CT hubsite exists at '$hubURI'..." -NoNewline -type $SPSD.LogTypes.Normal
            $hubExists = (Get-SPSite $hubURI -ErrorAction SilentlyContinue) -ne $null
            if($hubExists -and $Overwrite){
                Log "True" -type $SPSD.LogTypes.Warning -NoIndent
                Log "Overwrite is set, existing hub will be removed" -type $SPSD.LogTypes.Normal
                Remove-SPSite -Identity $hubURI -Confirm:$Confirm
                $hubExists = (Get-SPSite $hubURI -ErrorAction SilentlyContinue) -ne $null
            }
            elseif($hubExists -and -not $Overwrite){
                Log "True" -type $SPSD.LogTypes.Success -NoIndent
	            Log "Overwrite not set, using existing CT hub" -type $SPSD.LogTypes.Normal
            }
	        else{
                Log "False" -type $SPSD.LogTypes.Success -NoIndent
	        }	

	        if(-not $hubExists){
               # create hub site
	           Log "Creating CT hub SiteCollection..." -NoNewline -type $SPSD.LogTypes.Normal
	           $site = New-SPSite -Url $hubURI -Template 'STS#0' -OwnerAlias $hubOwner -Name "Content Type hub"
               Log "Done" -type $SPSD.LogTypes.Success -NoIndent
   
	        }


            #activate the Content Type hub feature 
            Log "Checking if CT hub feature is enabled..." -NoNewline -type $SPSD.LogTypes.Normal
            $feature = Get-SPFeature -site $hubURI  –Identity "ContentTypeHub" -ErrorAction SilentlyContinue
            if($feature -eq $null) {     
                Log "False" -type $SPSD.LogTypes.Warning -NoIndent

               # enable feature
	           Log "Activating 'ContentTypehub' feature..." -NoNewline -type $SPSD.LogTypes.Normal
               Enable-SPFeature –Identity "ContentTypeHub" –url $hubURI -Force -ErrorAction SilentlyContinue
               Log "Done" -type $SPSD.LogTypes.Success -NoIndent
            } 
    	    else{
	           Log "True" -type $SPSD.LogTypes.Success -NoIndent
	        }	

            #configure the Managed Metadata Service Application to use ContentType hub
            if($null -eq $MMSAName -or $MMSAName -eq ""){
                $mmscoll = Get-SPServiceApplication | ? { $_.TypeName -eq "Managed Metadata Service"}
                if($mmscoll -ne $null -and $mmscoll.Count -gt 1){
                    $MMSAName = $mmscoll[0].DisplayName
                }
                elseif($mmscoll -ne $null){
                    $MMSAName = $mmscoll.DisplayName
                }
                else{
                    Throw $("Managed Metadata Service Application could not be found")
                }
            }
            Log "Configuring the Managed Metadata Service Application '$MMSAName'..." -NoNewline -type $SPSD.LogTypes.Normal
            $mms = Get-SPServiceApplication -Name $MMSAName -ErrorAction SilentlyContinue

            if($mms -ne $null){
                Set-SPMetadataServiceApplication -Identity $mms -hubURI $hubURI -Confirm:$false
                Log "Done" -type $SPSD.LogTypes.Success -NoIndent
             }
             else{
                Log "Failed" -type $SPSD.LogTypes.Error -NoIndent
                Throw $("Managed Metadata Service Application with name '"+$MMSAName+"' does not exist")
             }
        
            #configure the Managed Metadata Service Application proxy
            Log "Configuring the Managed Metadata Service Application proxy '$MMSAName'..." -NoNewline -type $SPSD.LogTypes.Normal
            $mmsp = Get-SPServiceApplicationProxy | ? {$_.DisplayName -eq $MMSAName}
            if($mmsp -ne $null){
                Set-SPMetadataServiceApplicationProxy -Identity $MMSAName -ContentTypeSyndicationEnabled -ContentTypePushdownEnabled -Confirm:$false
                Log "Done" -type $SPSD.LogTypes.Success -NoIndent
             }
             else{
                Log "Failed" -type $SPSD.LogTypes.Error -NoIndent
                Throw $("Managed Metadata Service Application proxy with name '"+$MMSAName+"' does not exist")
             }

        }
        finally{
            Stop-SPAssignment -Global
        }
    }
	
	end {}
}


# Description: Removes a ContentType hub site collection and configures the managed metadata service application
#
# hubURI:    URL of the ContentType hub site collection (mandatory)
# MMSAName:  Name of the Managed Metadata service application (default: first found instance of MMS)
# Confrim:   Confirm when existing hub is removed (default: true)
#
# Examples: 
#
# Remove-SPContentTypeHub -hubURI "http://<siteurl>" -MMSAName "Managed Metadata Service"
# Remove-SPContentTypeHub -hubURI "http://<siteurl>" -Comfirm:$false
function Remove-SPContentTypeHub {
	[CmdletBinding()]
	param(
		[parameter(Mandatory=$true, Position=0, ValueFromPipeline=$false, ValueFromPipelineByPropertyName=$false)]
		[string]$hubURI, 
		[parameter(Mandatory=$false, Position=1, ValueFromPipeline=$false, ValueFromPipelineByPropertyName=$false)]
		[string]$MMSAName,
		[parameter(Mandatory=$false, Position=2, ValueFromPipeline=$false, ValueFromPipelineByPropertyName=$false)]
		[switch]$Confirm = $true # if set to false the script will not ask when removing an existing hub
	) 
	
	begin{

    }
	
	process {
        try{
            Start-SPAssignment -Global

            # create content type hub site collection
            Log "Checking ContentType hub site at '$hubURI'..." -NoNewline -type $SPSD.LogTypes.Normal
            $hubExists = (Get-SPSite $hubURI -ErrorAction SilentlyContinue) -ne $null
            if($hubExists){
                Log "Exists" -type $SPSD.LogTypes.Success -NoIndent
                $hubFeatureActive = (Get-SPFeature -site $hubURI –Identity "ContentTypeHub" -ErrorAction SilentlyContinue) -ne $null
                if($hubFeatureActive){
                    Log "Removing Hub" -type $SPSD.LogTypes.Normal
                    Remove-SPSite -Identity $hubURI -Confirm:$Confirm
                }
                else{
                    Log "ContentType hub feature not enabled, confirm to remove site anyway" -type $SPSD.LogTypes.Normal
                    Remove-SPSite -Identity $hubURI -Confirm:$true
                }
            }
            else{
                Log "False" -type $SPSD.LogTypes.Error -NoIndent
                Throw $("The site '$hubURI' does not exist")
            }

            # check again in case the user cancelled the site removal
            $hubExists = (Get-SPSite $hubURI -ErrorAction SilentlyContinue) -ne $null
            if($hubExists){
                Throw $("ContentType hub still exists, operation cancelled")
            }
     
            #configure the Managed Metadata Service Application to remove ContentType hub
            if($null -eq $MMSAName -or $MMSAName -eq ""){
                $mmscoll = Get-SPServiceApplication | ? { $_.TypeName -eq "Managed Metadata Service"}
                if($mmscoll -ne $null -and $mmscoll.Count -gt 1){
                    $MMSAName = $mmscoll[0].DisplayName
                }
                elseif($mmscoll -ne $null){
                    $MMSAName = $mmscoll.DisplayName
                }
                else{
                    Throw $("Managed Metadata Service Application could not be found")
                }
            }
            Log "Configuring the Managed Metadata Service Application '$MMSAName'..." -NoNewline -type $SPSD.LogTypes.Normal
            $mms = Get-SPServiceApplication -Name $MMSAName -ErrorAction SilentlyContinue
            if($mms -ne $null){
                Set-SPMetadataServiceApplication -Identity $mms -hubURI "" -Confirm:$false
                Log "Done" -type $SPSD.LogTypes.Success -NoIndent
             }
             else{
                Log "Failed" -type $SPSD.LogTypes.Error -NoIndent
                Throw $("Managed Metadata Service Application with name '"+$MMSAName+"' does not exist")
             }
 
        
            #configure the Managed Metadata Service Application proxy
            Log "Configuring the Managed Metadata Service Application proxy '$MMSAName'..." -NoNewline -type $SPSD.LogTypes.Normal
            $mmsp = Get-SPServiceApplicationProxy | ? {$_.DisplayName -eq $MMSAName}
            if($mmsp -ne $null){
                Set-SPMetadataServiceApplicationProxy -Identity $MMSAName -ContentTypeSyndicationEnabled:$false -ContentTypePushdownEnabled:$false -Confirm:$false
                Log "Done" -type $SPSD.LogTypes.Success -NoIndent
             }
             else{
                Log "Failed" -type $SPSD.LogTypes.Error -NoIndent
                Throw $("Managed Metadata Service Application proxy with name '"+$MMSAName+"' does not exist")
             }
        }
        finally{
            Stop-SPAssignment -Global
        }
    }
	
	end {}
}