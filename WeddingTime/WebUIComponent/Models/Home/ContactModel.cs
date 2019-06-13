using System.ComponentModel.DataAnnotations;

namespace AIT.WebUIComponent.Models.Home
{
    public class ContactModel
    {        
        [Required]
        public string Name { get; set; }

        [EmailAddress(ErrorMessage = "Niepoprawny adres email.")]
        [Required(ErrorMessage = "Niepoprawny adres email.")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Pole Tytuł jest wymagane")]
        [StringLength(50, ErrorMessage = "Tytuł nie może być dłuższy niż 50 znaków")]
        public string Subject { get; set; }

        [Required(ErrorMessage = "Pole Treść jest wymagane")]
        [StringLength(1000, ErrorMessage = "Wiadomość nie może być dłuższa niż 1000 znaków")]
        public string Body { get; set; }
    }
}