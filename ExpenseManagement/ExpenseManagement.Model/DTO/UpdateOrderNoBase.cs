using AIT.ExpenseManagement.Model.Enums;

namespace AIT.ExpenseManagement.Model.DTO
{
    public class UpdateOrderNoBase
    {
        public string UserId { get; set; }        
        public UpdateOrderNoType UpdateType { get; set; }
    }
}
