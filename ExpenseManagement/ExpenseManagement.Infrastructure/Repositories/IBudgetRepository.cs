using AIT.DomainUtilities.Repositories;
using AIT.ExpenseManagement.Model.DTO;

namespace AIT.ExpenseManagement.Infrastructure.Repositories
{
    public interface IBudgetRepository : IRepositoryBase<Budget>
    {
        Budget Find(string userId, int budgetId);
        Budget Find(string userId);
    }
}
