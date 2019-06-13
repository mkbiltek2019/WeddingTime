using AIT.GuestDomain.Model.Entities;
using System;
using System.Collections.Generic;

namespace AIT.GuestDomain.Services
{
    public interface IGroupService
    {
        void Create(Group group);
        void Recreate(Group group);        
        void Delete(Group group);
        void Update(Group groupUpdate);
        List<Group> Get(string userId);                   // used also by ballroom controller, force fetching persons! only persons!
        Group Get(string userId, int groupId);      
        Group GetToDelete(string userId, int groupId);
    }
}
