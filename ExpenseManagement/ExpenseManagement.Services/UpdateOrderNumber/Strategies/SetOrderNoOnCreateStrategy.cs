using AIT.ExpenseManagement.Infrastructure.Repositories;
using AIT.ExpenseManagement.Model.DTO;

namespace AIT.ExpenseManagement.Services.UpdateOrderNumber.Strategies
{
    public class SetOrderNoOnCreateStrategy : UpdateOrderNoStrategy
    {
        public SetOrderNoOnCreateStrategy(IExpenseRepository expenseRepository)
            : base(expenseRepository)
        {
        }

        public override void Execute(UpdateOrderNoBase data)
        {
            var item = (UpdateNewOrderNo)data;

            var orderNo = _expenseRepository.GetMaxOrderNo(item.UserId);
            var maxOrderNo = orderNo.HasValue ? orderNo.Value + 1 : 0;

            item.Expenses.ForEach(n => n.OrderNo = maxOrderNo++);
        }
    }
}
