using AIT.TaskDomain.Model.Enums;
using System;

namespace AIT.TaskDomain.Model.Entities
{
    public class TaskCardItem
    {
        public int Id { get; set; }
        public int CardId { get; set; }
        public string Value { get; set; }
        public ItemType Type { get; set; }

        public bool HasIdValue
        {
            get { return Id != 0; }
        }
    }
}
