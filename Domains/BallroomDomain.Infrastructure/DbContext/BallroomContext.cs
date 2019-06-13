using AIT.BallroomDomain.Infrastructure.DbContext.EntityConfig;
using AIT.BallroomDomain.Model.Entities;
using AIT.DomainUtilities.Database;
using System.Data.Entity;

namespace AIT.BallroomDomain.Infrastructure.DbContext
{
    public class BallroomContext : BaseContext<BallroomContext>
    {
        public DbSet<Ballroom> Ballroom { get; set; }
        public DbSet<BallroomItem> BallromItems { get; set; }
        public DbSet<Seat> Seats { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new BallroomEntityConfiguration())
                                       .Add(new BallroomItemEntityConfiguration())
                                       .Add(new BallroomTableBaseEntityConfiguration());
        }
    }
}
