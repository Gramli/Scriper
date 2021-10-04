using NUnit.Framework;
using Scriper.UnitTests.Models;
using ScriperLib;
using ScriperLib.Configuration;

namespace Scriper.UnitTests
{
    public class ContainerTests : TestsBase
    {
        [Test]
        public void VerifyScriperLibContainer()
        {
            var container = new TestScriperLibContainer(filePath);
            container.Verify();
        }

        [Test]
        public void VerifyScriperContainer()
        {
            var container = new AvaloniaScriperContainer(filePath, uiFilePath);
            container.Verify();
        }

        [Test]
        public void ScriptManagerInstance()
        {
            var container = new TestScriperLibContainer(filePath);
            var scriptManager = container.GetInstance<IScriptManager>();
            Assert.IsNotNull(scriptManager);
        }

        [Test]
        public void ScriptNeededInstances()
        {
            var container = new TestScriperLibContainer(filePath);
            var scriptCreator = container.GetInstance<IScriptFactory>();
            var scriptConfiguration = container.GetInstance<IScriptConfigurationFactory>().CreateEmptyScriptConfiguration();
            Assert.IsNotNull(scriptCreator);
            Assert.IsNotNull(scriptConfiguration);
            Assert.Throws<SimpleInjector.ActivationException>(() => container.GetInstance<IScript>());
        }
    }
}
