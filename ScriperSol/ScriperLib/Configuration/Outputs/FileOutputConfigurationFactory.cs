namespace ScriperLib.Configuration.Outputs
{
    class FileOutputConfigurationFactory : IFileOutputConfigurationFactory
    {
        public IFileOutputConfiguration Create()
        {
            return new FileOutputConfiguration();
        }
    }
}
