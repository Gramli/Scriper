namespace Scriper.UnitTests.Models
{
    public class TestScriperContainer : ScriperContainer
    {
        public TestScriperContainer(string configurationFilePath, string uiConfigfurationFilePath)
            : base(configurationFilePath, uiConfigfurationFilePath)
        {
            _container.Options.EnableAutoVerification = false;
        }
    }
}
