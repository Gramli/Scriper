using System;

namespace ScriperLib.Configuration.Attributes
{
    internal class ConfigurationCollectionAttribute : ConfigurationBaseAttribute
    {
        public string CollectionItemName { get; private set; }
        public ConfigurationCollectionAttribute(string name, string collectionItemName) 
            : base(name)
        {
            CollectionItemName = collectionItemName;
        }
    }
}
