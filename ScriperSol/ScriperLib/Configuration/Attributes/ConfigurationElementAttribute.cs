namespace ScriperLib.Configuration.Attributes
{
    public class ConfigurationElementAttribute : ConfigurationBaseAttribute
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
