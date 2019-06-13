namespace AIT.WebUIComponent.Models.Pdf
{
    public class ExpenseItemModel
    {
        public string Description { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }        
        public decimal Price { get; set; }

        public bool HasQuantity
        {
            get { return Quantity != 0; }
        }

        public bool HasUnitPrice
        {
            get { return UnitPrice != 0; }
        }
    }
}