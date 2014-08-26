function Create-SiteStructure($parameters, [System.Xml.XmlElement]$xml,[string]$extId, [string]$extensionPath){
    $webAppUrl = $parameters["WebAppUrl"]
    $databaseServer = $parameters["DatabaseServer"]
    Log "Creating SiteStructure in WebApp '$webAppUrl'" -type $SPSD.LogTypes.Information -Indent

    #disposing all objects which where used before
    Stop-SPAssignment -Global
    $sites = $xml.Sites
    foreach($site in $sites.Site) {
        $siteElapsedTime = [System.Diagnostics.Stopwatch]::StartNew()
        Start-SPAssignment -Global
        try{
            Log -message ("Site: " + $site.Title) -type $SPSD.LogTypes.Information -indent
                # content database
                New-SPSDContentDB $site.ContentDB $databaseServer $webAppUrl
    
                # managed path
                $explicitPath = [System.Convert]::ToBoolean($site.ExplicitPath)
                if($explicitPath){
                    $managedPath = $site.Url.TrimStart("/").TrimEnd("/")
                    New-SPSDManagedPath $managedPath $webAppUrl $explicitPath
                }
                elseif ($site.Url.Split("/").Length -gt 1) {
                    $managedPath = $site.Url.Substring($site.Url.IndexOf("/") + 1)
                    if ($managedPath.Contains("/")) {
                        $managedPath = $managedPath.Substring(0, $managedPath.IndexOf("/"))
                        New-SPSDManagedPath $managedPath $webAppUrl
                    }
                }
            
                $url = $($webAppUrl + $site.Url)
                # site collection + webs
                New-SPSDSite $url $site.Template $($site.LCID) $site.Title $vars["UserID"] $site.ContentDB
                if($site.PermissionGroup -ne $null -and $xml.PermissionGroups -ne $null){
                    Sleep -Seconds 5
                    $xml.PermissionGroups.PermissionGroup | ? {$_.Name -eq $site.PermissionGroup} | % {
                        Add-SPSDPermission $url $_.Permission
                    }
                }
                # Create Subwebs
                if($site.Webs -ne $null -and $site.Webs.Web.Count -gt 0){
                    Log -message ("Subwebs") -type $SPSD.LogTypes.Normal -indent
                    foreach($web in $site.Webs.Web){
                          New-SPSDWeb $($webAppUrl + $web.Url) $web.Template $web.Title $($web.LCID) $web.Webs
                    }
                    LogOutdent
                }
        }
        finally{
            Log -message ("("+$siteElapsedTime.Elapsed+ ")") -type $SPSD.LogTypes.Normal
            Stop-SPAssignment -Global
        }
        LogOutdent
    }
    #retarting global assigment for the rest of SPSD
    Start-SPAssignment -Global
    LogOutdent
}

function New-SPSDManagedPath([string]$managedpath, [string]$webapp, [bool]$explicit) {
	Log -message "Managed Path '$managedpath'..." -type $SPSD.LogTypes.Normal -NoNewLine
    if ( (Get-SPManagedPath -Identity $managedpath -webapplication $webapp -ErrorAction SilentlyContinue -WarningAction SilentlyContinue) -eq $null) {
		$null = New-SPManagedPath -RelativeURL $managedpath -webapplication $webapp -Explicit:$explicit
		Log -message "Created" -type $SPSD.LogTypes.Success -NoIndent
		
	} else {
		Log -message "Exists" -type $SPSD.LogTypes.Warning -NoIndent
	}
}

function New-SPSDSite([string]$url, [string]$template, [int]$lcid, [string]$name, [string]$owner, [string]$contentdb) {
	Log -message "Site: '$url'..." -type $SPSD.LogTypes.Normal -NoNewLine
    [System.Web.HttpContext]::Current = $null

	$url = $url.TrimEnd("/")
    $site = (Get-SPSite -Identity $url -ErrorAction SilentlyContinue -WarningAction SilentlyContinue)
    try{
        if ( $site -eq $null) {
		    try {
                # check if it is a webtemplate
                if($template.StartsWith("{")){
                    $site = New-SPSite -Url $url -Language $lcid -Name $name -OwnerAlias $owner -ContentDatabase $contentdb

                }
                else{
                    $site = New-SPSite -Url $url -Template $template -Language $lcid -Name $name -OwnerAlias $owner -ContentDatabase $contentdb
                }
		        Log -message "Created" -type $SPSD.LogTypes.Success -NoIndent
            } catch {
    		    Log -message "Failed" -type $SPSD.LogTypes.Error -NoIndent
            }
	    } 
        else {
   		    Log -message "Exists" -type $SPSD.LogTypes.Warning -NoIndent
	    }
    }
    finally{
       #$site.Dispose()
       #removed, disposing globally for each site creation instead
    }
    if($template.StartsWith("{")){
	   Set-SPSDWebTemplate $url $template $true
    }



}

