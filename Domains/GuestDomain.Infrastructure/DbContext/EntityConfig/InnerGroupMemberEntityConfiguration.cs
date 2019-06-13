using AIT.GuestDomain.Model.ValueObjects;
using System.Data.Entity.ModelConfiguration;

namespace AIT.GuestDomain.Infrastructure.DbContext.EntityConfig
{
    class InnerGroupMemberEntityConfiguration : EntityTypeConfiguration<InnerGroupMember>
    {
        public InnerGroupMemberEntityConfiguration()
        {
            HasKey(x => x.PersonId);
            ToTable("InnerGroupMembers");
        }
    }
}
