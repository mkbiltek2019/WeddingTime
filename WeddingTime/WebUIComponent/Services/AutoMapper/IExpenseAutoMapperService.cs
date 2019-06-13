using AIT.ExpenseManagement.Model.DTO;
using AIT.WebUIComponent.Models.Expenses;
using AIT.WebUIComponent.Services.Undo.DTO.Expenses;
using System.Collections.Generic;

namespace AIT.WebUIComponent.Services.AutoMapper
{
    public interface IExpenseAutoMapperService
    {
        List<Expense> MapExpenseUndoModels(List<ExpenseUndo> items);
        List<ExpenseUndo> MapExpenseItemsUndo(List<Expense> entities);
        List<ExpenseItemModel> MapExpenseItems(List<Expense> entities);
        List<Expense> MapExpenseModels(List<ExpenseItemModel> items);
        ExpenseItemModel MapExpenseItem(Expense entity);
        Expense MapExpenseModel(ExpenseItemModel item);

        BudgetModel MapBudgetItem(Budget entity);
        Budget MapBudgetModel(BudgetModel model);
        UpdateExistingOrderNo MapUpdateExpenseModel(UpdateExpenseOrderNoModel model);
    }
}
