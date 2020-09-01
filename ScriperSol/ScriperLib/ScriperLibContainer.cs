using ScriperLib.Configuration;
using ScriperLib.Configuration.Outputs;
using ScriperLib.Core;
using SimpleInjector;

namespace ScriperLib
{
    public class ScriperLibContainer
    {
        private Container _container;
        public ScriperLibContainer(string configurationFile)
        {
            _container = new Container();
            Register(configurationFile);
        }

        private void Register(string configurationFile)
        {
            var configuration = ScriperConfiguration.Load(configurationFile);

            _container.RegisterSingleton(() => configuration.ScriptManagerConfiguration);
            _container.Register<IScriptManager, ScriptManager>();
            _container.Register<IScriptConfiguration>(() => new ScriptConfiguration());
            _container.Register<ITimeScheduleConfiguration>(() => new TimeScheduleConfiguration());
            _container.Register<IConsoleOutputConfiguration>(() => new ConsoleOutputConfiguration());
            _container.Register<IFileOutputConfiguration>(() => new FileOutputConfiguration());
        }
    }
}
