using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AIT.WebUIComponent.Models.Account
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        public string Username { get; set; }

        public string LoginProvider { get; set; }
    }

    public class ExternalLoginListViewModel
    {
        public string ReturnUrl { get; set; }
    }

    public class SendCodeViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
        public string ReturnUrl { get; set; }
        public bool RememberMe { get; set; }
    }
    
    public class ResetPasswordViewModel
    {
        [Required]
        public string UserId { get; set; }

        [Required(ErrorMessage = "Pole Nowe hasło jest wymagane.")]
        [StringLength(100, ErrorMessage = "Hasło musi mieć co najmniej {2} znaków.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Hasła różnią się od siebie.")]
        public string ConfirmPassword { get; set; }

        [Required]
        public string Code { get; set; }
    }

    public class ForgotPasswordViewModel
    {
        [EmailAddress(ErrorMessage = "Niepoprawny adres email.")]
        [Required(ErrorMessage = "Niepoprawny adres email.")]
        public string Email { get; set; }
    }
}
