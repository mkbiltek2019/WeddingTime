using AIT.ExpenseManagement.Model.DTO;
using System.Collections.Generic;

namespace AIT.ExpenseManagement.Services
{
    public interface IExpenseService
    {
        void CreateBudget(Budget budget);
        void UpdateBudget(string userId, Budget budget);
        void CreateExpenses(string userId, List<Expense> expenses);
        void UpdateExpenses(string userId, List<Expense> expenses);
        void UpdateExpensesOrderNo(UpdateOrderNoBase data);
        void Recreate(List<Expense> expenses);
        decimal GetTotalPrice(string userId);
        Budget GetBudget(string userId);
        Expense GetExpense(string userId, int id);                              // for editing one item
        List<Expense> DeleteExpenses(string userId, List<int> idsToDelete);        
        List<Expense> GetExpenses(string userId);                               // showing all
        List<Expense> GetExpenses(string userId, List<int> ids);                // for editing
    }
}
