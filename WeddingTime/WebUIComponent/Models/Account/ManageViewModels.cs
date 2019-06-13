using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;

namespace AIT.WebUIComponent.Models.Account
{
    public class IndexViewModel
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public bool HasPassword { get; set; }
        public int LoginsCount { get; set; }
    }

    public class ManageLoginsViewModel
    {
        public IList<UserLoginInfo> CurrentLogins { get; set; }
        public IList<AuthenticationDescription> OtherLogins { get; set; }
    }

    public class SetPasswordViewModel
    {
        [Required(ErrorMessage = "Pole Nowe hasło jest wymagane.")]
        [StringLength(100, ErrorMessage = "Hasło musi mieć co najmniej {2} znaków.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage = "Hasła różnią się od siebie.")]
        public string ConfirmPassword { get; set; }
    }

    public class ChangePasswordViewModel
    {
        [Required(ErrorMessage = "Pole Stare hasło jest wymagane")]
        [DataType(DataType.Password)]
        public string OldPassword { get; set; }

        [Required(ErrorMessage = "Pole Nowe hasło jest wymagane")]
        [StringLength(100, ErrorMessage = "Hasło musi mieć co najmniej {2} znaków", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage = "Hasła różnią się od siebie")]        
        public string ConfirmPassword { get; set; }
    }    

    public class ChangeEmailViewModel
    {
        [EmailAddress(ErrorMessage = "Niepoprawny adres email.")]
        [Required(ErrorMessage = "Niepoprawny adres email.")]
        public string Email { get; set; }
    }
}