function Import-SPTermStoreXml($parameters, [System.Xml.XmlElement]$xml,[string]$extId, [string]$extensionPath){
    $overwrite = [System.Convert]::ToBoolean($parameters["Overwrite"])
    $backup = [System.Convert]::ToBoolean($parameters["Backup"])
    if($backup){
        $backupfile = "$logDir\$LogTime-$Command-ManagedMetadata-Backup-$extId.xml"
        Log ("Creating Backup to {0}" -f (GetRelFilePath -filePath $backupfile)) -type $SPSD.LogTypes.Normal 

        . "$extensionPath\ExportManagedMetadata.ps1"
        $export = Export-SPTermStoreXml (Get-SPTermStore "Managed Metadata Service")
        Save-Xml $export $backupfile
    }

    Log ("Retrieving TermStore: {0}" -f $xml.TermStore.Name) -type $SPSD.LogTypes.Normal -Indent

    [Microsoft.SharePoint.Taxonomy.TermStore]$ts = Get-SPTermStore $xml.TermStore.Name

    if($ts -eq $null){
        throw "Termstore with name `"{0}` not found!" -f $xml.TermStore.Name
    }


    # make sure that TermStore is up-to-date
    $ts.UpdateCache()
    Log "Configuring" -type $SPSD.LogTypes.Normal -Indent
    try
    {
        Log "Administrators" -type $SPSD.LogTypes.Normal -Indent
        #administrators
        if($xml.TermStore.TermStoreAdministrators -ne $null -and $xml.TermStore.TermStoreAdministrators.HasChildNodes){
            foreach($tsa in $xml.TermStore.TermStoreAdministrators.TermStoreAdministrator){
                Log ("Adding TermStoreAdministrator with ID: {0}" -f $tsa.InnerText) -type $SPSD.LogTypes.Normal 
                $ts.AddTermStoreAdministrator($tsa.InnerText)
            }
            if($overwrite){
                $ts.TermStoreAdministrators | ? {$_.PrincipalName -notin $xml.TermStore.TermStoreAdministrators.TermStoreAdministrator.InnerText } | %{
                Log ("Removing TermStoreAdministrator with ID: {0}" -f $_.PrincipalName) -type $SPSD.LogTypes.Normal 
                    $ts.DeleteTermStoreAdministrator($_.PrincipalName)
                }
            }
        }
        LogOutdent
        if($xml.TermStore.DefaultLanguage -ne $null -and $overwrite){
            $ts.DefaultLanguage = $xml.TermStore.DefaultLanguage
        }
        if($xml.TermStore.WorkingLanguage -ne $null -and $overwrite){
            $ts.WorkingLanguage = $xml.TermStore.WorkingLanguage
        }

        Log "Languages" -type $SPSD.LogTypes.Normal -Indent
        #languages
        if($xml.TermStore.Languages -ne $null -and $xml.TermStore.Languages.HasChildNodes){
            foreach($LCID in $xml.TermStore.Languages.Language){
                Log ("Adding language with LCID: {0}" -f $LCID) -type $SPSD.LogTypes.Normal 
                $ts.AddLanguage($LCID -as [int])
            }
            if($overwrite){
                $deleteLangs = $ts.Languages | ? {$_.ToString() -notin $xml.TermStore.Languages.Language }
                # save in variable in order to prevent error when modifying collection over which it will be iterated
                $deleteLangs | %{
                    Log ("Removing language with LCID: {0}" -f $_) -type $SPSD.LogTypes.Normal 
                    $ts.DeleteLanguage($_)
                }
            }

        }
        LogOutdent
        LogOutdent

        Log "Groups" -type $SPSD.LogTypes.Normal -Indent
        #groups
        if($xml.TermStore.Groups -ne $null -and $xml.TermStore.Groups.HasChildNodes){
            foreach($group in $xml.TermStore.Groups.Group){
                Set-SPTermStoreGroupFromXml $ts $group $overwrite
            }
        }
        LogOutdent
    }
    catch{
        $ts.RollbackAll()
        throw
    }
    
    $ts.CommitAll()
    $ts.FlushCache()
    LogOutdent

}
function Set-SPTermStoreGroupFromXml([Microsoft.SharePoint.Taxonomy.TermStore]$ts, [System.Xml.XmlElement]$group, [bool]$overwrite){

        if($ts -eq $null){
            throw "Termstore is null!"
        }

        $tsGroup = $null

        # check if group with ID already exists and (re)create.
        if($group.ID -ne $null){
            $tsGroup = $ts.Groups[$group.ID -as [guid]]
            if($tsGroup -ne $null){
                Log ("Group exists already with ID {0}..." -f $group.ID) -NoNewline -type $SPSD.LogTypes.Normal 
                if($overwrite){
                    Log "Recreating (Overwrite=true)" -type $SPSD.LogTypes.Warning -NoIndent 
                    $tsGroup.TermSets | % {$_.Delete()}
                    $tsGroup.Delete()
                    $tsGroup = $ts.CreateGroup($group.Name, $group.ID -as [guid])
                }
                else{
                    Log "Merging" -type $SPSD.LogTypes.Success -NoIndent
                }
            }
        }

        # check if group with Name already exists and (re)create.
        if($group.Name -ne $null -and $tsGroup -eq $null){
            $tsGroup = $ts.Groups[$group.Name]
            if($tsGroup -ne $null){
                Log ("Group exists already with Name {0}..." -f $group.Name) -NoNewline -type $SPSD.LogTypes.Normal 
                if($overwrite){
                    Log "Recreating (Overwrite=true)" -type $SPSD.LogTypes.Warning -NoIndent 
                    $tsGroup.TermSets | % {$_.Delete()}
                    $tsGroup.Delete()
                    $tsGroup = $ts.CreateGroup($group.Name, $group.ID -as [guid])
                }
                else{
                    Log "Merging" -type $SPSD.LogTypes.Success
                }
            }
        }

        if($tsGroup -eq $null){
            if($group.Name -ne $null){
                # if id is specified use it
                Log ("Group {0} does not exist..." -f $group.Name) -NoNewline -type $SPSD.LogTypes.Normal 
                if($group.ID -ne $null){
                   $tsGroup = $ts.CreateGroup($group.Name, $group.ID -as [guid])
                }
                else{
                   $tsGroup = $ts.CreateGroup($group.Name)
                }
                Log "Created" -type $SPSD.LogTypes.Success -NoIndent
            }
            else{
                Throw "Neither name nor id have been specified for the TermGroup"
            }
        }

        # description
        if($group.Description -ne $null -and $overwrite){
            $tsGroup.Description = $group.Description.InnerText
        }
        LogIndent
        Log "GroupManagers" -type $SPSD.LogTypes.Normal -Indent

        #GroupManagers
        if($group.GroupManagers -ne $null -and $group.GroupManagers.HasChildNodes){
            foreach($mgm in $group.GroupManagers.GroupManager){
                Log ("Adding GroupManager with ID: {0}" -f $mgm.InnerText) -type $SPSD.LogTypes.Normal 
                $tsGroup.AddGroupManager($mgm.InnerText)
            }
            if($overwrite){
                $tsGroup.GroupManagers | ? {$_.PrincipalName -notin $group.GroupManagers.GroupManager.InnerText } | %{
                    Log ("Removing GroupManager with ID: {0}" -f $_.PrincipalName) -type $SPSD.LogTypes.Normal 
                    $tsGroup.DeleteGroupManager($_.PrincipalName)
                }
            }
        }
        LogOutdent

        Log "Contributors" -type $SPSD.LogTypes.Normal -Indent
        #contributors
        if($group.Contributors -ne $null -and $group.Contributors.HasChildNodes){
            foreach($contr in $group.Contributors.Contributor){
                Log ("Adding Contributor with ID: {0}" -f $contr.InnerText) -type $SPSD.LogTypes.Normal 
                $tsGroup.AddContributor($contr.InnerText)
            }
            if($overwrite){
                $tsGroup.Contributors | ? {$_.PrincipalName -notin $group.Contributors.Contributor.InnerText } | %{
                    Log ("Removing Contributor with ID: {0}" -f $_.PrincipalName) -type $SPSD.LogTypes.Normal 
                    $tsGroup.DeleteContributor($_.PrincipalName)
                }
            }
        }
        LogOutdent
        
        # committing changes and getting group again to create termsets and terms
        $id = $tsGroup.Id
        $tsGroup.TermStore.CommitAll()
        $tsGroup = $ts.Groups[$id -as [guid]]

        if($group.TermSets -ne $null -and $group.TermSets.ChildNodes){
            foreach($termSet in $group.TermSets.TermSet){
               Set-SPTermSetFromXml $tsGroup $termSet $overwrite
            }
        }
        LogOutdent


}
function Set-SPTermSetFromXml([Microsoft.SharePoint.Taxonomy.Group]$termGroup, [System.Xml.XmlElement]$termSet, [bool]$overwrite){

        if($termGroup -eq $null){
            throw "TermGroup is null!"
        }

        $tsTermSet = $null

        # check if termset with ID already exists and (re)create.
        if($termSet.ID -ne $null){
            $tsTermSet = $termGroup.TermSets[$termSet.ID -as [guid]]
            if($tsTermSet -ne $null){
                Log ("TermSet exists already with ID {0}..." -f $termSet.ID) -NoNewline -type $SPSD.LogTypes.Normal 
                if($overwrite){
                    Log "Recreating (Overwrite=true)" -type $SPSD.LogTypes.Warning -NoIndent
                    $tsTermSet.Delete()
                    $tsTermSet = $termGroup.CreateTermSet($termSet.Name, $termSet.ID -as [guid])
                }
                else{
                    Log "Merging" -type $SPSD.LogTypes.Success -NoIndent
                }
            }
        }

        # check if TermSet with Name already exists and (re)create.
        if($termSet.Name -ne $null -and $tsTermSet -eq $null){
            $tsTermSet = $termGroup.TermSets[$termSet.Name]
            if($tsTermSet -ne $null){
                Log ("TermSet exists already with Name {0}..." -f $termSet.Name) -NoNewline -type $SPSD.LogTypes.Normal 
                if($overwrite){
                    Log "Recreating (Overwrite=true)" -type $SPSD.LogTypes.Warning -NoIndent
                    $tsTermSet.Delete()
                    $tsTermSet = $termGroup.CreateTermSet($termSet.Name, $termSet.ID -as [guid])
                }
                else{
                    Log "Merging" -type $SPSD.LogTypes.Success -NoIndent
                }
            }
        }

        if($tsTermSet -eq $null){
            if($termSet.Name -ne $null){
                # if id is specified use it
                Log ("TermSet {0}..." -f $termSet.Name) -NoNewline -type $SPSD.LogTypes.Normal 
                if($termSet.ID -ne $null){
                   $tsTermSet = $termGroup.CreateTermSet($termSet.Name, $termSet.ID -as [guid])
                }
                else{
                   $tsTermSet = $termGroup.CreateTermSet($termSet.Name)
                }
                Log "Created" -type $SPSD.LogTypes.Success -NoIndent
            }
            else{
                Throw "Neither name nor id have been specified for the TermSet"
            }
        }

        # description
        if($termSet.Description -ne $null -and $overwrite){
            $tsTermSet.Description = $termSet.Description.InnerText
        }

        # IsAvailableForTagging
        if($termSet.IsAvailableForTagging -ne $null -and $overwrite){
            $tsTermSet.IsAvailableForTagging = $termSet.IsAvailableForTagging -as [bool]
        }

        # IsOpenForTermCreation
        if($termSet.IsOpenForTermCreation -ne $null -and $overwrite){
            $tsTermSet.IsOpenForTermCreation = $termSet.IsOpenForTermCreation -as [bool]
        }

        # Owner
        if($termSet.Owner -ne $null -and $overwrite){
            $tsTermSet.Owner = $termSet.Owner
        }

        # Contact
        if($termSet.Contact -ne $null -and $overwrite){
            $tsTermSet.Contact = $termSet.Contact
        }

        LogIndent
        Log "TermSets" -type $SPSD.LogTypes.Normal -Indent
        #stakeholders
        if($termSet.Stakeholders -ne $null -and $termSet.Stakeholders.HasChildNodes){
            foreach($sh in $termSet.Stakeholders.Stakeholder){
                Log ("Adding Stakeholder with ID: {0}" -f $sh.InnerText) -type $SPSD.LogTypes.Normal 
                $tsTermSet.AddStakeholder($sh.InnerText)
            }
            if($overwrite){
                $tsTermSet.Stakeholders | ? {$_ -notin $termSet.Stakeholders.Stakeholder.InnerText } | %{
                    Log ("Removing Stakeholder with ID: {0}" -f $_) -type $SPSD.LogTypes.Normal 
                    $tsTermSet.DeleteStakeholder($_)
                }
            }
        }
        LogOutdent

        #custom properties
        Set-SPTermSetCustomPropertiesFromXml $tsTermSet $termSet.CustomProperties $overwrite
         
        # committing changes and getting group again to create termsets and terms
        $id = $tsTermSet.Id
        $termGroup.TermStore.CommitAll()
        $tsTermSet = $termGroup.TermSets[$termSet.ID -as [guid]]

        Log "Terms" -type $SPSD.LogTypes.Normal -Indent
        #terms
        Set-SPTermsFromXml $tsTermSet $termSet.Terms $overwrite
        LogOutdent
        # CustomSortOrder
        if($termSet.CustomSortOrder -ne $null -and $overwrite){
            $tsTermSet.CustomSortOrder = $termSet.CustomSortOrder
        }
        LogOutdent
}
function Set-SPTermsFromXml([Microsoft.SharePoint.Taxonomy.TermSet]$tsTermSet, [System.Xml.XmlElement]$terms, [bool]$overwrite){
        if($tsTermSet -eq $null){
            throw "TermSet is null!"
        }
        if($terms -eq $null -or -not($terms.HasChildNodes)){
            return
        }

        foreach($term in $terms.Term){
            Set-SPTermFromXml $tsTermSet $term $overwrite
        }
}
function Set-SPTermFromXml([Microsoft.SharePoint.Taxonomy.TermSet]$tsTermSet, [System.Xml.XmlElement]$term, [bool]$overwrite){

        if($tsTermSet -eq $null){
            throw "TermSet is null!"
        }
        if($term -eq $null){
            return
        }
        LogIndent

        $tsTerm = $null

        # check if term with ID already exists and (re)create.
        if($term.ID -ne $null){
            $tsTerm = $tsTermSet.Terms[$term.ID -as [guid]]
            if($tsTerm -ne $null){
                Log ("Term exists already with ID {0}..." -f $term.ID) -NoNewline -type $SPSD.LogTypes.Normal 
                if($overwrite){
                    Log "Recreating (Overwrite=true)" -type $SPSD.LogTypes.Warning -NoIndent 
                    $tsTerm.Delete()
                    $tsTerm = $tsTermSet.CreateTerm($term.Name,$tsTermSet.TermStore.DefaultLanguage, $term.ID -as [guid])
                }
                else{
                    Log "Merging" -type $SPSD.LogTypes.Success -NoIndent
                }
            }
        }

        # check if TermSet with Name already exists and (re)create.
        if($term.Name -ne $null -and $tsTerm -eq $null){
            $tsTerm = $tsTermSet.Terms[$term.Name]
            if($tsTerm -ne $null){
                Log ("Term exists already with Name {0}..." -f $term.Name) -NoNewline -type $SPSD.LogTypes.Normal 
                if($overwrite){
                    Log "Recreating (Overwrite=true)" -type $SPSD.LogTypes.Warning -NoIndent 
                    $tsTerm.Delete()
                    $tsTerm = $tsTermSet.CreateTerm($term.Name,$tsTermSet.TermStore.DefaultLanguage, $term.ID -as [guid])
                }
                else{
                    Log "Merging" -type $SPSD.LogTypes.Success -NoIndent
                }
            }
        }

        if($tsTerm -eq $null){
            if($term.Name -ne $null){
                # if id is specified use it
                Log ("Term {0}..." -f $term.Name) -NoNewline -type $SPSD.LogTypes.Normal 
                if($term.ID -ne $null){
                   $tsTerm = $tsTermSet.CreateTerm($term.Name, $tsTermSet.TermStore.DefaultLanguage, $term.ID -as [guid])
                }
                else{
                   $tsTerm = $tsTermSet.CreateTerm($term.Name, $tsTermSet.TermStore.DefaultLanguage)
                }
                Log "Created" -type $SPSD.LogTypes.Success -NoIndent
            }
            else{
                Throw "Neither name nor id have been specified for the Term"
            }
        }

        Set-SPTermPropertiesFromXml $tsTerm $term $overwrite

        if($terms -ne $null -and $term.Terms.HasChildNodes){
            foreach($subTerm in $term.Terms.Term){
                Set-SPTermFromXml2 $tsTerm $subTerm $overwrite
            }
        }
        LogOutdent

}
function Set-SPTermPropertiesFromXml([Microsoft.SharePoint.Taxonomy.Term]$tsTerm, [System.Xml.XmlElement]$term, [bool]$overwrite){
        if($tsTerm -eq $null){
            throw "Term is null!"
        }
        if($term -eq $null){
            return
        }

        LogIndent

        # description
        if($term.Description -ne $null -and $overwrite){
            $tsTerm.SetDescription($term.Description.InnerText, $tsTerm.TermStore.DefaultLanguage)
        }

        # IsAvailableForTagging
        if($term.IsAvailableForTagging -ne $null -and $overwrite){
            $tsTerm.IsAvailableForTagging = $term.IsAvailableForTagging -as [bool]
        }

        # IsOpenForTermCreation
        if($term.IsOpenForTermCreation -ne $null -and $overwrite){
            $tsTerm.IsOpenForTermCreation = $term.IsOpenForTermCreation -as [bool]
        }

        # Owner
        if($term.Owner -ne $null -and $overwrite){
            $tsTerm.Owner = $term.Owner
        }

        #labels
        #Set-SPTermLabelsFromXml $tsTerm $term.Labels $overwrite


        #custom properties
        Set-SPTermCustomPropertiesFromXml $tsTerm $term.LocalCustomProperties $true $overwrite
        Set-SPTermCustomPropertiesFromXml $tsTerm $term.CustomProperties $false $overwrite


        # CustomSortOrder
        if($term.CustomSortOrder -ne $null -and $overwrite){
            $tsTerm.CustomSortOrder = $term.CustomSortOrder
        }
        LogOutdent

}
function Set-SPTermFromXml2([Microsoft.SharePoint.Taxonomy.Term]$parentTerm, [System.Xml.XmlElement]$term, [bool]$overwrite){
       
    if($parentTerm -eq $null){
        throw "TermSet is null!"
    }
    if($term -eq $null){
        return
    }

    LogIndent
    $tsTerm = $null

    # check if term with ID already exists and (re)create.
    if($term.ID -ne $null){
        $tsTerm = $parentTerm.Terms[$term.ID -as [guid]]
        if($tsTerm -ne $null){
            Log ("Term exists already with ID {0}..." -f $term.ID) -NoNewline -type $SPSD.LogTypes.Normal 
            if($overwrite){
                Log "Recreating (Overwrite=true)" -type $SPSD.LogTypes.Warning -NoIndent 
                $tsTerm.Delete()
                $tsTerm = $parentTerm.CreateTerm($term.Name,$parentTerm.TermStore.DefaultLanguage, $term.ID -as [guid])
            }
            else{
                Log "Merging" -type $SPSD.LogTypes.Success -NoIndent
            }
        }
    }

    # check if TermSet with Name already exists and (re)create.
    if($term.Name -ne $null -and $tsTerm -eq $null){
        $tsTerm = $parentTerm.Terms[$term.Name]
        if($tsTerm -ne $null){
            Log ("Term exists already with Name {0}..." -f $term.Name) -NoNewline -type $SPSD.LogTypes.Normal 
            if($overwrite){
                Log "Recreating (Overwrite=true)" -type $SPSD.LogTypes.Warning -NoIndent 
                $tsTerm.Delete()
                $tsTerm = $parentTerm.CreateTerm($term.Name,$parentTerm.TermStore.DefaultLanguage, $term.ID -as [guid])
            }
            else{
                Log "Merging" -type $SPSD.LogTypes.Success -NoIndent
            }
        }
    }

    if($tsTerm -eq $null){
        if($term.Name -ne $null){
            # if id is specified use it
            Log ("Term {0}..." -f $term.Name) -NoNewline -type $SPSD.LogTypes.Normal 
            if($term.ID -ne $null){
                $tsTerm = $parentTerm.CreateTerm($term.Name, $parentTerm.TermStore.DefaultLanguage, $term.ID -as [guid])
            }
            else{
                $tsTerm = $parentTerm.CreateTerm($term.Name, $parentTerm.TermStore.DefaultLanguage)
            }
            Log "Created" -type $SPSD.LogTypes.Success -NoIndent
        }
        else{
            Throw "Neither name nor id have been specified for the Term"
        }
    }

    Set-SPTermPropertiesFromXml $tsTerm $term $overwrite

    #terms
    foreach($subTerm in $term.Terms.Term){
        Set-SPTermFromXml2 $tsTerm $subTerm $overwrite
    }
    LogOutdent
}
function Set-SPTermCustomPropertiesFromXml([Microsoft.SharePoint.Taxonomy.Term]$tsTerm, [System.Xml.XmlElement]$properties, [bool]$isLocal, [bool]$overwrite){
    if($tsTerm -eq $null){
        throw "Term is null!"
    }

    if($properties -eq $null -or -not($properties.HasChildNodes)){
        return
    }
    
    foreach($property in $properties.Property){
        if($isLocal){
            $tsTerm.SetLocalCustomProperty($property.Name, $property.InnerText)
        }
        else{
            $tsTerm.SetLocalCustomProperty($property.Name, $property.InnerText)
        }
    }
}
function Set-SPTermSetCustomPropertiesFromXml([Microsoft.SharePoint.Taxonomy.TermSet]$tsTermSet, [System.Xml.XmlElement]$properties, [bool]$overwrite){
    if($tsTermSet -eq $null){
        throw "TermSet is null!"
    }

    if($properties -eq $null -or -not($properties.HasChildNodes)){
        return
    }

    foreach($property in $properties.Property){
        $tsTermSet.SetCustomProperty($property.Name, $property.InnerText)
    }
}
function Set-SPTermLabelsFromXml([Microsoft.SharePoint.Taxonomy.Term]$tsTerm, [System.Xml.XmlElement]$labels, [bool]$overwrite){
    if($tsTerm -eq $null){
        throw "Term is null!"
    }

    if($labels -eq $null -or -not($labels.HasChildNodes)){
        return
    }

    foreach($label in $labels.Label){
        $lbl = $tsTerm.CreateLabel($label.InnerText, $label.Language -as [int], $label.IsDefaultForLanguage -as [bool])
    }
}