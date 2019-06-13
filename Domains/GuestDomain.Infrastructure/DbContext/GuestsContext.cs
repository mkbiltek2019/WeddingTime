using AIT.DomainUtilities.Database;
using AIT.GuestDomain.Infrastructure.DbContext.EntityConfig;
using AIT.GuestDomain.Model.Entities;
using AIT.GuestDomain.Model.ValueObjects;
using System.Data.Entity;

namespace AIT.GuestDomain.Infrastructure.DbContext
{
    public class GuestsContext : BaseContext<GuestsContext>
    {
        public DbSet<Group> Groups { get; set; }
        public DbSet<Person> Persons { get; set; }
        public DbSet<InnerGroup> InnerGroups { get; set; }
        public DbSet<InnerGroupMember> InnerGroupMembers { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new GroupEntityConfiguration())
                                       .Add(new PersonEntityConfiguration())
                                       .Add(new InnerGroupEntityConfiguration())
                                       .Add(new InnerGroupMemberEntityConfiguration());
        }
    }
}
