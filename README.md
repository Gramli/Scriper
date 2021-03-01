# Scriper
Are you tired from everyday searching application and scripts? Using Scriper you can manage and run all yours scripts or applications from one place.

![Scriper Example](/Images/scriper.png)

## Menu
* [Features](#features)  
* [Download and Install](#download-and-install)  
* [Get Started](#get-started)  
    * [Configuration](#configuration)
    * [Use The Scripts](#sse-the-scripts)
    * [System Tray](#system-tray)
    * [Order of Scripts](#order-of-scripts)      
* [Technologies and Tools](#technologies-and-tools-used)  
* [Planned](#planned)  

## Features
**Actual version supports these file extensions (scripts):**
* Windows Proccess - .bat - [Proccess](https://docs.microsoft.com/en-gb/dotnet/api/system.diagnostics.process?view=netcore-3.1)
* PowerShell - .ps1, .ps2 -  [System.Management.Automation](https://www.nuget.org/packages/Microsoft.PowerShell.SDK/)
* Python - .py - [IronPython](https://github.com/IronLanguages/ironpython2)
* Exe - .exe -  [Proccess](https://docs.microsoft.com/en-gb/dotnet/api/system.diagnostics.process?view=netcore-3.1)
* Linux Shell - .sh -  [Proccess](https://docs.microsoft.com/en-gb/dotnet/api/system.diagnostics.process?view=netcore-3.1)
* Javascript - .js - [Jint](https://github.com/sebastienros/jint)

## Download and Install
### release v1.2
* [Windows](https://github.com/Gramli/Scriper/releases/download/v1.2/ScriperInstaller.exe)
* [Linux](https://github.com/Gramli/Scriper/releases/download/v1.2/linux-x64-netcore3.1.7z)
### release v1.1
* [Windows](https://github.com/Gramli/Scriper/releases/download/v1.1/ScriperInstaller.exe)

## Get Started
All scripts are saved in configuration file. After first start application creates **defaultScriper.config**, where are saved added scripts. You can add script throught UI pressing "Add Script" button (most top left) or edit the config file (in case you understant xml). At start Scriper is looking for config files in Config directory which is in same folder as .exe file. In Config directory you can have more configs and pick which one you want to use, but every config file has to ends with **"Scriper.config"**. 

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
* inSystemTray - allow to add script to system tray context menu and run it
* outputWindow - determines if output window will be opened -> script output will be written there
* TimeSchedules - *not implemented yet*
* FileOuput
  * path - path to script output file -> script output will be written there
* Order - define order of script in collection and UI

### Use The Scripts
#### Javascript
If you want to write to output just call log or logf function, the functions are defined in C# code.
For example:
```js
//example of log function
var someString = 'just some string';
log(someString);
log('just some another string');
//example of logf function - format
var result = 5*10;
logf(result, '5*10={0}');
```

### System Tray
You can hide Scriper using hide button and then run scripts from System Tray context menu like in pictures. If you want to show Scriper window, just click Show in System Tray context menu.

![Scriper Hide](/Images/scriper_hide.png)
![Scriper Edit](/Images/scriper_system_tray.png)
![Scriper Edit](/Images/scriper_edit.png)

### Order of Scripts
By click on script icon you can change order of script -> its moved to up.
![Scriper Hide](/Images/scriper_icon_click.png)


## Technologies and Tools Used
* [Avalonia](https://github.com/AvaloniaUI/Avalonia)
* [IronPython](https://github.com/IronLanguages/ironpython2)
* [SimpleInjector](https://github.com/simpleinjector/SimpleInjector)
* [NLog](https://github.com/NLog/NLog)
* [MessageBox.Avalonia](https://github.com/AvaloniaUtils/MessageBox.Avalonia)
* [NUnit](https://github.com/nunit/nunit)
* [Jint](https://github.com/sebastienros/jint)
* [NSIS](https://nsis.sourceforge.io/Download)
* [Icons8](https://icons8.com)

  
## Planned to v1.3
* TimeSchedule support
* Custom Icon selection

