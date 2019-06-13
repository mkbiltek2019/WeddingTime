using AIT.GuestDomain.Model.Entities;
using AIT.GuestDomain.Services.UpdateOrderNo.DTO;
using System.Collections.Generic;

namespace AIT.GuestDomain.Services.UpdateOrderNo.Strategies
{
    public class SetMaxOrderNoPersonStrategy : PersonUpdateOrderNoStrategy
    {
        public override void Execute(UpdatePersonOrderContainer container)
        {
            Group group = container.Group;
            List<int> idsToUpdate = container.UpdateOrderNoData.PersonIdsToUpdate;

            int? maxOrderNo = group.GetMaxPersonOrderNo(idsToUpdate);

            List<Person> persons = group.FindPersons(idsToUpdate);
            persons.ForEach(n => n.UpdateOrderNo(maxOrderNo.HasValue ? maxOrderNo.Value + 1 : 0));
        }
    }
}
