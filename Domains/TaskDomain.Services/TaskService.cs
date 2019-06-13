using AIT.DomainUtilities;
using AIT.TaskDomain.Infrastructure.DbContext;
using AIT.TaskDomain.Infrastructure.Repositories;
using AIT.TaskDomain.Model.Entities;
using AIT.TaskDomain.Model.Enums;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;

namespace AIT.TaskDomain.Services
{
    public class TaskService : ITaskService
    {
        private ITaskRepository _taskRepository;
        private IUnitOfWork<TaskContext> _unitOfWork;

        public TaskService(ITaskRepository taskRepository, IUnitOfWork<TaskContext> unitOfWork)
        {
            _taskRepository = taskRepository;
            _unitOfWork = unitOfWork;
        }

        public void Create(Task task)
        {    
            _taskRepository.Insert(task);
            _unitOfWork.Save();
        }

        public void Update(string userId, Task taskUpdate)
        {
            Task task = _taskRepository.LoadEager(userId, taskUpdate.Id);                // can't invoke eager load together with CardItems, only Cards can be fetched eagerly...if I get also CardItems I will get an exception that null value for items can't be set - then I would have to also mark CardItems as deleted

            List<TaskCard> toDelete = task.GetCards(taskUpdate.Cards.Select(x => x.Id));
            toDelete.ForEach(card =>
            {
                SetEntityDeleteState(card);
            });

            task.Update(taskUpdate);
            _unitOfWork.Save();
        }

        public void UpdateState(string userId, int taskId, TaskState newState)
        {
            Task task = _taskRepository.Load(userId, taskId);
            task.UpdateState(newState);

            _unitOfWork.Save();
        }

        public Task GetTask(string userId, int taskId)
        {
            return _taskRepository.LoadEagerWithItems(userId, taskId);
        }

        public Task GetToDelete(string userId, int taskId)
        {
            return _taskRepository.LoadEagerWithItems(userId, taskId);
        }

        public void Delete(Task task)
        {            
            _taskRepository.Delete(task);
            _unitOfWork.Save();
        }

        public List<Task> Get(string userId)
        {
            return _taskRepository.Find(userId).ToList();
        }

        public void AddTaskCard(string userId, int taskId, TaskCard taskCard)
        {
            Task task = _taskRepository.LoadEager(userId, taskId);
            task.AddCard(taskCard);

            _unitOfWork.Save();
        }

        public void UpdateTaskCard(string userId, int taskId, TaskCard taskCard)
        {
            Task task = _taskRepository.LoadEagerWithItems(userId, taskId);

            TaskCard card = task.GetCard(taskCard.Id);
            card.Update(taskCard);

            List<TaskCardItem> toDelete = card.GetItems(taskCard.Items.Where(x => x.HasIdValue).Select(x => x.Id));
            toDelete.ForEach(item =>
            {
                SetEntityDeleteState(item);
            });

            card.AddItems(taskCard.Items.Where(x => !x.HasIdValue));            // first delete then add new items, the order is important to not remove already added items
            _unitOfWork.Save();
        }

        public TaskCard GetCard(string userId, int taskId, int cardId)
        {
            Task task = _taskRepository.LoadEagerWithItems(userId, taskId);
            return task.GetCard(cardId);
        }

        private void SetEntityDeleteState(object item)
        {
            _unitOfWork.Context.Entry(item).State = EntityState.Deleted;
        }
    }
}
