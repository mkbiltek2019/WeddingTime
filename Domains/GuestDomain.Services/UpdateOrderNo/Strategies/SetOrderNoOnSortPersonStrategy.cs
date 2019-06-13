using AIT.GuestDomain.Model.Entities;
using AIT.GuestDomain.Services.UpdateOrderNo.DTO;
using System.Collections.Generic;

namespace AIT.GuestDomain.Services.UpdateOrderNo.Strategies
{
    public class SetOrderNoOnSortPersonStrategy : PersonUpdateOrderNoStrategy
    {
        public override void Execute(UpdatePersonOrderContainer container)
        {
            Group group = container.Group;
            int orderNoFrom = group.FindPerson(container.UpdateOrderNoData.BaseItemId.Value).OrderNo.Value;     // i'm sure that found person hs order no

            List<int> personIdsToUpdate = container.UpdateOrderNoData.PersonIdsToUpdate;

            List<Person> persons = group.FindPersonsWithOrderNumberGraterThan(orderNoFrom, personIdsToUpdate);
            persons.ForEach(n => n.UpdateOrderNo(n.OrderNo.Value + 1));                                         // all persons placed after the one which was inserted

            group.FindPersons(personIdsToUpdate).ForEach(n => n.UpdateOrderNo(orderNoFrom));
        }
    }
}
