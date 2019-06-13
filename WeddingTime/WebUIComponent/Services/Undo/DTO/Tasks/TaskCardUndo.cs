using System.Collections.Generic;

namespace AIT.WebUIComponent.Services.Undo.DTO.Tasks
{
    public class TaskCardUndo
    {
        public string Title { get; set; }
        public List<TaskCardItemUndo> Items { get; set; }
    }
}