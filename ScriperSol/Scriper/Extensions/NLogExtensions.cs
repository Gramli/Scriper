using NLog;

namespace Scriper.Extensions
{
    public static class NLogExtensions
    {
        private static LogFactory logFactory;
        public static LogFactory LogFactory
        {
            get
            {
                if(logFactory is null)
                {
                    logFactory = LogManager.LoadConfiguration(@"Config\nlog.config");
                }

                return logFactory;
            }
        }
    }
}
