using AIT.DomainUtilities.Database;
using AIT.TaskDomain.Infrastructure.DbContext.EntityConfig;
using AIT.TaskDomain.Model.Entities;
using System.Data.Entity;

namespace AIT.TaskDomain.Infrastructure.DbContext
{
    public class TaskContext : BaseContext<TaskContext>
    {
        public DbSet<Task> Tasks { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new TaskEntityConfiguration())
                                       .Add(new TaskCardEntityConfiguration())
                                       .Add(new TaskCardItemEntityConfiguration());
        }
    }
}
