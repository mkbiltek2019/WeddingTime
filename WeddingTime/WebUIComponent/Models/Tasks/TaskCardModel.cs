using AIT.UtilitiesComponents.Services;
using AIT.WebUIComponent.Services.Tasks;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AIT.WebUIComponent.Models.Tasks
{
    public class TaskCardModel : IValidatableObject
    {
        public int? Id { get; set; }
        public int TaskId { get; set; }                             // needed for card template

        public bool? HasPhoneItems { get; set; }                    // this info is filled in during mapping
        public bool? HasContactItems { get; set; }
        public bool? HasAddressItems { get; set; }
        public bool? HasLinkItems { get; set; }
        public bool? HasEmailItems { get; set; }

        [Required]
        public string Title { get; set; }

        public List<TaskCardItemModel> Items { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var service = UnityService.Get().Container().GetInstance<ITaskCardItemsService>();

            if (Items != null)
            {
                foreach (var item in Items)
                {
                    if (service.Validate(item.Type, item.Value))
                        continue;

                    yield return new ValidationResult(string.Format("Invalid {0} format", item.Type), new[] { item.Type });
                }
            }
        }
    }    
}