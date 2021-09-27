using Scriper.Configuration;

namespace Scriper.ViewModels
{
    public interface IMainVM
    {
        IScriperUIConfiguration ActualUiConfiguration { get; }

        void Init();
    }
}
