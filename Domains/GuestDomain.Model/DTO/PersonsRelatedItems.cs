using System;
using System.Collections.Generic;

namespace AIT.GuestDomain.Model.DTO
{
    public class PersonsRelatedItems
    {
        public PersonsRelatedItems()
        {
            PersonsToModify = new List<PersonItem>();
            InnerGroupsToDelete = new List<Guid>();
        }

        public List<PersonItem> PersonsToModify { get; private set; }
        public List<Guid> InnerGroupsToDelete { get; private set; }

        public bool HasPersons
        {
            get { return PersonsToModify.Count > 0; }
        }

        public bool HasGroups
        {
            get { return InnerGroupsToDelete.Count > 0; }
        }
    }
}
