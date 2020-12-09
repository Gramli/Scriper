# Dialog Name
Name "Scriper"
# name the installer
OutFile "ScriperInstaller.exe"
# Dialog Text
DirText "Scriper Installer. It will install all needed files and directories to use scriper."

# define the directory to install to, the desktop in this case as specified  
InstallDir $PROGRAMFILES\Scriper

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
File /r \
/x "*.pdb" \
"E:\GitHub\Scriper\ScriperSol\Scriper\bin\Release\netcoreapp3.1\*.*"

# CopyFiles "E:\GitHub\Scriper\ScriperSol\Install\defaultScriper.config" "$INSTDIR\Config"
 
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