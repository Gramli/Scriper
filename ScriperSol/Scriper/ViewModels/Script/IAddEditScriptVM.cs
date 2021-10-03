using Scriper.Closing;
using ScriperLib;

namespace Scriper.ViewModels.Script
{
    public interface IAddEditScriptVM : IClose<IScript>
    {
        void InvalidName(string message);
    }
}
