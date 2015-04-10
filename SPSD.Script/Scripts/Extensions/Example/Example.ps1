###############################################################################
# SharePoint Solution Deployer (SPSD)
# Version          : 5.0.4.6440
# Url              : http://spsd.codeplex.com
# Creator          : Matthias Einig
# License          : MS-PL
# File             : ExampleExtension.ps1
###############################################################################

# This is an example SPSD extension function.
# This function can be called  when registered for an SPSD event defined 
# in the extension configuration file in the SPSD/Environments folder
# 
# Each extension can have multiple functions and also included other PowerShell files
# In the extension you should use the SPSD logging functions as described in the "HowToExtendSPSD.txt" file.
#
# The functions require to have following parameters
# - $parameters     : a collection of all named parameters defined in the extension configuration file
# - $data            : the <Data></Data> node of the extension configuration file
# - $extId          : the extension Id as defined in the extension configuration file 
# - $extenstionPath : the path to this file, in case you require to load from other files in the extensions script folder 

#region Execute-ExampleExtension
	# Desc: An example implementation of an SPSD extension
    function Execute-ExampleExtension($parameters, [System.Xml.XmlElement]$data, [string]$extId, [string]$extensionPath){
        Log -message "Parameters:" -indent -type $SPSD.LogTypes.Normal
            Log -message ("Parameter1: "+ $parameters["ExtensionParameter1"]) -type $SPSD.LogTypes.Normal
            Log -message ("Parameter2: "+ $parameters["ExtensionParameter2"]) -type $SPSD.LogTypes.Normal
        LogOutdent


        Log -message "Data:" -indent -type $SPSD.LogTypes.Normal
        foreach($node in $data.ChildNodes){
            Log -message ($node.Name + ": "+ $node.innerText) -type $SPSD.LogTypes.Normal
        }
        LogOutdent

        Log -message ("Extension ID: "+ $extId) -type $SPSD.LogTypes.Normal
        Log -message ("Extension path: "+ $extensionPath) -type $SPSD.LogTypes.Normal

        # implement your extension here    
    }
#endregion