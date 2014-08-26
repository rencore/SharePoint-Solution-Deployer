###############################################################################
# Export a managed Metadata Termstore to XML including custom properties
# Url              : 
# Creator          : Matthias Einig, www.matthiaseinig.de
###############################################################################
 

Add-PSSnapin "Microsoft.SharePoint.PowerShell" -ErrorAction SilentlyContinue

function Export-SPTermStoreXml([Microsoft.SharePoint.Taxonomy.TermStore]$termstore){
  
    $ts = $termstore
    $xml = "<?xml version=`"1.0`" encoding=`"utf-8`" ?>"
    $xml = "<TermStore Name=`"{0}`" WorkingLanguage=`"{1}`" DefaultLanguage=`"{2}`">" -f $ts.Name, $ts.WorkingLanguage, $ts.DefaultLanguage
    
    #groups
    if($ts.Groups -ne $null -and $ts.Groups.Count -gt 0){
        $xml = $xml + "<Groups>"
        foreach($group in $ts.Groups){
            $xml =  $xml + (Get-SPTermStoreGroupXml $group)
        }
        $xml = $xml + "</Groups>"
    }
    #languages
    if($ts.Languages -ne $null -and $ts.Languages.Count -gt 0){
        $xml = $xml + "<Languages>"
        foreach($language in $ts.Languages){
            $xml = $xml + "<Language>{0}</Language>" -f $language
        }
        $xml = $xml + "</Languages>"    
    }
    #administrators
    if($ts.TermStoreAdministrators -ne $null -and $ts.TermStoreAdministrators.Count -gt 0){
        $xml = $xml + "<TermStoreAdministrators>"
        foreach($tsa in $ts.TermStoreAdministrators){
            $xml = $xml + "<TermStoreAdministrator DenyRightsMask=`"{1}`" GrantRightsMask=`"{2}`">{0}</TermStoreAdministrator>" -f $tsa.PrincipalName, $tsa.DenyRightsMask, $tsa.GrantRightsMask
        }
        $xml = $xml + "</TermStoreAdministrators>" 
    }
    $xml = $xml + "</TermStore>"
    return $xml
}

function Get-SPTermStoreGroupXml([Microsoft.SharePoint.Taxonomy.Group]$group){


        $xml = "<Group Name=`"{0}`" ID=`"{1}`">" -f $group.Name, $group.Id

        $xml = $xml + "<Description>{0}</Description>" -f $group.Description

        #managers
        if($group.GroupManagers -ne $null -and $group.GroupManagers.Count -gt 0){
            $xml = $xml + "<GroupManagers>"
            foreach($mgm in $group.GroupManagers){
                $xml = $xml + "<GroupManager>{0}</GroupManager>" -f $mgm.PrincipalName
            }
            $xml = $xml + "</GroupManagers>" 
        }
        #contributors
        if($group.Contributors -ne $null -and $group.Contributors.Count -gt 0){
            $xml = $xml + "<Contributors>"
            foreach($contr in $group.Contributors){
               $xml = $xml + "<Contributor>{0}</Contributor>" -f $contr.PrincipalName
            }
            $xml = $xml + "</Contributors>" 
        }
        #termsets
        if($group.TermSets -ne $null -and $group.TermSets.Count -gt 0){
            $xml = $xml + "<TermSets>"
            foreach($ts in $group.TermSets){
                $xml =  $xml + (Get-SPTermSetXml $ts)
            }
            $xml = $xml + "`n</TermSets>"        
        }
        $xml = $xml + "`n</Group>"
    return $xml
}
function Get-SPTermSetXml([Microsoft.SharePoint.Taxonomy.TermSet]$termSet){
        $xml = "<TermSet Name=`"{0}`" ID=`"{1}`" IsAvailableForTagging=`"{2}`" IsOpenForTermCreation=`"{3}`" Owner=`"{4}`" Contact=`"{5}`" CustomSortOrder=`"{6}`">" -f $termSet.Name, $termSet.Id, $termSet.IsAvailableForTagging, $termSet.IsOpenForTermCreation, $termSet.Owner, $termSet.Contact, $termSet.CustomSortOrder
        $xml = $xml + "<Description>{0}</Description>" -f $termSet.Description

        #stakeholders
        if($termSet.Stakeholders -ne $null -and $termSet.Stakeholders.Count -gt 0){
            $xml = $xml + "<Stakeholders>"
            foreach($sh in $termSet.Stakeholders){
                $xml = $xml + "<Stakeholder>{0}</Stakeholder>" -f $sh
            }
            $xml = $xml + "</Stakeholders>" 
        }
        #custom properties
        $xml = $xml + (Get-SPTermCustomPropertiesXml $termSet.CustomProperties)

        #terms
        $xml = $xml + (Get-SPTermsXml $termSet.Terms)

        $xml = $xml + "`n</TermSet>"
        return $xml
}

