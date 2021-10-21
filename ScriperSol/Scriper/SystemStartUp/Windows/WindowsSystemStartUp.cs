using System;
using System.IO;

namespace Scriper.SystemStartUp.Windows
{
    internal class WindowsSystemStartUp : IWindowsSystemStartUp
    {
        public bool IsStartUp => File.Exists(_shortcutAddress);

        private const string shortcutName = "Scriper.lnk";
        private readonly string _shortcutAddress;

        public WindowsSystemStartUp()
        {
            _shortcutAddress = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Startup), shortcutName);
        }

        public void AddToStartUp()
        {
            var shell = new IWshRuntimeLibrary.WshShell();
            var curAssembly = System.Reflection.Assembly.GetExecutingAssembly();
            var location = curAssembly.Location;
            if(Path.GetExtension(location).Equals(".dll"))
            {
                location = $"{Path.Combine(Path.GetDirectoryName(location),Path.GetFileNameWithoutExtension(location))}.exe";
            }

            var shortcut = (IWshRuntimeLibrary.IWshShortcut)shell.CreateShortcut(_shortcutAddress);
            shortcut.Description = "Scriper Application";
            shortcut.WorkingDirectory = AppDomain.CurrentDomain.BaseDirectory;
            shortcut.TargetPath = location;
            shortcut.IconLocation = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Scriper.ico");
            shortcut.Save();
        }

        public void RemoveFromStartUp()
        {
            if (IsStartUp)
            {
                File.Delete(_shortcutAddress);
            }
        }
    }
}
