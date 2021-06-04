using ScriperLib.Configuration;
using ScriperLib.Configuration.Outputs;
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
        private Container _container;
        public ScriperLibContainer(string configurationFile)
        {
            _container = new Container();
            Register(configurationFile);
        }

        private void Register(string configurationFile)
        {
            var configuration = ScriperConfiguration.Load(configurationFile);

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
            _container.Register<IScriptCreator, ScriptCreator>();
            _container.Register<IScriptManager, ScriptManager>();
            _container.Register<IScriptConfiguration>(() => new ScriptConfiguration());
            _container.Register<ITimeTriggerConfiguration>(() => new TimeTriggerConfiguration());
            _container.Register<IFileOutputConfiguration>(() => new FileOutputConfiguration());
            _container.Register<IScriptTaskSchedulerRunner, ScriptTaskSchedulerRunner>();

            _container.Verify();
        }

        public T GetInstance<T>() where T : class
        {
            return _container.GetInstance<T>();
        }
    }
}
