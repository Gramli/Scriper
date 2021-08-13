using NUnit.Framework;
using ScriperLib;
using ScriperLib.Configuration;
using ScriperLib.Exceptions;
using System.Linq;

namespace Scriper.UnitTests
{
    public class ManageScriptsTests : TestsBase
    {
        private ScriperLibContainer _scriperLibContainer;

        [SetUp]
        public void SetUp()
        {
            _scriperLibContainer = new ScriperLibContainer(filePath);
        }

        [Test]
        public void AddScript()
        {
            var scriptManager = _scriperLibContainer.GetInstance<IScriptManager>();
            var scriptCreator = _scriperLibContainer.GetInstance<IScriptFactory>();
            var scriptConfiguration = _scriperLibContainer.GetInstance<IScriptConfigurationFactory>().CreateEmptyScriptConfiguration();
            scriptConfiguration.Name = "test";
            scriptConfiguration.Path = "som.py";
            var script = scriptCreator.Create(scriptConfiguration);
            scriptManager.AddScript(script);
            CollectionAssert.Contains(scriptManager.Scripts, script);
        }

        [Test]
        public void AddScriptShouldThrow()
        {
            var scriptManager = _scriperLibContainer.GetInstance<IScriptManager>();
            var scriptCreator = _scriperLibContainer.GetInstance<IScriptFactory>();
            var scriptConfiguration = _scriperLibContainer.GetInstance<IScriptConfigurationFactory>().CreateEmptyScriptConfiguration();
            scriptConfiguration.Path = "som.py";
            Assert.Throws<ConfigurationException>(() => scriptCreator.Create(scriptConfiguration));
            scriptConfiguration.Name = "ahoj";
            scriptConfiguration.Path = null;
            Assert.Throws<ConfigurationException>(() => scriptCreator.Create(scriptConfiguration));
        }

        [Test]
        public void RemoveScript()
        {
            var scriptManager = _scriperLibContainer.GetInstance<IScriptManager>();
            var script = scriptManager.Scripts.First();
            scriptManager.RemoveScript(script);
            CollectionAssert.DoesNotContain(scriptManager.Scripts, script);
        }
    }
}
