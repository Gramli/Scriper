using NUnit.Framework;
using ScriperLib.Configuration;
using System.Linq;

namespace Scriper.UnitTests
{
    public class ConfigurationParsingTests
    {
        private string filePath = @"Assets\Configuration.xml";

        [SetUp]
        public void SetUp()
        {

        }

        [Test]
        public void LoadConfiguration()
        {
            var config = ScriperConfiguration.Load(filePath);
            Assert.AreEqual(2, config.ScriptManagerConfiguration.ScriptsConfigurations.Count);
            Assert.AreEqual(2, config.ScriptManagerConfiguration.ScriptsConfigurations.First().TimeScheduleConfigurations.Count);
        }

        [Test]
        public void SaveConfiguration()
        {
            var config = ScriperConfiguration.Load(filePath);
            config.Save();
        }
    }
}