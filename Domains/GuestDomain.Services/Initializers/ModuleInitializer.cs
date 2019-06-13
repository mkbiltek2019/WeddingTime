using AIT.UtilitiesComponents.Commands;
using AIT.UtilitiesComponents.Services;

namespace AIT.GuestDomain.Services.Initializers
{
    public class ModuleInitializer : IBootCommand
    {
        public void Execute()
        {
            UnityService.Get().Register<IGroupService, GroupService>()
                              .Register<IPersonService, PersonService>();
        }
    }
}