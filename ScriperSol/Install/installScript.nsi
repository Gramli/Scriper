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
 
# define what to install and place it in the output path
File /r \
/x "cs" \
/x "de" \
/x "es" \
/x "fr" \
/x "it" \
/x "ja" \
/x "ko" \
/x "pl" \
/x "de" \
/x "pt-BR" \
/x "ru" \
/x "tr" \
/x "zh-Hans" \
/x "zh-Hant" \
/x "*.pdb" \
"E:\GitHub\Scriper\ScriperSol\Scriper\bin\Release\netcoreapp3.1\*.*"
 
# default section end
SectionEnd
 
# create a section to define what the uninstaller does.
# the section will always be named "Uninstall"
Section "Uninstall"
 
# Always delete uninstaller first
Delete $INSTDIR\ScriperUninstaller.exe
Delete /r $INSTDIR\*.*
 
# Delete the directory
RMDir $INSTDIR
SectionEnd