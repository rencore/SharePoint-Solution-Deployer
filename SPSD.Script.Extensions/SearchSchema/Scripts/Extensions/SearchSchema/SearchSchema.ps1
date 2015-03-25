###############################################################################
# SharePoint Solution Deployer (SPSD)
# SearchSchema Extension
# This extension creates crawled properties, managed properties and mappings between the two.
# Version          : 15.0.0.0
# Creator          : Bernd Rickenberg
###############################################################################

function Deploy-SearchSchema($parameters, [System.Xml.XmlElement]$data, [string]$extId, [string]$extensionPath) {
	if ($parameters -eq $null) {
		Log -message "No parameters defined. Cannot deploy search schema." -type $SPSD.LogTypes.Error
		return 
	}

	if ($data -eq $null) {
		Log -message "No crawled/managed properties defined." -type $SPSD.LogTypes.Warning
		return;
	}

	$searchAppName = $parameters["SearchServiceApplicationName"]
	$category = $parameters["CategoryName"]
	$propSet = $parameters["PropsetId"]

	Log -message "Parameters:" -indent -type $SPSD.LogTypes.Normal
		Log -message ("SearchServiceApplicationName: "+ $searchAppName) -type $SPSD.LogTypes.Normal
		Log -message ("CategoryName: "+ $category) -type $SPSD.LogTypes.Normal
		Log -message ("PropsetId: "+ $propSet) -type $SPSD.LogTypes.Normal
	LogOutdent

	$searchApp = Get-SPEnterpriseSearchServiceApplication -Identity $searchAppName

	foreach($prop in $data.SelectNodes("//AddCrawledProperty")) {
		if ($prop.Searchable -ne $null) { $isSearchable = [System.Convert]::ToBoolean($prop.Searchable) }

		AddCrawledProperty -searchServiceApplication $searchApp -category $category -propSet $propSet -name $prop.Name -searchable $isSearchable
	}

	foreach($prop in $data.SelectNodes("//AddManagedProperty")) {
		if ($prop.Searchable -ne $null) { $isSearchable = [System.Convert]::ToBoolean($prop.Searchable) }
		if ($prop.Queryable -ne $null) { $isQueryable = [System.Convert]::ToBoolean($prop.Queryable) }
		if ($prop.Retrievable -ne $null) { $isRetrievable = [System.Convert]::ToBoolean($prop.Retrievable) }
		if ($prop.Refinable -ne $null) { $isRefinable = [System.Convert]::ToBoolean($prop.Refinable) }
		if ($prop.Sortable -ne $null) { $isSortable= [System.Convert]::ToBoolean($prop.Sortable) }
		if ($prop.HasMultipleValues -ne $null) { $hasMultipleValues = [System.Convert]::ToBoolean($prop.HasMultipleValues) }
		if ($prop.Safe -ne $null) { $isSafe = [System.Convert]::ToBoolean($prop.Safe) }
		if ($prop.WeightGroup -eq $null) { $weightGroup = 0 } else { $weightGroup = $prop.WeightGroup}
	
		AddManagedProperty -searchServiceApplication $searchApp `
			-name $prop.Name `
			-type $prop.Type `
			-searchable $isSearchable `
			-queryable $isQueryable `
			-retrievable $isRetrievable `
			-refinable $isRefinable `
			-sortable $isSortable `
			-hasMultipleValues $hasMultipleValues `
			-safe $isSafe `
			-weightGroup $weightGroup
	}

	foreach($prop in $data.SelectNodes("//MapToManagedProperty")) {
		MapToManagedProperty -searchServiceApplication $searchApp -propSet $propSet -crawledPropertyName $prop.CrawledPropertyName -managedPropertyName $prop.ManagedPropertyName
	}
}

function AddCrawledProperty($searchServiceApplication, $category, $propSet, $name, $searchable) {
    # Check if crawled property exists. If not, create it.
    $crawledproperty = Get-SPEnterpriseSearchMetadataCrawledProperty -Name $name -SearchApplication $searchServiceApplication
    if (!$crawledproperty)
    {
		Log -message "Creating crawled property $name." -type $SPSD.LogTypes.Success
        # Note: VariantType is obselete, so we set it to 0
        # Note: searchable adds the property to the full text search without a managed property
        New-SPEnterpriseSearchMetadataCrawledProperty -SearchApplication $searchServiceApplication -Category $category -PropSet $propSet -Name $name -IsNameEnum $false -VariantType 0 -IsMappedToContents $searchable
    }
    else {
		Log -message "Crawled property $name already exists. Skipping." -type $SPSD.LogTypes.Warning
    }
}

function AddManagedProperty($searchServiceApplication, $name, $type, $searchable, $queryable, $retrievable, $refineable, $sortable, $hasMultipleValues, $safe, $weightGroup) {
    $managedproperty = Get-SPEnterpriseSearchMetadataManagedProperty -Identity $name -SearchApplication $searchServiceApplication -ErrorAction:SilentlyContinue
    if ($managedproperty -eq $null)
    {
		Log -message "Creating managed property $name" -type $SPSD.LogTypes.Success
        $managedproperty = New-SPEnterpriseSearchMetadataManagedProperty -Name $name -SearchApplication $searchServiceApplication -Type $type -FullTextQueriable $searchable -Queryable $queryable -Retrievable $retrievable -EnabledForScoping $true -NameNormalized $true -Safe $safe -NoWordBreaker $false 
        $managedproperty.Refinable = $refinable
        $managedproperty.Sortable = $sortable
        $managedproperty.HasMultipleValues = $hasMultipleValues
		$managedproperty.Context = $weightGroup
        $managedproperty.Update()
    }
    else {
		Log -message "Managed property $name already exists. Skipping." -type $SPSD.LogTypes.Warning
    }
}

function MapToManagedProperty($searchServiceApplication, $propSet, $crawledPropertyName, $managedPropertyName) {
	$managedproperty = Get-SPEnterpriseSearchMetadataManagedProperty -Identity $managedPropertyName -SearchApplication $searchServiceApplication -ErrorAction:SilentlyContinue
	$crawledproperty = Get-SPEnterpriseSearchMetadataCrawledProperty -Name $crawledPropertyName -PropSet $propSet -SearchApplication $searchServiceApplication -ErrorAction:SilentlyContinue

	if ($managedproperty -eq $null) {
		Log -message "Managed property $managedPropertyName not found. Skipping." -type $SPSD.LogTypes.Error
		return
	}

	if ($crawledproperty -eq $null) {
		Log -message "Crawled property $crawledPropertyName not found. Skipping." -type $SPSD.LogTypes.Error
		return
	}

	$mapping = Get-SPEnterpriseSearchMetadataMapping -SearchApplication $searchServiceApplication -ManagedProperty $managedproperty -CrawledProperty $crawledproperty

	if ($mapping -eq $null)
	{
		Log -message "Mapping from $crawledPropertyName to $managedPropertyName." -type $SPSD.LogTypes.Success
		New-SPEnterpriseSearchMetadataMapping -SearchApplication $searchServiceApplication -ManagedProperty $managedproperty -CrawledProperty $crawledproperty > Out-Null
	}
	else
	{
		Log -message " Mapping from $crawledPropertyName to $managedPropertyName already exists. Skipping." -type $SPSD.LogTypes.Warning
	}
}

