namespace ScriperLib.Configuration.Attributes
{
    public class ConfigurationAttributeAttribute : ConfigurationBaseAttribute
    {
        public ConfigurationAttributeAttribute(string name)
            : base(name)
        {
        }

        public ConfigurationAttributeAttribute(string name, bool mandatory)
            : base(name, mandatory)
        {
        }
    }
}
