namespace ScriperLib.Configuration.Attributes
{
    internal class ConfigurationAttributeAttribute : ConfigurationBaseAttribute
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
