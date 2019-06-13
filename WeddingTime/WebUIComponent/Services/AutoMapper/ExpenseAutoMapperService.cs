using AIT.ExpenseManagement.Model.DTO;
using AIT.WebUIComponent.Models.Expenses;
using AIT.WebUIComponent.Services.Undo.DTO.Expenses;
using AutoMapper;
using System.Collections.Generic;

namespace AIT.WebUIComponent.Services.AutoMapper
{
    public class ExpenseAutoMapperService : IExpenseAutoMapperService
    {
        public List<Expense> MapExpenseUndoModels(List<ExpenseUndo> items)
        {
            return Mapper.Map<List<ExpenseUndo>, List<Expense>>(items);
        }

        public List<ExpenseUndo> MapExpenseItemsUndo(List<Expense> entities)
        {
            return Mapper.Map<List<Expense>, List<ExpenseUndo>>(entities);
        }

        public List<ExpenseItemModel> MapExpenseItems(List<Expense> entities)
        {
            return Mapper.Map<List<Expense>, List<ExpenseItemModel>>(entities);
        }

        public List<Expense> MapExpenseModels(List<ExpenseItemModel> items)
        {
            return Mapper.Map<List<ExpenseItemModel>, List<Expense>>(items);
        }

        public Expense MapExpenseModel(ExpenseItemModel item)
        {
            return Mapper.Map<ExpenseItemModel, Expense>(item);
        }

        public ExpenseItemModel MapExpenseItem(Expense entity)
        {
            return Mapper.Map<Expense, ExpenseItemModel>(entity);
        }

        public BudgetModel MapBudgetItem(Budget entity)
        {
            return Mapper.Map<Budget, BudgetModel>(entity);
        }

        public Budget MapBudgetModel(BudgetModel model)
        {
            return Mapper.Map<BudgetModel, Budget>(model);
        }

        public UpdateExistingOrderNo MapUpdateExpenseModel(UpdateExpenseOrderNoModel model)
        {
            return Mapper.Map<UpdateExpenseOrderNoModel, UpdateExistingOrderNo>(model);
        }
    }
}