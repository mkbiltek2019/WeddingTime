using System.Collections.Generic;
using System.Linq;

namespace AIT.WebUIComponent.Models.Pdf
{
    public class ExpensesModel
    {
        public List<ExpenseItemModel> Expenses { get; set; }

        public decimal Sum
        {
            get { return Expenses.Sum(n => n.Price); }
        }
    }
}