using System;
using System.Collections.Generic;

namespace AIT.WebUIComponent.Services.Undo.DTO.Tasks
{
    public class TaskUndo
    {
        public string UserId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime? ReminderDate { get; set; }
        public int State { get; set; }                      // corresponds to TaskState enum in card domain

        public List<TaskCardUndo> Cards { get; set; }
    }
}