using AIT.GuestDomain.Model.Entities;
using System.Collections.Generic;

namespace AIT.GuestDomain.Model.DTO
{
    public class PersonsDeletedData
    {
        public List<Person> Deleted { get; set; }
        public PersonsRelatedItems Related { get; set; }
    }
}
