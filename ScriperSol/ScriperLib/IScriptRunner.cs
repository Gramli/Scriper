using ScriperLib.Enums;

namespace ScriperLib
{
    internal interface IScriptRunner
    {
        public ScriptType[] ScriptTypes { get; }
        void Run(IScript script);
    }
}
