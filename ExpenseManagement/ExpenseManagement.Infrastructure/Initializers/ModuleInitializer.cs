using AIT.ExpenseManagement.Infrastructure.Repositories;
using AIT.UtilitiesComponents.Commands;
using AIT.UtilitiesComponents.Services;

namespace AIT.ExpenseManagement.Infrastructure.Initializers
{
    public class ModuleInitializer : IBootCommand
    {
        public void Execute()
        {
            UnityService.Get().Register<IExpenseRepository, ExpenseRepository>()
                              .Register<IBudgetRepository, BudgetRepository>();
        }
    }
}
