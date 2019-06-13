namespace AIT.WebUIComponent.Services.Undo.DTO.Expenses
{
    public class ExpenseUndo
    {
        public string UserId { get; set; }
        public string Description { get; set; }
        public int? OrderNo { get; set; }
        public int? Quantity { get; set; }
        public decimal? UnitPrice { get; set; }
        public decimal Price { get; set; }
    }
}