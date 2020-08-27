using ScriperLib.Enums;

namespace ScriperLib.Configuration
{
    public interface IOutputConfiguration
    {
        OutputType Type { get; set; }
    }
}
