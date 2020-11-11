using ScriperLib.Configuration.Base;

namespace Scriper.Configuration
{
    public interface IScriperUIConfiguration : IConfigurationElement
    {
        ITextEditorConfiguration TextEditor { get; set; }
        void Save(string path);
    }
}
