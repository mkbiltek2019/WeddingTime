using System;

namespace AIT.UndoManagement.Infrastructure.Serialization
{
    public interface IXmlSerializationTypeRegistry
    {
        void RegisterType(Type xmlSerializationType);
        Type Lookup(Guid typeId);
        Guid GetTypeKeyFromSerializationAttribute(Type type);
    }
}
