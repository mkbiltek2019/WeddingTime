using AIT.GuestDomain.Services.UpdateOrderNo.DTO;

namespace AIT.GuestDomain.Services.UpdateOrderNo.Strategies
{
    public abstract class PersonUpdateOrderNoStrategy
    {
        public abstract void Execute(UpdatePersonOrderContainer data);
    }
}