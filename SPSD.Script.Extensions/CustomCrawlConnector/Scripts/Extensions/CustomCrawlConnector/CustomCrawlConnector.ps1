###############################################################################
# SharePoint Solution Deployer (SPSD)
# CustomCrawlConnector Extension
# Registers a custom crawl connector for SharePoint search
# Version          : 15.0.0.1
# Creator          : Bernd Rickenberg
###############################################################################

function Add-CustomCrawlConnector($parameters, [System.Xml.XmlElement]$data, [string]$extId, [string]$extensionPath) {
	if ($parameters -eq $null) {
		Log -message "No parameters defined. Cannot create custom crawl connector." -type $SPSD.LogTypes.Error
		return 
	}

	$name = $parameters["ProtocolName"]
	$searchAppName = $parameters["SearchServiceApplicationName"]
	$modelFilePath = $parameters["ModelFilePath"]
	$categoryName = $parameters["CategoryName"]
	$propsetId = $parameters["PropsetId"]

	Log -message "Parameters:" -indent -type $SPSD.LogTypes.Normal
		Log -message ("ProtocolName: "+ $name) -type $SPSD.LogTypes.Normal
		Log -message ("Search Service Application Name: "+ $searchAppName) -type $SPSD.LogTypes.Normal
		Log -message ("Model file path: "+ $modelFilePath) -type $SPSD.LogTypes.Normal
		Log -message ("Category name: "+ $categoryName) -type $SPSD.LogTypes.Normal
		Log -message ("Propset ID: "+ $propsetId) -type $SPSD.LogTypes.Normal
	LogOutdent

	# Add protocol handler to registry
	$protocolHandler = Get-ItemProperty -Path "HKLM:\SOFTWARE\Microsoft\Office Server\15.0\Search\Setup\ProtocolHandlers" -Name $name -ErrorAction SilentlyContinue
	if ($protocolHandler -eq $null) {
		New-ItemProperty -Path "HKLM:\SOFTWARE\Microsoft\Office Server\15.0\Search\Setup\ProtocolHandlers" -Name $name -PropertyType String -Value "OSearch15.ConnectorProtocolHandler.1"
		Log -message "Protocol handler '$name' created." -type $SPSD.LogTypes.Success
	} 
	else {
		Log -message "Protocol handler '$name' already registered." -type $SPSD.LogTypes.Warning
	}

	# Register the custom indexing connector
	$searchApp = Get-SPEnterpriseSearchServiceApplication -Identity $searchAppName
	$connector = Get-SPEnterpriseSearchCrawlCustomConnector -SearchApplication $searchApp -Protocol $name
	# Ok, weird stuff - if the crawl connector is not registered (does not exist) an object with empty properties is returned
	if ($connector.DisplayName -eq "") {
		New-SPEnterpriseSearchCrawlCustomConnector -SearchApplication $searchApp -protocol $name -ModelFilePath $modelFilePath -Name $name
		Log -message "Custom crawl connector '$name' created." -type $SPSD.LogTypes.Success
	}
	else {
		Log -message "Custom crawl connector '$name' exists already." -type $SPSD.LogTypes.Warning
	}

	# Create and configure the metadata category (for the crawled properties exposed by the connector)
	$category = Get-SPEnterpriseSearchMetadataCategory -Identity $categoryName -SearchApplication $searchApp
	if ($category -eq $null) {
		New-SPEnterpriseSearchMetadataCategory -Name $categoryName -Propset $propsetId -searchApplication $searchApp -DiscoverNewProperties $true
		Log -message "Metadata category '$categoryName' created." -type $SPSD.LogTypes.Success
	}
	else {
		Log -message "Metadata category '$categoryName' exists already." -type $SPSD.LogTypes.Warning
	}
}
