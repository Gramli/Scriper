using ScriperLib;

namespace Scriper.UnitTests.Models
{
    public class TestScriperLibContainer : ScriperLibContainer
    {
        public TestScriperLibContainer(string configurationFilePath) 
            : base(configurationFilePath)
        {
            Register();
        }

        public void Verify()
        {
            _container.Verify();
        }
    }
}
