using AIT.BallroomDomain.Model.Entities;
using System.Data.Entity.ModelConfiguration;

namespace AIT.BallroomDomain.Infrastructure.DbContext.EntityConfig
{
    class BallroomTableBaseEntityConfiguration : EntityTypeConfiguration<TableBase>
    {
        public BallroomTableBaseEntityConfiguration()
        {
            HasMany(x => x.Seats)
                .WithRequired()
                .HasForeignKey(x => x.TableId)
                .WillCascadeOnDelete(true);
        }
    }
}
