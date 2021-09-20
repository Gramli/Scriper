using Avalonia.Controls;
using Scriper.Extensions;
using System.Linq;
using System.Threading.Tasks;

namespace Scriper.Dialogs
{
    public class OpenFileDialogAdapter
    {
        public bool AllowMultiple
        {
            get { return _openFileDialog.AllowMultiple; }
            set { _openFileDialog.AllowMultiple = value; }
        }

        public string Directory
        {
            get { return _openFileDialog.Directory; }
            set { _openFileDialog.Directory = value; }
        }

        public string Filter
        {
            set
            {
                _openFileDialog.Filters.Clear();
                if(string.IsNullOrEmpty(value))
                {
                    return;
                }

                var filter = new FileDialogFilter
                {
                    Name = value
                };
                filter.Extensions.AddRange(value.Replace(" ", "").Split("|"));
                _openFileDialog.Filters.Add(filter);
            }
        }

        private readonly OpenFileDialog _openFileDialog;
        public OpenFileDialogAdapter()
        {
            _openFileDialog = new OpenFileDialog();
            _openFileDialog.AllowMultiple = false;
        }

        public async Task<FileDialogResult> ShowAsync(string filter = "")
        {
            if(!string.IsNullOrEmpty(filter))
            {
                this.Filter = filter;
            }

            var result = await _openFileDialog.ShowAsync(App.Current.GetMainWindow());
            var ok = result != null && ((AllowMultiple && result.Length == 1) || (!AllowMultiple && result.Any()));
            return new FileDialogResult(ok, result);
        }

    }
}
