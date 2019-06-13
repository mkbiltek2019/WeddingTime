using System;
using System.Collections.Generic;

namespace AIT.WebUIComponent.Models.Guests
{
    public class PersonModificationModel
    {
        public int GroupId { get; set; }
        public List<int> ModifiedPersonIds { get; set; }
        public Guid? InnerGroupKey { get; set; }
    }
}