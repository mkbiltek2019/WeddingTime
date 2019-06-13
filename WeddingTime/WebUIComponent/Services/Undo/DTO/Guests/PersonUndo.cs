using System;
using System.Xml.Serialization;

namespace AIT.WebUIComponent.Services.Undo.DTO.Guests
{
    [Serializable]
    public class PersonUndo
    {
        [XmlIgnore]
        public int? Id { get; set; }                            // just to help find seat id, will be ignored while serializing

        public Guid UniqueKey { get; set; }                     // used when doing undo - to get new db generated ids for seat that will be assigned to this person
        public int GroupId { get; set; }                        // used in strategies
        public int? OrderNo { get; set; }
        public int Status { get; set; }                         // mapped from enum
        public int Genre { get; set; }
        public int? RelatedPersonId { get; set; }               // if inner group has to be recreated
        public int? TableId { get; set; }                       // if person was assigned to seat on ballroom site remember table id and seat id
        public int? SeatId { get; set; }                        
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }

        public InnerGroupMemberUndo InnerGroupMember { get; set; }

        public bool IsInnerGroupMember
        {
            get { return InnerGroupMember != null; }
        }
    }
}