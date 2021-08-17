using ScriperLib.Configuration;
using ScriperLib.Configuration.Outputs;
using ScriperLib.Configuration.TimeTrigger;
using ScriperLib.Extensions;
using ScriperLib.Outputs;
using ScriperLib.Runners;
using ScriperLib.Scripts;
using ScriperLib.ScriptScheduler;
using SimpleInjector;

namespace ScriperLib
{
    public class ScriperLibContainer : IScriperLibContainer
    {
        protected readonly Container _container;
        protected readonly string _configurationFile;
        public ScriperLibContainer(string configurationFile)
        {
            _container = new Container();
            _configurationFile = configurationFile;
        }

        protected virtual void Register()
        {
            var configuration = ScriperConfiguration.Load(_configurationFile);

            _container.RegisterInstance(configuration);
            _container.RegisterInstance(configuration.ScriptManagerConfiguration);
            _container.Collection.Register<IRunner>(
                typeof(ProcessRunner),
                typeof(PythonRunner),
                typeof(PowerShellRunner),
                typeof(JavascriptRunner));
            _container.Register<IScriptRunner, ScriptRunner>();
            _container.RegisterWithFactoryCollection<IScript>(
                typeof(BatchScript),
                typeof(PythonScript),
                typeof(PowerShellScript_v1),
                typeof(PowerShellScript_v2),
                typeof(ExeFile),
                typeof(LinuxShell),
                typeof(JavascriptScript));
            _container.RegisterWithFactoryCollection<IOutput>(
                typeof(FileOutput));
            _container.Register<IScriptFactory, ScriptFactory>();
            _container.Register<IScriptManager, ScriptManager>();
            _container.Register<IScriptConfigurationFactory, ScriptConfigurationFactory>();
            _container.Register<IScriptTaskSchedulerRunner, ScriptTaskSchedulerRunner>();
            _container.Register<ITaskScheduleAdapter, TaskScheduleAdapter>();
            _container.Register<IScriptSchedulerManager, ScriptSchedulerManager>();
            _container.Register<IFileOutputConfigurationFactory, FileOutputConfigurationFactory>();
            _container.Register<ITimeTriggerConfigurationFactory, TimeTriggerConfigurationFactory>();
        }

        public T GetInstance<T>() where T : class
        {
            return _container.GetInstance<T>();
        }
    }
}
