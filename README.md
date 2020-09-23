# Scriper
Using Scriper you can manage and run all yours scripts or applications from one place.

Actual version supports:

* Windows Proccess - .bat
* PowerShell - .ps1, .ps2
* Pyhton - .py
* Exe - .exe

##Get Started
All scripts are saved in configuration file. After first start application creates **defaultScriper.config**, where are saved added scripts. You can add script throught UI or edit the config file. At start Scriper looking for config files which ends with **"Scriper.config"** s o you can have more configs and pick at start which you want to use.

### Configuration

Mandatory attributes are **name** and **path**, name has to be unique, rest of attributes and elements are optional. See example below:

```xml
    <Script name="allDirectoriesinPath" 
            description="Write all directories and files in argument dir" 
            path="allDirectoriesinPath.bat"   
            arguments="E:\GitHub\Scriper\ScriperSol\Scriper\bin\Debug\netcoreapp3.1" 
            inSystemTray="true" 
            outputWindow="true">
      <TimeSchedules />
      <FileOuput path="log.txt" />
    </Script>
```
#### Attributes and Elements
* name - script name
* description - script description
* path - script file path
* arguments - script arguments
* inSystemTray - *not implemented yet - blocked because of AvaloniaUI*
* outputWindow - determines if output window will be opened -> script output will be written there
* TimeSchedules - *not implemented yet*
* FileOuput
  * path - path to script output file -> script output will be written there
