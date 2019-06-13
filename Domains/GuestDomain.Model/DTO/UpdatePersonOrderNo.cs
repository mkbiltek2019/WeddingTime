using AIT.GuestDomain.Model.Enums;
using System.Collections.Generic;

namespace AIT.GuestDomain.Model.DTO
{
    public class UpdatePersonOrderNo
    {
        public int? BaseItemId { get; set; }                        // based on this id I get order number based on which I find elements that has greater order no and update them
        public UpdateOrderNoType UpdateType { get; set; }
        public List<int> PersonIdsToUpdate { get; set; }
    }
}
