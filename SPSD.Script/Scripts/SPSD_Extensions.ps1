###############################################################################
# SharePoint Solution Deployer (SPSD)
# Version          : 5.0.4.6440
# Url              : http://spsd.codeplex.com
# Creator          : Matthias Einig, RENCORE AB, http://twitter.com/mattein
# License          : MS-PL
###############################################################################
#region Extenstions
#endregion
	    #region BuildParametersCollection
	    # Desc: Builds the parameters collection out of the configured parameters which should be available extension
	    Function BuildParametersCollection([System.Xml.XMLElement]$node){
	        $paramArray = @{}
			if($node.ChildNodes){
	        	$node.ChildNodes | ? {$_.GetType() -ne [System.Xml.XmlComment] } | ForEach-Object { $paramArray.Add($_.getAttribute('Name'),$_.InnerXML) }
			}
	        return $paramArray
	    }
        #endregion



#endregion
$Script:Extensions = @{}
$Script:Events =  @{
                    BeforeDeploy    = "BeforeDeploy"
                    AfterDeploy     = "AfterDeploy"
                    BeforeRetract   = "BeforeRetract"
                    AfterRetract    = "AfterRetract"
                    BeforeUpdate    = "BeforeUpdate"
                    AfterUpdate     = "AfterUpdate"
                    Initialize      = "Initialize"
                    Finalize        = "Finalize"
                    ProcessSolution = "ProcessSolution"
                    Preconditions   = "Preconditions"
                    }
function Register-Extensions(){
    Log -message "Registering Extensions" -type $SPSD.LogTypes.Information -Indent
    Get-ChildItem "$scriptDir\Extensions\*\Manifest.xml" | % {
        $manifest = ([xml](Get-Content $_)).SPSD.Extensions.Extension.Manifest
        $scriptFile = (Split-Path -Parent $_)+ "\" + $manifest.Script
        Register-Extension $manifest $scriptFile 
    }
    LogOutdent
}

function Register-Extension([System.Xml.XmlElement]$manifest, [string]$extensionPS){
    $type = $manifest.Type
    $version = $manifest.Version
    if ([System.Convert]::ToBoolean($manifest.Enabled)){
        Log -message ("Registering Extension `"{0}`", v{1}..." -f $type, $version) -type $SPSD.LogTypes.Normal -NoNewline
        $Script:Extensions.Add($type, $extensionPS)
        Log -message "Done" -type $SPSD.LogTypes.Success -NoIndent
    }
    else{
        Log -message ("Extension `"{0}`", Version `"{1}`" found but " -f $type, $version) -type $SPSD.LogTypes.Normal -NoNewLine
        Log -message "disabled" -type $SPSD.LogTypes.Warning -NoIndent
    }
}


function Execute-Extensions([string]$event){
    Log -message ("Executing Extensions for `"{0}`" event" -f $event)  -type $SPSD.LogTypes.Information -Indent
    foreach($extension in $Script:ext.Extension){
        $extension.Events.Event | ?{$event -eq $_.Name} | % {

            # Check if extension is registered
            if($Script:Extensions[$extension.Type] -ne $null){
                $extensionEvendElapsedTime = [System.Diagnostics.Stopwatch]::StartNew()

                try{
                    Log -message ("Loading Extension `"{0}`"..." -f $extension.Type) -type $SPSD.LogTypes.Normal -NoNewline
                    . ($Script:Extensions[$extension.Type])
                    Log -message "Ok" -type $SPSD.LogTypes.Success -NoIndent
                }
                catch{
                    Log -message "Failed" -type $SPSD.LogTypes.Warning -NoIndent
                    throw
                }

                if($extension.Parameters.HasChildNodes){
                    $parameters = BuildParametersCollection $extension.Parameters
                }
                else{
                    $parameters = ""
                }
            
                if(-not($extension.Data.HasChildNodes)){
                    $data = $null
                }
                else{
                    $data = $extension.Data
                }

                Log -message ("Executing  `"{1}:{0}`" with method `"{2}`"" -f $extension.ID, $extension.Type, $_.InnerText) -type $SPSD.LogTypes.Information -Indent
                # disposing all previous objects, and starting new assigment for all objects of the extension
                Stop-SPAssignment -Global
                Start-SPAssignment -Global
                try{
                    & ($_.InnerText) $parameters $data $extension.ID (Split-Path -Parent ($Script:Extensions[$extension.Type]))
                }
                catch{
                    Log -message "Executing extension failed" -type $SPSD.LogTypes.Error
                    throw
                }
                finally{
                    Stop-SPAssignment -Global
                }
                #retarting global assigment for the rest of SPSD
                Start-SPAssignment -Global
                Log -message ("("+$extensionEvendElapsedTime.Elapsed+ ")") -type $SPSD.LogTypes.Normal
                LogOutdent
            }
        }
    }
    LogOutdent
}
#ideas

# http://imaverick.wordpress.com/2007/12/06/namepaces-in-powershell/#comment-40
# https://blogs.vmware.com/vipowershell/2007/09/powershell-name.html

