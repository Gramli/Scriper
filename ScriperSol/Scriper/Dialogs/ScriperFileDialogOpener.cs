using ScriperLib.Enums;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Scriper.Dialogs
{
    internal class ScriperFileDialogOpener : IScriperFileDialogOpener
    {
        public string ImageFileFilter => "png | jpg | jpeg | bmp | ico";
        public string ScriptFilter { get; private set; }
        
        public ScriperFileDialogOpener()
        {
            InitScriptFilter();
        }

        public Task<(bool ok, string file)> OpenImageFileDialogAsync()
        {
            return OpenFile(ImageFileFilter);
        }

        public Task<(bool ok, string file)> OpenOutputFileDialogAsync()
        {
            return OpenFile();
        }

        public Task<(bool ok, string file)> OpenScriptFileDialogAsync()
        {
            return OpenFile(ScriptFilter);
        }

        private Task<(bool ok, string file)> OpenFile(string filter = "")
        {
            return Task.Run(async () =>
            {
                var openFileDialog = new OpenFileDialogAdapter();
                var result = await openFileDialog.ShowAsync(filter);
                return (result.Ok, result.Files?.SingleOrDefault());
            });
        }

        private void InitScriptFilter()
        {
            var builder = new StringBuilder();
            var type = typeof(ScriptType);
            var fields = type.GetFields();
            foreach (var field in fields[1..^1])
            {
                var attribute = GetAttribute(field);
                builder.Append($"{attribute.FileExtensionts.First()} | ");
            }

            var LastAttribute = GetAttribute(fields.Last());
            builder.Append($"{LastAttribute.FileExtensionts.First()}");

            ScriptFilter = builder.ToString();
        }

        private FileExtensionAttribute GetAttribute(FieldInfo field)
        {
            return (FileExtensionAttribute)field.GetCustomAttributes(typeof(FileExtensionAttribute), false).First();
        }
    }
}
