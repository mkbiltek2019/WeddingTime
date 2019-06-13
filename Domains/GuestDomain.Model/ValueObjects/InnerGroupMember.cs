using System;

namespace AIT.GuestDomain.Model.ValueObjects
{
    public class InnerGroupMember
    {
        public InnerGroupMember()
        {
        }

        public InnerGroupMember(Guid innerGroupKey)
        {
            InnerGroupKey = innerGroupKey;
        }

        public int PersonId { get; private set; }
        public Guid InnerGroupKey { get; private set; }
    }
}
