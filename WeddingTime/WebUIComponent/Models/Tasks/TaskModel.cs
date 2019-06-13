using AIT.TaskDomain.Model.Enums;
using AIT.WebUtilities.DTO;
using AIT.WebUtilities.Helpers;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AIT.WebUIComponent.Models.Tasks
{
    public class TaskModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public int State { get; set; }

        [Required]
        public string Title { get; set; }

        public string Description { get; set; }

        [DataType(DataType.Date)]
        public string ReminderDate { get; set; }

        public List<TaskCardModel> Cards { get; set; }

        public List<DropDownItem> TaskStateCollection
        {
            get { return DropDownCollectionHelpers.CreateDropDownCollection<TaskState>(); }
        }
    }
}