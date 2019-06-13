using AIT.ExpenseManagement.Infrastructure.Repositories;
using AIT.ExpenseManagement.Model.DTO;
using System.Linq;

namespace AIT.ExpenseManagement.Services.UpdateOrderNumber.Strategies
{
    public class SetOrderNoOnSortStrategy : UpdateOrderNoStrategy
    {
        public SetOrderNoOnSortStrategy(IExpenseRepository expenseRepository)
            : base(expenseRepository)
        {
        }

        public override void Execute(UpdateOrderNoBase data)
        {
            var item = (UpdateExistingOrderNo)data;

            var orderNoFrom = _expenseRepository.Find(item.UserId, item.BaseItemId.Value).OrderNo.Value;    // first get expense from which I'll get orderNoFrom; i'm sure that found expense has order no

            var expenses = _expenseRepository.FindWithOrderNumberGraterThan(item.UserId, item.UpdateId, orderNoFrom).ToList();
            expenses.ForEach(n => n.OrderNo = n.OrderNo.Value + 1);

            var expense = _expenseRepository.Find(item.UserId, item.UpdateId);                              // get moved expense and change it's order no
            expense.OrderNo = orderNoFrom;
        }
    }
}
