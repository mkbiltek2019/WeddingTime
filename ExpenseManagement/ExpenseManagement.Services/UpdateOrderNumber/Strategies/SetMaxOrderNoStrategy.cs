using AIT.ExpenseManagement.Infrastructure.Repositories;
using AIT.ExpenseManagement.Model.DTO;

namespace AIT.ExpenseManagement.Services.UpdateOrderNumber.Strategies
{
    public class SetMaxOrderNoStrategy : UpdateOrderNoStrategy
    {
        public SetMaxOrderNoStrategy(IExpenseRepository expenseRepository)
            : base(expenseRepository)
        {
        }

        public override void Execute(UpdateOrderNoBase data)
        {
            var item = (UpdateExistingOrderNo)data;

            var maxOrderNo = _expenseRepository.GetMaxOrderNo(item.UserId);             // value is always present (sort as last)

            var expense = _expenseRepository.Find(item.UserId, item.UpdateId);
            expense.OrderNo = maxOrderNo.Value + 1;
        }
    }
}
