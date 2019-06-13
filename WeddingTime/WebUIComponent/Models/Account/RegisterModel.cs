using AIT.WebUtilities.ValidationAttributes;
using System.ComponentModel.DataAnnotations;

namespace AIT.WebUIComponent.Models.Account
{
    public class RegisterModel
    {
        [Required]
        public string Username { get; set; }

        [EmailAddress(ErrorMessage = "Niepoprawny adres email.")]
        [Required(ErrorMessage = "Niepoprawny adres email.")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Pole Hasło jest wymagane.")]
        [StringLength(100, ErrorMessage = "Hasło musi mieć co najmniej {2} znaków.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Hasła różnią się od siebie.")]
        public string ConfirmPassword { get; set; }

        [RequiredTrue(ErrorMessage = "Regulamin musi zostać zaakceptowany.")]
        //[Range(typeof(bool), "true", "true", ErrorMessage = "You gotta tick it!")]
        public bool TermsAccepted { get; set; }
    }
}