function Set-SPSDWebTemplate([string]$url, [string]$template, [bool]$isRootWeb) {
	Log -message "Template '$template'..." -type $SPSD.LogTypes.Normal -NoNewLine
    [System.Web.HttpContext]::Current = $null

	$url = $url.TrimEnd("/")
    $web = (Get-SPWeb -Identity $url -ErrorAction SilentlyContinue -WarningAction SilentlyContinue)
    if ($web -ne $null) {
		try {
				# check if it is a webtemplate
				if($template.StartsWith("{")){
					$web.ApplyWebTemplate($template)
                    $web.Update()		
				}
		    Log -message "Applied" -type $SPSD.LogTypes.Success -NoIndent
            }
		catch {
    		Log -message "A template is already applied" -type $SPSD.LogTypes.Warning -NoIndent
        }
        finally{
            #$web.Dispose()
            #removed, disposing globally for each site creation instead
        }
	} else {
		Log -message "Web missing"  -type $SPSD.LogTypes.Warning
	}
}

function New-SPSDContentDB([string]$dbname, [string]$dbserver, [string]$webapp) {
	Log -message "Content database '$dbname' on SQL '$dbserver'..."  -type $SPSD.LogTypes.Normal -NoNewLine
	if ( (Get-SPContentDatabase -Identity $dbname -ErrorAction SilentlyContinue -WarningAction SilentlyContinue) -eq $null) {
		$null = New-SPContentDatabase -Name $dbname -DatabaseServer $dbserver -WebApplication $webapp
		Log -message "Created" -type $SPSD.LogTypes.Success -NoIndent
	} else {
		Log -message "Exists" -type $SPSD.LogTypes.Warning -NoIndent
	}
}

function New-SPSDWeb([string]$url, [string]$template, [string]$name, [int]$lcid, [System.Xml.XmlElement]$webs ) {
    [System.Web.HttpContext]::Current = $null
    $url = $url.TrimEnd("/")
    $web = Get-SPWeb -Identity $url -ErrorAction SilentlyContinue -WarningAction SilentlyContinue
	Log -message "Web '$name'..."  -type $SPSD.LogTypes.Normal -NoNewLine

    try{
        if ($web -eq $null) {
		    try {
                # check if it is a webtemplate
                if($template.StartsWith("{")){
                    $web = New-SPWeb -Url $url -Name $name -Language $lcid
                }
                else{
                    $web = New-SPWeb -Url $url -Template $template -Name $name -Language $lcid
                }
		        Log -message "Created" -type $SPSD.LogTypes.Success -NoIndent
            } 
            catch {
    		    Log -message "Failed" -type $SPSD.LogTypes.Error -NoIndent
            }
	    } 
        else {
   		    Log -message "Exists" -type $SPSD.LogTypes.Success -NoIndent
	    }
    }
    finally{
       #$web.Dispose();
       #removed, disposing globally for each site creation instead
    }
    if($template.StartsWith("{")){
	        Set-SPSDWebTemplate $url $template $false
    }

    #TODO check for double path in url
    #TODO Permissions for webs
    # Create Subwebs
    if($webs -ne $null -and $webs.Web.Count -gt 0){
        Log -message ("Subwebs") -type $SPSD.LogTypes.Normal -indent
        foreach($web in $webs.Web){
                New-SPSDWeb $($url + "/" + $web.Url) $web.Template $web.Title $($web.LCID) $web.Webs
        }
        LogOutdent
    }
}


function Add-SPSDPermission([string]$url, $permissions){
	if($permissions -eq $null -or $permissions.Count -eq 0 -or $url -eq $null){
		return
	}
    [System.Web.HttpContext]::Current = $null
    Log -message "Permissions" -type $SPSD.LogTypes.Normal -Indent
    $web = (Get-SPWeb -Identity $url -ErrorAction SilentlyContinue -WarningAction SilentlyContinue)
    if ($web -ne $null) {
        try{
                foreach($permission in $permissions){
    	            $account = $null
	                $permissionSet  = $($permission.getAttribute("PermissionSet"))
	                $accountName =  $($permission.getAttribute("User"))

	                # Make sure user is known by SharPoint Groups
	                if ($accountName.Contains("\")) { 
                        Log -message "Ensuring User: '$accountName'..." -type $SPSD.LogTypes.Normal -NoNewline
		                $account = $web.EnsureUser($accountName) 
                       Log -message "Done" -type $SPSD.LogTypes.Success -NoIndent
	                }
	                else {
                        #If the SharePoint Group does not exist, create it with the name and description specified
                        if (!$web.SiteGroups[$accountName])
                        {
                           Log -message "Creating SPGroup: '$accountName'..." -type $SPSD.LogTypes.Normal -NoNewline
                           $rootWeb.SiteGroups.Add($accountName, $web.CurrentUser, $web.CurrentUser, $($accountName + " Description"))
                           Log -message "Done" -type $SPSD.LogTypes.Success -NoIndent
                        }
                        $account = $web.SiteGroups[$accountName]
                    }   
                    Log -message "Setting permission '$permissionSet' for '$accountName'..." -type $SPSD.LogTypes.Normal -NoNewline
	
                    $web.Update()
                    $role = $web.RoleDefinitions[$permissionSet]
                    $assignment = New-Object Microsoft.SharePoint.SPRoleAssignment($account)
                    $assignment.RoleDefinitionBindings.Add($role)
                    $web.RoleAssignments.Add($assignment)
	                $web.Update()
                    Log -message "Done" -type $SPSD.LogTypes.Success -NoIndent

                }
        }
        finally{
            #$web.Dispose()
            #removed, disposing globally for each site creation instead
        }
    }
    else{
        Log -message "Web was not found!" -type $SPSD.LogTypes.Error -NoIndent
    }
    LogOutdent
}

