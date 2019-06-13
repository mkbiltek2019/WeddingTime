using AIT.WebUtilities.ValidationAttributes;
using AIT.WebUtilities.ValidationAttributes.Enum;
using System.ComponentModel.DataAnnotations;

namespace AIT.WebUIComponent.Models.Expenses
{
    public class ExpenseItemModel
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Nazwa/Opis")]
        public string Description { get; set; }

        [RequiredIf("UnitPrice", RequiredIfComparison.IsNotEqualTo, "", AllowMultiple = true)]
        [RegularExpression(@"^[0-9]*$")]
        [Display(Name = "Ilość")]
        public string Quantity { get; set; }

        [RequiredIf("Quantity", RequiredIfComparison.IsNotEqualTo, "", AllowMultiple = true)]       /*ErrorMessage = "TEKST PRZETŁUMACZONY"*/
        [RegularExpression(@"^\d+\,?\d{0,2}$")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        [Display(Name = "Cena jednostkowa")]
        public string UnitPrice { get; set; }

        [Required]
        [RegularExpression(@"^\d+\,?\d{0,2}$")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        [Display(Name = "Wartość")]
        public string Price { get; set; }
    }
}