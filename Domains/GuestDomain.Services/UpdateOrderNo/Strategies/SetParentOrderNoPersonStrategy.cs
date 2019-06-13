using AIT.GuestDomain.Model.Entities;
using AIT.GuestDomain.Services.UpdateOrderNo.DTO;
using System.Collections.Generic;

namespace AIT.GuestDomain.Services.UpdateOrderNo.Strategies
{
    public class SetParentOrderNoPersonStrategy : PersonUpdateOrderNoStrategy
    {
        public override void Execute(UpdatePersonOrderContainer container)
        {
            Group group = container.Group;
            int parentOrderNo = group.FindPerson(container.UpdateOrderNoData.BaseItemId.Value).OrderNo.Value;

            List<Person> persons = group.FindPersons(container.UpdateOrderNoData.PersonIdsToUpdate);
            persons.ForEach(n => n.UpdateOrderNo(parentOrderNo));
        }
    }
}
