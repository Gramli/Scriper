[![Gitter](https://badges.gitter.im/ScriperApp/community.svg)](https://gitter.im/ScriperApp/community?utm_source=badge&utm_medium=badge&utm_campaign=pr-badge)
[![CodeFactor](https://www.codefactor.io/repository/github/gramli/scriper/badge/master)](https://www.codefactor.io/repository/github/gramli/scriper/overview/master)
[![Unit Tests](https://github.com/Gramli/Scriper/actions/workflows/dotnet.yml/badge.svg?branch=master)](https://github.com/Gramli/Scriper/actions/workflows/dotnet.yml)
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://github.com/Gramli/Scriper/blob/master/LICENSE.md)
![GitHub repo size](https://img.shields.io/github/repo-size/Gramli/Scriper)

# Scriper

![Scriper Example](/Images/scriper.png)

Are you tired from everyday searching application and scripts? Using Scriper you can manage and run all yours scripts or applications from one place.
Scriper is capable to **run js/python/powershell/windows process scripts or .exe applications**. You can for example schedule script/app run through simple UI or hide Scriper into Tray Menu and run script/app from it.    

When you do **something repetitive what can make a script**, Scriper is totally for you. Write script, add it to Scriper and simply run it as you need.


As a developer I use Scriper for example to open visual studio solutions as admin, kill all running processes by name, open .exe applications with specified arguments, register libs to GAC etc. and **all of it I run from tray menu**.

## Menu
* [Features](#features) - what Scriper can do
* [Wiki](https://github.com/Gramli/Scriper/wiki) - all needed information should be there like configuration, adding/ordering scripts, set arguments, time schedule etc..    
* [Download and Install](#download-and-install) - fast links to releases   
* [Technologies and Tools](#technologies-and-tools-used) - links to used frameworks and libraries 

## Features
- **Task Scheduler**  
- **Windows System Tray**
- **Passing Arguments to Scripts**  
- **Actual version supports these file extensions (scripts):**
  * Windows Proccess - .bat - [Proccess](https://docs.microsoft.com/en-gb/dotnet/api/system.diagnostics.process?view=netcore-3.1)
  * PowerShell - .ps1, .ps2 -  [System.Management.Automation](https://www.nuget.org/packages/Microsoft.PowerShell.SDK/)
  * Python - .py - [IronPython](https://github.com/IronLanguages/ironpython2)
  * Exe - .exe -  [Proccess](https://docs.microsoft.com/en-gb/dotnet/api/system.diagnostics.process?view=netcore-3.1)
  * Javascript - .js - [Jint](https://github.com/sebastienros/jint)
  * Linux Shell - .sh -  [Proccess](https://docs.microsoft.com/en-gb/dotnet/api/system.diagnostics.process?view=netcore-3.1) - on Windows You need to Enable the Linux Bash Shell in Windows 10
- **Run At Start Up**
- **Custom Icon for Scripts**
- **More Configurations**


## Download and Install
* [Download Last Release Installer - 1.4](https://github.com/Gramli/Scriper/releases/download/v1.4/ScriperInstaller.exe)
* [Older Versions - Releases](https://github.com/Gramli/Scriper/releases)


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
* [TaskScheduler](https://github.com/dahall/TaskScheduler)

