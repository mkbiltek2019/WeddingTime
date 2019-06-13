using AIT.ExpenseManagement.Infrastructure.Repositories;
using AIT.ExpenseManagement.Model.DTO;

namespace AIT.ExpenseManagement.Services.UpdateOrderNumber.Strategies
{
    public abstract class UpdateOrderNoStrategy
    {
        protected IExpenseRepository _expenseRepository;

        public UpdateOrderNoStrategy(IExpenseRepository expenseRepository)
        {
            _expenseRepository = expenseRepository;
        }

        public abstract void Execute(UpdateOrderNoBase data);
    }
}