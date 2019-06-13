using AIT.BallroomDomain.Infrastructure.Repositories;
using AIT.UtilitiesComponents.Commands;
using AIT.UtilitiesComponents.Services;

namespace AIT.BallroomDomain.Infrastructure.Initializers
{
    public class ModuleInitializer : IBootCommand
    {
        public void Execute()
        {
            UnityService.Get().Register<IBallroomRepository, BallroomRepository>();
        }
    }
}
