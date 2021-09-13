using Scriper.Closing;
using ScriperLib;

namespace Scriper.ViewModels
{
    public interface IAddEditScriptVM : IClose<IScript>
    {
        void InvalidName(string message);
    }
}
