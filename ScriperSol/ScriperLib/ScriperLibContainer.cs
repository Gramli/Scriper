using ScriperLib.Configuration;
using ScriperLib.Configuration.Outputs;
using ScriperLib.Core;
using ScriperLib.Extensions;
using SimpleInjector;

namespace ScriperLib
{
    public class ScriperLibContainer : IScriperLibContainer
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
            _container.Collection.Register<IScriptRunner>(
                typeof(ProcessRunner),
                typeof(PowerShellRunner));
            _container.Collection.RegisterWithFactory<IOutput>(
                typeof(ConsoleOutput),
                typeof(FileOutput));
            _container.Register<IScriptManager, ScriptManager>();
            _container.Register<IScriptConfiguration>(() => new ScriptConfiguration());
            _container.Register<ITimeScheduleConfiguration>(() => new TimeScheduleConfiguration());
            _container.Register<IConsoleOutputConfiguration>(() => new ConsoleOutputConfiguration());
            _container.Register<IFileOutputConfiguration>(() => new FileOutputConfiguration());

            _container.Verify();
        }

        public T GetInstance<T>() where T : class
        {
            return _container.GetInstance<T>();
        }
    }
}
