using NUnit.Framework;
using ScriperLib;

namespace Scriper.UnitTests
{
    public class RunScriptsTests : TestsBase
    {
        private ScriperLibContainer _scriperLibContainer;

        [SetUp]
        public void SetUp()
        {
            _scriperLibContainer = new ScriperLibContainer(filePath);
        }

        [Test]
        public void RunAllScripts()
        {
            var scriptManager = _scriperLibContainer.GetInstance<IScriptManager>();
            var scriptRunner = _scriperLibContainer.GetInstance<IScriptRunner>();
            foreach (var script in scriptManager.Scripts)
            {
                scriptRunner.Run(script);
            }
        }
    }
}
