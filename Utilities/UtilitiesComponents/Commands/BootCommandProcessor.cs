using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace AIT.UtilitiesComponents.Commands
{
    public static class BootCommandProcessor
    {
        private static Logger _logger = LogManager.GetCurrentClassLogger();

        public static void Run()
        {            
            IEnumerable<Type> typesThatImplementIBootCommand = null;

            try
            {
                typesThatImplementIBootCommand = AppDomain.CurrentDomain.GetAssemblies()
                                                          .SelectMany(n => n.GetTypes())
                                                          .Where(n => n.IsClass && typeof(IBootCommand).IsAssignableFrom(n));                                                                        
            }
            catch (ReflectionTypeLoadException ex)
            {
                _logger.Error(ex.Message, ex);
            }

            foreach (var type in typesThatImplementIBootCommand)
            {
                IBootCommand bootCommand = (IBootCommand)Activator.CreateInstance(type, null);
                bootCommand.Execute();
            }           
        }        
    }
}
