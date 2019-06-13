using AIT.DomainUtilities.Database;
using AIT.UndoManagement.Infrastructure.DbContext.ModelsConfig;
using AIT.UndoManagement.Model.DTO;
using System.Data.Entity;

namespace AIT.UndoManagement.Infrastructure.DbContext
{
    public class UndoContext : BaseContext<UndoContext>
    {
        public DbSet<Undo> Undo { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new UndoModelConfiguration());
        }
    }
}
