
namespace AIT.UserDomain.Services.Interfaces
{
    public interface IAuthUserService
    {
        string CreateUserAndAccount(string username, string password, string email);
        string CreateAccount(string username, string password);
        bool DeleteUserAndAccount(string username);
        bool ChangePassword(string username, string currentPassword, string newPassword);
        bool ConfirmAccount(string username, string confirmationToken);
        bool ConfirmAccount(string confirmationToken);
        bool SignIn(string username, string password, bool persistCookie = false);
        void SignOut();
    }
}
