###############################################################################
# SharePoint Solution Deployer (SPSD)
# Version          : 5.0.4.6440
# Url              : http://spsd.codeplex.com
# Creator          : Matthias Einig, RENCORE AB, http://twitter.com/mattein
# License          : MS-PL
###############################################################################

param 
(
    #optional parameter
    [ValidateNotNullOrEmpty()]
    [ValidateSet('Deploy', 'Redeploy', 'Retract', 'Update', 'Ask')]
    [string]$Command = "Deploy",

    [ValidateNotNullOrEmpty()]
    [ValidateSet('All', 'Solutions', 'Extensions')]
    [string]$Type = "All",          # Makes it possible to only deploy the solutions or the defined site structure

    [ValidateNotNullOrEmpty()]
    [ValidateSet('Error', 'Warning', 'Information', 'Verbose', 'VerboseExtended')]
    [string]$Verbosity = "verbose", # defines how detailed the log is created each level includes the ones above
    [string]$envFile,               # external environment configuration file
    [switch]$saveEnvXml = $true,    # filename of the used environment configuration (merged file of referenced files with replaced variables)
    [string]$solutionDirectory = "" # Optional: specify a custom folder location where the solutions files are stored

)

#region Include External Functions and Configuration
$0 = $myInvocation.MyCommand.Definition
$scriptDir = [System.IO.Path]::GetDirectoryName($0)
. $scriptDir"\SPSD_Base.ps1"
. $scriptDir"\SPSD_Utilities.ps1"
. $scriptDir"\SPSD_Deployment.ps1"
. $scriptDir"\SPSD_Extensions.ps1"
. $scriptDir"\CustomTargets.ps1"
#endregion

try{
    StartUp
	LoadEnvironment
	RunPrerequisites
	Run
}
catch{
	ErrorSummary
}
finally{
	FinishUp
	Pause
}
