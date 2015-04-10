###############################################################################
# SharePoint Solution Deployer (SPSD)
# Version          : 5.0.4.6440
# Url              : http://spsd.codeplex.com
# Creator          : Matthias Einig, RENCORE AB, http://twitter.com/mattein
# License          : MS-PL
###############################################################################

#region Deployment
    #region Deployment.Actions
	    #region RecycleAppPools
	    # Desc: Recycles IIS application pools (either only SharePoint related or all)
        Function RecycleAppPools([bool]$all){
            # Inspired by http://blog.mijalko.com/2011/11/using-powershell-to-get-iis-worker.html
            foreach ($server in $servers)
            {           
                Log -message "Recycling IIS application pools on server $($server)" -type $SPSD.LogTypes.Information -Indent
                CustomEnter-PSSession $server
                
                try{
                    $appPools = $null
                    if($all){
                        $appPools = dir IIS:\AppPools | ForEach-Object { $_.Name }
                    }
                    else{
                        # get apppools from web applications
                        $appPools = Get-spwebapplication -includecentraladministration | ForEach-Object { Get-Item "IIS:\Sites\$($_.DisplayName)"} | ForEach-Object {$_.applicationPool}
                        # get apppools from service applications
                        $appPools += Get-SPServiceApplicationPool | ForEach-Object {$_.Name}
                    }
			        if($appPools){
	                    $appPools | ForEach-Object {
	                        Log -message "Recycling '$($_)'" -type $SPSD.LogTypes.Information -Indent -NoNewline
                            $apppool = $_
                            try{
                                # Check if apppool exists under display name
                                Get-WebAppPoolState -Name $apppool | Out-Null 
                                Log -message "" -NoIndent                        
                            }                            
                            catch{
                                # Get service apppool based on Guid
                                $apppool = Get-SPServiceApplicationPool | Where-Object {$_.Name -eq $apppool} | ForEach-Object {$_.ID -replace "-", ""}
                                Log -message " ($apppool)" -type $SPSD.LogTypes.Information -NoIndent
                            }	                        
	                        Log -message "Recycling..." -type $SPSD.LogTypes.Normal -NoNewline
                        
                            if( (Get-WebAppPoolState -Name $apppool).Value -eq "Started"){
	                            Restart-WebAppPool -Name $apppool
	                        }
	                        else {
	                            Start-WebAppPool -Name $apppool
	                        }

	                        Log -message "Done" -type $SPSD.LogTypes.Success -NoIndent
	                        EnsureAppPoolRunning -appPoolName $_
	                        LogOutdent
	                    }
			        }
			        else{
	                    Log -message "Not application pools found!" -type $SPSD.LogTypes.Warning
			        }
                }
                finally{
                    CustomExit-PSSession $server
                    LogOutdent
                }
            }
        }
        #endregion
	    #region PrintIISStatus
	    # Desc: Beautyfies the IIS status
        Function PrintIISStatus([Array]$statusLines){
          $statusLines | ForEach-Object {
          if($_.Trim()){
                    $statusLine = $_.Split(":")
                    Log -message ($statusLine[0].Trim()+ "...") -type $SPSD.LogTypes.Normal -NoNewline
                    if($statusLine[1].Contains("Running")){
                        Log -message ($statusLine[1].Trim()) -type $SPSD.LogTypes.Success -NoIndent
                    }
                    elseif($statusLine[1].Contains("Stopped")){
                        Log -message ($statusLine[1].Trim()) -type $SPSD.LogTypes.Error -NoIndent
                    }
                    else {
                       Log -message ($statusLine[1].Trim()) -type $SPSD.LogTypes.Warning -NoIndent
                    }
                }
            }
        }
        #endregion
	    #region ResetIIS
	    # Desc: Runs IIS reset on all servers in the farm 
        #  Ref: http://wellytonian.com/2012/04/iis-reset-your-whole-sharepoint-farm/
        Function ResetIIS([bool]$force){
            Log -message "Resetting Internet Information Servers (IIS) in farm" -type $SPSD.LogTypes.Information -Indent
            try{
                foreach ($server in $servers)
                {
                    Log -message "Resetting IIS on $server" -type $SPSD.LogTypes.Information -Indent
                    if($force){
                        RunProcess -filename "iisreset" -arguments "$server /restart \\"$server
                    }
                    else{
                        RunProcess -filename "iisreset" -arguments "$server /restart /noforce \\"$server
                    }
                    LogOutdent
                    $timeout = $DefaultTimeout
                    Log -message "Getting status for IIS on $server..." -type $SPSD.LogTypes.Information -Indent -NoNewline
                    $timeout = $DefaultTimeout
                    $iisstatus = (iisreset $server /status "\\"$server)
		            While(($iisstatus -match "Stopped" -or $iisstatus -match "Start pending") -and $timeout ){
		                Log -message "." -type $SPSD.LogTypes.Information -NoNewline -NoIndent
		                $timeout-=1000
		                Start-Sleep -Milliseconds 1000
                        $iisstatus = (iisreset $server /status "\\"$server)
		            }
		            Log -message "Done" -type $SPSD.LogTypes.Success -NoIndent
                    PrintIISStatus -statusLines $iisstatus
                    if($iisstatus -match "Stopped" -or $iisstatus -match "Start pending"){
                        Throw "IIS on '$server' did not restart within the timeout of $($DefaultTimeout)ms"
                    }

                    LogOutdent
                }
            }
            finally{
                LogOutdent
            }
        }
        #endregion
	    #region RestartService
	    # Desc: Restarts a Windows service on all servers in the farm
        #  Ref: http://stackoverflow.com/questions/4304821/get-startup-type-of-windows-service-using-powershell
        Function RestartService([string]$serviceName, [bool]$force){
            if($force){
                 Log -message "Force " -type $SPSD.LogTypes.Information -NoNewline
            }
            Log -message "Restarting $($service.DisplayName) ($serviceName) in farm" -type $SPSD.LogTypes.Information -Indent
	        try {	        
	            foreach ($server in $servers)
	            {
                
				    $service = Get-Service -ComputerName $server -Name $serviceName -ErrorAction SilentlyContinue
		            if($service){
	                    Log -message "Restarting on $server..." -type $SPSD.LogTypes.Normal -NoNewline
	                    $startupMode = (Get-WmiObject -ComputerName $server -Query "Select StartMode From Win32_Service Where Name='$serviceName'").StartMode
	                    if($startupMode -ieq "Disabled"){
	                        Log -message "Service is disabled" -type $SPSD.LogTypes.Warning -NoIndent
	                    }
					    else{
		                    if($force){ Restart-Service -InputObject $service -force }
		                    else{ Restart-Service -InputObject $service }
		                    Log -message "Done" -type $SPSD.LogTypes.Success -NoIndent
		                    EnsureServiceRunning -serviceName $serviceName -computer $server
					    }
	                }
		            else
		            {
		                Log -message "Service not found." -type $SPSD.LogTypes.Warning
		            }
			    }
		    } 
		    finally{
                LogOutdent
            }
	    }
        #endregion
	    #region WarmUpWebApps
	    # Desc: Warms up all web application urls in the farm (excluding Central Admin)
        Function WarmUpWebApps(){
            Log -message "Warming up web application urls" -type $SPSD.LogTypes.Information -Indent
            try{
                Get-SPAlternateUrl -Zone Default | foreach-object { WarmUpUrl -url $_.IncomingUrl}
            }
            finally{
                LogOutdent
            }
        }
        #endregion
	    #region WarmUpAllSites
	    # Desc: Warms up all SPSites in the farm (Warning! Use with caution when having a lot of sites)
        Function WarmUpAllSites(){
            Log -message "Warming up all sites in the farm" -type $SPSD.LogTypes.Information -Indent
            try{
                Get-SPSite -Limit ALL | foreach-object { WarmUpUrl -url $_.url }
            }
            finally{
                LogOutdent
            }
        }
        #endregion
	    #region WarmUpUrls
	    # Desc: Warms a list of Urls
        Function WarmUpUrls([Array]$urls){
            Log -message "Warming up custom urls" -type $SPSD.LogTypes.Information -Indent
            try{
                foreach($url in $urls){
                    WarmUpUrl -url $url
                }
            }
            finally{
                LogOutdent
            }
        }
        #endregion
	    #region WarmUpUrl
	    # Desc: Warms a a specific Url, uses a local web proxy to override load balancers
        #       Runs on all servers in the farm, requires to have the LoopBackCheck disabled on the server
        #  Ref: by http://www.jonthenerd.com/2011/04/19/easy-sharepoint-2010-warmup-script-using-powershell/
        #  Ref: http://sharepintblog.com/2011/10/23/warm-up-a-sharepoint-web-application-with-powershell/
        #  Ref://bramnuyts.be/2011/08/18/sharepoint-warmup-script-for-loadblanced-wfes/
        Function WarmUpUrl([string]$url){
            if(!$url){ return }
            Log -message "Hitting url: $url..." -type $SPSD.LogTypes.Normal -NoNewline
            # set up a local proxy 
            # the loopback has to be disabled to make that work
            $bypassonlocal = $false
            $proxyuri = "http://" + $server
            $proxy = New-Object system.Net.WebProxy($proxyuri, $bypassonlocal)
            $Request = [System.Net.HttpWebRequest]::Create($url); 
            $Request.Proxy = $proxy

            $Request.UseDefaultCredentials = $true 
            $Request.UserAgent     = "SPSD Warmup script"
            $Request.CachePolicy   = [System.Net.Cache.RequestCacheLevel]::BypassCache
            $Request.Timeout       = $DefaultTimeout

            try 
            {
                ## Make the request 
                $Response = $Request.GetResponse(); 
        
                ## Process the response 
                if($Response.StatusCode -eq 200) { 
                    [int]$Length = $Response.ContentLength 
                    $Stream = $Response.GetResponseStream()
                    [byte[]]$Chunk = new-object byte[] 1024 
                    [int]$TotalBytesRead = 0 
                    [int]$BytesRead = 0 
            
                    ## Loop through the stream reading a Chunk (1024 bytes) at a time 
                    do { 
                        $BytesRead = $Stream.Read($Chunk, 0, $Chunk.Length) 
                        $TotalBytesRead += $BytesRead 
                        if($Length -gt 0) 
                        { Write-Progress "GET $url" "Bytes read: $TotalBytesRead of $Length" -percentComplete (($TotalBytesRead/$Length)*100) } 
                        else { Write-Progress "GET $url" "Total bytes read: $TotalBytesRead bytes" } 
                    } 
                    while ($BytesRead -gt 0)
                    $Stream.Close() 
                }
                $Response.Close(); 
                Write-Progress "GET $url" "Bytes read: $TotalBytesRead of $Length" -Completed
                Log -message "Done" -type $SPSD.LogTypes.Success -NoIndent
            } 
            catch 
            { 
                Log -message "Failed" -type $SPSD.LogTypes.Error -NoIndent
                Log -message ($error[0].ToString().Substring(54)) -type $SPSD.LogTypes.Error
            } 
        }
        #endregion
	    #region RunAction
	    # Desc: Runs a post deployment action specified in the configuration node
        Function RunAction([System.Xml.XMLElement]$action){
            if(!$action){
                return
            }
            $actionType = $action.LocalName
            switch ($actionType.ToLower()){
                "restartservice" {
                    $name  = $action.Name
                    $force = [System.Convert]::ToBoolean($action.Force) 
                   RestartService -serviceName $name -force $force
                }
                "resetiis" {
                    $force = [System.Convert]::ToBoolean($action.Force) 
                    ResetIIS -force $force
                }
                "recycleapppools"{
                    $all = [System.Convert]::ToBoolean($action.All) 
                    RecycleAppPools -all $all
                }
                "warmupurls"{
                    foreach ($server in $servers){ 
                        Log -message "Warming up urls from server $server" -type $SPSD.LogTypes.Information -Indent
                        try{
                            CustomEnter-PSSession $server
                               
                            if((GetBoolAttribute -node $action -attribute "AllWebApps")){
                                WarmUpWebApps
                            }
                            if((GetBoolAttribute -node $action -attribute "AllSites")){
                                WarmUpAllSites
                            }
                            WarmUpUrls -urls ($action.WarmUp | ForEach-Object {$_.Url})
                        }
                        finally{
                            CustomExit-PSSession $server
                            LogOutdent
                        }

                    }
                }
                default{
                    Log -message "The action type '$actionType' is not recognized. Valid types are RestartService, ResetIIS, RecycleAppPools" -type $SPSD.LogTypes.Warning
                }
            }
        }
        #endregion
	    #region RunActions
	    # Desc: Runs all post deployment actions in the configuration node
        Function RunActions([int]$actionCmd, [switch]$skipWarmup, [switch]$onlyWarmup){
            $elapsedTime = [System.Diagnostics.Stopwatch]::StartNew()
            try{
                $actionsNodes = $null
                switch ($actionCmd) {
                    $SPSD.Commands.Deploy {
                            Log -message "Running 'Deployment' actions" -type $SPSD.LogTypes.Information -Indent 
                            $actionsNodes  = (Select-Xml -Xml $Script:conf -XPath "Actions[@AfterDeploy='true']") | ForEach-Object { $_.Node }
                        }
                    $SPSD.Commands.Retract {
                            Log -message "Running 'Retraction' actions" -type $SPSD.LogTypes.Information -Indent 
                            $actionsNodes  = (Select-Xml -Xml $Script:conf -XPath "Actions[@AfterRetract='true']") | ForEach-Object { $_.Node }
                        }
                    $SPSD.Commands.Update {
                             Log -message "Running 'Update' actions" -type $SPSD.LogTypes.Information -Indent
                            $actionsNodes  = (Select-Xml -Xml $Script:conf -XPath "Actions[@AfterUpdate='true']") | ForEach-Object { $_.Node }
                        }
                }

                if($actionsNodes){
                    $restartingServices = $false
                    $actions = (Select-Xml -Xml $actionsNodes -XPath "*") | ForEach-Object { $_.Node }
            
                    if($skipWarmup){
                        $actions = $actions | Where-Object {$_.LocalName -ine "warmupurls"}
                    } 
                    if($onlyWarmup){
                        $actions = $actions | Where-Object {$_.LocalName -ieq "warmupurls"}
                    } 
                    $actions | ForEach-Object { 
                            if(!$_){
                                Log -message "No actions specified for this type" -type $SPSD.LogTypes.Normal
                            }
                            $actionType  = $_.LocalName
                            # create a heading for all services to be restarted in a row
                            if($actionType -ieq "restartservice" -and !$restartingServices){
                                  Log -message "Restarting services" -type $SPSD.LogTypes.Information -Indent
                                  $restartingServices = $true
                            }
                            elseif($actionType -ine "restartservice" -and $restartingServices){
                                LogOutdent
                                $restartingServices = $false
                            }
                            RunAction -action $_ 
                        }
                }
                else {
                    Log -message "No actions specified for this type" -type $SPSD.LogTypes.Normal
                }
            }
            finally{
                Log -message ("("+$elapsedTime.Elapsed+ ")") -type $SPSD.LogTypes.Normal
                LogOutdent
            }
        }
        #endregion
    #endregion
	#region Deployment.Utilities
	    #region WaitForJobToFinish
	    # Desc: Waits for a deploymen/retraction job to be finished
        #       Works both for farm and sandboxed solutions
		#  Ref: http://gallery.technet.microsoft.com/office/Add-Install-and-Enable-fe8c945c
		Function WaitForJobToFinish([string]$solutionFileName, [string]$Site, [switch]$retract) { 
            if(!$site){
                # Farm Soluion
                $timeout = $DefaultTimeout * 5
                while(!(Get-SPFarm) -or (Get-SPFarm).Status -ne "Online"){
                    if($timeout -le 0)
                    {
                        Throw "Farm not online after "+ ($DefaultTimeout * 5 / 60000) + " minutes" 
                    }
                    Start-Sleep -Seconds 5
                    $timeout -= 5000
                }
 		        $timeout = $DefaultTimeout * 2
                if(!$retract){ # Deployment / Update
                    $solution = Get-SPSolution -Identity $solutionName -ErrorAction:SilentlyContinue
		            $jobName = "*solution-deployment*$solutionFileName*" 
		            $job = Get-SPTimerJob | ?{ $_.Name -like $jobName } 
		            if ($job -eq $null) 
		            { 
		                Throw "Timer job for '$solutionFileName' not found, maybe file name is too long and does not fit into the jobname. Please report to https://spsd.codeplex.com/workitem/16451 "
		            } 
		            $jobFullName = $job.Name 
		            Log -Message "Waiting to finish job '$jobFullName'..." -Type $SPSD.LogTypes.Normal -NoNewline
		            do
                    { 
                        if($timeout -le 0)
                        {
                	        Log -Message "Failed" -Type $SPSD.LogTypes.Error -NoIndent
                            Throw "Job '$jobFullName' not finished after "+ ($DefaultTimeout * 2 / 1000) + " seconds" 
                        }
                        Start-Sleep -Seconds 2
		                Log  . -NoNewLine -Type $SPSD.LogTypes.Normal -NoIndent
                        $timeout -= 2000
		            } while ((Get-SPTimerJob $jobFullName) -ne $null) 

                }
                else{ # Retraction
            	    Log -Message "Waiting to finish retraction..." -Type $SPSD.LogTypes.Normal -NoNewline
		            $solution = Get-SPSolution -Identity $solutionName -ErrorAction:SilentlyContinue
		            do{ 
                        if($timeout -le 0)
                        {
                	        Log -Message "Failed" -Type $SPSD.LogTypes.Error -NoIndent
                            Throw "Solution still not retracted after "+ ($DefaultTimeout * 2 / 1000) + " seconds" 
                        }
                        Start-Sleep -Seconds 2
		                Log  . -NoNewLine -Type $SPSD.LogTypes.Normal -NoIndent
                        $timeout -= 2000
		            } while ($solution.Deployed -or $solution.JobExists) 

                }
            }
            else{
                # Sandboxed Solution
 		        $timeout = $DefaultTimeout * 2
                $solution = Get-SPUserSolution -Identity $solutionFileName -Site $url
                if(!$retract){ # Deployment / Update
                    Log -Message "Waiting to finish deployment..." -Type $SPSD.LogTypes.Normal -NoNewline
		            do
                    { 

                        if($timeout -le 0)
                        {
                	        Log -Message "Failed" -Type $SPSD.LogTypes.Error -NoIndent
                            Throw "Sandboxed solution not activated after "+ ($DefaultTimeout * 2 / 1000) + " seconds" 
                        }
                        Start-Sleep -Seconds 2
		                Log  . -NoNewLine -Type $SPSD.LogTypes.Normal -NoIndent
                        $timeout -= 2000
		            } while ($solution.Status -ne [Microsoft.SharePoint.SPUserSolutionStatus]::Activated) 

                }
                else{ # Retraction
            	    Log -Message "Waiting to finish retraction..." -Type $SPSD.LogTypes.Normal -NoNewline
		            do{ 
                        if($timeout -le 0)
                        {
                	        Log -Message "Failed" -Type $SPSD.LogTypes.Error -NoIndent
                            Throw "Sandboxed solution still not retracted after "+ ($DefaultTimeout * 2 / 1000) + " seconds" 
                        }
                        Start-Sleep -Seconds 2
		                Log  . -NoNewLine -Type $SPSD.LogTypes.Normal -NoIndent
                        $timeout -= 2000
		            } while ($solution.Status -ne [Microsoft.SharePoint.SPUserSolutionStatus]::Deactivated) 

                }
            }
		    Log -Message "Finished" -Type $SPSD.LogTypes.Success -NoIndent
		} 
        #endregion
	    #region FarmSolutionDeployedSuccessful
	    # Desc: Checks if a farm solution has been deployed successfully
        Function FarmSolutionDeployedSuccessful([string]$solutionName ){
             Log -Message "Checking deployment..." -Type $SPSD.LogTypes.Normal -NoNewline

            $solution = Get-SPSolution -Identity $solutionName -ErrorAction:SilentlyContinue
            if($solution){
                if($solution.LastOperationResult -eq [Microsoft.SharePoint.Administration.SPSolutionOperationResult]::DeploymentSucceeded){
                    Log -Message "Ok" -Type $SPSD.LogTypes.Success -NoIndent
                    return $true
                }
                if($solution.Deployed){
                    Log -Message "Ok" -Type $SPSD.LogTypes.Success -NoIndent
                    return $true
                }
                Log -Message "Not deployed" -Type $SPSD.LogTypes.Error -NoIndent
                return $false

            }
            Log -Message "Solution not found" -Type $SPSD.LogTypes.Error -NoIndent

            return $false
	    }
        #endregion
	    #region FarmSolutionRetractedSuccessful
	    # Desc: Checks if a farm solution has been retracted successfully
        Function FarmSolutionRetractedSuccessful([string]$solutionName ){
             Log -Message "Checking retraction..." -Type $SPSD.LogTypes.Normal -NoNewline

            $solution = Get-SPSolution -Identity $solutionName -ErrorAction:SilentlyContinue
            if($solution){
                if($solution.Deployed){
                    Log -Message "Failed" -Type $SPSD.LogTypes.Error -NoIndent
                    Log -Message "Solution still deployed" -Type $SPSD.LogTypes.Error
                    return $false
                }
                Log -Message "Failed" -Type $SPSD.LogTypes.Error -NoIndent
                Log -Message "Solution not deployed, but still installed" -Type $SPSD.LogTypes.Error
                return $false

            }
            Log -Message "Ok" -Type $SPSD.LogTypes.Success -NoIndent
            return $true
	    }
        #endregion
	    #region SandboxedSolutionDeployedSuccessful
	    # Desc: Checks if a sandboxed solution has been deployed successfully
        Function SandboxedSolutionDeployedSuccessful([string]$solutionName, [string]$Site ){
             Log -Message "Checking deployment..." -Type $SPSD.LogTypes.Normal -NoNewline

            $solution = Get-SPUserSolution -Identity $solutionName -Site $Site -ErrorAction:SilentlyContinue
            if($solution){
                if($solution.Status -eq [Microsoft.SharePoint.SPUserSolutionStatus]::Activated){
                    Log -Message "Ok" -Type $SPSD.LogTypes.Success -NoIndent
                    return $true
                }
                Log -Message ($solution.Status) -Type $SPSD.LogTypes.Error -NoIndent
                return $false

            }
            Log -Message "Solution not found" -Type $SPSD.LogTypes.Error -NoIndent

            return $false
	    }
        #endregion
	    #region SandboxedSolutionRetractedSuccessful
	    # Desc: Checks if a sandboxed solution has been retracted successfully
        Function SandboxedSolutionRetractedSuccessful([string]$solutionName, [string]$Site){
             Log -Message "Checking retraction..." -Type $SPSD.LogTypes.Normal -NoNewline

            $solution = Get-SPUserSolution -Identity $solutionName -Site $Site -ErrorAction:SilentlyContinue
            if($solution){
                if($solution.Status -eq [Microsoft.SharePoint.SPUserSolutionStatus]::Activated){
                    Log -Message "Failed" -Type $SPSD.LogTypes.Error -NoIndent
                    Log -Message "Sandboxed solution still activated" -Type $SPSD.LogTypes.Error
                    return $false
                }
                Log -Message "Failed" -Type $SPSD.LogTypes.Error -NoIndent
                Log -Message "Sandboxed solution not deployed, but still installed" -Type $SPSD.LogTypes.Error
                return $false

            }
            Log -Message "Ok" -Type $SPSD.LogTypes.Success -NoIndent
            return $true
	    }
        #endregion
	#endregion
    #region Deployment.PreConditions
	    #region PreConditions
	    # Desc: Checks preconditions before the deployment
        #       this target should be called before every deployment
	    #       deployment will not continue if a precondition fails
	    Function PreConditions(){
	        Log -Message "Checking System Preconditions" -Type $SPSD.LogTypes.Information -Indent
            CheckPSRemotingEnabled
	        CheckUserIsAdministrator
	        CheckSPMinimalVersion
	        CheckSPMinimalLicense
            CheckSPLanguagePacks
            CheckServicesRunning
            CheckLoopbackCheckDisabled
	        CheckPrerequisiteSolutions
	        Log -Message "Start Checking Custom Preconditions" -Type $SPSD.LogTypes.Normal -Indent
            CheckCustomPreconditions $vars
            Execute-Extensions $events.Preconditions
   	        Log -Message "Custom Preconditions finished" -Type $SPSD.LogTypes.Normal -Outdent
	        Log -Message "Checking System Preconditions finished" -Type $SPSD.LogTypes.Information -Outdent
	    }
        #endregion
	    #region CheckServicesRunning
	    # Desc: Checks if all required services for deployment are running
        Function CheckServicesRunning(){
 		    Log -Message "Checking services for deployment" -Type $SPSD.LogTypes.Information -Indent
            # only testing current server as we need these only for the deloyment
     	    EnsureServiceRunning -serviceName "SPAdminV4" -computer $env:COMPUTERNAME
	        EnsureServiceRunning -serviceName "SPTimerV4" -computer $env:COMPUTERNAME
            LogOutdent
		}
        #endregion
	    #region CheckLoopbackCheckDisabled
	    # Desc: Checks if the loopback check is disabled on all servers on the farm
        #       Required for the warmup functions
        #       To disable loopback check on a server run:
        #       New-ItemProperty HKLM:\System\CurrentControlSet\Control\Lsa -Name "DisableLoopbackCheck" -Value "1" -PropertyType dword
        #       and reboot.
        Function CheckLoopbackCheckDisabled(){
            foreach ($server in $servers)
            {
		        Log -Message "Checking if LoopbackCheck is disabled on server '$server'..." -Type $SPSD.LogTypes.Normal -NoNewline
                # running on remote server
                CustomEnter-PSSession $server
                try{
                    $loopbackCheckDisabled = (Get-ItemProperty HKLM:\System\CurrentControlSet\Control\Lsa).DisableLoopbackCheck
		            if($loopbackCheckDisabled){
		                Log -Message "Disabled" -Type $SPSD.LogTypes.Success -NoIndent
		            }
		            else
		            {
		                Log -Message "Enabled" -Type $SPSD.LogTypes.Warning  -NoIndent
		                Log -Message "LoopbackCheck has to be disabled in order to run the warmup action." -Type $SPSD.LogTypes.Warning  
		                Log -Message "To disable LoopbackCheck run:" -Type $SPSD.LogTypes.Warning  
		                Log -Message "  New-ItemProperty HKLM:\System\CurrentControlSet\Control\Lsa -Name `"DisableLoopbackCheck`" -Value `"1`" -PropertyType dword" -Type $SPSD.LogTypes.Warning  
		                Log -Message "  And reboot the server" -Type $SPSD.LogTypes.Warning  
		                Log -Message "This is not recommended in Production environment!" -Type $SPSD.LogTypes.Warning  
		            }
                }
                catch{
		                Log -Message "Error" -Type $SPSD.LogTypes.Error  -NoIndent
		                Throw $_.toString() 
                }
                finally{
                    CustomExit-PSSession $server
                }
            }
		}
        #endregion
	    #region CheckPSRemotingEnabled
	    # Desc: Checks if powershell remoting is enabled on all servers
        #       This is required for some post-deployment actions if you want to run them on all servers in the farm
        #       Run
        #       Enable-PSRemoting -Confirm:$false 
        #       on each server or restrict deployment to the current machine by setting 
        #       <IncludeAllServersInFarm> to`"false`" in the <Settings> node
        Function CheckPSRemotingEnabled(){
            foreach ($server in $servers)
            {
		        Log -Message "Checking if PowerShell Remoting is enabled on server '$server'" -Type $SPSD.LogTypes.Information -Indent
                # running on remote server
                
                try{

                        if($server -ne $env:COMPUTERNAME){
                            EnsureServiceRunning -serviceName "winrm" -computer $server
                            Log -Message "Remoting into '$server'..." -Type $SPSD.LogTypes.Normal -NoNewline
                            Enter-PSSession -ComputerName $server -ErrorAction SilentlyContinue -ErrorVariable PSError
                            if(!$PSError){
		                        Log -Message "Ok" -Type $SPSD.LogTypes.Success  -NoIndent
                            }
                            else { Throw $PSError }
                        }
                        else{
		                    Log -Message "PowerShell Remoting not required on local machine..." -Type $SPSD.LogTypes.Normal -NoNewline
                            Log -Message "Skipped" -Type $SPSD.LogTypes.Warning -NoIndent
                        }
                }
                catch{
		                Log -Message "Failed" -Type $SPSD.LogTypes.Error  -NoIndent
		                Log -Message "PowerShell Remoting is disabled on the remote server or access is denied to current user!" -Type $SPSD.LogTypes.Error
		                Log -Message "Run 'Enable-PSRemoting -Confirm:`$false' on each server or restrict deployment to the" -Type $SPSD.LogTypes.Error
		                Log -Message "current machine by setting <RunOnMultipleServersInFarm> to`"OnlyLocal`" in the <Settings> node" -Type $SPSD.LogTypes.Error
		                Throw $_.toString() 
                }
                finally{
                    if($server -ne $env:COMPUTERNAME){
                        Exit-PSSession
                    }
                    LogOutdent
                }
            }
        }
        #endregion
	    #region CheckUserIsAdministrator
	    # Desc: Checks if user is farm admin and local admin on all servers in the farm
		Function CheckUserIsAdministrator(){
		    Log -Message "Checking permissions" -Type $SPSD.LogTypes.Information -Indent
		    Log -Message "Checking if user '$env:USERDOMAIN\$env:USERNAME' is farm administrator..." -Type $SPSD.LogTypes.Normal -NoNewline
		    $farm = Get-SPFarm
		    if($farm.CurrentUserIsAdministrator()){
		        Log -Message "Ok" -Type $SPSD.LogTypes.Success -NoIndent
		    }
		    else
		    {
		        Log -Message "Failed" -Type $SPSD.LogTypes.Error  -NoIndent
		        Throw "User '$env:USERDOMAIN\$env:USERNAME' has to be farm administrator to continue deployment!"
		    }
            foreach ($server in $servers)
            {
                
		        Log -Message "Checking if user '$env:USERDOMAIN\$env:USERNAME' is local administrator on '$server'..." -Type $SPSD.LogTypes.Normal -NoNewline
                try{
                    # running on remote server
                    CustomEnter-PSSession $server
                    $isLocalAdmin = ([Security.Principal.WindowsPrincipal] [Security.Principal.WindowsIdentity]::GetCurrent()).IsInRole([Security.Principal.WindowsBuiltInRole] "Administrator")
		            if($isLocalAdmin){
		                Log -Message "Ok" -Type $SPSD.LogTypes.Success -NoIndent
		            }
		            else
		            {
		                Log -Message "Failed" -Type $SPSD.LogTypes.Error  -NoIndent
		                Throw "User '$env:USERDOMAIN\$env:USERNAME' has to be local administrator on '$server'!"
		            }
                }
                catch{
		                Log -Message "Error" -Type $SPSD.LogTypes.Error  -NoIndent
		                Throw $_.toString() 
                }
                finally{
                    CustomExit-PSSession $server
                }
            }
            LogOutdent
		}
        #endregion
	    #region CheckSPMinimalVersion
	    # Desc: Checks if the configured minimal SharePoint version is installed on the server
		Function CheckSPMinimalVersion(){
		    Log -Message "Checking minimal required SharePoint version..." -Type $SPSD.LogTypes.Information -Indent -NoNewline
		    try
		    {
		        $minimalVersion = [System.Version]$conf.Restrictions.MinimalSharePointVersion
		        if($minimalVersion){
                    # load external SharePoint versions lookup data
                    [xml]$SPSDSharePoint = LoadXMLFile($scriptDir+"\SharePointVersions.xml")
		            $installedVersion = (Get-SPFarm).BuildVersion
                    # set script variable for deployment actions
					$SPSD.InstalledVersion = $installedVersion.Major

		            $SPVersionName = ($SPSDSharePoint.SPSD.SharePoint.Versions.Version | Where-Object {$_.Number -eq $installedVersion}).Name 
					if($SPVersionName -eq $null -or $SPVersionName -eq "")
					{
						$SPVersionName ="unknown, please update SharePointVersions.xml from http://www.matthiaseinig.de/files/SharePointVersions.xml"
					}

		            if($installedVersion -ge $minimalVersion)
		            {
		                Log -Message "Ok" -Type $SPSD.LogTypes.Success -NoIndent
		            }
		            else{
		                Log -Message "Failed" -Type $SPSD.LogTypes.Error -NoIndent
		                Throw "The installed SharePoint version '$installedVersion' does not meet the version requirement '$minimalVersion'"
		            }
		            
		            Log -Message "Minimal version   : $minimalVersion" -Type $SPSD.LogTypes.Normal
		            Log -Message "Installed version : $installedVersion ($SPVersionName)" -Type $SPSD.LogTypes.Normal
		        }
		        Else{
		            Log -Message "Skipped" -Type $SPSD.LogTypes.Warning  -NoIndent
		            Log -Message "Value not specified in configuration" -Type $SPSD.LogTypes.Warning
		        }
		   }
		    finally{
		        LogOutdent
		    }
		}
        #endregion
	    #region CheckSPLanguagePack
	    # Desc: Checks if the configured SharePoint language packs are installed on the server
		Function CheckSPLanguagePacks(){
            try
		    {
		        $requiredLanguages = $conf.Restrictions.RequiredSharePointLanguages
		        if($requiredLanguages -and $requiredLanguages.SharePointLanguage){
                    Log -Message "Checking required SharePoint language packs" -Type $SPSD.LogTypes.Information -Indent  

                    # get installed languages from central admin rootweb
                    $site = Get-SPSite (Get-SPWebApplication -IncludeCentralAdministration | where {$_.IsAdministrationWebApplication}).Url
                    $installedLanguages = $site.RootWeb.RegionalSettings.InstalledLanguages

                    $output = @()
                    $output+= [PSCustomObject]@{
                                DisplayName = "DisplayName"
                                LanguageTag = "LanguageTag" 
                                LCID = "LCID" 
                                Installed = "Installed"
                                }
                    $langMissing = $false
                    #find matches in the installed languages
                    foreach($lang in $requiredLanguages.SharePointLanguage){
                        $instLang = $installedLanguages | ? { $_.LCID -eq $lang.LCID -or $_.LanguageTag -ieq $lang.LanguageTag -or $_.DisplayName -ieq $lang.DisplayName }
                        if($instLang){
                            $output+= [PSCustomObject]@{
                                DisplayName = $instLang.DisplayName 
                                LanguageTag = $instLang.LanguageTag 
                                LCID = $instLang.LCID 
                                Installed = "True"
                                }

                        }
                        else{
                            $output+=
                             [PSCustomObject]@{
                                DisplayName =  $lang.DisplayName 
                                LanguageTag = $lang.LanguageTag 
                                LCID = $lang.LCID 
                                Installed = "False"
                                }

                            $langMissing = $true
                        }
                    }
                    $output | ft -AutoSize -HideTableHeaders | Out-String | ColorPattern -ErrorPattern "False" -SuccessPattern "True"
                    if($langMissing){
		                Throw "Not all required SharePoint language packs are installed on this farm"
		            }

                }
		        Else{
		            Log -Message "Checking required SharePoint language packs...." -Type $SPSD.LogTypes.Information -Indent -NoNewline
		            Log -Message "Skipped" -Type $SPSD.LogTypes.Warning  -NoIndent
		            Log -Message "Value not specified in configuration" -Type $SPSD.LogTypes.Warning
		        }
		    }
		    finally{
		        LogOutdent
		    }
		}

        #endregion
	    #region CheckSPMinimalLicense
	    # Desc: Checks if the configured minimal SharePoint license is installed on the server
		#  Ref: http://msdn.microsoft.com/en-us/library/ff721969.aspx
		Function CheckSPMinimalLicense(){
		    Log -Message "Checking minimal required SharePoint license..." -Type $SPSD.LogTypes.Information -Indent -NoNewline  
            try
		    {
		        $minimalLicense = $conf.Restrictions.MinimalSharePointLicense
		        if($minimalLicense){
                    # load external SharePoint releases lookup data
                    [xml]$SPSDSharePoint = LoadXMLFile($scriptDir+"\SharePointVersions.xml")
		            $installedProducts =  Get-SPFarm | select Products
		            $installedLicences = $SPSDSharePoint.SPSD.SharePoint.Licenses.License  | Where-Object { $installedProducts.Products -icontains $_.Guid}
		            if($installedLicences -and (($installedLicences | ForEach-Object {$_.Type}) | Where-Object { $_ -ieq $minimalLicense -or ($minimalLicense -ieq "Standard" -and $_ -ieq "Enterprise") }))
		            {
		                Log -Message "Ok" -Type $SPSD.LogTypes.Success -NoIndent
		            }
		            else{
		                Log -Message "Failed" -Type $SPSD.LogTypes.Error -NoIndent
						If ($installedLicences.Count -eq 0){
							Log -Message ("Installed Licenses : unknown, please update SharePointVersions.xml from http://www.matthiaseinig.de/files/SharePointVersions.xml") -Type $SPSD.LogTypes.Warning
						}
		                Throw "The installed SharePoint license does not meet the license requirement '$minimalLicense'"
		            }
		            Log -Message "Minimal License    : $minimalLicense" -Type $SPSD.LogTypes.Normal
                    $firstItem = $true
                    foreach($license in $installedLicences){
                        if(!$firstItem){
		                    Log -Message ((" "*21)+$license.Name) -Type $SPSD.LogTypes.Normal
                        }
                        else{
		                    Log -Message ("Installed Licenses : " + $license.Name) -Type $SPSD.LogTypes.Normal
                            $firstItem =  $false
                        }
                    }
		        }
		        Else{
		            Log -Message "Skipped" -Type $SPSD.LogTypes.Warning  -NoIndent
		            Log -Message "Value not specified in configuration" -Type $SPSD.LogTypes.Warning
		        }
		    }
		    finally{
		        LogOutdent
		    }
		}
        #endregion

	    #region CheckPrerequisiteSolutions
	    # Desc: Checks if the configured prerequisite solutions are deployed (will be skipped on retraction)
		Function CheckPrerequisiteSolutions(){
		    switch($DeploymentCommand){
		       $SPSD.Commands.Deploy   {  }
		       $SPSD.Commands.Retract  { return  }
		       $SPSD.Commands.Redeploy {  }
		       $SPSD.Commands.Update  {  }
		    }
		    Log -Message "Checking prerequisite solutions" -Type $SPSD.LogTypes.Information -Indent
		    try{
		        if(!$env.PreRequisiteSolutions -or $env.PreRequisiteSolutions.Solution.Count -eq 0){
		            Log -Message "Not configured" -Type $SPSD.LogTypes.Normal
		            return
		        }
                $preReqOk = $true
                $solutions = $env.PreRequisiteSolutions.Solution
		        if($solutions)
                {
                    $solutions | ForEach-Object {
		                $solutionName = $_.Name
                        Log -Message "Solution '$solutionName'" -Type $SPSD.LogTypes.Information -Indent
                        try{
                             # Check on SiteCollections (sandboxed)
                            $siteColls = (Select-Xml -xml $_ -XPath "SiteCollection") | ForEach-Object {$_.Node}
                            if($siteColls){
                		        Log -Message "Checking sandboxed deployment" -Type $SPSD.LogTypes.Normal -Indent

                    	        if((Get-Service "SPUserCodeV4").Status -ne "Running"){
		                            Log -Message "Service 'SPUserCodeV4' not running. Cannot check sandboxed solutions." -Type $SPSD.LogTypes.Warning 
                                    $preReqOk = $false
		                        }
                                else{
                                    $siteColls | ForEach-Object {
                                        $url = $_.Url 
                                        $preReqOk = (IsSolutionDeployed -solutionName $solutionName -url $url -sandboxed) -and $preReqOk
                                        }
                                }
                                LogOutdent
                            }

                            # check on WebApplications (farm)
                            $webApps = (Select-Xml -xml $_ -XPath "WebApplication") | ForEach-Object {$_.Node}
                            if($webApps){
                                Log -Message "Checking WebApplication deployment" -Type $SPSD.LogTypes.Normal -Indent
                                $webApps | ForEach-Object {
                                        $url = $_.Url 
                                        $preReqOk = (IsSolutionDeployed -solutionName $solutionName -url $url) -and $preReqOk
                                    }
                                LogOutdent
                            }

                            #Check Global (farm)
                            if($_.ChildNodes.Count -eq 0){
                                Log -Message "Checking GAC deployment..." -Type $SPSD.LogTypes.Normal -NoNewline
                                $preReqOk = (IsSolutionDeployed -solutionName $solutionName) -and $preReqOk
                            }
                        }
                        finally{
                            LogOutdent
                        }
                    }
		        }
                if(!$preReqOk){
                   Throw "Prerequisite check failed! One or more prereqisite solutions are not deployed."
                }
		    }
		    finally{
		        LogOutdent
		    }
		}
        #endregion
	    #region IsSolutionDeployed
	    # Desc: Checks if a solution is deployed, 
        #       Validates if SPUserCodeV4 is running if it is a sandboxed solution
        #       Validates if Webapplication solutions are deployed to the specific url
		Function IsSolutionDeployed([string]$solutionName, [string]$url, [switch]$sandboxed){
            Start-SPAssignment -Global
            try{
                if(!$solutionName){
                    Log -Message "Solution name not specified in configuration." -Type $SPSD.LogTypes.Warning 
                    return $false
                }

		        # check sandboxed solution
		        if($sandboxed){
                    Log -Message "Url: '$url'..." -Type $SPSD.LogTypes.Normal -NoNewline
                    if(!$url -or (Get-SPSite $url -ErrorAction SilentlyContinue) -eq $null){
                        Log -Message "Url invalid" -Type $SPSD.LogTypes.Error -NoIndent
                        return $false
                    }
                    if((Get-Service "SPUserCodeV4").Status -ne "Running"){
		                Log -Message "Invalid! Service 'SPUserCodeV4' not running." -Type $SPSD.LogTypes.Error -NoIndent
		                return $false
		            }

                    # get sanboxed solution
		            $solution = Get-SPUserSolution -Identity $solutionName -Site $url -ErrorAction SilentlyContinue
                    if(!$solution)
                    {
                        Log -Message "Not installed" -Type $SPSD.LogTypes.Error -NoIndent
                        return $false
                    }
                    $deployed = $solution.Status -ieq 'Activated'
                    if($deployed){ Log -Message ($solution.Status) -Type $SPSD.LogTypes.Success -NoIndent }
                    else{ Log -Message ($solution.Status) -Type $SPSD.LogTypes.Error -NoIndent }
		            return $deployed
		        }

                # get farm solution
		        $solution = Get-SPSolution -Identity $solutionName -ErrorAction SilentlyContinue
		        $isGAC = $solution.ContainsGlobalAssembly
		        $deployed = $solution.Deployed
		        $webApps = $solution.DeployedWebApplications

		        # check solution deployed to web application
		        if($url){
                    Log -Message "Url: '$url'..." -Type $SPSD.LogTypes.Normal -NoNewline
                    if((Get-SPWebApplication $url -ErrorAction SilentlyContinue) -eq $null){
                        Log -Message "Url invalid" -Type $SPSD.LogTypes.Error -NoIndent
                        return $false
                    }
                    if(!$solution){
                        Log -Message "Not installed" -Type $SPSD.LogTypes.Error -NoIndent
                        return $false
                    }
		            if($isGAC){
                        Log -Message "Invalid" -Type $SPSD.LogTypes.Error -NoIndent
		                Log -Message "     Solution '$solutionName' is a GAC solution." -Type $SPSD.LogTypes.Warning 
		                Log -Message "     Checking GAC instead..." -Type $SPSD.LogTypes.Normal -NoNewline 
		            }
                    elseif($deployed -and ($webApps | Where-Object { $_.Url -match "(?i)$url" }).count){
                        Log -Message "Deployed" -Type $SPSD.LogTypes.Success -NoIndent
                        return $true 
		            }
                
                    else{
                        Log -Message "Not Deployed" -Type $SPSD.LogTypes.Error -NoIndent
                        return $false
                    }
		        }
                if(!$solution){
                    Log -Message "Not installed" -Type $SPSD.LogTypes.Error -NoIndent
                    return $false
                }
		        # check solution deployed to all content urls
		        if(!$isGAC){
                    Log -Message "Invalid" -Type $SPSD.LogTypes.Error -NoIndent
		            Log -Message "Solution '$solutionName' is a WebApplication solution." -Type $SPSD.LogTypes.Warning 
                    return $false
		        }
                if($deployed){ Log -Message "Deployed" -Type $SPSD.LogTypes.Success -NoIndent }
                else{ Log -Message "Not Deployed" -Type $SPSD.LogTypes.Error -NoIndent }
		        return $deployed
            }
            finally{
                Stop-SPAssignment -Global
            }
		}
        #endregion
	    #region GetSolutions
	    # Desc: Creates a Hashtable wit farm and sandboxed solutions out of the environment configuration
        #       It parses the input xml an assures that solutions are not deployed multiple times
        #       If no solutions are configured all WSPs in the solutions folder are returned as farm solutions
        Function GetSolutions(){
            $result = @{ 
                        SiteSolutions = @{ }
                        FarmSolutions = @{ }
                       }
		    Log -Message "Getting solution files to $($Command.ToLower())" -Type $SPSD.LogTypes.Information -Indent
		    try{
                # get overall attributes (if present)
                $gForce = GetBoolAttribute -node $env.Solutions -attribute "Force"
                $gOverwrite = GetBoolAttribute -node $env.Solutions -attribute "Overwrite"
                $gCompLevel = $env.Solutions.CompatibilityLevel
                
                # No solutions configured in Solutions node, taking all in solutions folder
		        if(!$env.Solutions -or !$env.Solutions.Solution){
		            Log -Message "Solutions node not configured. Using all solutions in '.\Solutions' folder" -Type $SPSD.LogTypes.Normal
                    $fileEntries = Get-ChildItem $solDir.TrimEnd("\\") *.wsp | Select Name
                    foreach($file in $fileEntries) 
                    {
                        if(!$result.FarmSolutions.ContainsKey($file.Name)){
                            $result.FarmSolutions.Add($file.Name, 
                                @{ Name = $file.Name
                                Force = $gForce
                                AllUrls = $true
                                Overwrite = $gOverwrite
                                CompatibilityLevel = $gCompLevel })
                        }
                    }
                    return $result
		        }
                # validating configured solutions in solutions node
                $solutions = $env.Solutions.Solution
                if($solutions)
                {
                    $solutions | ForEach-Object {
		                $solutionName = $_.Name
                        $force = GetBoolAttribute -node $_ -attribute "Force" -defaultIfNotExisting $gForce
                        $overwrite = GetBoolAttribute -node $_ -attribute "Overwrite" -defaultIfNotExisting $gOverwrite
                        $compLevel = $_.CompatibilityLevel
                        if(-not($compLevel) -and $gCompLevel){
                            $compLevel = $gCompLevel
                        }

                        # Check on SiteCollections (sandboxed)
                        $siteColls = (Select-Xml -xml $_ -XPath "SiteCollection") | ForEach-Object {$_.Node}
                        if($siteColls){
                            $siteColls | ForEach-Object {
                                # add a new solution
                                if(!$result.SiteSolutions.ContainsKey($solutionName)){
                                    $result.SiteSolutions.Add($solutionName, 
                                        @{  Name = $solutionName
                                        Force = $force
                                        Overwrite = $overwrite
                                        Urls = @($_.Url)})
                                }
                                else {
                                    # just add another url
                                    $result.SiteSolutions[$solutionName]["Urls"] += $_.Url
                                }
                            }
                        }
                      
                        # check on WebApplications (farm)
                        $webApps = (Select-Xml -xml $_ -XPath "WebApplication") | ForEach-Object {$_.Node}
                        if($webApps){
                            $webApps | ForEach-Object {
                                # add a new solution
                                if(!$result.FarmSolutions.ContainsKey($solutionName)){
                                    $result.FarmSolutions.Add($solutionName, 
                                        @{  Name = $solutionName
                                        Force = $force
                                        Overwrite = $overwrite
                                        CompatibilityLevel = $compLevel
                                        Urls = @($_.Url)})
                                }
                                else {
                                    # just add another url
                                    $result.FarmSolutions[$solutionName]["Urls"] += $_.Url
                                }
                            }
                        }

                        #Check Global (farm)
                        if($_.ChildNodes.Count -eq 0){
                            if(!$result.FarmSolutions.ContainsKey($solutionName)){
                                $result.FarmSolutions.Add($solutionName, 
                                    @{ Name = $solutionName
                                    Force = $force
                                    CompatibilityLevel = $compLevel
                                    AllUrls = $true # will override additonal web application deployments
                                    Overwrite = $overwrite })
                            }
                            else {
                                 $result.FarmSolutions[$solutionName]["AllUrls"] = $true # will override additonal web application deployments
                            }
                        }
                    }
		        }
		    }
		    finally{
		        LogOutdent
		    }
            return $result
        }
        #endregion
	    #region CheckIfSolutionFilesExist
	    # Desc: Checks if all configured solutions file do exist in the solutions folder
        Function CheckIfSolutionFilesExist([System.Collections.Hashtable]$solutions){
            $allSolutions = ($solutions.FarmSolutions + $solutions.SiteSolutions).Keys | Get-Unique
            Log -Message "Checking if solution files exist in '.\Solutions' folder" -Type $SPSD.LogTypes.Information -Indent
            if($allSolutions){
                $errors = $false
                $allSolutions | ForEach-Object{
                    Log -Message ($_+"...") -Type $SPSD.LogTypes.Normal -NoNewline
                    if((Test-Path -Path ($solDir + "\" + $_))){
                    	Log -Message "Exists" -Type $SPSD.LogTypes.Success -NoIndent
		            }
		            else{
		                Log -Message "Missing" -Type $SPSD.LogTypes.Error -NoIndent
                        $errors = $true
                    }
                }
                if($errors){
                    Throw "One or more configured solutions files were not found in the '.\Solutions' folder"
                }
            }
            else{
                Log -Message "No solutions configured to $($Command.ToLower())" -Type $SPSD.LogTypes.Warning
            }
            LogOutdent
        }
        #endregion
	    #region CheckIfSiteExists
	    # Desc: Checks if a SPSite object exists at a given url
        Function CheckIfSiteExists([string]$url){
            if(!$url){ return $true }
            Start-SPAssignment -Global
            Log -Message "Site: '$url'..." -Type $SPSD.LogTypes.Normal -NoNewline
            if(!$url -or (Get-SPSite $url -ErrorAction SilentlyContinue) -eq $null){
                Log -Message "No Found" -Type $SPSD.LogTypes.Error -NoIndent
                return $false
            }
            Log -Message "Ok" -Type $SPSD.LogTypes.Success -NoIndent
            Stop-SPAssignment -Global
            return $true
        }
        #endregion
	    #region CheckIfWebAppExists
	    # Desc: Checks if a WebApplication object exists at a given url
        Function CheckIfWebAppExists([string]$url){
            if(!$url){ return $true }
            Start-SPAssignment -Global
            Log -Message "WebApplication: '$url'..." -Type $SPSD.LogTypes.Normal -NoNewline
            if(!$url -or(Get-SPWebApplication $url -ErrorAction SilentlyContinue) -eq $null){
                Log -Message "No Found" -Type $SPSD.LogTypes.Error -NoIndent
                return $false
            }
            Log -Message "Ok" -Type $SPSD.LogTypes.Success -NoIndent
            Stop-SPAssignment -Global
            return $true
        }
        #endregion
	    #region CheckIfUrlsExists
	    # Desc: Checks if all configured urls for solutions deployment do exist
        Function CheckIfUrlsExists([System.Collections.Hashtable]$solutions){
           Log -Message "Checking if specified Sites and Web Applications exist" -Type $SPSD.LogTypes.Information -Indent
           $urlExists = $true

           # checking web applications solutions
           if($solutions.FarmSolutions.Values.Urls.Count -gt 0){
               $solutions.FarmSolutions.Values.Urls | Get-Unique | ForEach-Object {$urlExists =  (CheckIfWebAppExists $_) -and $urlExists} 
           }
           else { Log -Message "No target WebApplications configured" -Type $SPSD.LogTypes.Normal}

           # checking site collections
           if($solutions.SiteSolutions.Values.Urls.Count -gt 0){
               $solutions.SiteSolutions.Values.Urls | Get-Unique | ForEach-Object {$urlExists =  (CheckIfSiteExists $_) -and $urlExists} 
           }
           else { Log -Message "No target Sites configured" -Type $SPSD.LogTypes.Normal}

           if(!$urlExists){
                Throw "One or more target Sites/WebApplications do not exists"
           }
           LogOutdent
        }
        #endregion
    #endregion
    #region Deployment.Commands
	    #region RunDeployment
	    # Desc: Runs the deployment tasks based on the user input
		Function Run(){
		    PreConditions
			Initialize $vars
            Execute-Extensions $events.Initialize

		    switch($DeploymentCommand){
		       $SPSD.Commands.Deploy   { Deploy   }
		       $SPSD.Commands.Retract  { Retract  }
		       $SPSD.Commands.Redeploy { Redeploy }
		       $SPSD.Commands.Update  { Update  }
		    }
		}
        #endregion
        #region Deployment.Commands.Deploy
	        #region Deploy
	        # Desc: Deploys all configured solutions and performs pre and post deploy custom code
	        Function Deploy(){ 
                $deployElapsedTime = [System.Diagnostics.Stopwatch]::StartNew()
   
	            Log -Message "Begin solution deployment" -Type $SPSD.LogTypes.Information -Indent

                Execute-Extensions $events.BeforeDeploy

	            Log -Message "Begin custom BeforeDeploy target" -Type $SPSD.LogTypes.Information -Indent
	            BeforeDeploy $vars
                LogOutdent

	            if($DeploymentType -ne $SPSD.DeploymentTypes.Extensions){
	                DeploySolutions
	                RunActions -actionCmd $SPSD.Commands.Deploy -skipWarmup
	            }

                Execute-Extensions $events.AfterDeploy

                Log -Message "Begin custom AfterDeploy target" -Type $SPSD.LogTypes.Information -Indent
	            AfterDeploy $vars
                LogOutdent


                RunActions -actionCmd $SPSD.Commands.Deploy -onlyWarmup
	            Log -Message ("Solution deployment finished ("+$deployElapsedTime.Elapsed+ ")") -Type $SPSD.LogTypes.Information -Outdent
	        }
            #endregion
	        #region DeploySolutions
	        # Desc: Deploys the solutions
	        Function DeploySolutions(){
               $solutions = GetSolutions
               CheckIfSolutionFilesExist -solutions $solutions
               CheckIfUrlsExists -solutions $solutions
               Log -Message "Deploying:" -Type $SPSD.LogTypes.Information -Indent
               $solutions.FarmSolutions.Values | ForEach-Object{ DeployFarmSolution -solutionDefinition $_}
               $solutions.SiteSolutions.Values | ForEach-Object{ DeploySandboxedSolution -solutionDefinition $_ }
               LogOutdent
	        }
            #endregion
	        #region DeployFarmSolution
	        # Desc: Deploys farm solutions either to GAC or to WebApplication urls
	        Function DeployFarmSolution([System.Collections.Hashtable]$solutionDefinition){
                $elapsedTime = [System.Diagnostics.Stopwatch]::StartNew()

                if($solutionDefinition){
                    try{
                        $solutionName = $solutionDefinition["Name"]
                        $force = $solutionDefinition["Force"]
                        $overwrite = $solutionDefinition["Overwrite"]
                        $compLevel = $solutionDefinition["CompatibilityLevel"]
                        $AllUrls = $solutionDefinition["AllUrls"]
                        $Urls = $solutionDefinition["Urls"] | Get-Unique

            	        Log -Message "'$solutionName' (farm solution)" -Type $SPSD.LogTypes.Information -Indent
						
						$processSolution = (ProcessSolution $vars $solutionName $SPSD.Commands.Deploy $false) -or $false
                        Execute-Extensions $events.ProcessSolutions

						if($processSolution){
							Log -Message "'$solutionName' (farm solution) skipped by custom target" -Type $SPSD.LogTypes.Warning
							return
						}
						
                        $solution = Get-SPSolution -Identity $solutionName -ErrorAction:SilentlyContinue
                        if ($solution -ne $null -and $overwrite)
                        {
                            Log -Message "Solution already exist and overwriting set to true" -Type $SPSD.LogTypes.warning -Indent
                            RetractFarmSolution -solutionDefinition $solutionDefinition
                            LogOutdent
                        }
                        elseif ($solution -ne $null){
                            Log -Message "Solution already exist and overwrite is set to false, " -Type $SPSD.LogTypes.Warning
                            Log -Message "Skipping this solution" -Type $SPSD.LogTypes.Warning
                            return
                        }

                        # Adding Solution
                        Log -Message "Adding solution..." -Type $SPSD.LogTypes.Normal -NoNewLine
                        Add-SPSolution -LiteralPath "$solDir\$solutionName" | Out-Null
                        $solution = Get-SPSolution -Identity $solutionName -ErrorAction:SilentlyContinue
                        if($solution){ Log -Message "Ok" -Type $SPSD.LogTypes.Success -NoIndent }
                        else{ Log -Message "Failed" -Type $SPSD.LogTypes.Error -NoIndent
                              Throw "Farm solution '$solutionName' could not be added"
                        }

                        if($SPSD.InstalledVersion -eq 14 -and !$AllowCASPolicies -and $solution.ContainsCasPolicy){
                            Log -Message "Solution contains CAS policy which is restricted in the deploymentconfiguration. Solution skipped" -Type $SPSD.LogTypes.Warning
                            Remove-SPSolution -Identity $solutionName -Confirm:$false -ErrorAction:SilentlyContinue
                            return
                        }
                        if(!$AllowGACDeployment -and $solution.ContainsGlobalAssembly){
                            Log -Message "Solution contains GAC assemlby which is restricted in the deployment configuration. Solution skipped" -Type $SPSD.LogTypes.Warning
                            Remove-SPSolution -Identity $solutionName -Confirm:$false -ErrorAction:SilentlyContinue
                            return
                        }

                        # Deploying
                        $runCount = 0
                        While($runCount -le $DeploymentRetries){
                            $runCount++

                            if ($solution.ContainsWebApplicationResource)
                            {

                                if($AllUrls -or $Urls.Count -eq 0){
                                    Log -Message "Deploying to all content urls..." -Type $SPSD.LogTypes.Normal -NoNewline
									if ($SPSD.InstalledVersion -eq 14){
                                    	Install-SPSolution -Identity $solutionName -AllWebApplications -CASPolicies:$AllowCASPolicies -GACDeployment:$AllowGACDeployment -force:$force
									}
                                    elseif ($SPSD.InstalledVersion -eq 15){
                                        if($compLevel){
                                    	    Install-SPSolution -Identity $solutionName -AllWebApplications -FullTrustBinDeployment:$AllowFullTrustBinDeployment -GACDeployment:$AllowGACDeployment -CompatibilityLevel $compLevel -force:$force 
									    }
                                        else{
                                            Install-SPSolution -Identity $solutionName -AllWebApplications -FullTrustBinDeployment:$AllowFullTrustBinDeployment -GACDeployment:$AllowGACDeployment -force:$force 
									    }
                                    }
									Log -Message "Done" -Type $SPSD.LogTypes.Success -NoIndent
                                }
                                else{
                                    $Urls | ForEach-Object {
                                        # if previous deployment job to other URL is still running, wait 5 secs
                                        # fix provided by Elio Struyf
                                        Log -Message ("Deploying to '"+$_+"'...") -Type $SPSD.LogTypes.Normal -NoNewline
                                        if ($Urls.Count -gt 1) {
                                            $solution = Get-SPSolution -Identity $solutionName -ErrorAction:SilentlyContinue
 		                                    $timeout = $DefaultTimeout * 2
		                                    while ($solution.JobExists) 
                                            { 
                                                if($timeout -le 0)
                                                {
                	                                Log -Message "Failed" -Type $SPSD.LogTypes.Error -NoIndent
                                                    Throw "Previous deployment job not finished after "+ ($DefaultTimeout * 2 / 1000) + " seconds" 
                                                }
                                                Start-Sleep -Seconds 2
		                                        Log  . -NoNewLine -Type $SPSD.LogTypes.Normal -NoIndent
                                                $timeout -= 2000
		                                    } 
                                        }
                                        if ($SPSD.InstalledVersion -eq 14){
                                            Install-SPSolution -Identity $solutionName -webapplication $_ -CASPolicies:$AllowCASPolicies -GACDeployment:$AllowGACDeployment -force:$force
                                        }
                                        elseif ($SPSD.InstalledVersion -eq 15){
											if($compLevel){
												Install-SPSolution -Identity $solutionName -webapplication $_ -FullTrustBinDeployment:$AllowFullTrustBinDeployment -GACDeployment:$AllowGACDeployment -CompatibilityLevel $compLevel -force:$force
											}
											else {
												Install-SPSolution -Identity $solutionName -webapplication $_ -FullTrustBinDeployment:$AllowFullTrustBinDeployment -GACDeployment:$AllowGACDeployment -force:$force
											}
                                        }
                                        Log -Message "Done" -Type $SPSD.LogTypes.Success -NoIndent
                                    }
                                }
                            }
                            else
                            {
                                Log -Message "Deploying globally..." -Type $SPSD.LogTypes.Normal -NoNewLine
								if ($SPSD.InstalledVersion -eq 14){
                                	Install-SPSolution -Identity $solutionName -GACDeployment:$AllowGACDeployment -force:$force
								}
                                elseif ($SPSD.InstalledVersion -eq 15){
									if($compLevel){
                                		Install-SPSolution -Identity $solutionName -GACDeployment:$AllowGACDeployment -CompatibilityLevel $compLevel -force:$force
									}
									else {
										Install-SPSolution -Identity $solutionName -GACDeployment:$AllowGACDeployment -force:$force
									}
								}
                                Log -Message "Done" -Type $SPSD.LogTypes.Success -NoIndent
                            }

                            WaitForJobToFinish $solutionName

                            if(FarmSolutionDeployedSuccessful -solutionName $solutionName){
                                return
                            }
                            else{
                                Log -Message ("Message: " + $solution.LastOperationDetails) -Type $SPSD.LogTypes.Warning
                            }
                            if($runCount -gt $DeploymentRetries){
                                Throw ("Could not deploy solution '$solutionName'`nMessage: " + $solution.LastOperationDetails)
                            }
                            else{ Log -Message "Retrying" -Type $SPSD.LogTypes.Information }
                        }
                    }
                    finally{
                        Log -message ("("+$elapsedTime.Elapsed+ ")") -type $SPSD.LogTypes.Normal
                        LogOutdent
                    }
                }
            }
            #endregion
	        #region DeploySandboxedSolution
	        # Desc: Deploys sandboxed solutions to the given urls
	        Function DeploySandboxedSolution([System.Collections.Hashtable]$solutionDefinition){
                $elapsedTime = [System.Diagnostics.Stopwatch]::StartNew()

                if($solutionDefinition){
                    try{
                        $solutionName = $solutionDefinition["Name"]
                        $overwrite = $solutionDefinition["Overwrite"]
                        $Urls = $solutionDefinition["Urls"] | Get-Unique
                    
            	        Log -Message "'$solutionName' (sandboxed solution)" -Type $SPSD.LogTypes.Information -Indent
						
						$processSolution = (ProcessSolution $vars $solutionName $SPSD.Commands.Deploy $true) -or $false
						if($processSolution){
							Log -Message "'$solutionName' (sandboxed solution) skipped by custom target" -Type $SPSD.LogTypes.Warning
							return
						}
						
                        $Urls | Foreach-object{
                            $url = $_

               	            Log -Message "Deploy to '$url'" -Type $SPSD.LogTypes.Normal -Indent
                 
                            try{
                                $solution = Get-SPUserSolution -Identity $solutionName -Site $url -ErrorAction:SilentlyContinue
                                if ($solution -ne $null -and $overwrite)
                                {
                                    Log -Message "Sandboxed solution already exist and overwriting set to true" -Type $SPSD.LogTypes.warning -Indent
                                    $solutionDefinitionRetract = @{
                                        Name = $solutionName
                                        Overwrite =$overwrite
                                        Urls = $url
                                    }
                                    RetractSandboxedSolution -solutionDefinition $solutionDefinitionRetract
                                    LogOutdent
                                }
                                elseif ($solution -ne $null){
                                    Log -Message "Sandboxed solution already exist and overwrite is set to false, " -Type $SPSD.LogTypes.Warning
                                    Log -Message "Skipping this solution" -Type $SPSD.LogTypes.Warning
                        
                                }
                                $solution = Get-SPUserSolution -Identity $solutionName -Site $url -ErrorAction:SilentlyContinue
                                if ($solution -eq $null)
                                {
                                    # Adding User Solution
                                    Log -Message "Adding solution..." -Type $SPSD.LogTypes.Normal -NoNewLine
                                    Add-SPUserSolution -LiteralPath "$solDir\$solutionName" -Site $url | Out-Null
                                    $solution = Get-SPUserSolution -Identity $solutionName -Site $url -ErrorAction:SilentlyContinue
                                    if($solution){ Log -Message "Ok" -Type $SPSD.LogTypes.Success -NoIndent }
                                    else{ Log -Message "Failed" -Type $SPSD.LogTypes.Error -NoIndent
                                          Throw "Sandboxed solution '$solutionName' could not be added to $url"
                                    }

                                    # Deploying
                                    $runCount = 0
                                    While($runCount -le $DeploymentRetries){
                                        $runCount++

                                        Log -Message "Deploying..." -Type $SPSD.LogTypes.Normal -NoNewLine
                                        Install-SPUserSolution -Identity $solutionName -Site $url | Out-Null
                                        Log -Message "Done" -Type $SPSD.LogTypes.Success -NoIndent

                                        WaitForJobToFinish $solutionName -Site $url
                        
                                        if(SandboxedSolutionDeployedSuccessful -solutionName $solutionName -Site $url){
                                            return
                                        }
                                        if($runCount -gt $DeploymentRetries){
                                            Throw "Could not deploy sandboxed solution '$solutionName' to '$url"
                                        }
                                        else{ Log -Message "Retrying" -Type $SPSD.LogTypes.Information }
                                    }
                                }
                            }
                            finally{
                                LogOutdent
                            }
                        }
                    }
                    finally{
                        Log -message ("("+$elapsedTime.Elapsed+ ")") -type $SPSD.LogTypes.Normal
                        LogOutdent
                    }
                }
	        }
            #endregion
        #endregion
        #region Deployment.Commands.Update
	        #region Update
	        # Desc: Updates all configured solutions and performs pre and post update custom code
	        Function Update(){
                $updateElapsedTime = [System.Diagnostics.Stopwatch]::StartNew()
	            Log -Message "Begin solution update" -Type $SPSD.LogTypes.Information -Indent
                
                Execute-Extensions $events.BeforeUpdate

                Log -Message "Begin custom BeforeUpdate target" -Type $SPSD.LogTypes.Information -Indent
	            BeforeUpdate $vars
	            LogOutdent
	        
	            if($DeploymentType -ne $SPSD.DeploymentTypes.Extensions){
	                UpdateSolutions
	                RunActions -actionCmd $SPSD.Commands.Upgrade -skipWarmup
	            }

                Execute-Extensions $events.AfterUpdate

                Log -Message "Begin custom AfterUpdate target" -Type $SPSD.LogTypes.Information -Indent
	            AfterUpdate $vars
                LogOutdent
               

                RunActions -actionCmd $SPSD.Commands.Update -onlyWarmup
	            Log -Message ("Solution update finished ("+$updateElapsedTime.Elapsed+ ")") -Type $SPSD.LogTypes.Information -Outdent
	        }
            #endregion
	        #region UpdateSolutions
	        # Desc: Updates the solutions
	        Function UpdateSolutions(){
               $solutions = GetSolutions
               CheckIfSolutionFilesExist -solutions $solutions
               CheckIfUrlsExists -solutions $solutions
               Log -Message "Updating:" -Type $SPSD.LogTypes.Information -Indent
               $solutions.FarmSolutions.Values | ForEach-Object{ UpdateFarmSolutions -solution $_}
               $solutions.SiteSolutions.Values | ForEach-Object{ UpdateSandboxedSolutions -solution $_ }
               LogOutdent
	        }
            #endregion
	        #region UpdateFarmSolutions
	        # Desc: Updates farm solutions either to GAC or to WebApplication urls
	        Function UpdateFarmSolutions([System.Collections.Hashtable]$solutionDefinition){
                $elapsedTime = [System.Diagnostics.Stopwatch]::StartNew()
                if($solutionDefinition){
                    try{
                        $solutionName = $solutionDefinition["Name"]
                        $force = $solutionDefinition["Force"]
                        $overwrite = $solutionDefinition["Overwrite"]

            	        Log -Message "'$solutionName' (farm solution)" -Type $SPSD.LogTypes.Information -Indent

						$processSolution = (ProcessSolution $vars $solutionName $SPSD.Commands.Update $false) -or $false
						if($processSolution){
							Log -Message "'$solutionName' (farm solution) skipped by custom target" -Type $SPSD.LogTypes.Warning
							return
						}
						
                        $solution = Get-SPSolution -Identity $solutionName -ErrorAction:SilentlyContinue
                        if ($solution -eq $null)
                        {
                            Log -Message "Solution does not exist, deploying instead" -Type $SPSD.LogTypes.warning -Indent
                            DeployFarmSolution -solutionDefinition $solutionDefinition
                            LogOutdent
                            return
                        }

                        # Updating
                        $runCount = 0
                        While($runCount -le $DeploymentRetries){
                            $runCount++

                            Log -Message "Updating..." -Type $SPSD.LogTypes.Normal -NoNewline
							if ($SPSD.InstalledVersion -eq 14){
                            	Update-SPSolution -LiteralPath "$solDir\$solutionName" -Identity $solutionName -CASPolicies:$AllowCASPolicies -GACDeployment:$AllowGACDeployment -force:$force -Confirm:$false
							}
                            elseif ($SPSD.InstalledVersion -eq 15){
								if($solution.ContainsWebApplicationResource){
									Update-SPSolution -LiteralPath "$solDir\$solutionName" -Identity $solutionName -FullTrustBinDeployment:$AllowFullTrustBinDeployment -GACDeployment:$AllowGACDeployment -force:$force -Confirm:$false
								}
								else{
									Update-SPSolution -LiteralPath "$solDir\$solutionName" -Identity $solutionName -GACDeployment:$AllowGACDeployment -force:$force -Confirm:$false
								}
							}
                            Log -Message "Done" -Type $SPSD.LogTypes.Success -NoIndent

                            WaitForJobToFinish $solutionName

                            if(FarmSolutionDeployedSuccessful -solutionName $solutionName){
                                return
                            }
                            else{
                                Log -Message ("Message: " + $solution.LastOperationDetails) -Type $SPSD.LogTypes.Warning
                            }
                            if($runCount -gt $DeploymentRetries){
                                Throw ("Could not update solution '$solutionName'`nMessage: " + $solution.LastOperationDetails)
                            }
                            else{ Log -Message "Retrying" -Type $SPSD.LogTypes.Information }
                        }
                    }
                    finally{
                        Log -message ("("+$elapsedTime.Elapsed+ ")") -type $SPSD.LogTypes.Normal
                        LogOutdent
                    }
                }
	        }
            #endregion
	        #region UpdateSandboxedSolutions
	        # Desc: Updates sandboxed solutions to the given urls, renames the target solution with the current date
            #       updates the currently activated solution with the same name
	        Function UpdateSandboxedSolutions([System.Collections.Hashtable]$solutionDefinition){
                $elapsedTime = [System.Diagnostics.Stopwatch]::StartNew()
                if($solutionDefinition){
                    try{
                        $solutionName = $solutionDefinition["Name"]
                        $overwrite = $solutionDefinition["Overwrite"]
                        $Urls = $solutionDefinition["Urls"] | Get-Unique

						
            	        Log -Message "'$solutionName' (sandboxed solution)" -Type $SPSD.LogTypes.Information -Indent
						
						$processSolution = (ProcessSolution $vars $solutionName $SPSD.Commands.Update $true) -or $false
						if($processSolution){
							Log -Message "'$solutionName' (sandboxed solution) skipped by custom target" -Type $SPSD.LogTypes.Warning
							return
						}
						
                        $Urls | Foreach-object{
                            $url = $_

               	            Log -Message "Update to '$url'" -Type $SPSD.LogTypes.Information -Indent
                            try{
                                # get the first activated (!) solution of that name (there should be only one)
                                $solutions = ((Get-SPUserSolution -Site $url) | Where-Object {$_.Name -imatch $solutionName -and $_.Status -eq [Microsoft.SharePoint.SPUserSolutionStatus]::Activated})
                                if ($solutions -eq $null)
                                {
                                    Log -Message "Activated sandboxed does not exist, deploying instead" -Type $SPSD.LogTypes.warning -Indent
                                    DeploySandboxedSolution -solutionDefinition $solutionDefinition
                                    LogOutdent
                            
                                }
                                else{
                                    # Adding User Solution
                                    Log -Message "Adding solution..." -Type $SPSD.LogTypes.Normal -NoNewLine
                                    $newSolutionName = "$LogTime-"+[System.IO.Path]::GetFileNameWithoutExtension("$solDir\$solutionName")+".wsp"
                                    Copy-Item -LiteralPath "$solDir\$solutionName" -Destination "$solDir\$newSolutionName" -Force
                                    Add-SPUserSolution -LiteralPath "$solDir\$newSolutionName" -Site $url | Out-Null
                                    $newSolution = Get-SPUserSolution -Identity "$newSolutionName" -Site $url -ErrorAction:SilentlyContinue
                                    if($newSolution){ Log -Message "Ok" -Type $SPSD.LogTypes.Success -NoIndent }
                                    else{ Log -Message "Failed" -Type $SPSD.LogTypes.Error -NoIndent
                                          Throw "Sandboxed solution '$newSolution' could not be added to $url"
                                    }
                                    Remove-Item -Path "$solDir\$newSolutionName" -Force
                                    # Updating
                                    $runCount = 0
                                    $solution = $solutions[0]
                                    $oldSolutionName = $solution.Name # use realname of solution to update from now on
                                    While($runCount -le $DeploymentRetries){
                                        $runCount++

                                        Log -Message "Updating '$oldSolutionName' to '$newSolutionName'..." -Type $SPSD.LogTypes.Normal -NoNewline
                                        Update-SPUserSolution -Identity $solution -ToSolution $newSolutionName -Site $url -Confirm:$false
                                        Log -Message "Done" -Type $SPSD.LogTypes.Success -NoIndent

                                        WaitForJobToFinish $newSolutionName -Site $url

                                        if(SandboxedSolutionDeployedSuccessful -solutionName $newSolutionName -Site $url){
                                            return
                                        }
                                        if($runCount -gt $DeploymentRetries){
                                            Throw "Could not update sandboxed solution '$oldSolutionName' at *$url'"
                                        }
                                        else{ Log -Message "Retrying" -Type $SPSD.LogTypes.Information }
                                    }

                                }
                            }
                            finally{
                                LogOutdent
                            }
                        }
                    }
                    finally{
                        Log -message ("("+$elapsedTime.Elapsed+ ")") -type $SPSD.LogTypes.Normal
                        LogOutdent
                    }
                }
	        }
            #endregion
        #endregion
        #region Deployment.Commands.Retract
	        #region Retract
	        # Desc: Retracts all configured solutions and performs pre and post retraction custom code
	        Function Retract(){
                $retractElapsedTime = [System.Diagnostics.Stopwatch]::StartNew()
	            Log -Message "Begin solution retraction" -Type $SPSD.LogTypes.Information -Indent

                Execute-Extensions $events.BeforeRetract

                Log -Message "Begin custom BeforeRetract target" -Type $SPSD.LogTypes.Information -Indent
	            BeforeRetract $vars
	            LogOutdent
	        
	            if($DeploymentType -ne $SPSD.DeploymentTypes.Extensions){
	                RetractSolutions
	                RunActions -actionCmd $SPSD.Commands.Retract -skipWarmup
	            }

                Execute-Extensions $events.AfterRetract

                Log -Message "Begin custom AfterRetract target" -Type $SPSD.LogTypes.Information -Indent
	            AfterRetract $vars


	            LogOutdent
	            Log -Message ("Solution retraction finished("+$retractElapsedTime.Elapsed+ ")") -Type $SPSD.LogTypes.Information -Outdent
	        }
            #endregion
	        #region RetractSolutions
	        # Desc: Retracts the solutions
	        Function RetractSolutions(){
               $solutions = GetSolutions
               # existance of files in /Solutions folder not neccesary on retraction

               CheckIfUrlsExists -solutions $solutions
               Log -Message "Retracting:" -Type $SPSD.LogTypes.Information -Indent
               $solutions.FarmSolutions.Values | ForEach-Object{ RetractFarmSolution -solution $_}
               $solutions.SiteSolutions.Values | ForEach-Object{ RetractSandboxedSolution -solution $_ }
               LogOutdent

	        }
            #endregion
	        #region RetractFarmSolution
	        # Desc: Retracts farm solutions from either GAC or WebApplication urls
	        Function RetractFarmSolution([System.Collections.Hashtable]$solutionDefinition){
                $elapsedTime = [System.Diagnostics.Stopwatch]::StartNew()
                if($solutionDefinition){
                    try{
                        $solutionName = $solutionDefinition["Name"]
                        $force = $solutionDefinition["Force"]

            	        Log -Message "'$solutionName' (farm solution)" -Type $SPSD.LogTypes.Information -Indent

						$processSolution = (ProcessSolution $vars $solutionName $SPSD.Commands.Retract $false) -or $false
						if($processSolution){
							Log -Message "'$solutionName' (farm solution) skipped by custom target" -Type $SPSD.LogTypes.Warning
							return
						}
						
                        $solution = Get-SPSolution -Identity $solutionName -ErrorAction:SilentlyContinue
                        if ($solution -eq $null -and $overwrite)
                        {
                            Log -Message "Solution is not installed, skipping retraction" -Type $SPSD.LogTypes.Normal
                            return
                        }

                        # Retracting
                        $runCount = 0
                        While($runCount -le $DeploymentRetries){
 		                    $timeout = $DefaultTimeout * 2
		                    while ($solution.JobExists) 
                            { 
                                if($timeout -le 0)
                                {
                                    Throw "Previous deployment job not finished after "+ ($DefaultTimeout * 2 / 1000) + " seconds" 
                                }
                                Start-Sleep -Seconds 2
                                $timeout -= 2000
		                    }


                            $runCount++

                            if($solution.Deployed){
                                Log -Message "Retracting..." -Type $SPSD.LogTypes.Normal -NoNewline
                                if ($solution.ContainsWebApplicationResource)
                                {



                                     $caUrl = (Get-spwebapplication -includecentraladministration | where {$_.IsAdministrationWebApplication}).Url
                                     if(($solution.DeployedWebApplications | ? { $_.Url -eq $caUrl}) -ne $null){
                                        # Solution also deployed to central admin 
                                        # remove there first as this is not done with the -AllWebApplications switch
                                        Uninstall-SPSolution -Identity $solutionName -WebApplication $caUrl -Confirm:$false
                                        Log -Message "Done (Central Admin)" -Type $SPSD.LogTypes.Success -NoIndent
                                        WaitForJobToFinish $solutionName -Retract
                                        Log -Message "Retracting (all other web applications)..." -Type $SPSD.LogTypes.Normal -NoNewline
                                     }
                                     Uninstall-SPSolution -Identity $solutionName -AllWebApplications -Confirm:$false
                                }
                                else
                                {
                                     Uninstall-SPSolution -identity $solutionName -confirm:$false
                                }
                                Log -Message "Done" -Type $SPSD.LogTypes.Success -NoIndent
                            }
                            WaitForJobToFinish $solutionName -Retract

                            # Removing Solution
                            Log -Message "Removing solution..." -Type $SPSD.LogTypes.Normal -NoNewLine
                            Remove-SPSolution -Identity $solutionName -Confirm:$false -ErrorAction:SilentlyContinue
                            Log -Message "Ok" -Type $SPSD.LogTypes.Success -NoIndent

                            if(FarmSolutionRetractedSuccessful -solutionName $solutionName){
                                return
                            }
                            if($runCount -gt $DeploymentRetries){
                                Throw ("Could not retract solution '$solutionName'")
                            }
                            else{ Log -Message "Retrying" -Type $SPSD.LogTypes.Information }
                        }
                    }
                    finally{
                        Log -message ("("+$elapsedTime.Elapsed+ ")") -type $SPSD.LogTypes.Normal
                        LogOutdent
                    }
                }
	        }
            #endregion
	        #region RetractSandboxedSolution
	        # Desc: Retracts sandboxed solutions to the given urls
	        Function RetractSandboxedSolution([System.Collections.Hashtable]$solutionDefinition){
                $elapsedTime = [System.Diagnostics.Stopwatch]::StartNew()
                if($solutionDefinition){
                    try{
                        $solutionName = $solutionDefinition["Name"]
                        $force = $solutionDefinition["Force"]
                        $Urls = $solutionDefinition["Urls"] | Get-Unique
            	        Log -Message "'$solutionName' (sandboxed solution)" -Type $SPSD.LogTypes.Information -Indent
						
						$processSolution = (ProcessSolution $vars $solutionName $SPSD.Commands.Retract $true) -or $false
						if($processSolution){
							Log -Message "'$solutionName' (sandboxed solution) skipped by custom target" -Type $SPSD.LogTypes.Warning
							return
						}
						
                        $Urls | Foreach-object{
                            $url = $_

               	            Log -Message "Retract from '$url'" -Type $SPSD.LogTypes.Normal -Indent
                            try{
                                $solutions = ((Get-SPUserSolution -Site $url) | Where-Object {$_.Name -imatch $solutionName -and $_.Status -eq [Microsoft.SharePoint.SPUserSolutionStatus]::Activated})
                                if ($solutions -eq $null)
                                {
                                    Log -Message "Sandboxed solution is not installed, skipping retraction" -Type $SPSD.LogTypes.Normal
                                }
                                else{
                                    # Retracting
                                    $runCount = 0
                                    While($runCount -le $DeploymentRetries){
                                        $runCount++
                                        $solutions | ForEach-Object {
                                            if($_){
                                               $solution = $_
                                               $localSolutionName = $solution.Name
                                                if($solution.Status -ne [Microsoft.SharePoint.SPUserSolutionStatus]::Deactivated ){
                                                    Log -Message "Retracting '$localSolutionName'..." -Type $SPSD.LogTypes.Normal -NoNewline
                                                    Uninstall-SPUserSolution -Identity $localSolutionName -Site $url -Confirm:$false
                                                    Log -Message "Done" -Type $SPSD.LogTypes.Success -NoIndent
                                                }
                                                WaitForJobToFinish $localSolutionName -Retract -Site $url

                                                # Removing Solution
                                                Log -Message "Removing sandboxed solution..." -Type $SPSD.LogTypes.Normal -NoNewLine
                                                Remove-SPUserSolution -Identity $localSolutionName -Site $url -Confirm:$false -ErrorAction:SilentlyContinue
                                                Log -Message "Ok" -Type $SPSD.LogTypes.Success -NoIndent

                                                if(SandboxedSolutionRetractedSuccessful -solutionName $localSolutionName -Site $url){
                                                    break
                                                }
                                                if($runCount -gt $DeploymentRetries){
                                                    Throw ("Could not retract sandboxed solution '$localSolutionName' from '$url'")
                                                }
                                                else{ Log -Message "Retrying" -Type $SPSD.LogTypes.Information }
                                            }
                                        }
                                    }
                                }
                            }
                            finally{
                                LogOutdent
                            }
                        }
                    }
                    finally{
                        Log -message ("("+$elapsedTime.Elapsed+ ")") -type $SPSD.LogTypes.Normal
                        LogOutdent
                    }
                }
	        }
            #endregion
        #endregion
        #region Deployment.Commands.Redeploy
	        #region Redeploy
	        # Desc: Redeploys the solutions. Runs first Retract and then Deploy
	        Function Redeploy(){
	            Log -Message "Begin solution redeployment" -Type $SPSD.LogTypes.Information -Indent
	            Retract
	            Deploy
	            Log -Message "Solution redeployment finished" -Type $SPSD.LogTypes.Information -Outdent
	        }
            #endregion
        #endregion
    #endregion
#endregion
