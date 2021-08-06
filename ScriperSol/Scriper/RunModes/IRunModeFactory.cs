namespace Scriper.RunModes
{
    interface IRunModeFactory
    {
        IRunMode CreateRunMode(string[] command);
    }
}
