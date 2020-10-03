using NUnit.Framework;
using ScriperLib;
using ScriperLib.Configuration;

namespace Scriper.UnitTests
{
    public class ContainerTests : TestsBase
    {
        [Test]
        public void VerifyContainer()
        {
            var container = new ScriperLibContainer(filePath);
            Assert.IsNotNull(container);
        }

        [Test]
        public void ScriptManagerInstance()
        {
            var container = new ScriperLibContainer(filePath);
            var scriptManager = container.GetInstance<IScriptManager>();
            Assert.IsNotNull(scriptManager);
        }

        [Test]
        public void ScriptNeededInstances()
        {
            var container = new ScriperLibContainer(filePath);
            var scriptCreator = container.GetInstance<IScriptCreator>();
            var scriptConfiguration = container.GetInstance<IScriptConfiguration>();
            Assert.IsNotNull(scriptCreator);
            Assert.IsNotNull(scriptConfiguration);
            Assert.Throws<SimpleInjector.ActivationException>(() => container.GetInstance<IScript>());
        }
    }
}
