using System.Collections.Generic;

namespace AIT.ExpenseManagement.Model.DTO
{
    public class UpdateNewOrderNo : UpdateOrderNoBase
    {
        public List<Expense> Expenses { get; set; }
    }
}
