# Dialog Name
Name "Scriper"
# name the installer
OutFile "ScriperInstaller.exe"
# Dialog Text
DirText "Scriper Installer. It will install all needed files and directories to use scriper."

# define the directory to install to, the desktop in this case as specified  
InstallDir $PROGRAMFILES\Scriper

!include LogicLib.nsh

Page Directory "" "" RemoveExceptConfig
Page InstFiles

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
DeleteRegKey HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\Scriper"
Delete $INSTDIR\*.*
 
# Delete the directory
RMDir /r $INSTDIR
SectionEnd