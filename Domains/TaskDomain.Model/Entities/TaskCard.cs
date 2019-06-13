using System.Collections.Generic;
using System.Linq;

namespace AIT.TaskDomain.Model.Entities
{
    public class TaskCard
    {
        public int Id { get; set; }
        public int TaskId { get; set; }
        public string Title { get; set; }

        public virtual List<TaskCardItem> Items { get; set; }

        public void Update(TaskCard taskCard)
        {
            Title = taskCard.Title;
        }

        public void AddItems(IEnumerable<TaskCardItem> items)
        {
            Items.AddRange(items);
        }

        public List<TaskCardItem> GetItems(IEnumerable<int> idsToExclude)
        {
            return Items.Where(x => !idsToExclude.Contains(x.Id)).ToList();
        }
    }
}
