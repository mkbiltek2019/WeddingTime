namespace AIT.WebUIComponent.Models.Expenses
{
    public class ExpensesInfoModel
    {
        public BudgetModel Budget { get; set; }             // needed for an update view, to have a value and id
        public decimal TotalPrice { get; set; }

        public decimal BudgetLeft
        {
            get { return decimal.Parse(Budget.Value) - TotalPrice; }
        }
    }
}