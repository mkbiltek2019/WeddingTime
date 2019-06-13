using AIT.DomainUtilities;
using AIT.DomainUtilities.Repositories;
using AIT.ExpenseManagement.Infrastructure.DbContext;
using AIT.ExpenseManagement.Model.DTO;
using System.Linq;

namespace AIT.ExpenseManagement.Infrastructure.Repositories
{
    public class BudgetRepository : RepositoryBase<Budget, ExpenseContext>, IBudgetRepository
    {
        public BudgetRepository(IUnitOfWork<ExpenseContext> unitOfWork)
            : base(unitOfWork)
        {
        }

        public Budget Find(string userId, int budgetId)
        {
            return GetAll().Single(x => x.UserId == userId && x.Id == budgetId);
        }

        public Budget Find(string userId)
        {
            return GetAll().SingleOrDefault(x => x.UserId == userId);
        }
    }
}
