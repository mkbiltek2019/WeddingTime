using AIT.GuestDomain.Model.Enums;
using AIT.GuestDomain.Services.UpdateOrderNo.DTO;

namespace AIT.GuestDomain.Services.UpdateOrderNo
{
    public interface IUpdatePersonOrderNoService
    {
        void UpdateOrderNo(UpdateOrderNoType key, UpdatePersonOrderContainer data);
    }
}
