@ECHO OFF
SETLOCAL
cls
REM Batchfile inspired by the launchscript of http://autospinstaller.codeplex.com

SET command=Retract
SET verbosity=Verbose
SET type=All
SET saveEnvXml=-saveEnvXml

@TITLE -- SharePoint Solution Deployer --
FOR /F "tokens=2-4 delims=/ " %%i IN ('date /t') DO SET SHORTDATE=%%i-%%j-%%k
FOR /F "tokens=1-3 delims=: " %%i IN ('time /t') DO SET SHORTTIME=%%i-%%j%%k
SET LaunchedFromBAT=1

echo."%~dp0"| findstr /C:" " 1>nul

if errorlevel 1 (
  GOTO INPUT
) ELSE (
  COLOR 0C
  ECHO.
  ECHO.Please run SPSD from a path without spaces.
  ECHO.
  PAUSE
  EXIT
)

:INPUT
SET envFile=
IF "%1"=="" GOTO START
IF EXIST "%~dp0\%1" SET envFile= -envFile %~dp0\%1
IF EXIST "%1" SET envFile= -envFile %1
ECHO - Specified Environment File: %1
:START
:: Check for Powershell
IF NOT EXIST "%SYSTEMROOT%\system32\windowspowershell\v1.0\powershell.exe" (
    COLOR 0C
    ECHO - "powershell.exe" not found!
    ECHO - This script requires PowerShell - install v2.0/3.0, then re-run this script.
    COLOR
    pause
    EXIT
    )
:: Check for Powershell v2.0 (minimum)
ECHO - Checking for Powershell 2.0 (minimum)...
"%SYSTEMROOT%\system32\windowspowershell\v1.0\powershell.exe" $host.Version.Major | find "1" >nul
IF %ERRORLEVEL% == 0 (
    COLOR 0C
    ECHO - This script requires a minimum PowerShell version of 2.0!
    ECHO - Please uninstall v1.0, install v2.0/3.0, then re-run this script.
    COLOR
    pause
    EXIT
    )
ECHO - OK.
:: Check Installed SharePoint version
IF EXIST "%ProgramW6432%\Common Files\microsoft shared\Web Server Extensions\15\ISAPI\Microsoft.SharePoint.dll" (
	SET SharePointVersion=15
	SET PSVersion=3
) ELSE IF EXIST "%ProgramW6432%\Common Files\microsoft shared\Web Server Extensions\14\ISAPI\Microsoft.SharePoint.dll" (
	SET SharePointVersion=14
	SET PSVersion=2
) ELSE (
	ECHO "SharePoint 2010/2013 is not found in the default installation directory"
	EXIT
)
:: Get existing Powershell ExecutionPolicy
FOR /F "tokens=*" %%x in ('"%SYSTEMROOT%\system32\windowspowershell\v1.0\powershell.exe" Get-ExecutionPolicy') do (set ExecutionPolicy=%%x)
:: Set Bypass, in case we are running over a net share or UNC
IF NOT "%ExecutionPolicy%"=="Bypass" IF NOT "%ExecutionPolicy%"=="Unrestricted" (
	ECHO - PS ExecutionPolicy is %ExecutionPolicy%, setting ExecutionPolicy to Bypass.
	"%SYSTEMROOT%\system32\windowspowershell\v1.0\powershell.exe" -Command Start-Process "$PSHOME\powershell.exe" -Verb RunAs -ArgumentList "'-Command Set-ExecutionPolicy Bypass'"
	)
GOTO LAUNCHSCRIPT
:LAUNCHSCRIPT
"%SYSTEMROOT%\system32\windowspowershell\v1.0\powershell.exe" -Command Start-Process "$PSHOME\powershell.exe" -Verb RunAs -ArgumentList '-Version %PSVersion%  "%~dp0\Scripts\SPSD_Main.ps1 -Command %command% -Type %type% -Verbosity %verbosity% %saveEnvXml% %envFile%"'
ENDLOCAL