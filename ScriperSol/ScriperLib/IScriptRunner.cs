using ScriperLib.Core;

namespace ScriperLib
{
    internal interface IScriptRunner
    {
        void Run(IScript script, IOutput[] outputs);
    }
}
