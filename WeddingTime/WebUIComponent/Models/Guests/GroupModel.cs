using System.ComponentModel.DataAnnotations;

namespace AIT.WebUIComponent.Models.Guests
{
    public class GroupModel
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}