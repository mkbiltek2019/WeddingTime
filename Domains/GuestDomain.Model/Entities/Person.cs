using AIT.GuestDomain.Model.Enums;
using AIT.GuestDomain.Model.ValueObjects;
using System;

namespace AIT.GuestDomain.Model.Entities
{
    public class Person
    {
        public int Id { get; private set; }
        public Guid? UniqueKey { get; private set; }                // is only assigned by automaper when we create new person
        public int GroupId { get; private set; }
        public int? OrderNo { get; private set; }
        public string Name { get; private set; }
        public string Surname { get; private set; }
        public string Phone { get; private set; }
        public string Email { get; private set; }
        public string Address { get; private set; }
        public ConfirmationStatus Status { get; private set; }
        public PersonGenre Genre { get; private set; }

        public virtual InnerGroupMember InnerGroupMember { get; private set; }

        public bool HasMembership
        {
            get { return InnerGroupMember != null; }
        }

        public void Update(Person personUpdate)
        {
            Name = personUpdate.Name;
            Surname = personUpdate.Surname;
            Phone = personUpdate.Phone;
            Address = personUpdate.Address;
            Email = personUpdate.Email;
            Genre = personUpdate.Genre;
            Status = personUpdate.Status;
        }

        public void UpdateOrderNo(int orderNo)
        {
            OrderNo = orderNo;
        }

        public void CreateInnerGroupMembership(Guid innerGroupKey)
        {
            InnerGroupMember = new InnerGroupMember(innerGroupKey);            
        }

        public void RemoveInnerGroupMembership()
        {
            InnerGroupMember = null;
        }

        public Person Copy()
        {
            return new Person
            {
                Name = Name,
                Surname = Surname,
                Phone = Phone,
                Email = Email,
                Address = Address,
                Status = Status,
                Genre = Genre
            };
        }

        public Person FullCopy()
        {
            return new Person
            {
                Id = Id,                                            // id and key is needed for preparing undo data
                UniqueKey = UniqueKey,
                GroupId = GroupId,
                OrderNo = OrderNo,
                Name = Name,
                Surname = Surname,
                Phone = Phone,
                Email = Email,
                Address = Address,
                Status = Status,
                Genre = Genre,
                InnerGroupMember = InnerGroupMember
            };
        }
    }
}
