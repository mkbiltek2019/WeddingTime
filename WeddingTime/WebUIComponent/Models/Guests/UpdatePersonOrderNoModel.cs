using AIT.WebUIComponent.Models.Shared;
using System.Collections.Generic;

namespace AIT.WebUIComponent.Models.Guests
{
    public class UpdatePersonOrderNoModel : UpdateOrderNoBaseModel
    {
        public List<int> PersonIdsToUpdate { get; set; }
    }
}
