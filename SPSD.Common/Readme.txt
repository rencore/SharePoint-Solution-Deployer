###############################################################################
# SharePoint Solution Deployer (SPSD)
# Version          : 5.0.4.6440
# Url              : http://spsd.codeplex.com
# Creator          : Matthias Einig, RENCORE AB, http://twitter.com/mattein
# License          : MS-PL
# File			   : Readme.txt
###############################################################################

Warning: Leave all script files starting with SPSD_ in the Scripts folder untouched in order to be able to easily update to the next version of the scripts.

### Environment files
SPSD is configured with environment XML files located in the /Environments folder

How to create environments:

1.Modify the 'default.xml' environment definition XML in the "Environments" folder
2.Create your own environment file in the "Environments" folder 
  - If you name the file after a machine it will be used when the script runs on that machine, 
  - If you name it after a user name, it will be used when the script runs by this user,
  - If none of the above exist, default.xml is used
3.Extract any XML node of a environment file to a separate file and specify the ID and FilePath tags. 
  SPSD will automatically merge the files together. 
  This allows you to reuse parts of the configuration for different deployment environments.
  The referenced file should be place in the environments folder.
4.Specify variables in the environment definition file, which will be replaced on runtime 
  You can also use system environment variables (run "Get-ChildItem env:" in PowerShell to see all available variables)

### Environment Editor
To edit the XML files you can use the SPSD Environment Editor located which can be downloaded from http://spsd.codeplex.com/releases/view/100340. 
Comments in the XML files will be lost when saving with the Environment Editor.

Currently the environment editor does not yet support 
Configuration
 - Compatibility level
 - Require Languages
Extensions

### Scripts/AppLogo.txt
If you want to embed your own ASCII art logo or application info, please customize the file Scripts/AppLogo.txt.
Links:
- http://patorjk.com/software/taag/#p=display&h=0&v=0&f=Doh&t=SPSD
- http://picascii.com/

### Scripts/CustomTargets.ps1
	The custom targets PS file can be used to perform custom actions at certain events during the deployment process
	The available Events are (in order how they are called): 
		- RunCustomPrerequisites
		- CheckCustomPreconditions
		- Initialize
		- BeforeDeploy/BeforeRetract/BeforeUpdate
		- AfterDeploy/AfterRetract/AfterUpdate
		- SkipSolution
		- Finalize

### Scripts/Extensions and Environments/Extensions
	SPSD has an extension system which allows you to develop re-usable extensions which can be just dropped and registered in any SPSD deployment package.
	This makes it easy for to inject common tasks into the deployment process , 
	eg. creating a ContentTypeHub, creating a site structure, importing managed meta data etc.

	Please have a look at the example extension in the extensions folder to understand how it works.

	You can publish your extensions to the Microsoft Technet Gallery an tag it with "SPSD" to be found through this link
	http://gallery.technet.microsoft.com/site/search?f%5B0%5D.Type=Tag&f%5B0%5D.Value=SPSD&f%5B0%5D.Text=SPSD

### Logging:
	 You can (and should) use the SPSD Logging function to write to the PowerShell window/log file 
	 instead of using Write-Host, Write-Output, Write Error
 
	 Using the log method provided by SPSD will help you to assure that 
	 - all messages fit well into the rest of the log
	 - messages are still logged even if SPSD is run from a custom PS Host
	 - messages of remoted PS sessions are logged
 
	 Example:
		  Log "My custom log message" -Type $SPSD.LogTypes.Normal -Indent
		  ...your commands...
		  LogOutdent
 
	 Valid parameters for the "Log" function are:
	 -Message (or first parameter) [string]
		The message which should be logged
	 -Type [int]
		Values:
		0 or $SPSD.LogTypes.Success     -> Green
		1 or $SPSD.LogTypes.Error       -> Red
		2 or $SPSD.LogTypes.Warning     -> Yellow
		3 or $SPSD.LogTypes.Information -> White
		4 or $SPSD.LogTypes.Normal      -> Gray
	 -NoNewline [switch]
	   Will not add a line break after the message, e.g. for progress checks or if you have to change the log type for the rest of the line
	 -Indent [switch]
 		Indents all following logging messages by 2 characters (adds to previous indentation)
	 -Outdent [switch]
 		Outdents all following logging messages by 2 characters (subtracts from previous indentation)
	 -NoIndent [switch]
 		Disregards current indentation, i.e. when the previous log message had -NoNewline set

	 Additionally you can use the commands 
	   LogIndent
	   LogOutdent
	 to adjust the current indentation level without printing a log message
