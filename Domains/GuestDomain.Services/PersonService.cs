using AIT.DomainUtilities;
using AIT.GuestDomain.Infrastructure.DbContext;
using AIT.GuestDomain.Infrastructure.Repositories;
using AIT.GuestDomain.Model.DTO;
using AIT.GuestDomain.Model.Entities;
using AIT.GuestDomain.Model.Enums;
using AIT.GuestDomain.Model.ValueObjects;
using AIT.GuestDomain.Services.RenewMembership;
using AIT.GuestDomain.Services.RenewMembership.DTO;
using AIT.GuestDomain.Services.UpdateOrderNo;
using AIT.GuestDomain.Services.UpdateOrderNo.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;

namespace AIT.GuestDomain.Services
{
    public class PersonService : IPersonService
    {
        private IGroupRepository _groupRepository;        
        private IUnitOfWork<GuestsContext> _unitOfWork;

        public PersonService(IGroupRepository groupRepository, IUnitOfWork<GuestsContext> unitOfWork)
        {            
            _groupRepository = groupRepository;
            _unitOfWork = unitOfWork;
        }

        public List<Person> Get(string userId, int groupId)
        {
            var group = _groupRepository.LoadEagerWithPersonsMembership(userId, groupId);       // can get persons for default group also
            return group.PersonsInOrder();
        }

        public void Create(string userId, List<Person> persons)
        {
            var groupId = persons.GroupBy(n => n.GroupId).First().Key;                          // persons are always added or deleted (undo) for the same group!
            var group = _groupRepository.LoadEagerWithPersons(userId, groupId);

            persons.ForEach(@group.AddPerson);

            UpdateOrderNo(new UpdatePersonOrderNo
            {
                UpdateType = UpdateOrderNoType.Create,
                PersonIdsToUpdate = new List<int> { 0 }                                         // id does not exist yet - get all items with id = 0
            }, group);

            _unitOfWork.Save();
        }

        public int Recreate(string userId, List<Person> persons, List<RenewItem> renewItems)
        {
            var groupId = persons.GroupBy(n => n.GroupId).First().Key;                          // persons are always added or deleted (undo) for the same group!
            var group = _groupRepository.LoadEagerWithPersonsAndInnerGroups(userId, groupId);

            var renewMembershipService = new RenewMembershipService();
            var specyfication = new RenewSpecification { Group = group };

            foreach (var item in renewItems)
            {
                specyfication.RenewItem = item;
                renewMembershipService.Process(specyfication);
            }

            persons.ForEach(@group.AddPerson);

            _unitOfWork.Save();
            return groupId;
        }

        public void UpdateOrderNo(string userId, int groupId, UpdatePersonOrderNo data)
        {
            var group = _groupRepository.LoadEagerWithPersons(userId, groupId);

            UpdateOrderNo(data, group);
            _unitOfWork.Save();
        }

        public PersonsDeletedData DeleteAndModify(string userId, PersonModificationData data)
        {
            Group group = _groupRepository.LoadEager(userId, data.GroupId);

            var relatedItems = Collect(group, data.ModifiedPersonIds);
            var personsCloned = group.DeepClonePersons(data.ModifiedPersonIds);

            var personsToDelete = group.FindPersons(data.ModifiedPersonIds);

            Delete(personsToDelete);
            DeleteMembership(group, relatedItems.PersonsToModify);
            DeleteInnerGroups(group, relatedItems.InnerGroupsToDelete);

            _unitOfWork.Save();

            return new PersonsDeletedData { Related = relatedItems, Deleted = personsCloned };
        }

        public void DetachMember(string userId, PersonModificationData personData, UpdatePersonOrderNo updateData)
        {
            var group = _groupRepository.LoadEager(userId, personData.GroupId);
            var relatedItems = Collect(group, personData.ModifiedPersonIds);

            DeleteMembership(group, personData.ModifiedPersonIds.First(), personData.InnerGroupKey.Value);
            UpdateOrderNo(updateData, group);

            DeleteMembership(group, relatedItems.PersonsToModify);
            DeleteInnerGroups(group, relatedItems.InnerGroupsToDelete);

            _unitOfWork.Save();
        }

        public void CreateInnerGroupAndMembership(string userId, PersonModificationData personData, UpdatePersonOrderNo updateData)
        {
            var group = _groupRepository.LoadEagerWithPersonsAndInnerGroups(userId, personData.GroupId);

            CreateInnerGroupAndMembershipUncommited(group, personData.InnerGroupKey.Value, personData.ModifiedPersonIds);
            UpdateOrderNo(updateData, group);                                                   // here I update only one element

            _unitOfWork.Save();
        }

        // adding single person to existing inner group
        public void AddInnerGroupMember(string userId, PersonModificationData personData, UpdatePersonOrderNo updateData)
        {
            var group = _groupRepository.LoadEagerWithPersonsAndInnerGroups(userId, personData.GroupId);
            var personId = personData.ModifiedPersonIds.First();

            if (!group.CheckInnerGroupKey(personData.InnerGroupKey.Value) || !group.CheckGroupMember(personId))
                throw new Exception();

            group.CreateInnerGroupMembership(personData.InnerGroupKey.Value, personId);
            UpdateOrderNo(updateData, group);

            _unitOfWork.Save();
        }

