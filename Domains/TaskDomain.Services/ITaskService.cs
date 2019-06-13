using AIT.TaskDomain.Model.Entities;
using AIT.TaskDomain.Model.Enums;
using System.Collections.Generic;

namespace AIT.TaskDomain.Services
{
    public interface ITaskService
    {
        void Create(Task task);
        void Update(string userId, Task task);                                 // I can update only one at the time
        void UpdateState(string userId, int taskId, TaskState newState);
        void Delete(Task task);
        Task GetTask(string userId, int taskId);
        Task GetToDelete(string userId, int taskId);
        TaskCard GetCard(string userId, int taskId, int cardId);
        List<Task> Get(string userId);        

        void AddTaskCard(string userId, int taskId, TaskCard taskCard);
        void UpdateTaskCard(string userId, int taskId, TaskCard taskCard);
    }
}
