using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIT.GuestDomain.Services.RenewMembership.DTO
{
    public class RenewItem
    {
        public int? RelatedPersonId { get; set; }
        public Guid? InnerGroupKey { get; set; }

        public bool IsInnerGroupMember
        {
            get { return InnerGroupKey.HasValue; }
        }

        public bool RenewRelatedPersonGroupMembership
        {
            get { return RelatedPersonId.HasValue; }
        }
    }
}