        public void Update(string userId, List<Person> persons)                                    // always from the same group! //what if list == 0
        {
            var groupId = persons.GroupBy(n => n.GroupId).First().Key;
            var group = _groupRepository.LoadEagerWithPersons(userId, groupId);

            persons.ForEach(n => group.UpdatePerson(n));

            _unitOfWork.Save();
        }

        public List<Person> Get(string userId, int groupId, List<int> personIds)
        {
            var group = _groupRepository.LoadEagerWithPersonsMembership(userId, groupId);       // can get persons for default group also
            return group.PersonsInOrder().Where(n => personIds.Contains(n.Id)).ToList();        // consider movig where clause inside Group object? FindPersons method?
        }

        public void MoveBetweenGroups(string userId, int groupIdTo, PersonModificationData from, UpdatePersonOrderNo updateData)
        {
            Group groupFrom = _groupRepository.LoadEagerWithPersonsAndInnerGroups(userId, from.GroupId);
            Group groupTo = _groupRepository.LoadEagerWithPersonsAndInnerGroups(userId, groupIdTo);

            if (groupFrom == null || groupTo == null)
                throw new ArgumentException("Invalid group id.");

            var personsToMove = groupFrom.FindPersons(from.ModifiedPersonIds);
            personsToMove.ForEach(n => groupTo.AddPerson(n));

            if (from.InnerGroupKey != null)
            {
                DeleteInnerGroup(groupFrom, from.InnerGroupKey.Value);                          // because inner group is value object remove it and add it again with the same key
                groupTo.AddInnerGroup(from.InnerGroupKey.Value);
            }

            UpdateOrderNo(updateData, groupTo);                                                 // update order no for moved elements
            _unitOfWork.Save();
        }

        private void CreateInnerGroupAndMembershipUncommited(Group group, Guid innerGroupKey, List<int> personIds)
        {
            if (!group.CheckGroupMemebers(personIds))
                throw new Exception();

            group.AddInnerGroup(innerGroupKey);
            group.CreateInnerGroupMemebers(innerGroupKey, personIds);
        }

        private void UpdateOrderNo(UpdatePersonOrderNo data, Group group)
        {
            var updateOrderNoService = new UpdatePersonOrderNoService();
            var container = new UpdatePersonOrderContainer { UpdateOrderNoData = data, Group = group };
            updateOrderNoService.UpdateOrderNo(data.UpdateType, container);
        }

        private PersonsRelatedItems Collect(Group group, List<int> modifiedPersonIds)
        {
            var keys = group.GetInnerGroupKeys(modifiedPersonIds);

            var relatedItems = new PersonsRelatedItems();
            keys.ForEach(n => GetInnerGroupRelatedItems(n, group, modifiedPersonIds, relatedItems));

            return relatedItems;
        }

        private void GetInnerGroupRelatedItems(Guid innerGroupKey, Group group, List<int> idsToExclude, PersonsRelatedItems relatedItems)
        {
            var members = group.GetRelatedInnerGroupMemebers(innerGroupKey, idsToExclude);

            if (members.Count == 0 || members.Count == 1)                   // nie wiem dlaczego .Count == 0?
                relatedItems.InnerGroupsToDelete.Add(innerGroupKey);

            if (members.Count == 1)
                relatedItems.PersonsToModify.Add(new PersonItem { Id = members.First().Id, InnerGroupKey = innerGroupKey });
        }

        public void Delete(List<Person> persons)
        {
            persons.ForEach(p =>        // http://stackoverflow.com/questions/17723626/entity-framework-remove-vs-deleteobject
                {
                    if (p.HasMembership)
                        _unitOfWork.Context.Entry(p.InnerGroupMember).State = EntityState.Deleted;

                    _unitOfWork.Context.Entry(p).State = EntityState.Deleted;
                });
        }

        // I can delete one of two members from two different inner groups - person.Count > 1 
        // that is why I created private function and I check for each item
        private void DeleteMembership(Group group, List<PersonItem> persons)
        {
            persons.ForEach(n => DeleteInnerGroupMembership(group, n));
        }

        private void DeleteMembership(Group group, int personId, Guid innerGroupKey)
        {
            DeleteMembership(group, new List<PersonItem> { new PersonItem { Id = personId, InnerGroupKey = innerGroupKey } });
        }

        private void DeleteInnerGroups(Group group, List<Guid> keys)
        {
            keys.ForEach(n => DeleteInnerGroup(group, n));
        }

        private void DeleteInnerGroupMembership(Group group, PersonItem person)
        {
            var innerGroupExists = group.CheckInnerGroupKey(person.InnerGroupKey.Value);
            var memberExists = group.CheckInnerGroupMemeber(person.InnerGroupKey.Value, person.Id);

            if (!innerGroupExists || !memberExists)
                throw new Exception();

            group.RemoveInnerGroupMembership(person.Id);            
        }

        private void DeleteInnerGroup(Group group, Guid innerGroupKey)
        {
            bool innerGroupExists = group.CheckInnerGroupKey(innerGroupKey);

            if (!innerGroupExists)
                throw new ArgumentException("Invalid inner group key.");

            InnerGroup innerGroup = group.FindInnerGroup(innerGroupKey);
            _unitOfWork.Context.Entry(innerGroup).State = EntityState.Deleted;
        }
    }
}
