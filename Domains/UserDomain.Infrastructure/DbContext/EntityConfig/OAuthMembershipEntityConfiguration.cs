using AIT.UserDomain.Model.Entities;
using System.Data.Entity.ModelConfiguration;

namespace AIT.UserDomain.Infrastructure.DbContext.EntityConfig
{
    internal class OAuthMembershipEntityConfiguration : EntityTypeConfiguration<OAuthMembership>
    {
        internal OAuthMembershipEntityConfiguration()
        {
            ToTable("webpages_OAuthMembership");
            HasKey(x => x.UserId);                  // new { x.Provider, x.ProviderUserId });            
        }
    }
}
