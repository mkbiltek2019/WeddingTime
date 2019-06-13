using AIT.DomainUtilities;
using AIT.DomainUtilities.Repositories;
using AIT.TaskDomain.Infrastructure.DbContext;
using AIT.TaskDomain.Model.Entities;
using System.Data.Entity;
using System.Linq;

namespace AIT.TaskDomain.Infrastructure.Repositories
{
    public class TaskRepository : RepositoryBase<Task, TaskContext>, ITaskRepository
    {
        public TaskRepository(IUnitOfWork<TaskContext> unitOfWork)
            : base(unitOfWork)
        {
        }

        public Task LoadEagerWithItems(string userId, int taskId)
        {
            return Context.Tasks.Include(x => x.Cards.Select(n => n.Items)).Single(n => n.UserId == userId && n.Id == taskId);
        }

        public Task LoadEager(string userId, int taskId)
        {
            return Context.Tasks.Include(x => x.Cards).Single(n => n.UserId == userId && n.Id == taskId);
        }

        public Task Load(string userId, int taskId)
        {
            return GetAll().Single(n => n.UserId == userId && n.Id == taskId);
        }

        public IQueryable<Task> Find(string userId)
        {
            return GetAll().Where(x => x.UserId == userId);
        }
    }
}
