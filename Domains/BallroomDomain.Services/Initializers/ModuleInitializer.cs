using AIT.BallroomDomain.Services.LayoutService;
using AIT.BallroomDomain.Services.LayoutService.Services;
using AIT.BallroomDomain.Services.LayoutService.Strategies;
using AIT.UtilitiesComponents.Commands;
using AIT.UtilitiesComponents.Services;

namespace AIT.BallroomDomain.Services.Initializers
{
    public class ModuleInitializer : IBootCommand
    {
        public void Execute()
        {
            UnityService.Get().Register<IBallroomService, BallroomService>()
                              .Register<IProcessLayoutService, ProcessLayoutService>()
                              .Register<IProcessTableSeatsService, ProcessTableSeatsService>()
                              .Register<ProcessTableItemStrategy>()
                              .Register<ProcessBallroomItemStrategy>();
        }
    }
}
