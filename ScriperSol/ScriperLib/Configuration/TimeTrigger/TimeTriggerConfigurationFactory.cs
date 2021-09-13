namespace ScriperLib.Configuration.TimeTrigger
{
    class TimeTriggerConfigurationFactory : ITimeTriggerConfigurationFactory
    {
        public ITimeTriggerConfiguration Create()
        {
            return new TimeTriggerConfiguration();
        }
    }
}
