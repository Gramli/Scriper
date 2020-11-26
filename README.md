# Scriper
Are you tired from everyday searching application and scripts? Using Scriper you can manage and run all yours scripts or applications from one place.

![Scriper Example](/Images/scriper.png)

## Menu
[Features](#features)
[Download](#download)
[Get Started](#get-started)
[Technologies and Tools](#technologies-and-tools)
[Planned](#planned)

## Features

Actual version supports:

* Windows Proccess - .bat - [Proccess](https://docs.microsoft.com/en-gb/dotnet/api/system.diagnostics.process?view=netcore-3.1)
* PowerShell - .ps1, .ps2 -  [System.Management.Automation](https://www.nuget.org/packages/Microsoft.PowerShell.SDK/)
* Python - .py - [IronPython](https://github.com/IronLanguages/ironpython2)
* Exe - .exe -  [Proccess](https://docs.microsoft.com/en-gb/dotnet/api/system.diagnostics.process?view=netcore-3.1)
* Linux Shell - .sh -  [Proccess](https://docs.microsoft.com/en-gb/dotnet/api/system.diagnostics.process?view=netcore-3.1)
* Javascript - .js - [Jint](https://github.com/sebastienros/jint)

## Download
### pre-release v1
* [Windows]()
* [Binaries](https://github.com/Gramli/Scriper/releases/download/v1.0/pre-release_v1.0.zip)

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
* inSystemTray - *not implemented yet - blocked because of AvaloniaUI*
* outputWindow - determines if output window will be opened -> script output will be written there
* TimeSchedules - *not implemented yet*
* FileOuput
  * path - path to script output file -> script output will be written there

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



## Technologies and Tools Used
* [Avalonia](https://github.com/AvaloniaUI/Avalonia)
* [IronPython](https://github.com/IronLanguages/ironpython2)
* [SimpleInjector](https://github.com/simpleinjector/SimpleInjector)
* [NLog](https://github.com/NLog/NLog)
* [MessageBox.Avalonia](https://github.com/AvaloniaUtils/MessageBox.Avalonia)
* [NUnit](https://github.com/nunit/nunit)
* [Jint](https://github.com/sebastienros/jint)

  
## Planned
* TimeSchedule support

