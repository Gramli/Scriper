using NUnit.Framework;
using ScriperLib.Configuration;

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
        public void ParseConfiguration()
        {
            var config = ScriperConfiguration.Load(filePath);
        }
    }
}