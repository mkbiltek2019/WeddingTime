using AIT.UndoManagement.Infrastructure.Serialization;
using AIT.UndoManagement.Infrastructure.UndoInterface;
using AIT.UtilitiesComponents.Services;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace AIT.UndoManagement.Infrastructure.Initializers
{
    public class XmlSerializationInitializer
    {
        private static Logger _logger = LogManager.GetCurrentClassLogger();

        public static void Init()
        {
            var typeRegistry = UnityService.Get().Container().GetInstance<IXmlSerializationTypeRegistry>();

            IEnumerable<Type> typesThatImplementIUndoCommand = null;

            try
            {
                typesThatImplementIUndoCommand = AppDomain.CurrentDomain.GetAssemblies()
                                                                        .SelectMany(n => n.GetTypes())
                                                                        .Where(n => n.IsClass && typeof(IUndoCommand).IsAssignableFrom(n));
            }
            catch (ReflectionTypeLoadException ex)
            {
                _logger.Error(ex.Message, ex);
            }
            
            foreach (var type in typesThatImplementIUndoCommand)
                typeRegistry.RegisterType(type);
        }
    }
}
