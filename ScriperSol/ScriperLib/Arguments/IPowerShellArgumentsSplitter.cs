namespace ScriperLib.Arguments
{
    public interface IPowerShellArgumentsSplitter
    {
        PowerShellScriptInputs Get(string rawData);
    }
}
