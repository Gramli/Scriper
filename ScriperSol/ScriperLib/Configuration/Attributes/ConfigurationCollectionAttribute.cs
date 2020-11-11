using System;

namespace ScriperLib.Configuration.Attributes
{
    public class ConfigurationCollectionAttribute : ConfigurationBaseAttribute
    {
        public string CollectionItemName { get; private set; }
        public ConfigurationCollectionAttribute(string name, string collectionItemName) 
            : base(name)
        {
            CollectionItemName = collectionItemName;
        }

        public ConfigurationCollectionAttribute(string name, bool mandatory)
            : base(name, mandatory)
        {
        }
    }
}
