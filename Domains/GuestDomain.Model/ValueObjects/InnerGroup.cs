using System;

namespace AIT.GuestDomain.Model.ValueObjects
{
    public class InnerGroup
    {
        public InnerGroup()
        {
        }

        public InnerGroup(Guid key)
        {
            GroupKey = key;
        }

        public int Id { get; private set; }
        public int GroupId { get; private set; }
        public Guid GroupKey { get; private set; }
    }
}
