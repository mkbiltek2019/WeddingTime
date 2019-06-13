using AIT.DomainUtilities.Database;
using AIT.UserDomain.Infrastructure.DbContext.EntityConfig;
using AIT.UserDomain.Model.Entities;
using System.Data.Entity;

namespace AIT.UserDomain.Infrastructure.DbContext
{
    public class UsersContext : BaseContext<UsersContext>
    {
        public DbSet<User> Users { get; set; }
        public DbSet<OAuthMembership> OAuthMembership { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new UserEntityConfiguration());
            modelBuilder.Configurations.Add(new OAuthMembershipEntityConfiguration());
        }
    }
}
