using AIT.TaskDomain.Model.Entities;
using System.Data.Entity.ModelConfiguration;

namespace AIT.TaskDomain.Infrastructure.DbContext.EntityConfig
{
    class TaskCardItemEntityConfiguration : EntityTypeConfiguration<TaskCardItem>
    {
        public TaskCardItemEntityConfiguration()
        {
            ToTable("TaskCardItems");
        }
    }
}
