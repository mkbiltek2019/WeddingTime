using AIT.GuestDomain.Model.ValueObjects;
using System.Data.Entity.ModelConfiguration;

namespace AIT.GuestDomain.Infrastructure.DbContext.EntityConfig
{
    class InnerGroupEntityConfiguration : EntityTypeConfiguration<InnerGroup>
    {
        public InnerGroupEntityConfiguration()
        {
            ToTable("InnerGroups");          
        }
    }
}
