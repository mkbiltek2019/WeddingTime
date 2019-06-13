using System.ComponentModel.DataAnnotations;

namespace AIT.WebUIComponent.Models.Expenses
{
    public class BudgetModel
    {
        public int Id { get; set; }       

        [Required]
        [RegularExpression(@"^\d+\,?\d{0,2}$")]     //[RegularExpression(@"^\d+\,?\d{0,2}$")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public string Value { get; set; }
    }
}