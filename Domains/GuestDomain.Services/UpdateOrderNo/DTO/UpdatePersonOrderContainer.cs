using AIT.GuestDomain.Model.DTO;
using AIT.GuestDomain.Model.Entities;

namespace AIT.GuestDomain.Services.UpdateOrderNo.DTO
{
    public class UpdatePersonOrderContainer
    {
        public UpdatePersonOrderNo UpdateOrderNoData { get; set; }
        public Group Group { get; set; }
    }
}
