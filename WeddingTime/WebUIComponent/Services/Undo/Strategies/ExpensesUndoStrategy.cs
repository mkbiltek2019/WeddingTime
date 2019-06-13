using AIT.ExpenseManagement.Model.DTO;
using AIT.ExpenseManagement.Services;
using AIT.WebUIComponent.Services.AutoMapper;
using AIT.WebUIComponent.Services.Undo.DTO.Expenses;
using System.Collections.Generic;

namespace AIT.WebUIComponent.Services.Undo.Strategies
{
    public class ExpensesUndoStrategy : UndoStrategyBase<List<ExpenseUndo>>
    {
        private readonly IExpenseService _expenseService;
        private readonly IExpenseAutoMapperService _autoMapperService;

        public ExpensesUndoStrategy(IExpenseService expenseService, IExpenseAutoMapperService autoMapperService)
        {
            _expenseService = expenseService;
            _autoMapperService = autoMapperService;
        }

        protected override object Process(List<ExpenseUndo> input)
        {
            List<Expense> items = _autoMapperService.MapExpenseUndoModels(input);
            _expenseService.Recreate(items);
                                
            return null;    // means there is no need to return anything from here because after undoing all items are fetched. I could remeber index(es) on page...and then just return items from here...
        }
    }
}