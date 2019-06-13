using AIT.UndoManagement.Infrastructure.Repositories;
using AIT.UndoManagement.Infrastructure.Serialization;
using AIT.UtilitiesComponents.Commands;
using AIT.UtilitiesComponents.Services;
using SimpleInjector;

namespace AIT.UndoManagement.Infrastructure.Initializers
{
    public class ModuleInitializer : IBootCommand
    {
        public void Execute()
        {
            UnityService.Get().Register<IUndoRepository, UndoRepository>()
                              .Register<IXmlSerializationTypeRegistry, XmlSerializationTypeRegistry>(Lifestyle.Singleton);            
        }
    }
}
