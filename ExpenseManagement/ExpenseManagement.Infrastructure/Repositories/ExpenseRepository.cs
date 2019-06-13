using AIT.DomainUtilities;
using AIT.DomainUtilities.Repositories;
using AIT.ExpenseManagement.Infrastructure.DbContext;
using AIT.ExpenseManagement.Model.DTO;
using System.Collections.Generic;
using System.Linq;

namespace AIT.ExpenseManagement.Infrastructure.Repositories
{
    public class ExpenseRepository : RepositoryBase<Expense, ExpenseContext>, IExpenseRepository
    {
        public ExpenseRepository(IUnitOfWork<ExpenseContext> unitOfWork)
            : base(unitOfWork)
        {
        }

        public IQueryable<Expense> Find(string userId)
        {
            return GetAll().Where(n => n.UserId == userId).OrderBy(n => n.OrderNo);
        }

        public IQueryable<Expense> Find(string userId, IEnumerable<int> ids)
        {
            return GetAll().Where(n => n.UserId == userId && ids.Contains(n.Id)).OrderBy(n => n.OrderNo);
        }

        public IQueryable<Expense> FindWithOrderNumberGraterThan(string userId, int expenseIdToExclude, int orderNoFrom)
        {
            return GetAll().Where(n => n.UserId == userId && n.Id != expenseIdToExclude && n.OrderNo >= orderNoFrom);
        }

        public Expense Find(string userId, int expenseId)
        {
            return GetAll().Single(n => n.UserId == userId && n.Id == expenseId);
        }

        public decimal GetTotalPrice(string userId)
        {
            return GetAll().Where(n => n.UserId == userId).Select(n => n.Price).DefaultIfEmpty().Sum();
        }

        public int? GetMaxOrderNo(string userId)
        {
            return GetAll().Where(n => n.UserId == userId).Max(n => n.OrderNo);
        }
    }
}
