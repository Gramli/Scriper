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

        [TestCaseSource(typeof(PowerShellArgumentsData))]
        public void GetTest(string rawData, PowerShellScriptInputs expectedPowerShellScriptInputs)
        {
            var powerShellArgumentsSplitter = _scriperContainer.GetInstance<IPowerShellArgumentsSplitter>();
            var result = powerShellArgumentsSplitter.Get(rawData);
            CollectionAssert.AreEqual(expectedPowerShellScriptInputs.Arguments, result.Arguments);
            CollectionAssert.AreEqual(expectedPowerShellScriptInputs.Parameters, result.Parameters);
        }
    }
}
