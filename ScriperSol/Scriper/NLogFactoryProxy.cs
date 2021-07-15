using NLog;
using Scriper.Configuration.Finders;

namespace Scriper
{
    public class NLogFactoryProxy
    {
        private static NLogFactoryProxy _instance;
        public static NLogFactoryProxy Instance => _instance ??= new NLogFactoryProxy(new NLogConfigFinder());

        private readonly LogFactory _logFactory;

        private NLogFactoryProxy(ConfigFinder config)
        {
            var filePath = config.FindConfig();
            _logFactory = LogManager.LoadConfiguration(filePath);
        }

        public Logger GetLogger()
        {
            return _logFactory.GetCurrentClassLogger();
        }

        public void Shutdown()
        {
            _logFactory.Shutdown();
        }
    }
}
