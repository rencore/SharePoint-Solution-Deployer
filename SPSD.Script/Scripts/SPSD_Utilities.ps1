###############################################################################
# SharePoint Solution Deployer (SPSD)
# Version          : 5.0.4.6440
# Url              : http://spsd.codeplex.com
# Creator          : Matthias Einig, RENCORE AB, http://twitter.com/mattein
# License          : MS-PL
###############################################################################
#region Utilities
	#region Utilities.PowerShell
	    #region LoadSharePointPS
	    # Desc: Load SharePoint PowerShell snapin
	    Function LoadSharePointPS(){
	    	Log -message "Loading SharePoint Powershell Snapin" -type $SPSD.LogTypes.Normal
            If ((Get-PsSnapin |?{$_.Name -eq "Microsoft.SharePoint.PowerShell"})-eq $null)
		    { $PSSnapin = Add-PsSnapin Microsoft.SharePoint.PowerShell -ErrorAction SilentlyContinue -WarningAction SilentlyContinue | Out-Null }
	    }
	    #endregion
	    #region LoadWebAdminPS
	    # Desc: Load WebAdministration PowerShell snapin / module depending on OS version
	    # Ref : http://stackoverflow.com/questions/1924217/powershell-load-webadministration-in-ps1-script-on-both-iis-7-and-iis-7-5
	    Function LoadWebAdminPS(){
	        Log -message "Loading WebAdministration Powershell Snapin" -type $SPSD.LogTypes.Normal
	        if ([System.Version]$(Gwmi Win32_OperatingSystem).Version -ge [System.Version]"6.1")
	        { Import-Module WebAdministration -ErrorAction SilentlyContinue -WarningAction SilentlyContinue | Out-Null}
	        else
	        { Add-PSSnapin WebAdministration -ErrorAction SilentlyContinue -WarningAction SilentlyContinue | Out-Null}
	    }
        #endregion
        #region CustomEnter-PSSession
	    # Desc: Created or reuses a remote PowerShell session
        Function CustomEnter-PSSession([string]$server){
            if($isAppHost){
                return
            }
            if($server -ne $env:COMPUTERNAME){
                
                if($Script:RemoteSessions[$server] -eq $null){
                    $Script:RemoteSessions[$server] = New-PSSession -ComputerName $server
                    
                }
                Enter-PSSession $Script:RemoteSessions[$server]
            }
        }
        #endregion
        #region CustomExit-PSSession
	    # Desc: Exits a remote PowerShell session
        Function CustomExit-PSSession([string]$server){
            if($isAppHost){
                return
            }
            if($server -ne $env:COMPUTERNAME){
                Exit-PSSession
            }
        }
	    #endregion
        #region CloseAllPSSessions
	    # Desc: Closes any open remote PowerShell sessions
        Function CloseAllPSSessions(){
            if($isAppHost){
                return
            }
            Exit-PSSession
            foreach ($server in $servers)
            { 
                if($server -ne $env:COMPUTERNAME -and $Script:RemoteSessions[$server] -ne $null){
                    Remove-PSSession $Script:RemoteSessions[$server]
                }
            }
        }
	    #endregion
	#endregion
	#region Utilities.Process
	    #region RunProcess
	    # Desc: Load WebAdministration PowerShell snapin / module depending on OS version
	    #  Ref: http://stackoverflow.com/questions/8761888/powershell-capturing-standard-out-and-error-with-start-process
	    #  Ref: http://powershell.com/cs/forums/p/6488/10632.aspx
	    Function RunProcess([string]$filename, [string]$arguments){
	        $pinfo = New-Object System.Diagnostics.ProcessStartInfo
	        $pinfo.FileName = $filename
	        $pinfo.RedirectStandardError = $true
	        $pinfo.RedirectStandardOutput = $true
	        $pinfo.UseShellExecute = $false
	        $pinfo.CreateNoWindow = $true
	        $pinfo.Arguments = $arguments
	        $p = New-Object System.Diagnostics.Process
	        $p.StartInfo = $pinfo

	        $fStarted = $p.Start()
	        if (!$fStarted)
	        {
	          Log -message "Unable to run command '$filename $arguments'" -type $SPSD.LogTypes.Error
	        }
	        $p.WaitForExit()
	        $p.StandardOutput.ReadToEnd().Trim().Split("`n") | Foreach {
	            Log -message ($_) -type $SPSD.LogTypes.Normal
	        }

	    }
        #endregion
	#endregion
    #region Utilities.Logging
	    #region Log
	    # Desc: Logging function which sets color and indentation
	    Function Log([string]$message, [int]$type, [switch]$NoNewline, [switch]$Indent, [switch]$Outdent, [switch]$NoIndent){
	        # TODO
			# if($type -gt $Script:LogLevel){
	        #    return
	        #}
	        $foregroundColor = "Gray"
	        $backgroundColor = "Blue"
	        switch ($type){
	            $SPSD.LogTypes.Success          { $foregroundColor = "Green" }
	            $SPSD.LogTypes.Error            { $foregroundColor = "Red" }
	            $SPSD.LogTypes.Warning          { $foregroundColor = "Yellow" }
	            $SPSD.LogTypes.Information      { $foregroundColor = "White" }
	            $SPSD.LogTypes.Normal           { $foregroundColor = "Gray" }
	        }
	        if($Outdent){ LogOutdent }
	        if(!$NoIndent){
	            $indentChars = " " * (2 * $Script:LogIndentVal)
	        }

				$loggingHost = (Get-Host).Name
            if(($loggingHost -eq "ConsoleHost" -or $loggingHost -eq 'Windows PowerShell ISE Host') -and -not $isAppHost){
	            if($NoNewline)
	            {
	                Write-Host -foregroundColor $foregroundColor ($indentChars + $message) -NoNewline
	            }
	            else{
	                Write-Host -foregroundColor $foregroundColor ($indentChars + $message)
	            }
            }
            else
            {
	            if($NoNewline){
                    [System.IO.File]::AppendAllText($script:LogFile, ($indentChars + $message), [System.Text.Encoding]::Default)
	            }
                else{
                    Add-Content $script:LogFile ($indentChars + $message)
	            }
            }
	        if($Indent){ LogIndent }
	    }
	    #endregion
        #region LogOutdent
	    # Desc: Decrease the indentation level of the log
	    Function LogOutdent(){
	        $Script:LogIndentVal--
	        if($Script:LogIndentVal -lt 0){
	            $Script:LogIndentVal = 0
	        }
	    }
	    #endregion
        #region LogIndent
	    # Desc: Decrease the indentation level of the log
	    Function LogIndent(){
	        $Script:LogIndentVal++
	    }
	    #endregion
        #region StartTracing
	    # Desc: Start tracing the PowerShell Output to a file
	    Function StartTracing()    {
	        $script:LogTime = Get-Date -Format yyyyMMdd-HHmmss
	        $script:LogFile = "$logDir\$LogTime-$Command.log"
            if((Get-Host).Name -eq "ConsoleHost" -and -not $isAppHost){
	            Start-Transcript -Path $LogFile -Force
            }
	        $script:ElapsedTime = [System.Diagnostics.Stopwatch]::StartNew()
	    }
        #endregion
	    #region StopTracing
	    # Desc: Stop tracing the PowerShell Output to a file
	    function StopTracing(){
            if((Get-Host).Name -eq "ConsoleHost" -and -not $isAppHost){
		        Stop-Transcript
            }
	    }
        #endregion
	    #region ColorPattern
	    # Desc: Colorizes and outputs a certain regex match on the piped string
        #  Ref: http://stackoverflow.com/questions/7362097/color-words-in-powershell-script-format-table-output
        filter ColorPattern( [string]$ErrorPattern, [string]$SuccessPattern) {
          $lines = $_ -split "`n"
          for( $k = 1; $k -lt $lines.Count-3; ++$k ) {
              if($k -eq 1){
            	 Log $lines[$k] -Type $SPSD.LogTypes.Information
              }
              else{
                  Log -NoNewline #for correct indentation
                  $split1 = $lines[$k] -split $ErrorPattern
                  $error = [regex]::Matches( $lines[$k], $ErrorPattern, 'IgnoreCase' )
                  for( $i = 0; $i -lt $split1.Count; ++$i ) {
	                $split2 =  $split1[$i] -split $SuccessPattern
                    $success = [regex]::Matches( $split1[$i], $SuccessPattern, 'IgnoreCase' )
                    for( $j = 0; $j -lt $split2.Count; ++$j ) {
            	        Log $split2[$j] -Type $SPSD.LogTypes.Normal -NoNewline -NoIndent
            	        Log $success[$j] -Type $SPSD.LogTypes.Success -NoNewline -NoIndent
                    }
                    Log $error[$i] -Type $SPSD.LogTypes.Error -NoNewline -NoIndent
                    }
                   Log
               }
            }
        }
        #endregion
    #endregion
    #region Utilities.Variables
	    #region ParseParameter
	    # Desc: Parses a given parameter gagins a hashtable and returns the value
	    Function ParseParameter([string]$value = $(throw "You have to specify the desired value"), 
	                            [System.Collections.Hashtable]$values = $(throw "You have to specify the values HashTable"), 
	                            [int]$default = 0){
	       foreach($key in $values.Keys){
	            if([System.String]::Compare($key,  $value, $true) -eq 0){
	                return $values[$key]
	            }
	       }
	       return $default
	    }
        #endregion
	    #region ReplaceBooleans
	    # Desc: Replaces all booleans in the xml to be lowercase
	    Function ReplaceBooleans([string]$xml){
	        $xml = [regex]::Replace($xml, ">(?i)true<", ">true<")
	        $xml = [regex]::Replace($xml, ">(?i)false<", ">false<")
            $xml = [regex]::Replace($xml, "`"(?i)true`"", "`"true`"")
	        $xml = [regex]::Replace($xml, "`"(?i)false`"", "`"false`"")
	        return $xml
	    }
        #endregion
	    #region ReplaceSystemVars
	    # Desc: Replaces als system environment variables in the xml
	    Function ReplaceSystemVars([string]$xml){
	        Get-ChildItem env: | ForEach-Object { $xml = [regex]::Replace($xml, "(?i)\`$\(env:"+$_.Key +"\)", $_.Value) }
	        return $xml
	    }
        #endregion
	    #region ReplaceUserVars
	    # Desc: Replaces all configured custom user variables in the whole xml
	    Function ReplaceUserVars([System.Xml.XMLElement]$variables, [string]$xml){
            if($variables.ChildNodes){
				$variables.ChildNodes | ? {$_.GetType() -ne [System.Xml.XmlComment] } | ForEach-Object { $xml = [regex]::Replace($xml , "(?i)\`$\("+$_.getAttribute('Name')+"\)", $_.InnerXML) }
			}
	        return $xml
	    }
        #endregion
	    #region ReplaceVariables
	    # Desc: Replaces all variables in the xml
        Function ReplaceVariables([System.Xml.XMLElement]$variables, [string]$xml){
            $xml = ReplaceUserVars $variables $xml
            $xml = ReplaceSystemVars $xml
	        $xml = ReplaceBooleans $xml

            # reload XML and replaces user variables again if they have been nested
            [xml]$rawXML = $xml
            if($rawXML.SPSD.Environment.Variables){
                $xml = ReplaceUserVars $rawXML.SPSD.Environment.Variables $xml
            }

            return $xml
        }
        #endregion
	    #region BuildVarsCollection
	    # Desc: Builds the variables collection out of the configured variables which should be available in the custom deployment actions
	    Function BuildVarsCollection([System.Xml.XMLElement]$node){
	        $varArray = @{}
			if($node.ChildNodes){
	        	$node.ChildNodes | ? {$_.GetType() -ne [System.Xml.XmlComment] } | ForEach-Object { $varArray.Add($_.getAttribute('Name'),$_.InnerXML) }
			}
	        return $varArray
	    }
        #endregion
	    #region GetBoolAttribute
	    # Desc: Gets an XML attribute as boolean and optionally sets a given default value if the attribute does not exists
	    Function GetBoolAttribute([System.Xml.XMLElement]$node, [string]$attribute, [bool]$defaultIfNotExisting){
            if($node.GetType() -eq [System.Xml.XmlComment]){
               return $null
            }

            if($node -eq $null){
                return $false
            }
            if(!$node.HasAttribute($attribute)){
                return $defaultIfNotExisting
            }
	        $att = $node.GetAttribute($attribute)
	        return $att -and [System.Convert]::ToBoolean($att)  
	    }
        #endregion
    #endregion
    #region Utilities.XML
	    #region LoadXMLFile
	    # Desc: Loads an xml file and checkes if the major and minor version of the xml definition equals the current script version 
        Function LoadXMLFile([string]$filePath){
	        [xml]$xml = Get-Content $filePath
	        if(!$xml.SPSD)
	        {
	            Throw "Not a valid SPSD XML file: $filePath"
	        }
	        $fileVersion = $xml.SPSD.Version
	        if(!$fileVersion)
	        {
	            Throw "No version is specified in SPSD XML file: $filePath"
	        }
	        if((([System.Version]$fileVersion).Major -ne ([System.Version]$SPSD.Version).Major) -or (([System.Version]$fileVersion).Minor -ne ([System.Version]$SPSD.Version).Minor))
	        {
	            Throw "Major and minor version '$fileVersion' specified in SPSD XML file '$filePath' must be equal to the script version '$($SPSD.Version)'"
	        }
	        return $xml
	    }
        #endregion
	    #region LoadNodeFromFile
	    # Desc: Loads a given XML node form an external file and integrates it in the current document
	    Function LoadNodeFromFile([System.Xml.XMLElement]$node, [string]$srcFile, [string]$filterAttribute){
            if($node.GetType() -eq [System.Xml.XmlComment]){
               return $node
            }
	        $nodeName = $($node.Name)
	        $idText = ""
	        if($filterAttribute){
	            $idText = " with '$filterAttribute'='"+$node.GetAttribute($filterAttribute)+"'"
	            }
	        #get deployment configuration from external file
	        if($node.HasAttributes -and $node.FilePath){
	            if($node.HasChildNodes){
	                Log -message "'$nodeName' node$idText in '$srcFile' has both ChildNodes and a referenced external file '$($node.FilePath)'! The original child nodes will be discarded." -type $SPSD.LogTypes.Warning
	            }
	    
	            $extNodeFile = (Split-Path $srcFile -Parent) + "\" + $node.FilePath
	            $relFilePath = (GetRelFilePath -filePath $extNodeFile)
	            if(!(Test-Path $extNodeFile)){
	                Log -message "Referenced file '$relFilePath' not found." -type $SPSD.LogTypes.Error
	                return $null   
	            }
	        
	            [xml]$extFileXml = LoadXMLFile $extNodeFile
	            $xPath = "//$nodeName"
	            if($filterAttribute){
	                $xPath += "[@$filterAttribute='$($node.GetAttribute($filterAttribute))']"
	            }
	            
	            $resultNodes = Select-Xml -Xml $extFileXml -XPath $xPath
	        
	            if($resultnodes -ne $null -and $resultNodes.Count -gt 1){
	                Log -message "More than one '$nodeName' nodes$idText found in '$relFilePath'. Using the first one found." -type $SPSD.LogTypes.Warning
					$newNode = $resultNodes[0].Node
	            } 
	            elseif($resultNodes -eq $null){
	                Log -message "No '$nodeName' node$idText found in '$relFilePath'." -type $SPSD.LogTypes.Error
	                return $null
	            }
				else {
					$newNode = $resultNodes.Node
				}
	            if(!$newNode.HasChildNodes){
	                Log -message "External '$nodeName' node$idText in '$relFilePath' has no ChildNodes." -type $SPSD.LogTypes.Warning
	                return $null
	            }

	            # replace orignal node with imported node in owner document
	            $newOwnedNode = $node.OwnerDocument.ImportNode($newNode, $true)
	            $node.ParentNode.ReplaceChild($newOwnedNode, $node)

	            Log -message ("Loaded '$nodeName' node$idText from '$relFilePath'") -type $SPSD.LogTypes.Normal -level
	            return $newOwnedNode
	        }
	        # no external node loaded, keep the on in the $envFile
	        if(!$node.HasChildNodes){
	            Log -message "'$nodeName' node$idText in '$srcFile' has no ChildNodes and no referenced external file" -type $SPSD.LogTypes.Warning
	            return $null
	        }
	        return $node
	    }
        #endregion
	    #region GetStringOfXML
	    # Desc: Convert and XML object to a string
	    Function GetStringOfXML([xml]$inputXml){
	        $sb = new-object System.Text.StringBuilder
	        $settings = new-object System.Xml.XmlWriterSettings
	        $settings.Encoding = [System.Text.Encoding]::UTF8
	        $settings.CloseOutput = $true
	        $settings.Indent = $true
	        $writer = [System.Xml.XmlWriter]::Create($sb, $settings)
	        $inputXml.WriteContentTo($writer)
	        $writer.Close()
	        return $sb.ToString()
	    }
        #endregion
    #endregion
	#region Utilities.Environment
		#region GetEnvironmentFile
	    # Desc: Gets the environment file in the order
        #       1. File passed as parameter to the script
        #       2. xml file in the environments folder named after the current user
        #       3. xml file in the environments folder named after the current computer
        #       4. default environments file "default.xml"
		function RunPrerequisites(){
			Log -Message "Run Prerequisites" -Type $SPSD.LogTypes.Information -Indent
			Log -Message "Run Custom Prerequisites" -Type $SPSD.LogTypes.Normal -Indent
			RunCustomPrerequisites $vars
			Log -Message "Custom Prerequisites finished" -Type $SPSD.LogTypes.Normal -Outdent
			LogOutdent
		}	
        #endregion	
	    #region GetEnvironmentFile
	    # Desc: Gets the environment file in the order
        #       1. File passed as parameter to the script
        #       2. xml file in the environments folder named after the current user
        #       3. xml file in the environments folder named after the current computer
        #       4. default environments file "default.xml"
		Function GetEnvironmentFile(){
		    if($envFile -and (Test-Path $envFile)){
	            $relFilePath = (GetRelFilePath -filePath $envFile)
		        Log -message "Loading passed environment from '$relFilePath'" -type $SPSD.LogTypes.Normal
		        return $envFile
		    }
		    # no envfile passed to script
		    if(!(Test-Path $envDir)){
		        Log -message "Environment directory not found at path $envDir" -type $SPSD.LogTypes.Error
		        return $null   
		    }
		    $file = "$envDir\$env:USERNAME.xml"
	        $relFilePath = (GetRelFilePath -filePath $file)
		    if(Test-Path $file ){
		        Log -message "Loading user specific environment for '$env:USERNAME' from '$relFilePath'" -type $SPSD.LogTypes.Normal
		        return $file    
		    }
		    $file  = "$envDir\$env:COMPUTERNAME.xml"
	        $relFilePath = (GetRelFilePath -filePath $file)
		    if(Test-Path $file ){
		        Log -message "Loading machine specific environment for '$env:COMPUTERNAME' from '$relFilePath'" -type $SPSD.LogTypes.Normal
		        return $file    
		    }
		    $file  = "$envDir\Default.xml"
	        $relFilePath = (GetRelFilePath -filePath $file)
		    if(Test-Path $file ){
		        Log -message "Loading default environment from '$relFilePath'" -type $SPSD.LogTypes.Normal
		        return $file    
		    }
		    return $null
		}
        #endregion
	    #region LoadEnvironment
	    # Desc: Loads the enviroment definition file
		Function LoadEnvironment(){
		    Log -message "Loading deployment environment configuration" -type $SPSD.LogTypes.Information -Indent
		    
		    $envFile = GetEnvironmentFile
	        $relEnvFilePath = (GetRelFilePath -filePath $envFile)

			if(!$envFile){
		        Throw "Environment definition file not found at '$relEnvFilePath'"
		    }
			[xml]$rawXML = LoadXMLFile $envFile

			$allExternalNodes = (Select-Xml -xml $rawXML -XPath "//*[@FilePath]")
		    $loopLimit = 10 # make sure not to have an endless loop
		    # get all nodes from external files
            LogIndent
		    while($allExternalNodes -ne $null -and $loopLimit)
		    {
		        $allExternalNodes | ForEach-Object{
		            $newNode = LoadNodeFromFile -node $_.Node -srcFile $envFile -filterAttribute "ID"

		            if(!$newNode -and $_.GetType() -eq [System.Xml.XmlElement]){
		                $_.RemoveAttribute("FilePath")
		            }
		        }
		        $loopLimit--
		        $allExternalNodes = (Select-Xml -xml $rawXML -XPath "//*[@FilePath]")
		    }
            LogOutdent

		    if(!$rawXML.SPSD.Configuration -or $rawXML.SPSD.Configuration.ChildNodes.Count -eq 0){
		        Throw "No valid 'Configuration' node found in '$relEnvFilePath' or referenced files"
		    }

		    # replacing environment and user variables
		    $completeXml = GetStringOfXML -inputXml $rawXML
		    if($rawXML.SPSD.Environment.Variables){
		        $completeXml = ReplaceVariables -Variables $rawXML.SPSD.Environment.Variables -xml $completeXml
		    }
		    else {
		        Log -message "No 'Variables' node found in '$relEnvFilePath'" -type $SPSD.LogTypes.Normal
		    }
		    # save result XML file if name is specified
		    if($saveEnvXml){
		        $Script:resultXmlFile = $logDir + "\" + "$LogTime-$Command-"+([System.IO.Path]::GetFileNameWithoutExtension($envFile))+".xml"
		        Set-Content -Value $completeXml -Path $resultXmlFile -Encoding UTF8
	            $relResultFilePath = (GetRelFilePath -filePath $resultXmlFile)
		        Log -message "Saved complete environment XML to '$relResultFilePath'" -type $SPSD.LogTypes.Normal
		    }
		    LogOutdent
		    [xml]$Script:xmlinput =   $completeXml
		    [System.Xml.XMLElement]$Script:conf = $xmlinput.SPSD.Configuration
		    [System.Xml.XMLElement]$Script:env = $xmlinput.SPSD.Environment
		    [System.Xml.XMLElement]$Script:struct = $xmlinput.SiteStructures
		    [System.Xml.XMLElement]$Script:ext = $xmlinput.SPSD.Extensions
            if($env.Variables -and $env.Variables.GetType() -eq [System.Xml.XmlElement]){
		        $Script:vars = BuildVarsCollection -node $env.Variables
            }
            else{
                $Script:vars = @{}
            }
            LoadSettings
		}
        #endregion
	#endregion
	#region Utilities.IO
	    #region GetDirOrCreateIt
	    # Desc: Gets and existing directory or creates it if not
	    Function GetDirOrCreateIt($dir){
		    If (!(Test-Path $dir))
		    {
	    	    New-Item $dir -type directory | out-null
		    }
	        return $dir
	    }
        Function GetRelFilePath($filePath){
            return "."+$filePath -replace $("(?i)"+$baseDir -replace "\\", "\\"), ""
        }
        #endregion
	    #region Pause
	    # Desc: Wait for user to press a key - normally used after an error has occured
        #       If configured the function will only wait for a specific time  
	    #  Ref: http://www.microsoft.com/technet/scriptcenter/resources/pstips/jan08/pstip0118.mspx 
	    Function Pause{
            if($isAppHost){
                return
            }
            if($WaitAfterDeployment -eq $null -or $WaitAfterDeployment -ieq "pause"){
		        Write-Host "Press any key to exit..."
		        $null = $host.UI.RawUI.ReadKey("NoEcho,IncludeKeyDown")
            }
            else {
                while ($WaitAfterDeployment -gt 0){
     		        Write-Host "Waiting for"$($WaitAfterDeployment/1000)"seconds before closing this window.    "
                    $Host.UI.RawUI.CursorPosition = New-Object System.Management.Automation.Host.Coordinates 0,(($Host.UI.RawUI.CursorPosition).Y-1)
                    Start-Sleep -Milliseconds 1000
                    $WaitAfterDeployment = $WaitAfterDeployment - 1000;
                }
            }
	    }
        #endregion
        #region AskForDeploymentCommand
	    # Desc: Asks for user input to set the deployment command
 	    Function AskForDeploymentCommand{
            if($Script:DeploymentCommand -ne $SPSD.Commands.Ask){
                return
            }
            if($isAppHost){
                Throw "When SPSD is hosted in an application, the 'Ask' deployment type is not supported";
            }

            while ($Script:DeploymentCommand -eq $SPSD.Commands.Ask){
                 Log -message "Select deployment command:" -type $SPSD.LogTypes.Information
                 Log -message " (1) Deploy" -type $SPSD.LogTypes.Normal
                 Log -message " (2) Retract" -type $SPSD.LogTypes.Normal
                 Log -message " (3) Redeploy (first retract, then deploy)" -type $SPSD.LogTypes.Normal
                 Log -message " (4) Update" -type $SPSD.LogTypes.Normal
                 Log -message "SPSD:> " -type $SPSD.LogTypes.Information -NoNewline
		         $key = $host.UI.RawUI.ReadKey("NoEcho,IncludeKeyDown")
                 Log -message $key.Character
                 switch($key.Character){
                    '1' { 
                        $Script:DeploymentCommand = $SPSD.Commands.Deploy
                        Log -message "Command: Deploy" -type $SPSD.LogTypes.Normal
                        $Script:Command = "Deploy"
                     }
                    '2' { 
                        $Script:DeploymentCommand = $SPSD.Commands.Retract
                        Log -message "Command: Retract" -type $SPSD.LogTypes.Normal
                        $Script:Command = "Retract"
                     }
                    '3' { 
                        $Script:DeploymentCommand = $SPSD.Commands.Redeploy
                        Log -message "Command: Redeploy" -type $SPSD.LogTypes.Normal
                        $Script:Command = "Redeploy"
                     }
                    '4' { 
                        $Script:DeploymentCommand = $SPSD.Commands.Update 
                        Log -message "Command: Update" -type $SPSD.LogTypes.Normal
                        $Script:Command = "Update"
                    }
                    default {
                        Log -message "Please choose a valid command" $SPSD.LogTypes.Warning
                    }
                 }
            }
	    }
        #endregion
	#endregion
	#region Utilities.Services
	    #region EnsureServiceRunning
	    # Desc: Ensures that a windows services is running. Starts it if services is stopped
		Function EnsureServiceRunning([string]$serviceName, [string]$computer){
		   $service = Get-Service -Computer $computer -Name $serviceName -ErrorAction SilentlyContinue
		   if($service){
		        Log -message "Ensuring $($service.DisplayName) ($serviceName) is running..." -type $SPSD.LogTypes.Normal -NoNewline
		        if($service.Status -ne "Running"){
		               Start-Service -Name $serviceName
		        }
		        $timeout = $DefaultTimeout
                $serviceStatus = (Get-Service -Computer $computer -Name $serviceName).Status
		        While($serviceStatus -ne "Running" -and $timeout ){
		            Log -message "." -type $SPSD.LogTypes.Normal -NoNewline -NoIndent
		            $timeout-=500
		            Start-Sleep -Milliseconds 500
                    $serviceStatus = (Get-Service -Computer $computer -Name $serviceName).Status
		        }
		        if($serviceStatus -ne "Running"){
		            Log -message "$serviceStatus" -type $SPSD.LogTypes.Error -NoIndent
		            Throw "Service '$serviceName' did not restart within the timeout intervall of $($DefaultTimeout)ms"
		        }
		        else{
		            Log -message "$($service.Status)" -type $SPSD.LogTypes.Success -NoIndent
		        }
		    }
		    else
		    {
		        Log -message "Service $serviceName not found." -type $SPSD.LogTypes.Warning
		    }
		}
        #endregion
	    #region EnsureAppPoolRunning
	    # Desc: Ensures that an application pool is running
		Function EnsureAppPoolRunning([string]$appPoolName){
		    Log -message "Ensure '$appPoolName' " -type $SPSD.LogTypes.Normal -NoNewline
            try{
                # Check if apppool exists under display name
                Get-WebAppPoolState -Name $appPoolName  | Out-Null
                Log -message "is started..." -type $SPSD.LogTypes.Normal -NoNewline -NoIndent                      
            }                            
            catch{
                # Get service apppool based on Guid
                $appPoolName = Get-SPServiceApplicationPool | Where-Object {$_.Name -eq $appPoolName} | ForEach-Object {$_.ID -replace "-", ""}
                Log -message " ($appPoolName) is started..." -type $SPSD.LogTypes.Normal -NoNewline -NoIndent
            }	

            if((Get-WebAppPoolState -Name $appPoolName).Value -ine "Started"){
		         Start-WebAppPool -Name $appPoolName
		    }
		    $timeout = $DefSvcTimeout
		    While( ((Get-WebAppPoolState -Name $appPoolName).Value -ine "Started") -and $timeout ){
		        Log -message "." -type $SPSD.LogTypes.Normal -NoNewline -NoIndent
		        $timeout-=500
		        Start-Sleep -Milliseconds 500
		    }
		    $status = (Get-WebAppPoolState -Name $appPoolName).Value
		    if($status -ine "Started"){
		        Log -message "$status" -type $SPSD.LogTypes.Error -NoIndent
		    }
		    else{
		        Log -message "$status" -type $SPSD.LogTypes.Success -NoIndent
		    }
		}
        #endregion
	#endregion
    #region Utilities.SharePoint
	    #region Get-SPTermStore
	    # Desc: Retrieves the termstore by name from the central admin web application
        function Get-SPTermStore([string] $name) {
            $site = Get-SPSite (Get-SPCentralAdministrationUrl)
            try{
                $session = new-object Microsoft.SharePoint.Taxonomy.TaxonomySession($site)
                $termstore = $session.TermStores[$name]
            }
            finally{
                $site.Dispose()
            }
     
            return $termstore
        }
        #endregion
	    #region Get-SPCentralAdministrationUrl
	    # Desc: Retrieves the Url of the central administration
        function Get-SPCentralAdministrationUrl(){
            return Get-SPWebApplication -includecentraladministration | where {$_.IsAdministrationWebApplication} | Select-Object -ExpandProperty Url
        }
        #enregion
    #endregion

#endregion
