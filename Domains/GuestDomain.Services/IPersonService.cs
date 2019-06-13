using AIT.GuestDomain.Model.DTO;
using AIT.GuestDomain.Model.Entities;
using AIT.GuestDomain.Services.RenewMembership.DTO;
using System.Collections.Generic;

namespace AIT.GuestDomain.Services
{
    public interface IPersonService
    {
        List<Person> Get(string userId, int groupId);
        List<Person> Get(string userId, int groupId, List<int> personIds);        
        PersonsDeletedData DeleteAndModify(string userId, PersonModificationData data);        
        void Create(string userId, List<Person> persons);
        void CreateInnerGroupAndMembership(string userId, PersonModificationData personData, UpdatePersonOrderNo updateData);
        void Update(string userId, List<Person> persons);      
        void UpdateOrderNo(string userId, int groupId, UpdatePersonOrderNo data);
        void DetachMember(string userId, PersonModificationData personData, UpdatePersonOrderNo updateData);
        void AddInnerGroupMember(string userId, PersonModificationData personData, UpdatePersonOrderNo updateData);
        void MoveBetweenGroups(string userId, int groupIdTo, PersonModificationData from, UpdatePersonOrderNo updateData);                
        int Recreate(string userId, List<Person> persons, List<RenewItem> renewItems);
    }
}
