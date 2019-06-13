using AIT.UtilitiesComponents.Commands;
using AIT.UtilitiesComponents.Services;
using SimpleInjector;

namespace AIT.DomainUtilities.Initializers
{
    public class ModuleInitializer : IBootCommand
    {
        public void Execute()
        {
            UnityService.Get().Register(typeof(IUnitOfWork<>), typeof(UnitOfWork<>), Lifestyle.Scoped);
        }
    }
}
