namespace ScriperLib.Configuration.Attributes
{
    internal class ConfigurationElementAttribute : ConfigurationBaseAttribute
    {
        public ConfigurationElementAttribute(string name) 
            : base(name)
        {
        }

        public ConfigurationElementAttribute(string name, bool mandatory)
            : base(name, mandatory)
        {
        }
    }
}
