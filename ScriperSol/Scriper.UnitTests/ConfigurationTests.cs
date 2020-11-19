using NUnit.Framework;
using ScriperLib;
using ScriperLib.Configuration;
using System.Linq;

namespace Scriper.UnitTests
{
    public class ConfigurationTests : TestsBase
    {
        [Test]
        public void LoadConfiguration()
        {
            var config = ScriperConfiguration.Load(filePath);
            Assert.AreEqual(5, config.ScriptManagerConfiguration.ScriptsConfigurations.Count);
        }

        [Test]
        public void SaveConfiguration()
        {
            var config = ScriperConfiguration.Load(filePath);
            config.Save(filePath);
        }

        [Test]
        public void AddRemoveSaveConfiguration()
        {
            var name = "Sleep";

            var scriperLibContainer = new ScriperLibContainer(filePath);
            var scriptConfig = scriperLibContainer.GetInstance<IScriptConfiguration>();
            scriptConfig.Name = name;
            scriptConfig.Path = @"Assets\sleep.py";

            var scriptManager = scriperLibContainer.GetInstance<IScriptManager>();
            scriptManager.AddScript(scriptConfig);

            var scriperConfiguration = scriperLibContainer.GetInstance<IScriperConfiguration>();
            scriperConfiguration.Save(filePath);

            var config = ScriperConfiguration.Load(filePath);
            Assert.AreEqual(6, config.ScriptManagerConfiguration.ScriptsConfigurations.Count);

            var scriptToDelete = config.ScriptManagerConfiguration.ScriptsConfigurations.Single(item => item.Name == name);
            config.ScriptManagerConfiguration.ScriptsConfigurations.Remove(scriptToDelete);

            config.Save(filePath);

        }
    }
}