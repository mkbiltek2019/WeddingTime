using AIT.TaskDomain.Model.Entities;
using System.Data.Entity.ModelConfiguration;

namespace AIT.TaskDomain.Infrastructure.DbContext.EntityConfig
{
    class TaskCardEntityConfiguration : EntityTypeConfiguration<TaskCard>
    {
        public TaskCardEntityConfiguration()
        {
            ToTable("TaskCards");

            HasMany(x => x.Items)
                .WithRequired()
                .HasForeignKey(x => x.CardId)
                .WillCascadeOnDelete(true);
        }
    }
}
