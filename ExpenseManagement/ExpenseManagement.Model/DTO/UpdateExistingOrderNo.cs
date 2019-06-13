namespace AIT.ExpenseManagement.Model.DTO
{
    public class UpdateExistingOrderNo : UpdateOrderNoBase
    {
        public int UpdateId { get; set; }
        public int? BaseItemId { get; set; }
    }
}
