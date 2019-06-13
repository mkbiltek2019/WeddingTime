using AIT.ExpenseManagement.Infrastructure.Repositories;
using AIT.ExpenseManagement.Model.DTO;
using AIT.ExpenseManagement.Model.Enums;
using AIT.ExpenseManagement.Services.UpdateOrderNumber.Strategies;
using AIT.UtilitiesComponents.Strategy;

namespace AIT.ExpenseManagement.Services.UpdateOrderNumber
{
    internal class UpdateOrderNoService : IUpdateOrderNoService
    {
        IActionStrategyService<UpdateOrderNoType, UpdateOrderNoBase> _updateOrderNoService;

        public UpdateOrderNoService(IExpenseRepository expenseRepository)
        {
            _updateOrderNoService = new ActionStrategyService<UpdateOrderNoType, UpdateOrderNoBase>();

            _updateOrderNoService.AddStrategy(UpdateOrderNoType.Create, new SetOrderNoOnCreateStrategy(expenseRepository).Execute);
            _updateOrderNoService.AddStrategy(UpdateOrderNoType.Sort, new SetOrderNoOnSortStrategy(expenseRepository).Execute);
            _updateOrderNoService.AddStrategy(UpdateOrderNoType.SortAsLast, new SetMaxOrderNoStrategy(expenseRepository).Execute);            
        }

        public void UpdateOrderNo(UpdateOrderNoBase data)
        {
            _updateOrderNoService.Execute(data.UpdateType, data);
        }
    }
}
