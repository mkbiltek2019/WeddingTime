using AIT.ExpenseManagement.Model.DTO;
using AIT.GuestDomain.Model.Entities;
using AIT.WebUIComponent.Models.Pdf;
using AutoMapper;
using System.Collections.Generic;

namespace AIT.WebUIComponent.Services.AutoMapper
{
    public class PdfAutoMapperService : IPdfAutoMapperService
    {
        public List<GroupModel> MapGroupEntities(List<Group> entities)
        {
            return Mapper.Map<List<Group>, List<GroupModel>>(entities);
        }

        public List<GroupByStatusModel> MapGroupEntitiesByStatus(List<Group> entities)
        {
            return Mapper.Map<List<Group>, List<GroupByStatusModel>>(entities);
        }

        public List<ExpenseItemModel> MapExpenseItems(List<Expense> expenses)
        {
            return Mapper.Map<List<Expense>, List<ExpenseItemModel>>(expenses);
        }
    }
}