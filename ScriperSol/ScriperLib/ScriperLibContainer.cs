﻿using ScriperLib.Configuration;
using ScriperLib.Configuration.Outputs;
using ScriperLib.Core;
using ScriperLib.Extensions;
using ScriperLib.Scripts;
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
            _container.Collection.Register<IScriptRunner>(
                typeof(ProcessRunner),
                typeof(PythonRunner),
                typeof(PowerShellRunner));
            _container.RegisterWithFactoryCollection<IScript>(
                typeof(BatchScript),
                typeof(PythonScript),
                typeof(PowerShellScript_v1),
                typeof(PowerShellScript_v2),
                typeof(ExeFile));
            _container.RegisterWithFactoryCollection<IOutput>(
                typeof(FileOutput));
            _container.Register<IScriptManager, ScriptManager>();
            _container.Register<IScriptConfiguration>(() => new ScriptConfiguration());
            _container.Register<ITimeScheduleConfiguration>(() => new TimeScheduleConfiguration());
            _container.Register<IFileOutputConfiguration>(() => new FileOutputConfiguration());

            _container.Verify();
        }

        public T GetInstance<T>() where T : class
        {
            return _container.GetInstance<T>();
        }
    }
}
