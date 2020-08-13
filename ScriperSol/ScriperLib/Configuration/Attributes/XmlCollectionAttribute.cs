using System;

namespace ScriperLib.Configuration.Attributes
{
    internal class XmlCollectionAttribute : XmlRepresentationAttribute
    {
        public string CollectionItemName { get; private set; }
        public XmlCollectionAttribute(string name, string collectionItemName) 
            : base(name)
        {
            CollectionItemName = collectionItemName;
        }
    }
}
