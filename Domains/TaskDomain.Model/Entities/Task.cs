using AIT.TaskDomain.Model.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AIT.TaskDomain.Model.Entities
{
    public class Task
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime? ReminderDate { get; set; }
        public TaskState State { get; set; }

        public virtual List<TaskCard> Cards { get; set; }

        public void Update(Task task)
        {
            Title = task.Title;
            Description = task.Description;
            ReminderDate = task.ReminderDate;
            State = task.State;
        }

        public void UpdateState(TaskState newState)
        {
            State = newState;
        }

        public void AddCard(TaskCard taskCard)
        {
            Cards.Add(taskCard);
        }

        public TaskCard GetCard(int cardId)
        {
            return Cards.Single(n => n.Id == cardId);
        }

        public List<TaskCard> GetCards(IEnumerable<int> idsToExclude)
        {
            return Cards.Where(x => !idsToExclude.Contains(x.Id)).ToList();
        }
    }
}
