using AIT.GuestDomain.Model.Entities;
using AIT.GuestDomain.Services.UpdateOrderNo.DTO;
using System.Collections.Generic;

namespace AIT.GuestDomain.Services.UpdateOrderNo.Strategies
{
    public class SetOrderNoOnCreatePersonStrategy : PersonUpdateOrderNoStrategy
    {        
        public override void Execute(UpdatePersonOrderContainer container)
        {
            Group group = container.Group;
            List<int> idsToUpdate = container.UpdateOrderNoData.PersonIdsToUpdate;

            int? orderNo = group.GetMaxPersonOrderNo(idsToUpdate);
            int maxOrderNo = orderNo.HasValue ? orderNo.Value + 1 : 0;

            List<Person> persons = group.FindPersons(idsToUpdate);
            persons.ForEach(n => n.UpdateOrderNo(maxOrderNo++));
        }
    }
}
