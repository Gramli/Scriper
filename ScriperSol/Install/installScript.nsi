# Dialog Name
Name "Scriper"
# name the installer
OutFile "ScriperInstaller.exe"
# Dialog Text
DirText "Scriper Installer. It will install all needed files and directories to use scriper."

# define the directory to install to, the desktop in this case as specified  
InstallDir $PROGRAMFILES\Scriper

!include LogicLib.nsh
!include "MUI.nsh"
!include nsdialogs.nsh

RequestExecutionLevel admin

Page Directory "" "" RemoveExceptConfig
Page Custom RunAtStartPage RunAtStartPageLeave "Windows StartUp"
Page InstFiles
 
var runAtStartState

Function RunAtStartPage
nsDialogs::Create 1018
${NSD_CreateLabel} 4 0 100% 12u "Do you want to run Scriper at Windows startup?"
${NSD_CreateCheckbox} 8 20 100u 15u "Run at Windows startup."
Pop $runAtStartState
nsDialogs::Show
FunctionEnd
 
Function RunAtStartPageLeave
${NSD_GetState} $runAtStartState $0
	${If} $0 == 1
		CreateShortCut "$SMPROGRAMS\Startup\Scriper.lnk" "$INSTDIR\Scriper.exe"
	${EndIf}
FunctionEnd

Function RemoveExceptConfig
${If} ${FileExists} "$InstDir\*"
    MessageBox MB_YESNO `"$InstDir" already exists, ScriperInstaller will delete this folder (except Config\*) and continue installing?` IDYES yep
    Abort
yep:
	CopyFiles $INSTDIR\Config\*.* $TEMP\Config
	RMDir /R $INSTDIR # Remembering, of course, that you should do this with care
	CreateDirectory $INSTDIR\Config
	CopyFiles $TEMP\Config\*.* $INSTDIR\Config
${EndIf}
FunctionEnd

# default section start; every NSIS script has at least one section.
Section "Install"

# define the output path for this file
SetOutPath $INSTDIR

# create the uninstaller
WriteUninstaller "$INSTDIR\ScriperUninstaller.exe"

CreateShortCut "$DESKTOP\Scriper.lnk" "$INSTDIR\Scriper.exe"
CreateShortCut "$SMPROGRAMS\Scriper.lnk" "$INSTDIR\Scriper.exe"

WriteRegStr HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\Scriper" \
                 "DisplayName" "Scriper -- Application for managing scripts"
WriteRegStr HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\Scriper" \
                 "UninstallString" "$\"$INSTDIR\ScriperUninstaller.exe$\""
 
# define what to install and place it in the output path
SetOverwrite off
File /r \
/x "*.pdb" \
"E:\GitHub\Scriper\ScriperSol\Scriper\bin\Release\netcoreapp3.1\*.*"
 
# default section end
SectionEnd
 
# create a section to define what the uninstaller does.
# the section will always be named "Uninstall"
Section "Uninstall"
 
# Always delete uninstaller first
Delete $INSTDIR\ScriperUninstaller.exe
Delete "$DESKTOP\Scriper.lnk"
Delete "$SMPROGRAMS\Scriper.lnk"
Delete "$SMPROGRAMS\Startup\Scriper.lnk"
DeleteRegKey HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\Scriper"
Exec "$INSTDIR\Scriper.exe -un"
Delete $INSTDIR\*.*
 
# Delete the directory
RMDir /r $INSTDIR
SectionEnd