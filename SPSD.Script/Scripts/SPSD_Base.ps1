###############################################################################
# SharePoint Solution Deployer (SPSD)
# Version          : 5.0.4.6440
# Url              : http://spsd.codeplex.com
# Creator          : Matthias Einig, RENCORE AB, http://twitter.com/mattein
# License          : MS-PL
###############################################################################
#region Base
	#region InitializeScript
	# Desc: Initializes constants and directories
	Function InitializeScript(){
	    $Script:SPSD = @{
	                        Version = [System.Version]"5.0.4.6440"
                            DisplayName ="SharePoint Solution Deployer (SPSD)"
                            StatusWidth = 79
	                        LogTypes = @{
                                Success     = 0
	                            Error       = 1
	                            Warning     = 2
	                            Information = 3
	                            Normal      = 4

	                        }
	                        Commands = @{
	                            Deploy      = 0
	                            Retract     = 1
	                            Redeploy    = 2
	                            Update      = 3
                                Ask         = 4
	                        }
	                        DeploymentTypes = @{
	                            All         = 0
	                            Solutions   = 1
	                            Extensions  = 2
	                        }
	                    }
	    $Script:LogIndentVal      = 0
	    $Script:baseDir = GetDirOrCreateIt -dir (Split-Path $scriptDir -Parent)
	    $Script:envDir  = GetDirOrCreateIt -dir ($baseDir + "\Environments")
	    $Script:logDir  = GetDirOrCreateIt -dir ($baseDir + "\Logs")
        $Script:RemoteSessions = @{}
	    #region Set parameters
	    [int]$Script:DeploymentCommand  = ParseParameter -value $Command    -values $SPSD.Commands        -default $SPSD.Commands.Deploy
	    [int]$Script:DeploymentType     = ParseParameter -value $Type       -values $SPSD.DeploymentTypes -default $SPSD.DeploymentTypes.All
	    # TODO [int]$Script:LogLevel           = ParseParameter -value $Verbosity  -values $SPSD.LogTypes        -default $SPSD.LogTypes.Normal
        [bool]$Script:isAppHost =  (Get-Host).PrivateData -eq $null

        if(-not $isAppHost){
            cls
        }
	    #endregion

	}
    #endregion
	#region LoadSettings
	# Desc: Loads the settings specified in the configuration file to variables
    #       has to be executed after the environment configuration is loaded
    Function LoadSettings(){
    	[int]$Script:DefaultTimeout = $conf.Settings.DeploymentTimeout
	    [int]$Script:DeploymentRetries = $conf.Settings.DeploymentRetries
	    $Script:WaitAfterDeployment = $conf.Settings.WaitAfterDeployment
        [bool]$Script:AllowGACDeployment = [System.Convert]::ToBoolean($conf.Restrictions.AllowGACDeployment)
        [bool]$Script:AllowCASPolicies = [System.Convert]::ToBoolean($conf.Restrictions.AllowCASPolicies)
        [bool]$Script:AllowFullTrustBinDeployment = [System.Convert]::ToBoolean($conf.Restrictions.AllowFullTrustBinDeployment)
        [bool]$Script:CreateULSLogfile = [System.Convert]::ToBoolean($conf.Settings.CreateULSLogfile)
        
        [Array]$Script:servers = $env:COMPUTERNAME
        # get multiple servers if configured, keeps downward compatibility to deprecated setting "IncludeAllServersInFarm"
        if([System.Convert]::ToBoolean($conf.Settings.IncludeAllServersInFarm) -or
           $conf.Settings.RunOnMultipleServersInFarm -ieq "All" ){
           $Script:servers = Get-SPServer | Where-Object {$_.Role -ine "Invalid"} | ForEach-Object {$_.Address}
        }
        elseif ($conf.Settings.RunOnMultipleServersInFarm -ieq "WebFrontEnd" -or
                $conf.Settings.RunOnMultipleServersInFarm -ieq "Application") {
                #include also SingleServer farms in any case
            $Script:servers = Get-SPServer | Where-Object {$_.Role -ieq $conf.Settings.RunOnMultipleServersInFarm -or $_.Role -ieq "SingleServer"} | ForEach-Object {$_.Address}
        }
    }
    #endregion
	#region Setting solution directory
	# Desc: Sets the directory where the solutions to be deployed are stored 
    Function SetSolutionDir(){
     	Log -message ("Getting solutions directory") -type $SPSD.LogTypes.Information -Indent

		If ($solutionDirectory -and $solutionDirectory.Length -gt 0 -and (Test-Path $solutionDirectory)){
            Log -message "Absolute custom directory specified" -type $SPSD.LogTypes.Normal
	        $Script:solDir  = $solutionDirectory
        }
        elseif($solutionDirectory -and $solutionDirectory.Length -gt 0 -and (Test-Path $solutionDirectory)){
            Log -message "Relative custom directory specified" -type $SPSD.LogTypes.Normal
            $Script:solDir  = (Get-Item $($baseDir + "\" +$solutionDirectory)).FullName
        }
        else {
       	    Log -message "Custom directory not configured or does not exist. Using default" -type $SPSD.LogTypes.Normal
        	$Script:solDir  = GetDirOrCreateIt -dir ($baseDir + "\Solutions")
        }
   	    Log -message ("Solutions directory: "+ (Get-Item $Script:solDir).FullName) -type $SPSD.LogTypes.Normal
        LogOutdent
    }
    #endregion
	#region StartUp
	# Desc: Writes the startup header, starts tracing and loads the required PS Addins
	Function StartUp(){
	    InitializeScript
        
        if(-not $isAppHost){
            $Host.UI.RawUI.WindowTitle = $SPSD.DisplayName + " - Version: " + $SPSD.Version
        }

	    StartTracing

        AskForDeploymentCommand

        Get-Content -Path "$scriptDir\AppLogo.txt"
    	Log    
		
		# Do not modify title, author, version, licenses or url! Thanks for keeping the credits, Matthias
        Log -message ("*"*$SPSD.StatusWidth) -type $SPSD.LogTypes.Information
	    Log -message (GetStatusLine -text ($SPSD.DisplayName + " by Matthias Einig (@mattein)") ) -type $SPSD.LogTypes.Information
	    Log -message (GetStatusLine -text ("Version          : "+$($SPSD.Version))) -type $SPSD.LogTypes.Information
	    Log -message (GetStatusLine -text ("License          : MS-PL")) -type $SPSD.LogTypes.Information
        Log -message (GetStatusLine -text ("Url              : http://spsd.codeplex.com")) -type $SPSD.LogTypes.Information
        Log -message (GetStatusLine -text "") -type $SPSD.LogTypes.Information
        Log -message (GetStatusLine -text ("Started on       : "+$(get-date))) -type $SPSD.LogTypes.Information
        Log -message (GetStatusLine -text ("Command          : "+$Command)) -type $SPSD.LogTypes.Information
        Log -message (GetStatusLine -text ("Type             : "+$Type)) -type $SPSD.LogTypes.Information
        Log -message (GetStatusLine -text ("Machine          : $env:COMPUTERNAME")) -type $SPSD.LogTypes.Information
        Log -message (GetStatusLine -text ("User             : $env:USERDOMAIN\$env:USERNAME")) -type $SPSD.LogTypes.Information
	    Log -message ("*"*$SPSD.StatusWidth) -type $SPSD.LogTypes.Information
	    Log -message ""

	    Log -message "Load Addins" -type $SPSD.LogTypes.Information -Indent
	    LoadSharePointPS
	    LoadWebAdminPS
	    LogOutdent
        Register-Extensions
        SetSolutionDir

	}
    #endregion
	#region GetStatusLine
	# Desc: Gets a status line in a fixed with *  (text)   *
    Function GetStatusLine($text){
        $width = ($SPSD.StatusWidth-3-$text.Length)
        if($width -lt 0){
            $width = 0
        }
        return "* $text"+(" "*$width)+"*"
    }
    #endregion
	#region ErrorSummary
	# Desc: Writes an error summary into a separate logfile
    Function ErrorSummary(){
        Log -message "One or multiple errors occurred while excecuting SPSD" -type $SPSD.LogTypes.Information -NoIndent 
        $Script:errNum = 0;
        $error | foreach { 
            Log -message ("Error "+$Script:errNum+": "+$_) -type $SPSD.LogTypes.Error -NoIndent 
             $Script:errNum +=1
            }

        if($logFile -ne $null){
            $errorLog = $logFile.Substring(0, $logFile.Length -4 ) + "-Errors.log"
            Log -message ("More details can be found in "+(GetRelFilePath -filePath $errorLog)) -type $SPSD.LogTypes.Information -NoIndent 
            $Error > $errorLog
        }
    }
    #endregion
	#region FinishUp
	# Desc: Writes the finalization text, elapsed time and log file location
	Function FinishUp(){
        CloseAllPSSessions
        Execute-Extensions $events.Finalize
		Finalize $vars
        Log
        if($Script:errNum -gt 0){
	        Log -NoIndent -message ("*"*$SPSD.StatusWidth) -type $SPSD.LogTypes.Error 
            Log -NoIndent -message (GetStatusLine -text "") -type $SPSD.LogTypes.Error
            Log -NoIndent -message (GetStatusLine -text "Operation failed!") -type $SPSD.LogTypes.Error 
            Log -NoIndent -message (GetStatusLine -text "") -type $SPSD.LogTypes.Error
	        Log -NoIndent -message ("*"*$SPSD.StatusWidth) -type $SPSD.LogTypes.Error 
        }
        else{
	        Log -NoIndent -message ("*"*$SPSD.StatusWidth) -type $SPSD.LogTypes.Success 
            Log -NoIndent -message (GetStatusLine -text "") -type $SPSD.LogTypes.Success
            Log -NoIndent -message (GetStatusLine -text "Operation completed!") -type $SPSD.LogTypes.Success 
            Log -NoIndent -message (GetStatusLine -text "") -type $SPSD.LogTypes.Success
	        Log -NoIndent -message ("*"*$SPSD.StatusWidth) -type $SPSD.LogTypes.Success 
        }
        Log
	    Log -NoIndent -message ("*"*$SPSD.StatusWidth) -type $SPSD.LogTypes.Information 
        Log -NoIndent -message (GetStatusLine -text ("Command          : "+$Command)) -type $SPSD.LogTypes.Information
        Log -NoIndent -message (GetStatusLine -text ("Type             : "+$Type)) -type $SPSD.LogTypes.Information
        Log -NoIndent -message (GetStatusLine -text ("Machine          : $env:COMPUTERNAME")) -type $SPSD.LogTypes.Information
        Log -NoIndent -message (GetStatusLine -text ("User             : $env:USERDOMAIN\$env:USERNAME")) -type $SPSD.LogTypes.Information
        Log -NoIndent -message (GetStatusLine -text "") -type $SPSD.LogTypes.Information
        Log -NoIndent -message (GetStatusLine -text ("Started on       : "+$(get-date))) -type $SPSD.LogTypes.Information
        Log -NoIndent -message (GetStatusLine -text ("Ended on         : "+$(get-date))) -type $SPSD.LogTypes.Information
        Log -NoIndent -message (GetStatusLine -text ("Elapsed Time     : "+$Script:ElapsedTime.Elapsed)) -type $SPSD.LogTypes.Information
        Log -NoIndent -message (GetStatusLine -text ("Log file         : "+(GetRelFilePath -filePath $LogFile))) -type $SPSD.LogTypes.Information
	    if($saveEnvXml){
            Log -NoIndent -message (GetStatusLine -text ("Environment file : "+(GetRelFilePath -filePath $resultXmlFile))) -type $SPSD.LogTypes.Information
	    }
	    Log -NoIndent -message ("*"*$SPSD.StatusWidth) -type $SPSD.LogTypes.Information
	    StopTracing
	}
    #endregion
#endregion
