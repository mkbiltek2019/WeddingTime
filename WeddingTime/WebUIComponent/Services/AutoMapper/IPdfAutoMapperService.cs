using AIT.ExpenseManagement.Model.DTO;
using AIT.GuestDomain.Model.Entities;
using AIT.WebUIComponent.Models.Pdf;
using System.Collections.Generic;

namespace AIT.WebUIComponent.Services.AutoMapper
{
    public interface IPdfAutoMapperService
    {
        List<GroupModel> MapGroupEntities(List<Group> entities);
        List<GroupByStatusModel> MapGroupEntitiesByStatus(List<Group> entities);
        List<ExpenseItemModel> MapExpenseItems(List<Expense> expenses);
    }
}