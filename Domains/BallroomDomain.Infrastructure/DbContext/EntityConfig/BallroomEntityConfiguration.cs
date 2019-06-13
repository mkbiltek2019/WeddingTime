using AIT.BallroomDomain.Model.Entities;
using System.Data.Entity.ModelConfiguration;

namespace AIT.BallroomDomain.Infrastructure.DbContext.EntityConfig
{
    class BallroomEntityConfiguration : EntityTypeConfiguration<Ballroom>
    {
        public BallroomEntityConfiguration()
        {
            ToTable("Ballroom");

            HasMany(x => x.BallroomItems)
                .WithRequired()
                .HasForeignKey(x => x.BallroomId)
                .WillCascadeOnDelete(true);
        }
    }
}
