using AIT.TaskDomain.Infrastructure.Repositories;
using AIT.UtilitiesComponents.Commands;
using AIT.UtilitiesComponents.Services;

namespace AIT.TaskDomain.Infrastructure.Initializers
{
    public class ModuleInitializer : IBootCommand
    {
        public void Execute()
        {
            UnityService.Get().Register<ITaskRepository, TaskRepository>();
        }
    }
}
