using ScriperLib.Configuration.Base;

namespace ScriperLib.Configuration.Outputs
{
    public interface IFileOutputConfiguration : IConfigurationElement
    {
        string Path { get; set; }
    }
}
