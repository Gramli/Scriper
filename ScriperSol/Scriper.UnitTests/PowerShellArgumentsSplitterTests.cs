using NUnit.Framework;
using Scriper.UnitTests.Models;
using ScriperLib.Arguments;

namespace Scriper.UnitTests
{
    public class PowerShellArgumentsSplitterTests : TestsBase
    {
        private readonly TestScriperLibContainer _scriperContainer;

        public PowerShellArgumentsSplitterTests()
        {
            _scriperContainer = new TestScriperLibContainer(filePath);
        }

        [TestCaseSource(typeof(ArgumentsSourceData))]
        public void GetTest()
        {
            var powerShellArgumentsSplitter = _scriperContainer.GetInstance<IPowerShellArgumentsSplitter>();
        }
    }
}
