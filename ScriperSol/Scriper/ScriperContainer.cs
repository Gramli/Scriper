using Scriper.AssetsAccess;
using Scriper.Configuration;
using Scriper.Converters;
using Scriper.Models;
using Scriper.SystemStartUp;
using Scriper.SystemTray;
using Scriper.TimeSchedule;
using Scriper.ViewModels;
using Scriper.ViewModels.Validation;
using ScriperLib;
using ScriperLib.Configuration;
using ScriperLib.Configuration.Outputs;
using ScriperLib.Configuration.TimeTrigger;
using System;
using System.Collections.Generic;

namespace Scriper
{
    public class ScriperContainer : ScriperLibContainer
    {
        private readonly string _uiConfigfurationFilePath;
        public ScriperContainer(string configurationFilePath, string uiConfigfurationFilePath)
            : base(configurationFilePath)
        {
            _uiConfigfurationFilePath = uiConfigfurationFilePath;
            Register();
        }

        protected override void Register()
        {
            base.Register();
            _container.RegisterInstance(ScriperUIConfiguration.Load(_uiConfigfurationFilePath));
            _container.RegisterInstance<IUserAssets>(new UserAssets("Scriper"));
            _container.Register<IEmbeddedAssets, EmbeddedAssets>(SimpleInjector.Lifestyle.Singleton);
            _container.Register<IAssets, Assets>(SimpleInjector.Lifestyle.Singleton);
            _container.Register<IImageResize, ImageResize>();
            _container.Register<IScriptIconImageEditor, ScriptIconImageEditor>();
            _container.Register<IOperatingSystemTrayMenuFactory, OperatingSystemTrayMenuFactory>(SimpleInjector.Lifestyle.Singleton);
            _container.Register<ISystemStartUpFactory, SystemStartUpFactory>();
            _container.Register<ISystemTrayMenu, SystemTrayMenuAdapter>(SimpleInjector.Lifestyle.Singleton);
            _container.Register<ISystemStartUp, SystemStartUpAdapter>();
            _container.Register<IScriptFormValidator, ScriptFormValidator>();
            _container.Register<IScriptSchedulerManagerAdapter, ScriptSchedulerManagerAdapter>();
            _container.Register<IOpenEditorScriptCreator, OpenEditorScriptCreator>();
            _container.Register<IScriptTypeToAssetNameConverter, ScriptTypeToAssetNameConverter>();
            _container.Register<Func<IOutputVM>>(() => () => new OutputVM());
            _container.Register<Func<IScript, IScriptVM>>(() => (script) => new ScriptVM(script));
            _container.Register<Func<ICollection<ITimeTriggerConfiguration>, ITimeScheduleVM>>(() => (collection) => 
            new TimeScheduleVM(
                _container.GetInstance<ITimeTriggerConfigurationFactory>(),
                collection));
            _container.Register<Func<IScriptConfiguration, IAddEditScriptVM>>(() => (config) =>
             new AddEditScriptVM(
                 _container.GetInstance<IScriptFactory>(),
                 _container.GetInstance<IFileOutputConfigurationFactory>(),
                 config,
                 _container.GetInstance<IScriptFormValidator>(),
                 _container.GetInstance<Func<ICollection<ITimeTriggerConfiguration>, ITimeScheduleVM>>(),
                 _container.GetInstance<IScriptIconImageEditor>()));

            _container.Register<Func<IScriperUIConfiguration, ISettingsVM>>(() => (config) => new SettingsVM(config, _container.GetInstance<ISystemStartUp>()));
            _container.Register<IScriptManagerVM, ScriptManagerVM>();
            _container.Register<IMainVM, MainVM>();
        }
    }
}
