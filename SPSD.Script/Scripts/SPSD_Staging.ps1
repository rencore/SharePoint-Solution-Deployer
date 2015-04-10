###############################################################################
# SharePoint Solution Deployer (SPSD)
# Version          : 5.0.4.6440
# Url              : http://spsd.codeplex.com
# Creator          : Matthias Einig
# License          : MS-PL
###############################################################################


# inspired by Chris O'Briens https://sharepointci.codeplex.com/
param 
(
    [ValidateNotNullOrEmpty()]
    [string]$TargetServer,

    [ValidateNotNullOrEmpty()]
    [string]$Username,

	[ValidateNotNullOrEmpty()]
    [string]$Domain,

	[ValidateNotNullOrEmpty()]
    [string]$Password,

    [ValidateNotNullOrEmpty()]
    [string]$DropLocation,

    [ValidateNotNullOrEmpty()]
    [ValidateSet('Deploy', 'Redeploy', 'Retract', 'Update')]
    [string]$Command,

    [ValidateNotNullOrEmpty()]
    [ValidateSet('All', 'Solutions', 'Extensions')]
    [string]$Type,          # Makes it possible to only deploy the solutions or the defined site structure

    [ValidateNotNullOrEmpty()]
    [ValidateSet('Error', 'Warning', 'Information', 'Verbose', 'VerboseExtended')]
    [string]$Verbosity, # defines how detailed the log is created each level includes the ones above
    [string]$envFile,               # external environment configuration file
    [switch]$saveEnvXml = $true,    # filename of the used environment configuration (merged file of referenced files with replaced variables)
    [string]$solutionDirectory		# Optional: specify a custom folder location where the solutions files are stored

)

# Create credentials for calling into SharePoint environment (works also cross-domain)
$securePassword = ConvertTo-SecureString $Password -AsPlainText -force
$credential = New-Object System.Management.Automation.PsCredential("$Domain\$Username", $securePassword)


try
{ 
	Write-Output  "Opening remote session on: $TargetServer"  
	$s = New-PSSession -ComputerName $TargetServer -Authentication CredSSP -Credential $credential	
	$TargetFolder = "C:\Deploy\" + (Split-Path $DropLocation -Leaf)

	Write-Output "Copying deployment package from $DropLocation to $TargetFolder on $TargetServer"
	Invoke-Command -Session $s { param([string]$dropLocation, [string]$targetFolder) Copy-Item -Path $("Microsoft.PowerShell.Core\FileSystem::"+$dropLocation) -Destination $targetFolder -Force -Recurse } -ArgumentList $DropLocation,$TargetFolder 
	

	Write-Output "Excecuting SPSD deployment script from $TargetFolder"
	Invoke-Command -Session $s { param([string]$package) cd $package } -ArgumentList ($TargetFolder+"\Package")
	$result = Invoke-Command -Session $s { param([string]$Command, [string]$Type, [string]$Verbosity) .\Scripts\SPSD_Main.ps1 @PSBoundParameters } -ArgumentList $Command, $Type, $Verbosity

	$remotelastexitcode = Invoke-Command -session $s -ScriptBlock {$LASTEXITCODE}
}
catch 
{
	Write-Error "Error occurred: " $_.Exception.ToString()
}
finally
{
	if($s -ne $null){
		Remove-PSSession $s
	}
}
exit $remotelastexitcode
