using System;

namespace AIT.UndoManagement.Infrastructure.Serialization
{
    public class XmlSerializationTypeKeyAttribute : Attribute
    {
        public Guid SerializationTypeKey { get; set; }

        public XmlSerializationTypeKeyAttribute(string serializationTypeKey)
        {
            SerializationTypeKey = Guid.Parse(serializationTypeKey);
        }
    }
}
