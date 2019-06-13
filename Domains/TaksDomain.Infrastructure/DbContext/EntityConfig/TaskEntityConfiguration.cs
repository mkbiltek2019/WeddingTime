using AIT.TaskDomain.Model.Entities;
using System.Data.Entity.ModelConfiguration;

namespace AIT.TaskDomain.Infrastructure.DbContext.EntityConfig
{
    class TaskEntityConfiguration : EntityTypeConfiguration<Task>
    {
        public TaskEntityConfiguration()
        {
            ToTable("Tasks");

            HasMany(x => x.Cards)
                .WithRequired()
                .HasForeignKey(x => x.TaskId)
                .WillCascadeOnDelete(true);
        }
    }
}
