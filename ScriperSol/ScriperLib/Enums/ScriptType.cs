namespace ScriperLib.Enums
{
    public enum ScriptType
    {
        [FileExtension(".ps1")]
        PowerShell1,
        [FileExtension(".ps2")]
        PowerShell2,
        [FileExtension(".bat")]
        WindowsProcess,
        [FileExtension(".exe")]
        ExeFile,
        [FileExtension(".py")]
        PythonFile,
        [FileExtension(".sh")]
        LinuxShell,
    }
}
