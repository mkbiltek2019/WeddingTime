using AIT.ExpenseManagement.Model.DTO;
using AIT.WebUIComponent.Models.Expenses;
using AIT.WebUIComponent.Services.Undo.DTO.Expenses;
using AutoMapper;

namespace AIT.WebUIComponent.AutoMapping
{
    public class ExpenseMap
    {
        public static void CreateMap()
        {
            Mapper.CreateMap<Expense, ExpenseUndo>();
            Mapper.CreateMap<ExpenseUndo, Expense>();

            Mapper.CreateMap<Expense, ExpenseItemModel>();
            Mapper.CreateMap<ExpenseItemModel, Expense>()
                .ForMember(n => n.Price, o => o.ResolveUsing(n => string.IsNullOrEmpty(n.Price) ? int.Parse(n.Quantity) * decimal.Parse(n.UnitPrice) : decimal.Parse(n.Price)));

            Mapper.CreateMap<Budget, BudgetModel>();
            Mapper.CreateMap<BudgetModel, Budget>();

            Mapper.CreateMap<UpdateExpenseOrderNoModel, UpdateExistingOrderNo>();
        }
    }
}