using System;
using System.Collections.Generic;

namespace AIT.GuestDomain.Model.DTO
{
    public class PersonModificationData
    {
        public int GroupId { get; set; }       
        public List<int> ModifiedPersonIds { get; set; }
        public Guid? InnerGroupKey { get; set; }                        //can be new or for update
    }
}
