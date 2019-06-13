using AIT.ExpenseManagement.Model.DTO;
using AIT.GuestDomain.Model.Entities;
using AIT.GuestDomain.Model.Enums;
using AIT.WebUIComponent.Models.Pdf;
using AutoMapper;
using System.Linq;

namespace AIT.WebUIComponent.AutoMapping
{
    public class PdfMap
    {
        public static void CreateMap()
        {
            Mapper.CreateMap<Group, GroupModel>()
                .ForMember(n => n.Name, o => o.ResolveUsing(x => x.Name.ToUpper()))
                .ForMember(n => n.Persons, o => o.ResolveUsing(x => x.Persons.OrderBy(y => y.OrderNo)));                
            Mapper.CreateMap<Group, GroupByStatusModel>()
                .ForMember(n => n.Name, o => o.ResolveUsing(x => x.Name.ToUpper()))
                .ForMember(n => n.PersonsAccepted, o => o.ResolveUsing(x => x.Persons.Where(p => p.Status == ConfirmationStatus.Accepted).OrderBy(y => y.OrderNo)))
                .ForMember(n => n.PersonsDeclined, o => o.ResolveUsing(x => x.Persons.Where(p => p.Status == ConfirmationStatus.Declined).OrderBy(y => y.OrderNo)))
                .ForMember(n => n.PersonsUnconfirmed, o => o.ResolveUsing(x => x.Persons.Where(p => p.Status == ConfirmationStatus.Unconfirmed).OrderBy(y => y.OrderNo)));

            Mapper.CreateMap<Person, PersonModel>()
                .ForMember(n => n.FullName, o => o.ResolveUsing(x => string.Format("{0} {1}", x.Name, x.Surname).TrimEnd()));

            Mapper.CreateMap<Expense, ExpenseItemModel>();
        }
    }
}