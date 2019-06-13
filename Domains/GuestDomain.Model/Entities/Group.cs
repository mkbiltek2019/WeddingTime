using AIT.GuestDomain.Model.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AIT.GuestDomain.Model.Entities
{
    public class Group
    {
        public Group()
        {
            Persons = new List<Person>();
            InnerGroups = new List<InnerGroup>();
        }
        
        public int Id { get; private set; }        
        public int? OrderNo { get; private set; }
        public string UserId { get; private set; }
        public string Name { get; private set; }

        public virtual List<Person> Persons { get; private set; }
        public virtual List<InnerGroup> InnerGroups { get; private set; }

        public void SetUserId(string userId)
        {
            UserId = userId;
        }

        public void AddPerson(Person person)
        {
            Persons.Add(person);
        }

        public void AddInnerGroup(Guid key)
        {
            var innerGroup = new InnerGroup(key);
            InnerGroups.Add(innerGroup);
        }

        public List<Person> DeepClonePersons(List<int> personIds)
        {
            return Persons.Where(n => personIds.Contains(n.Id))
                          .Select(n => n.FullCopy())
                          .ToList();
        }

        public List<Person> PersonsInOrder()
        {
            return Persons.OrderBy(n => n.OrderNo).ToList();
        }

        public void Update(string name)
        {
            Name = name;
        }

        public void UpdateOrderNo(int orderNo)
        {
            OrderNo = orderNo;
        }

        public void UpdatePerson(Person personUpdate)
        {
            var person = FindPerson(personUpdate.Id);
            person.Update(personUpdate);
        }

        public void CreateInnerGroupMembership(Guid innerGroupKey, int personId)
        {
            var guest = FindPerson(personId);
            guest.CreateInnerGroupMembership(innerGroupKey);
        }

        public void RemoveInnerGroupMembership(int personId)
        {
            var guest = FindPerson(personId);
            guest.RemoveInnerGroupMembership();
        }

        public void CreateInnerGroupMemebers(Guid innerGroupKey, List<int> guestIds)
        {
            FindPersons(guestIds).ForEach(n => n.CreateInnerGroupMembership(innerGroupKey));
        }

        public List<Person> FindPersons(List<int> ids)                          // change person to members ???
        {
            return Persons.Where(n => ids.Contains(n.Id)).ToList();
        }

        public List<Person> GetRelatedInnerGroupMemebers(Guid innerGroupKey, List<int> idsToExclude)
        {
            return Persons.Where(n => !idsToExclude.Contains(n.Id) && n.HasMembership && n.InnerGroupMember.InnerGroupKey == innerGroupKey).ToList();
        }

        public bool CheckGroupMemebers(List<int> personIds)
        {
            return personIds.All(n => Persons.Select(x => x.Id).Contains(n));
        }

        public bool CheckGroupMember(int personId)
        {
            return Persons.Exists(n => n.Id == personId);
        }

        public bool CheckInnerGroupMemeber(Guid innerGroupKey, int guestId)
        {
            return Persons.Exists(n => n.HasMembership && n.InnerGroupMember.InnerGroupKey == innerGroupKey && n.Id == guestId);
        }

        public bool CheckInnerGroupKey(Guid innerGroupKey)
        {
            return InnerGroups.Exists(n => n.GroupKey == innerGroupKey);
        }

        public int? GetMaxPersonOrderNo(List<int> idsToExclude)
        {
            return Persons.Where(n => !idsToExclude.Contains(n.Id)).Max(n => n.OrderNo);
        }

        public Person FindPerson(int id)
        {
            return Persons.Single(n => n.Id == id);
        }

        public List<Person> FindPersonsWithOrderNumberGraterThan(int orderNoFrom, List<int> idsToExclude)
        {
            return Persons.Where(n => n.OrderNo >= orderNoFrom && !idsToExclude.Contains(n.Id)).ToList();
        }

        public List<Guid> GetInnerGroupKeys(List<int> personIds)
        {
            return Persons.Where(n => personIds.Contains(n.Id) && n.HasMembership).Select(n => n.InnerGroupMember.InnerGroupKey).Distinct().ToList();
        }

        public InnerGroup FindInnerGroup(Guid key)
        {
            return InnerGroups.Single(n => n.GroupKey == key);
        }
    }
}
