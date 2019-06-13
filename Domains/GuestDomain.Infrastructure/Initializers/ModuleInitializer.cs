using AIT.GuestDomain.Infrastructure.Repositories;
using AIT.UtilitiesComponents.Commands;
using AIT.UtilitiesComponents.Services;
using SimpleInjector;

namespace AIT.GuestDomain.Infrastructure.Initializers
{
    public class ModuleInitializer : IBootCommand
    {
        public void Execute()
        {
            UnityService.Get().Register<IGroupRepository, GroupRepository>(Lifestyle.Scoped);
        }
    }
}
