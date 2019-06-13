using AIT.UserDomain.Model.Entities;
using System.Data.Entity.ModelConfiguration;

namespace AIT.UserDomain.Infrastructure.DbContext.EntityConfig
{
    internal class UserEntityConfiguration : EntityTypeConfiguration<User>
    {
        internal UserEntityConfiguration()
        {
            ToTable("Users");
            HasKey(n => n.Id);

            HasOptional(x => x.OAuthMembership)
                .WithRequired()
                //.Map(x => x.MapKey(new[] { "UserId" }))
                .WillCascadeOnDelete(true);
        }
    }
}
