using AIT.DomainUtilities.Repositories;
using AIT.ExpenseManagement.Model.DTO;
using System.Collections.Generic;
using System.Linq;

namespace AIT.ExpenseManagement.Infrastructure.Repositories
{
    public interface IExpenseRepository : IRepositoryBase<Expense>
    {
        IQueryable<Expense> Find(string userId);
        IQueryable<Expense> Find(string userId, IEnumerable<int> ids);        
        IQueryable<Expense> FindWithOrderNumberGraterThan(string userId, int expenseIdToExclude, int orderNoFrom);
        Expense Find(string userId, int expenseId);
        decimal GetTotalPrice(string userId);
        int? GetMaxOrderNo(string userId);        
    }
}
