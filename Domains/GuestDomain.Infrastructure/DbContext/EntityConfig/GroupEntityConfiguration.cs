using AIT.GuestDomain.Model.Entities;
using System.Data.Entity.ModelConfiguration;

namespace AIT.GuestDomain.Infrastructure.DbContext.EntityConfig
{
    class GroupEntityConfiguration : EntityTypeConfiguration<Group>
    {
        public GroupEntityConfiguration()
        {
            ToTable("Groups");

            HasMany(x => x.Persons)
                .WithRequired()
                .HasForeignKey(x => x.GroupId)
                .WillCascadeOnDelete(true);

            HasMany(x => x.InnerGroups)
                .WithRequired()
                .HasForeignKey(x => x.GroupId)
                .WillCascadeOnDelete(true);
        }
    }
}
