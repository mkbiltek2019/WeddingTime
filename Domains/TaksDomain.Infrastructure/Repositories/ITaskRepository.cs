using AIT.DomainUtilities.Repositories;
using AIT.TaskDomain.Model.Entities;
using System.Linq;

namespace AIT.TaskDomain.Infrastructure.Repositories
{
    public interface ITaskRepository : IRepositoryBase<Task>
    {
        Task LoadEagerWithItems(string userId, int taskId);
        Task LoadEager(string userId, int taskId);
        Task Load(string userId, int taskId);
        IQueryable<Task> Find(string userId);
    }
}
