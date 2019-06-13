namespace AIT.ExpenseManagement.Model.DTO
{
    public class Expense
    {
        public int Id { get; set; }
        public int? OrderNo { get; set; }
        public int? Quantity { get; set; }
        public string UserId { get; set; }
        public string Description { get; set; }
        public decimal? UnitPrice { get; set; }
        public decimal Price { get; set; }
    }
}
