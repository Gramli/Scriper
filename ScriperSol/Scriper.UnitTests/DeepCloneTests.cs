using NUnit.Framework;
using Scriper.UnitTests.Models;
using ScriperLib.Clone;
using ScriperLib.Configuration;
using ScriperLib.Configuration.TimeTrigger;
using System.Linq;

namespace Scriper.UnitTests
{
    public class DeepCloneTests : TestsBase
    {
        private readonly ScriperContainer _scriperContainer;

        public DeepCloneTests()
        {
            _scriperContainer = new TestScriperContainer(filePath, uiFilePath);
        }

        [Test]
        public void CheckSameObject()
        {
            var configurationCreator = _scriperContainer.GetInstance<IScriptConfigurationFactory>();
            var deepCloneAdapter = _scriperContainer.GetInstance<IDeepCloneAdapter>();
            var timeTriggerConfigCreate = _scriperContainer.GetInstance<ITimeTriggerConfigurationFactory>();

            var config = configurationCreator.CreateEmptyScriptConfiguration();
            config.Name = "SOMETHING";
            var timeTriggerConfig = timeTriggerConfigCreate.Create();
            timeTriggerConfig.DelayInSeconds = 50;
            config.TimeScheduleConfigurations.Add(timeTriggerConfig);

            var newConfig = deepCloneAdapter.DeepClone(config);

            Assert.AreNotSame(config, newConfig, "Objects are same");
            Assert.AreEqual(config.Name, newConfig.Name);
            Assert.AreEqual(config.Arguments, newConfig.Arguments);
            Assert.AreEqual(config.InSystemTray, newConfig.InSystemTray);
            Assert.AreNotSame(config.TimeScheduleConfigurations, newConfig.TimeScheduleConfigurations);
            Assert.AreNotSame(config.TimeScheduleConfigurations.First(), newConfig.TimeScheduleConfigurations.First());
            Assert.AreEqual(config.TimeScheduleConfigurations.First().DelayInSeconds, newConfig.TimeScheduleConfigurations.First().DelayInSeconds);

        }
    }
}
