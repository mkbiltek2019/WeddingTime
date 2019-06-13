using AIT.UtilitiesComponents.Commands;
using AIT.UtilitiesComponents.Services;

namespace AIT.ExpenseManagement.Services.Initializers
{
    public class ModuleInitializer : IBootCommand
    {
        public void Execute()
        {
            UnityService.Get().Register<IExpenseService, ExpenseService>();
        }
    }
}
