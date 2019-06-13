using AIT.GuestDomain.Model.Entities;
using System.Data.Entity.ModelConfiguration;

namespace AIT.GuestDomain.Infrastructure.DbContext.EntityConfig
{
    class PersonEntityConfiguration : EntityTypeConfiguration<Person>
    {
        public PersonEntityConfiguration()
        {
            ToTable("Persons");           

            HasOptional(x => x.InnerGroupMember)
                .WithRequired()
                .WillCascadeOnDelete(true);
        }
    }
}
