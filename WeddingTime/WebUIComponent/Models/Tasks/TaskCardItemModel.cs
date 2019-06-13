using System.ComponentModel.DataAnnotations;

namespace AIT.WebUIComponent.Models.Tasks
{
    public class TaskCardItemModel
    {
        public int? Id { get; set; }

        [Required]
        public string Value { get; set; }

        [Required]
        public string Type { get; set; }       
    }
}
