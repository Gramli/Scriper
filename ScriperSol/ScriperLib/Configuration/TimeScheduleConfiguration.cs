using System.Xml.Linq;

namespace ScriperLib.Configuration
{
    internal class TimeScheduleConfiguration : ConfigurationElement, ITimeScheduleConfiguration
    {
        public TimeScheduleConfiguration(XElement element) : base(element)
        {
        }
    }
}
