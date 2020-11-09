using ScriperLib.Configuration.Base;

namespace Scriper.Configuration
{
    public interface ITextEditorConfiguration : IConfigurationElement
    {
        string Path { get; set; }
    }
}