function Get-SPTermsXml([Microsoft.SharePoint.Taxonomy.TermCollection]$terms){
        if($terms -eq $null -or $terms.Count -eq 0){
            return ""
        }

        $xml = "<Terms>"
        foreach($term in $terms){
            $xml = $xml + (Get-SPTermXml $term)
        }
        $xml = $xml + "`n</Terms>"
        return $xml
}


function Get-SPTermXml([Microsoft.SharePoint.Taxonomy.Term]$term){
        if($term -eq $null){
            return ""
        }

        $xml = "<Term Name=`"{0}`"  ID=`"{1}`" IsAvailableForTagging=`"{2}`" IsOpenForTermCreation=`"{3}`" `Owner=`"{4}`" CustomSortOrder=`"{5}`">" -f $term.Name, $term.Id, $term.IsAvailableForTagging, $term.IsOpenForTermCreation, $term.Owner, $term.CustomSortOrder

        # add multi language support
        $xml = $xml + "<Description>{0}</Description>" -f $term.GetDescription()

        #labels
        $xml = $xml + (Get-SPTermLabelsXml $term.Labels)


        #custom properties
        $xml = $xml + (Get-SPTermCustomPropertiesXml $term.LocalCustomProperties "LocalCustomProperties")
        $xml = $xml + (Get-SPTermCustomPropertiesXml $term.CustomProperties "CustomProperties")

        #terms
        $xml = $xml + (Get-SPTermsXml $term.Terms)
        
        $xml = $xml + "</Term>" 
        return $xml
}


function Get-SPTermCustomPropertiesXml([Microsoft.SharePoint.Taxonomy.Generic.ReadOnlyDictionary`2[string,string]]$dict, [string]$enclosingTagname)
{
    if(-not($enclosingTagname) -or $enclosingTagname.Trim().Length -eq 0){
        $enclosingTagname = "CustomProperties"
    }
    if($dict -eq $null -or $dict.Count -eq 0 ){
        return "";
    }

    $xml = "<{0}>" -f $enclosingTagname
        foreach($key in $dict.Keys){
            $xml = $xml + "<Property Name=`"{0}`">{1}</Property>" -f $key, $dict[$key]
        }
    $xml = $xml + "</{0}>" -f $enclosingTagname 
    return $xml
}

function Get-SPTermLabelsXml([Microsoft.SharePoint.Taxonomy.LabelCollection]$labels)
{
        if($labels -eq $null -or $labels.Count -eq 0){
            return ""
        }

        $xml = "<Labels>"
        foreach($label in $labels){
            $xml = $xml + "<Label Language=`"{0}`" IsDefaultForLanguage=`"{1}`">{2}</Label>" -f $label.Language, $label.IsDefaultForLanguage, $label.Value
        }
        $xml = $xml + "</Labels>"
        return $xml
}

function Save-Xml ([string]$xml, [string]$file) 
{ 

    $xmlout = [xml]$xml
    $xmlout.Save($file)
}


