using System;
using System.Collections.Generic;
using System.Linq;

namespace AIT.UndoManagement.Infrastructure.Serialization
{
    public class XmlSerializationTypeRegistry : IXmlSerializationTypeRegistry
    {        
        readonly Dictionary<Guid, Type> typeMap = new Dictionary<Guid, Type>();

        public void RegisterType(Type xmlSerializationType)
        {
            var typeKey = GetTypeKeyFromSerializationAttribute(xmlSerializationType);

            if (typeMap.ContainsKey(typeKey))
                throw new InvalidOperationException("That typeKey has already been registered: " + typeKey);

            typeMap.Add(typeKey, xmlSerializationType);
        }

        public Type Lookup(Guid typeKey)
        {
            Type type;
            return typeMap.TryGetValue(typeKey, out type) ? type : null;
        }

        public Guid GetTypeKeyFromSerializationAttribute(Type type)
        {
            var attributes = type.GetCustomAttributes(typeof(XmlSerializationTypeKeyAttribute), false);

            if (!attributes.Any(n => n is XmlSerializationTypeKeyAttribute))
                throw new InvalidOperationException(string.Format("{0} should have an XmlSerializationTypeKeyAttribute", type.FullName));

            return attributes.OfType<XmlSerializationTypeKeyAttribute>().First().SerializationTypeKey;
        }
    }
}
